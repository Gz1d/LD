using System;
using System.ComponentModel;



namespace LD.Config
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    [Serializable]
    public class ConfigLog : Configuration
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigLog()
        {
            this.RunLogs = new BindingList<Log.Runlog>();
            this.VisionLogs = new BindingList<VisionLog>();
            this.LogRunCount = 1000;
            SelectLanguage = false;
        }

        private static string ConfigName = string.Format(@"config\{0}", "Log.config");


        /// <summary>
        /// Mes访问日志
        /// </summary>
        public bool WriteLogMesUrl
        {
            set;
            get;
        }

        /// <summary>
        /// 测试项上载日志
        /// </summary>
        public bool WriteLogMesTest
        {
            set;
            get;
        }

        public int LogRunCount
        {
            set;
            get;
        }

        public bool WriteLogRun
        {
            set;
            get;
        }

        public bool WriteLogVision
        {
            set;
            get;
        }

        public bool SelectLanguage
        {
            set;
            get;
        }
        /// <summary>
        ///  运行日志
        /// </summary>
        public BindingList<Log.Runlog> RunLogs
        {
            set;
            get;
        }


        /// <summary>
        /// 视觉日志
        /// </summary>
        public BindingList<Config.VisionLog> VisionLogs
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
        public static ConfigLog Load()
        {
            try
            {
                ConfigLog obj = (ConfigLog)Serializition.LoadFromFile(typeof(ConfigLog), ConfigName);
                return obj;
            }
            catch (Exception ex)
            {
                throw new LoadException(ConfigName, ex.Message);
            }
        }
    }
}
