using LD;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD.Config;

namespace LD.Logic
{
    public class SocketSHandle
    {
        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static SocketSHandle instance = new SocketSHandle();
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SocketSHandle()
        {

        }

        /// <summary>
        /// 静态属性
        /// </summary>
        public static SocketSHandle Instance
        {
            get { return instance; }
        }
        #endregion

        public Config.ConfigSocketS Config
        {
            get
            {
                return ConfigManager.Instance.ConfigSocketS;
            }
        }


        public Config.SocketSItem GetConfig(Common.SocketDevice device)
        {
            IEnumerable ie = from lst in this.Config.SocketSItems
                             where lst.SocketDevice == device
                             select lst;
            List<Config.SocketSItem> ioLst = ie.Cast<Config.SocketSItem>().ToList();
            if (ioLst.Count > 0)
                return ioLst[0];
            else
                return null;
        }



        /// <summary>
        /// 数据接收
        /// </summary>
        /// <param name="device">服务端</param>
        /// <param name="socket">Socket</param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public void socket_OnSocketReceive(Config.SocketSItem device, System.Net.Sockets.Socket socket, byte[] dataB, string dataS)
        {
            Log.LogWriter.WriteSocketLog(string.Format("{0},{1},{2}", device.ToString(), socket.RemoteEndPoint.ToString(), dataS));
            device.BuffReceive = dataS;
            device.Value = dataS;
            device.Reset.Set();
            switch (device.SocketDevice)
            {
                default :
                    //business business = SystemHandle.Instance.GetMachineKylin(device.MachineID);
                    //if (business != null)
                    //{
                    //    business.SocketSReceive(device, socket, dataB, dataS);
                    //}
                    break;
            }
        }


        /// <summary>
        /// 发送,轮询所有连接socket
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="dataS">字串</param>
        public void SocketSend(Common.SocketDevice device, string data)
        {
            Device.DeviceManager.Instance.DeviceSocketS.SocketSend(device, data);
            Log.LogWriter.WriteSocketLog(string.Format("{0}, {1}", device.ToString(), data));

        }


        /// <summary>
        /// 发送,轮询所有连接socket
        /// </summary>
        /// <param name="device">设备配置</param>
        /// <param name="dataS">byte[]</param>
        public void SocketSend(Common.SocketDevice device, byte[] data)
        {
            Device.DeviceManager.Instance.DeviceSocketS.SocketSend(device, data);
        }

        private object lock_send = new object();


        /// <summary>
        /// 发送，指定socket
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        public void SendData( System.Net.Sockets.Socket socket,byte[] data)
        {
            lock (lock_send)
            {
                if (socket.Connected)
                    socket.Send(data, 0, data.Length, System.Net.Sockets.SocketFlags.None );
              
            }
        }


        /// <summary>
        /// 发送，指定socket
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        public void SendData(System.Net.Sockets.Socket socket,string data)
        {
            lock (lock_send)
            {
                if (socket.Connected)
                {
                    byte[] buff;
                    buff = Encoding.UTF8.GetBytes(data);
                    socket.Send(buff, 0, buff.Length, System.Net.Sockets.SocketFlags.None);
                }
            }
        }


    }
}
