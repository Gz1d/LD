using System;
using System.ComponentModel;
using System.Threading;



namespace LD.Config
{

    /// <summary>
    /// 串口设备
    /// </summary>
    [Serializable]
    public class SerialItem: INotifyPropertyChanged
    {

        public SerialItem()
        {
            this.MachineID = "01";
            this.StationID = "";
            this.PortName = "COM1";
            this.BaudRate = 115200;
            this.StopBits = System.IO.Ports.StopBits.One;
            this.DataBits = 8;
            this.Parity = System.IO.Ports.Parity.None;
            this.ReceivedBytesThreshold = 1;
            this.WriteTimeout = 100;
            this.ReadTimeout = 100;
            this.SleepMs = 100;
            this.IsActive = true;
            this.CallReceive = true;
            this.NewLine = "";
            this.Descrip = "串口";
            this.ResetMs = 500;
           
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(info));
            }
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
        /// 停止位
        /// </summary>
        public System.IO.Ports.StopBits StopBits
        {
            set;
            get;
        }


        /// <summary>
        /// 校验
        /// </summary>
        public System.IO.Ports.Parity Parity
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
        /// 新行标志
        /// </summary>
        public string NewLine
        {
            set;
            get;
        }


        /// <summary>
        /// 回调触发字符数
        /// </summary>
        public int ReceivedBytesThreshold
        {
            set;
            get;
        }

        /// <summary>
        /// 写超时
        /// </summary>
        public int WriteTimeout
        {
            set;
            get;
        }

        /// <summary>
        /// 读超时
        /// </summary>
        public int ReadTimeout
        {
            set;
            get;
        }

        public int SleepMs
        {
            set;get;
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


        /// <summary>
        /// 设备类型
        /// </summary>
        public Common.SerialDevice SerialDevice
        {
            set;
            get;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Descrip
        {
            set;
            get;
        }

        private string mBuffSend;
        
        //[System.Xml.Serialization.XmlIgnore]
        /// <summary>
        /// 串口发送
        /// </summary>
        public string BuffSend
        {
            set
            {
                if (this.mBuffSend != value)
                {
                    this.mBuffSend = value;
                    this.NotifyPropertyChanged("BuffSend");
                }
            }
            get
            {
                return this.mBuffSend;
            }
        }

        private string mBuffReceive;

        /// <summary>
        /// 串口接收
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string BuffReceive
        {
            set
            {
                if (this.mBuffReceive != value)
                {
                    this.mBuffReceive = value;
                    this.NotifyPropertyChanged("BuffReceive");
                }
            }
            get
            {
                return this.mBuffReceive;
            }
        }

        /// <summary>
        /// 是否十六进制
        /// </summary>
        public bool IsHex
        {
            set;
            get;
        }


        /// <summary>
        /// 是否回调
        /// </summary>
        public bool CallReceive
        {
            set;
            get;
        }

        /// <summary>
        /// 串口TAG
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public object Tag
        {
            set;
            get;
        }

         /// <summary>
         /// 存放串口读取 
         /// </summary>
         [System.Xml.Serialization.XmlIgnore]
         public string Value;

         [System.Xml.Serialization.XmlIgnore]
         public AutoResetEvent Reset = new AutoResetEvent(false);

         /// <summary>
         /// 信号灯等待时间毫秒
         /// </summary>
         public int ResetMs
         {
             set;
             get;
         }

    }



}
