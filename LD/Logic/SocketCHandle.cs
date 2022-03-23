using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using LD.Config;

namespace LD.Logic
{
    public class SocketCHandle
    {
        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static SocketCHandle instance = new SocketCHandle();
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SocketCHandle()
        {

        }

        /// <summary>
        /// 静态属性
        /// </summary>
        public static SocketCHandle Instance
        {
            get { return instance; }
        }
        #endregion

        public Config.ConfigSocketC Config
        {
            get
            {
                return ConfigManager.Instance.ConfigSocketC;
            }
        }


        public Config.SocketCItem GetConfig(Common.SocketDevice device)
        {
            IEnumerable ie = from lst in this.Config.SocketCItems
                             where lst.SocketDevice == device
                             select lst;
            List<Config.SocketCItem> ioLst = ie.Cast<Config.SocketCItem>().ToList();
            if (ioLst.Count > 0)
                return ioLst[0];
            else
                return null;
        }



        /// <summary>
        /// Socket接收
        /// </summary>
        /// <param name="device">设备配置</param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public void SocketReceive(Config.SocketCItem device, System.Net.Sockets.Socket socket, byte[] dataB, string dataS)
        {
            try
            {
                device.BuffReceive = dataS;
                switch (device.SocketDevice)
                {
                    case Common.SocketDevice.MES:

                        break;
                    default:
                     //business business = SystemHandle.Instance.GetMachineKylin(device.MachineID);
                     //   if (business != null)
                     //       business.SocketCReceive(device, socket, dataB, dataS);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.LogWriter.WriteException(ex);
            }

        }




        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="dataS">字串</param>
        public void SocketSend(Common.SocketDevice device, string data)
        {
            Device.DeviceManager.Instance.DeviceSocketC.SocketSend(device, data);
        }


        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="device">设备配置</param>
        /// <param name="dataS">byte[]</param>
        public void SocketSend(Common.SocketDevice device, byte[] data)
        {
            Device.DeviceManager.Instance.DeviceSocketC.SocketSend(device, data);
        }

    }
}
