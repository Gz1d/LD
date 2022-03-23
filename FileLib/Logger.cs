using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Threading;

namespace FileLib
{
    public enum LogLevel
    {
        All,
        Information,
        Debug,
        Success,
        Failure,
        Warning,
        Error
    }

    public class Logger 
    {
        public delegate void LoggerHappenedDelegate(string content, bool isError);
        public static event LoggerHappenedDelegate OnLogHappenedEvent;
        private static  Object ObjLock=new object();

        public readonly static List<string> LogContentList = new List<string>();
        public readonly static List<string> ErrorContentList = new List<string>();
        /// Write log to log file
        /// </summary>
        /// <param name="logContent">Log content</param>
        /// <param name="logType">Log type</param>
        public static void Pop(string logContent,bool isError=false, string fileName = null)
        {
            if (!LogProcess.Instance.IsRunning) return;

            lock (ObjLock)
            {
                if (OnLogHappenedEvent != null) OnLogHappenedEvent(logContent, isError);
            }
            LogProcess.Instance.AddLog(logContent, isError, fileName);

            if (LogContentList.Count > 500)
                LogContentList.Clear();

            LogContentList.Add(logContent);
            if (isError)
            {
                ErrorContentList.Add(logContent);
            }

            if (true)
            {
                Console.WriteLine(logContent);
            }         
        }
    }
}
