using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LD.Config;

namespace LD.Logic
{
    public class SearialHandle
    {
        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static SearialHandle instance = new SearialHandle();
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SearialHandle()
        {

        }

        /// <summary>
        /// 静态属性
        /// </summary>
        public static SearialHandle Instance
        {
            get { return instance; }
        }
        #endregion

        public Config.ConfigSerial Config
        {
            get
            {
                return ConfigManager.Instance.ConfigSerial;
            }
        }


        public Config.SerialItem GetConfig(Common.SerialDevice device)
        {
            IEnumerable ie = from lst in this.Config.SerialItems
                             where lst.SerialDevice == device
                             select lst;
            List<Config.SerialItem> ioLst = ie.Cast<Config.SerialItem>().ToList();
            if (ioLst.Count > 0)
                return ioLst[0];
            else
                return null;
        }



        /// <summary>
        /// Serial数据接收
        /// </summary>
        /// <param name="device"></param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public void SerialReceive(Common.SerialDevice device, byte[] dataB, string dataS)
        {
            Config.SerialItem item = this.GetConfig(device);
            if (item.IsHex)
                item.BuffReceive = Common.ByteArrayToHexString(dataB);
            else
                item.BuffReceive = dataS;
            switch (item.SerialDevice)
            {
                default:
                    item.Value = item.BuffReceive;
                    item.Reset.Set();
                    business kylin = SystemHandle.Instance.GetMachineKylin(item.MachineID);
                    if (kylin != null)
                    {
                        // kylin.SerialReceive(device, dataS, dataB);
                        this.Async_SerialReceive(kylin, device, dataB, dataS);
                    }
                    break;
                case Common.SerialDevice.TEST:
                    break;
            }
        }

        private delegate void delegate_serial_receive(Common.SerialDevice device,  string dataS,byte[] dataB);

        public void Async_SerialReceive(business business, Common.SerialDevice device, byte[] dataB, string dataS)
        {
            delegate_serial_receive rece = new delegate_serial_receive(business.SerialReceive);
            rece.BeginInvoke(device, dataS, dataB, null, rece);
        }


        /// <summary>
        /// Serial数据接收
        /// </summary>
        /// <param name="device"></param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public void SerialReceive(Config.SerialItem device, byte[] dataB, string dataS)
        {
            if (device.IsHex)
                device.BuffReceive = Common.ByteArrayToHexString(dataB);
            else
                device.BuffReceive = dataS;
        }


        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public void SerialSend(Common.SerialDevice device, string data)
        {
            Device.DeviceManager.Instance.DeviceSerial.SerialSend(device, data);
        }


        /// <summary>
        /// 发送该数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public void SerialSend(Common.SerialDevice device, byte[] data)
        {
            Device.DeviceManager.Instance.DeviceSerial.SerialSend(device, data);
        }

        public string SerialReadLine(Common.SerialDevice device)
        {
            return Device.DeviceManager.Instance.DeviceSerial.SerialReadLine(device);
        }

        public byte[] SerialRead(Common.SerialDevice device)
        {
            return Device.DeviceManager.Instance.DeviceSerial.SerialRead(device);
        }

        public string SerialReadString(Common.SerialDevice device)
        {
            byte[] r = this.SerialRead(device);
            return Encoding.Default.GetString(r);
        }



        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="id">id 16进制 00</param>
        /// <returns></returns>
        public void UbxMoveStart(Common.SerialDevice com, int id = 0)
        {

            string tmp = string.Format("{0} 01 FE 00 01 F4", id.ToString("X2"));

            byte[] command = Common.HexStringToByteArray(tmp);
            this.SerialSend(com, command);
        }

       
    }
}
