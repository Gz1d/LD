using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace VisionBase

{
    class TempTwoCircleLocal:LocalBase
    {
        private TempLocal MyTempLocal = new TempLocal();
        private TwoCycleLocal MyCircleLocal = new TwoCycleLocal();
        public override bool doLocal()
        {
            try
            {
                NowResult = new LocalResult();
                MyTempLocal.Set(NowImg, NowVisionPara);
                MyTempLocal.doLocal();
                LocalResult TempLocalResult = new LocalResult();
                TempLocalResult = MyTempLocal.GetResult();
                HTuple HomMat = new HTuple(); //
                                              //计算当前图片到示教图片的偏移矩阵
                HOperatorSet.VectorAngleToRigid(TempLocalResult.row, TempLocalResult.col, TempLocalResult.angle, NowVisionPara.Template.CenterY,
                    NowVisionPara.Template.CenterX, NowVisionPara.Template.TemplateAngle, out HomMat);
                HObject AffineImg = new HObject();
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");//矫正图像位
                                                                                                  //计算示教图片到当前图片的偏移矩阵
                HTuple ReHomMat = new HTuple();
                HOperatorSet.VectorAngleToRigid(NowVisionPara.Template.CenterY, NowVisionPara.Template.CenterX, NowVisionPara.Template.TemplateAngle,
                    TempLocalResult.row, TempLocalResult.col, TempLocalResult.angle, out ReHomMat);
                MyCircleLocal.Set(NowImg, NowVisionPara);
                MyCircleLocal.doLocal();
                LocalResult CircleResult = new LocalResult();
                MyCircleLocal.NowResult.AffineTransResult(ReHomMat, out CircleResult);
                HOperatorSet.ConcatObj(TempLocalResult.ShowContour, CircleResult.ShowContour, out CircleResult.ShowContour);
                NowResult = CircleResult;
                return true;
            }
            catch
            { return false; }
        }

    }
}
