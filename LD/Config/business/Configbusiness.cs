using System;
using System.ComponentModel;
using System.Threading;
using System.Xml.Serialization;

namespace LD.Config
{
    /// <summary>
    /// 视觉基类
    /// </summary>
    [Serializable]
    public class Configbusiness : Configuration
    {
        public Configbusiness()
        {
            this.MachineID = "01";
            this.StationID = "";
            this.Caption = "机台";
            this.LogCount = 1000;
            this.WriteLog = false;
            this.Descrip = "功能描述";
            //工作日志
            this.LogList = new BindingList<Log.Runlog>();
        }


        /// <summary>
        /// 设备号
        /// </summary>
        public string MachineID
        {
            set;
            get;
        }

        /// <summary>
        /// 站号
        /// </summary>
        public string StationID
        {
            set;
            get;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption
        {
            set;
            get;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Descrip
        {
            set;
            get;
        }



        /// <summary>
        /// 写日志
        /// </summary>
        public bool WriteLog
        {
            set;
            get;
        }


        /// <summary>
        /// 记录数
        /// </summary>
        public int LogCount
        {
            set;
            get;
        }

        ///// <summary>
        ///// MES请求+过站信息(请求发基类，过站发过站信息）ScadaQueryMemoryAfter
        ///// </summary>
        //[XmlElement("ScadaQueryAutoScrewAft", Type = typeof(Bp.Mes.ScadaQueryAutoScrewAft))]
        //[XmlElement("ScadaQueryMemoryAfter", Type = typeof(Bp.Mes.ScadaQueryMemoryAfter))]
        //[XmlElement("ScadaQueryHardAft", Type = typeof(Bp.Mes.ScadaQueryHardAft))]
        //[XmlElement("ScadaQuery", Type = typeof(Bp.Mes.ScadaQuery))]


        /// <summary>
        /// 相机设备列表
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public BindingList<Log.Runlog> LogList = new BindingList<Log.Runlog>();

        [System.Xml.Serialization.XmlIgnore]
        public AutoResetEvent Reset = new AutoResetEvent(true);

    }
}
