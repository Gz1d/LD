using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Runtime.Serialization;


namespace VisionBase
{
   public class LocalBase
   {
        public HObject NowImg;
        public LocalPara NowVisionPara;
        public LocalResult NowResult = new LocalResult();
        public virtual void Set(   HObject ImgIn, LocalPara LocalParaIn) 
        {
            NowImg = ImgIn;
            NowVisionPara = LocalParaIn;  
        }
        public virtual void SetLocalPara(LocalPara LocalParaIn) 
        {
            NowVisionPara = LocalParaIn;
        }
        public virtual void SetLocalImg( HObject ImgIn)
        {
            NowImg = ImgIn;      
        }

        public virtual bool doLocal()
        {
            NowResult = new LocalResult();
            return true;
        }


        public virtual LocalResult GetResult()
        {
            CopyParaToResult();
            return NowResult;
        }

        public void  CopyParaToResult()
        {
            NowResult.TeachRow = NowVisionPara.localSetting.TeachImgLocal.Row;
            NowResult.TeachCol = NowVisionPara.localSetting.TeachImgLocal.Col;
            NowResult.TeachAngle = NowVisionPara.localSetting.TeachImgLocal.Angle;
            NowResult.TeachX = NowVisionPara.localSetting.GrabPosTeach.x;
            NowResult.TeachY = NowVisionPara.localSetting.GrabPosTeach.y;
            NowResult.TeachTheta = NowVisionPara.localSetting.GrabPosTeach.angle;
        }
    }

    public class LocalResult
    {
        /// <summary>定位结果行</summary>
        public double row =0;
        /// <summary> 定位结果列 </summary>
        public double col =0;
        /// <summary> 定位结果角度 </summary>
        public double angle = 0;

        /// <summary> 对应机械坐标系坐标X </summary>
        public double x = 0;
        /// <summary>对应的机械坐标系坐标Y </summary>
        public double y = 0;
        /// <summary> 对应的机械坐标系Theta </summary>
        public double Theta = 0;
        /// <summary> 示教的行坐标 </summary>
        public double TeachRow = 0;
        /// <summary>示教的列坐标 </summary>
        public double TeachCol = 0;
        /// <summary> 示教的角度坐标 </summary>
        public double TeachAngle = 0;
         /// <summary> 示教点对应的机械X坐标 </summary>
        public double TeachX = 0;
        /// <summary>示教点对应的机械Y坐标 </summary>
        public double TeachY = 0;
        /// <summary> 示教点对应的机械Theta坐标 </summary>
        public double TeachTheta = 0;
        /// <summary> 是否定位完成 </summary> 
        public bool IsLocalOk = false;
        /// <summary> 显示定位过程中产生的轮廓 </summary>
        public HObject  ShowContour = new HObject();
        /// <summary>
        /// 示教时mark到旋转中心的坐标
        /// </summary>
        public St_VectorAngle TeachPosToRot = new St_VectorAngle();
        /// <summary>
        /// 实际定位时Mark到旋转中心的坐标
        /// </summary>
        public St_VectorAngle PosToRot = new St_VectorAngle();

        /// <summary>
        /// 这版软件的思想是将新图片模板匹配后，先转到和示教图片一样的位置，完成定位后，再将定位结果转到实际图片位置
        /// </summary>
        /// <param name="HomMat"></param>
        /// <param name="AffinResult"></param>
        public virtual void AffineTransResult(  HTuple  HomMat, out LocalResult AffinResult  )
        {
            AffinResult = new LocalResult();         
            MyVisionBase.AffineTransPt(row, col, HomMat, out AffinResult.row, out AffinResult.col);
            HTuple sx = new HTuple(), sy = new HTuple(), phi = new HTuple(), theta = new HTuple(),tx =new HTuple(),ty =new HTuple();
            HOperatorSet.HomMat2dToAffinePar(HomMat, out sx, out sy, out phi, out theta, out tx, out ty);
            AffinResult.angle = angle - phi.D;
            HOperatorSet.AffineTransContourXld(ShowContour, out AffinResult.ShowContour, HomMat);
            AffinResult.IsLocalOk = true;
        }


        public LocalResult Copy()
        {
            LocalResult NowLocalRlt = new LocalResult();
            NowLocalRlt.row = this.row;
            NowLocalRlt.col = this.col;
            NowLocalRlt.angle = this.angle;
            NowLocalRlt.x = this.x;
            NowLocalRlt.y = this.y;
            NowLocalRlt.Theta = this.Theta;
            NowLocalRlt.TeachRow = this.TeachRow;
            NowLocalRlt.TeachCol = this.TeachCol;
            NowLocalRlt.TeachAngle = this.TeachAngle;
            NowLocalRlt.TeachX = this.TeachX;
            NowLocalRlt.TeachY = this.TeachY;
            NowLocalRlt.TeachTheta = this.TeachTheta;
            NowLocalRlt.IsLocalOk = this.IsLocalOk;
            NowLocalRlt.TeachPosToRot = this.TeachPosToRot; 
            NowLocalRlt.PosToRot = this.PosToRot;
            if (!(this.ShowContour == null || !this.ShowContour.IsInitialized()))
                HOperatorSet.CopyObj(this.ShowContour, out NowLocalRlt.ShowContour, 1, -1);
            else
                HOperatorSet.GenEmptyObj(out NowLocalRlt.ShowContour);
            return NowLocalRlt;
        }

        ~LocalResult()
        {    
        }
    }

    public class BlobLocalRlt : LocalResult
    {
        public List<double> ListRow = new List<double>();
        public List<double> ListCol = new List<double>();
        public List<double> ListWid = new List<double>();
        public List<double> ListHei = new List<double>();
        public List<double> ListArea = new List<double>();
        public BlobLocalRlt()
        {
            ListRow = new List<double>();
            ListCol = new List<double>();
            ListWid = new List<double>();
            ListHei = new List<double>();
            ListArea = new List<double>();
            row = 0;
            col = 0;
            angle = 0;
            x = 0;
            y = 0;
            Theta = 0;
            TeachRow = 0;
            TeachCol = 0;
            TeachAngle = 0;
            TeachX = 0;
            TeachY = 0;
            TeachTheta = 0;
            IsLocalOk = false;
            ShowContour = new HObject();
            TeachPosToRot = new St_VectorAngle();
            PosToRot = new St_VectorAngle();
        }
    }


    public class LineCircRectRlt : LocalResult
    {
        public bool IsOk = true;

        public HObject NgContour = new HObject();
        public HObject DetectContour = new HObject();

        public LineCircRectRlt()
        {
            IsOk = true;
            NgContour = new HObject();
            DetectContour = new HObject();
            row = 0;
            col = 0;
            angle = 0;
            x = 0;
            y = 0;
            Theta = 0;
            TeachRow = 0;
            TeachCol = 0;
            TeachAngle = 0;
            TeachX = 0;
            TeachY = 0;
            TeachTheta = 0;
            IsLocalOk = false;

            ShowContour = new HObject();
            NgContour = new HObject();
            DetectContour = new HObject();
            TeachPosToRot = new St_VectorAngle();
            PosToRot = new St_VectorAngle();

        }

        public override void AffineTransResult(HTuple HomMat, out LocalResult AffinResult)
        {
            AffinResult = this;
            MyVisionBase.AffineTransPt(row, col, HomMat, out AffinResult.row, out AffinResult.col);
            HTuple sx = new HTuple(), sy = new HTuple(), phi = new HTuple(), theta = new HTuple(), tx = new HTuple(), ty = new HTuple();
            HOperatorSet.HomMat2dToAffinePar(HomMat, out sx, out sy, out phi, out theta, out tx, out ty);
            AffinResult.angle = angle - phi.D;

            HOperatorSet.AffineTransContourXld(this.DetectContour, out ((LineCircRectRlt)AffinResult).DetectContour, HomMat);
            HObject AffineNgCont = new HObject();
            HOperatorSet.GenEmptyObj(out AffineNgCont);

            HObject myCont = new HObject();
            HOperatorSet.AffineTransRegion(this.NgContour, out AffineNgCont, HomMat, "constant");
            HOperatorSet.CopyObj(AffineNgCont, out ((LineCircRectRlt)AffinResult).NgContour, 1, -1);
           
        }




    }

}
