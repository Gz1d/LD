using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Bp.Socket
{
    public class Client : INotifyPropertyChanged
    {
        public delegate void SocketReceiveDelegate(object devicetype, System.Net.Sockets.Socket socket, byte[] dataB, string dataS);
        public event SocketReceiveDelegate OnSocketReceive = null;
        private System.Net.Sockets.Socket client;
        private byte[] buf = new byte[1024 * 2];
        public static object SendLock = new object();

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        /// <summary>
        /// 服务器
        /// </summary>
        public string ServerIP
        {
            set;
            get;
        }

        /// <summary>
        /// 端口号
        /// </summary>
        public int ServerPort
        {
            set;
            get;
        }

        /// <summary>
        /// 毫秒
        /// </summary>
        public int TimeOut
        {
            set;
            get;
        }


        /// <summary>
        /// 心跳秒
        /// </summary>
        public int TimeHeart
        {
            set;
            get;
        }


        private bool mIsConnected=false;
        /// <summary>
        /// 已连接
        /// </summary>
        public bool IsConnected
        {
            set
            {
                if (this.mIsConnected != value)
                {
                    this.mIsConnected = value;
                    this.NotifyPropertyChanged("IsConnected");
                }

            }
            get
            {
                return this.mIsConnected;
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public object DeviceType
        {
            set;
            get;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.DeviceType.ToString();
        }


        /// <summary>
        /// 附属物
        /// </summary>
        public object Tag
        {
            set;
            get;
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        public Client(string ip, int port ,object  devicetype)
        {
            this.ServerIP = ip;
            this.ServerPort = port;
            this.DeviceType = devicetype;
        }



        /// <summary>
        /// 初始化
        /// </summary>
        public void DoInit()
        {
            try
            {
                this.InitTimer_Connect();
            }
            catch 
            {
            }
        }


        public object obj_start = new object();


        /// <summary>
        /// 启动
        /// </summary>
        public void DoStart()
        {
            lock (obj_start)
            {
                try
                {
                    if (this.client != null)
                    {
                        try
                        {
                            if (this.client.Connected)
                                this.client.Close();
                            this.client.Dispose();
                            this.client = null;
                        }
                        catch { }
                    }
                    try
                    {
                        this.client = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPAddress ipad = IPAddress.Parse(this.ServerIP);
                        IPEndPoint ipp = new IPEndPoint(ipad, this.ServerPort);
                        client.ReceiveTimeout = TimeOut;
                        client.SendTimeout = TimeOut;
                        this.client.Connect(ipp);
                        this.client.BeginReceive(buf, 0, buf.Length, SocketFlags.None, new AsyncCallback(Receive), this.client);
                        this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, this.TimeOut);
                    }
                    catch { }
                    this.Timer_Connect_Elapsed(null, null);
                }
                catch 
                {
                }
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void DoStop()
        {
            try
            {
                this.StopTimer_Connect();
               
                if (client != null && client.Connected)
                {
                    this.client.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public  void DoRelease()
        {
            try
            {
                if (this.client == null) return;
                this.client.Dispose();
                this.client = null;
            }
            catch
            {
            }
        }




        /// <summary>
        /// 接收数据回调
        /// </summary>
        /// <param name="ia"></param>
        private void Receive(IAsyncResult ia)
        {
            try
            {
                client = ia.AsyncState as System.Net.Sockets.Socket;
                int count = client.EndReceive(ia);
                client.BeginReceive(buf, 0, buf.Length, SocketFlags.None, new AsyncCallback(Receive), client);
                string context = Encoding.UTF8 .GetString(buf, 0, count);
                if (context.Length > 0)
                {
                    //抛出事件
                    if (this.OnSocketReceive != null)
                    {
                        byte[] buf_tmp = new byte[count];
                        Array.Copy(buf, buf_tmp, count);
                        this.OnSocketReceive.BeginInvoke(this.DeviceType, client, buf_tmp, context, null, this.OnSocketReceive);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendData(string data)
        {
            lock (SendLock)
            {
                bool r = true;
                try
                {
                    byte[] buff;
                    buff = Encoding.UTF8.GetBytes(data);
                    int intR = client.Send(buff, 0, buff.Length, SocketFlags.None);
                }
                catch
                {
                    r = false;
                }
                return r;
            }
        }

        /// <summary>
        ///  发送
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendData(byte[] data)
        {
            lock (SendLock)
            {//
                bool r = true;
                try
                {
                    int intR = client.Send(data, 0, data.Length, SocketFlags.None);
                }
                catch
                {
                    r = false;
                }
                return r;
            }
        }


        #region 重连定时器......
        /// <summary>
        /// 重连定时器
        /// </summary>
        private System.Timers.Timer Timer_Connect ;

        /// <summary>
        ///  初始化识别计时器
        /// </summary>

        /// <summary>
        /// 开启定时器
        /// </summary>
        public void InitTimer_Connect()
        {
            if (this.Timer_Connect == null) this.Timer_Connect = new System.Timers.Timer();

            this.Timer_Connect.Enabled = false;
            this.Timer_Connect.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Connect_Elapsed);
            this.Timer_Connect.Interval = 1000 * (this.TimeHeart == 0 ? 2 : this.TimeHeart);
        }



        /// <summary>
        /// 开启定时器
        /// </summary>
        public void StartTimer_Connect()
        {

            this.Timer_Connect.Enabled = false;
            this.Timer_Connect.Enabled = true;
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Connect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                this.StopTimer_Connect();

                try
                {
                    if (this.client != null)
                    {
                        if (((this.client.Poll(10, SelectMode.SelectRead)) && (this.client.Available == 0)) || !this.client.Connected)
                            this.IsConnected = false;
                        else
                            this.IsConnected = true;
                    }
                    else
                        this.IsConnected = false;
                }
                catch
                {
                    this.IsConnected = false;
                }
               
                this.StartTimer_Connect();
                
            }
            catch { }
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void StopTimer_Connect()
        {
            this.Timer_Connect.Enabled = false;
        }
        #endregion

   


    }
}
