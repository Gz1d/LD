using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public class InspectControl
    {
        private InspectBase inspectBase;

        public void setInspect(InspectBase InspectIn)
        {
            inspectBase = InspectIn;
        }
        public void SetParam(HObject ImgIn, VisionPara VisionParaIn)
        {
            inspectBase.Set(ImgIn, VisionParaIn);
        }
        public void DoInspect()
        {

            inspectBase.doInInspect();
        }

        public VisionResult GetResult()
        {
            return inspectBase.GetResult();
        }




    }
}
