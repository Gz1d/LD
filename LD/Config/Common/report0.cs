using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.Config
{
    [Serializable]
    public class report0
    {

        public report0()
        {
            this.Time = DateTime.Now.ToString("yy-MM-dd-HH-mm");
        }


        public string Time
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

}
}
