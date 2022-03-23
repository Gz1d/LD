using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;


namespace LD.Config
{
    /// <summary>
    /// PLC配置类
    /// </summary>
    [Serializable]
    public class PlcTypeItem : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(info));
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PlcTypeItem()
        {
            this.MachineID = "01";
            this.StationID = "";
            this.DevName = "PLC01";
            this.DevType = Common.DeviceType.S1200;
            this.IP = "127.0.0.1";
            this.Port = 102;
            this.Rack = "0";
            this.Slot = "0";
            this.UpdatRate = 100;
            this.ConnectOutTime = 100;
            this.HeartSecond = 100;
            this.IsActive = true;
            this.IsConnected = false;

            this.PortName = "COM1";
            this.BaudRate = 115200;
            this.DataBits = 8;
            this.StopBits = 1;
            this.Parity = System.IO.Ports.Parity.None;

            this.DataFormat = HslCommunication.Core.DataFormat.ABCD;
            this.Station = "1";
            this.IsStringReverse = false;
            this.AddressStartWithZero = true;
        }

        /// <summary>
        /// 机台号
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
        /// PLC名称
        /// </summary>
        public string DevName
        {
            set;
            get;
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public Common.DeviceType DevType
        {
            set;
            get;
        }

        /// <summary>
        /// PLC类型
        /// </summary>
        //[System.Xml.Serialization.XmlIgnore]
        //public SiemensPLCS PlcType
        //{
        //    set;
        //    get;
        //}

        /// IP
        /// </summary>
        public string IP
        {
            set;
            get;
        }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            set;
            get;
        }

        /// <summary>
        /// 更新频率（ms)
        /// </summary>
        public int UpdatRate
        {
            set;
            get;
        }

        /// <summary>
        /// 死区(%)
        /// </summary>
        public string Rack
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Slot
        {
            get;
            set;
        }

        /// <summary>
        /// 超时毫秒
        /// </summary>
        public int ConnectOutTime
        {
            set;
            get;
        }

        /// <summary>
        /// 心跳
        /// </summary>
        public int HeartSecond
        {
            set;
            get;
        }

        //数据格式
        public HslCommunication.Core.DataFormat DataFormat
        {
            set;
            get;
        }

        //字符串颠倒
        public bool IsStringReverse
        {
            set;
            get;
        }

        //站号
        public string Station
        {
            set;
            get;
        }

        //是否从0开始
        public bool AddressStartWithZero
        {
            set;
            get;
        }

        /// <summary>
        /// 串口
        /// </summary>
        public string PortName
        {
            set;
            get;
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            set;
            get;
        }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits
        {
            set;
            get;
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public int StopBits
        {
            set;
            get;
        }


        /// <summary>
        /// 校验奇偶
        /// </summary>
        public System.IO.Ports.Parity Parity
        {
            set;
            get;
        }


        /// <summary>
        /// 设备是否连接正常
        /// </summary>       
        private bool mIsConnected;
        [System.Xml.Serialization.XmlIgnore]
        public bool IsConnected
        {
            get
            {
                return this.mIsConnected;
            }
            set
            {
                if (this.mIsConnected != value)
                {
                    this.mIsConnected = value;
                    this.NotifyPropertyChanged("IsConnected");
                }
            }
        }

        /// <summary>
        /// 激活
        /// </summary>
        public bool IsActive
        {
            set;
            get;
        }

        [System.Xml.Serialization.XmlIgnore]
        public System.Timers.Timer Timer;

        /// <summary>
        /// PLC-TAG
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public object Tag
        {
            set;
            get;
        }

    }
}
