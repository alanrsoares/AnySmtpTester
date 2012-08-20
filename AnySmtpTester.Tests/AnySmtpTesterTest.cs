using AnySmtpTester;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Mail;

namespace AnySmtpTester.Tests
{
    
    
    /// <summary>
    ///This is a test class for AnySmtpTesterTest and is intended
    ///to contain all AnySmtpTesterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AnySmtpTesterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AnySmtpTester Constructor
        ///</summary>
        [TestMethod()]
        public void AnySmtpTesterConstructorTest()
        {
            SmtpClient smtpClient = null; // TODO: Initialize to an appropriate value
            AnySmtpTester target = new AnySmtpTester(smtpClient);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AnySmtpTester Constructor
        ///</summary>
        [TestMethod()]
        public void AnySmtpTesterConstructorTest1()
        {
            string host = string.Empty; // TODO: Initialize to an appropriate value
            int port = 0; // TODO: Initialize to an appropriate value
            bool enableSsl = false; // TODO: Initialize to an appropriate value
            string user = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            AnySmtpTester target = new AnySmtpTester(host, port, enableSsl, user, password);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetServerStatus
        ///</summary>
        [TestMethod()]
        public void GetServerStatusTest()
        {
            SmtpClient smtpClient = null; // TODO: Initialize to an appropriate value
            AnySmtpTester target = new AnySmtpTester(smtpClient); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetServerStatus();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SendMessage
        ///</summary>
        [TestMethod()]
        public void SendMessageTest()
        {
            SmtpClient smtpClient = null; // TODO: Initialize to an appropriate value
            AnySmtpTester target = new AnySmtpTester(smtpClient); // TODO: Initialize to an appropriate value
            string from = string.Empty; // TODO: Initialize to an appropriate value
            string to = string.Empty; // TODO: Initialize to an appropriate value
            string subject = string.Empty; // TODO: Initialize to an appropriate value
            string body = string.Empty; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.SendMessage(from, to, subject, body);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SendTestMessage
        ///</summary>
        [TestMethod()]
        public void SendTestMessageTest()
        {
            SmtpClient smtpClient = null; // TODO: Initialize to an appropriate value
            AnySmtpTester target = new AnySmtpTester(smtpClient); // TODO: Initialize to an appropriate value
            string from = string.Empty; // TODO: Initialize to an appropriate value
            string to = string.Empty; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.SendTestMessage(from, to);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Client
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AnySmtpTester.dll")]
        public void ClientTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            AnySmtpTester_Accessor target = new AnySmtpTester_Accessor(param0); // TODO: Initialize to an appropriate value
            SmtpClient expected = null; // TODO: Initialize to an appropriate value
            SmtpClient actual;
            target.Client = expected;
            actual = target.Client;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
