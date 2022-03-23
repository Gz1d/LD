using System;
using System.ComponentModel;
using System.Threading;

namespace LD.Config
{
     /// <summary>
     /// 客户端
     /// </summary>
    [Serializable]
    public class SocketCItem : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(info));

            }
        }

        public SocketCItem()
        {
            this.MachineID = "01";
            this.StationID = "";
            this.IP = "127.0.0.1";
            this.Port = 6000;
            this.OutTime = 100;
            this.HeartSecond = 2;
            this.ConnectSecond = 3;
            this.IsActive = true;
            this.IsConnected = false;
            this.Descrip = "客户端";
            this.ResetMs = 500;
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
        /// 服务器
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
        /// 超时毫秒
        /// </summary>
        public int OutTime
        {
            set;
            get;
        }

        /// <summary>
        /// 心跳秒
        /// </summary>
        public int HeartSecond
        {
            set;
            get;
        }

        /// <summary>
        /// 重连秒
        /// </summary>
        public int ConnectSecond
        {
            set;
            get;
        }

        /// <summary>
        /// 设备是否连接正常
        /// </summary>       
        private bool mIsConnected=true;
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
        public Common.SocketDevice SocketDevice
        {
            set;
            get;
        }

        public string mDescrip;

        /// <summary>
        /// 备注
        /// </summary>
        public string Descrip
        {
            set
            {
                if (this.mDescrip != value)
                {
                    this.mDescrip = value;
                    this.NotifyPropertyChanged("Descrip");
                }
            }
            get
            {
                return this.mDescrip;
            }
        }

        private string mBuffSend;
        [System.Xml.Serialization.XmlIgnore]
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

        [System.Xml.Serialization.XmlIgnore]
        public System.Timers.Timer Timer;


        [System.Xml.Serialization.XmlIgnore]
        public string Value="";

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

        public override string ToString()
        {
            return this.SocketDevice.ToString();
        }
    }

    /// <summary>
    /// 服务端
    /// </summary>
    [Serializable]
    public class SocketSItem : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(info));

            }
        }

        public SocketSItem()
        {
            this.MachineID = "01";
            this.StationID = "";
            this.IP = "127.0.0.1";
            this.Port = 6000;
            this.OutTime = 100;
            this.HeartSecond = 2;
            this.IsActive = true;
            this.Descrip = "服务端";
            this.ClientCount = 0;
            this.ClientMax = 10;

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
        /// 服务器
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
        /// 超时毫秒
        /// </summary>
        public int OutTime
        {
            set;
            get;
        }

        /// <summary>
        /// 获取数据间隔毫秒
        /// </summary>
        public int HeartSecond
        {
            set;
            get;
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
        public Common.SocketDevice SocketDevice
        {
            set;
            get;
        }

        /// 链接数
        /// </summary>
        public int ClientCount
        {
            set;
            get;
        }

        /// <summary>
        /// 链接数
        /// </summary>
        public int ClientMax
        {
            set;
            get;
        }

        /// <summary>
        /// 设备是否连接正常
        /// </summary>       
        private bool mIsConnected = false;
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


        private string mBuffSend;
        [System.Xml.Serialization.XmlIgnore]
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
        /// 备注
        /// </summary>
        public string Descrip
        {
            set;
            get;
        }

        [System.Xml.Serialization.XmlIgnore]
        public System.Timers.Timer Timer;


        [System.Xml.Serialization.XmlIgnore]
        public string Value = "";

        [System.Xml.Serialization.XmlIgnore]
        public AutoResetEvent Reset = new AutoResetEvent(false);

        [System.Xml.Serialization.XmlIgnore]
        public BindingList<Bp.Socket.Server.SocketItem> SocketClients
        {
            set;
            get;
        }
       


       

    }
}
