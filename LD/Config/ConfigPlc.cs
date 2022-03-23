using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;


namespace LD.Config
{
    /// <summary>
    /// PLC配置类
    /// </summary>
    [Serializable]
    public class ConfigPlc : Configuration
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigPlc()
        {
            this.bReadThread = true;
        }

        private static string ConfigName = string.Format(@"config\{0}", "Plc.config");

        /// <summary>
        /// 实时读取PLC线程
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool bReadThread
        {
            set;
            get;
        }

        /// <summary>
        /// 显示全部数据
        /// </summary>
        public bool ShowAllItem
        {
            set;
            get;
        }

        /// <summary>
        /// plc列表
        /// </summary>
        public BindingList<Config.PlcTypeItem> PlcTypeItems
        {
            set;
            get;
        }

        /// <summary>
        /// Plc数据项集
        /// </summary>
        public BindingList<PlcDataItem> PlcDataItems
        {
            set;
            get;
        }



        /// <summary>
        /// 保存设置
        /// </summary>
        public void Save()
        {
            try
            {
                Serializition.SaveToFile(this, ConfigName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <returns></returns>
        public static ConfigPlc Load()
        {
            try
            {
                ConfigPlc obj = (ConfigPlc)Serializition.LoadFromFile(typeof(ConfigPlc), ConfigName);
                return obj;
            }
            catch (Exception ex)
            {
                throw new LoadException(ConfigName, ex.Message);
            }
        }
    }
}
