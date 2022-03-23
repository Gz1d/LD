using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace LD.Config
{
    /// <summary>
    /// PCB上板工站配置
    /// </summary>
    [Serializable]
    public class Configbusiness01 : Configbusiness
    {
        private static string ConfigName = string.Format(@"config\{0}", "Configbusiness01.config");

        public Configbusiness01()
        {
            this.MachineID = "01";
            this.StationID = "01";
            this.Caption = "PCB上板机台";
            this.Descrip = "PCB上板";
        }

        private bool mTestLamp;
        public bool TestLamp
        {
            get
            {
                return this.mTestLamp;
            }
            set
            {
                if (this.mTestLamp != value)
                {
                    this.mTestLamp = value;
                    this.NotifyPropertyChanged("TestLamp");
                }
            }
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
        public static Configbusiness01 Load()
        {
            try
            {
                Configbusiness01 obj = (Configbusiness01)Serializition.LoadFromFile(typeof(Configbusiness01), ConfigName);
                return obj;
            }
            catch (Exception ex)
            {
                throw new LoadException(ConfigName, ex.Message);
            }
        }
    }

}
