using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LD.Config
{
    public class ConfigSocketS : Configuration
    {
        private static string ConfigName = string.Format(@"config\{0}", "SocketS.config");

        public ConfigSocketS()
        {
            this.SocketSItems = new BindingList<SocketSItem>();
        }


        /// <summary>
        /// socket列表
        /// </summary>
        public BindingList<Config.SocketSItem> SocketSItems
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
        public static ConfigSocketS Load()
        {
            try
            {
                ConfigSocketS obj = (ConfigSocketS)Serializition.LoadFromFile(typeof(ConfigSocketS), ConfigName);
                return obj;
            }
            catch (Exception ex)
            {
                throw new LoadException(ConfigName, ex.Message);
            }
        }
    }
}
