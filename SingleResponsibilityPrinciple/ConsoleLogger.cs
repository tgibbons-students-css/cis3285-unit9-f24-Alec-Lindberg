using System;
using System.IO;
using System.Xml.Linq;
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class ConsoleLogger : ILogger
    {
        public void LogWarning(string message, params object[] args)
        {
            Console.WriteLine(string.Concat("WARN: ", message), args);
        }

        public void LogInfo(string message, params object[] args)
        {
            Console.WriteLine(string.Concat("INFO: ", message), args);
        }

        private void LogMessage(string type, string message, params object[] args)
        {
            // Format the message with any additional arguments
            string formattedMessage = string.Format(message, args);

            // Console logging
            Console.WriteLine($"{type}: {formattedMessage}");

            // XML file logging
            LogToXmlFile(type, formattedMessage);
        }

        private void LogToXmlFile(string type, string message)
        {
            // Construct XML log entry
            XElement logEntry = new XElement("log",
                new XElement("type", type),
                new XElement("message", message)
            );

            // Append the log entry to the XML log file
            using (StreamWriter logfile = File.AppendText("log.xml"))
            {
                logfile.WriteLine(logEntry);
            }
        }
    }
}
