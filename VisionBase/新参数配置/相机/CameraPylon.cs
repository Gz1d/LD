using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public class CameraPylon:CameraBase
    {
        public override bool DoInit()
        {
            try {
                HTuple DeviceInfo = new HTuple(), DeviceValue = new HTuple();
                HOperatorSet.InfoFramegrabber("pylon", "device", out DeviceInfo, out DeviceValue);
                HOperatorSet.OpenFramegrabber("pylon", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "default", "default",
                this.myCamPara.CameraName.ToString(), -1, -1, out this.CamHandle);
                this.IsOpen = true;
            }
            catch  {
                return false;      
            }
            return true;
        }
    }
}
