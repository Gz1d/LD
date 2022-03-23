using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.Config.Common
{
    class ConfigReport : Configuration
    {

        [Serializable]
        public string PathReport
        {
            set;
            get;
        }


        /// <summary>
        /// 报表列表
        /// </summary>
        public BindingList<Config.Common.ConfigReport> PlcTypeItems
        {
            set;
            get;
        }



    }
}
