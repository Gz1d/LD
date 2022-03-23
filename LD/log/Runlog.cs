using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LD.Log
{
      [Serializable]
    public class Runlog
    {

        #region 单例.....
        private static object syncObj = new object();
        private static Runlog _instance;
        public static Runlog Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Runlog();
                        }
                    }
                }

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Runlog()
        {
            this.Time = DateTime.Now;
        }

        public Runlog(string notes)
        {
            this.Time = DateTime.Now;
            this.Notes = notes;
        }
        #endregion


        /// <summary>
        /// 设备号
        /// </summary>
        public string MachineID
        {
            set;
            get;
        }


        public string Device
        {
            set;
            get;
        }



        public DateTime Time
        {
            set;
            get;
        }

        public string Notes
        {
            set;
            get;
        }

        public void Add(string device, string note)
        {
            Add("SYS", device, note);
        }

        public void Add(string machine, string device, string notes)
        {
            if (string.IsNullOrEmpty(Notes))
            {
                Logic.LogHandle.Instance.WriteRunLog(machine, device, notes);
            }
            else
            {
                Logic.LogHandle.Instance.WriteRunLog(machine, device, Notes);
            }
        }
    }
}
