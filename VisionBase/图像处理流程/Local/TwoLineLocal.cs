using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace VisionBase
{/// <summary>
/// 计算两线的焦点与角度
/// </summary>
    class TwoLineLocal : LocalBase
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
                HTuple CrossRow = new HTuple(), CrossCol = new HTuple(), IsParallel = new HTuple();
                //第一条直线和第二条直线的交点
                HOperatorSet.IntersectionLl(LineRow1[0], LineCol1[0], LineRow2[0], LineCol2[0], LineRow1[1], LineCol1[1], LineRow2[1], LineCol2[1],
                    out CrossRow, out CrossCol, out IsParallel);
                HTuple angle;
                //图像坐标轴X轴到直线1的角度
                HOperatorSet.AngleLx(LineRow1[0], LineCol1[0], LineRow2[0], LineCol2[0], out angle);
                NowResult.row = CrossRow.D;
                NowResult.col = CrossCol.D;
                NowResult.angle = angle.D;
                HOperatorSet.ConcatObj(LinesContour, LinePtCont, out LinesContour);
                LinePtCont.Dispose();
                NowResult.ShowContour = LinesContour;
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
