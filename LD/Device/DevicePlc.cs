using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HslCommunication;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Siemens;
using HslCommunication.Profinet.Melsec;
using HslCommunication.Profinet.Keyence;


namespace LD.Device
{

    /// <summary>
    /// Plc设备
    /// </summary>
    public class DevicePlc : Device
    {
        /// <summary>
        /// Plc集
        /// </summary>
        public SortedList Plcs = new SortedList();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        public DevicePlc(LD.Config.ConfigPlc config)
        {
            this.config = config;
        }

        /// <summary>
        /// plc配置表
        /// </summary>
        private LD.Config.ConfigPlc config
        {
            set;
            get;
        }

        /// <summary>
        /// 关闭标志
        /// </summary>
        private bool isClose
        {
            set;
            get;
        }

        public event LD.Config.PlcValueChangeEventHandler PlcValueChanged = null;

        //KeyencePLC初始化
        void DoInitKeyencePlc(LD.Config.PlcTypeItem item)
        {
            KeyenceMcNet KeyencePLC = new KeyenceMcNet(item.IP, item.Port);
            KeyencePLC.ConnectTimeOut = item.ConnectOutTime;
            item.Tag = KeyencePLC;
            if (KeyencePLC != null) KeyencePLC.ConnectClose();
            OperateResult connect = KeyencePLC.ConnectServer();
            if (connect.IsSuccess)
            {
                item.IsConnected = true;
            }
            else
            {
                item.IsConnected = false;
            }
        }

        //MelsecPLC初始化
        void DoInitMelsecPlc(LD.Config.PlcTypeItem item)
        {
            MelsecMcNet MelsecPlc = new MelsecMcNet(item.IP, item.Port);
            MelsecPlc.ConnectTimeOut = item.ConnectOutTime;
            item.Tag = MelsecPlc;
            if (MelsecPlc != null) MelsecPlc.ConnectClose();
            OperateResult connect = MelsecPlc.ConnectServer();
            if (connect.IsSuccess)
            {
                item.IsConnected = true;
            }
            else
            {
                item.IsConnected = false;
            }
        }

        //SiemensPLC初始化
        void DoInitS7Plc(LD.Config.PlcTypeItem item)
        {
            //if (item.Timer == null) item.Timer = new System.Timers.Timer();
            //item.Timer.Enabled = false;
            //item.Timer.Interval = 1000;
            //item.Timer.Elapsed += TimerCheck_Tick;

            SiemensS7Net S7PLC = new SiemensS7Net((SiemensPLCS)item.DevType);
            S7PLC.ConnectTimeOut = item.ConnectOutTime;
            S7PLC.Rack = byte.Parse(item.Rack);
            S7PLC.Slot = byte.Parse(item.Slot);

            System.Net.IPAddress address;
            if (!System.Net.IPAddress.TryParse(item.IP, out address))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("{0} input wrong!", item.IP));
                return;
            }

            int port;
            if (!int.TryParse(item.Port.ToString(), out port))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("port {0} input wrong!", item.Port));
                return;
            }
            S7PLC.IpAddress = item.IP;
            S7PLC.Port = port;
            item.Tag = S7PLC;

            if (S7PLC != null) S7PLC.ConnectClose();
            OperateResult connect = S7PLC.ConnectServer();
            if (connect.IsSuccess)
            {
                item.IsConnected = true;
                //Plcs.Add(item.DevName, S7PLC);
            }
            else
            {
                item.IsConnected = false;
                //Plcs.Add(item, S7PLC);
            }

        }

        //MudbusTCP设备初始化
        void DoInitMudbusTcp(LD.Config.PlcTypeItem item)
        {
            // 连接
            byte station;
            if (!byte.TryParse(item.Station, out station))
            {
                MessageBox.Show(string.Format("station {0} input is wrong！", item.Station));
                return;
            }
            System.Net.IPAddress address;
            if (!System.Net.IPAddress.TryParse(item.IP, out address))
            {
                MessageBox.Show(string.Format("IP {0} input wrong！", item.IP));
                return;
            }
            int port;
            if (!int.TryParse(item.Port.ToString(), out port))
            {
                MessageBox.Show(string.Format("port {0} input wrong！", item.Port));
                return;
            }
            ModbusTcpNet busTcpClient = new ModbusTcpNet(item.IP, port, station);
            busTcpClient.AddressStartWithZero = item.AddressStartWithZero;
            if (busTcpClient != null) busTcpClient.DataFormat = item.DataFormat;
            busTcpClient.IsStringReverse = item.IsStringReverse;
            item.Tag = busTcpClient;
            OperateResult connect = busTcpClient.ConnectServer();
            if (connect.IsSuccess)
            {
                item.IsConnected = true;
                //Plcs.Add(item.DevName, busTcpClient);
            }
            else
            {
                item.IsConnected = false;
            }

        }

        //MudbusRTU设备初始化
        void DoInitMudbusRtu(LD.Config.PlcTypeItem item)
        {
            // 连接
            byte station;
            if (!byte.TryParse(item.Station, out station))
            {
                MessageBox.Show(string.Format("station {0} input is wrong！", item.Station));
                return;
            }

            int baudRate;
            if (!int.TryParse(item.BaudRate.ToString(), out baudRate))
            {
                MessageBox.Show(string.Format("baudRate {0} input wrong！", item.BaudRate));
                return;
            }

            int dataBits;
            if (!int.TryParse(item.DataBits.ToString(), out dataBits))
            {
                MessageBox.Show(string.Format("dataBits {0} input wrong！", item.DataBits));
                return;
            }

            int stopBits;
            if (!int.TryParse(item.StopBits.ToString(), out stopBits))
            {
                MessageBox.Show(string.Format("stopBits {0} input wrong！", item.StopBits));
                return;
            }

            ModbusRtu busRtuClient = new ModbusRtu(station);
            busRtuClient.AddressStartWithZero = item.AddressStartWithZero;
            if (busRtuClient != null) busRtuClient.DataFormat = item.DataFormat;
            busRtuClient.IsStringReverse = item.IsStringReverse;
            busRtuClient.SerialPortInni(sp =>
                {
                    sp.PortName = item.PortName;
                    sp.BaudRate = baudRate;
                    sp.DataBits = dataBits;
                    sp.StopBits = stopBits == 0 ? StopBits.None : (stopBits == 1 ? StopBits.One : StopBits.Two);
                    sp.Parity = item.Parity;
                });
            item.Tag = busRtuClient;

            busRtuClient.Open();
            if (busRtuClient.IsOpen())
            {
                item.IsConnected = true;
                //Plcs.Add(item.DevName, busRtuClient);
            }
            else
            {
                item.IsConnected = false;
            }
        }

        /// plc项目变化事件，异步出去
        void plc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PlcValueChanged != null && e.PropertyName == "ValueNew")
            {
                LD.Config.PlcDataItem plc = (LD.Config.PlcDataItem)sender;
                //异步出去
                this.PlcValueChanged.BeginInvoke(plc, plc.ValueNew, null, this.PlcValueChanged);
            }
        }

        //连接监控
        public void TimerCheck_Tick(object sender, EventArgs e)
        {
            IDictionaryEnumerator enumerator = Plcs.GetEnumerator();
            while (enumerator.MoveNext())
            {
                SiemensS7Net plc = enumerator.Value as SiemensS7Net;
                if (plc != null)
                {
                    if (!plc.ReadBool("M0").IsSuccess) plc.ConnectClose();
                }
            }
        }

        //PLC设备连接属性变化回调
        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {

            }
        }

        //PLC值改变
        void PlcClient_PlcValueChanged(LD.Config.PlcDataItem plc, object value)
        {
         LD.Logic.PlcHandle.Instance.PlcValueChange((LD.Config.PlcDataItem)plc);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void DoInit()
        {
            try
            {
                foreach (LD.Config.PlcTypeItem item in this.config.PlcTypeItems)
                {
                    if (!item.IsActive) continue;
                    switch (item.DevType)
                    {
                        //keyence
                        case Common.DeviceType.Keyence:
                            this.DoInitKeyencePlc(item);
                            break;

                        //Melsec
                        case Common.DeviceType.Qseries:
                            this.DoInitMelsecPlc(item);
                            break;

                        //Seiemens
                        case Common.DeviceType.S1200:
                        case Common.DeviceType.S1500:
                        case Common.DeviceType.S200:
                        case Common.DeviceType.S200Smart:
                        case Common.DeviceType.S300:
                        case Common.DeviceType.S400:
                            this.DoInitS7Plc(item);
                            break;

                        case Common.DeviceType.ModbusTcp:
                            this.DoInitMudbusTcp(item);
                            break;

                        case Common.DeviceType.ModbusRtu:
                            this.DoInitMudbusRtu(item);
                            break;

                        default:
                            break;
                    }

                    //item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }

                foreach (LD.Config.PlcDataItem plc in this.config.PlcDataItems)
                {
                    plc.PropertyChanged += new PropertyChangedEventHandler(plc_PropertyChanged);
                    System.Threading.Tasks.Task.Factory.StartNew(new Action(() => {
                        Thread.Sleep(100);
                        plc.PropertyChanged += new PropertyChangedEventHandler(Ui.frmSiemens.Instance.plc_PropertyChanged);
                    }));
                        //(new Action(() => { }));
                    
                }

                this.PlcValueChanged += new LD.Config.PlcValueChangeEventHandler(PlcClient_PlcValueChanged);

            }
            catch (Exception ex)
            {
                throw new InitException(this.ToString(), ex.ToString());
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public override void DoStart()
        {
            try
            {
                //IDictionaryEnumerator enumerator = this.Plcs.GetEnumerator();
                //while (enumerator.MoveNext())
                //{
                //    Thread ReadDevThread = new Thread(new ParameterizedThreadStart(this.RThreadReadServer));
                //    ReadDevThread.IsBackground = true;
                //    ReadDevThread.Start(enumerator.Key);
                //}

                foreach (LD.Config.PlcTypeItem item in this.config.PlcTypeItems)
                {
                    if (item.IsConnected)
                    {
                        Thread ReadDevThread = new Thread(new ParameterizedThreadStart(this.RThreadReadServer));
                        ReadDevThread.IsBackground = true;
                        ReadDevThread.Start(item);
                    }
                }

                this.isClose = false;

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
                this.isClose = true;
                foreach (LD.Config.PlcTypeItem item in this.config.PlcTypeItems)
                {
                    if (!item.IsActive || !item.IsConnected) continue;

                    switch (item.DevType)
                    {
                        case Common.DeviceType.S1200:
                        case Common.DeviceType.S1500:
                        case Common.DeviceType.S200:
                        case Common.DeviceType.S200Smart:
                        case Common.DeviceType.S300:
                        case Common.DeviceType.S400:
                            SiemensS7Net plc = (SiemensS7Net)item.Tag;
                            plc.ConnectClose();
                            break;

                        case Common.DeviceType.ModbusTcp:
                            ModbusTcpNet mdTcp = (ModbusTcpNet)item.Tag;
                            mdTcp.ConnectClose();
                            break;

                        case Common.DeviceType.ModbusRtu:
                            ModbusRtu mdRtu = (ModbusRtu)item.Tag;
                            mdRtu.Close();
                            break;

                        default:
                            break;
                    }

                    item.IsConnected = false;
                }

                foreach (LD.Config.PlcDataItem plc in this.config.PlcDataItems)
                {
                    plc.PropertyChanged -= new PropertyChangedEventHandler(plc_PropertyChanged);
                    plc.PropertyChanged -= new PropertyChangedEventHandler(Ui.frmSiemens.Instance.plc_PropertyChanged);
                }

                this.PlcValueChanged -= new LD.Config.PlcValueChangeEventHandler(PlcClient_PlcValueChanged);

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
                //IDictionaryEnumerator enumerator = Plcs.GetEnumerator();
                //while (enumerator.MoveNext())
                //{
                //    SiemensS7Net plc = enumerator.Value as SiemensS7Net;
                //    if (plc != null) plc = null;
                //}
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
                return "DevicePlc";
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

        //循环读取所有PLC实时值
        private void RThreadReadServer(object obj)
        {
            while (!this.isClose)
            {
                if (this.config.bReadThread)
                {
                    try
                    {
                        LD.Config.PlcTypeItem devType = obj as LD.Config.PlcTypeItem;
                        IEnumerable ie = from lst in this.config.PlcDataItems
                                         where lst.DeviceName == devType.DevName
                                         select lst;

                        List<LD.Config.PlcDataItem> ioLst = ie.Cast<LD.Config.PlcDataItem>().ToList();

                        foreach (LD.Config.PlcDataItem item in ioLst)
                        {
                            //Config.PlcTypeItem devType = Logic.PlcHandle.Instance.GetPlcTypeItem(item.PlcDevice);
                            if (!item.IsActive) continue;
                            LD.Logic.PlcHandle.Instance.ReadValue(item.PlcDevice);
                        }
                        Thread.Sleep(1);
                    }
                    catch (Exception)
                    { }  
                }
            }
        }



    }
}
