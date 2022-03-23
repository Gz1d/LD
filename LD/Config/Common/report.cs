using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.Config
{
    [Serializable]
    public class report
    {

        public report()
        {
            this.Time = DateTime.Now.ToString("yy-MM-dd-HH-mm");
        }


        public string Time
        {
            set;
            get;
        }
        //产品编号
        public string PosNumObj 
        {
            get;
            set; 
        
        }
        //工位编号
        public string LinkNumObj
        {
            get;
            set;

        }
        //最大偏移量
        public double MaxDists
        {
            set;
            get;

        }

        public string VisionID
        {
            set;
            get;
        }

        public string Class
        {
            set;
            get;
        }

        public int  NumberOK
        {
            set;
            get;
        }

        public int NumberNG
        {
            set;
            get;
        }

        public int  Total
        {
            set;
            get;
        }

        public double PerOK
        {
            set;
            get;

        }

/// <summary>
/// 划痕数量
/// </summary>
       public int scratch
       {
            set;
            get;
        }
        public void Save()
        {
            try
            {
                Serializition.SaveToFile(this, string.Format(@"config\{0}", "Report.config"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
