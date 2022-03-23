using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public class CameraSaperaLT:CameraBase
    {
        public override bool DoInit()
        {
            try {
                string ccfFile = @"ccf\" + myCamPara.CcfPath + ".ccf";
                HOperatorSet.OpenFramegrabber("SaperaLT", 1, 1, myCamPara.Width, myCamPara.Height, 0, 0, "default", -1, "default", -1, "false", ccfFile,
                               this.myCamPara.ServerName.ToString(), 0, -1, out this.CamHandle);
            }
            catch  {
                return false;          
            }
            finally {
                this.IsOpen = true;
            }
            return true;
        }

    }
}
