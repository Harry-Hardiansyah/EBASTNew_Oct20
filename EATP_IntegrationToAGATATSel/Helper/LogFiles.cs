using System;
using System.Configuration;
using System.IO;

namespace ConsoleApplication1.Helper
{
    public class LogFiles
    {
        public static void createLogFile(string labelName, string contentName)
        {
            string fileName = ConfigurationManager.AppSettings["logPath"] + "EATPLOG_AGATA_TSEL_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";

            try
            {
                if (!File.Exists(fileName))
                {
                    using (FileStream fs = File.Create(fileName))
                    {
                        using (var fw = new StreamWriter(fs))
                        {
                            fw.WriteLine(labelName + " : " + contentName);
                        }
                    }
                }
                else
                {
                    using (var fw = new StreamWriter(fileName, true))
                    {
                        fw.WriteLine(labelName + " : " + contentName);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}
