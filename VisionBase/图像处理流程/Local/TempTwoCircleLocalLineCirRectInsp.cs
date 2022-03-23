using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public class TempTwoCircleLocalLineCirRectInsp : LocalBase
    {
        private TempTwoCircleLocal MyTempTwoCirLocal = new TempTwoCircleLocal();

        public override bool doLocal()
        {
            LocalResult TempTwoCirLocalRlt = new LocalResult();
            NowResult = new LineCircRectRlt();

            try{
                MyTempTwoCirLocal.Set(NowImg, NowVisionPara);
                MyTempTwoCirLocal.doLocal();
                TempTwoCirLocalRlt = MyTempTwoCirLocal.GetResult();
                HTuple HomMat = new HTuple(); //
                //计算当前图片到示教图片的偏移矩阵
                HOperatorSet.VectorAngleToRigid(TempTwoCirLocalRlt.row, TempTwoCirLocalRlt.col, TempTwoCirLocalRlt.angle, TempTwoCirLocalRlt.TeachRow,
                    TempTwoCirLocalRlt.TeachCol, TempTwoCirLocalRlt.TeachAngle, out HomMat);//
                HObject AffineImg = new HObject();
                //矫正图像位置
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");
                ((LineCircRectRlt)NowResult).IsOk = LineCircleRectInspBase.LineCircleRectInsp(AffineImg, NowVisionPara.LineCirRectInspParam,
                       out ((LineCircRectRlt)NowResult).DetectContour, out ((LineCircRectRlt)NowResult).NgContour);  //
                HOperatorSet.CopyObj(TempTwoCirLocalRlt.ShowContour, out ((LineCircRectRlt)NowResult).ShowContour, 1, -1);
                //位置的逆变换
                HOperatorSet.VectorAngleToRigid(TempTwoCirLocalRlt.TeachRow, TempTwoCirLocalRlt.TeachCol, TempTwoCirLocalRlt.TeachAngle,
                    TempTwoCirLocalRlt.row, TempTwoCirLocalRlt.col, TempTwoCirLocalRlt.angle, out HomMat);//
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
