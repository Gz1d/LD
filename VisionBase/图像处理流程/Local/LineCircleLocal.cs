using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace VisionBase
{
    class LineCircleLocal:LocalBase
    {
        public override bool doLocal()
        {
            try
            {
                NowResult = new LocalResult();
                St_LinesParam LinesPara = new St_LinesParam(2);
                LinesPara = NowVisionPara.Lines;
                HTuple LineRow1 = new HTuple(), LineCol1 = new HTuple(), LineRow2 = new HTuple(), LineCol2 = new HTuple();
                HObject LinesContour = new HObject(), LinePtCont = new HObject();
                //找出所有的直线
                LineTypePos.FindLine(LinesPara, NowImg, out LineRow1, out LineCol1, out LineRow2, out LineCol2, out LinesContour, out LinePtCont);
                St_CirclesParam CirclesParam = new St_CirclesParam(2);
                CirclesParam = NowVisionPara.Circles;
                HTuple CircleRows = new HTuple(), CircleCols = new HTuple(), CircleRs = new HTuple(), StartPhis = new HTuple(), EndPhis = new HTuple();
                HObject CircleContour = new HObject(), CircleCont = new HObject(), CenterCross = new HObject();
                HTuple angle;
                //图像坐标轴X轴到直线1的角度
                HOperatorSet.AngleLx(LineRow1[0], LineCol1[0], LineRow2[0], LineCol2[0], out angle);
                //找出要找的圆
                CircleTypePos.FindCircle(NowImg, CirclesParam, out CircleRows, out CircleCols, out CircleRs, out StartPhis, out EndPhis,
                    out CircleContour, out CircleCont, out CenterCross);
                HOperatorSet.ConcatObj(CircleContour, CircleCont, out CircleContour);
                HOperatorSet.ConcatObj(CircleContour, CenterCross, out CircleContour);
                CircleCont.Dispose();
                CenterCross.Dispose();
                HOperatorSet.ConcatObj(CircleContour, LinesContour, out CircleContour);
                HOperatorSet.ConcatObj(CircleContour, LinePtCont, out CircleContour);
                LinesContour.Dispose();
                LinePtCont.Dispose();
                NowResult.row = CircleRows[0].D;
                NowResult.col = CircleCols[0].D;
                NowResult.angle = angle.D;
                NowResult.ShowContour = CircleContour;
                return true;
            }
            catch
            { return false; }
        }

    }
}
