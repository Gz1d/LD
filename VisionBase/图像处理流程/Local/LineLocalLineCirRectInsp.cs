using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public class LineLocalLineCirRectInsp:LocalBase
    {
        private TwoLineLocal MyLineLocal = new TwoLineLocal();

        public override bool doLocal()
        {
            NowResult = new LineCircRectRlt();
            LocalResult lineLocalRlt = new LocalResult();
            try
            {
                MyLineLocal.Set(NowImg, NowVisionPara);
                MyLineLocal.doLocal();
                lineLocalRlt = MyLineLocal.GetResult();
                HTuple HomMat = new HTuple(); //
                //计算当前图片到示教图片的偏移矩阵
                HOperatorSet.VectorAngleToRigid(lineLocalRlt.row, lineLocalRlt.col, lineLocalRlt.angle, lineLocalRlt.TeachRow,
                    lineLocalRlt.TeachCol, lineLocalRlt.TeachAngle, out HomMat);//
                HObject AffineImg = new HObject();
                //矫正图像位置
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");
                ((LineCircRectRlt)NowResult).IsOk = LineCircleRectInspBase.LineCircleRectInsp(AffineImg, NowVisionPara.LineCirRectInspParam,
                       out ((LineCircRectRlt)NowResult).DetectContour, out ((LineCircRectRlt)NowResult).NgContour);  //
                HOperatorSet.CopyObj(lineLocalRlt.ShowContour, out ((LineCircRectRlt)NowResult).ShowContour, 1, -1);
                //位置的逆变换
                HOperatorSet.VectorAngleToRigid(lineLocalRlt.TeachRow,lineLocalRlt.TeachCol, lineLocalRlt.TeachAngle,
                    lineLocalRlt.row, lineLocalRlt.col, lineLocalRlt.angle, out HomMat);//
                NowResult.AffineTransResult(HomMat, out NowResult);
                return true;
            }
            catch (Exception e0){
                Logger.PopError1(e0, false, "视觉运行错误日志");
                return false;
            }
        }



    }
}
