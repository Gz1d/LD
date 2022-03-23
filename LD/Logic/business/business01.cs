using System;
using HalconDotNet;
namespace LD.Logic
{
    /// <summary>
    /// PCB上板工站
    /// </summary>
    public class business01 : business
    {

        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static business01 instance = new business01();

        /// <summary>
        /// 静态属性
        /// </summary>
        public static business01 Instance
        {
            get { return instance; }
        }
        #endregion

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private business01()
        {
           // this.FrmBase= new Ui.Kylin .frmKylin01(this);
        }

        /// <summary>
        /// 机台配置
        /// </summary>
        public override Config.Configbusiness CfgBase
        {
            get { return Config.ConfigManager.Instance.Configbusiness01; }
        }

        /// <summary>
        /// 机台配置
        /// </summary>
        public  Config.Configbusiness01 Cfg
        {
            get { return this.CfgBase  as Config.Configbusiness01; }
        }



        /// <summary>
        /// PLC值改变
        /// </summary>
        ///// <param name="item"></param>
        //public override void PlcValueChange(Config.PlcDataItem item)
        //{
        //    try
        //    {
        //        base.PlcValueChange(item);

        //        switch (item.PlcDevice)
        //        {
        //            case Common.PlcDevice.NULL:
        //                break;
        //            case Common.PlcDevice.ERROR:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogWriter.WriteException(ex);
        //    }
        //}


        /// <summary>
        /// 串口接收
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="data"></param>
        /// <param name="data_byte"></param>
        public override void SerialReceive(Common.SerialDevice serial, string data, byte[] data_byte)
        {
            base.SerialReceive(serial, data, data_byte);
        }


        /// <summary>
        /// socket客户端接收
        /// </summary>
        /// <param name="device"></param>
        /// <param name="socket"></param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public override void SocketCReceive(Config.SocketCItem device, System.Net.Sockets.Socket socket, byte[] dataB, string dataS)
        {
            base.SocketCReceive(device, socket, dataB, dataS);

        }

 
        /// <summary>
        /// 服务端接收
        /// </summary>
        /// <param name="device"></param>
        /// <param name="socket"></param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public override void SocketSReceive(Config.SocketSItem device, System.Net.Sockets.Socket socket, byte[] dataB, string dataS)
        {
            base.SocketSReceive(device, socket, dataB, dataS);
            try
            { 
 
            }
            catch (Exception ex)
            {
                Log.LogWriter.WriteException(ex);
            }
        }





    }
}





