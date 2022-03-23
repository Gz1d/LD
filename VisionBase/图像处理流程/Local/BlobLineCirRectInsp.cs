using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public  class BlobLineCirRectInsp :LocalBase
    {
        private BlobLocal myBlobLocal = new BlobLocal();

        public override bool doLocal()
        {
            NowResult = new LineCircRectRlt();
            BlobLocalRlt myBlobLocalRlt = new BlobLocalRlt();
            try {
                myBlobLocal.Set(NowImg, NowVisionPara);
                myBlobLocal.doLocal();
                myBlobLocalRlt = (BlobLocalRlt)myBlobLocal.GetResult();
                HTuple HomMat = new HTuple(); //
                //计算当前图片到示教图片的偏移矩阵
                HOperatorSet.VectorAngleToRigid(myBlobLocalRlt.row, myBlobLocalRlt.col, myBlobLocalRlt.angle,
                    myBlobLocalRlt.TeachRow, myBlobLocalRlt.TeachCol, myBlobLocalRlt.TeachAngle, out HomMat);
                HObject AffineImg = new HObject();
                //矫正图像位置
                HOperatorSet.AffineTransImage(NowImg, out AffineImg, HomMat, "constant", "false");
                //开始检测
                ((LineCircRectRlt)NowResult).IsOk = LineCircleRectInspBase.LineCircleRectInsp(AffineImg, NowVisionPara.LineCirRectInspParam,
                         out ((LineCircRectRlt)NowResult).DetectContour, out ((LineCircRectRlt)NowResult).NgContour);  //
                HOperatorSet.CopyObj(myBlobLocalRlt.ShowContour, out ((LineCircRectRlt)NowResult).ShowContour, 1, -1);
                //位置的逆变换
                HOperatorSet.VectorAngleToRigid(myBlobLocalRlt.TeachRow, myBlobLocalRlt.TeachCol, myBlobLocalRlt.TeachAngle, 
                                    myBlobLocalRlt.row, myBlobLocalRlt.col, myBlobLocalRlt.angle, out HomMat);
                NowResult.AffineTransResult(HomMat, out NowResult);
                return true;
            }
            catch
            {
                return false;
            }


        }




    }
}
