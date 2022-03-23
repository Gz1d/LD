using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionBase
{
   public  class LightCrlParaItem
   {
        public LightCrlParaItem()
        {
            this.PortName = ComEnum.COM0;
            this.BaudRate = 115200;
            this.StopBits = System.IO.Ports.StopBits.One;
            this.DataBits = 8;
            this.Parity = System.IO.Ports.Parity.None;
            this.IsConnect = false;
            this.IsActive = false;
            this.IsHex = true;
            this.LightCtrlType = LightCtrlTypeEnum.WordP;
            this.LightCtrlName = LightCtrlEmun.LightCtrl0;
            this.Describe = "";
        }
        /// <summary> 串口  </summary>
        public ComEnum PortName { set; get; }
        /// <summary>  波特率  </summary>
        public int BaudRate { set; get; }
        /// <summary> 停止位   </summary>
        public System.IO.Ports.StopBits StopBits { set; get; }
        /// <summary> 校验 </summary>
        public System.IO.Ports.Parity Parity { set; get; }
        /// <summary> 数据位  </summary>
        public int DataBits { set; get; }
        /// <summary> 是否连接成功  </summary>
        public bool IsConnect { set; get; }
        /// <summary> 是否激活 </summary>
        public bool IsActive { set; get; }
        /// <summary> 光源控制器描述  </summary>
        public string Describe { set; get; }
        /// <summary> 是否进制  </summary>
        public bool IsHex { set; get; }
        /// <summary> 光源控制器类型  </summary>
        public LightCtrlTypeEnum LightCtrlType { set; get; }
        public LightCtrlEmun LightCtrlName { set; get; }
        /// <summary> 串口TAG  </summary>
        [System.Xml.Serialization.XmlIgnore]
        public object Tag { set; get; }

    }

}
