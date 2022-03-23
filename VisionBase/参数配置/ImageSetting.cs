using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionBase
{
    public struct St_ImageSetting
    {
        /// <summary> 是不是找模板</summary>
        public bool IsFindTemplate;
        /// <summary> 是不是找线</summary>
        public bool IsFindLine;
        /// <summary> 是不是找圆 </summary>
        public bool IsFindCircle;
        /// <summary> 是不是Pin针检测 </summary>
        public bool IsPinInspect;
        /// <summary>是不是偏移检测 </summary>
        public bool IsOffSetDetect;
        /// <summary> 是不是Blob分析，胶量检测 </summary>
        public bool IsBlobInspect;

        
        public string CcdName;
        public string FolderName;
        public string CamColorMode;
        public int SearchAreaX;
        public int SearchAreaY;
        public int SearchWidth;
        public int SearchHeight;
        public int ExposureTime;
        public int LightControllerIndex;
        public int LightSourceValue;
        public int LightChannelIndex;
        public double StepDistance;
        public int MatrixIndex;


        public St_ImageSetting(bool isInit=true)
        {
            CcdName = "Cam1";
            CamColorMode = "gray";
            IsFindTemplate = true;
            IsFindLine = true ;
            IsFindCircle=false;
            IsPinInspect = true;
            IsOffSetDetect = true;
            IsBlobInspect = true;
            FolderName = "";
            SearchAreaX=0;
            SearchAreaY=0;
            SearchWidth=1000;
            SearchHeight=1000;
            ExposureTime=100;
            LightControllerIndex=0;
            LightSourceValue=10;
            LightChannelIndex = 1;
            StepDistance = 1;
            MatrixIndex = 1;
        }
    }
}
