using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD.Config
{
    [Serializable]
    public class VisionLog
    {
        public VisionLog()
        {
            this.Time = DateTime.Now;
            this.LogID =this.Time.ToString("yyMMddHHmmssfff");
        }


        /// <summary>
        /// 编号
        /// </summary>
        public string LogID
        {
            set;
            get;
        }

        /// <summary>
        /// 机台
        /// </summary>
        public string MachineID
        {
            set;
            get;
        }

        /// <summary>
        /// 视觉设备
        /// </summary>
        public string Device
        {
            set;
            get;
        }


        /// <summary>
        /// 相机
        /// </summary>
        public string CameraID
        {
            set;
            get;
        }

        public string VisionID
        {
            set;
            get;
        }


        /// <summary>
        /// 处理图像
        /// </summary>
        public string ImageLast
        {
            set;
            get;
        }
        /// <summary>
        /// 处理图像
        /// </summary>
        public string ImageOK
        {
            set;
            get;
        }
        /// <summary>
        /// 处理图像
        /// </summary>
        public string ImageNG
        {
            set;
            get;
        }
        /// <summary>
        /// 处理图像
        /// </summary>
        public string Result
        {
            set;
            get;
        }

        /// <summary>
        /// 运动X
        /// </summary>
        public double X
        {
            set;
            get;
        }


        /// <summary>
        /// 运动Y
        /// </summary>
        public double Y
        {
            set;
            get;
        }


        /// <summary>
        /// 列
        /// </summary>
        public double Col
        {
            set;
            get;
        }

        public double Row
        {
            set;
            get;
        }

        public double Angle
        {
            set;
            get;
        }

        public bool? OK
        {
            set;
            get;
        }

        /// <summary>
        /// 取图时间
        /// </summary>
        public int MsGrap
        {
            set;
            get;
        }

        /// <summary>
        /// 处理时间
        /// </summary>
        public double  MsVision
        {
            set;
            get;

        }


        /// <summary>
        /// 处理时间
        /// </summary>
        public double MsGrab
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
    }
}
