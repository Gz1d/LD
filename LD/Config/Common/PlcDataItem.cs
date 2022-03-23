using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel;


namespace LD.Config
{

    //数据变化委托
    public delegate void PlcValueChangeEventHandler(PlcDataItem plc, object value);


    /// <summary>
    /// PLC设备数据类
    /// </summary>
    [Serializable]
    public class PlcDataItem : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(info));
            }
        }

        //构造函数
        public PlcDataItem()
        {
            this.MachineID = "01";
            this.StationID = "";
            this.ItemName = "";
            this.IsActive = true;
            this.PlcDevice = Common.PlcDevice.NULL;
            this.DataType = Common.DataTypes.Bool;
            this.ValueNew = 0;
            //this.mValue = 0;
            Length = 0;
        }

        /// <summary>
        /// 设备号
        /// </summary>
        public string MachineID
        {
            set;
            get;
        }

        /// <summary>
        /// 站号
        /// </summary>
        public string StationID
        {
            set;
            get;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            set;
            get;
        }

        /// <summary>
        /// 项名称
        /// </summary>
        public string ItemName
        {
            set;
            get;
        }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            set;
            get;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public Common.DataTypes DataType
        {
            set;
            get;
        }



        /// <summary>
        /// 设备类型
        /// </summary>
        public Common.PlcDevice PlcDevice
        {
            set;
            get;
        }
        private object mValue;
        /// <summary>
        /// 值
        /// </summary>
        public object ValueNew
        {
            set
            {
                if (this.mValue != null)
                {
                    if (!this.mValue.Equals(value))
                    {
                        this.ValueOld = this.mValue;
                        this.mValue = value;
                        NotifyPropertyChanged("ValueNew");
                    }
                }
                else
                {
                    if (value != null)
                    {
                        this.ValueOld = this.mValue;
                        this.mValue = value;
                        NotifyPropertyChanged("ValueNew");
                    }
                }
            }
            get
            {
                return mValue;
            }
        }

        public object ValueOld
        {
            set;
            get;
        }

        public object ValueWrite
        {
            set;
            get;
        }

        /// <summary>
        /// 实时更新
        /// </summary>
        public bool IsActive
        {
            set;
            get;
        }

        //描述
        public string Descrip
        {
            set;
            get;
        }

        /// <summary>
        /// 字串长度
        /// </summary>
        public ushort Length
        { set; get; }

    }
}
