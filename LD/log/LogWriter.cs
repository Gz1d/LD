using System;
using System.IO;

namespace LD.Log

{
    /// <summary>
    /// LogWriter 的摘要说明。
    /// 日志类
    /// </summary>
    public class LogWriter
    {
        static LogWriter()
        {
            CreateDirectoryLog();
        }

        public static void CreateDirectoryLog()
        {
            //创建文件夹
            if (!Directory.Exists(@"log"))
                Directory.CreateDirectory(@"log");
            if (!Directory.Exists(@"log\Run"))
                Directory.CreateDirectory(@"log\Run");
            if (!Directory.Exists(@"log\Socket"))
                Directory.CreateDirectory(@"log\Socket");
            if (!Directory.Exists(@"log\Vision"))
                Directory.CreateDirectory(@"log\Vision");
        }

        /// <summary>
        /// error文件名
        /// </summary>
        private const string ErrorFile = @"log\Error.txt";

        /// <summary>
        /// log文件名
        /// </summary>
        public static string LogFile
        {
            get
            {
                return string.Format(@"log\Run\Run_{0}.txt", DateTime.Now.Day.ToString("00"));   //@"log\System.txt";
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="file"></param>
        /// <param name="content"></param>
        private static void Write(string file, string content)
        {
            try
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.FileInfo fi = new FileInfo(file);
                    if (fi.CreationTime.Date.ToShortDateString() != DateTime.Now.Date.ToShortDateString())
                        System.IO.File.Delete(file);

                    //if (fi.Length > 5 * 1024 * 1024)
                    //{
                    //    System.IO.File.Delete(file);
                    //}
                }

                StreamWriter writer = new StreamWriter(file, true, new System.Text.UnicodeEncoding());
                writer.WriteLine(content);
                writer.Flush();
                writer.Close();
            }
            catch 
            {
                CreateDirectoryLog();
            }
        }

        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteException(Exception ex)
        {
            try
            {
                StreamWriter writer = new StreamWriter(ErrorFile, true, new System.Text.UnicodeEncoding());
                writer.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss fff] "));
                writer.WriteLine(ex.TargetSite);
                writer.WriteLine(ex.StackTrace);
                writer.WriteLine(ex.Message);
                writer.Flush();
                writer.Close();
            }
            catch
            {
                CreateDirectoryLog();
            }
        }
        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteException(string format, params object[] args)
        {
            try
            {
                string time = DateTime.Now.ToString("[yy-MM-dd HH:mm:ss fff] ");
                string data = String.Format(format, args);
                string logStr = time + data;

                Write(LogWriter.ErrorFile, logStr);
            }
            catch { }
        }

        /// <summary>
        /// socket日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteSocketLog(string format, params object[] args)
        {
            try
            {
                string time = DateTime.Now.ToString("[yy-MM-dd HH:mm:ss fff] ");
                string data = String.Format(format, args);
                string logStr = time + data;
                string file = string.Format(@"log\Socket\Socket_{0}.txt", DateTime.Now.Day.ToString ("00"));

                Write(file, logStr);
            }
            catch { }
        }

        public static void WriteVisionLog(string format, params object[] args)
        {
            try
            {
                string time = DateTime.Now.ToString("[yy-MM-dd HH:mm:ss fff] ");
                string data = String.Format(format, args);
                string logStr = time + data;
                string file = string.Format(@"log\Vision\Vision_{0}.txt", DateTime.Now.Day.ToString("00"));

                Write(file, logStr);
            }
            catch { }
        }

        /// <summary>
        /// 写一般日志
        /// </summary>
        /// <param name="file"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteEnum(string file, string format, params object[] args)
        {
            string data = String.Format(format, args);
            string logStr = data;
            Write(file, logStr);
        }


        /// <summary>
        /// 写一般日志
        /// </summary>
        /// <param name="file"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteLog(string format, params object[] args)
        {
            string time = DateTime.Now.ToString("[yy-MM-dd HH:mm:ss fff] ");
            string data = String.Format(format, args);
            string logStr = time + data;
            Write(LogWriter.LogFile, logStr);
        }
    }
}
