using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public  class BlobTwoLineLocal : LocalBase
    {
        private BlobLocal myBlobLocal = new BlobLocal();
        private TwoLineLocal myTwoLineLocal = new TwoLineLocal();
        public override bool doLocal()
        {
            try
            {
                NowResult = new LocalResult();
                myBlobLocal.Set(NowImg, NowVisionPara);
                myBlobLocal.doLocal();
                LocalResult blobLocalRlt = new LocalResult();
                blobLocalRlt = myBlobLocal.GetResult();
                HTuple HomMat = new HTuple(); //
                                              //计算当前图片到示教图片的偏移矩阵
                HOperatorSet.VectorAngleToRigid(blobLocalRlt.row, blobLocalRlt.col, blobLocalRlt.angle, blobLocalRlt.TeachRow,
                    blobLocalRlt.TeachCol, blobLocalRlt.TeachAngle, out HomMat);//
                HObject AffineImg = new HObject();
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");//矫正图像位置
                myTwoLineLocal.Set(AffineImg, NowVisionPara);
                myTwoLineLocal.doLocal();
                LocalResult LineLocalResult = new LocalResult();
                //计算当前示教图片到当前图片的偏移矩阵
                HTuple ReHomMat = new HTuple();
                HOperatorSet.VectorAngleToRigid(blobLocalRlt.TeachRow, blobLocalRlt.TeachCol, blobLocalRlt.TeachAngle,
                    blobLocalRlt.row, blobLocalRlt.col, blobLocalRlt.angle, out ReHomMat);//
                myTwoLineLocal.NowResult.AffineTransResult(ReHomMat, out LineLocalResult);
                //调整直线定位的位置和
                HOperatorSet.ConcatObj(blobLocalRlt.ShowContour, LineLocalResult.ShowContour, out LineLocalResult.ShowContour);
                NowResult = LineLocalResult;
                return true;
            }
            catch
            { return false;  }
        }


    }
}
