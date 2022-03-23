using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VisionBase 
{
    public class CameraLightPara : Configuration
    {
        /// <summary> 相机名字 </summary>
        public CameraEnum CamName = CameraEnum.Cam0;
        /// <summary> 曝光时间 </summary>
        public int Exposure = 100;
        /// <summary> 相机的触发模式 </summary>
        public bool  TriggerModel = false;
        /// <summary>光源参数 </summary>
        public BindingList<LightPara> lightPara = new BindingList<LightPara>();
        /// <summary> 是否滤波 </summary>
        public bool IsFilter=false;
        /// <summary> 滤波参数 </summary>
        public double FilterC = 0.5;
        /// <summary>
        /// 线阵相机采集行数
        /// </summary>
        public int LineNum = 1000;

        private static string ConfigName = string.Format(@"config\{0}", "CameraLightPara");
        public CameraLightPara() {
            Exposure = 100;
            TriggerModel = false;
            lightPara = new BindingList<LightPara>();
            CamName = CameraEnum.Cam0;
            IsFilter = false;
            FilterC = 0.5;
            LineNum = 1000;
        }

        public void Save(string Path)
        {
            try{
                Serializition.SaveToFile(this, Path+ ConfigName);
            }
            catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }


        /// <summary>
        /// 加载配置
        /// </summary>
        /// <returns></returns>
        public static CameraLightPara Load(string Path)
        {
            try {
                CameraLightPara obj = (CameraLightPara)Serializition.LoadFromFile(typeof(CameraLightPara), Path+ConfigName);
                return obj;
            }
            catch (Exception ex) {
                throw new LoadException(Path + ConfigName, ex.Message);
            }
        }

    }


    public enum LightCtrlEmun
    {
        LightCtrl0 =0,
        LightCtrl1,
        LightCtrl2,
        LightCtrl3,
        LightCtrl4,
        LightCtrl5,
        LightCtrl6,
        LightCtrl7,
        LightCtrl8,
        LightCtrl9,
    }

    public enum LightCtrlTypeEnum
    {
        /// <summary> 沃德普 </summary>
        WordP = 0,
        /// <summary> 盟拓 </summary>
        MengT,
        /// <summary> 辰科 </summary>
        ChengKe,  
    
    }

    public enum LightPanelEnum
    { 
        Panel0 =0,
        Panel1,
        Panel2,
        Panel3,
        Panel4,
        Panel5,
        Panel6,
        Panel7,
    }

    public enum CameraEnum
    {        
        Cam0 = 0,
        Cam1,
        Cam2,
        Cam3,
        Cam4,
        Cam5,
        Cam6,
        Cam7,
        Cam8,
        Cam9,
        Cam10,
        Cam11,
        Cam12,
        Cam13,
        Cam14,
        Cam15,
        Cam16,
        Cam17,
        Cam18,
        Cam19,
        Cam20
    }
    /// <summary>
    /// 相机接口枚举
    /// </summary>
    public enum CamInterfaceEnum
    { 
        /// <summary>Halcon默认采图接口 </summary>
        HalconGigeE,
        /// <summary> balser接口 </summary>
        Pylon,
        /// <summary>大华接口 </summary>
        HMV3rdParty,
        /// <summary> dalsa接口 </summary>
        SaperaLT,
        /// <summary>dalsa图像采集卡接口 </summary>
        GenICamTL,

        CameraBasler,
        /// <summary> Dalsa线阵相机</summary>
        CameraCLDalsa,

    }

    public class LightPara
    {
        public LightCtrlEmun LightCtrl {set;get; }
        public LightPanelEnum Panel  { set;get;  }
        public int LightValue { set;get;}

        public LightPara()
        {
            this.LightCtrl = LightCtrlEmun.LightCtrl0;
            this.Panel = LightPanelEnum.Panel0;
            this.LightValue = 0;
        }
    
    }





}
