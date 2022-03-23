using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
   public static class PinInsepct
    {
       public static bool InspectPinPos(St_InspectPinParam  InspectPinParamIn , HObject ImgIn ,out HTuple OffSetRows,out HTuple OffSetCols,out HTuple OffSetDist,out  HObject PinContour ,out  HObject ErrorContour )
       {
           //1.0获得最新图片PIN针的中心坐标
           OffSetRows = new HTuple();
           OffSetCols = new HTuple();
           OffSetDist = new HTuple();
           ErrorContour = new HObject();
           PinContour = new HObject();
           #region //定义参数
           if (InspectPinParamIn.Elements.Count() == 0|| InspectPinParamIn.Elements==null ) return false;
           double Element = InspectPinParamIn.Elements[0];
           double DetectHight = InspectPinParamIn.DetectHeights[0];
           double DtetectWidth = InspectPinParamIn.DetectWidths[0];
           double LineRow1 = InspectPinParamIn.Row1s[0];
           double LineCol1 = InspectPinParamIn.Col1s[0];
           double LineRow2 = InspectPinParamIn.Row2s[0];
           double LineCol2 = InspectPinParamIn.Col2s[0];
           HObject DetectRegions = new HObject();
           HObject RoiContour = new HObject();
           double DnThreshold = InspectPinParamIn.DnThresholds[0];
           double UpThreshold = InspectPinParamIn.UpThresholds[0];
           double MinArea = InspectPinParamIn.MinAreas[0];
           double MaxArea = InspectPinParamIn.MaxAreas[0];
           #endregion

           HTuple PinRows = new HTuple(), PinCols = new HTuple();
           for (int i = 0; i < InspectPinParamIn.Elements.Count(); i++)
           {
               #region 参数赋值
               Element = InspectPinParamIn.Elements[i];
               DetectHight = InspectPinParamIn.DetectHeights[i];
               DtetectWidth = InspectPinParamIn.DetectWidths[i];
               LineRow1 = InspectPinParamIn.Row1s[i];
               LineCol1 = InspectPinParamIn.Col1s[i];
               LineRow2 = InspectPinParamIn.Row2s[i];
               LineCol2 = InspectPinParamIn.Col2s[i];
               DnThreshold = InspectPinParamIn.DnThresholds[i];
               UpThreshold = InspectPinParamIn.UpThresholds[i];
               MinArea = InspectPinParamIn.MinAreas[i];
               MaxArea = InspectPinParamIn.MaxAreas[i];
               #endregion
               //1.1 生成单行Pin针ROI
               MyVisionBase.gen_rake_ROI1(ImgIn, out DetectRegions, out RoiContour, Element, DetectHight,
                                             DtetectWidth, LineRow1, LineCol1, LineRow2, LineCol2);
               HObject ReduceImg;
               HTuple area = new HTuple(), row = new HTuple(), col = new HTuple();
               HObject CircleObj = new HObject();
               HOperatorSet.GenCircle(out CircleObj, 10, 10, 5);
               #region 找出每行Pin针的中心坐标
               for (int j = 0; j < DetectRegions.CountObj(); j++)  //计算每根针的中心
               {
                   HObject RegionI = new HObject();
                   HOperatorSet.CopyObj(DetectRegions, out RegionI, j + 1, 1);
                   HOperatorSet.ReduceDomain(ImgIn, RegionI, out ReduceImg); RegionI.Dispose();  //获取目标区域的图像
                   HObject ThrdRegion;
                   HOperatorSet.Threshold(ReduceImg, out ThrdRegion, DnThreshold, UpThreshold); ReduceImg.Dispose();  //阈值分割
                   HObject ExpandRegion = new HObject();
                   HOperatorSet.ExpandRegion(ThrdRegion, CircleObj, out ExpandRegion, 3, "image");  //膨胀
                   ThrdRegion.Dispose(); ;
                   HObject ConnectRegion;
                   HOperatorSet.Connection(ExpandRegion, out  ConnectRegion); ExpandRegion.Dispose();  //链接
                   HObject SelectRegion = new HObject();
                   HOperatorSet.SelectShape(ConnectRegion, out SelectRegion, "area", "and", MinArea, MaxArea); ConnectRegion.Dispose();  //挑出目标区域
                   HTuple areaI, RowI, ColI;
                   HOperatorSet.AreaCenter(SelectRegion, out areaI, out  RowI, out ColI); //获取Pin针中心坐标
                   if (RowI.Length > 0) {
                       HTuple MaxValue = areaI.TupleMax();
                       int MaxIndex = areaI.TupleFind(MaxValue);
                       row[j] = RowI[MaxIndex].D;
                       col[j] = ColI[MaxIndex].D;
                   }
                   else
                   {
                       row[j] = 0;
                       col[j] = 0;
                   }
               }
               #endregion
                PinRows = PinRows.TupleConcat(row);
                PinCols = PinCols.TupleConcat(col);
                CircleObj.Dispose();         
           }
           HTuple TeachPinRows = new HTuple(), TeachPinCols = new HTuple();
           for (int i = 0; i < InspectPinParamIn.Elements.Count(); i++)
           {
               HTuple row0 = new HTuple(), col0 = new HTuple();
               MyVisionBase.ListToHTuple(InspectPinParamIn.ListRows[i], out row0);
               MyVisionBase.ListToHTuple(InspectPinParamIn.ListCols[i], out col0);
               TeachPinRows = TeachPinRows.TupleConcat(row0);
               TeachPinCols = TeachPinCols.TupleConcat(col0); 
           }
           HTuple HomMat =new HTuple();
           HOperatorSet.VectorToHomMat2d(TeachPinCols, TeachPinRows, PinCols, PinRows, out  HomMat); //计算平移矩阵
           HTuple TargetRows =new HTuple(),TargetCols =new HTuple();
           HOperatorSet.AffineTransPixel(HomMat, TeachPinCols, TeachPinRows, out TargetCols, out TargetRows); //平移示教点的坐标
           if (TargetRows.Length != TeachPinRows.Length) return false;
           HTuple OffSetRows0 = new HTuple(), OffSetCols0 = new HTuple(),OffSetDist0 =new HTuple();
           OffSetCols0 = PinCols - TargetCols;
           OffSetRows0 = PinRows - TargetRows;
           HOperatorSet.DistancePp(TargetRows, TargetCols, PinRows, PinCols, out  OffSetDist0); //计算出偏移量
           HTuple Max = OffSetDist0.TupleMax();
           HTuple MaxIndex0 = OffSetDist0.TupleFind(Max);
          
           HOperatorSet.GenCircleContourXld(out ErrorContour, PinRows[MaxIndex0[0].I], PinCols[MaxIndex0[0].I], 50, 0, Math.PI * 2, "positive", 1.0);

           HOperatorSet.GenCrossContourXld(out PinContour, PinRows, PinCols, 50, 0);


           OffSetRows = OffSetRows0;
           OffSetCols = OffSetCols0;
           OffSetDist = OffSetDist0;
           return true;
       }
       /// <summary>
       /// 计算出所有Pin针的坐标
       /// </summary>
       /// <param name="InspectPinParamIn"></param>
       /// <param name="ImgIn"></param>
       /// <param name="PinRows"></param>
       /// <param name="PinCols"></param>
       /// <returns></returns>
       public static bool FindPinPos(St_InspectPinParam InspectPinParamIn, HObject ImgIn, out HTuple PinRows, out HTuple PinCols)
       {
           PinRows = new HTuple();
           PinCols = new HTuple();
           //1.0获得最新图片PIN针的中心坐标
           #region //定义参数
           if (InspectPinParamIn.Elements.Count() == 0 || InspectPinParamIn.Elements == null) return false;
           double Element = InspectPinParamIn.Elements[0];
           double DetectHight = InspectPinParamIn.DetectHeights[0];
           double DtetectWidth = InspectPinParamIn.DetectWidths[0];
           double LineRow1 = InspectPinParamIn.Row1s[0];
           double LineCol1 = InspectPinParamIn.Col1s[0];
           double LineRow2 = InspectPinParamIn.Row2s[0];
           double LineCol2 = InspectPinParamIn.Col2s[0];
           HObject DetectRegions = new HObject();
           HObject RoiContour = new HObject();
           double DnThreshold = InspectPinParamIn.DnThresholds[0];
           double UpThreshold = InspectPinParamIn.UpThresholds[0];
           double MinArea = InspectPinParamIn.MinAreas[0];
           double MaxArea = InspectPinParamIn.MaxAreas[0];
           #endregion

           HTuple PinRows0 = new HTuple(), PinCols0 = new HTuple();
           for (int i = 0; i < InspectPinParamIn.Elements.Count(); i++)
           {
               #region 参数赋值
               Element = InspectPinParamIn.Elements[i];
               DetectHight = InspectPinParamIn.DetectHeights[i];
               DtetectWidth = InspectPinParamIn.DetectWidths[i];
               LineRow1 = InspectPinParamIn.Row1s[i];
               LineCol1 = InspectPinParamIn.Col1s[i];
               LineRow2 = InspectPinParamIn.Row2s[i];
               LineCol2 = InspectPinParamIn.Col2s[i];
               DnThreshold = InspectPinParamIn.DnThresholds[i];
               UpThreshold = InspectPinParamIn.UpThresholds[i];
               MinArea = InspectPinParamIn.MinAreas[i];
               MaxArea = InspectPinParamIn.MaxAreas[i];
               #endregion
               //1.1 生成单行Pin针ROI
               MyVisionBase.gen_rake_ROI1(ImgIn, out DetectRegions, out RoiContour, Element, DetectHight,
                                             DtetectWidth, LineRow1, LineCol1, LineRow2, LineCol2);
               HObject ReduceImg;
               HTuple area = new HTuple(), row = new HTuple(), col = new HTuple();
               HObject CircleObj = new HObject();
               HOperatorSet.GenCircle(out CircleObj, 10, 10, 5);
               #region 找出每行Pin针的中心坐标
               for (int j = 0; j < DetectRegions.CountObj(); j++)  //计算每根针的中心
               {
                   HObject RegionI = new HObject();
                   HOperatorSet.CopyObj(DetectRegions, out RegionI, j + 1, 1);
                   HOperatorSet.ReduceDomain(ImgIn, RegionI, out ReduceImg); RegionI.Dispose();  //获取目标区域的图像
                   HObject ThrdRegion;
                   HOperatorSet.Threshold(ReduceImg, out ThrdRegion, DnThreshold, UpThreshold); ReduceImg.Dispose();  //阈值分割
                   HObject ExpandRegion = new HObject();
                   HOperatorSet.ExpandRegion(ThrdRegion, CircleObj, out ExpandRegion, 3, "image"); ThrdRegion.Dispose();  //膨胀      
                   HObject ConnectRegion;
                   HOperatorSet.Connection(ExpandRegion, out  ConnectRegion); ExpandRegion.Dispose();  //链接
                   HObject SelectRegion = new HObject();
                   HOperatorSet.SelectShape(ConnectRegion, out SelectRegion, "area", "and", MinArea, MaxArea); ConnectRegion.Dispose();  //挑出目标区域
                   HTuple areaI, RowI, ColI;
                   HOperatorSet.AreaCenter(SelectRegion, out areaI, out  RowI, out ColI); //获取Pin针中心坐标
                   if (RowI.Length > 0)
                   {
                       HTuple MaxValue = areaI.TupleMax();
                       int MaxIndex = areaI.TupleFind(MaxValue);
                       row[j] = RowI[MaxIndex].D;
                       col[j] = ColI[MaxIndex].D;
                   }
                   else
                   {
                       row[j] = 0;
                       col[j] = 0;
                   }
               }
               #endregion
               PinRows0 = PinRows0.TupleConcat(row);
               PinCols0 = PinCols0.TupleConcat(col);
               CircleObj.Dispose();
           }
           PinRows = PinRows0;
           PinCols = PinCols0;
           return true;
       }
       /// <summary>
       /// 计算出每个Pin针的偏移量
       /// </summary>
       /// <param name="InspectPinParamIn"></param>
       /// <param name="PinRows"></param>
       /// <param name="PinCols"></param>
       /// <param name="OffSetRows"></param>
       /// <param name="OffSetCols"></param>
       /// <param name="OffSetDist"></param>
       /// <returns></returns>
       public static bool CalculatePinOffset(St_InspectPinParam InspectPinParamIn, HTuple PinRows, HTuple PinCols, out HTuple OffSetRows, out HTuple OffSetCols, out HTuple OffSetDist)
       {
           OffSetRows = new HTuple();
           OffSetCols = new HTuple();
           OffSetDist = new HTuple();
           HTuple TeachPinRows = new HTuple(), TeachPinCols = new HTuple();
           for (int i = 0; i < InspectPinParamIn.Elements.Count(); i++)
           {
               HTuple row0 = new HTuple(), col0 = new HTuple();
               MyVisionBase.ListToHTuple(InspectPinParamIn.ListRows[i], out row0);
               MyVisionBase.ListToHTuple(InspectPinParamIn.ListCols[i], out col0);
               TeachPinRows = TeachPinRows.TupleConcat(row0);
               TeachPinCols = TeachPinCols.TupleConcat(col0);
           }
           HTuple HomMat = new HTuple();
           HOperatorSet.VectorToHomMat2d(TeachPinCols, TeachPinRows, PinCols, PinRows, out  HomMat); //计算平移矩阵
           HTuple TargetRows = new HTuple(), TargetCols = new HTuple();
           HOperatorSet.AffineTransPixel(HomMat, TeachPinCols, TeachPinRows, out TargetCols, out TargetRows); //平移示教点的坐标
           if (TargetRows.Length != TeachPinRows.Length) return false;
           HTuple OffSetRows0 = new HTuple(), OffSetCols0 = new HTuple(), OffSetDist0 = new HTuple();
           OffSetCols0 = PinCols - TargetCols;
           OffSetRows0 = PinRows - TargetRows;
           HOperatorSet.DistancePp(TargetRows, TargetCols, PinRows, PinCols, out  OffSetDist0); //计算出偏移量
           OffSetRows = OffSetRows0;
           OffSetCols = OffSetCols0;
           OffSetDist = OffSetDist0;
           return true;
       }
    }
}
