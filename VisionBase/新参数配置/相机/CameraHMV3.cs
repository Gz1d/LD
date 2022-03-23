using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public  class CameraHMV3 : CameraBase
    {
        public override bool DoInit()
        {
            try{
                HOperatorSet.OpenFramegrabber("HMV3rdParty", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default",
                                    this.myCamPara.CameraName.ToString(), 0, -1, out this.CamHandle);
                this.IsOpen = true;
            }
            catch{
                return false;          
            }
            return true;
        }

    }
}
