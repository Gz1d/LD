using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Bp.Socket
{
    public delegate void RecievedDataDelegate(IAsyncResult ar);

    /// <summary>
    /// 通讯服务端
    /// </summary>
    public class Server : INotifyPropertyChanged
    {
        //private byte[] buff = new byte[1024 * 2];
        private static object SendLock = new object();
        public delegate void SocketReceiveDelegate(object device, System.Net.Sockets.Socket socket, byte[] dataB, string dataS);

        public delegate void SocketItemChange(object devicetype, BindingList<SocketItem> Sockets);

        public event SocketReceiveDelegate OnSocketReceive = null;
        public event SocketItemChange OnSocketItemChange = null;

        private System.Net.Sockets.Socket server;

        public virtual event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String name)
        {
            if (PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        /// <summary> Socket集</summary>
        public BindingList<SocketItem> Sockets= new BindingList<SocketItem>();
        /// <summary> 服务器 </summary>
        public string ServerIP {set; get;}
        /// <summary> 端口号</summary>
        public int ServerPort {set; get; }
        /// <summary> 超时(毫秒）</summary>
        public int TimeOut {set;get; }
        /// <summary>心跳秒</summary>
        public int TimeHeart {set;get; }
        /// <summary> 客户端数量  </summary>
        public int ClientMax { set; get; }
        /// <summary> 设备类型 </summary>
        public object DeviceType { set; get; }
        /// <summary> 附属物 </summary>
        public object Tag { set; get; }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.DeviceType.ToString();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void DoInit( )
        {
            try{
                this.InitTimer_Connect();
            }
            catch {}
        }


        public object obj_start = new object();


        /// <summary>
        /// 私有构造函数
        /// </summary>
        public Server(string ip, int port, object devicetype = null) {
            this.ServerIP = ip;
            this.ServerPort = port;
            this.DeviceType = devicetype;
            this.Sockets = new BindingList<SocketItem>();
            this.Sockets.ListChanged += new ListChangedEventHandler(Sockets_ListChanged);
        }

        void Sockets_ListChanged(object sender, ListChangedEventArgs e)
        {
            try{
                if (this.OnSocketItemChange != null)
                    this.OnSocketItemChange(this.DeviceType ,this.Sockets);
            }
            catch { }
        }


        /// <summary>
        /// 启动
        /// </summary>
        public void DoStart()
        {
            lock (obj_start){            
                // IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
                // IPAddress[] aryLocalAddr = ipEntry.AddressList;
                // // Verify we got an IP address. Tell the user if we did
                // if (aryLocalAddr == null || aryLocalAddr.Length < 1)
                // {
                //     Console.WriteLine("Unable to get local address");
                //     return;
                //  }
                // Create the listener socket in this machines IP address
                this.server = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(this.ServerIP);
                server.SendTimeout = TimeOut;
                server.ReceiveTimeout = TimeOut;
                this.server.Bind(new IPEndPoint(ip, this.ServerPort));
                //listener.Bind( new IPEndPoint( IPAddress.Loopback, 399 ) );	// For use with localhost 127.0.0.1
                this.server.Listen(this.ClientMax);
                // Setup a callback to be notified of connection requests
                this.server.BeginAccept(new AsyncCallback(this.OnConnectRequest), this.server);
                this.StartTimer_Connect();
            }
        }

        private void OnConnectRequest(IAsyncResult ar)
        {
            try
            {
                System.Net.Sockets.Socket listener = (System.Net.Sockets.Socket)ar.AsyncState;
                NewConnection(listener.EndAccept(ar));
                listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);
            }
            catch { }
        }

        private void NewConnection(System.Net.Sockets.Socket socket)
        {
            try
            {
                SocketItem item = new SocketItem(socket);
                //Sokcets.Add(socket.RemoteEndPoint.ToString(), item);
                this.AddSocket(item);
                item.SetupRecieveCallback(this.OnRecievedData);
              
            }
            catch { }
        }

        public void OnRecievedData(IAsyncResult ar) {
            SocketItem item = (SocketItem)ar.AsyncState;
            byte[] buff = item.GetRecievedData(ar);
            // If no data was recieved then the connection is probably dead
            if (buff.Length < 1)  {
                Console.WriteLine("Client {0}, disconnected", item.Socket.RemoteEndPoint);
                item.Socket.Close();
                this.RemoveSocket(item);
                return;
            }
            //抛出事件
            string context = Encoding.UTF8 .GetString(buff, 0, buff.Length );
            item.BuffReceive = context;
            if (context.Length > 0) {
                //抛出事件
                if (this.OnSocketReceive != null){
                    this.OnSocketReceive.BeginInvoke(this.DeviceType, item.Socket, buff, context, null, this.OnSocketReceive);
                    //this.OnSocketReceive.BeginInvoke(this.DeviceType, item.Socket, buff, context, InvokeCallBack, this.OnSocketReceive);
                }
            }
            item.SetupRecieveCallback(this.OnRecievedData);
        }
        void InvokeCallBack(IAsyncResult pResult)
        {
            if (pResult.IsCompleted) {
                SocketReceiveDelegate dlg = (SocketReceiveDelegate)(((AsyncResult)pResult).AsyncDelegate);
                try{
                    dlg.EndInvoke(pResult);
                }
                catch (Exception ex){
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void DoStop()
        {
            try{
                // Clean up before we go home
                if (this.server == null) return;
                this.server.Close();            
            }
            catch (Exception ex){
                throw ex;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void DoRelease()
        {
            try{
                this.server = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch{
            }
        }

        public void  AddSocket( SocketItem socket){
            this.RemoveSocket(socket);
            this.Sockets.Add(socket);
        }

        private void RemoveSocket(SocketItem socket)
        {
            foreach (SocketItem si in this.Sockets) {
                if (socket.RemoteEndPoint == si.RemoteEndPoint){
                    this.Sockets.Remove(si);
                    break;
                }
            }
        }


        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendData(string data)
        {
            lock (SendLock){
                bool r = true;
                try{
                    foreach (SocketItem si in this.Sockets){                       
                        if (si.Socket.Connected) {
                            byte[] buff;
                            buff = Encoding.UTF8 .GetBytes(data);
                            si.Socket.Send(buff, 0, buff.Length, SocketFlags.None);
                        }
                    }
                }
                catch{
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
            lock (SendLock){
                bool r = true;
                try{
                    foreach (SocketItem si in this.Sockets ){
                        if (si.Socket.Connected)
                            si.Socket.Send(data, 0, data.Length, SocketFlags.None);
                    }
                }
                catch{
                    r = false;
                }
                return r;
            }
        }

       

 
        #region 重连定时器......
        /// <summary>
        /// 重连定时器
        /// </summary>
        private System.Timers.Timer Timer_Connect;

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
            this.Timer_Connect.Interval = 1000 * this.TimeHeart ;
        }



        /// <summary>
        /// 开启定时器
        /// </summary>
        public void StartTimer_Connect()
        {
            this.Timer_Connect.Enabled = false;
            this.Timer_Connect.Interval = 1000 * this.TimeHeart;
            this.Timer_Connect.Enabled = true;
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Connect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {            
            this.StopTimer_Connect();
            try{
                foreach (SocketItem si in this.Sockets) {
                    if (si.Socket != null){
                        try {
                            if (((si.Socket.Poll(20, SelectMode.SelectRead)) && (si.Socket.Available == 0)) || !si.Socket.Connected){
                                this.RemoveSocket(si);
                            }
                        }
                        catch {
                            this.RemoveSocket(si);
                        }
                    }
                    else {
                        this.RemoveSocket(si);
                    }
                }
            }
            catch { }
            this.StartTimer_Connect();
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void StopTimer_Connect()
        {
            this.Timer_Connect.Enabled = false;
        }
        #endregion

         [Serializable]
        public class SocketItem : INotifyPropertyChanged
        {
            public virtual event PropertyChangedEventHandler PropertyChanged;
            protected void NotifyPropertyChanged(String name)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }

            private System.Net.Sockets.Socket m_sock;
            private byte[] m_byBuff = new byte[1024 * 2];
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="sock">client socket conneciton this object represents</param>
            public SocketItem(System.Net.Sockets.Socket sock)
            {
                m_sock = sock;
            }

            // Readonly access
            public System.Net.Sockets.Socket Socket
            {
                get { return m_sock; }
            }



            public string RemoteEndPoint
            {
                get{
                    try {
                        return this.Socket.RemoteEndPoint.ToString();
                    }
                    catch{
                        return null;
                    }
                }
            }

            public string LocalEndPoint
            {
                get{
                    return this.Socket.LocalEndPoint.ToString();
                }
            }


            private bool mIsConnected = true;
            /// <summary>
            /// 已连接
            /// </summary>
            public bool IsConnected
            {
                set{
                    if (this.mIsConnected != value){
                        this.mIsConnected = value;
                        this.NotifyPropertyChanged("IsConnected");
                    }
                }
                get{
                    return this.mIsConnected;
                }
            }

            private string mBuffSend;
            [System.Xml.Serialization.XmlIgnore]
            public string BuffSend
            {
                set{
                    if (this.mBuffSend != value){
                        this.mBuffSend = value;
                        this.NotifyPropertyChanged("BuffSend");
                    }
                }
                get{
                    return this.mBuffSend;
                }
            }

            private string mBuffReceive;
            [System.Xml.Serialization.XmlIgnore]
            public string BuffReceive
            {
                set{
                    if (this.mBuffReceive != value){
                        this.mBuffReceive = value;
                        this.NotifyPropertyChanged("BuffReceive");
                    }
                }
                get {
                    return this.mBuffReceive;
                }
            }

            public void SetupRecieveCallback(RecievedDataDelegate OnReceive)
            {
                try{
                    AsyncCallback recieveData = new AsyncCallback(OnReceive);
                    m_sock.BeginReceive(m_byBuff, 0, m_byBuff.Length, SocketFlags.None, recieveData, this);
                }
                catch (Exception ex){
                    Console.WriteLine("Recieve callback setup failed! {0}", ex.Message);
                }
            }

            public byte[] GetRecievedData(IAsyncResult ar)
            {
                int nBytesRec = 0;
                try{
                    nBytesRec = m_sock.EndReceive(ar);
                }
                catch { }
                byte[] byReturn = new byte[nBytesRec];
                Array.Copy(m_byBuff, byReturn, nBytesRec);
                return byReturn;
            }


        }

    }

 
}
