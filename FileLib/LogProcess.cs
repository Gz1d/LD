using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.IO;

namespace FileLib
{
    internal class LogProcess
    {
        public bool IsRunning { get { return IsThreadLive; } }

        private bool IsThreadLive = false;
        private List<KeyValuePair<string, string>> LogKeyPairList = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> ErrorKeyPairList = new List<KeyValuePair<string, string>>();
        private object AddLock = new object();
        private Thread selfThread;
        private bool ThreadSwitch = false;
        private int LogNO;
        private string DefaultFileName,DefaultErrorFileName="Error";
        private string BaseFilePath = @"D:\Program Files\LD\Log\";//basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private object LogLock = new object();
        private static LogProcess LogInstance = null;

        public static LogProcess Instance
        {
            get
            {
                if (LogInstance == null)
                {
                    LogInstance = new LogProcess();
                    LogInstance.Init();
                }

                return LogInstance;
            }
        }

         ~LogProcess()
        {
            if (IsThreadLive)
            {
                IsThreadLive = false;
                selfThread.Join();
            }
             
            Console.WriteLine(this.GetType().ToString()+"退出！");
        }

        private void Init()
        {
            LogNO = 0;
            ThreadSwitch = true;
            selfThread = new Thread(new ParameterizedThreadStart(RunProcess));
            selfThread.IsBackground = true;
            selfThread.Name = this.GetType().ToString();
            selfThread.Start();
        }

        public void AddLog(string content, bool isError, string fileName)
        {
            lock (AddLock)
            {
                if (isError)
                {
                    ErrorKeyPairList.Add(new KeyValuePair<string, string>(fileName, content));
                }
                LogKeyPairList.Add(new KeyValuePair<string, string>(fileName, content));
            }
        }

        private void RunProcess(object obj)
        {
            IsThreadLive = true;
            KeyValuePair<string, string> logkeyPair=new KeyValuePair<string,string>();
            KeyValuePair<string, string> errorKeyPair=new KeyValuePair<string,string>();

            while(true)
            {
                lock(AddLock)
                {
                    if (LogKeyPairList.Count > 0)
                    {
                        logkeyPair = LogKeyPairList[0];
                        LogKeyPairList.RemoveAt(0);
                    }

                    if(ErrorKeyPairList.Count>0)
                    {
                        errorKeyPair = ErrorKeyPairList[0];
                        ErrorKeyPairList.RemoveAt(0);
                    }
                }

                if (!string.IsNullOrEmpty(logkeyPair.Value))
                {
                    PopLog(logkeyPair.Value,logkeyPair.Key);
                    logkeyPair = new KeyValuePair<string, string>();
                }

                if (!string.IsNullOrEmpty(errorKeyPair.Value))
                {
                    PopError(errorKeyPair.Value, errorKeyPair.Key);
                    errorKeyPair = new KeyValuePair<string, string>();
                }

                if (!ThreadSwitch) break;
                Thread.Sleep(10);
            }

            IsThreadLive = false;
        }

        private void PopLog(string logContent,string fileName = null)
        {
            string logFileName = fileName;
            
            if (!DirectoryEx.Exist(BaseFilePath))
            {
                if (!DirectoryEx.Create(BaseFilePath)) return;
            }
            string dataString = DateTime.Now.ToString("yyyy-MM-dd");//log文件路径;
            if (!DirectoryEx.Exist(BaseFilePath + dataString))
            {
                DirectoryEx.Create(BaseFilePath + dataString);
                LogNO = 0;
            }

            if (LogNO == 0)
            {
                DefaultFileName = DateTime.Now.Hour.ToString() + "点"; //log文件名
            }
            LogNO++;

            string[] logText = new string[] { DateTime.Now.ToString("HH:mm:ss;fff") + ": " + logContent };
            if (string.IsNullOrEmpty(logFileName))
            {
                logFileName = DefaultFileName + ".txt";
            }
            else
            {
                logFileName = logFileName+ ".txt";
            }
            long start = System.Environment.TickCount;
            try
            {
               File.AppendAllLines(BaseFilePath + dataString + "\\" + logFileName, logText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            long end = System.Environment.TickCount;

            if ((end - start) > 1)
            {
                Console.WriteLine("配置文件写入耗时：{0}ms -----" + logContent, end - start);
            }
        }

        private void PopError(string logContent, string fileName = null)
        {
            long start = System.Environment.TickCount;

            string logFileName = fileName;

            //if (!DirectoryEx.Exist(BaseFilePath))
            //{
            //    DirectoryEx.Create(BaseFilePath);
            //}
            string dataString = DateTime.Now.ToString("yyyy-MM-dd");//log文件路径;

            //if (!DirectoryEx.Exist(BaseFilePath + dataString))
            //{
            //    DirectoryEx.Create(BaseFilePath + dataString);
            //}
            string[] logText = new string[] { DateTime.Now.ToString("HH:mm:ss;fff") + ": "+ logContent };
            if (string.IsNullOrEmpty(logFileName))
            {
                logFileName = DefaultErrorFileName + ".txt";
            }
            else
            {
                logFileName = logFileName + "_" + DefaultErrorFileName + ".txt";
            }

            try
            {
                File.AppendAllLines(BaseFilePath + dataString + "\\" + logFileName, logText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            long end = System.Environment.TickCount;

            if ((end - start) > 1)
            {
                Console.WriteLine("配置文件写入耗时：{0}ms -----" + logContent, end - start);
            }
        }
    }
}
