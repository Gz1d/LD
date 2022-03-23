using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using VisionBase.Matching;
using HalconDotNet;

namespace VisionBase
{
    public  class TempLocal:LocalBase
    {
        public override bool doLocal()
        {
            NowResult = new LocalResult();
            //1.0模板匹配
            St_TemplateParam TemplateParam = NowVisionPara.Template;
            RectangleF roi = new RectangleF();
            LocalSettingPara Setting = NowVisionPara.localSetting;
            roi.X = Setting.SearchAreaX;
            roi.Y = Setting.SearchAreaY;
            roi.Width = Setting.SearchWidth;
            roi.Height = Setting.SearchHeight;
            MatchingResult result;
            try{
                NowResult.IsLocalOk = TemplateParam.FindSharpTemplate(NowImg, roi, TemplateParam, out result);
                NowResult.row = result.mRow;
                NowResult.col = result.mCol;
                NowResult.angle = result.mAngle;
                //NowResult.ShowContour = result.mContour;
                NowResult.ShowContour = result.getDetectionResults();
                return true;
            }
            catch 
            { return false; }


        }
    }
}
