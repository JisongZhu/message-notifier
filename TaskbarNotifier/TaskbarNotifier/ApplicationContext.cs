using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TaskbarNotifier
{
    public class ApplicationContext
    {
        private static string connectionString = "";

        private static string serverHost = "";

        private static int timerSpan = 0;

        private static CredentialModel credential = new CredentialModel();

        private static string filePath = System.Windows.Forms.Application.StartupPath + "\\config.xml";

        private ApplicationContext()
        {
        }

        public static string ConnectionString()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                XmlNode rootNode = xmlDoc.SelectSingleNode("config");
                connectionString = rootNode.FirstChild.InnerText;
            }

            return connectionString;
        }

        public static string ServerHost()
        {
            if (string.IsNullOrEmpty(serverHost))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                XmlNode rootNode = xmlDoc.SelectSingleNode("config");
                serverHost = rootNode.ChildNodes[1].InnerText;
            }

            return serverHost;
        }

        public static int TimerSpan()
        {
            if (timerSpan == 0)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                XmlNode rootNode = xmlDoc.SelectSingleNode("config");
                timerSpan = string.IsNullOrEmpty(rootNode.ChildNodes[2].InnerText) ? 5000 : Convert.ToInt32(rootNode.ChildNodes[2].InnerText);
            }
            return timerSpan;
        }

        public static CredentialModel Credential
        {
            get
            {
                return credential;
            }
            set
            {
                credential = value;
            }
        }

        public static void Logout()
        {
            credential = new CredentialModel();
        }
    }
}
