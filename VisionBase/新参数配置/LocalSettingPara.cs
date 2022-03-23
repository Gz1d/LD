using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionBase
{
   public  class LocalSettingPara : Configuration
    {
        /// <summary> 定位模式 </summary>
        public  LocalModelEnum localModel = LocalModelEnum.Temp;
        public int SearchAreaX =0;
        public int SearchAreaY=0;
        public int SearchWidth=1000;
        public int SearchHeight=1000;
        /// <summary> 拍照位置的示教 ，用于计算示教时产品的坐标</summary>
        public Point3Db GrabPosTeach;
        /// <summary> 示教图片的像素坐标定位值</summary>
        public St_VectorAngle TeachImgLocal;
        public CoordiEmum TeachCoordi;
        /// <summary> 用来获取对应的手眼标定参数 </summary>
        public CoordiCamHandEyeMatEnum CoordiCam;

        public double Offset_x_range;
        public double Offset_y_range;
        public double Offset_theta_range;
        /// <summary> 偏移补偿量 </summary>
        public double Offset_x;
        public double Offset_y;
        public double Offset_theta;
        public double PixelSize;
        private static string ConfigName = string.Format(@"config\{0}", "LocalSetting");

        public LocalSettingPara()
        {
            SearchAreaX = 0;
            SearchAreaY = 0;
            SearchWidth = 1000;
            SearchHeight = 1000;
            localModel = LocalModelEnum.Temp;
          
            GrabPosTeach = new Point3Db();
            TeachImgLocal = new St_VectorAngle();
            TeachCoordi = CoordiEmum.Coordi0;
            CoordiCam = CoordiCamHandEyeMatEnum.Coordi0Cam0;

           Offset_x_range = 1;
            Offset_y_range = 1;
            Offset_theta_range = 1;
            Offset_x = 0;
            Offset_y = 0;
            Offset_theta = 0;
            PixelSize = 1;
        }
    }


    public enum LocalModelEnum
    { 
        /// <summary> 模板匹配定位 </summary>
        Temp =0,
        /// <summary> 两条直线定位 </summary>
        TwoLine,
        /// <summary> 三条直线定位 </summary>
        ThreeLine,
        /// <summary> 四条直线定位 </summary>
        FourLine,
        /// <summary> 单圆定位</summary>
        OneCircle,
        /// <summary> 双圆定位 </summary>
        TwoCircle,
        /// <summary> 直线圆定位 </summary>
        LineCircle,
        /// <summary>Blob定位 </summary>
        Blob,
        BlobTwoLine,
        /// <summary> 模板匹配定位，再Blob定位 </summary>
        TempBlob,
        TempTwoLine,
        TempThreeLine,
        TempFourLine,
        TempOneCircle,
        TempTwoCircle,
        TempLineCircle,
        TempBlobInspect,
        TempLinCirRectInsp,
        TwoLineLocalLinCirRectInsp,
        TempTwoLineLocalLinCirRectInsp,
        TempTwoCircleLocalLinCirRectInsp,
        TempLineCircleLocalLinCirRectInsp,
        BlobLinCirRectInsp,
    }







}
