using LD.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LD.Logic
{

    /// <summary>
    /// 系统处理
    /// </summary>
    public class LogHandle
    {
        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static LogHandle instance = new LogHandle();
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private LogHandle()
        {


        }

        /// <summary>
        /// 静态属性
        /// </summary>
        public static LogHandle Instance
        {
            get { return instance; }
        }
        #endregion


        public Config.ConfigLog Config
        {
            get
            {
                return ConfigManager.Instance.ConfigLog;
            }
        }



        /// <summary>
        /// 写工作日志
        /// </summary>
        /// <param name="machine">机台</param>
        /// <param name="device">设备</param>
        /// <param name="note_format">描述</param>
        /// <param name="args">参数</param>
        public void WriteRunLog(string machine, string device, string format, params object[] args)
        {
            try
            {
                //写日志，表格显示
                Log.Runlog log = new Log.Runlog();
                log.MachineID = machine;
                log.Time = DateTime.Now;
                log.Device = device;
                log.Notes = String.Format(format, args);

                this.WriteRunLog(log);
            }
            catch { }
        }

        /// <summary>
        /// 写运行日志
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="note_format">描述</param>
        /// <param name="args">参数</param>
        public void WriteRunLog( string device, string format, params object[] args)
        {
            try
            {
                //写日志，表格显示
                Log.Runlog log = new Log.Runlog();
                log.MachineID = "SYS";
                log.Time = DateTime.Now;
                log.Device = device;
                log.Notes = String.Format(format, args);
                this.WriteRunLog(log);
            }
            catch { }
        }


        object log_lock = new object();

        /// <summary>
        /// 运行日志
        /// </summary>
        /// <param name="log"></param>
        public  void WriteRunLog(Log.Runlog log)
        {
            try
            {
                    //Ui.frmMdi .frm .Invoke(new EventHandler(delegate
                    //{
                        try
                        {
                            lock (log_lock)
                            {
                                if (this.Config.WriteLogRun)
                                {
                                    if (this.Config.RunLogs.Count < this.Config.LogRunCount)
                                    {
                                        this.Config.RunLogs.Insert(0, log);
                                    }
                                    else
                                    {
                                        this.Config.RunLogs.RemoveAt(this.Config.RunLogs.Count - 1);
                                        this.Config.RunLogs.Insert(0, log);
                                    }
                                }
                            }
                        }
                        catch { }
                    //}));

                //写日志，文本记录
                Log.LogWriter.WriteLog("{0}:{1};{2}", log.MachineID, log.Device, log.Notes);
            }
            catch
            { 
            
            }
        }


        /// <summary>
        /// 写视觉日志
        /// </summary>
        /// <param name="log"></param>
        //public void WriteVisionLog(Config.VisionItem v,Config.VisionLog log)
        //{

        //    while (this.Config.VisionLogs.Count > v.LogCount)
        //    {
        //        this.Config.VisionLogs.RemoveAt(0);
        //    }

        //    try
        //    {
        //            this.Config.VisionLogs.Add(log);
        //        //写日志，文本记录
        //        Log.LogWriter.WriteLog("{0}:{1};{2}", log.MachineID, log.Device, log.Notes);
        //    }
        //    catch { }
        //}



        public BindingList<Config.VisionLog> GetVisionLogs(string machine, string device)
        {
            IEnumerable ie = from lst in this.Config.VisionLogs
                             where lst.MachineID == machine && lst.ToString () == device
                             select lst;
                List<Config.VisionLog> ioLst = ie.Cast<Config.VisionLog>().ToList();

                return new BindingList < Config.VisionLog > ((IList<Config.VisionLog>)ioLst);
        }


        public BindingList<Log.Runlog> GetRunlogs(string machine, string device)
        {
            IEnumerable ie = from lst in this.Config.RunLogs
                             where lst.MachineID == machine && lst.Device == device
                             select lst;
            List<Log.Runlog> ioLst = ie.Cast<Log.Runlog>().ToList();
            return new BindingList<Log.Runlog>((IList<Log.Runlog>)ioLst);
        }



    }
}
