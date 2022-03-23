using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace LD.Logic
{
    public  class ReportHandle
    {
        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static ReportHandle instance = new ReportHandle();


        private ReportHandle()
        {
            TimerE = new System.Timers.Timer();
            //TimerE.Elapsed += new System.Timers.ElapsedEventHandler(TimerEvent);//到达时间的时候执行事件；
            OldClass = Config.ConfigManager.Instance.ConfigReport.Class;
        }


        /// <summary>
        /// 静态属性
        /// </summary>
        public static ReportHandle Instance
        {
            get { return instance; }
        }
        #endregion
        /// <summary>
        /// 报表单元
        /// </summary>
        public Config.report Report
        {
            set;
            get;
        }

        public Config.ConfigReport config
        {
            get
            {
                return Config.ConfigManager.Instance.ConfigReport;
            }
        }


        /// <summary>
        /// 报表定时器
        /// </summary>
        private System.Timers.Timer TimerE  ;
        /// <summary>
        /// 信号灯 防止线程混乱
        /// </summary>
        public   AutoResetEvent Reset = new AutoResetEvent(true);
        /// <summary>
        /// 启动报表
        /// </summary>
        /// <param name="HeartHour">报表行产生时间</param>
        /// <param name="Class">设置班次</param>
        /// 
        public void ReportStart( )
        {

            //foreach (var v in Config.ConfigManager.Instance.ConfigVision.VisionItems)
            //{
            //    if (v.Report == null)
            //    {
            //        v.Report = new Config.report();
            //    }
            //}
            if (config.AcquTime<=0)
            {
                config.AcquTime = 1;
            }
            TimerE.Interval = config.AcquTime * 1000;
            TimerE.Enabled = false;//是否执行System.Timers.Timer.Elapsed事件；
            TimerE.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }


        private string OldClass;
        public  void ChangeeClass()
        {
            if (config.Class != OldClass && Report!= null)
            {
                //foreach (var v in Config.ConfigManager.Instance.ConfigVision.VisionItems)
                //{
                //    try
                //    {
                //        if (v.Report.VisionID != null)
                //        {
                //            config.ReportItems.Add(v.Report);
                //            v.Report = new Config.report();
                //            ClearReport();
                //        }
                //    }
                //    catch (Exception)
                //    { }
                //}
                if (TimerE.Enabled)
                {
                    TimerE.Enabled = false;
                    TimerE.Enabled = true;
                }
                
            }
            OldClass = config.Class;
        }

        /// <summary>
        /// 停止报表
        /// </summary>
        public void ReportStop()
        {
            TimerE.Enabled = false;//是否执行System.Timers.Timer.Elapsed事件；
        }


        /// <summary>
        /// 时间触发产生报表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void TimerEvent(object sender, ElapsedEventArgs e)
        //{
        //    TimerE.Enabled = false;
        //    Reset.WaitOne();

        //    Ui.frmMdi.frm.Invoke(new EventHandler(delegate
        //    {
        //        foreach (var v in Config.ConfigManager.Instance.ConfigVision.VisionItems)
        //        {
        //            try
        //            {
        //                if (v.Report.VisionID != null)
        //                {
        //                    v.Report.Class = OldClass;
        //                    config.ReportItems.Add(v.Report);
        //                    v.Report = new Config.report();
        //                    ClearReport();
        //                }
        //            }
        //            catch (Exception)
        //            { }
        //        }
        //    }));

        //    TimerE.Enabled = true;
        //    Reset.Set();
        //}



    //    public void ReportAdd(Config.report v)
    //    {
    //        v.PosNumObj=

    //            Reset.Reset();//关闭信号灯 使定时器处于等待状态
    //                          //////////////////////报表行数据////////////////////////////////////
    //            v.Report.NumberOK = v.Result == "OK" ? v.Report.NumberOK + 1 : v.Report.NumberOK;
    //            v.Report.NumberNG = v.Result == "NG" ? v.Report.NumberNG + 1 : v.Report.NumberNG;
    //            v.Report.Total = v.Report.NumberOK + v.Report.NumberNG;
    //            v.Report.PerOK = Convert.ToDouble(v.Report.NumberOK) / Convert.ToDouble(v.Report.Total);
    //            v.Report.VisionID = v.VisionID;
    //            v.Report.Time = DateTime.Now.ToString("yy-MM-dd-HH-mm");
    //            Reset.Set();


    //}


    /// <summary>
    /// 当报表库超出容量上限清除
    /// </summary>
    private void ClearReport()
        {
           while( config.ReportItems.Count> config.ClassNumberMax)
            {
                config.ReportItems.RemoveAt(0);
            }
        }

/// <summary>
/// 通过指定时间段来生成报表数据
/// </summary>
/// <param name="Min"></param>
/// <param name="Max"></param>
/// <returns></returns>
public  List<Config.report>  GetReportItemsByDateTime(string Min,string Max,string VisionID)
        {
            //  ("yy-MM-dd-HH-mm")
            try
            {
                int min = int.Parse(Min.Replace("-", ""));
                int max = int.Parse(Max.Replace("-", ""));

                IEnumerable ie = from lst in config.ReportItems
                                 where int.Parse(lst.Time.Replace("-", "")) >= min
                                 && int.Parse(lst.Time.Replace("-", "")) <= max
                                 && lst.VisionID == VisionID
                                 select lst;
                List<Config.report> ioLst = ie.Cast<Config.report>().ToList();
                return ioLst;
            }
            catch (Exception)
            {
                return null;
            }
           
        } 



/// <summary>
/// 删除列表项
/// </summary>
/// <param name="Report"></param>
        public void ReportSub(Config.report Report)
        {
            config.ReportItems.Remove(Report);
        }
        /// <summary>
        /// 删除列表项
        /// </summary>
        /// <param name="index"></param>
        public void ReportSub(int index)
        {
            config.ReportItems.RemoveAt(index);
        }


    }
}
