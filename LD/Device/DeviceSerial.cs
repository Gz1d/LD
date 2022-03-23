using System;
using System.Text;
using System.Collections;


namespace LD.Device
{

    /// <summary>
    /// Plc设备
    /// </summary>
    public class DeviceSerial : Device
    {

        /// <summary>
        /// Socket集
        /// </summary>
        public SortedList Serials = new SortedList();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        public DeviceSerial(Config.ConfigSerial config)
        {
            this.config = config;
        }

        /// <summary>
        /// 串口配置表
        /// </summary>
        private Config.ConfigSerial config
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
                foreach (Config.SerialItem item in this.config.SerialItems)
                {
                    if (!item.IsActive) continue;
                    System.IO.Ports.SerialPort serial = new System.IO.Ports.SerialPort();
                    serial.PortName = item.PortName;
                    serial.BaudRate = item.BaudRate;
                    serial.StopBits = item.StopBits;
                    serial.Parity = item.Parity;
                    serial.DataBits = item.DataBits;
                    serial.ReceivedBytesThreshold = item.ReceivedBytesThreshold;
                    //设置一行结束符标志
                    if (!string.IsNullOrEmpty(item.NewLine))
                    {
                        try
                        {
                            byte[] line = Common.HexStringToByteArray(item.NewLine);
                            serial.NewLine = Encoding.Default.GetString(line); ////回车换行，命令结束符\r\n
                        }
                        catch { }
                    }
                    serial.ReadTimeout = item.ReadTimeout;
                    serial.WriteTimeout = item.WriteTimeout;
                    if (item.CallReceive)
                        serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Seria_DataReceived);
                    item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(item_PropertyChanged);
                    item.Tag = serial;
                    Serials.Add(item.SerialDevice, serial);
                }
             
            }
            catch (Exception ex)
            {
                throw new InitException(this.ToString(), ex.ToString());
            }
        }

        private object seriallock = new object();



/// <summary>
/// 串口接收数据
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void Seria_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
               // lock (this.seriallock)
                {
                    System.IO.Ports.SerialPort serial = (System.IO.Ports.SerialPort)sender;
                    string data = null;
                    byte[] dataB = null;
                    Common.SerialDevice device = Common.SerialDevice.NULL;
                    foreach (Config.SerialItem item in this.config.SerialItems)
                    {
                        if (item.Tag == serial)
                        {
                            if (item.CallReceive)
                            {
                                if (item.SleepMs <= 0) item.SleepMs = 100;
                                System.Threading.Thread.Sleep(item.SleepMs);//等待保障缓冲区有一行数据
                                if (string.IsNullOrEmpty(item.NewLine))
                                {
                                    dataB = new byte[serial.BytesToRead];
                                    serial.Read(dataB, 0, dataB.Length);
                                    //编码缺省
                                    data = Encoding.Default.GetString(dataB, 0, dataB.Length);
                                    device = item.SerialDevice;
                                }
                                else
                                {
                                    data = serial.ReadLine();
                                    dataB = Encoding.Default.GetBytes(data);
                                    device = item.SerialDevice;
                                }
                            }
                            break;
                        }
                    }

                    Logic.SearialHandle.Instance.SerialReceive(device, dataB, data);
                   // if (!string.IsNullOrEmpty(data))
                   //     this.Async_SerialReceive(device, dataB, data);
                }
            }
            catch { }
        }


        private delegate void delegate_serial_receive(Common.SerialDevice device, byte[] dataB, string dataS);

        public void Async_SerialReceive(Common.SerialDevice device, byte[] dataB, string dataS)//异步调用委托
        {
            delegate_serial_receive rece = new delegate_serial_receive(Logic.SearialHandle.Instance.SerialReceive);
            rece.BeginInvoke(device, dataB, dataS, null, rece);
        }




        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }



        /// <summary>
        /// 启动
        /// </summary>
        public override void DoStart()
        {
            try
            {
                IDictionaryEnumerator enumerator = this.Serials.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    try
                    {
                        System.IO.Ports.SerialPort serial = enumerator.Value as System.IO.Ports.SerialPort;
                        serial.Open();
                        //打开正常
                        foreach (Config.SerialItem item in this.config.SerialItems)
                        {
                            if (item.Tag == serial)
                            {
                                if (serial == (System.IO.Ports.SerialPort)item.Tag)
                                {
                                    item.IsConnected = true;
                                }
                                Common.SerialDevice device = (Common.SerialDevice)enumerator.Key;
                                //foreach (var cam in Config.ConfigManager.Instance.ConfigCamera.CamdeviceList)
                                //{
                                //    if (cam.SerialDevice == device)
                                //    {
                                //        try
                                //        {
                                //            switch (device)
                                //            {
                                                
                                //                default:
                                //                    break;
                                //            }
                                //        }
                                //        catch (Exception ex)
                                //        {

                                //        }
                                //        break;
                                //    }
                                //}
                                break;
                            }
                        }
                    }
                    catch 
                    {

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
                IDictionaryEnumerator enumerator = this.Serials.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    System.IO.Ports.SerialPort serial = enumerator.Value as System.IO.Ports.SerialPort;
                    serial.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(Seria_DataReceived);
                    Common.SerialDevice device = (Common.SerialDevice)enumerator.Key;
                    //foreach (var cam in Config.ConfigManager.Instance.ConfigCamera.CamdeviceList)
                    //{
                    //    if (cam.SerialDevice == device)
                    //    {
                    //        try
                    //        {
                    //            switch (device)
                    //            {
                    //                default:
                    //                    break;
                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //        }
                    //        break;
                    //    }
                    //}
                    serial.Close();
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
                IDictionaryEnumerator enumerator = this.Serials.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    System.IO.Ports.SerialPort serial = enumerator.Value as System.IO.Ports.SerialPort;
                    serial.Dispose();
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
                return "DeviceSerial";
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
        public void SerialSend(Common.SerialDevice device, string data)
        {
            try
            {
                System.IO.Ports.SerialPort serial = (System.IO.Ports.SerialPort)this.Serials[device];
                byte[] buff = System.Text.Encoding.Default.GetBytes(data);
                serial.DiscardInBuffer();
                serial.DiscardOutBuffer();
                //this.SerialSend(device, buff);
                serial.Write(buff, 0, buff.Length);
            }
            catch (Exception ex)
            {
                Log.LogWriter.WriteException(ex);
            }
        }


        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        public void SerialSend(Common.SerialDevice device, byte[] data)
        {
            System.IO.Ports.SerialPort serial = (System.IO.Ports.SerialPort)Serials[device];
            serial.DiscardInBuffer();
            serial.DiscardOutBuffer();
            serial.Write(data, 0, data.Length);
        }

        public string SerialReadLine(Common.SerialDevice device)
        {
            try
            {
                 System.IO.Ports.SerialPort serial = (System.IO.Ports.SerialPort)Serials[device];
                 return serial.ReadLine();
            }
            catch
            {
                return "ERROR";
            }
        }

        public byte[] SerialRead(Common.SerialDevice device)
        {
            try
            {
                System.IO.Ports.SerialPort serial = (System.IO.Ports.SerialPort)Serials[device];
                System.Threading.Thread.Sleep(20);
                byte[] dataB = new byte[serial.BytesToRead];
                serial.Read(dataB, 0, dataB.Length);
                return dataB;
            }
            catch
            {
                return null;
            }
        }

        public string SerialReadString(Common.SerialDevice device)
        {
            try
            {
                byte[] dataB = this.SerialRead(device);

                return "";
            }
            catch
            {
                return null;
            }
        }
    }
}
