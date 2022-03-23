using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.Config
{
    /// <summary>
    /// ConfigurationManager 的摘要说明。
    /// 配置管理类
    /// </summary>
    public class ConfigManager
    {
        #region 单例.....
        private static object syncObj = new object();
        private static ConfigManager _instance;
        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private ConfigManager()
        {

        }
        #endregion

        /// <summary>
        /// 系统配置
        /// </summary>
        public ConfigSystem ConfigSystem
        {
            set;
            get;
        }
        /// <summary>
        /// 特征参数
        /// </summary>
        public Configbusiness01 Configbusiness01
        {
            set;
            get;
        }

        /// <summary>
        /// Report  配置
        /// </summary>
        public ConfigReport ConfigReport
        {
            set;
            get;
        }
        public report report
        {
            set;
            get;
        }


        /// <summary>
        /// SOCKET  配置
        /// </summary>
        public ConfigSocketC ConfigSocketC
        {
            set;
            get;
        }

        /// <summary>
        /// SOCKET  配置
        /// </summary>
        public ConfigSocketS ConfigSocketS
        {
            set;
            get;
        }

        /// <summary>
        /// Serial  配置
        /// </summary>
        public ConfigSerial ConfigSerial
        {
            set;
            get;
        }


        /// <summary>
        /// 相机
        /// </summary>
        //public ConfigCamera ConfigCamera
        //{
        //    set;
        //    get;
        //}

        /// <summary>
        /// 视觉配置
        /// </summary>
        //public ConfigVision ConfigVision
        //{
        //    set;
        //    get;
        //}

        /// <summary>
        /// 日志
        /// </summary>
        public ConfigLog ConfigLog
        {
            set;
            get;
        }

  
        /// <summary>
        /// PLC配置
        /// </summary>
        public ConfigPlc ConfigPlc
        {
            set;
            get;
        }



        /// <summary>
        /// 加载所有配置
        /// </summary>
        public void Load()
        {
            try
            {
                //日志
                this.ConfigLog = ConfigLog.Load();
                //系统参数
                this.ConfigSystem = ConfigSystem.Load();
                //生产报表
                this.ConfigReport = ConfigReport.Load();
                //网络客户端
                this.ConfigSocketC = ConfigSocketC.Load();
                this.ConfigSocketS = ConfigSocketS.Load();
                //串口
                this.ConfigSerial = ConfigSerial.Load();;
                //PLC
                this.ConfigPlc = ConfigPlc.Load();

                //相机
                //this.ConfigCamera = ConfigCamera.Load();
                //视觉
                //this.ConfigVision = ConfigVision.Load();

                this.Configbusiness01 = Configbusiness01.Load();

            }
            catch (LoadException ex)
            {
                //写日志
                Log.LogWriter.WriteLog(ex.ToString());
                Log.LogWriter.WriteException(ex);
            }
        }




        /// <summary>
        /// 保存所有配置
        /// </summary>
        public void Save()
        {
            try
            {
                this.ConfigSystem.Save();
                this.ConfigReport.Save();
                this.ConfigSocketC.Save();
                this.ConfigSocketS.Save();
                this.ConfigSerial.Save();
                this.ConfigPlc.Save();
                //this.ConfigCamera.Save();
                //this.ConfigVision.Save();
                this.ConfigLog.Save();
                this.Configbusiness01.Save();

            }
            catch (LoadException ex)
            {
                //写日志
                Log.LogWriter.WriteLog(ex.ToString());
                Log.LogWriter.WriteException(ex);
            }
        }
    }
}
