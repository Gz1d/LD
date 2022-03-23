using System;
using System.Text;
using HalconDotNet;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LD.Logic
{
    /// <summary>
    /// 机台业务基类
    /// </summary>
    public class business
    {
        public business()
        {
            //初始化
            this.CfgBase = new Config.Configbusiness();
            //this.FrmBase = new Ui.Kylin.frmKylin();
        }

        /// <summary>
        /// 机台配置
        /// </summary>
        public virtual Config.Configbusiness CfgBase
        {
            set;
            get;
        }




        #region 自定义变量


        /// <summary>
        /// 获取自定义变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetDefinInt(string name)
        {
            return 0;
        }

        #endregion

        #region 视觉业务


        /// <summary>
        /// 
        /// </summary>
        private object lockVision = new object();


        /// <summary>
        /// 处理图像
        /// </summary>
        //public virtual bool HalconMachineImage(Config.VisionItem vi)
        //{
        //    return true;
        //}

   


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vi"></param>
        //public virtual void GrabImage(Config.VisionItem vi)
        //{
        //    //抓图
        //    VisionHandle.Instance.GrabImage(vi);
        //}


        #region PLC&Mudbus操作

        /// <summary>
        /// 读PLC，Modbus
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public object PlcRead(Common.PlcDevice device)
        {
            try
            {
                return Logic.PlcHandle.Instance.ReadValue(device);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 视觉处理类
        /// </summary>
        //private Logic.VisionHandle VP
        //{
        //    get
        //    {
        //        return Logic.VisionHandle.Instance;
        //    }

        //}


        /// <summary>
        /// 写值PLC，Modbus
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="value"></param>
        public void PlcWrite(Common.PlcDevice device, object value)
        {
            try
            {
                Logic.PlcHandle.Instance.WriteValue(device, value);
            }
            catch { }
        }


        /// <summary>
        /// 写PLC
        /// </summary>
        /// <param name="device"></param>
        /// <param name="value"></param>
        public void PlcWrite(string device, object value)
        {
            try
            {
                Logic.PlcHandle.Instance.WriteValue(device, value);
            }
            catch
            {
            }
        }


        ///// <summary>
        ///// 值改变PLC，Modbus，机台重写业务
        ///// </summary>
        ///// <param name="item"></param>
        //public virtual void PlcValueChange(Config.PlcDataItem item)
        //{
        //    // LogHandle.Instance.WriteRunLog(item.MachineID, "PLC", "变化:{0},{1}", item.PlcDevice, item.ValueNew);
        //    try
        //    {
        //        Regex regex = new Regex(@"V_[0-9]{2}_TriggerGrab");
        //        if (regex.IsMatch(item.PlcDevice.ToString()) && (bool)item.ValueNew)
        //        {
        //            //匹配Vision(拍照点）
        //            Config.MachineVision mv = VisionHandle.Instance.GetMachineVision(item.PlcDevice);
        //            //视觉方案
        //            Config.VisionItem vi = VisionHandle.Instance.GetVisionItem(mv.VisionDevice, mv.MachineID);

        //            #region 工位1: 胶管有无触发检测
        //            if ((item.PlcDevice.ToString() == "V_01_TriggerGrab"))
        //            {
        //                LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "发送拍照请求", " 1");
        //                VP.ProcessImage(vi);
        //                if (vi.Result == "OK")
        //                {
        //                    this.PlcWrite(Common.PlcDevice.V_01_Result_Judge, 1);
        //                    this.PlcWrite(Common.PlcDevice.V_01_VisionFinish, 1);
        //                    LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "PlcWrite", " 1");
        //                }
        //                if (vi.Result == "NG")
        //                {
        //                    this.PlcWrite(Common.PlcDevice.V_01_Result_Judge, 0);
        //                    this.PlcWrite(Common.PlcDevice.V_01_VisionFinish, 1);
        //                    LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "PlcWrite", " 0");
        //                }
        //            }
        //            #endregion


        //            #region 工位2: 刀头相机测试
        //            if ((item.PlcDevice.ToString() == "V_02_TriggerGrab"))
        //            {
        //                Console.WriteLine("触发刀头工位相机拍照");

        //                LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "发送拍照请求", " 1");
        //                VP.ProcessImage(vi);
                        
        //                //czw 2020-8-8
        //                if (vi.Result!=null)
        //                {
        //                    this.PlcWrite(Common.PlcDevice.V_02_Offset_R, vi.call.GetOutputCtrlParamTuple("Offset"));
        //                    this.PlcWrite(Common.PlcDevice.V_02_Offset_X, vi.call.GetOutputCtrlParamTuple("Dist"));
        //                    this.PlcWrite(Common.PlcDevice.V_02_VisionFinish, 1);
        //                    LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "PlcWrite", " 1");

                            
        //                }
        //                //if (vi.Result == "OK")
        //                //{
        //                //    this.PlcWrite(Common.PlcDevice.V_03_Offset_R, 1);
        //                //    this.PlcWrite(Common.PlcDevice.V_03_VisionFinish, 1);
        //                //    LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "PlcWrite", " 1");
        //                //}
        //                //if (vi.Result == "NG")
        //                //{
        //                //    this.PlcWrite(Common.PlcDevice.V_03_Offset_R, 1);
        //                //    this.PlcWrite(Common.PlcDevice.V_03_VisionFinish, 0);
        //                //    LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "PlcWrite", " 0");
        //                //}

        //            }
        //            #endregion

        //            #region 工位3: 胶条贴合检测
        //            if ((item.PlcDevice.ToString() == "V_03_TriggerGrab"))
        //            {
        //                Console.WriteLine("胶条贴合检测相机拍照");

        //                LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "发送拍照请求", " 1");
        //                VP.ProcessImage(vi);
        //                if (vi.Result == "OK")
        //                {
        //                    this.PlcWrite(Common.PlcDevice.V_03_Result_Judge, 1);

        //                    this.PlcWrite(Common.PlcDevice.V_03_VisionFinish, 1);
        //                    LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "PlcWrite", " 1");
        //                }
        //                if (vi.Result == "NG")
        //                {
        //                    this.PlcWrite(Common.PlcDevice.V_03_Result_Judge, 0);
        //                    this.PlcWrite(Common.PlcDevice.V_03_VisionFinish, 1);
        //                    LogHandle.Instance.WriteRunLog(mv.MachineID, item.PlcDevice.ToString(), "{0}:发送,{1}", "PlcWrite", " 0");
        //                }

        //                //for mes
        //                this.PlcWrite(Common.PlcDevice.CushionTapeL_Up_L_Check, vi.call.GetOutputCtrlParamTuple("MesTopLongTapeLeft"));
        //                this.PlcWrite(Common.PlcDevice.CushionTapeL_Up_R_Check, vi.call.GetOutputCtrlParamTuple("MesTopLongTapeRight"));
        //                this.PlcWrite(Common.PlcDevice.CushionTapeL_Down_L_Check, vi.call.GetOutputCtrlParamTuple("MesBottomLongTapeLeft"));
        //                this.PlcWrite(Common.PlcDevice.CushionTapeL_Down_R_Check, vi.call.GetOutputCtrlParamTuple("MesBottomLongTapeRight"));

        //            }
        //            #endregion

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogWriter.WriteException(ex);
        //    }
        //}

        #endregion


        /// <summary>
        /// 串口发送byte[]
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public void SerialSend(Common.SerialDevice device, byte[] data)
        {
            Logic.SearialHandle.Instance.SerialSend(device, data);
        }

        /// <summary>
        /// 串口发送string
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public void SerialSend(Common.SerialDevice device, string data)
        {
            Logic.SearialHandle.Instance.SerialSend(device, data);
        }

        /// <summary>
        /// 串口加锁string
        /// </summary>
        private object lock_send_serial_string = new object();
        /// <summary>
        /// 串口发送string，返回string
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public string SerialSendReturn(Common.SerialDevice device, string data)
        {
            lock (lock_send_serial_string)
            {
                Config.SerialItem serial = SearialHandle.Instance.GetConfig(device);
                //暂存包编号
                serial.Value = "";
                serial.Reset.Reset();
                SearialHandle.Instance.SerialSend(device, data);
                if (serial.ReadTimeout < 200)
                {
                    serial.Reset.WaitOne(200);
                }
                else
                {
                    serial.Reset.WaitOne(serial.ReadTimeout);
                }
                if(serial .Value =="")
                {
                  
                }
                return serial.Value.ToString();
            }
        }

        private object lock_send_serial_byte = new object();
        /// <summary>
        /// 串口加锁byte[]
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public string SerialSendReturn(Common.SerialDevice device, byte[] data)
        {
            LogHandle.Instance.WriteRunLog(this.CfgBase.MachineID, "Serial", "{0}:发送,{1}", device.ToString(),Encoding.Default.GetString(data));
            lock (lock_send_serial_byte)
            {
                Config.SerialItem serial = SearialHandle.Instance.GetConfig(device);
                //暂存包编号
                serial.Value = "";
                serial.Reset.Reset();
                SearialHandle.Instance.SerialSend(device, data);
                if (serial.ReadTimeout < 200)
                {
                    serial.Reset.WaitOne(200);
                }
                else
                {
                    serial.Reset.WaitOne(serial.ReadTimeout);
                }
                return serial.Value.ToString();
            }
        }


        /// <summary>
        /// 串口回调虚函数，机台重写
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="frid"></param>
        public virtual void SerialReceive(Common.SerialDevice serial, string data, byte[] data_byte)
        {
            LogHandle.Instance.WriteRunLog(this.CfgBase.MachineID, "Serial", "收到:{0},{1}", serial, data);
            //业务
        }

        #endregion


        /// <summary>
        /// Socket服务端发送,轮询所有连接socket,string
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="dataS">字串</param>
        public void SocketSSend(Common.SocketDevice device, string data)
        {
            LogHandle.Instance.WriteRunLog(this.CfgBase.MachineID, "SocketServer", "发送:{0},{1}", device, data);
            SocketSHandle.Instance.SocketSend(device, data);
        }

        /// <summary>
        /// Socket服务端发送,轮询所有连接socket,byte[]
        /// </summary>
        /// <param name="device">设备配置</param>
        /// <param name="dataS">byte[]</param>
        public void SocketSSend(Common.SocketDevice device, byte[] data)
        {
            SocketSHandle.Instance.SocketSend(device, data);
        }

        /// <summary>
        /// Socket服务端发送，指定socket,byte[]
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        public void SocketSSendData(System.Net.Sockets.Socket socket, byte[] data)
        {
            SocketSHandle.Instance.SendData(socket, data);
        }

        /// <summary>
        /// Socket服务端发送，指定socket,string
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        public void SocketSSendData(System.Net.Sockets.Socket socket, string data)
        {
            SocketSHandle.Instance.SendData(socket, data);
        }

        /// <summary>
        /// Socket客户端发送，string
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="dataS">字串</param>
        public void SocketCSend(Common.SocketDevice device, string data)
        {
            LogHandle.Instance.WriteRunLog(this.CfgBase.MachineID, "SocketServer", "发送:{0},{1}", device, data);
            Logic.SocketCHandle.Instance.SocketSend(device, data);
        }

        private object lock_send_socketc_byte = new object();
        /// <summary>
        /// Socket客户端发送，byte[]，返回string
        /// </summary>
        /// <param name="device">设备配置</param>
        /// <param name="dataS">byte[]</param>
        public string SocketCSendReturn(Common.SocketDevice device, byte[] data)
        {
            lock (this.lock_send_socketc_byte)
            {
                Config.SocketCItem socket = SocketCHandle.Instance.GetConfig(device);
                //暂存包编号
                socket.Value = "";
                socket.Reset.Reset();
                SocketCHandle.Instance.SocketSend(device, data);
                socket.Reset.WaitOne(500);
                return socket.Value.ToString();
            }
        }

        /// <summary>
        /// Socket客户端string锁
        /// </summary>
        private object lock_send_socketc_string = new object();
        /// <summary>
        /// Socket客户端发送，string，返回string
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string SocketCSendReturn(Common.SocketDevice device, string data)
        {
            lock (this.lock_send_socketc_string)
            {
                Config.SocketCItem socket = SocketCHandle.Instance.GetConfig(device);
                //暂存包编号
                socket.Value = "";
                socket.Reset.Reset();
                SocketCHandle.Instance.SocketSend(device, data);
                if (socket.OutTime < 500)
                {
                    socket.Reset.WaitOne(500);
                }
                else
                {
                    socket.Reset.WaitOne(socket.OutTime);
                }
                return socket.Value.ToString();
            }
        }

        /// <summary>
        /// Socket服务端byte锁
        /// </summary>
        private object lock_send_sockets_byte = new object();
        /// <summary>
        ///  Socket服务端发送，byte[]，返回string
        /// </summary>
        /// <param name="device">设备配置</param>
        /// <param name="dataS">byte[]</param>
        public string SocketSSendReturn(Common.SocketDevice device, byte[] data)
        {
            lock (this.lock_send_sockets_byte)
            {
                Config.SocketSItem socket = SocketSHandle.Instance.GetConfig(device);
                //暂存包编号
                socket.Value = "";
                socket.Reset.Reset();
                SocketSHandle.Instance.SocketSend(device, data);
                if (socket.OutTime < 500)
                {
                    socket.Reset.WaitOne(500);
                }
                else
                {
                    socket.Reset.WaitOne(socket.OutTime);
                }
                return socket.Value.ToString();
            }
        }

        /// <summary>
        /// Socket服务端string锁
        /// </summary>
        private object lock_send_sockets_string = new object();
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="device">设备配置</param>
        /// <param name="data">string</param>
        public string SocketSSendReturn(Common.SocketDevice device, string data)
        {
            lock (this.lock_send_sockets_string)
            {
                Config.SocketSItem socket = SocketSHandle.Instance.GetConfig(device);
                //暂存包编号
                socket.Value = "";
                socket.Reset.Reset();
                SocketSHandle.Instance.SocketSend(device, data);
                if (socket.OutTime < 500)
                {
                    socket.Reset.WaitOne(500);
                }
                else
                {
                    socket.Reset.WaitOne(socket.OutTime);
                }
                return socket.Value.ToString();
            }
        }

        /// <summary>
        /// socket客户端回调，虚函数，机台重写业务
        /// </summary>
        /// <param name="device"></param>
        /// <param name="socket"></param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public virtual void SocketCReceive(Config.SocketCItem device, System.Net.Sockets.Socket socket, byte[] dataB, string dataS)
        {
            //业务
            LogHandle.Instance.WriteRunLog(device.MachineID, device.SocketDevice.ToString(), "收到:{0},Client",  dataS);
            //若抛出ABB业务
            if (device.SocketDevice.ToString().Contains("ABB"))
            {
               
            }
        }

        /// <summary>
        /// socket服务端回调，虚函数，机台重写业务
        /// </summary>
        /// <param name="device"></param>
        /// <param name="socket"></param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public virtual void SocketSReceive(Config.SocketSItem device, System.Net.Sockets.Socket socket, byte[] dataB, string dataS)
        {
            //业务
            LogHandle.Instance.WriteRunLog(device.MachineID, device.SocketDevice.ToString(), "收到:{0},Server",dataS);
            //若抛出ABB业务
            if (device.SocketDevice.ToString().Contains("ABB"))
            {
                
            }
        }

        #region 报警操作

        /// <summary>
        /// 写报警日志
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="note_format"></param>
        /// <param name="args"></param>
        public void WriteAlarmLog(string machine, string note_format, params object[] args)
        {
            LogHandle.Instance.WriteRunLog(machine, "ALARM", note_format, args);
        }

        #endregion

 

    }
}
