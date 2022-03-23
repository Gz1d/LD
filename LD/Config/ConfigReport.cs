using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.Config
{
    public  class ConfigReport : Configuration
    {
        private static string ConfigName = string.Format(@"config\{0}", "Report.config");
        public int ClassNumberMax
        {
            set;
            get;
        }

        public string Class
        {
            set;
            get;
        }

        public int AcquTime
        {
            set;
            get;
        }
        /// <summary>
        /// 报表列表
        /// </summary>
        public BindingList<Config.report> ReportItems
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
        public static ConfigReport Load()
        {
            try
            {
                ConfigReport obj = (ConfigReport)Serializition.LoadFromFile(typeof(ConfigReport), ConfigName);
                return obj;
            }
            catch (Exception ex)
            {
                throw new LoadException(ConfigName, ex.Message);
            }
        }

    }
}
