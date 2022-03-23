using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LD.Config
{
    public class ConfigSocketC : Configuration
    {
        private static string ConfigName = string.Format(@"config\{0}", "SocketC.config");

        public ConfigSocketC()
        {
            this.SocketCItems = new BindingList<SocketCItem>();
        }

        /// <summary>
        /// socket列表
        /// </summary>
        public BindingList<Config.SocketCItem> SocketCItems
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
        public static ConfigSocketC Load()
        {
            try
            {
                ConfigSocketC obj = (ConfigSocketC)Serializition.LoadFromFile(typeof(ConfigSocketC), ConfigName);
                return obj;
            }
            catch (Exception ex)
            {
                throw new LoadException(ConfigName, ex.Message);
            }
        }
    }
}
