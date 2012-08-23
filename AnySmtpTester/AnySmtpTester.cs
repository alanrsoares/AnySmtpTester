using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AnySmtpTester
{
    public class AnySmtpTester
    {
        #region Attributes

        #endregion

        #region Properties

        public SmtpClient Client { get; private set; }

        #endregion

        #region Public Methods

        #region Constructors
        public AnySmtpTester(SmtpClient smtpClient)
        {
            Client = smtpClient;
        }

        public AnySmtpTester(string host, int port, bool enableSsl, string user, string password)
        {
            Client = new SmtpClient
                         {
                             Host = host,
                             Port = port,
                             EnableSsl = enableSsl,
                             UseDefaultCredentials = false,
                             Credentials = new NetworkCredential(user, password)
                         };
        }

        #endregion

        public string GetServerStatus()
        {
            return "";
        }

        public object SendMessage(string from, string to, string subject, string body)
        {
            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentNullException("from", "Required parameter (from) is missing.");
            }
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException("to", "Required parameter (to) is missing.");
            }
            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException("body", "Required parameter (body) is missing.");
            }

            var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject ?? "",
                Body = body,
            };

            message.To.Add(new MailAddress(to));

            try
            {
                Client.Send(message);
            }
            catch (Exception e)
            {
                return new { Success = false, e.Message };
            }

            return new { Success = true };
        }

        public object SendTestMessage(string from, string to)
        {
            return SendMessage(from, to, "[AnySmtpTester] - SendTestMessage", "Test Message from AnySmtpTester.");
        }

        #endregion

        #region Private Methods


        #endregion

    }


    public static class SmtpHelper
    {
        public static bool TestSmtpServer(string hostName, int hostPort)
        {
            var responseData = GetResponse(hostName, hostPort, "EHLO");
            return CheckResponse(responseData, new[] { 220, 250 });
        }

        public static string GetResponse(string hostName, int hostPort, string message)
        {

            try
            {
                return GetSslResponse(hostName, hostPort, message);
            }
            catch (IOException)
            {
                try
                {
                    return GetNonSslResponse(hostName, hostPort, message);
                }
                catch (SocketException socket)
                {
                    return "SocketException: " + socket.Message;
                }
            }
            catch (SocketException socket)
            {
                return "SocketException: " + socket.Message;
            }
            catch (Exception e)
            {
                return "GenericException: " + e.Message;
            }
        }

        public static string GetSslResponse(string hostName, int hostPort, string message)
        {
            using (var client = new TcpClient())
            {
                client.Connect(hostName, hostPort);

                using (var stream = client.GetStream())
                using (var sslStream = new SslStream(stream, false))
                {
                    sslStream.AuthenticateAsClient(hostName);
                    using (var writer = new StreamWriter(sslStream))
                    using (var reader = new StreamReader(sslStream))
                    {
                        writer.WriteLine("{0} {1}\r\n", message, Dns.GetHostName());
                        writer.Flush();
                        return reader.ReadLine();
                    }
                }
            }
        }

        public static string GetNonSslResponse(string hostName, int hostPort, string message)
        {
            try
            {
                //FallBack to non-ssl test

                var hostEntry = Dns.GetHostEntry(hostName);
                var endPoint = new IPEndPoint(hostEntry.AddressList[0], hostPort);


                using (var tcpSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    tcpSocket.Connect(endPoint);
                    if (!CheckResponse(GetResponse(tcpSocket), 220))
                    {
                        return string.Format("Unexpected Response from host {0} at port {1}", hostName, hostPort);
                    }

                    SendData(tcpSocket, String.Format("{0} {1}\r\n", message, Dns.GetHostName()));

                    return GetResponse(tcpSocket);
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static bool CheckResponse(string responseData, int expectedCode)
        {
            return (ParseResponseCode(responseData) == expectedCode);
        }

        private static bool CheckResponse(string responseData, IEnumerable<int> expectedCodes)
        {
            return expectedCodes.Contains(ParseResponseCode(responseData));
        }

        private static int ParseResponseCode(string responseData)
        {
            int i;
            var responseCode = string.IsNullOrEmpty(responseData) || !int.TryParse(responseData.Substring(0, 3), out i)
                ? 0
                : i;

            return responseCode;
        }

        private static string GetResponse(Socket socket)
        {
            const int maxTimeOut = 5000;
            const int sleepTime = 100;
            var timeOut = 0;

            while (socket.Available == 0 && timeOut < maxTimeOut)
            {
                Thread.Sleep(sleepTime);
                timeOut += sleepTime;
            }

            if (timeOut >= maxTimeOut)
                return "";

            var responseArray = new byte[1024];
            socket.Receive(responseArray, 0, socket.Available, SocketFlags.None);
            var responseData = Encoding.ASCII.GetString(responseArray);

            return responseData;

        }

        private static void SendData(Socket socket, string data)
        {
            var dataArray = Encoding.ASCII.GetBytes(data);
            socket.Send(dataArray, 0, dataArray.Length, SocketFlags.None);
        }

        public static void SendMail(SmtpClient client, string from, string to, string subject, string body)
        {
            var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject ?? "",
                Body = body,
            };

            message.To.Add(new MailAddress(to));

            client.Send(message);

        }

    }


}
