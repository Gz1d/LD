using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionBase
{
   public  class InspectBase
    {
        public HObject NowImg;

        public VisionPara NowVisionPara;

        public VisionResult NowResult;

        public virtual void Set(HObject ImgIn, VisionPara VisionParaIn)
        {
            NowImg = ImgIn;
            NowVisionPara = VisionParaIn;
        }

        public virtual void  doInInspect()
        {
            NowResult = new VisionResult();
        }
        public virtual VisionResult GetResult()
        {

            return NowResult;
        }
    }



    public class VisionResult
    {
        /// <summary> 产品是否OK </summary>
        public string InspectIsOk ="";
        /// <summary> 检测是否出错 </summary>
        public string IsErr ="";
        /// <summary> 对位偏差X </summary>
        public double OffSetX=0;
        /// <summary> 对位偏差Y</summary>
        public double OffSetY=0;
        /// <summary>对位偏差Theta </summary>
        public double OffSetAngle=0;
        /// <summary> 最小外结矩形宽 </summary>
        public double MinRectWid=0;
        /// <summary> 最小外结矩形高 </summary>
        public double MinRectHei=0;
        /// <summary>区域面积 </summary>
        public double Area=0;
        /// <summary> pin针的最大偏移量</summary>
        public double PinOffSetDist=0;

        public HObject ShowContour = new HObject();
        public HObject ErrContour = new HObject();

        public VisionResult()
        {
            InspectIsOk = "";
            IsErr = "";
            OffSetX = 0;
            OffSetY = 0;
            OffSetAngle = 0;
            MinRectWid = 0;
            MinRectHei = 0;
            Area = 0;
            PinOffSetDist = 0;
            ShowContour = new HObject();
            HOperatorSet.GenEmptyObj(out ShowContour);
            ErrContour = new HObject();
            HOperatorSet.GenEmptyObj(out ErrContour);
        }


        public void  OffSetResultIn(double X,double Y,double Theta,HObject ContourIn  )
        {
            OffSetX = X;
            OffSetY = Y;
            OffSetAngle = Theta;

            ShowContour = ContourIn;


        }

        public void GlueResultIn( double Wid ,double Hei,double AreaIn,HObject ContourIn   )
        {
            Area = AreaIn;
            MinRectWid = Wid;
            MinRectHei = Hei;

            ShowContour = ContourIn;
        }

        ~VisionResult()
           {

        
        
        }
    }
}
