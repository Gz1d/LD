using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace VisionBase
{
    public struct St_CirclesParam
    {
        public int Count;
        public List<double> CenterRows;
        public List<double> CenterCols;
        public List<double> CircleRs;
        public List<double> Elements;
        public List<double> DetectHeights;
        public List<double> Thresholds;
        public List<string> Directs;
        public List<double> StartPhis;
        public List<double> EndPhis;
		public double OffsetPixelX;   ///视觉计算目标位置后的补偿值
        public double OffsetPixelY;
        public double OffSetPixelTh;
        public List<string> PointOrders;
        public St_CirclesParam(bool isInit=true)
        {
            Count = 2;
            CenterRows = new List<double>() ;
            CenterCols=new List<double>();
            CircleRs=new List<double>();
            Elements=new List<double>();
            DetectHeights=new List<double>();
            Thresholds=new List<double>();
            Directs=new List<string>();
            PointOrders = new List<string>();
            StartPhis = new List<double>();
            EndPhis = new List<double>();
			OffsetPixelX=0;
            OffsetPixelY = 0;
            OffSetPixelTh = 0;

            for (int i = 0; i < Count; i++)
            {
                CenterRows.Add(0);
                CenterCols.Add(0);
                CircleRs.Add(0);
                Elements.Add(32);
                DetectHeights.Add(50);
                Thresholds.Add(10);
                Directs.Add("");
                StartPhis.Add(0);
                EndPhis.Add(0);
                PointOrders.Add("");
            }
        }
        public St_CirclesParam(int Num = 2)
        {
            Count = Num;
            CenterRows = new List<double>();
            CenterCols = new List<double>();
            CircleRs = new List<double>();
            Elements = new List<double>();
            DetectHeights = new List<double>();
            Thresholds = new List<double>();
            Directs = new List<string>();
            PointOrders = new List<string>();
            StartPhis = new List<double>();
            EndPhis = new List<double>();
            OffsetPixelX = 0;
            OffsetPixelY = 0;
            OffSetPixelTh = 0;
            for (int i = 0; i < Count; i++)
            {
                CenterRows.Add(0);
                CenterCols.Add(0);
                CircleRs.Add(0);
                Elements.Add(32);
                DetectHeights.Add(50);
                Thresholds.Add(10);
                Directs.Add("");
                StartPhis.Add(0);
                EndPhis.Add(0);
                PointOrders.Add("");
            }
        }

        public bool GetCircleParam(int Index,out double  CenterRowOut,out double  CenterColOut,out double  CircleROut, out double ElementOut, 
            out double  ThresholdOut,out double HeightOut,out double  StartPhiOut ,out double  EndPhiOut ,out string DirectOut ,out string PointOrderOut)
        {
            CenterRowOut = 0;
            CenterColOut = 0;
            CircleROut = 0;
            ElementOut = 0;
            HeightOut = 0;
            StartPhiOut = 0;
            EndPhiOut = 0;
            DirectOut = "";
            PointOrderOut = "";
            ThresholdOut = 10;
            if (Count > Index)
            {
                CenterRowOut = CenterRows[Index];
                CenterColOut = CenterCols[Index];
                CircleROut = CircleRs[Index];
                ElementOut = Elements[Index];
                HeightOut = DetectHeights[Index];
                StartPhiOut = StartPhis[Index];
                EndPhiOut = EndPhis[Index];
                DirectOut = Directs[Index];
                PointOrderOut = PointOrders[Index];
                ThresholdOut = Thresholds[Index];
                return true;
            }
            return false;
        }
        public bool SetParam(int Index,double CenterRow,double CenterCol,double  CircleR,double Element,double Threshold, double DetectHeight,double StartPhi,double EndPhi,string Direct,
                             string PointOrder)
        {
            if (Count > Index)
            {
                CenterRows[Index] = CenterRow;
                CenterCols[Index] = CenterCol;
                CircleRs[Index] = CircleR;
                Elements[Index] = Element;
                DetectHeights[Index] = DetectHeight;
                StartPhis[Index] = StartPhi;
                EndPhis[Index] = EndPhi;
                Directs[Index] = Direct;
                PointOrders[Index] = PointOrder;
                Thresholds[Index] = Threshold;
            }
            return false;
        }
    }

    public enum En_DotLocModel
    { 
        两线交点=0,
        单圆圆心,
        双圆圆心,
        三角中心1,
        三角中心2,
        方形中心       
    }
    public enum En_OriOrTarget
    {  
        原点 = 0,
        目标中心
      
    }

    public enum En_AngleLocModel
    { 
        单线,
        两圆
    
    }

    /// <summary> 定位量X、Y定位参数 </summary>
    public struct St_DotParam
    {
        /// <summary>点定位模式  -1表示没有示教，0表示两线交点，1表示单圆圆心，2表示双圆中心，3表示三角中心, 4三角中心2, 5表示方形中心 </summary>
        public int DotLocModel;
        /// <summary>选择线的编号 </summary>
        public List<int> LineN0List;
        /// <summary> 选择圆的编号</summary>
        public List<int> CircleN0List;

        public St_DotParam( int LocModel =0 )
        {
            DotLocModel = LocModel;
            LineN0List = new List<int>();
            CircleN0List = new List<int>();
            switch (LocModel)
            {
                case 0: //
                    LineN0List.Add(0);
                    LineN0List.Add(1);
                    break;
                case 1:
                    CircleN0List.Add(0);
                    break;
                case 2:
                    CircleN0List.Add(0);
                    CircleN0List.Add(1);
                    break;
                case 3:
                    LineN0List.Add(0);
                    LineN0List.Add(1);
                    LineN0List.Add(2);
                    break;
                case 4:
                    LineN0List.Add(0);
                    LineN0List.Add(1);
                    LineN0List.Add(2);
                    break;
                case 5:
                    LineN0List.Add(0);
                    LineN0List.Add(1);
                    LineN0List.Add(2);
                    LineN0List.Add(3);
                    break;
            }
        }
    }

    /// <summary> 定位量角度Angle的定位参数 </summary>
    public struct St_AngleParam
    {
        /// <summary> 角度定位模式  -1表示没有示教，0表示直线定位角度，1表示两个圆心定位角度  </summary>
        public int AngleLocModel;
        /// <summary>选择线的编号 </summary>
        public int LineN0;
        /// <summary> 选择圆的编号</summary>
        public int[] CircleNo;
        /// <summary> 补偿角度 </summary>
        public double AddAngle;
        public St_AngleParam(int LocModel)
        {
            AngleLocModel = LocModel;
            LineN0 = 0;
            CircleNo = new int[2];
            CircleNo[0] = 0;
            CircleNo[1] = 1;
            AddAngle = 0;
            switch (LocModel)
            {
                case 0:
                    LineN0 = 0;
                    break;
                case 1:
                    CircleNo[0] = 0;
                    CircleNo[1] = 1;
                    AddAngle = 0;
                    break;
            }
        }
    }

    /// <summary> 目标点相对坐标原点偏移参数 </summary>
    public struct St_PosDesignMsg
    {
        /// <summary> 相对坐标原点的偏移向量</summary>
        public St_VectorAngle vectorAngle;
        /// <summary> 单像素对应的空间尺寸 </summary>
        public double PixelSize;
        public St_PosDesignMsg(bool IsInit = true)
        {
            vectorAngle = new St_VectorAngle();
            PixelSize = 0.02;
        }
    }

    /// <summary> 坐标定位定位参数 </summary>
    public struct St_VectorLocParam
    {
        /// <summary>定位参数，X.Y的定位参数   </summary>
        public St_DotParam DotParam;
        /// <summary>定位参数，角度Angle的定位参数   </summary>
        public St_AngleParam AngleParam;
        public St_VectorLocParam(int DotLocModel=0, int AngleLocModel=0)
        {
            DotParam = new St_DotParam(DotLocModel);
            AngleParam = new St_AngleParam(AngleLocModel);
        }
    }

    public struct St_OffSetDetectParam
    {
        /// <summary> 坐标原点或者参考点的定位参数  </summary>
        public St_VectorLocParam OriLocParam;
        /// <summary> 目标点或者芯片中心点的定位参数 </summary>
        public St_VectorLocParam TargetPointParam;
        /// <summary> 目标点相对坐标原点的理论偏移量 </summary>
        public St_PosDesignMsg PosDesignMsg;
        public St_OffSetDetectParam(int OriDotLocModel = 0, int OriAngleLocModel = 0,int TargetPointDotLocModel = 0, int TargetPointAngleLocModel = 0)
        {
            OriLocParam = new St_VectorLocParam(OriDotLocModel, OriAngleLocModel);
            TargetPointParam = new St_VectorLocParam(TargetPointDotLocModel, TargetPointAngleLocModel);
            PosDesignMsg = new St_PosDesignMsg();
        } 
    }



    public struct St_CaliMarksParam
    {
        public double CenterX;
        public double CenterY;
        public int TapeDetectX;  ///检测下相机胶带漏贴ROI
        public int TapeDetectY;
        public int TapeDetectWidth;
        public int TapeDetectHeight;
        public double MeanGray;
        public double Deviation;
        public St_CirclesParam CircleParam;

        public St_CaliMarksParam(bool isInit = true)
        {
            CenterX = 0;
            CenterY = 0;
            TapeDetectX = 0;
            TapeDetectY = 0;
            TapeDetectWidth = 0;
            TapeDetectHeight = 0;
            MeanGray = 0;
            Deviation = 0;
            CircleParam = new St_CirclesParam(isInit);
        }
    }
}
