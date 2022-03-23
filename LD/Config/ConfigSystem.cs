using System;
using System.ComponentModel;



namespace LD.Config
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    [Serializable]
    public class ConfigSystem : Configuration
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigSystem()
        {
            this.SystemName = "联得智能控制系统";
            
        }

       private static string ConfigName =string .Format (@"config\{0}", "System.config");

        /// <summary>
        /// 系统名称
        /// </summary>
       public string SystemName
       {
           set;
           get;
       }

       /// <summary>
       /// 用户
       /// </summary>
       public string[] Admin
       {
           set;
           get;
       }

       public string[] Pass
       {
           set;
           get;
       }

        public int  State
        {
            set;
            get;
        }
        private bool mIslongin;

      [System.Xml.Serialization.XmlIgnore]
       public bool IsLogin
       {
           set
           {
               if (this.mIslongin != value)
               {
                   this.mIslongin = value;
                   this.NotifyPropertyChanged("IsLogin");
               }
           }
           get
           {
               return this.mIslongin;
           }
       }

       /// <summary>
       /// 刷新心跳间隔，秒
       /// </summary>
       public int UpdateHeart
       {
           set;
           get;
       }

       /// <summary>
       /// 只显示激活机台
       /// </summary>
       public bool ShowActive
       {
           set;
           get;
       }

        /// <summary>
        /// 系统报警
        /// </summary>
       public bool IsAlarm
       {
           set;
           get;
       }


       private int mProductCount = 0;
       /// <summary>
       /// 生产数量
       /// </summary>
       public int ProductCount
       {
           set
           {
               if (this.mProductCount != value)
               {
                   this.mProductCount = value;
                   this.NotifyPropertyChanged("ProductCount");
               }
           }
           get
           {
               return this.mProductCount;
           }
       }

       /// <summary>
       /// 开始线程
       /// </summary>
       [System.Xml.Serialization.XmlIgnore]
       public bool StartThread
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
        public static ConfigSystem Load()
        {
            try
            {
                ConfigSystem obj = (ConfigSystem)Serializition.LoadFromFile(typeof(ConfigSystem), ConfigName);
                return obj;
            }
            catch (Exception ex)
            {
                throw new LoadException(ConfigName, ex.Message);
            }
        }
    }
}
