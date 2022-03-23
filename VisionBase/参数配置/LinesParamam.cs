using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Security.Permissions;
using System.Threading;

namespace VisionBase
{
    public struct St_LinesParam
    {
        /// <summary>
        /// 对3#Feeder变形检测处理一条线
        /// </summary>
        //public bool IsFindOneLine;
        public int Count;   //示教了几条直线
        public List<double> Row1s;
        public List<double> Col1s ;
        public List<double> Row2s ;
        public List<double> Col2s;
        public List<double> Elements ;
        public List<double> DetectHeights;
        public List<double> Thresholds;
        public List<double> CenterClips;
		public double OffSetPixelX;
        public double OffSetPixelY;
        public double OffSetPixelTh;
        public St_LinesParam(int isInitCount=3)
        {
            Count = isInitCount;
            Row1s = new List<double>();
            Col1s = new List<double>();
            Row2s = new List<double>();
            Col2s = new List<double>();
            Elements = new List<double>();
            DetectHeights = new List<double>();
            Thresholds = new List<double>();
            CenterClips = new List<double>();
            OffSetPixelX =0;
            OffSetPixelY = 0;
            OffSetPixelTh = 0;
            //IsFindOneLine = false;
            for (int i = 0; i < Count; i++)
            {
                Row1s.Add(0);
                Col1s.Add(0);
                Row2s.Add(0);
                Col2s.Add(0);
                Elements.Add(32);
                DetectHeights.Add(50);
                Thresholds.Add(10);
                CenterClips.Add(0);
            }
        }


        public bool GetLineParam(int Index, out double LineRow1Out, out double LineCol1Out, out double LineRow2Out, out double LineCol2Out
                                  ,out double ElementOut,out double HeightOut)
        {
            LineRow1Out = 0;
            LineCol1Out = 0;
            LineRow2Out = 0;
            LineCol2Out = 0;
            ElementOut = 0;
            HeightOut = 0;
            if (Index < Count)
            {
                LineRow1Out = Row1s[Index];
                LineCol1Out = Col1s[Index];
                LineRow2Out = Row2s[Index];
                LineCol2Out = Col2s[Index];
                ElementOut = Elements[Index];
                HeightOut = DetectHeights[Index];
                return true;
            }
            return false;
        }
        public bool GetLineParam(int Index, out double LineRow1Out, out double LineCol1Out, out double LineRow2Out, out double LineCol2Out
                          , out double ElementOut, out double HeightOut,out double ThresholdOut)
        {
            LineRow1Out = 0;
            LineCol1Out = 0;
            LineRow2Out = 0;
            LineCol2Out = 0;
            ElementOut = 0;
            HeightOut = 0;
            ThresholdOut = 0;
            if (Index < Count)
            {
                LineRow1Out = Row1s[Index];
                LineCol1Out = Col1s[Index];
                LineRow2Out = Row2s[Index];
                LineCol2Out = Col2s[Index];
                ElementOut = Elements[Index];
                HeightOut = DetectHeights[Index];
                ThresholdOut = Thresholds[Index];
                return true;
            }
            return false;
        }

        public bool SetParam(int Index,double Row1,double Col1 ,double Row2,double Col2,double Element,double DetectHeight,double Threshold )
        {
            if (Index < Count)
            {
                Row1s[Index] = Row1;
                Col1s[Index] = Col1;
                Row2s[Index] = Row2;
                Col2s[Index] = Col2;
                Elements[Index] = Element;
                DetectHeights[Index] =DetectHeight;
                Thresholds[Index] = Threshold;
                return true;
            }
            return false;
        }
    }

    public struct St_InspectPinParam
    {

        public int Count;   //有几行Pin针
        public List<double> Row1s;
        public List<double> Col1s;
        public List<double> Row2s;
        public List<double> Col2s;
        public List<double> Elements;
        public List<double> DetectHeights;
        public List<double> DetectWidths;
        public List<double> DnThresholds;
        public List<double> UpThresholds;
        public List<double> MinAreas;
        public List<double> MaxAreas;
        public List<List<double>> ListRows;
        public List<List<double>> ListCols;
        public St_InspectPinParam(int LinNum=2, bool IsInit = true)
        {
            Count = LinNum;
            Row1s = new List<double>();
            Col1s = new List<double>();
            Row2s = new List<double>();
            Col2s = new List<double>();
            Elements = new List<double>();
            DetectHeights = new List<double>();
            DetectWidths = new List<double>();
            DnThresholds = new List<double>();
            UpThresholds = new List<double>();
            ListRows = new List<List<double>>();
            ListCols = new List<List<double>>();
            MinAreas = new List<double>();
            MaxAreas = new List<double>();
            for (int i = 0; i < Count; i++)
            {
                Row1s.Add(0);
                Col1s.Add(0);
                Row2s.Add(0);
                Col2s.Add(0);
                Elements.Add(10);
                DetectWidths.Add(20);
                DetectHeights.Add(20);
                MinAreas.Add(10);
                MaxAreas.Add(1000);
                DnThresholds.Add(50);
                UpThresholds.Add(255);
                List<double> iRows = new List<double>();
                List<double> iCols = new List<double>();
                for (int j = 0; j < Elements[i]; j++)
                {
                    iRows.Add(0);
                    iCols.Add(0);             
                }
                ListRows.Add(iRows);
                ListCols.Add(iCols);              
            }
        }
    }

    public struct St_BlobInSpectParam
    {
        /// <summary> 检测的区域数量 </summary>
        public int Count;
        /// <summary> 检测区域 </summary>
        public List< RectangleF> InspectRois;
        /// <summary> 阈值分割的最小灰度， 默认值50，根据实际情况调整</summary>
        public List<int>  MinGrays;
        /// <summary> 阈值分割的最大灰度， 默认值200，根据实际情况调整</summary>
        public List<int> MaxGrays;
        /// <summary> 阈值分割出来的最小面积， 默认值100，根据实际情况调整</summary>
        public List<int> AreaMins;
        /// <summary> 阈值分割出来的最大面积， 默认值9999999，根据实际情况调整</summary>
        public double PixelSize;

        public List<int> AreaMaxs;
        /// <summary> 初始化</summary>
        /// <param name="RoiNum"> </param>
        /// <param name="IsInit"> </param>
        public St_BlobInSpectParam(int RoiNum =1, bool IsInit = true)
        {
            Count = RoiNum;
            PixelSize = 1.0;
            InspectRois = new List<RectangleF>();
            MinGrays = new List<int>();
            MaxGrays = new List<int>();
            AreaMins = new List<int>();
            AreaMaxs = new List<int>();
            for (int i = 0; i < Count; i++)
            {
                InspectRois.Add(new RectangleF(0, 0, 100, 100));
                MinGrays.Add(50);
                MaxGrays.Add(200);
                AreaMins.Add(100);
                AreaMaxs.Add(999999);
            }
        }
        public bool SetRoi(int Index,RectangleF RectFIn)
        {
            if (Index > Count + 1) return false;
            InspectRois[Index] = RectFIn;
            return true;
        }
        public bool SetMinGray(int Index, int GrayIn)
        {
            if (Index > Count + 1) return false;
            MinGrays[Index] = GrayIn;
            return true;
        }
        public bool SetMaxGray(int Index, int GrayIn)
        {
            if (Index > Count + 1) return false;
            MaxGrays[Index] = GrayIn;
            return true;
        }
        public bool SetAreaMax(int Index, int AreaIn)
        {
            if (Index > Count + 1) return false;
            AreaMaxs[Index] = AreaIn;
            return true;
        }
        public bool SetAreaMin(int Index, int AreaIn)
        {
            if (Index > Count + 1) return false;
            AreaMins[Index] = AreaIn;
            return true;
        }
    }

    /// <summary>
    /// 斑点分析，用标准的产品
    /// </summary>
    public struct St_BlobInspectParam1
    {

        /// <summary> 检测的区域数量 </summary>
        public int Count;
        /// <summary> 检测区域 </summary>
        public List<RectangleF> InspectRois;
        /// <summary> 阈值分割的最小灰度， 默认值50，根据实际情况调整</summary>
        public List<int> MinGrays;
        /// <summary> 阈值分割的最大灰度， 默认值200，根据实际情况调整</summary>
        public List<int> MaxGrays;
        /// <summary> 阈值分割出来的最小面积， 默认值100，根据实际情况调整</summary>
        public List<int> AreaMins;
        /// <summary> 阈值分割出来的最大面积， 默认值9999999，根据实际情况调整</summary>
        public List<int> AreaMaxs;

        /// <summary>自动提取点胶区域的轮廓坐标 ，用于生成标准胶水区域</summary>
        public List<List<double>> ListRows;
        /// <summary> 自动提取点胶区域的轮廓坐标 </summary>
        public List<List<double>> ListCols;

        public List<List<double>> ListHoleRows;
        public List<List<double>> ListHoleCols;


        public double PixelSize;
        /// <summary> 瑕疵面积最小值 </summary>
        public List<int> FlawAreaMins;
        /// <summary> 瑕疵面积最大值 </summary>
        public List<int> FlawAreaMaxs;

        public double DilationR;
        public double ErosionR;


        public St_BlobInspectParam1(int RoiNum = 1, bool IsInit = true)
        {
            Count = RoiNum;
            PixelSize = 1.0;
            InspectRois = new List<RectangleF>();
            MinGrays = new List<int>();
            MaxGrays = new List<int>();
            AreaMins = new List<int>();
            AreaMaxs = new List<int>();
            ListRows = new List<List<double>>();
            ListCols = new List<List<double>>();
            ListHoleRows = new List<List<double>>();
            ListHoleCols = new List<List<double>>();
            FlawAreaMins = new List<int>();
            FlawAreaMaxs = new List<int>();
            DilationR = 3.5;
            ErosionR = 3.5;

            for (int i = 0; i < Count; i++)
            {
                InspectRois.Add(new RectangleF(0, 0, 100, 100));
                MinGrays.Add(50);
                MaxGrays.Add(200);
                AreaMins.Add(100);
                AreaMaxs.Add(999999);
                ListRows.Add(new List<double>());
                ListCols.Add(new List<double>());
                ListHoleRows.Add(new List<double>());
                ListHoleCols.Add(new List<double>());
                FlawAreaMins.Add(100);
                FlawAreaMaxs.Add(999999);
            }
        }


    }



    public struct St_BlobLocalParam
    {
        /// <summary> 检测的区域数量 </summary>
        public int Count;
        /// <summary> 检测区域 </summary>
        public List<RectangleF> InspectRois;
        /// <summary> 阈值分割的最小灰度， 默认值50，根据实际情况调整</summary>
        public List<int> MinGrays;
        /// <summary> 阈值分割的最大灰度， 默认值200，根据实际情况调整</summary>
        public List<int> MaxGrays;
        /// <summary> 阈值分割出来的最小面积， 默认值100，根据实际情况调整</summary>
        public List<int> AreaMins;
        /// <summary> 阈值分割出来的最大面积， 默认值9999999，根据实际情况调整</summary>
        public double PixelSize;

        public List<int> AreaMaxs;
        /// <summary> 初始化</summary>
        /// <param name="RoiNum"> </param>
        /// <param name="IsInit"> </param>
        public St_BlobLocalParam(int RoiNum = 1, bool IsInit = true)
        {
            Count = RoiNum;
            PixelSize = 1.0;
            InspectRois = new List<RectangleF>();
            MinGrays = new List<int>();
            MaxGrays = new List<int>();
            AreaMins = new List<int>();
            AreaMaxs = new List<int>();
            for (int i = 0; i < Count; i++)
            {
                InspectRois.Add(new RectangleF(0, 0, 100, 100));
                MinGrays.Add(50);
                MaxGrays.Add(200);
                AreaMins.Add(100);
                AreaMaxs.Add(999999);
            }
        }
        public bool SetRoi(int Index, RectangleF RectFIn)
        {
            if (Index > Count + 1) return false;
            InspectRois[Index] = RectFIn;
            return true;
        }
        public bool SetMinGray(int Index, int GrayIn)
        {
            if (Index > Count + 1) return false;
            MinGrays[Index] = GrayIn;
            return true;
        }
        public bool SetMaxGray(int Index, int GrayIn)
        {
            if (Index > Count + 1) return false;
            MaxGrays[Index] = GrayIn;
            return true;
        }
        public bool SetAreaMax(int Index, int AreaIn)
        {
            if (Index > Count + 1) return false;
            AreaMaxs[Index] = AreaIn;
            return true;
        }
        public bool SetAreaMin(int Index, int AreaIn)
        {
            if (Index > Count + 1) return false;
            AreaMins[Index] = AreaIn;
            return true;
        }
    }

    public struct St_CCDProcessResult
    {
        /// <summary>
        /// 视觉返回结果
        /// </summary>
        public En_ImageProcessResult Result;
        /// <summary>
        /// 最终位置
        /// </summary>
        public Point3Db XYRPos;
        public Point3Db OneLinePos;
        /// <summary>
        /// 胶带偏移量
        /// </summary>
        public Point3Db TapeOffset;
        public int TapeIndex;
    }

    public struct St_InsepctLinePara
    {
        public int Count;   //示教了几条直线
        public List<double> Row1s;
        public List<double> Col1s;
        public List<double> Row2s;
        public List<double> Col2s;
        public List<double> Thresholds;
        public List<double> Elements;
        public List<double> DetectHeights;
        public List<double> DetectWidths;
        
        public List<List<double>> DetectRegionGrays;
        public St_InsepctLinePara(int lineNum, bool isInit = true)
        {
            Count = lineNum;
            Row1s = new List<double>();
            Col1s = new List<double>();
            Row2s = new List<double>();
            Col2s = new List<double>();
            Thresholds = new List<double>();
            Elements = new List<double>();
            DetectHeights = new List<double>();
            DetectWidths = new List<double>(); ;
            DetectRegionGrays = new List<List<double>>();
            for (int i = 0; i < lineNum; i++)
            {
                Row1s.Add(0);
                Col1s.Add(0);
                Row2s.Add(0);
                Col2s.Add(0);
                Thresholds.Add(20);
                Elements.Add(10);
                DetectHeights.Add(10);
                DetectWidths.Add(10);
                List<double> LineGray = new List<double>();
                for (int j = 0; j < Elements[i]; j++)
                {
                    LineGray.Add(0);
                }
                DetectRegionGrays.Add(LineGray);
            }
        }

    }

    public struct St_InspectRectanglePara
    {
        public int Count; //示教了几个矩形
        public List<double> Rows;
        public List<double> Cols;
        public List<double> Phis;
        public List<double> Widths;
        public List<double> Heights;
        public List<double> MeanGrays; //标准图片的平均灰度
        public List<double> DivGrays;//示教区域的灰度的分布范围
        public List<double> GrayRanges;//标准区域示教示教区域的灰度范围
        public List<double> NgAreas;   //NG的面积阈值
        public List<double> DarkGrayRanges;

        public St_InspectRectanglePara(int LineNum, bool isInit = true)
        {
            Count = LineNum;
            Rows = new List<double>();
            Cols = new List<double>();
            Phis = new List<double>();
            Widths = new List<double>();
            Heights = new List<double>();
            MeanGrays = new List<double>();
            DivGrays = new List<double>();
            GrayRanges = new List<double>();
            NgAreas = new List<double>();
            DarkGrayRanges = new List<double>();
            for (int i = 0; i < LineNum; i++)
            {
                Rows.Add(0);
                Cols.Add(0);
                Phis.Add(0);
                Widths.Add(0);
                Heights.Add(0);
                MeanGrays.Add(0);
                DivGrays.Add(0);
                GrayRanges.Add(5);
                NgAreas.Add(0);
                DarkGrayRanges.Add(5);
            }
        }

    }

    public struct St_InspectCirclePara
    {
        public int Count;   //示教了几个圆
        public List<List<double>> ListRows;
        public List<List<double>> ListCols;
        public List<string> Directs;
        public List<double> AddRs;
        public List<double> Elements;
        public List<double> DetectHeights;
        public List<double> DetectWidths;
        public List<double> Thresholds;
        public List<List<double>> DetectRegionGrays;
        public St_InspectCirclePara(int lineNum, bool isInit = true)
        {
            Count = lineNum;
            ListRows = new List<List<double>>();
            ListCols = new List<List<double>>();
            Directs = new List<string>();
            AddRs = new List<double>();
            Elements = new List<double>();
            DetectHeights = new List<double>();
            DetectWidths = new List<double>();
            DetectRegionGrays = new List<List<double>>();
            Thresholds = new List<double>();
            for (int i = 0; i < lineNum; i++) {
                AddRs.Add(0);
                Elements.Add(10);
                DetectHeights.Add(10);
                DetectWidths.Add(10);
                Thresholds.Add(20);
                List<double> LineGray = new List<double>();
                for (int j = 0; j < Elements[i]; j++){
                    LineGray.Add(0);
                }
                DetectRegionGrays.Add(LineGray);
                List<double> Rows = new List<double>();
                List<double> Cols = new List<double>();
                ListRows.Add(Rows);
                ListCols.Add(Cols);
                Directs.Add("");
            }
        }
    }
    public struct St_InspectImageSetting
    {
        public St_LinesParam LinePara;                //直线定位的参数
        public St_CirclesParam CirclePara;            //圆定位的参数
        public St_VectorAngle VectorAngle0;//示教时定位XYR
        public St_VectorAngle NowVectorAngle;//实际当前图片的位置XYR
        public St_InsepctLinePara InspectLinePara;     //直线的检测参数
        public St_InspectCirclePara InspectCirclePara; //圆弧的检测参数 
        public St_InspectRectanglePara InspectRectPara;
        public St_InspectImageSetting(bool isInit = true)
        {
            LinePara = new St_LinesParam(4);
            CirclePara = new St_CirclesParam(true);
            VectorAngle0 = new St_VectorAngle(true);
            NowVectorAngle = new St_VectorAngle(true);
            InspectLinePara = new St_InsepctLinePara(2, true);
            InspectCirclePara = new St_InspectCirclePara(2, true);
            InspectRectPara = new St_InspectRectanglePara(1, true);
        }
    }



}
