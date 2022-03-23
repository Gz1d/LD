using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace LD.Config
{
    /// <summary>
    /// PLC配置类
    /// </summary>
    [Serializable]
    public class ConfigSerial : Configuration
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigSerial()
        {
            this.SerialItems = new BindingList<SerialItem>();
        }

        private static string ConfigName = string.Format(@"config\{0}", "Serial.config");


        /// <summary>
        /// Serial列表
        /// </summary>
        public BindingList<Config.SerialItem> SerialItems
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
        public static ConfigSerial Load()
        {
            try
            {
                ConfigSerial obj = (ConfigSerial)Serializition.LoadFromFile(typeof(ConfigSerial), ConfigName);
                return obj;
            }
            catch (Exception ex)
            {
                throw new LoadException(ConfigName, ex.Message);
            }
        }
    }
}
