using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace VisionBase
{
    public class CameraPara
    {
        public bool TriggerModel{set;get; }
        public bool IsMirrorX{set;get;}
        public bool IsMirrorY{ set;get;}
        public bool IsRot {set;get; }
        public CameraEnum CameraName{ set; get;}
        public CamInterfaceEnum CameInterface {set;get; }
        public string CamDecribe{ set;get; }
        public bool IsActive{set;get; }
        public bool IsOpen {set;get; }
        public  int Width { set; get; }
        public int Height{ get; set; }
        public string ServerName { get; set; }
        public string DeviceName { get; set; }
        public string CcfPath { get; set; }

        public CameraPara()
        {
            TriggerModel = false;
            IsMirrorX = false;
            IsMirrorY = false;
            IsRot = false;
            CameInterface = CamInterfaceEnum.HalconGigeE;
            CameraName = CameraEnum.Cam0;
            IsActive = false;
            IsOpen = false;
            this.Width = 8192;
            Height = 1000;
            ServerName = "";
            DeviceName = "";
            CcfPath = "";
        }
    }
}
