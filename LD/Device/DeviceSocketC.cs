using System;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;


namespace LD.Device
{

    /// <summary>
    /// Plc设备
    /// </summary>
    public class DeviceSocketC : Device
    {

        /// <summary>
        /// Socket集
        /// </summary>
        public SortedList Sokcets = new SortedList();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        public DeviceSocketC(Config.ConfigSocketC config)
        {
            this.config = config;
        }


        /// <summary>
        /// plc配置表
        /// </summary>
        private Config.ConfigSocketC config
        {
            set;
            get;
        }


        /// <summary>
        /// 初始化
        /// </summary>
        public override void DoInit()
        {
            try
            {
                foreach (Config.SocketCItem item in this.config.SocketCItems)
                {
                    if (!item.IsActive) continue;
                    if (item.Timer == null) item.Timer = new System.Timers.Timer();
                    item.Timer.Enabled = false;
                    item.Timer.Interval = 1000 * item.ConnectSecond ;
                    item.Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);

                    Bp.Socket.Client client = new Bp.Socket.Client(item.IP, item.Port, item);
                    client.TimeOut = item.OutTime;
                    client.TimeHeart = item.HeartSecond;
                    
                    Sokcets.Add(item.SocketDevice, client);
                    item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }

                IDictionaryEnumerator enumerator = this.Sokcets.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Bp.Socket.Client socket = enumerator.Value as Bp.Socket.Client;
                    socket.OnSocketReceive += new Bp.Socket.Client.SocketReceiveDelegate(this.Socket_OnSocketReceive);
                    socket.PropertyChanged += new PropertyChangedEventHandler(socket_PropertyChanged);
                    socket.DoInit();
                }

            }
            catch (Exception ex)
            {
                throw new InitException(this.ToString(), ex.ToString());
            }
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //重连
            if (e.PropertyName == "IsConnected")
            {
                Config.SocketCItem item = (Config.SocketCItem)sender;

                if (!item.IsConnected)
                {
                    item.Timer.Enabled = false;
                    item.Timer.Enabled = true;

                }
                else
                {
                    item.Timer.Enabled = false;

                    if (item.SocketDevice == Common.SocketDevice.MES)
                    {
                        
                    }
                }
            }
            else
            {

            }
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer t = (System.Timers.Timer)sender;
            t.Enabled = false;
            IDictionaryEnumerator enumerator = this.Sokcets.GetEnumerator();

            Config.SocketCItem item = null;
            while (enumerator.MoveNext())
            {
                Bp.Socket.Client socket = enumerator.Value as Bp.Socket.Client;
                item = (Config.SocketCItem)socket.DeviceType;
                if (item.Timer == t)
                {
                    if (!item.IsConnected)
                        socket.DoStart();
                    break;
                }
            }
            if (!item.IsConnected)
                t.Enabled = true;
        }

        void socket_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                Bp.Socket.Client client = (Bp.Socket.Client)sender;

                Config.SocketCItem item = (Config.SocketCItem)client.DeviceType;
                item.IsConnected = client.IsConnected;
                if (!item.IsConnected)
                    item.Timer.Enabled = true;              
            }
        }



        //private delegate void delegate_Socket_Receive(object device, byte[] dataB, string dataS);

        ///// <summary>
        ///// 接收
        ///// </summary>
        ///// <param name="device"></param>
        ///// <param name="dataB"></param>
        ///// <param name="dataS"></param>
        //private void Async_Socket_OnSocketReceive(object device, byte[] dataB, string dataS)
        //{
        //    delegate_Socket_Receive rece = new delegate_Socket_Receive(this.Socket_OnSocketReceive);
        //    rece.BeginInvoke(device, dataB, dataS, null, rece);
        //}

        private void Socket_OnSocketReceive(object device,   System.Net.Sockets.Socket socket, byte[] dataB, string dataS)
        {
            Logic.SocketCHandle.Instance.SocketReceive((Config .SocketCItem )device, socket, dataB, dataS);

        }

        /// <summary>
        /// 启动
        /// </summary>
        public override void DoStart()
        {
            try
            {
                IDictionaryEnumerator enumerator = this.Sokcets.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Bp.Socket.Client socket = enumerator.Value as Bp.Socket.Client;
                    socket.DoStart();

                    if (!socket.IsConnected)
                    {
                        Config.SocketCItem item = (Config.SocketCItem)socket.DeviceType;
                        item.Timer.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new StartException(this.ToString(), ex.ToString());
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void DoStop()
        {
            try
            {
                IDictionaryEnumerator enumerator = this.Sokcets.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Bp.Socket.Client socket = enumerator.Value as Bp.Socket.Client;

                    Config.SocketCItem item = (Config.SocketCItem)socket.DeviceType;
                    item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);

                    socket.DoStop();
                }
            }
            catch (Exception ex)
            {
                throw new StopException(this.ToString(), ex.ToString());
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void DoRelease()
        {
            try
            {
                IDictionaryEnumerator enumerator = this.Sokcets.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Bp.Socket.Client socket = enumerator.Value as Bp.Socket.Client;
                    socket.DoRelease();
                }
            }
            catch (Exception ex)
            {
                throw new ReleaseException(this.ToString(), ex.ToString());
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public override string DeviceType
        {
            get
            {
                return "DeviceSocketC";
            }
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.DeviceType;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public void SocketSend(Common.SocketDevice device, string data)
        {
            Bp.Socket.Client socket = (Bp.Socket.Client)Sokcets[device];
            socket.SendData(data);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public void SocketSend(Common.SocketDevice device, byte[] data)
        {
            Bp.Socket.Client socket = (Bp.Socket.Client)Sokcets[device];
            socket.SendData(data);
        }


    }
}
