using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileLib;
using System.Drawing;
using System.Windows.Forms;

namespace VisionBase
{
    public class CaliParam
    {
        public CameraLightPara camLightPara;
        [System.Xml.Serialization.XmlIgnore]
        public LocalPara localPara;
        private static string ConfigName = string.Format(@"\{0}", "CaliPara");
        /// <summary> 旋转中心的起始点坐标X</summary>
        public Point3Db StartRotPt;

        public List<RectangleF> ListRectRegion;
        public List<Point2Db> ListPt2D;
        /// <summary> 角度旋转范围 </summary>
        public double AngleRange { get;set; }
        /// <summary>  角度旋转次数 </summary>
        public double AngleStep {get;set; }
        /// <summary>标定起始点X   </summary> 
        public Point3Db StartCaliPt;
        /// <summary>标定终止点 </summary>
        public Point3Db EndCaliPt;
        /// <summary> 相机是否随着X轴移动</summary>
        public bool IsMoveX { get; set; }
        /// <summary> 相机是否随着Y轴移动 </summary>
        public bool IsMoveY { get; set; }
        /// <summary> 标定方式</summary>
        public CaliModelEnum caliModel { get; set; }
        /// <summary> 标定坐标系 </summary>
        public CoordiEmum coordi { get; set; }
        /// <summary> 标定参数的描述 </summary>
        public string describe { get; set; }
        /// <summary> 标定坐标系对应的相机 </summary>
        public CameraEnum cam { get; set; }
        /// <summary>获取手眼标定相关参数  </summary>
        public CoordiCamHandEyeMatEnum CoordiCam { get; set; }

        /// <summary> 标定矩阵 </summary>
        public MyHomMat2D HomMat;



        public CaliParam()
        {
            this.camLightPara = new CameraLightPara();
            this.localPara = new LocalPara();
            this.AngleRange = 10.0;
            this.StartCaliPt = new Point3Db();
            this.EndCaliPt = new Point3Db();
            this.IsMoveX = false;
            this.IsMoveY = false;
            this.caliModel = CaliModelEnum.HandEyeCali;
            this.HomMat = new MyHomMat2D();
            this.ListRectRegion = new List<RectangleF>();
            this.ListPt2D = new List<Point2Db>();
            this.cam = CameraEnum.Cam0;
            this.describe = "未定义";
            this.CoordiCam = CoordiCamHandEyeMatEnum.Coordi0Cam0;
        }
        /// <summary>
        /// 保存标定参数
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public bool Save(string Path)
        {
            bool IsOk = true;
            string path = Path + ConfigName + @"\";
            if (!FileLib.DirectoryEx.Exist(path)) FileLib.DirectoryEx.Create(path);
            //DirectoryEx.Delete(path);
            IsOk = IsOk && XML<CaliParam>.Write(this, path + "CaliPara.xml");            
            IsOk = IsOk && localPara.Save(path);
            return IsOk;
        }

        public static CaliParam Load(string Path)
        {
            string path = Path + ConfigName + @"\";
            try {
                CaliParam obj = XML<CaliParam>.Read(path + "CaliPara.xml");
                if (obj == null){
                    return obj = new CaliParam();
                }
                obj.localPara.Read(path);
                return obj;
            }
            catch(Exception ex) {
                throw new LoadException(path, ex.Message);                      
            }              
        }
       public CaliValue GetCaliValue() {
            CaliValue myCaliValue = new CaliValue();
            myCaliValue.StartCaliPt = this.StartCaliPt;
            myCaliValue.EndCaliPt = this.EndCaliPt;
            myCaliValue.IsMoveX = this.IsMoveX;
            myCaliValue.IsMoveY = this.IsMoveY;
            myCaliValue.caliModel = this.caliModel;
            myCaliValue.HomMat = this.HomMat;
            myCaliValue.Coordi = this.coordi;
            myCaliValue.cam = this.cam;
            myCaliValue.describe = this.describe;
            return myCaliValue;
        }
    }
    /// <summary> 标定模式枚举 </summary>
    public enum CaliModelEnum
    { 
        UpDnCamCali=0,
        HandEyeCali,
        Cali9PtCali,
    }
    public class CaliValue
    {
        public Point3Db StartCaliPt;
        /// <summary>标定终止点 </summary>
        public Point3Db EndCaliPt;
        /// <summary> 相机是否随着X轴移动</summary>
        public bool IsMoveX;
        /// <summary> 相机是否随着Y轴移动 </summary>
        public bool IsMoveY;
        /// <summary> 标定方式</summary>
        public CaliModelEnum caliModel;
        /// <summary> 标定矩阵 </summary>
        public MyHomMat2D HomMat;
        /// <summary> 对应的机械坐标系 </summary>
        public CoordiEmum Coordi;
        /// <summary> 对应的相机 </summary>
        public CameraEnum cam;
        public string describe;
    }

}
