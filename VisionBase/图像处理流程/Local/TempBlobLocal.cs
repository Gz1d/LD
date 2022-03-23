using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public class TempBlobLocal : LocalBase
    {
        private TempLocal MyTempLocal = new TempLocal();
        private BlobLocal MyBlobLocal = new BlobLocal();
        public override bool doLocal() {
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
                    NowVisionPara.Template.CenterX, NowVisionPara.Template.TemplateAngle, out HomMat);//
                HObject AffineImg = new HObject();
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");//矫正图像位置
                MyBlobLocal.Set(AffineImg, NowVisionPara);
                MyBlobLocal.doLocal();
                //计算当前示教图片到当前图片的偏移矩阵
                HTuple ReHomMat = new HTuple();
                HOperatorSet.VectorAngleToRigid(NowVisionPara.Template.CenterY, NowVisionPara.Template.CenterX, NowVisionPara.Template.TemplateAngle,
                    TempLocalResult.row, TempLocalResult.col, TempLocalResult.angle, out ReHomMat);//
                MyBlobLocal.NowResult.AffineTransResult(ReHomMat, out NowResult);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
