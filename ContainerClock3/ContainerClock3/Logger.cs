using System;
using System.IO;
using System.Reflection;

namespace ContainerClock3
{
    public class Logger
    {
        private string path = string.Empty;

        public void LogWrite(string Message)
        {
            path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(path + "\\" + "log.txt"))
                {
                    Log(Message, w);
                }
            }
            catch (Exception e)
            {
                //TODO: handle Error
                throw;

            }
        }

        private void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                string text = "Entry "+ DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString() + " : " + logMessage;
                txtWriter.WriteLine(text);
            }
            catch (Exception e)
            {
                //TODO: handle Error
                throw;
            }
        }
    }
}
