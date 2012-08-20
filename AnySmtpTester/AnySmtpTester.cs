using System;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;

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
        /// <summary>
        /// test the smtp connection by sending a HELO command
        /// </summary>
        /// <param name="smtpServerAddress"></param>
        /// <param name="port"></param>
        public static bool TestConnection(string smtpServerAddress, int port)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(smtpServerAddress);
            var endPoint = new IPEndPoint(hostEntry.AddressList[0], port);
            using (var tcpSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                //try to connect and test the rsponse for code 220 = success
                tcpSocket.Connect(endPoint);
                if (!CheckResponse(tcpSocket, 220))
                {
                    return false;
                }

                // send HELO and test the response for code 250 = proper response
                SendData(tcpSocket, string.Format("HELO {0}\r\n", Dns.GetHostName()));

                return CheckResponse(tcpSocket, 250);

                // if we got here it's that we can connect to the smtp server
            }
        }

        private static void SendData(Socket socket, string data)
        {
            var dataArray = Encoding.ASCII.GetBytes(data);
            socket.Send(dataArray, 0, dataArray.Length, SocketFlags.None);
        }

        private static bool CheckResponse(Socket socket, int expectedCode)
        {
            while (socket.Available == 0)
            {
                System.Threading.Thread.Sleep(100);
            }
            var responseArray = new byte[1024];
            socket.Receive(responseArray, 0, socket.Available, SocketFlags.None);
            var responseData = Encoding.ASCII.GetString(responseArray);
            var responseCode = Convert.ToInt32(responseData.Substring(0, 3));
            return responseCode == expectedCode;
        }
    }
}
