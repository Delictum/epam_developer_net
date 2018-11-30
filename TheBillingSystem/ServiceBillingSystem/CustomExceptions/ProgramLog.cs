using System;
using System.IO;

namespace ServiceBillingSystem.CustomExceptions
{
    public class ProgramLog
    {
        private static readonly object Sync = new object();
        private static string _path;
        public static FileInfo FileInfo;

        public ProgramLog(string fileLog)
        {
            _path = fileLog;

            if (!File.Exists(_path))
            {
                using (StreamWriter sw = File.AppendText(_path))
                {
                    sw.WriteLine(string.Join(string.Empty, "***START ", DateTime.Now, "***"));
                }
            }
        }

        internal static void FileEx()
        {
            string newFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now + ".old");
            FileInfo = new FileInfo(_path);
            if (FileInfo.Length > 102400)
            {
                if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now + ".old")))
                {
                    File.Move(_path, newFile);
                    File.SetCreationTime(newFile, DateTime.Now);
                }
                else
                {
                    FileInfo.Delete();
                }
            }
        }

        internal static void Info(string msg)
        {
            FileEx();
            using (StreamWriter sw = File.AppendText(_path))
            {
                lock (Sync)
                {
                    sw.WriteLine("{0} [INFO]  {1}", DateTime.Now, msg);
                }
            }
        }
        internal static void Exception(string msg)
        {
            FileEx();
            using (StreamWriter sw = File.AppendText(_path))
            {
                lock (Sync)
                {
                    sw.WriteLine("{0} [EXCEPTION]  {1}", DateTime.Now, msg);
                }
            }
        }
    }
}
