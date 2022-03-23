using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
   public static  class LineCircleRectInspBase
    {

        public static   bool LineCircleRectInsp(HObject ImgIn,St_InspectImageSetting LineCircleRectParaIn, out HObject DetectContours, out HObject BadRegions)
        {
            bool LineIsOK = true, CircleIsOk =true, RectIsOk =true;
            St_InspectImageSetting ImgInspectPara0 = LineCircleRectParaIn;
            HOperatorSet.GenEmptyObj(out BadRegions);
            HOperatorSet.GenEmptyObj(out DetectContours);
            //3.1直线区域检测
            HObject LineBadRegions = new HObject(), DetectLineContours = new HObject();
            HOperatorSet.GenEmptyObj(out LineBadRegions);
            HOperatorSet.GenEmptyObj(out DetectLineContours);
            LineIsOK = MyVisionBase.DectectLines(ImgIn, ImgInspectPara0.InspectLinePara,  out LineBadRegions, out DetectLineContours);
            HOperatorSet.ConcatObj(BadRegions, LineBadRegions, out BadRegions);
            HOperatorSet.ConcatObj(DetectLineContours, DetectContours, out DetectContours);
            //3.2圆形区域检测
            HObject CircleBadRegion = new HObject(), DetectCircleContours = new HObject();
            HOperatorSet.GenEmptyObj(out CircleBadRegion);
            HOperatorSet.GenEmptyObj(out DetectCircleContours);
            CircleIsOk = MyVisionBase.DetectCircles(ImgIn, ImgInspectPara0.InspectCirclePara,  out CircleBadRegion, out DetectCircleContours);
            HOperatorSet.ConcatObj(BadRegions, CircleBadRegion, out BadRegions);
            HOperatorSet.ConcatObj(DetectCircleContours, DetectContours, out DetectContours);
            //3.3矩形区域检测
            HObject RectBadRegion = new HObject(), DetectRectContours = new HObject();
            HOperatorSet.GenEmptyObj(out RectBadRegion);
            HOperatorSet.GenEmptyObj(out DetectRectContours);
            St_InspectRectanglePara NowRectPara = new St_InspectRectanglePara(ImgInspectPara0.InspectRectPara.Cols.Count(), true);
            NowRectPara = ImgInspectPara0.InspectRectPara;
            RectIsOk = MyVisionBase.DetetctRectangles(ImgIn, ImgInspectPara0.InspectRectPara, out RectBadRegion, out DetectRectContours);
            HOperatorSet.ConcatObj(RectBadRegion, BadRegions, out BadRegions);
            HOperatorSet.ConcatObj(DetectRectContours, DetectContours, out DetectContours);
            if (LineIsOK && CircleIsOk && RectIsOk) return true;
            else return false;
        }

    }
}
