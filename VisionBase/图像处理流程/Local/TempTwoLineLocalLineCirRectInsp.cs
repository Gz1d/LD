using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public  class TempTwoLineLocalLineCirRectInsp:LocalBase
    {
        private TempTwoLineLocal MyTempTwoLineLocal = new TempTwoLineLocal();
        public override bool doLocal()
        {
            NowResult = new LineCircRectRlt();
            LocalResult TempTwoLineLocalRlt = new LocalResult();
            try {
                MyTempTwoLineLocal.Set(NowImg, NowVisionPara);
                MyTempTwoLineLocal.doLocal();
                TempTwoLineLocalRlt = MyTempTwoLineLocal.GetResult();
                HTuple HomMat = new HTuple(); //
                //计算当前图片到示教图片的偏移矩阵
                HOperatorSet.VectorAngleToRigid(TempTwoLineLocalRlt.row, TempTwoLineLocalRlt.col, TempTwoLineLocalRlt.angle, TempTwoLineLocalRlt.TeachRow,
                    TempTwoLineLocalRlt.TeachCol, TempTwoLineLocalRlt.TeachAngle, out HomMat);//
                HObject AffineImg = new HObject();
                //矫正图像位置
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");
                ((LineCircRectRlt)NowResult).IsOk = LineCircleRectInspBase.LineCircleRectInsp(AffineImg, NowVisionPara.LineCirRectInspParam,
                       out ((LineCircRectRlt)NowResult).DetectContour, out ((LineCircRectRlt)NowResult).NgContour);  //
                HOperatorSet.CopyObj(TempTwoLineLocalRlt.ShowContour, out ((LineCircRectRlt)NowResult).ShowContour, 1, -1);
                //位置的逆变换
                HOperatorSet.VectorAngleToRigid(TempTwoLineLocalRlt.TeachRow, TempTwoLineLocalRlt.TeachCol, TempTwoLineLocalRlt.TeachAngle,
                    TempTwoLineLocalRlt.row, TempTwoLineLocalRlt.col, TempTwoLineLocalRlt.angle, out HomMat);//
                NowResult.AffineTransResult(HomMat, out NowResult);
                return true;
            }
            catch (Exception e0)
            {
                Logger.PopError1(e0, false, "视觉运行错误日志");
                return false;
            }
        }



    }
}
