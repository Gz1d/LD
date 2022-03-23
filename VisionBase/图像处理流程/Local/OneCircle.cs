using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    class OneCircle : LocalBase
    {
        public override bool doLocal()
        {
            try {
                NowResult = new LocalResult();
                St_CirclesParam CirclesParam = new St_CirclesParam(2);
                CirclesParam = NowVisionPara.Circles;
                HTuple CircleRows = new HTuple(), CircleCols = new HTuple(), CircleRs = new HTuple(), StartPhis = new HTuple(), EndPhis = new HTuple();
                HObject CircleContour = new HObject(), CircleCont = new HObject(), CenterCross = new HObject();
                //找出要找的圆
                CircleTypePos.FindCircle(NowImg, CirclesParam, out CircleRows, out CircleCols, out CircleRs, out StartPhis, out EndPhis,
                    out CircleContour, out CircleCont, out CenterCross);
                HOperatorSet.ConcatObj(CircleContour, CircleCont, out CircleContour);
                HOperatorSet.ConcatObj(CircleContour, CenterCross, out CircleContour);
                CircleCont.Dispose();
                CenterCross.Dispose();
                NowResult.ShowContour = CircleContour;

                NowResult.row = CircleRows.D;
                NowResult.col = CircleCols.D;
                HTuple angle = new HTuple();
                angle = 0;
                NowResult.angle = angle.D;
                return true;
            }
            catch
            { return false; }
        }




    }
}
