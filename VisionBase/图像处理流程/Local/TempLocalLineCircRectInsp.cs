using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public class TempLocalLineCircRectInsp : LocalBase
    {
        private TempLocal MyTempLocal = new TempLocal();

        public override bool doLocal()
        {
            NowResult = new LineCircRectRlt();
            LocalResult TempLocalResult = new LocalResult();
            try {
                MyTempLocal.Set(NowImg, NowVisionPara);
                MyTempLocal.doLocal();
                TempLocalResult = MyTempLocal.GetResult();
                HTuple HomMat = new HTuple(); //
                //计算当前图片到示教图片的偏移矩阵
                HOperatorSet.VectorAngleToRigid(TempLocalResult.row, TempLocalResult.col, TempLocalResult.angle, NowVisionPara.Template.CenterY,
                    NowVisionPara.Template.CenterX, NowVisionPara.Template.TemplateAngle, out HomMat);//
                HObject AffineImg = new HObject();
                //矫正图像位置
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");
                //开始检测
                ((LineCircRectRlt)NowResult).IsOk = LineCircleRectInspBase.LineCircleRectInsp(AffineImg, NowVisionPara.LineCirRectInspParam,
                    out ((LineCircRectRlt)NowResult).DetectContour, out ((LineCircRectRlt)NowResult).NgContour);  //
                HOperatorSet.CopyObj(TempLocalResult.ShowContour, out ((LineCircRectRlt)NowResult).ShowContour, 1, -1);
                //位置的逆变换
                HOperatorSet.VectorAngleToRigid(NowVisionPara.Template.CenterY, NowVisionPara.Template.CenterX, NowVisionPara.Template.TemplateAngle,
                    TempLocalResult.row, TempLocalResult.col, TempLocalResult.angle, out HomMat);//
                NowResult.AffineTransResult(HomMat, out NowResult);
                return true;
            }
            catch(Exception e0){
                Logger.PopError1(e0, false, "视觉运行错误日志");
                return false;
            }
        }

    }
}
