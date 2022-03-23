using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public class TempLineCircleLocalLinCirRectInsp :LocalBase
    {
        private TempLineCirLocal MyTempLineCirLocal = new TempLineCirLocal();
        public override bool doLocal()
        {
            NowResult =   new LineCircRectRlt();
            LocalResult TempLineCirRlt = new LocalResult();
            try{
                MyTempLineCirLocal.Set(NowImg, NowVisionPara);
                MyTempLineCirLocal.doLocal();
                TempLineCirRlt = MyTempLineCirLocal.GetResult();
                HTuple HomMat = new HTuple(); //
                //计算当前图片到示教图片的偏移矩阵
                HOperatorSet.VectorAngleToRigid(TempLineCirRlt.row, TempLineCirRlt.col, TempLineCirRlt.angle, TempLineCirRlt.TeachRow,
                    TempLineCirRlt.TeachCol, TempLineCirRlt.TeachAngle, out HomMat);//
                HObject AffineImg = new HObject();
                //矫正图像位置
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");
                ((LineCircRectRlt)NowResult).IsOk = LineCircleRectInspBase.LineCircleRectInsp(AffineImg, NowVisionPara.LineCirRectInspParam,
                       out ((LineCircRectRlt)NowResult).DetectContour, out ((LineCircRectRlt)NowResult).NgContour);  //
                HOperatorSet.CopyObj(TempLineCirRlt.ShowContour, out ((LineCircRectRlt)NowResult).ShowContour, 1, -1);
                //位置的逆变换
                HOperatorSet.VectorAngleToRigid(TempLineCirRlt.TeachRow, TempLineCirRlt.TeachCol, TempLineCirRlt.TeachAngle,
                    TempLineCirRlt.row, TempLineCirRlt.col, TempLineCirRlt.angle, out HomMat);//
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
