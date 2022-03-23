using HslCommunication.ModBus;
using HslCommunication.Profinet.Siemens;
using HslCommunication.Profinet.Keyence;
using HslCommunication.Profinet.Melsec;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LD.Logic
{
    /// <summary>
    /// PLC处理类，
    /// </summary>
    public class PlcHandle
    {
        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static PlcHandle instance = new PlcHandle();
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private PlcHandle()
        {

        }

        /// <summary>
        /// 静态属性
        /// </summary>
        public static PlcHandle Instance
        {
            get { return instance; }
        }
        #endregion

        public Config.ConfigPlc config
        {
            get
            {
                return Config.ConfigManager.Instance.ConfigPlc;
            }
        }
        public event LD.Config.PlcValueChangeEventHandler PlcValueChangedEvent = null;
        #region PLC&Mudbus设备读写
        // 读设备的值
        public object ReadValue(Common.PlcDevice plcDevice)
        {
            Config.PlcDataItem devData = this.GetPlcDataItem(plcDevice);
            if (devData == null) return null;
            Config.PlcTypeItem devType = this.GetPlcTypeItem(devData.DeviceName);
            switch (devType.DevType) {
                //Keyence
                case Common.DeviceType.Keyence:
                    KeyenceMcNet kPlcClient = (KeyenceMcNet)devType.Tag;
                    return this.ReadSieTcpValue(devType, devData, kPlcClient);
                //Melsec 三菱PLC
                case Common.DeviceType.Qseries:
                    MelsecMcNet mPlcClient = (MelsecMcNet)devType.Tag;
                    return this.ReadSieTcpValue(devType, devData, mPlcClient);
                //Siemens
                case Common.DeviceType.S1200:
                case Common.DeviceType.S300:
                case Common.DeviceType.S400:
                case Common.DeviceType.S1500:
                case Common.DeviceType.S200Smart:
                case Common.DeviceType.S200:
                    SiemensS7Net sPlcClient = (SiemensS7Net)devType.Tag;
                    return this.ReadSieTcpValue(devType, devData, sPlcClient);
                case Common.DeviceType.ModbusTcp:
                    ModbusTcpNet mTcpClient = (ModbusTcpNet)devType.Tag;
                    return this.ReadModTcpValue(devType, devData, mTcpClient);
                case Common.DeviceType.ModbusRtu:
                    ModbusRtu mRtuClinet = (ModbusRtu)devType.Tag;
                    return this.ReadModRtuValue(devType, devData, mRtuClinet);
                default:
                    return null;
            }
        }

        public object GetValue(Common.PlcDevice plcDevice)
        {
            Config.PlcDataItem devData = this.GetPlcDataItem(plcDevice);
            return devData.ValueNew;
        }

        // 读设备的值
        public object ReadValue(string itemName)
        {
            //try
            //{
            Config.PlcDataItem devData = this.GetPlcDataItem(itemName);
            Config.PlcTypeItem devType = this.GetPlcTypeItem(devData.DeviceName);

            switch (devType.DevType)
            {
                case Common.DeviceType.S1200:
                case Common.DeviceType.S300:
                case Common.DeviceType.S400:
                case Common.DeviceType.S1500:
                case Common.DeviceType.S200Smart:
                case Common.DeviceType.S200:
                    SiemensS7Net sPlcClient = (SiemensS7Net)devType.Tag;
                    return this.ReadSieTcpValue(devType, devData, sPlcClient);
                //break;

                case Common.DeviceType.ModbusTcp:
                    ModbusTcpNet mTcpClient = (ModbusTcpNet)devType.Tag;
                    return this.ReadModTcpValue(devType, devData, mTcpClient);
                //break;

                case Common.DeviceType.ModbusRtu:
                    ModbusRtu mRtuClinet = (ModbusRtu)devType.Tag;
                    return this.ReadModRtuValue(devType, devData, mRtuClinet);
                //break;

                default:
                    return null;
                //break;
            }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        // 写设备的值
        public bool  WriteValue(Common.PlcDevice plcDevice, object value)
        {
            bool IsOk = false;
            try
            {
                Config.PlcDataItem devData = this.GetPlcDataItem(plcDevice);
                Config.PlcTypeItem devType = this.GetPlcTypeItem(devData.DeviceName);

                switch (devType.DevType)
                {
                    //Keyence
                    case Common.DeviceType.Keyence:
                        KeyenceMcNet kPlcClient = (KeyenceMcNet)devType.Tag;
                        IsOk = this.WriteSieTcpValue(devType, devData, kPlcClient, value);
                        break;
                    //Melsec
                    case Common.DeviceType.Qseries:
                        MelsecMcNet mPlcClient = (MelsecMcNet)devType.Tag;
                        IsOk = this.WriteSieTcpValue(devType, devData, mPlcClient, value);
                        break;


                    case Common.DeviceType.S1200:
                    case Common.DeviceType.S300:
                    case Common.DeviceType.S400:
                    case Common.DeviceType.S1500:
                    case Common.DeviceType.S200Smart:
                    case Common.DeviceType.S200:
                        SiemensS7Net sPlcClient = (SiemensS7Net)devType.Tag;
                        IsOk = this.WriteSieTcpValue(devType, devData, sPlcClient, value);
                        break;

                    case Common.DeviceType.ModbusTcp:
                        ModbusTcpNet mTcpClient = (ModbusTcpNet)devType.Tag;
                        IsOk = this.WriteModTcpValue(devType, devData, mTcpClient, value);
                        break;

                    case Common.DeviceType.ModbusRtu:
                        ModbusRtu mRtuClinet = (ModbusRtu)devType.Tag;
                        IsOk = this.WriteModRtuValue(devType, devData, mRtuClinet, value);
                        break;

                    default:
                        break;
                }
            }
            catch { }
            return IsOk;

        }

        // 写设备的值
        public bool  WriteValue(string itemName, object value)
        {
            bool IsOK = false;
            try
            {
                Config.PlcDataItem devData = this.GetPlcDataItem(itemName);
                Config.PlcTypeItem devType = this.GetPlcTypeItem(devData.DeviceName);

                switch (devType.DevType)
                {
                    case Common.DeviceType.S1200:
                    case Common.DeviceType.S300:
                    case Common.DeviceType.S400:
                    case Common.DeviceType.S1500:
                    case Common.DeviceType.S200Smart:
                    case Common.DeviceType.S200:
                        SiemensS7Net sPlcClient = (SiemensS7Net)devType.Tag;
                        IsOK =this.WriteSieTcpValue(devType, devData, sPlcClient, value);
                        break;

                    case Common.DeviceType.ModbusTcp:
                        ModbusTcpNet mTcpClient = (ModbusTcpNet)devType.Tag;
                        IsOK=this.WriteModTcpValue(devType, devData, mTcpClient, value);
                        break;

                    case Common.DeviceType.ModbusRtu:
                        ModbusRtu mRtuClinet = (ModbusRtu)devType.Tag;
                        IsOK=this.WriteModRtuValue(devType, devData, mRtuClinet, value);
                        break;

                    default:
                        break;
                }
            }
            catch { }
            return IsOK;
        }
        #endregion


        #region KeyencePlc读写操作

        //PLC读取
        private object ReadSieTcpValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, KeyenceMcNet plc)
        {
            try
            {
                if (plctype == null || !plctype.IsConnected || plcdata == null || plc == null) return null;

                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Bool
                        plcdata.ValueNew = plc.ReadBool(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Byte://Byte
                        plcdata.ValueNew = plc.Read(plcdata.Address, 1).Content;
                        break;

                    case Common.DataTypes.Short:
                        plcdata.ValueNew = plc.ReadInt16(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Ushort:
                        plcdata.ValueNew = plc.ReadUInt16(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Int:
                        plcdata.ValueNew = plc.ReadInt32(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.UInt:
                        plcdata.ValueNew = plc.ReadUInt32(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Long:
                        long lValueNew = 0;
                        if (long.TryParse(plc.ReadInt64(plcdata.Address).Content.ToString(), out lValueNew))
                        {
                            long temp = BpLong.SwapInt64(lValueNew);
                            plcdata.ValueNew = temp;
                        }
                        // plcdata.ValueNew = plc.ReadInt64(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.ULong:
                        plcdata.ValueNew = plc.ReadUInt64(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Float:
                        plcdata.ValueNew = plc.ReadFloat(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Double:
                        plcdata.ValueNew = plc.ReadDouble(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.String:
                        HslCommunication.OperateResult<byte[]> data = (HslCommunication.OperateResult<byte[]>)plc.Read(plcdata.Address, 50);
                        if (data != null && data.Content != null && data.Content.Length > 2)
                        {
                            List<byte> lstData = new List<byte>();
                            int nLen = data.Content[1];
                            for (int i = 2; i < nLen + 2; i++)
                            {
                                lstData.Add(data.Content[i]);
                            }
                            plcdata.ValueNew = System.Text.Encoding.ASCII.GetString(lstData.ToArray());
                        }
                        break;

                    default:
                        break;
                }
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }

            return plcdata.ValueNew;

        }

        //PLC写入
        private bool WriteSieTcpValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, KeyenceMcNet plc, object value)
        {
            HslCommunication.OperateResult rlt = new HslCommunication.OperateResult();
            rlt.IsSuccess = false;
            try
            {
                //string[] strAdrss = plcdata.Address.Split('.');
                //string Address = strAdrss[0] + "." + Regex.Replace(strAdrss[1], @"^[A-Za-z]+", string.Empty);

                if (plctype == null || !plctype.IsConnected || plcdata == null || plc == null) return rlt.IsSuccess;
                 
                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Bool
                        rlt= plc.Write(plcdata.Address, Convert.ToBoolean(value));
                        break;

                    case Common.DataTypes.Byte://Byte
                        rlt= plc.Write(plcdata.Address, Convert.ToChar(value));
                        break;

                    case Common.DataTypes.Short:
                        rlt = plc.Write(plcdata.Address, Convert.ToInt16(value));
                        break;

                    case Common.DataTypes.Ushort:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt16(value));
                        break;

                    case Common.DataTypes.Int:
                        rlt = plc.Write(plcdata.Address, Convert.ToInt32(value));
                        break;

                    case Common.DataTypes.UInt:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt32(value));
                        break;

                    case Common.DataTypes.Long:
                        long lValue = 0;
                        if (long.TryParse(value.ToString(), out lValue))
                        {
                            long lValueNew = BpLong.SwapInt64(lValue);
                            rlt = plc.Write(plcdata.Address, lValueNew);
                        }
                        //plc.Write(plcdata.Address, Convert.ToInt64(value));
                        break;
                    case Common.DataTypes.ULong:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt64(value));
                        break;
                    case Common.DataTypes.Float:
                        rlt = plc.Write(plcdata.Address, float.Parse(value.ToString()));
                        break;
                    case Common.DataTypes.Double:
                        rlt = plc.Write(plcdata.Address, Convert.ToDouble(value));
                        break;
                    case Common.DataTypes.String:
                        if (value != null)
                        {
                            byte[] btValue = System.Text.Encoding.ASCII.GetBytes(value.ToString());
                            byte[] arrData = new byte[btValue.Length + 2];
                            arrData[0] = 50;
                            arrData[1] = (byte)btValue.Length;
                            btValue.CopyTo(arrData, 2);
                            rlt = plc.Write(plcdata.Address, arrData);
                        }
                        break;
                    default: break;

                }
                return rlt.IsSuccess;
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
                return rlt.IsSuccess;
            }
        }

        #endregion

        #region MelsecPlc读写操作

        //PLC读取
        private object ReadSieTcpValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, MelsecMcNet plc)
        {
            try
            {
                if (plctype == null || !plctype.IsConnected || plcdata == null || plc == null) return null;

                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Bool
                        plcdata.ValueNew = plc.ReadBool(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Byte://Byte
                        plcdata.ValueNew = plc.Read(plcdata.Address, 1).Content;
                        break;

                    case Common.DataTypes.Short:
                        plcdata.ValueNew = plc.ReadInt16(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Ushort:
                        plcdata.ValueNew = plc.ReadUInt16(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Int:
                        plcdata.ValueNew = plc.ReadInt32(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.UInt:
                        plcdata.ValueNew = plc.ReadUInt32(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Long:
                        long lValueNew = 0;
                        if (long.TryParse(plc.ReadInt64(plcdata.Address).Content.ToString(), out lValueNew))
                        {
                            long temp = BpLong.SwapInt64(lValueNew);
                            plcdata.ValueNew = temp;
                        }
                        // plcdata.ValueNew = plc.ReadInt64(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.ULong:
                        plcdata.ValueNew = plc.ReadUInt64(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Float:
                        plcdata.ValueNew = plc.ReadFloat(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Double:
                        plcdata.ValueNew = plc.ReadDouble(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.String:
                        HslCommunication.OperateResult<byte[]> data = (HslCommunication.OperateResult<byte[]>)plc.Read(plcdata.Address, 50);
                        if (data != null && data.Content != null && data.Content.Length > 2)
                        {
                            List<byte> lstData = new List<byte>();
                            int nLen = data.Content[1];
                            for (int i = 2; i < nLen + 2; i++)
                            {
                                lstData.Add(data.Content[i]);
                            }
                            plcdata.ValueNew = System.Text.Encoding.ASCII.GetString(lstData.ToArray());
                        }
                        break;

                    default:
                        break;
                }
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }

            return plcdata.ValueNew;

        }

        //PLC写入
        private bool  WriteSieTcpValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, MelsecMcNet plc, object value)
        {
            HslCommunication.OperateResult rlt = new HslCommunication.OperateResult();
            rlt.IsSuccess = false;
            try
            {
                //string[] strAdrss = plcdata.Address.Split('.');
                //string Address = strAdrss[0] + "." + Regex.Replace(strAdrss[1], @"^[A-Za-z]+", string.Empty);

                if (plctype == null || !plctype.IsConnected || plcdata == null || plc == null) return rlt.IsSuccess;

                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Bool
                        rlt = plc.Write(plcdata.Address, Convert.ToBoolean(value));
                        break;

                    case Common.DataTypes.Byte://Byte
                        rlt =plc.Write(plcdata.Address, Convert.ToChar(value));
                        break;

                    case Common.DataTypes.Short:
                        rlt = plc.Write(plcdata.Address, Convert.ToInt16(value));
                        break;

                    case Common.DataTypes.Ushort:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt16(value));
                        break;

                    case Common.DataTypes.Int:
                        rlt = plc.Write(plcdata.Address, Convert.ToInt32(value));
                        break;

                    case Common.DataTypes.UInt:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt32(value));
                        break;

                    case Common.DataTypes.Long:
                        long lValue = 0;
                        if (long.TryParse(value.ToString(), out lValue))
                        {
                            long lValueNew = BpLong.SwapInt64(lValue);
                            rlt = plc.Write(plcdata.Address, lValueNew);
                        }
                        //plc.Write(plcdata.Address, Convert.ToInt64(value));
                        break;
                    case Common.DataTypes.ULong:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt64(value));
                        break;
                    case Common.DataTypes.Float:
                        rlt = plc.Write(plcdata.Address, float.Parse(value.ToString()));
                        break;
                    case Common.DataTypes.Double:
                        rlt = plc.Write(plcdata.Address, Convert.ToDouble(value));
                        break;
                    case Common.DataTypes.String:
                        if (value != null)
                        {
                            byte[] btValue = System.Text.Encoding.ASCII.GetBytes(value.ToString());
                            byte[] arrData = new byte[btValue.Length + 2];
                            arrData[0] = 50;
                            arrData[1] = (byte)btValue.Length;
                            btValue.CopyTo(arrData, 2);
                            rlt = plc.Write(plcdata.Address, arrData);
                        }
                        break;
                    default: break;

                }

            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }
            return rlt.IsSuccess;
        }

        #endregion

        #region SiemensPlc读写操作

        //PLC读取
        private object ReadSieTcpValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, SiemensS7Net plc)
        {
            try
            {
                if (plctype == null || !plctype.IsConnected || plcdata == null || plc == null) return null;

                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Bool
                        plcdata.ValueNew = plc.ReadBool(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.Byte://Byte
                        plcdata.ValueNew = plc.ReadByte(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.Short:
                        plcdata.ValueNew = plc.ReadInt16(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.Ushort:
                        plcdata.ValueNew = plc.ReadUInt16(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.Int:
                        plcdata.ValueNew = plc.ReadInt32(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.UInt:
                        plcdata.ValueNew = plc.ReadUInt32(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.Long:
                        long lValueNew = 0;
                        if (long.TryParse(plc.ReadInt64(plcdata.Address).Content.ToString(), out lValueNew))
                        {
                            long temp = BpLong.SwapInt64(lValueNew);
                            plcdata.ValueNew = temp;
                        }
                        // plcdata.ValueNew = plc.ReadInt64(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.ULong:
                        plcdata.ValueNew = plc.ReadUInt64(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.Float:
                        plcdata.ValueNew = plc.ReadFloat(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.Double:
                        plcdata.ValueNew = plc.ReadDouble(plcdata.Address).Content;
                        break;
                    case Common.DataTypes.String:
                        HslCommunication.OperateResult<byte[]> data = (HslCommunication.OperateResult<byte[]>)plc.Read(plcdata.Address, 50);
                        if (data != null && data.Content != null && data.Content.Length > 2)
                        {
                            List<byte> lstData = new List<byte>();
                            int nLen = data.Content[1];
                            for (int i = 2; i < nLen + 2; i++)
                            {
                                lstData.Add(data.Content[i]);
                            }
                            plcdata.ValueNew = System.Text.Encoding.ASCII.GetString(lstData.ToArray());
                        }
                        break;
                    default:
                        break;
                }
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }

            return plcdata.ValueNew;

        }

        //PLC写入
        private bool  WriteSieTcpValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, SiemensS7Net plc, object value)
        {
            HslCommunication.OperateResult rlt = new HslCommunication.OperateResult();
            rlt.IsSuccess = false;
            try
            {
                //string[] strAdrss = plcdata.Address.Split('.');
                //string Address = strAdrss[0] + "." + Regex.Replace(strAdrss[1], @"^[A-Za-z]+", string.Empty);

                if (plctype == null || !plctype.IsConnected || plcdata == null || plc == null) return rlt.IsSuccess;
                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Bool
                        rlt= plc.Write(plcdata.Address, Convert.ToBoolean(value));
                        break;

                    case Common.DataTypes.Byte://Byte
                        rlt = plc.Write(plcdata.Address, Convert.ToChar(value));
                        break;

                    case Common.DataTypes.Short:
                        rlt = plc.Write(plcdata.Address, Convert.ToInt16(value));
                        break;

                    case Common.DataTypes.Ushort:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt16(value));
                        break;

                    case Common.DataTypes.Int:
                        rlt = plc.Write(plcdata.Address, Convert.ToInt32(value));
                        break;

                    case Common.DataTypes.UInt:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt32(value));
                        break;

                    case Common.DataTypes.Long:
                        long lValue = 0;
                        if (long.TryParse(value.ToString(), out lValue))
                        {
                            long lValueNew = BpLong.SwapInt64(lValue);
                            rlt = plc.Write(plcdata.Address, lValueNew);
                        }
                        //plc.Write(plcdata.Address, Convert.ToInt64(value));
                        break;
                    case Common.DataTypes.ULong:
                        rlt = plc.Write(plcdata.Address, Convert.ToUInt64(value));
                        break;
                    case Common.DataTypes.Float:
                        rlt = plc.Write(plcdata.Address, float.Parse(value.ToString()));
                        break;
                    case Common.DataTypes.Double:
                        rlt = plc.Write(plcdata.Address, Convert.ToDouble(value));
                        break;
                    case Common.DataTypes.String:
                        if (value != null)
                        {
                            byte[] btValue = System.Text.Encoding.ASCII.GetBytes(value.ToString());
                            byte[] arrData = new byte[btValue.Length + 2];
                            arrData[0] = 50;
                            arrData[1] = (byte)btValue.Length;
                            btValue.CopyTo(arrData, 2);
                            rlt = plc.Write(plcdata.Address, arrData);
                        }
                        break;
                    default: break;

                }
               
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
            return rlt.IsSuccess;
        }

        #endregion


        #region MudbusTCP 读写操作
        //modbusTcp read
        private object ReadModTcpValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, ModbusTcpNet mod)
        {
            try
            {
                //string[] strAdrss = plcdata.Address.Split('.');
                //string Address = strAdrss[0] + "." + Regex.Replace(strAdrss[1], @"^[A-Za-z]+", string.Empty);

                if (plctype == null || !plctype.IsConnected || plcdata == null || mod == null) return null;

                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Coil
                        plcdata.ValueNew = mod.ReadCoil(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Discrete://Discrete
                        plcdata.ValueNew = mod.ReadDiscrete(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Short:
                        plcdata.ValueNew = mod.ReadInt16(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Ushort:
                        plcdata.ValueNew = mod.ReadUInt16(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Int:
                        plcdata.ValueNew = mod.ReadInt32(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.UInt:
                        plcdata.ValueNew = mod.ReadUInt32(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Long:
                        plcdata.ValueNew = mod.ReadInt64(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.ULong:
                        plcdata.ValueNew = mod.ReadUInt64(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Float:
                        plcdata.ValueNew = mod.ReadFloat(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Double:
                        plcdata.ValueNew = mod.ReadDouble(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.String:
                        plcdata.ValueNew = mod.ReadString(plcdata.Address, 10).Content;
                        break;

                    default:
                        break;
                }
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }
            return plcdata.ValueNew;
        }

        //modbusTcp write
        private bool  WriteModTcpValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, ModbusTcpNet mod, object value)
        {
            HslCommunication.OperateResult rlt = new HslCommunication.OperateResult();
            rlt.IsSuccess = false;
            try
            {
                //string[] strAdrss = plcdata.Address.Split('.');
                //string Address = strAdrss[0] + "." + Regex.Replace(strAdrss[1], @"^[A-Za-z]+", string.Empty);

                if (plctype == null || !plctype.IsConnected || plcdata == null || mod == null) return rlt.IsSuccess;

                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Bool
                        rlt= mod.WriteCoil(plcdata.Address, Convert.ToBoolean(value));
                        break;

                    case Common.DataTypes.Short:
                        rlt = mod.Write(plcdata.Address, Convert.ToInt16(value));
                        break;

                    case Common.DataTypes.Ushort:
                        rlt = mod.Write(plcdata.Address, Convert.ToUInt16(value));
                        break;

                    case Common.DataTypes.Int:
                        rlt = mod.Write(plcdata.Address, Convert.ToInt32(value));
                        break;

                    case Common.DataTypes.UInt:
                        rlt = mod.Write(plcdata.Address, Convert.ToUInt32(value));
                        break;

                    case Common.DataTypes.Long:
                        rlt = mod.Write(plcdata.Address, Convert.ToInt64(value));
                        break;

                    case Common.DataTypes.ULong:
                        rlt = mod.Write(plcdata.Address, Convert.ToUInt64(value));
                        break;

                    case Common.DataTypes.Float:
                        rlt = mod.Write(plcdata.Address, float.Parse(value.ToString()));
                        break;

                    case Common.DataTypes.Double:
                        rlt = mod.Write(plcdata.Address, Convert.ToDouble(value));
                        break;

                    case Common.DataTypes.String:
                        rlt = mod.Write(plcdata.Address, Convert.ToString(value));
                        break;

                    default: break;

                }
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }

            return rlt.IsSuccess;
        }
        #endregion


        #region MudbusRTU 读写操作
        //modbusRtu read
        private object ReadModRtuValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, ModbusRtu mod)
        {
            try
            {
                //string[] strAdrss = plcdata.Address.Split('.');
                //string Address = strAdrss[0] + "." + Regex.Replace(strAdrss[1], @"^[A-Za-z]+", string.Empty);

                if (plctype == null || !plctype.IsConnected || plcdata == null || mod == null) return null;

                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Coil
                        plcdata.ValueNew = mod.ReadCoil(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Discrete://Discrete
                        plcdata.ValueNew = mod.ReadDiscrete(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Short:
                        plcdata.ValueNew = mod.ReadInt16(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Ushort:
                        plcdata.ValueNew = mod.ReadUInt16(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Int:
                        plcdata.ValueNew = mod.ReadInt32(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.UInt:
                        plcdata.ValueNew = mod.ReadUInt32(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Long:
                        plcdata.ValueNew = mod.ReadInt64(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.ULong:
                        plcdata.ValueNew = mod.ReadUInt64(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Float:
                        plcdata.ValueNew = mod.ReadFloat(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.Double:
                        plcdata.ValueNew = mod.ReadDouble(plcdata.Address).Content;
                        break;

                    case Common.DataTypes.String:
                        plcdata.ValueNew = mod.ReadString(plcdata.Address, 10).Content;
                        break;

                    default:
                        break;
                }

            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }
            return plcdata.ValueNew;
        }

        //modbusRtu write
        private bool  WriteModRtuValue(Config.PlcTypeItem plctype, Config.PlcDataItem plcdata, ModbusRtu mod, object value)
        {
            HslCommunication.OperateResult rlt = new HslCommunication.OperateResult();
            rlt.IsSuccess = false;
            try
            {
                //string[] strAdrss = plcdata.Address.Split('.');
                //string Address = strAdrss[0] + "." + Regex.Replace(strAdrss[1], @"^[A-Za-z]+", string.Empty);

                if (plctype == null || !plctype.IsConnected || plcdata == null || mod == null) return rlt.IsSuccess;

                switch (plcdata.DataType)
                {
                    case Common.DataTypes.Bool://Bool
                        rlt = mod.WriteCoil(plcdata.Address, Convert.ToBoolean(value));
                        break;

                    case Common.DataTypes.Short:
                        rlt = mod.Write(plcdata.Address, Convert.ToInt16(value));
                        break;

                    case Common.DataTypes.Ushort:
                        rlt = mod.Write(plcdata.Address, Convert.ToUInt16(value));
                        break;

                    case Common.DataTypes.Int:
                        rlt = mod.Write(plcdata.Address, Convert.ToInt32(value));
                        break;

                    case Common.DataTypes.UInt:
                        rlt = mod.Write(plcdata.Address, Convert.ToUInt32(value));
                        break;

                    case Common.DataTypes.Long:
                        rlt = mod.Write(plcdata.Address, Convert.ToInt64(value));
                        break;

                    case Common.DataTypes.ULong:
                        rlt = mod.Write(plcdata.Address, Convert.ToUInt64(value));
                        break;

                    case Common.DataTypes.Float:
                        rlt = mod.Write(plcdata.Address, float.Parse(value.ToString()));
                        break;

                    case Common.DataTypes.Double:
                        rlt = mod.Write(plcdata.Address, Convert.ToDouble(value));
                        break;

                    case Common.DataTypes.String:
                        rlt = mod.Write(plcdata.Address, Convert.ToString(value));
                        break;

                    default: break;

                }

            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }
            return rlt.IsSuccess;

        }
        #endregion


        #region 设备项获取
        /// 获取PLC数据项
        public Config.PlcDataItem GetPlcDataItem(Common.PlcDevice device)
        {
            //string[] sArry = device.ToString().Split('_');
            //Test.Config.PlcTypeItem item = this.GetPlcTypeItem(sArry[1]);

            IEnumerable ie = from lst in this.config.PlcDataItems
                             where lst.PlcDevice == device
                             select lst;
            List<Config.PlcDataItem> ioLst = ie.Cast<Config.PlcDataItem>().ToList();
            if (ioLst.Count > 0)
                return ioLst[0];
            else
                return null;
        }

        // 获取PLC数据项
        public Config.PlcDataItem GetPlcDataItem(string itemName)
        {
            //string[] sArry = device.ToString().Split('_');
            //Test.Config.PlcTypeItem item = this.GetPlcTypeItem(sArry[1]);

            IEnumerable ie = from lst in this.config.PlcDataItems
                             where lst.ItemName == itemName
                             select lst;
            List<Config.PlcDataItem> ioLst = ie.Cast<Config.PlcDataItem>().ToList();
            if (ioLst.Count > 0)
                return ioLst[0];
            else
                return null;
        }

        ///  获取PLC设备
        public Config.PlcTypeItem GetPlcTypeItem(string devName)
        {
            IEnumerable ie = from lst in this.config.PlcTypeItems
                             where lst.DevName == devName
                             select lst;
            List<Config.PlcTypeItem> ioLst = ie.Cast<Config.PlcTypeItem>().ToList();
            if (ioLst.Count > 0)
                return ioLst[0];
            else
                return null;
        }

        /// SiemensPLC客户端
        public SiemensS7Net GetPlcClient(Common.PlcDevice device)
        {
            string[] sArry = device.ToString().Split('_');
            string name = sArry[1];

            IEnumerable ie = from lst in this.config.PlcTypeItems
                             where lst.DevName == name
                             select lst.Tag;
            List<SiemensS7Net> ioLst = ie.Cast<SiemensS7Net>().ToList();
            if (ioLst.Count > 0)
                return ioLst[0];
            else
                return null;
        }
        #endregion


        /// <summary>
        /// PLC值改变回调
        /// </summary>
        /// <param name="item"></param>
        public void PlcValueChange(Config.PlcDataItem item)
        {
            //try
            //{
            //    business business = SystemHandle.Instance.GetMachineKylin(item.MachineID);
            //    if (business != null)
            //        business.PlcValueChange(item);
            //}
            //catch (Exception ex)
            //{
            //    Log.LogWriter.WriteException(ex);
            //}
            if (PlcValueChangedEvent == null) return;
            Regex regex = new Regex(@"V_[0-9]{2}_TriggerGrab");
            if (!regex.IsMatch(item.PlcDevice.ToString())) return;
            // if (regex.IsMatch(item.PlcDevice.ToString())&&(bool)item.ValueNew ==true)
            if (item.DataType == Common.DataTypes.Short)
            {
                short value = (short)item.ValueNew;
                bool IsOk = true;
                if (value < 1) IsOk = false;
                else IsOk = true;
                if (IsOk)
                    PlcValueChangedEvent(item, new object());
            }

            if (item.DataType == Common.DataTypes.Bool)
            {
                bool value = (bool)item.ValueNew;
                if (value) PlcValueChangedEvent(item, new object());
                       
            }

        }

    }
}
