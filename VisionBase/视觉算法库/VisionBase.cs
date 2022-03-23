using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace VisionBase
{
   public static  class MyVisionBase
    {
       public static object DispLockObj = new object();

       public static void AdjImgRow( HObject Img ,ref HTuple Rows )
       {
           HTuple ImgH = new HTuple();
           HTuple ImgW =new HTuple();
           HOperatorSet.GetImageSize(Img, out ImgW, out ImgH);
           for (int i = 0; i < Rows.TupleLength(); i++)
           {
               Rows[i] = ImgH - Rows[i];
           }    
       }

        public static void AdjImgRow(HObject Img, ref double  Row)
        {
            HTuple ImgH = new HTuple();
            HTuple ImgW = new HTuple();
            HOperatorSet.GetImageSize(Img, out ImgW, out ImgH);
            Row = ImgH.D - Row;

        }

        /// <summary>
        /// 找出半径为R的亮区域，
        /// </summary>
        /// <param name="Img"></param>
        /// <param name="R">半径</param>
        /// <param name="MinGray">最小灰度</param>
        /// <param name="MaxGray">最大灰度</param>
        /// <param name="Rows">区域的行坐标</param>
        /// <param name="Cols">区域的列坐标</param>
        /// <param name="CenterCross"></param>
        /// <param name="Cof">允许的误差范围0.5-0.9,0.9表示面积的误差在0.9-1.1</param>
        public static void RegionCenter(HObject Img ,double R ,double MinGray ,double MaxGray,out List<double> Rows ,out List<double> Cols,out HObject CenterCross, double Cof =0.8)
       {
           Rows = new List<double>();
           Cols = new List<double>();
           CenterCross = new HObject();
           HObject Region =new HObject(),ConnectRegion =new HObject(),SelectedRegion =new HObject();
           HOperatorSet.Threshold(Img, out Region ,MinGray,MaxGray);
           HOperatorSet.Connection(Region,out  ConnectRegion);
           HOperatorSet.SelectShape(ConnectRegion, out SelectedRegion, "area", "and", Math.PI * R * R * Cof, Math.PI * R * R * (2 - Cof));
           HTuple Areas = new HTuple(), HRows = new HTuple(), HCols = new HTuple();
           HOperatorSet.AreaCenter(SelectedRegion, out Areas,out  HRows,out  HCols);
           HOperatorSet.GenCrossContourXld(out CenterCross,HRows,HCols,12,0.718);
           AdjImgRow(Img, ref HRows);
           for (int i = 0; i < HCols.TupleLength(); i++)
           {
               Cols.Add(HCols[i].D);
               Rows.Add(HRows[i].D);         
           }
       }



        #region   上相机与机械坐标的手眼标定部分

        #endregion
       /// <summary>
       /// 九点标定
       /// </summary>
       /// <param name="Px"></param>
       /// <param name="Py"></param>
       /// <param name="Qx"></param>
       /// <param name="Qy"></param>
       /// <param name="HomMat2D00"></param>
       public static void Calibra9(HTuple Px, HTuple Py, HTuple Qx, HTuple Qy, out HTuple HomMat2D00)
       {
           HTuple HomMat2D = new HTuple();
           HOperatorSet.HomMat2dIdentity(out HomMat2D);
           HOperatorSet.VectorToHomMat2d(Px, Py, Qx, Qy, out  HomMat2D);
           HTuple OutQx = new HTuple(), OutQy = new HTuple();
           HOperatorSet.AffineTransPoint2d(HomMat2D, Px, Py, out OutQx, out OutQy);
           HTuple DistTest = new HTuple();
           HOperatorSet.DistancePp(Qx, Qy, OutQx, OutQy, out  DistTest);
           HTuple MaxDist = new HTuple();
           HOperatorSet.TupleMax(DistTest, out MaxDist);
           if (DistTest < 0.2)
           {
               MessageBox.Show("标定成功，最大的标定误差为: " + MaxDist.ToString());
               HomMat2D00 = HomMat2D;
           }
           else
           {
               MessageBox.Show("标定成功，最大的标定误差为: " + MaxDist.ToString());
               HOperatorSet.HomMat2dIdentity(out HomMat2D00);
           }
       }

        /// <summary>
        /// 相似 九点标定
        /// </summary>
        /// <param name="Px"></param>
        /// <param name="Py"></param>
        /// <param name="Qx"></param>
        /// <param name="Qy"></param>
        /// <param name="HomMat2D00"></param>
        public static void Calibra9PtSimilar(HTuple Px, HTuple Py, HTuple Qx, HTuple Qy, out HTuple HomMat2D00)
        {
            HTuple HomMat2D = new HTuple();
            HOperatorSet.HomMat2dIdentity(out HomMat2D);
            HOperatorSet.VectorToHomMat2d(Px, Py, Qx, Qy, out HomMat2D);
            HTuple OutQx = new HTuple(), OutQy = new HTuple();
            HOperatorSet.AffineTransPoint2d(HomMat2D, Px, Py, out OutQx, out OutQy);
            HTuple DistTest = new HTuple();
            HOperatorSet.DistancePp(Qx, Qy, OutQx, OutQy, out DistTest);
            HTuple MaxDist = new HTuple();
            HOperatorSet.TupleMax(DistTest, out MaxDist);
            if (DistTest < 0.2)
            {
                MessageBox.Show("标定成功，最大的标定误差为: " + MaxDist.ToString());
                HomMat2D00 = HomMat2D;
            }
            else
            {
                MessageBox.Show("标定成功，最大的标定误差为: " + MaxDist.ToString());
                HOperatorSet.HomMat2dIdentity(out HomMat2D00);
            }
        }

        /// <summary>
        /// 根据向量角度求平台的平移量和旋转量
        /// </summary>
        /// <param name="VectorAngle0">金手指坐标角度</param>
        /// <param name="VectorAngle1">胶带的坐标角度</param>
        /// <param name="VectorAngleOut">贴胶的坐标角度</param>
        public static void VectorAngleToMotionXYTh(VectorAngle VectorAngle0, VectorAngle VectorAngle1, out VectorAngle VectorAngleOut)
       {
           VectorAngleOut = new VectorAngle();
           HTuple HomMat = new HTuple();
           HOperatorSet.HomMat2dIdentity(out HomMat);
           HOperatorSet.VectorAngleToRigid(VectorAngle0.Y, VectorAngle0.X, VectorAngle0.Angle, VectorAngle1.Y, VectorAngle1.X, VectorAngle1.Angle, out HomMat);  //根据坐标角度计算仿射变换矩阵
           HTuple Sx = new HTuple(), Sy = new HTuple(), Phi = new HTuple(), Theta = new HTuple(), Tx = new HTuple(), Ty = new HTuple();
           HOperatorSet.HomMat2dToAffinePar(HomMat, out Sx, out Sy, out Phi, out Theta, out Tx, out Ty);//根据仿射变换矩阵计算平台的XYU轴的平移量
           VectorAngleOut.Y = Tx;
           VectorAngleOut.X = Ty;
           VectorAngleOut.Angle = Theta;       
       }

        public static bool BitMapToHobject8(Bitmap bmp, out HObject Image)
        {
            try
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData srcBmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                HOperatorSet.GenImage1(out Image, "byte", bmp.Width, bmp.Height, srcBmpData.Scan0);
                bmp.UnlockBits(srcBmpData); }
            catch{
                Image = null;
                return false;
            }
            return true;
        }

        public static void VectorAngleToMotionXYTh(St_VectorAngle VectorAngle0, St_VectorAngle VectorAngle1, out St_VectorAngle VectorAngleOut)
        {
            VectorAngleOut = new St_VectorAngle();
            HTuple HomMat = new HTuple();
            HOperatorSet.HomMat2dIdentity(out HomMat);
            HOperatorSet.VectorAngleToRigid(VectorAngle0.Col, VectorAngle0.Row,  VectorAngle0.Angle, VectorAngle1.Col, VectorAngle1.Row,  VectorAngle1.Angle, out HomMat);  //根据坐标角度计算仿射变换矩阵
            HTuple Sx = new HTuple(), Sy = new HTuple(), Phi = new HTuple(), Theta = new HTuple(), Tx = new HTuple(), Ty = new HTuple();
            HOperatorSet.HomMat2dToAffinePar(HomMat, out Sx, out Sy, out Phi, out Theta, out Tx, out Ty);//根据仿射变换矩阵计算平台的XYU轴的平移量
            VectorAngleOut.Col = Tx;
            VectorAngleOut.Row = Ty;
            VectorAngleOut.Angle = Theta;
        }

        #region  //手眼标定的函数部分

        /// <summary>
        /// 拟合圆
        /// </summary>
        /// <param name="ho_Circle">                   拟合得到的圆                                          </param>
        /// <param name="ViewIn">                  显示窗口                                                      </param>
        /// <param name="hv_Rows">                     边界点行坐标                                          </param>
        /// <param name="hv_Cols">                     边界点列坐标                                          </param>
        /// <param name="hv_ActiveNum">输入点的个数限制，大于1，输入点的个数小于hv_ActiveNum时不进行直线拟合 </param>
        /// <param name="hv_ArcType">         圆弧类型，返回是圆"circle"还是圆弧“arc”                      </param>
        /// <param name="hv_RowCenter">               拟合圆的圆心行坐标                                     </param>
        /// <param name="hv_ColCenter">               拟合圆的圆心列坐标                                     </param>
        /// <param name="hv_Radius">                  拟合圆的半径                                           </param>
        /// <param name="hv_StartPhi">                拟合圆弧的起始角度                                     </param>
        /// <param name="hv_EndPhi">                  拟合圆弧的终止角度                                     </param>          
        /// <param name="hv_PointOrder"></param>
        /// <param name="hv_Flag">                  是否拟合成功，成功Flag=1，成功Flag=0；                   </param>
        public static bool  PtsToBestCircle(out HObject ho_Circle, ViewControl ViewIn, HTuple hv_Rows, HTuple hv_Cols, HTuple hv_ActiveNum, HTuple hv_ArcType, out HTuple hv_RowCenter,
                                          out HTuple hv_ColCenter, out HTuple hv_Radius, out HTuple hv_StartPhi, out HTuple hv_EndPhi, out HTuple hv_PointOrder, out HTuple hv_Flag)
       {
           #region
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_Regions, ho_Contour = null;

           // Local control variables 
           HTuple hv_Length, hv_Length1 = new HTuple();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Circle);
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_Contour);
           hv_StartPhi = new HTuple();
           hv_EndPhi = new HTuple();
           hv_PointOrder = new HTuple();
           hv_Flag = new HTuple();
           //初始化
           hv_RowCenter = 0;
           hv_ColCenter = 0;
           hv_Radius = 0;
           //产生一个空的对象，用来保存拟合后的圆
           ho_Regions.Dispose();
           HOperatorSet.GenEmptyObj(out ho_Regions);
           //计算边缘数量
           try
           {
               HOperatorSet.TupleLength(hv_Cols, out hv_Length);
               //当边缘数量不小于有效点数时进行拟合
               if ((int)((new HTuple(hv_Length.TupleGreaterEqual(hv_ActiveNum))).TupleAnd(new HTuple(hv_ActiveNum.TupleGreaterEqual(2)))) != 0)
               {
                   //Halcon的拟合是基于xld的，需要先把边缘拼接成xld
                   if ((int)(new HTuple(hv_ArcType.TupleEqual("circle"))) != 0)
                   {
                       //如果是闭合的圆，轮廓需要首尾相接
                       ho_Contour.Dispose();
                       HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows.TupleConcat(hv_Rows.TupleSelect(0)), hv_Cols.TupleConcat(hv_Cols.TupleSelect(0)));
                   }
                   else
                   {
                       ho_Contour.Dispose();
                       HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows, hv_Cols);
                   }
                   //拟合圆
                   HTuple ClipPtNum = new HTuple();
                   ClipPtNum = ((hv_Rows.TupleLength()) / 10);                 
                    ViewIn.AddViewObject(ho_Contour);
                   //HOperatorSet.FitCircleContourXld(ho_Coutour, "ahuber", -1, 0, 0, 3, 2, out hv_RowCenter, out hv_ColCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                   HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowCenter, out  hv_ColCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                   //判断拟合结果是够有效，如果拟合成功，数组中的元素数量大于0
                   HOperatorSet.TupleLength(hv_StartPhi, out hv_Length1);
                   if ((int)(new HTuple(hv_Length1.TupleLess(1))) != 0)
                   {
                       ho_Regions.Dispose();
                       ho_Contour.Dispose();
                       return false;
                   }
                   //根据拟合结果，产生xld
                   if ((int)(new HTuple(hv_ArcType.TupleEqual("arc"))) != 0)
                   {
                       ho_Circle.Dispose();
                       HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);
                   }
                   else
                   {
                       hv_StartPhi = 0;
                       hv_EndPhi = (new HTuple(360)).TupleRad();
                       ho_Circle.Dispose();
                       HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);
                   }
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Circle, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //拟合圆成功
                   hv_Flag = 1;
               }
               else
               {
                    ViewIn.SetString(12, 50, "red", "检测的边缘点太少，不够用来拟合圆");
                   //HOperatorSet.SetColor(ShowWindow, "red");
                   //HOperatorSet.WriteString(ShowWindow, "检测的边缘点太少，不够用来拟合圆");
                   //拟合圆失败
                   hv_Flag = 0;
               }
               ho_Regions.Dispose();
               ho_Contour.Dispose();
               return true;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_Regions.Dispose();
               ho_Contour.Dispose();
               Logger.PopError(HDevExpDefaultException.Message+HDevExpDefaultException.Source  );
               return false;
           }
           #endregion
       }

       public static void PtsToBestCircle1(out HObject ho_Circle, HTuple hv_Rows, HTuple hv_Cols, HTuple hv_ActiveNum, HTuple hv_ArcType, out HTuple hv_RowCenter,
                                   out HTuple hv_ColCenter, out HTuple hv_Radius, out HTuple hv_StartPhi, out HTuple hv_EndPhi,  out HTuple hv_Flag)
       {
           #region
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_Regions, ho_Contour = null;
           // Local control variables 
           HTuple hv_Length, hv_Length1 = new HTuple();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Circle);
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_Contour);
           hv_StartPhi = new HTuple();
           hv_EndPhi = new HTuple();
           HTuple  hv_PointOrder = new HTuple();
           hv_Flag = new HTuple();
           //string strLog = "Row=\r\n";
           //strLog += hv_Rows.ToString();
           //strLog += "hv_Cols=\r\n";
           //strLog += hv_Cols.ToString();
           //strLog += "hv_ActiveNum=\r\n";
           //strLog += hv_ActiveNum.ToString();
           //Logger.Pop1(strLog, "PtsToBestCircle1");
           //strLog = "结果：\r\n";

           try
           {
               //初始化
               hv_RowCenter = 0;
               hv_ColCenter = 0;
               hv_Radius = 0;
               //产生一个空的对象，用来保存拟合后的圆
               ho_Regions.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Regions);
               //计算边缘数量
               HOperatorSet.TupleLength(hv_Cols, out hv_Length);
               //当边缘数量不小于有效点数时进行拟合
               if ((int)((new HTuple(hv_Length.TupleGreaterEqual(hv_ActiveNum))).TupleAnd(new HTuple(hv_ActiveNum.TupleGreaterEqual(2)))) != 0)
               {
                   //Halcon的拟合是基于xld的，需要先把边缘拼接成xld
                   if ((int)(new HTuple(hv_ArcType.TupleEqual("circle"))) != 0)
                   {
                       //如果是闭合的圆，轮廓需要首尾相接
                       ho_Contour.Dispose();
                       HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows.TupleConcat(hv_Rows.TupleSelect(0)), hv_Cols.TupleConcat(hv_Cols.TupleSelect(0)));
                   }
                   else
                   {
                       ho_Contour.Dispose();
                       HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows, hv_Cols);
                   }
                   //拟合圆
                   HTuple ClipPtNum = new HTuple();
                   ClipPtNum = ((hv_Rows.TupleLength()) /10);
                   HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 5, ClipPtNum, 5, 3, out hv_RowCenter, out hv_ColCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                   //判断拟合结果是够有效，如果拟合成功，数组中的元素数量大于0
                   HOperatorSet.TupleLength(hv_StartPhi, out hv_Length1);
                   if ((int)(new HTuple(hv_Length1.TupleLess(1))) != 0)
                   {
                       ho_Regions.Dispose();
                       ho_Contour.Dispose();
                       return;
                   }
                   //根据拟合结果，产生xld
                   if ((int)(new HTuple(hv_ArcType.TupleEqual("arc"))) != 0)
                   {
                       ho_Circle.Dispose();
                       HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);
                   }
                   else
                   {
                       hv_StartPhi = 0;
                       hv_EndPhi = (new HTuple(360)).TupleRad();
                       ho_Circle.Dispose();
                       HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);
                   }
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Circle, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //拟合圆成功
                   hv_Flag = 1;
               }
               else
               {
                   //disp_message(ShowWindow, "检测的边缘点太少，不够用来拟合圆", "window", 12, 50, "red", "true");
                   //HOperatorSet.SetColor(ShowWindow, "red");
                   //HOperatorSet.WriteString(ShowWindow, "检测的边缘点太少，不够用来拟合圆");
                   //拟合圆失败
                   hv_Flag = 0;
               }

               //strLog += hv_ColCenter.ToString() + "\r\n";
               //strLog += hv_RowCenter.ToString() + "\r\n";
               //strLog += hv_Radius.ToString() + "\r\n";
               //Logger.Pop1(strLog, "PtsToBestCircle1");
               ho_Regions.Dispose();
               ho_Contour.Dispose();
               return;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_Regions.Dispose();
               ho_Contour.Dispose();
               throw HDevExpDefaultException;
           }
           #endregion
       }

       public static void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem, HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
       {
           #region
           // Local control variables 

           HTuple hv_Red, hv_Green, hv_Blue, hv_Row1Part;
           HTuple hv_Column1Part, hv_Row2Part, hv_Column2Part, hv_RowWin;
           HTuple hv_ColumnWin, hv_WidthWin = new HTuple(), hv_HeightWin;
           HTuple hv_MaxAscent, hv_MaxDescent, hv_MaxWidth, hv_MaxHeight;
           HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
           HTuple hv_FactorColumn = new HTuple(), hv_Width = new HTuple();
           HTuple hv_Index = new HTuple(), hv_Ascent = new HTuple(), hv_Descent = new HTuple();
           HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
           HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
           HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple(), hv_Exception = new HTuple();
           HTuple hv_CurrentColor = new HTuple();

           HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
           HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
           HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
           HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

           // Initialize local and output iconic variables 

           //This procedure displays text in a graphics window.
           //
           //Input parameters:
           //WindowHandle: The WindowHandle of the graphics window, where
           //   the message should be displayed
           //String: A tuple of strings containing the text message to be displayed
           //CoordSystem: If set to 'window', the text position is given
           //   with respect to the window coordinate system.
           //   If set to 'image', image coordinates are used.
           //   (This may be useful in zoomed images.)
           //Row: The row coordinate of the desired text position
           //   If set to -1, a default value of 12 is used.
           //Column: The column coordinate of the desired text position
           //   If set to -1, a default value of 12 is used.
           //Color: defines the color of the text as string.
           //   If set to [], '' or 'auto' the currently set color is used.
           //   If a tuple of strings is passed, the colors are used cyclically
           //   for each new textline.
           //Box: If set to 'true', the text is written within a white box.
           //
           //prepare window
           lock (DispLockObj)
           {
               HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
               HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part, out hv_Column2Part);
               HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin, out hv_WidthWin, out hv_HeightWin);
               HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
           }
           //
           //default settings
           if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
           {
               hv_Row_COPY_INP_TMP = 12;
           }
           if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
           {
               hv_Column_COPY_INP_TMP = 12;
           }
           if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
           {
               hv_Color_COPY_INP_TMP = "green";
           }
           //
           hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
           //
           //Estimate extentions of text depending on font size.
           lock (DispLockObj)
           {
               HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent, out hv_MaxWidth, out hv_MaxHeight);
           }
           if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
           {
               hv_R1 = hv_Row_COPY_INP_TMP.Clone();
               hv_C1 = hv_Column_COPY_INP_TMP.Clone();
           }
           else
           {
               //transform image to window coordinates
               hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
               hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
               hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
               hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
           }
           //
           //display text box depending on text size
           if ((int)(new HTuple(hv_Box.TupleEqual("true"))) != 0)
           {
               //calculate box extents
               hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
               hv_Width = new HTuple();
               for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
               {
                   lock (DispLockObj)
                   {
                       HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                   }
                   hv_Width = hv_Width.TupleConcat(hv_W);
               }
               hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()));
               hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
               hv_R2 = hv_R1 + hv_FrameHeight;
               hv_C2 = hv_C1 + hv_FrameWidth;
               //display rectangles
               lock (DispLockObj)
               {
                   HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                   HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                   HOperatorSet.SetColor(hv_WindowHandle, "light gray");
                   HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 3, hv_C1 + 3, hv_R2 + 3, hv_C2 + 3);
                   HOperatorSet.SetColor(hv_WindowHandle, "white");
                   HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                   HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
               }
           }
           else if ((int)(new HTuple(hv_Box.TupleNotEqual("false"))) != 0)
           {
               hv_Exception = "Wrong value of control parameter Box";
               throw new HalconException(hv_Exception);
           }
           //Write text.
           for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
           {
               hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength())));
               if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual("auto")))) != 0)
               {
                   lock (DispLockObj)
                   {
                       HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                   }
               }
               else
               {
                   lock (DispLockObj)
                   {
                       HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                   }
               }
               hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
               lock (DispLockObj)
               {
                   HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                   HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(hv_Index));
               }
           }
           //reset changed window settings
           lock (DispLockObj)
           {
               HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
               HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part, hv_Column2Part);
           }
           return;
           #endregion
       }
       /// <summary>
       /// 九点标定，四点标定，多点标定，求坐标系之间的仿射变换矩阵
       /// </summary>
       /// <param name="Pt2D1">像素坐标 </param>
       /// <param name="Pt2D2">机械中手坐标</param>
       /// <param name="OutHomMat2d">手眼标定矩阵</param>
       /// <param name="IsTrue">是否标定成功</param>
       /// 

       public static void DrawRectangle1(HWindow ShowWindow, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2)
       {
           row1 = 0;
           column1 = 0;
           row2 = 100;
           column2 = 100;
           lock (DispLockObj)
           {
               HOperatorSet.DrawRectangle1(ShowWindow, out row1, out column1, out row2, out column2);
           } 
       }
        public static void DrawRectangle11(HWindow ShowWindow, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2)
        {
            row1 = 0;
            column1 = 0;
            row2 = 100;
            column2 = 100;
            lock (DispLockObj)
            {             
                HOperatorSet.DrawRectangle1Mod(ShowWindow,100,100,200,200, out row1, out column1, out row2, out column2);
            }
        }

        public static void VectorToHomMat(List<Point2Db> Pt2D1, List<Point2Db> Pt2D2, out MyHomMat2D OutHomMat2d, out bool IsTrue)
       {
           IsTrue = false;
           OutHomMat2d = new MyHomMat2D();
           HTuple Px = new HTuple(), Py = new HTuple(), Qx = new HTuple(), Qy = new HTuple();
           if (Pt2D1.Count != Pt2D2.Count)
           { 
               IsTrue = false;
               return;
           }
           for (int i = 0; i < Pt2D1.Count(); i++)
           {
               Px[i] = Pt2D1[i].Col;
               Py[i] = Pt2D1[i].Row;            
               Qx[i] = Pt2D2[i].Col;
               Qy[i] = Pt2D2[i].Row;
           }
           HTuple NowHomMat = new HTuple();
           //  HOperatorSet.VectorToHomMat2d(Px, Py, Qx, Qy, out  NowHomMat);
            //HOperatorSet.VectorToRigid(Px, Py, Qx, Qy, out NowHomMat);
            HOperatorSet.VectorToHomMat2d(Px, Py, Qx, Qy, out NowHomMat);

            HTuple OutQx = new HTuple(), OutQy = new HTuple();
            HOperatorSet.AffineTransPoint2d(NowHomMat, Px, Py, out OutQx, out OutQy);
            HTuple DistTest = new HTuple();
            HOperatorSet.DistancePp(Qx, Qy, OutQx, OutQy, out DistTest);
            HTuple MaxDist = new HTuple();
            HOperatorSet.TupleMax(DistTest, out MaxDist);
            if (DistTest < 0.2)
            {
                MessageBox.Show("标定成功，最大的标定误差为: " + MaxDist.ToString());
                
            }
            else
            {
                MessageBox.Show("标定失败，最大的标定误差为: " + MaxDist.ToString());
                
            }


            //Calibra9(Px, Py, Qx, Qy, out  NowHomMat);
            HalconToMyHomMat(NowHomMat, out OutHomMat2d);
       
       }
        public static void VectorToRigidHomMat(List<Point2Db> Pt2D1, List<Point2Db> Pt2D2, out MyHomMat2D OutHomMat2d, out bool IsTrue)
        {
            IsTrue = false;
            OutHomMat2d = new MyHomMat2D();
            HTuple Px = new HTuple(), Py = new HTuple(), Qx = new HTuple(), Qy = new HTuple();
            if (Pt2D1.Count != Pt2D2.Count)
            {
                IsTrue = false;
                return;
            }
            for (int i = 0; i < Pt2D1.Count(); i++)
            {
                Px[i] = Pt2D1[i].Col;
                Py[i] = Pt2D1[i].Row;
                Qx[i] = Pt2D2[i].Col;
                Qy[i] = Pt2D2[i].Row;
            }
            HTuple NowHomMat = new HTuple();
            //  HOperatorSet.VectorToHomMat2d(Px, Py, Qx, Qy, out  NowHomMat);
            HOperatorSet.VectorToRigid(Px, Py, Qx, Qy, out NowHomMat);
            // HOperatorSet.VectorToHomMat2d(Px, Py, Qx, Qy, out NowHomMat);
            //Calibra9(Px, Py, Qx, Qy, out  NowHomMat);
            HalconToMyHomMat(NowHomMat, out OutHomMat2d);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductPos1">产品Pos1相对旋转中心的坐标</param>
        /// <param name="ProductPos2">产品Pos2相对旋转中心的坐标</param>
        /// <param name="TapePos1"> 胶带Pos1在世界坐标系中的坐标 </param>
        /// <param name="TapePos2">胶带Pos2在世界坐标系中的坐标</param>
        /// <param name="Wx">平台需要移动到的X轴坐标位置</param>
        /// <param name="Wy">平台需要移动到的Y轴坐标位置</param>
        /// <param name="Wtheta">平台需要移动到的R轴的角度，相对于示教角度，而非绝对角度</param>
        /// <returns></returns>
        public static bool CalculateThreeTapePos(Point2Db ProductPos2, Point2Db ProductPos1, Point2Db TapePos2, Point2Db TapePos1, out double Wx, out double Wy, out double Wtheta)
        {
            Wx = 0;
            Wy = 0;
            Wtheta = 0;
            try
            {
                HTuple HvTapeCenterX, HvTapeCenterY, HvTapeCenterTheta;
                HTuple HvProductCenterX, HvProductCenterY, HvProductCenterTheta;
                HTuple RotAngle = new HTuple();
                HOperatorSet.AngleLl(ProductPos1.Col, ProductPos1.Row, ProductPos2.Col, ProductPos2.Row, TapePos1.Col, TapePos1.Row, TapePos2.Col, TapePos2.Row, out RotAngle);
                // Wtheta = RotAngle.D/Math.PI *180;
                double Wtheta0 = RotAngle.D / Math.PI * 180;
                //产品与X轴的夹角，产品的中心坐标，相对旋转中心
                HOperatorSet.AngleLl(0, 0, 1, 0, ProductPos1.Col, ProductPos1.Row, ProductPos2.Col, ProductPos2.Row, out HvProductCenterTheta);
                 HvProductCenterX = (ProductPos1.Col + ProductPos2.Col) / 2;
                HvProductCenterY = (ProductPos1.Row + ProductPos2.Row) / 2;
                //胶带与X轴的夹角 ，胶带的中心坐标
                HOperatorSet.AngleLl(0, 0, 1, 0, TapePos1.Col, TapePos1.Row, TapePos2.Col, TapePos2.Row, out HvTapeCenterTheta);
                HvTapeCenterX = (TapePos1.Col + TapePos2.Col) / 2;
                HvTapeCenterY = (TapePos1.Row + TapePos2.Row) / 2;
                double Wtheta2 = (HvTapeCenterTheta.D - HvProductCenterTheta.D) / Math.PI * 180;
                //计算平台需要移动到的坐标位置
                LineTypePos.CalculateMotionPos1(HvProductCenterX.D, HvProductCenterY.D, HvProductCenterTheta.D, HvTapeCenterX.D, HvTapeCenterY.D, HvTapeCenterTheta.D, out Wx, out Wy, out Wtheta);
                Wtheta = Wtheta * 180 / (Math.PI);
            }
            catch (Exception e0)
            {
                Wx = 0;
                Wy = 0;
                Wtheta = 0;
                Logger.PopError(e0.Message + "3号头贴胶坐标计算错误");
                return false;
            }
            return true;
        }


        public static bool CalculateTwoPtPos(Point2Db ProductPos2, Point2Db ProductPos1, Point2Db TapePos2, Point2Db TapePos1, 
           double OffsetX,double OffsetY,double OffseTheta, out double Wx, out double Wy, out double Wtheta)
        {
            Wx = 0;
            Wy = 0;
            Wtheta = 0;
            try
            {
                HTuple HvTapeCenterX, HvTapeCenterY, HvTapeCenterTheta;
                HTuple HvProductCenterX, HvProductCenterY, HvProductCenterTheta;
                HTuple RotAngle = new HTuple();
                HOperatorSet.AngleLl(ProductPos1.Col, ProductPos1.Row, ProductPos2.Col, ProductPos2.Row, 
                    TapePos1.Col, TapePos1.Row, TapePos2.Col, TapePos2.Row, out RotAngle);
                // Wtheta = RotAngle.D/Math.PI *180;
                double Wtheta0 = RotAngle.D / Math.PI * 180;
                //产品与X轴的夹角，产品的中心坐标，相对旋转中心
                HOperatorSet.AngleLl(0, 0, 1, 0, ProductPos1.Col, ProductPos1.Row, ProductPos2.Col, ProductPos2.Row, 
                    out HvProductCenterTheta);
                HvProductCenterX = (ProductPos1.Col + ProductPos2.Col) / 2;
                HvProductCenterY = (ProductPos1.Row + ProductPos2.Row) / 2;
                //胶带与X轴的夹角 ，胶带的中心坐标
                HOperatorSet.AngleLl(0, 0, 1, 0, TapePos1.Col, TapePos1.Row, TapePos2.Col, TapePos2.Row, out HvTapeCenterTheta);
                HvTapeCenterX = (TapePos1.Col + TapePos2.Col) / 2;
                HvTapeCenterY = (TapePos1.Row + TapePos2.Row) / 2;
                double Wtheta2 = (HvTapeCenterTheta.D - HvProductCenterTheta.D) / Math.PI * 180;
                //计算平台需要移动到的坐标位置
                LineTypePos.CalculateMotionPos1(HvProductCenterX.D, HvProductCenterY.D, HvProductCenterTheta.D, HvTapeCenterX.D+ OffsetX,
                    HvTapeCenterY.D+ OffsetY, HvTapeCenterTheta.D+ OffseTheta, out Wx, out Wy, out Wtheta);
                Wtheta = Wtheta * 180.0 / (Math.PI);
            }
            catch (Exception e0)
            {
                Wx = 0;
                Wy = 0;
                Wtheta = 0;
                Logger.PopError(e0.Message + "3号头贴胶坐标计算错误");
                return false;
            }
            return true;
        }





        #endregion   //手眼标定的函数部分结束

        public static void GetRegionArea(HObject srcImg, HTuple row1, HTuple column1, HTuple row2, HTuple column2, out double mean, out double deviation)
       {
           HRegion roi = new HRegion(row1,column1,row2,column2);
           HTuple meanGray,devi;
           mean = 0;
           deviation = 0;
           try
           {
               HOperatorSet.Intensity(roi, srcImg, out meanGray, out devi);     
               mean = meanGray.D;
               deviation = devi.D;
           }
           catch (Exception ex)
           {
               Logger.PopError(ex.Message.ToString());
           }
           roi.Dispose();
       }

       public static bool CheckIsHaveTape(HObject srcImg, int x, double y, int width, int height, double srcMean,out double actualMean)
       {
           HRegion roi = new HRegion(y, x, y + height, x + width);
           HTuple devi, meanGray;
           actualMean = 0;

           try
           {
               HOperatorSet.Intensity(roi, srcImg, out meanGray, out devi);
               //Console.WriteLine("灰度值：{0}", meanGray);
               actualMean = meanGray.D;
               return (int)meanGray.D < srcMean;
           }
           catch (Exception ex)
           {
               Logger.PopError(ex.Message.ToString());
               return false;
           }
       }

       public static  void HalconToMyHomMat(HTuple HtupleHomMat, out MyHomMat2D OutMyHomMat)
       {
           OutMyHomMat = new MyHomMat2D();
           OutMyHomMat.c00 = HtupleHomMat[0].D;
           OutMyHomMat.c01 = HtupleHomMat[1].D;
           OutMyHomMat.c02 = HtupleHomMat[2].D;
           OutMyHomMat.c10 = HtupleHomMat[3].D;
           OutMyHomMat.c11 = HtupleHomMat[4].D;
           OutMyHomMat.c12 = HtupleHomMat[5].D;     
       }
       public static void MyHomMatToHalcon(MyHomMat2D InMyHomMat, out  HTuple HtupleHomMat)
       {
           HtupleHomMat = new HTuple();
           HtupleHomMat[0] = InMyHomMat.c00;
           HtupleHomMat[1] = InMyHomMat.c01;
           HtupleHomMat[2] = InMyHomMat.c02;
           HtupleHomMat[3] = InMyHomMat.c10;
           HtupleHomMat[4] = InMyHomMat.c11;
           HtupleHomMat[5] = InMyHomMat.c12;     
       }
       /// <summary>
       /// 找出拟合圆的边界点
       /// </summary>
       /// <param name="ho_Image"> 输入图像</param>
       /// <param name="hv_Elements"></param>
       /// <param name="hv_DetectHeight"></param>
       /// <param name="hv_DetectWidth"></param>
       /// <param name="hv_Sigma"></param>
       /// <param name="hv_Threshold"></param>
       /// <param name="hv_Transition">"all","positive","negetive"</param>
       /// <param name="hv_Select">"first","all","last"</param>
       /// <param name="hv_ROIRows">"示教圆的外轮廓点，行坐标，4点"</param>
       /// <param name="hv_ROICols">"示教圆的外轮廓点，列坐标，4点"</param>
       /// <param name="hv_Direct">方向"inner","outer"</param>
       /// <param name="hv_ResultRow">找到的边界拟合点行坐标</param>
       /// <param name="hv_ResultColumn">找到的边界拟合点列坐标</param>
       /// <param name="hv_ArcType">圆弧类型"circle"，"arc"</param>
       public static void  spoke2(HObject ho_Image, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Sigma, HTuple hv_Threshold, HTuple hv_Transition,
                        HTuple hv_Select, HTuple hv_ROIRows, HTuple hv_ROICols, HTuple hv_Direct, out HTuple hv_ResultRow, out HTuple hv_ResultColumn, out HTuple hv_ArcType)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 

           HObject ho_Regions, ho_Contour, ho_ContCircle;
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_RowC, hv_ColumnC;
           HTuple hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder;
           HTuple hv_RowXLD, hv_ColXLD, hv_Length2, hv_i, hv_j = new HTuple();
           HTuple hv_RowE = new HTuple(), hv_ColE = new HTuple(), hv_ATan = new HTuple();
           HTuple hv_MsrHandle_Measure = new HTuple(), hv_RowEdge = new HTuple();
           HTuple hv_ColEdge = new HTuple(), hv_Amplitude = new HTuple();
           HTuple hv_Distance = new HTuple(), hv_tRow = new HTuple();
           HTuple hv_tCol = new HTuple(), hv_t = new HTuple(), hv_Number = new HTuple();
           HTuple hv_ROICols_COPY_INP_TMP = hv_ROICols.Clone();
           HTuple hv_ROIRows_COPY_INP_TMP = hv_ROIRows.Clone();
           HTuple hv_Select_COPY_INP_TMP = hv_Select.Clone();
           HTuple hv_Transition_COPY_INP_TMP = hv_Transition.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_Contour);
           HOperatorSet.GenEmptyObj(out ho_ContCircle);
           hv_ArcType = new HTuple();
           try
           {
               HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
               //1.产生一个现实对象用于显示
               ho_Regions.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Regions);
               //2.初始化边缘数组
               hv_ResultRow = new HTuple();
               hv_ResultColumn = new HTuple();
               //3.产生xld
               ho_Contour.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows_COPY_INP_TMP, hv_ROICols_COPY_INP_TMP);
               //用回归线法（不抛出异常点，所有点的权重一样）拟合圆
               HOperatorSet.FitCircleContourXld(ho_Contour, "atukey", -1, 0, 0, 3, 2, out hv_RowC, out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
               //根据拟合结果产生xld，并保持到显示图像
               ho_ContCircle.Dispose();
               HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
               OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
               SP_O++;
               ho_Regions.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ContCircle, out ho_Regions);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
               //获取圆或圆弧xld上的点坐标
               HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);
               HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
               //判断检测边缘的数量是否过少
               if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
               {
                   hv_ROIRows_COPY_INP_TMP = new HTuple();
                   hv_ROICols_COPY_INP_TMP = new HTuple();
                   ho_Regions.Dispose();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   return;
               }
               //如果XLD是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
               //如果XLD是圆，有Length2个点，以0度为起点，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
               for (hv_i = 0; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
               {
                   if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(hv_Length2 - 1)))) != 0)
                   {
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / hv_Elements) * hv_i, out hv_j); //xld的起点和中点坐标相同，为圆
                   }
                   else
                   {
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i, out hv_j); //否则为圆弧
                   }
                   //索引越界，强制赋值为最后一个索引
                   if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                   {
                       hv_j = hv_Length2 - 1;
                       continue;
                   }
                   //获取卡尺工具中心
                   hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                   hv_ColE = hv_ColXLD.TupleSelect(hv_j);
                   //超出图像区域不检测，否则容易报异常
                   if ((int)((new HTuple((new HTuple((new HTuple(hv_RowE.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowE.TupleLess(0))))).TupleOr(new HTuple(hv_ColE.TupleGreater(
                       hv_Width - 1))))).TupleOr(new HTuple(hv_ColE.TupleLess(0)))) != 0)
                   {
                       continue;
                   }
                   //边缘搜索方向类型：'inner'搜索方向由圆外指向圆心：‘outer’搜索方向由圆心指向圆外
                   if ((int)(new HTuple(hv_Direct.TupleEqual("inner"))) != 0)
                   {
                       //求卡尺工具的边缘搜索方向
                       //求圆心指向边缘的矢量角度
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                       //角度反向
                       hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                   }
                   else
                   {
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                   }
                   //产生测量对象句柄
                   HOperatorSet.GenMeasureRectangle2(hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2, hv_Width, hv_Height, "nearest_neighbor", out hv_MsrHandle_Measure);
                   if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("negative"))) != 0)
                   {
                       hv_Transition_COPY_INP_TMP = "negative";
                   }
                   else
                   {
                       if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("positive"))) != 0)
                       {
                           hv_Transition_COPY_INP_TMP = "positive";
                       }
                       else
                       {
                           hv_Transition_COPY_INP_TMP = "all";
                       }
                   }
                   //设置边缘位置。最强点是从所有边缘中选择幅值绝对值最大点，需要设置为'all'
                   if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("first"))) != 0)
                   {
                       hv_Select_COPY_INP_TMP = "first";
                   }
                   else
                   {
                       if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("last"))) != 0)
                       {
                           hv_Select_COPY_INP_TMP = "last";
                       }
                       else
                       {
                           hv_Select_COPY_INP_TMP = "all";
                       }
                   }
                   HOperatorSet.MeasurePos(ho_Image, hv_MsrHandle_Measure, hv_Sigma, hv_Threshold, hv_Transition_COPY_INP_TMP, hv_Select_COPY_INP_TMP, out hv_RowEdge, out hv_ColEdge, out hv_Amplitude, out hv_Distance);
                   HOperatorSet.CloseMeasure(hv_MsrHandle_Measure);
                   hv_tRow = 0;
                   hv_tCol = 0;
                   //保存边缘幅度绝对值
                   hv_t = 0;
                   //找到的边缘至少为1个
                   HOperatorSet.TupleLength(hv_RowEdge, out hv_Number);
                   if ((int)(new HTuple(hv_Number.TupleLess(1))) != 0)
                   {
                       continue;
                   }
                   //有多个边缘时，选择幅度最大的边缘
                   for (hv_j = 0; hv_j.Continue(hv_Number - 1, 1); hv_j = hv_j.TupleAdd(1))
                   {
                       if ((int)(new HTuple(((((hv_Amplitude.TupleSelect(hv_j))).TupleAbs())).TupleGreater(
                           hv_t))) != 0)
                       {
                           hv_tRow = hv_RowEdge.TupleSelect(hv_j);
                           hv_tCol = hv_ColEdge.TupleSelect(hv_j);
                           hv_t = ((hv_Amplitude.TupleSelect(hv_j))).TupleAbs();
                       }
                   }
                   //把找到的边缘保存到输出数组
                   if ((int)(new HTuple(hv_t.TupleGreater(0))) != 0)
                   {
                       hv_ResultRow = hv_ResultRow.TupleConcat(hv_tRow);
                       hv_ResultColumn = hv_ResultColumn.TupleConcat(hv_tCol);
                   }
               }
               ho_Regions.Dispose();
               ho_Contour.Dispose();
               ho_ContCircle.Dispose();
               return;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_Regions.Dispose();
               ho_Contour.Dispose();
               ho_ContCircle.Dispose();
               throw HDevExpDefaultException;
           }
       }
       /// <summary>
       /// HTuple转 ST_Pt2D
       /// </summary>
       /// <param name="Rows"></param>
       /// <param name="Cols"></param>
       /// <param name="Pt2Ds"></param>
       public static void HTupleToPt2D(HTuple Rows, HTuple Cols, out List<Point2Db> Pt2Ds)
       {
           Pt2Ds = new List<Point2Db>();
           if (Rows.TupleLength() != Cols.TupleLength())
           {
               MessageBox.Show("HtupleToPt2D()函数中,Rows的数组长度和Cols数组长队不等。");
           }
           for (int i = 0; i < Rows.TupleLength(); i++)
           {
               Point2Db PtI = new Point2Db();
               PtI.Row = Rows[i];
               PtI.Col = Cols[i];
               Pt2Ds.Add(PtI);
           }      
       }
       /// <summary>
       /// Pt2D转HTuple
       /// </summary>
       /// <param name="Pt2Ds"></param>
       /// <param name="Rows"></param>
       /// <param name="Cols"></param>
       public static void Pt2dToHTuple(List<Point2Db> Pt2Ds, out HTuple Rows, out HTuple Cols)
       {
           Rows = new HTuple();
           Cols = new HTuple();
           Point2Db Pt2DI = new Point2Db();
           for (int i = 0; i < Pt2Ds.Count(); i++)
           {
               Pt2DI = Pt2Ds[i];
               Rows[i] = Pt2DI.Row;
               Cols[i] = Pt2DI.Col;         
           }  
       }

       /// <summary>
       /// 2维坐标点的仿射变换
       /// </summary>
       /// <param name="InPts">输入坐标</param>
       /// <param name="HomMat2D">仿射矩阵 </param>
       /// <param name="OutPts">仿射变换的坐标</param>
       public static void AffineTransPoint2D(List<Point2Db> InPts, MyHomMat2D HomMat2D, out List<Point2Db> OutPts)
       {
           OutPts = new List<Point2Db>();
           HTuple InRows = new HTuple(), InCols = new HTuple();
           HTuple OutRows = new HTuple(), OutCols = new HTuple();
           HTuple HvHomMat2D = new HTuple();
           Pt2dToHTuple(InPts, out InRows, out InCols);
           MyHomMatToHalcon(HomMat2D, out HvHomMat2D);
           HOperatorSet.AffineTransPoint2d(HvHomMat2D, InCols, InRows, out OutCols, out OutRows);
           Point2Db PtI = new Point2Db();
           for (int i = 0; i < OutRows.TupleLength();i++ )
           {
               PtI.Row = OutRows[i].D;
               PtI.Col = OutCols[i].D;
               OutPts.Add(PtI);
           }
       }

        public static void AffineTransPt(double x,double y,HTuple HomMat,out double  AdjX,out double  AdjY)
        {
            HTuple Row = new HTuple(), Col = new HTuple();
            HOperatorSet.AffineTransPoint2d(HomMat, x, y, out Col, out Row);
            AdjX = Col.D;
            AdjY = Row.D;
        }

        public static bool AffineTransPts(List<double> Rows, List<double> Cols, HTuple HomMat, out List<double> AdjRows, out List<double> AdjCols)
        {
            AdjRows = new List<double>();
            AdjCols = new List<double>();
            if (Rows.Count != Cols.Count)  return false; // 输入长度有问题

            try
            {
                HTuple HRows = new HTuple(), HCols = new HTuple();
                ListToHTuple(Rows, out HRows);
                ListToHTuple(Cols, out HCols);
                HTuple TranRows = new HTuple(), TranCols = new HTuple();
                HOperatorSet.AffineTransPixel(HomMat, HRows, HCols, out TranRows, out TranCols);
                HTupleToList(TranRows, out AdjRows);
                HTupleToList(TranCols, out AdjCols);
                return true;
            }
            catch (Exception e0)
            {
                FileLib.Logger.Pop(e0.ToString(), true, "视觉错误日志");
                return false;   
            }
        }

       /// <summary>
       /// 单点的坐标变换
       /// </summary>
       /// <param name="InPt"></param>
       /// <param name="HomMat2D"></param>
       /// <param name="OutPt"></param>
       public static void AffineTransPoint2D(Point2Db InPt, MyHomMat2D HomMat2D, out Point2Db OutPt)
       {
           OutPt = new Point2Db();
           HTuple InRow = new HTuple(), InCol= new HTuple();
           HTuple OutRow = new HTuple(), OutCol = new HTuple();
           HTuple HvHomMat2D = new HTuple();
           InRow = InPt.Row;
           InCol = InPt.Col;
           MyHomMatToHalcon(HomMat2D, out HvHomMat2D);
           HOperatorSet.AffineTransPoint2d(HvHomMat2D, InCol, InRow, out OutCol, out OutRow);
           OutPt.Row = OutRow;
           OutPt.Col = OutCol;
       }

       /// <summary>
       /// 单视野胶带的坐标转换到平台坐标
       /// </summary>
       /// <param name="TapeDnP"></param>
       /// <param name="TapeMotionPosW"></param>
       /// <param name="DnToWorldMat"></param>
       /// <param name="TapeW"></param>
       public static void GetTapePos(Point2Db TapeDnP, Point2Db TapeMotionPosW,MyHomMat2D DnToWorldMat, out Point2Db TapeW)
       {
           TapeW = new Point2Db();
           AffineTransPoint2D(TapeDnP, DnToWorldMat, out TapeW); //将胶带的视觉坐标转换到相对与U轴的坐标
           TapeW = TapeW + TapeMotionPosW;           
       }
       /// <summary>
       /// 双视野胶带的定位
       /// </summary>
       /// <param name="Tip2UpP">Tape上部的像素坐标</param>
       /// <param name="Tip2DnP">Tape下部的像素坐标</param>
       /// <param name="DnToWorldMat"></param>
       /// <param name="Tape2UpW"></param>
       /// <param name="Tape2DnW"></param>
       /// <param name="TapeCeterW"></param>
       /// <param name="TapeTH"></param>
       public static void GetTapePos(Point2Db Tap2UpP, Point2Db Tap2DnP, MyHomMat2D DnToWorldMat, Point2Db Tape2UpW, Point2Db Tape2DnW, out Point2Db TapeCeterW, out double  TapeTH)
       {
           TapeCeterW = new Point2Db();
           TapeTH = 0;
       }
       /// <summary>
       /// HomMat到放射变换的系数
       /// </summary>
       /// <param name="HomMat2DIn"></param>
       /// <param name="Theta"> 旋转角度   </param>
       /// <param name="Tx">    列平移</param>
       /// <param name="Ty">    行平移</param>
       public static void HomMat2dToAffinePara(MyHomMat2D HomMat2DIn ,out double Theta,out double Ty,out double Tx)
       {
           Theta = 0;
           Tx = 0;
           Ty = 0;
           HTuple HvHomMat2D = new HTuple();
           MyHomMatToHalcon(HomMat2DIn, out HvHomMat2D); //转换为Halcon的数据结构
           HTuple Sx =new HTuple(),Sy =new HTuple(),Phi =new HTuple(),HvTheta =new HTuple(),HvTx =new HTuple(),HvTy =new HTuple();
           HOperatorSet.HomMat2dToAffinePar(HvHomMat2D, out Sx, out Sy,out Phi, out HvTheta ,out HvTx,out HvTy);
           Theta = Phi.D;
           Tx = HvTx.D;
           Ty = HvTy.D;   
       }
       public static void hDispObj(HWindow ShowWindow ,HObject ShowObj)
       {
           try
           {
               if (ShowObj.IsInitialized())
               {
                   lock (DispLockObj)
                   {
                       HOperatorSet.DispObj(ShowObj, ShowWindow);
                   }
               }
           }
           catch { }  
       }

       public static void hSetColor(HWindow ShowWindow, HTuple ColorStr)
       {
           try
           {
               lock (DispLockObj)
               {
                   HOperatorSet.SetColor(ShowWindow, ColorStr);
               }
           }
           catch { }
       }
       public static void hClearWindow(HWindow ShowWindow)
       {
           lock (DispLockObj)
           {
               HOperatorSet.ClearWindow(ShowWindow);
           }     
       }
       public static void GenCirclrPts(double Row,double Col,double R,out List<double> Rows,out List<double>  Cols) 
       {
           Rows = new List<double>();
           Cols = new List<double>();
           Rows.Add(Row);
           Cols.Add(Col + R);
           Rows.Add(Row-R);
           Cols.Add(Col );
           Rows.Add(Row );
           Cols.Add(Col-R);
           Rows.Add(Row + R);
           Cols.Add(Col);
           Rows.Add(Row);
           Cols.Add(Col + R);             
       }
       public static void GenCirclePts1(HTuple Row,HTuple Col,HTuple  R,out HTuple Rows ,out HTuple Cols)
       {
           Rows = new HTuple();
           Cols = new HTuple();
           Rows=Row;
           Cols=Col + R;

           Rows[1] = Row - R;
           Cols[1] = Col;

           Rows[2] = Row ;
           Cols[2] = Col - R;

           Rows[3] = Row+ R;
           Cols[3] = Col;

           Rows[4] = Row ;
           Cols[4] = Col + R;
       
       }

       public static bool GenCirclePts2(HTuple Row, HTuple Col, HTuple R,HTuple StartPhi,HTuple EndPhi,HTuple PointOrder, out HTuple Rows, out HTuple Cols)
       {
           Rows = new HTuple();
           Cols = new HTuple();
           if (StartPhi.Length == 0) 
               return false;
           try
           {
               if (PointOrder.ToString().Equals("positive"))
               {
                   if (EndPhi < StartPhi)
                   {
                       EndPhi = EndPhi + 2.0 * Math.PI;
                   }
               }
               else if (PointOrder.ToString().Equals("negative"))
               {
                   if (EndPhi > StartPhi)
                   {
                       EndPhi = EndPhi - 2.0 * Math.PI;
                   }
               }
               Rows = Row - R * StartPhi.TupleSin();
               Cols = Col + R * StartPhi.TupleCos();
               for (int i = 0; i < 11; i++)
               {
                   Rows[i] = Row - R * ((StartPhi + i * (EndPhi - StartPhi) / 10.0).TupleSin());
                   Cols[i] = Col + R * ((StartPhi + i * (EndPhi - StartPhi) / 10.0).TupleCos());
               }
           }
           catch (Exception e0)
           {
               Logger.Pop(e0.Message + e0.Source);
               return false;
           
           }
           return true;
       }

       public static void ArrayToHTuple(Object[] Array, out HTuple OutHTuple)
       {
           OutHTuple = new HTuple();
           for (int i = 0; i < Array.Length; i++)
           {
               OutHTuple[i] = (HTuple)Array[i];
           }
       }

       public static void HTupleToArray(HTuple InHTuple, out Object[] OutArray)
       {
           HTuple HTupleLength = new HTuple();
           HOperatorSet.TupleLength(InHTuple, out HTupleLength);
           int Length0 = (int)HTupleLength;
           OutArray = new Object[Length0];
           for (HTuple i = 0; i < HTupleLength; i++)
           {
               OutArray[i] = InHTuple[i];
           }
       }

       public static void HTupleToList(HTuple InHTuple, out List<double > ListD)
       {
           ListD = new List<double>();
           for (int i = 0; i < InHTuple.TupleLength(); i++)
           {
               ListD.Add(InHTuple[i].D);           
           }    
       }
       public static void ListDoubToHtuple(List<double>ListDoubIn,out HTuple OutHTuple)
       {
           OutHTuple = new HTuple();
           for (int i = 0; i < ListDoubIn.Count;i++ )
           {
               OutHTuple[i] = ListDoubIn[i];
           
           }
       
       }

       public static void ListPt2dToHTuple(List<Point2Db> ListPtIn,out HTuple Rows ,out HTuple Cols)
       {
           Rows = new HTuple();
           Cols = new HTuple();
           Point2Db PtI = new Point2Db();
           for (int i = 0; i < ListPtIn.Count; i++)
           {
               PtI = ListPtIn[i];
               Rows[i] = PtI.Row;
               Cols[i] = PtI.Col;           
           }       
       }
       public static void HTupleToListPt2d(HTuple Rows,HTuple Cols,out List<Point2Db> ListPtOut)
       {
           Point2Db PtI = new Point2Db();
           ListPtOut = new List<Point2Db>();
           if (Rows.TupleLength() != Cols.TupleLength())
           { return; }
           for (int i =0; i < Rows.TupleLength(); i++)
           {
               PtI.Row = Rows[i].D;
               PtI.Col = Cols[i].D;
               ListPtOut.Add(PtI);
           }    
       }


       /// <summary>
       /// 生成箭头
       /// </summary>
       /// <param name="ho_Arrow"></param>
       /// <param name="hv_Row1"></param>
       /// <param name="hv_Column1"></param>
       /// <param name="hv_Row2"></param>
       /// <param name="hv_Column2"></param>
       /// <param name="hv_HeadLength"></param>
       /// <param name="hv_HeadWidth"></param>
       public static void GenArrowContourXld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_TempArrow = null;
           // Local control variables 
           HTuple hv_Length, hv_ZeroLengthIndices, hv_DR;
           HTuple hv_DC, hv_HalfHeadWidth, hv_RowP1, hv_ColP1, hv_RowP2;
           HTuple hv_ColP2, hv_Index;
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Arrow);
           HOperatorSet.GenEmptyObj(out ho_TempArrow);
           ho_Arrow.Dispose();
           HOperatorSet.GenEmptyObj(out ho_Arrow);
           //Calculate the arrow length
           HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
           //Mark arrows with identical start and end point
           //(set Length to -1 to avoid division-by-zero exception)
           hv_ZeroLengthIndices = hv_Length.TupleFind(0);
           if ((int)(new HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
           {
               hv_Length[hv_ZeroLengthIndices] = -1;
           }
           //Calculate auxiliary variables.
           hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
           hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
           hv_HalfHeadWidth = hv_HeadWidth / 2.0;

           //Calculate end points of the arrow head.
           hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
           hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
           hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
           hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
           //Finally create output XLD contour for each input point pair
           for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
           {
               if ((int)(new HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
               {
                   //Create_ single points for arrows with identical start and end point
                   ho_TempArrow.Dispose();
                   HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(hv_Index), hv_Column1.TupleSelect(hv_Index));
               }
               else
               {
                   //Create arrow contour
                   ho_TempArrow.Dispose();
                   HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                       hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                       hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                       hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                       ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                       hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                       hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                       hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
               }
               OTemp[SP_O] = ho_Arrow.CopyObj(1, -1);
               SP_O++;
               ho_Arrow.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_TempArrow, out ho_Arrow);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
           }
           ho_TempArrow.Dispose();
           return;
       }


       public static bool draw_rake(HObject ho_Image, out HObject ho_Regions, HTuple hv_WindowHandle, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, out HTuple hv_Row1,
                      out HTuple hv_Column1, out HTuple hv_Row2, out HTuple hv_Column2)
       {
           lock (DispLockObj)
           {
               #region
               // Stack for temporary objects 
               HObject[] OTemp = new HObject[20];
               long SP_O = 0;
               // Local iconic variables 
               HObject ho_RegionLines, ho_Rectangle = null;
               HObject ho_Arrow1 = null;
               // Local control variables 
               HTuple hv_Width, hv_Height, hv_ATan, hv_i;
               HTuple hv_RowC = new HTuple(), hv_ColC = new HTuple(), hv_Distance = new HTuple();
               HTuple hv_RowL2 = new HTuple(), hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple();
               HTuple hv_ColL1 = new HTuple();
               HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
               // Initialize local and output iconic variables 
               HOperatorSet.GenEmptyObj(out ho_Regions);
               HOperatorSet.GenEmptyObj(out ho_RegionLines);
               HOperatorSet.GenEmptyObj(out ho_Rectangle);
               HOperatorSet.GenEmptyObj(out ho_Arrow1);
               hv_Row1 = new HTuple(); 
               hv_Column1 = new HTuple(); 
               hv_Row2 = new HTuple();
               hv_Column2 = new HTuple();
               try
               {
                   //1.获取图像大小
                   HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                   ho_Regions.Dispose();
                   HOperatorSet.GenEmptyObj(out ho_Regions);
                   disp_message(hv_WindowHandle, "1.双击确定直线起点，单击直线终点，点击右键确认", "window", 12, 12, "red", "false");
                   HOperatorSet.SetColor(hv_WindowHandle,"red");
                   HOperatorSet.DrawLine(hv_WindowHandle, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2);               
                   //2.生成所画的图像并保存为对象
                   ho_RegionLines.Dispose();
                   HOperatorSet.GenContourPolygonXld(out ho_RegionLines, hv_Row1.TupleConcat(hv_Row2), hv_Column1.TupleConcat(hv_Column2));
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RegionLines, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //3.计算所画直线的角度
                   HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);
                   hv_ATan = hv_ATan + ((new HTuple(90)).TupleRad());
                   //4.生成卡尺工具的检测区域
                   for (hv_i = 1; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
                   {
                       //4.1如果只有一个测量矩形，作为卡尺工具，宽度为检测为直线的长度
                       if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
                       {
                           hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                           hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                           if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                               hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                           {
                               continue;
                           }
                           HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                           hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                           ho_Rectangle.Dispose();
                           HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_Distance / 50);
                           OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                           SP_O++;
                           ho_Regions.Dispose();
                           HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Regions);
                           OTemp[SP_O - 1].Dispose();
                           SP_O = 0;
                           //4.2如果有多个测量矩形，产生该测量矩形的XLD
                       }
                       else
                       {
                           hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                           hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                           //4.3超出图像区域，跳出当前循环
                           if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                               hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                           {
                               continue;
                           }
                           ho_Rectangle.Dispose();
                           HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                           OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                           SP_O++;
                           ho_Regions.Dispose();
                           HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Regions);
                           OTemp[SP_O - 1].Dispose();
                           SP_O = 0;
                       }
                       //4.4把测量矩形XLD存储到显示对象
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                       //4.5生成显示检测方向的箭头
                       if ((int)(new HTuple(hv_i.TupleEqual(1))) != 0)
                       {
                           hv_RowL2 = hv_RowC + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin())) * 1.2;
                           hv_RowL1 = hv_RowC - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                           hv_ColL2 = hv_ColC + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos())) * 1.2;
                           hv_ColL1 = hv_ColC - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                           ho_Arrow1.Dispose();
                           GenArrowContourXld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2, 5, 5);
                           OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                           SP_O++;
                           ho_Regions.Dispose();
                           HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Arrow1, out ho_Regions);
                           OTemp[SP_O - 1].Dispose();
                           SP_O = 0;
                       }
                   }
                   ho_RegionLines.Dispose();
                   ho_Rectangle.Dispose();
                   ho_Arrow1.Dispose();
                   return true;
               }
               catch (HalconException HDevExpDefaultException)
               {
                   ho_RegionLines.Dispose();
                   ho_Rectangle.Dispose();
                   ho_Arrow1.Dispose();
                   return false;
                   throw HDevExpDefaultException;
               }
               #endregion
           }
       }


       public static bool gen_rake_ROI(HObject ho_Image, out HObject ho_Regions, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Row1, HTuple hv_Column1,
                         HTuple hv_Row2, HTuple hv_Column2)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_RegionLines, ho_Rectangle = null;
           HObject ho_Arrow1 = null;
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_ATan, hv_i;
           HTuple hv_RowC = new HTuple(), hv_ColC = new HTuple(), hv_Distance = new HTuple();
           HTuple hv_RowL2 = new HTuple(), hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple();
           HTuple hv_ColL1 = new HTuple();
           HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_RegionLines);
           HOperatorSet.GenEmptyObj(out ho_Rectangle);
           HOperatorSet.GenEmptyObj(out ho_Arrow1);
           try
           {
               //1.获取图像大小
               HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
               ho_Regions.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Regions);
               //2.生成所画的图像并保存为对象
               ho_RegionLines.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_RegionLines, hv_Row1.TupleConcat(hv_Row2), hv_Column1.TupleConcat(hv_Column2));
               OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
               SP_O++;
               ho_Regions.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RegionLines, out ho_Regions);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
               //3.计算所画直线的角度
               HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);
               hv_ATan = hv_ATan + ((new HTuple(90)).TupleRad());
               //4.生成卡尺工具的检测区域
               for (hv_i = 1; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
               {
                   //4.1如果只有一个测量矩形，作为卡尺工具，宽度为检测为直线的长度
                   if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
                   {
                       hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                       hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                       if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                           hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                       {
                           continue;
                       }
                       HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                       hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                       ho_Rectangle.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_Distance / 50);
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                       //4.2如果有多个测量矩形，产生该测量矩形的XLD
                   }
                   else
                   {
                       hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                       hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                       //4.3超出图像区域，跳出当前循环
                       if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                           hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                       {
                           continue;
                       }
                       ho_Rectangle.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                   }
                   //4.4把测量矩形XLD存储到显示对象
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //4.5生成显示检测方向的箭头
                   if ((int)((new HTuple(((hv_i % 4)).TupleEqual(0))).TupleOr(new HTuple(hv_i.TupleEqual(1)))) != 0)
                   {
                       hv_RowL2 = hv_RowC + (((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin())) * 1.5);
                       hv_RowL1 = hv_RowC - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                       hv_ColL2 = hv_ColC + (((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos())) * 1.5);
                       hv_ColL1 = hv_ColC - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                       ho_Arrow1.Dispose();
                       GenArrowContourXld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2, 5, 5);
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Arrow1, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                   }
               }
               ho_RegionLines.Dispose();
               ho_Rectangle.Dispose();
               ho_Arrow1.Dispose();
               return  true;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_RegionLines.Dispose();
               ho_Rectangle.Dispose();
               ho_Arrow1.Dispose();
               Logger.PopError(HDevExpDefaultException.Message + HDevExpDefaultException.Source);
               return false;
           }
       }


       /// <summary>
       /// 直线卡尺工具，找出用于直线拟合的边界点
       /// </summary>    
       /// <param name="ho_Image">          输入图片                                                                                        </param>
       /// <param name="ho_Regions">        示教的直线                                                                                      </param>
       /// <param name="hv_Elements">       检测边缘点个数                                                                                  </param>
       /// <param name="hv_DetectHeight">   检测高度                                                                                        </param>
       /// <param name="hv_DetectWidth">    检测宽度                                                                                        </param>
       /// <param name="hv_Sigma">          高斯滤波系数                                                                                    </param>
       /// <param name="hv_Threshold">      边缘阈值                                                                                        </param>
       /// <param name="hv_Transition">     Transition = 'positive'，'negative'， 'all'，黑到白，白到黑                                     </param>
       /// <param name="hv_Select">         “all”measure_pos返回所有点，程序选择最大点；"First"返回第一边界点；"last" 返回最后一个点      </param>
       /// <param name="hv_Row1">           起点行                                                                                          </param>
       /// <param name="hv_Column1">        起点列                                                                                          </param>
       /// <param name="hv_Row2">           终点行                                                                                          </param>
       /// <param name="hv_Column2">        终点列                                                                                          </param>
       /// <param name="hv_ResultRow">      找到的边界点行                                                                                  </param>
       /// <param name="hv_ResultColumn">   找到的边界点列                                                                                  </param>
       public static bool Rake(HObject ho_Image, out HObject ho_Regions, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Sigma, HTuple hv_Threshold,
                               HTuple hv_Transition, HTuple hv_Select, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2, out HTuple hv_ResultRow, out HTuple hv_ResultColumn)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_RegionLines, ho_Rectangle = null;
           HObject ho_Arrow1 = null;
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_ATan, hv_i;
           HTuple hv_RowC = new HTuple(), hv_ColC = new HTuple(), hv_Distance = new HTuple();
           HTuple hv_RowL2 = new HTuple(), hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple();
           HTuple hv_ColL1 = new HTuple(), hv_MsrHandle_Measure = new HTuple();
           HTuple hv_RowEdge = new HTuple(), hv_ColEdge = new HTuple();
           HTuple hv_Amplitude = new HTuple(), hv_tRow = new HTuple();
           HTuple hv_tCol = new HTuple(), hv_t = new HTuple(), hv_Number = new HTuple();
           HTuple hv_j = new HTuple();
           HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
           HTuple hv_Select_COPY_INP_TMP = hv_Select.Clone();
           HTuple hv_Transition_COPY_INP_TMP = hv_Transition.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_RegionLines);
           HOperatorSet.GenEmptyObj(out ho_Rectangle);
           HOperatorSet.GenEmptyObj(out ho_Arrow1);
           //1.获取图像大小
           HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
           ho_Regions.Dispose();
           HOperatorSet.GenEmptyObj(out ho_Regions);
           //2.生成所画的图像并保存为对象
           hv_ResultRow = new HTuple();
           hv_ResultColumn = new HTuple();
           ho_RegionLines.Dispose();
           try 
           {
               #region
               HOperatorSet.GenContourPolygonXld(out ho_RegionLines, hv_Row1.TupleConcat(hv_Row2), hv_Column1.TupleConcat(hv_Column2));
               OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
               SP_O++;
               ho_Regions.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RegionLines, out ho_Regions);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
               //3.计算所画直线的角度
               HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);
               hv_ATan = hv_ATan + ((new HTuple(90)).TupleRad());
               //4.生成卡尺工具的检测区域
               for (hv_i = 1; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
               {
                   //4.1如果只有一个测量矩形，作为卡尺工具，宽度为检测为直线的长度
                   if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
                   {
                       hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                       hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                       //  if(RowC>Height-1 or RowC<0 or ColC>Width-1 or ColC<0 )  超过图像边界的点删除
                       if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                           hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                       {
                           continue;
                       }
                       HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                       hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                       ho_Rectangle.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_Distance / 50);
                       //4.2如果有多个测量矩形，产生该测量矩形的XLD
                   }
                   else
                   {
                       hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                       hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                       //4.3超出图像区域，跳出当前循环
                       if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                           hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                       {
                           continue;
                       }
                       ho_Rectangle.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                   }
                   //4.4把测量矩形XLD存储到显示对象
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //4.5生成显示检测方向的箭头
                   if ((int)(new HTuple(hv_i.TupleEqual(1))) != 0)
                   {
                       hv_RowL2 = hv_RowC + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                       hv_RowL1 = hv_RowC - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                       hv_ColL2 = hv_ColC + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                       hv_ColL1 = hv_ColC - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                       ho_Arrow1.Dispose();
                       GenArrowContourXld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2, 5, 5);
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Arrow1, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                   }
                   //4.6生成测量矩形
                   HOperatorSet.GenMeasureRectangle2(hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2, hv_Width, hv_Height, "nearest_neighbor", out hv_MsrHandle_Measure);
                   if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("negative"))) != 0)
                   {
                       hv_Transition_COPY_INP_TMP = "negative";
                   }
                   else
                   {
                       if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("positive"))) != 0)
                       {
                           hv_Transition_COPY_INP_TMP = "positive";
                       }
                       else
                       {
                           hv_Transition_COPY_INP_TMP = "all";
                       }
                   }
                   //4.7设置边缘位置。最强点是从所有边缘中选择幅值绝对值最大点，需要设置为'all'
                   if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("first"))) != 0)
                   {
                       hv_Select_COPY_INP_TMP = "first";
                   }
                   else
                   {
                       if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("last"))) != 0)
                       {
                           hv_Select_COPY_INP_TMP = "last";
                       }
                       else
                       {
                           hv_Select_COPY_INP_TMP = "all";
                       }
                   }
                   //4.8找出梯度幅值超过阈值的点
                   HOperatorSet.MeasurePos(ho_Image, hv_MsrHandle_Measure, hv_Sigma, hv_Threshold, hv_Transition_COPY_INP_TMP, hv_Select_COPY_INP_TMP, out hv_RowEdge, out hv_ColEdge,
                       out hv_Amplitude, out hv_Distance);
                   //measure_pos (Image, MsrHandle_Measure, Sigma, Threshold, 'all', 'all', RowEdge, ColEdge, Amplitude, Distance)
                   HOperatorSet.CloseMeasure(hv_MsrHandle_Measure);
                   hv_tRow = 0;
                   hv_tCol = 0;
                   //保存边缘幅度绝对值
                   hv_t = 0;
                   //找到的边缘至少为1个
                   HOperatorSet.TupleLength(hv_RowEdge, out hv_Number);
                   if ((int)(new HTuple(hv_Number.TupleLess(1))) != 0)
                   {
                       continue;
                   }
                   //有多个边缘时，选择幅度最大的边缘
                   for (hv_j = 0; hv_j.Continue(hv_Number - 1, 1); hv_j = hv_j.TupleAdd(1))
                   {
                       if ((int)(new HTuple(((((hv_Amplitude.TupleSelect(hv_j))).TupleAbs())).TupleGreater(hv_t))) != 0)
                       {
                           hv_tRow = hv_RowEdge.TupleSelect(hv_j);
                           hv_tCol = hv_ColEdge.TupleSelect(hv_j);
                           hv_t = ((hv_Amplitude.TupleSelect(hv_j))).TupleAbs();
                       }
                   }
                   //4.9 把找到的边缘保存到输出数组
                   if ((int)(new HTuple(hv_t.TupleGreater(0))) != 0)
                   {
                       hv_ResultRow = hv_ResultRow.TupleConcat(hv_tRow);
                       hv_ResultColumn = hv_ResultColumn.TupleConcat(hv_tCol);
                   }
               }

               ho_RegionLines.Dispose();
               ho_Rectangle.Dispose();
               ho_Arrow1.Dispose();
               return true;
               #endregion
           }
           catch(Exception e0)
           {
               Logger.PopError(e0.Message + e0.Source);
               return false;
           }

       }




       /// <summary>
       /// 直线卡尺工具，找出用于直线拟合的边界点
       /// </summary>    
       /// <param name="ho_Image">          输入图片                                                                                        </param>
       /// <param name="ho_Regions">        示教的直线                                                                                      </param>
       /// <param name="hv_Elements">       检测边缘点个数                                                                                  </param>
       /// <param name="hv_DetectHeight">   检测高度                                                                                        </param>
       /// <param name="hv_DetectWidth">    检测宽度                                                                                        </param>
       /// <param name="hv_Sigma">          高斯滤波系数                                                                                    </param>
       /// <param name="hv_Threshold">      边缘阈值                                                                                        </param>
       /// <param name="hv_Transition">     Transition = 'positive'，'negative'， 'all'，黑到白，白到黑                                     </param>
       /// <param name="hv_Select">         “all”measure_pos返回所有点，程序选择最大点；"first"返回第一边界点；"last" 返回最后一个点      </param>
       /// <param name="hv_Row1">           起点行                                                                                          </param>
       /// <param name="hv_Column1">        起点列                                                                                          </param>
       /// <param name="hv_Row2">           终点行                                                                                          </param>
       /// <param name="hv_Column2">        终点列                                                                                          </param>
       /// <param name="hv_ResultRow">      找到的边界点行                                                                                  </param>
       /// <param name="hv_ResultColumn">   找到的边界点列                                                                                  </param>
       public static bool Rake1(HObject ho_Image, out HObject ho_Regions, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Sigma, HTuple hv_Threshold, HTuple hv_Transition, HTuple hv_Select, HTuple hv_Row1,
                    HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2, out HTuple hv_ResultRow, out HTuple hv_ResultColumn)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_RegionLines;
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_ATan, hv_i;
           HTuple hv_RowC = new HTuple(), hv_ColC = new HTuple(), hv_Distance = new HTuple();
           HTuple hv_MsrHandle_Measure = new HTuple(), hv_RowEdge = new HTuple();
           HTuple hv_ColEdge = new HTuple(), hv_Amplitude = new HTuple();
           HTuple hv_tRow = new HTuple(), hv_tCol = new HTuple(), hv_t = new HTuple();
           HTuple hv_Number = new HTuple(), hv_j = new HTuple();
           HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
           HTuple hv_Select_COPY_INP_TMP = hv_Select.Clone();
           HTuple hv_Transition_COPY_INP_TMP = hv_Transition.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_RegionLines);
           //1.获取图像大小
           HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
           ho_Regions.Dispose();
           HOperatorSet.GenEmptyObj(out ho_Regions);
           //2.生成所画的图像并保存为对象
           hv_ResultRow = new HTuple();
           hv_ResultColumn = new HTuple();
           ho_RegionLines.Dispose();
           HOperatorSet.GenContourPolygonXld(out ho_RegionLines, hv_Row1.TupleConcat(hv_Row2), hv_Column1.TupleConcat(hv_Column2));
           OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
           SP_O++;
           ho_Regions.Dispose();
           HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RegionLines, out ho_Regions);
           OTemp[SP_O - 1].Dispose();
           SP_O = 0;
           //3.计算所画直线的角度
           HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);
           hv_ATan = hv_ATan + ((new HTuple(90)).TupleRad());

           //4.生成卡尺工具的检测区域
           for (hv_i = 1; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
           {
               //4.1如果只有一个测量矩形，作为卡尺工具，宽度为检测为直线的长度
               if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
               {
                   hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                   hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                   if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                       hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                   {
                       continue;
                   }
                   HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                   hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                   //4.2如果有多个测量矩形，产生该测量矩形的XLD
               }
               else
               {
                   hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                   hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                   //4.3超出图像区域，跳出当前循环
                   if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                       hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                   {
                       continue;
                   }
               }
               //4.6生成测量矩形
               HOperatorSet.GenMeasureRectangle2(hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2, hv_Width, hv_Height, "nearest_neighbor", out hv_MsrHandle_Measure);
               if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("negative"))) != 0)
               {
                   hv_Transition_COPY_INP_TMP = "negative";
               }
               else
               {
                   if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("positive"))) != 0)
                   {
                       hv_Transition_COPY_INP_TMP = "positive";
                   }
                   else
                   {
                       hv_Transition_COPY_INP_TMP = "all";
                   }
               }

               //4.7设置边缘位置。最强点是从所有边缘中选择幅值绝对值最大点，需要设置为'all'
               if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("first"))) != 0)
               {
                   hv_Select_COPY_INP_TMP = "first";
               }
               else
               {
                   if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("last"))) != 0)
                   {
                       hv_Select_COPY_INP_TMP = "last";
                   }
                   else
                   {
                       hv_Select_COPY_INP_TMP = "all";
                   }
               }
               //4.8找出梯度幅值超过阈值的点
               HOperatorSet.MeasurePos(ho_Image, hv_MsrHandle_Measure, hv_Sigma, hv_Threshold, hv_Transition_COPY_INP_TMP, hv_Select_COPY_INP_TMP, out hv_RowEdge, out hv_ColEdge, out hv_Amplitude, out hv_Distance);
               //measure_pos (Image, MsrHandle_Measure, Sigma, Threshold, 'all', 'all', RowEdge, ColEdge, Amplitude, Distance)
               HOperatorSet.CloseMeasure(hv_MsrHandle_Measure);
               hv_tRow = 0;
               hv_tCol = 0;
               //保存边缘幅度绝对值
               hv_t = 0;
               //找到的边缘至少为1个
               HOperatorSet.TupleLength(hv_RowEdge, out hv_Number);
               if ((int)(new HTuple(hv_Number.TupleLess(1))) != 0)
               {
                   continue;
               }
               //有多个边缘时，选择幅度最大的边缘
               for (hv_j = 0; hv_j.Continue(hv_Number - 1, 1); hv_j = hv_j.TupleAdd(1))
               {
                   if ((int)(new HTuple(((((hv_Amplitude.TupleSelect(hv_j))).TupleAbs())).TupleGreater(
                       hv_t))) != 0)
                   {
                       hv_tRow = hv_RowEdge.TupleSelect(hv_j);
                       hv_tCol = hv_ColEdge.TupleSelect(hv_j);
                       hv_t = ((hv_Amplitude.TupleSelect(hv_j))).TupleAbs();
                   }
               }
               //4.9 把找到的边缘保存到输出数组
               if ((int)(new HTuple(hv_t.TupleGreater(0))) != 0)
               {
                   hv_ResultRow = hv_ResultRow.TupleConcat(hv_tRow);
                   hv_ResultColumn = hv_ResultColumn.TupleConcat(hv_tCol);
               }
           }
           ho_RegionLines.Dispose();
           return hv_ResultRow.TupleLength() > 0 && hv_ResultColumn.TupleLength()>0;
       }

       /// <summary>
       /// 拟合直线
       /// </summary>
       /// <param name="ho_Line">         拟合得到的直线对象                                                     </param>
       /// <param name="hv_Rows">         输入点的行坐标                                                         </param>
       /// <param name="hv_Cols">         输入点的列坐标                                                         </param>
       /// <param name="hv_ActiveNum">    输入点的个数限制，大于1，输入点的个数小于hv_ActiveNum时不进行直线拟合  </param>
       /// <param name="hv_Row1">         拟合得到的直线起点行                                                   </param>
       /// <param name="hv_Column1">      拟合得到的直线起点列                                                   </param>
       /// <param name="hv_Row2">         拟合得到的直线终点行                                                   </param>
       /// <param name="hv_Column2">      拟合得到的直线终点行                                                   </param>   
       public static bool PtsToBestLine(out HObject ho_Line, HTuple hv_Rows, HTuple hv_Cols, HTuple hv_ActiveNum, out HTuple hv_Row1, out HTuple hv_Column1, out HTuple hv_Row2, out HTuple hv_Column2, out HTuple hv_Dist)
       {
           HObject ho_Contour = null;
           HTuple hv_Length, hv_Nr = new HTuple(), hv_Nc = new HTuple();
           HTuple hv_Length1 = new HTuple();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Line);
           HOperatorSet.GenEmptyObj(out ho_Contour);
           //1.初始化
           hv_Row1 = 0;
           hv_Column1 = 0;
           hv_Row2 = 0;
           hv_Column2 = 0;
           hv_Dist = 0;
           //2.产生一个空的直线对象，用于保存拟合后的直线
           ho_Line.Dispose();
           HOperatorSet.GenEmptyObj(out ho_Line);
           HOperatorSet.TupleLength(hv_Cols, out hv_Length);
           if ((int)((new HTuple(hv_Length.TupleGreater(hv_ActiveNum))).TupleAnd(new HTuple(hv_ActiveNum.TupleGreater(1)))) != 0)
           {
               ho_Contour.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows, hv_Cols);
               HOperatorSet.FitLineContourXld(ho_Contour, "tukey", -1, 0, 5, 2, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2, out hv_Nr, out hv_Nc, out hv_Dist);//hv_Nr hv_Nc单位向量行列坐标
               HOperatorSet.TupleLength(hv_Dist, out hv_Length1);
               if ((int)(new HTuple(hv_Length1.TupleLess(1))) != 0)
               {
                   ho_Contour.Dispose();
                   return  false;
               }
               ho_Line.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_Line, hv_Row1.TupleConcat(hv_Row2), hv_Column1.TupleConcat(hv_Column2));
           }
           ho_Contour.Dispose();
           return true;
       }
       /// <summary>
       ///                                             示教圆的位置
       /// </summary>
       /// <param name="ho_Image">                     输入图像                  </param>
       /// <param name="ho_Regions">                   输出示教的圆              </param>
       /// <param name="hv_WindowHandle">              窗口控件                  </param>
       /// <param name="hv_Elements">                  ROI个数                   </param>
       /// <param name="hv_DetectHeight">              ROI检测高度               </param>
       /// <param name="hv_DetectWidth">               ROI的检测宽度             </param>
       /// <param name="hv_ROIRows">                   ROI中心行坐标             </param>
       /// <param name="hv_ROICols">                   ROI中心列坐标             </param>
       /// <param name="hv_Direct">                    ROI边界点查找方向         </param>
       /// <param name="hv_ArcType">                   圆弧类型                  </param>
       public static void draw_spoke(HObject ho_Image, out HObject ho_Regions, HWindow hv_WindowHandle, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, out HTuple hv_ROIRows,
                             out HTuple hv_ROICols, out HTuple hv_Direct, out HTuple hv_ArcType)
       {
           lock (DispLockObj)
           {
               #region
               // Stack for temporary objects 
               HObject[] OTemp = new HObject[20];
               long SP_O = 0;
               // Local iconic variables 
               HObject ho_ContOut1, ho_Contour, ho_ContCircle;
               HObject ho_Cross, ho_Arrow11, ho_Rectangle1 = null, ho_Arrow1 = null;
               // Local control variables 
               HTuple hv_Rows, hv_Cols, hv_Weights, hv_Length1;
               HTuple hv_RowC, hv_ColumnC, hv_Radius, hv_StartPhi, hv_EndPhi;
               HTuple hv_PointOrder, hv_RowXLD, hv_ColXLD, hv_Row1, hv_Column1;
               HTuple hv_Row2, hv_Column2, hv_DistanceStart, hv_DistanceEnd;
               HTuple hv_Length2, hv_i, hv_j = new HTuple(), hv_RowE = new HTuple();
               HTuple hv_ColE = new HTuple(), hv_ATan = new HTuple(), hv_RowL2 = new HTuple();
               HTuple hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple(), hv_ColL1 = new HTuple();
               // Initialize local and output iconic variables 
               HOperatorSet.GenEmptyObj(out ho_Regions);
               HOperatorSet.GenEmptyObj(out ho_ContOut1);
               HOperatorSet.GenEmptyObj(out ho_Contour);
               HOperatorSet.GenEmptyObj(out ho_ContCircle);
               HOperatorSet.GenEmptyObj(out ho_Cross);
               HOperatorSet.GenEmptyObj(out ho_Arrow11);
               HOperatorSet.GenEmptyObj(out ho_Rectangle1);
               HOperatorSet.GenEmptyObj(out ho_Arrow1);
               hv_ROIRows = new HTuple();
               hv_ROICols = new HTuple();
               hv_Direct = new HTuple();
               hv_ArcType = new HTuple();
               try
               {
                   disp_message(hv_WindowHandle, "1.画四个以上点，确定一个圆弧，点击右键确认", "window", 12, 12, "red", "false");
                   //产生一个空对象用于显示          
                   HOperatorSet.GenEmptyObj(out ho_Regions);
                   //沿着圆弧或圆的边缘画点         
                   HOperatorSet.SetColor(hv_WindowHandle, "green");
                   HOperatorSet.DrawNurbs(out ho_ContOut1, hv_WindowHandle, "true", "true", "true", "true", 3, out hv_Rows, out hv_Cols, out hv_Weights);
                   HOperatorSet.SetRgb(hv_WindowHandle, 255, 255, 255);
                   HOperatorSet.TupleLength(hv_Weights, out hv_Length1);
                   //至少要4个点
                   if ((int)(new HTuple(hv_Length1.TupleLess(4))) != 0)
                   {
                       disp_message(hv_WindowHandle, "提示：点数太少，请重画", "window", 32, 12, "red", "false");
                       hv_ROICols = new HTuple();
                       hv_ROIRows = new HTuple();
                       ho_ContOut1.Dispose();
                       ho_Contour.Dispose();
                       ho_ContCircle.Dispose();
                       ho_Cross.Dispose();
                       ho_Arrow11.Dispose();
                       ho_Rectangle1.Dispose();
                       ho_Arrow1.Dispose();
                       return;
                   }
                   //获取点
                   hv_ROIRows = hv_Rows.Clone();
                   hv_ROICols = hv_Cols.Clone();
                   ho_Contour.Dispose();
                   HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows, hv_ROICols);
                   //用回归线法（不抛出异常点，所有点的权重一样）拟合圆
                   HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowC, out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                   ho_ContCircle.Dispose();
                   HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ContCircle, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //获取圆或圆弧xld上的点坐标
                   HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);
                   //显示图像和圆弧
                   HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                   HOperatorSet.SetColor(hv_WindowHandle, "green");
                   HOperatorSet.DispObj(ho_ContCircle, hv_WindowHandle);
                   HOperatorSet.SetRgb(hv_WindowHandle, 255, 255, 255);
                   //产生并显示圆心
                   ho_Cross.Dispose();
                   HOperatorSet.GenCrossContourXld(out ho_Cross, hv_RowC, hv_ColumnC, 60, 0.785398);
                   HOperatorSet.SetColor(hv_WindowHandle, "green");
                   HOperatorSet.DispObj(ho_Cross, hv_WindowHandle);
                   HOperatorSet.SetRgb(hv_WindowHandle, 255, 255, 255);
                   disp_message(hv_WindowHandle, "2.远离圆心，画箭头确定检测的方向，点击右键确认", "window", 12, 12, "red", "false");
                   //画线，确定检测方向
                   HOperatorSet.SetColor(hv_WindowHandle, "green");
                   HOperatorSet.DrawLine(hv_WindowHandle, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2);
                   HOperatorSet.SetRgb(hv_WindowHandle, 255, 255, 255);
                   ho_Arrow11.Dispose();
                   GenArrowContourXld(out ho_Arrow11, hv_Row1, hv_Column1, hv_Row2, hv_Column2, 5, 5);
                   //求圆心到检测方向起点的距离
                   HOperatorSet.DistancePp(hv_RowC, hv_ColumnC, hv_Row1, hv_Column1, out hv_DistanceStart);
                   //求圆心到检测方向终点的距离
                   HOperatorSet.DistancePp(hv_RowC, hv_ColumnC, hv_Row2, hv_Column2, out hv_DistanceEnd);
                   //求圆或者圆弧XLD上点的数量
                   HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
                   //判断检测边缘的数量是否过少
                   if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
                   {
                       hv_ROIRows = new HTuple();
                       hv_ROICols = new HTuple();
                       disp_message(hv_WindowHandle, "检测边缘的数量太少，请重新设置", "window", 12, 12, "red", "false");
                       ho_ContOut1.Dispose();
                       ho_Contour.Dispose();
                       ho_ContCircle.Dispose();
                       ho_Cross.Dispose();
                       ho_Arrow11.Dispose();
                       ho_Rectangle1.Dispose();
                       ho_Arrow1.Dispose();
                       return;
                   }
                   //如果XLD是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
                   //如果XLD是圆，有Length2个点，以0度为起点，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
                   for (hv_i = 0; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
                   {
                       if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(hv_Length2 - 1)))) != 0)
                       {
                           //xld的起点和中点坐标相同，为圆
                           HOperatorSet.TupleInt((((1.0 * hv_Length2) / hv_Elements) * hv_i) + ((0.3 * hv_Length2) / (2 * hv_Elements)), out hv_j);
                           hv_ArcType = "circle";
                       }
                       else
                       {
                           //否则为圆弧
                           HOperatorSet.TupleInt((((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i) + ((0.3 * hv_Length2) / (2 * hv_Elements)), out hv_j);
                           hv_ArcType = "arc";
                       }
                       //索引越界，强制赋值为最后一个索引
                       if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                       {
                           hv_j = hv_Length2 - 1;
                           continue;
                       }
                       //获取卡尺工具中心
                       hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                       hv_ColE = hv_ColXLD.TupleSelect(hv_j);
                       //通过判断起点和中点到圆心的距离判断判断搜索方向
                       if ((int)(new HTuple(hv_DistanceStart.TupleGreater(hv_DistanceEnd))) != 0)
                       {
                           //求圆心指向边缘的矢量的角度
                           HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                           //角度反向
                           hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                           //边缘搜索方向类型：‘inner’搜索方向由圆外指向圆心；'outer'搜索方向由圆心指向圆外
                           hv_Direct = "inner";
                       }
                       else
                       {
                           HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                           hv_Direct = "outer";
                       }
                       //产生卡尺XLD,并保存到显示对象
                       ho_Rectangle1.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle1, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                       //用箭头XLD指示边缘搜索方向，并保持到显示对象
                       if (hv_i == 0)
                       {
                           hv_RowL2 = hv_RowE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                           hv_RowL1 = hv_RowE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                           hv_ColL2 = hv_ColE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                           hv_ColL1 = hv_ColE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                           ho_Arrow1.Dispose();
                           GenArrowContourXld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2, 25, 10);
                           OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                           SP_O++;
                           ho_Regions.Dispose();
                           HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Arrow1, out ho_Regions);
                           OTemp[SP_O - 1].Dispose();
                           SP_O = 0;
                       }
                   }
                   ho_ContOut1.Dispose();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Cross.Dispose();
                   ho_Arrow11.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_Arrow1.Dispose();
                   return;
               }
               catch (HalconException HDevExpDefaultException)
               {
                   ho_ContOut1.Dispose();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Cross.Dispose();
                   ho_Arrow11.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_Arrow1.Dispose();
                   throw HDevExpDefaultException;
               }
               #endregion
           }
       }

       /// <summary>
       ///                                             示教圆的位置
       /// </summary>
       /// <param name="ho_Image">                     输入图像                  </param>
       /// <param name="ho_Regions">                   输出示教的圆              </param>
       /// <param name="hv_WindowHandle">              窗口控件                  </param>
       /// <param name="hv_Elements">                  ROI个数                   </param>
       /// <param name="hv_DetectHeight">              ROI检测高度               </param>
       /// <param name="hv_DetectWidth">               ROI的检测宽度             </param>
       /// <param name="hv_ROIRows">                   ROI中心行坐标             </param>
       /// <param name="hv_ROICols">                   ROI中心列坐标             </param>
       /// <param name="hv_Direct">                    ROI边界点查找方向         </param>
       /// <param name="hv_ArcType">                   圆弧类型                  </param>
       public static bool  draw_spoke1(HObject ho_Image, out HObject ho_Regions, HWindow hv_WindowHandle, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, out HTuple CenterRow,
           out HTuple CenterCol, out HTuple  CircleR , out HTuple StartPhi ,out HTuple EndPhi, out HTuple hv_Direct,out HTuple PointOrder, out HTuple hv_ArcType)
       {
           lock (DispLockObj)
           {
               #region
               // Stack for temporary objects 
               HObject[] OTemp = new HObject[20];
               long SP_O = 0;
               // Local iconic variables 
               HObject ho_ContOut1, ho_Contour, ho_ContCircle;
               HObject ho_Cross, ho_Arrow11, ho_Rectangle1 = null, ho_Arrow1 = null;
               // Local control variables 
               HTuple hv_Rows, hv_Cols, hv_Weights, hv_Length1;
               HTuple hv_RowC, hv_ColumnC, hv_Radius, hv_StartPhi, hv_EndPhi;
               HTuple hv_PointOrder, hv_RowXLD, hv_ColXLD, hv_Row1, hv_Column1;
               HTuple hv_Row2, hv_Column2, hv_DistanceStart, hv_DistanceEnd;
               HTuple hv_Length2, hv_i, hv_j = new HTuple(), hv_RowE = new HTuple();
               HTuple hv_ColE = new HTuple(), hv_ATan = new HTuple(), hv_RowL2 = new HTuple();
               HTuple hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple(), hv_ColL1 = new HTuple();
               // Initialize local and output iconic variables 
               HOperatorSet.GenEmptyObj(out ho_Regions);
               HOperatorSet.GenEmptyObj(out ho_ContOut1);
               HOperatorSet.GenEmptyObj(out ho_Contour);
               HOperatorSet.GenEmptyObj(out ho_ContCircle);
               HOperatorSet.GenEmptyObj(out ho_Cross);
               HOperatorSet.GenEmptyObj(out ho_Arrow11);
               HOperatorSet.GenEmptyObj(out ho_Rectangle1);
               HOperatorSet.GenEmptyObj(out ho_Arrow1);
               CenterRow = new HTuple();
               CenterCol = new HTuple();
               CircleR  =new HTuple();
               StartPhi =new HTuple();
               EndPhi = new HTuple();
               PointOrder = new HTuple();
               hv_Direct = new HTuple();
               hv_ArcType = new HTuple();
               
               HTuple hv_ROIRows = new HTuple(), hv_ROICols = new HTuple();
               try
               {
                   disp_message(hv_WindowHandle, "1.画四个以上点，确定一个圆弧，点击右键确认", "window", 12, 12, "red", "false");
                   //产生一个空对象用于显示
                   ho_Regions.Dispose();
                   HOperatorSet.GenEmptyObj(out ho_Regions);
                   //沿着圆弧或圆的边缘画点
                   ho_ContOut1.Dispose();
                   HOperatorSet.SetColor(hv_WindowHandle, "green");
                   HOperatorSet.DrawNurbs(out ho_ContOut1, hv_WindowHandle, "true", "true", "true", "true", 3, out hv_Rows, out hv_Cols, out hv_Weights);
                   HOperatorSet.SetRgb(hv_WindowHandle, 255, 255, 255);
                   HOperatorSet.TupleLength(hv_Weights, out hv_Length1);
                   //至少要4个点
                   if ((int)(new HTuple(hv_Length1.TupleLess(4))) != 0)
                   {
                       disp_message(hv_WindowHandle, "提示：点数太少，请重画", "window", 32, 12, "red", "false");
                       hv_ROICols = new HTuple();
                       hv_ROIRows = new HTuple();
                       ho_ContOut1.Dispose();
                       ho_Contour.Dispose();
                       ho_ContCircle.Dispose();
                       ho_Cross.Dispose();
                       ho_Arrow11.Dispose();
                       ho_Rectangle1.Dispose();
                       ho_Arrow1.Dispose();
                       return false;
                   }
                   //获取点
                   hv_ROIRows = hv_Rows.Clone();
                   hv_ROICols = hv_Cols.Clone();
                   ho_Contour.Dispose();
                   HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows, hv_ROICols);
                   //用回归线法（不抛出异常点，所有点的权重一样）拟合圆
                   HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowC, out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                   CenterRow = hv_RowC;
                   CenterCol = hv_ColumnC;
                   CircleR = hv_Radius;
                   StartPhi = hv_StartPhi;
                   EndPhi = hv_EndPhi;
                   PointOrder = hv_PointOrder;
                   ho_ContCircle.Dispose();
                   HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ContCircle, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //获取圆或圆弧xld上的点坐标
                   HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);
                   //显示图像和圆弧
                   HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                   HOperatorSet.SetColor(hv_WindowHandle, "green");
                   HOperatorSet.DispObj(ho_ContCircle, hv_WindowHandle);
                   HOperatorSet.SetRgb(hv_WindowHandle, 255, 255, 255);
                   //产生并显示圆心
                   ho_Cross.Dispose();
                   HOperatorSet.GenCrossContourXld(out ho_Cross, hv_RowC, hv_ColumnC, 60, 0.785398);
                   HOperatorSet.SetColor(hv_WindowHandle, "green");
                   HOperatorSet.DispObj(ho_Cross, hv_WindowHandle);
                   HOperatorSet.SetRgb(hv_WindowHandle, 255, 255, 255);
                   disp_message(hv_WindowHandle, "2.远离圆心，画箭头确定检测的方向，点击右键确认", "window", 12, 12, "red", "false");
                   //画线，确定检测方向
                   HOperatorSet.SetColor(hv_WindowHandle, "green");
                   HOperatorSet.DrawLine(hv_WindowHandle, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2);
                   HOperatorSet.SetRgb(hv_WindowHandle, 255, 255, 255);
                   ho_Arrow11.Dispose();
                   GenArrowContourXld(out ho_Arrow11, hv_Row1, hv_Column1, hv_Row2, hv_Column2, 5, 5);
                   //求圆心到检测方向起点的距离
                   HOperatorSet.DistancePp(hv_RowC, hv_ColumnC, hv_Row1, hv_Column1, out hv_DistanceStart);
                   //求圆心到检测方向终点的距离
                   HOperatorSet.DistancePp(hv_RowC, hv_ColumnC, hv_Row2, hv_Column2, out hv_DistanceEnd);
                   //求圆或者圆弧XLD上点的数量
                   HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
                   //判断检测边缘的数量是否过少
                   if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
                   {
                       hv_ROIRows = new HTuple();
                       hv_ROICols = new HTuple();
                       disp_message(hv_WindowHandle, "检测边缘的数量太少，请重新设置", "window", 12, 12, "red", "false");
                       ho_ContOut1.Dispose();
                       ho_Contour.Dispose();
                       ho_ContCircle.Dispose();
                       ho_Cross.Dispose();
                       ho_Arrow11.Dispose();
                       ho_Rectangle1.Dispose();
                       ho_Arrow1.Dispose();
                       return false;
                   }
                   //如果XLD是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
                   //如果XLD是圆，有Length2个点，以0度为起点，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
                   for (hv_i = 0; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
                   {
                       if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(hv_Length2 - 1)))) != 0)//xld的起点和中点坐标相同，为圆
                       {                     
                           HOperatorSet.TupleInt((((1.0 * hv_Length2) / hv_Elements) * hv_i) + ((0.3 * hv_Length2) / (2 * hv_Elements)), out hv_j);
                           hv_ArcType = "circle";
                       }
                       else   //否则为圆弧
                       {                      
                           HOperatorSet.TupleInt((((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i) + ((0.3 * hv_Length2) / (2 * hv_Elements)), out hv_j);
                           hv_ArcType = "arc";
                       }
                       //索引越界，强制赋值为最后一个索引
                       if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                       {
                           hv_j = hv_Length2 - 1;
                           continue;
                       }
                       //获取卡尺工具中心
                       hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                       hv_ColE = hv_ColXLD.TupleSelect(hv_j);
                       //通过判断起点和中点到圆心的距离判断判断搜索方向
                       if ((int)(new HTuple(hv_DistanceStart.TupleGreater(hv_DistanceEnd))) != 0)
                       {
                           //求圆心指向边缘的矢量的角度
                           HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                           //角度反向
                           hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                           //边缘搜索方向类型：‘inner’搜索方向由圆外指向圆心；'outer'搜索方向由圆心指向圆外
                           hv_Direct = "inner";
                       }
                       else
                       {
                           HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                           hv_Direct = "outer";
                       }
                       //产生卡尺XLD,并保存到显示对象
                       ho_Rectangle1.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle1, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                       //用箭头XLD指示边缘搜索方向，并保持到显示对象
                       if (hv_i == 0)
                       {
                           hv_RowL2 = hv_RowE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                           hv_RowL1 = hv_RowE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                           hv_ColL2 = hv_ColE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                           hv_ColL1 = hv_ColE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                           ho_Arrow1.Dispose();
                           GenArrowContourXld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2, 25, 10);
                           OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                           SP_O++;
                           ho_Regions.Dispose();
                           HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Arrow1, out ho_Regions);
                           OTemp[SP_O - 1].Dispose();
                           SP_O = 0;
                       }
                   }
                   ho_ContOut1.Dispose();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Cross.Dispose();
                   ho_Arrow11.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_Arrow1.Dispose();
                   return true;
               }
               catch (HalconException HDevExpDefaultException)
               {
                   ho_ContOut1.Dispose();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Cross.Dispose();
                   ho_Arrow11.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_Arrow1.Dispose();
                   MessageBox.Show(HDevExpDefaultException.Message );
                   return false;
               }
               #endregion
           }
       }

        /// <summary>
        /// 圆的卡尺工具，找出用于圆拟合的拟合边界点
        /// </summary>
        /// <param name="ho_Image">           输入图像                     </param>
        /// <param name="ho_Regions">         输出生成的ROI轮廓            </param>
        /// <param name="viewIn">   显示窗口句柄                 </param>
        /// <param name="hv_Elements">        边界ROI的卡尺个数            </param>
        /// <param name="hv_DetectHeight">    检测边缘的高度               </param>
        /// <param name="hv_DetectWidth">     检测边缘的宽度               </param>
        /// <param name="hv_Sigma">           高斯滤波的参数               </param>
        /// <param name="hv_Threshold">       阈值                         </param>
        /// <param name="hv_Transition">      Transition = 'positive'，'negative'， 'all'，黑到白，白到黑 </param>
        /// <param name="hv_Select">          “all”measure_pos返回所有点，程序选择最大点；"First"返回第一边界点；"last" 返回最后一个点 </param>
        /// <param name="hv_ROIRows">         边界ROI的行坐标              </param>
        /// <param name="hv_ROICols">         边界ROI的列坐标              </param>
        /// <param name="hv_Direct">          边界点查找方向               </param>
        /// <param name="hv_ResultRow">        </param>
        /// <param name="hv_ResultColumn">     </param>
        /// <param name="hv_ArcType"></param>
        public static bool  spoke(HObject ho_Image, out HObject ho_Regions, ViewControl viewIn, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Sigma, HTuple hv_Threshold,
           HTuple hv_Transition, HTuple hv_Select, HTuple hv_ROIRows, HTuple hv_ROICols, HTuple hv_Direct, out HTuple hv_ResultRow, out HTuple hv_ResultColumn, out HTuple hv_ArcType)
       {
           lock (DispLockObj)
           {
               #region
               // Stack for temporary objects 
               HObject[] OTemp = new HObject[20];
               long SP_O = 0;
               // Local iconic variables 
               HObject ho_Contour, ho_ContCircle, ho_Rectangle1 = null;
               HObject ho_Arrow1 = null;
               // Local control variables 
               HTuple hv_Width, hv_Height, hv_RowC, hv_ColumnC;
               HTuple hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder;
               HTuple hv_RowXLD, hv_ColXLD, hv_Length2, hv_i, hv_j = new HTuple();
               HTuple hv_RowE = new HTuple(), hv_ColE = new HTuple(), hv_ATan = new HTuple();
               HTuple hv_RowL2 = new HTuple(), hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple();
               HTuple hv_ColL1 = new HTuple(), hv_MsrHandle_Measure = new HTuple();
               HTuple hv_RowEdge = new HTuple(), hv_ColEdge = new HTuple();
               HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
               HTuple hv_tRow = new HTuple(), hv_tCol = new HTuple(), hv_t = new HTuple();
               HTuple hv_Number = new HTuple();
               HTuple hv_ROICols_COPY_INP_TMP = hv_ROICols.Clone();
               HTuple hv_ROIRows_COPY_INP_TMP = hv_ROIRows.Clone();
               HTuple hv_Select_COPY_INP_TMP = hv_Select.Clone();
               HTuple hv_Transition_COPY_INP_TMP = hv_Transition.Clone();
               // Initialize local and output iconic variables 
               HOperatorSet.GenEmptyObj(out ho_Regions);
               HOperatorSet.GenEmptyObj(out ho_Contour);
               HOperatorSet.GenEmptyObj(out ho_ContCircle);
               HOperatorSet.GenEmptyObj(out ho_Rectangle1);
               HOperatorSet.GenEmptyObj(out ho_Arrow1);
               hv_ResultRow = new HTuple();
               hv_ResultColumn = new HTuple();
               hv_ArcType = new HTuple();
               try
               {
                   HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                   //1.产生一个现实对象用于显示
                   ho_Regions.Dispose();
                   HOperatorSet.GenEmptyObj(out ho_Regions);
                   //2.初始化边缘数组
                   hv_ResultRow = new HTuple();
                   hv_ResultColumn = new HTuple();
                   //3.产生xld
                   ho_Contour.Dispose();
                   HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows_COPY_INP_TMP, hv_ROICols_COPY_INP_TMP);
                   //用回归线法（不抛出异常点，所有点的权重一样）拟合圆
                   HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowC, out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                   //根据拟合结果产生xld，并保持到显示图像
                   ho_ContCircle.Dispose();
                   HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ContCircle, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //获取圆或圆弧xld上的点坐标
                   HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);
                   //显示图像和圆弧
                   //dev_display (Image)
                   //dev_display (ContCircle)
                   //产生并显示圆心
                   //gen_cross_contour_xld (Cross, RowC, ColumnC, 60, 0.785398)
                   //dev_display (Cross)
                   //disp_message (WindowHandle, '2.远离圆心，画箭头确定检测的方向，点击右键确认', 'window', 12, 12, 'red', 'false')
                   //*画线，确定检测方向
                   //draw_line (WindowHandle, Row1, Column1, Row2, Column2)
                   //gen_arrow_contour_xld (Arrow11, Row1, Column1, Row2, Column2, 5, 5)
                   //求圆心到检测方向起点的距离
                   //distance_pp (RowC, ColumnC, Row1, Column1, DistanceStart)
                   //求圆心到检测方向终点的距离
                   //distance_pp (RowC, ColumnC, Row2, Column2, DistanceEnd)
                   //求圆或者圆弧XLD上点的数量
                   HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
                   HTuple String00 = "检测边缘的数量太少，请重新设置";
                   //判断检测边缘的数量是否过少
                   if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
                   {
                       hv_ROIRows_COPY_INP_TMP = new HTuple();
                       hv_ROICols_COPY_INP_TMP = new HTuple();
                       /// disp_message(hv_ExpDefaultWinHandle, "检测边缘的数量太少，请重新设置", "window",       52, 12, "red", "false");
                       viewIn.SetString(100, 100, "red", String00.ToString());
                       ho_Contour.Dispose();
                       ho_ContCircle.Dispose();
                       ho_Rectangle1.Dispose();
                       ho_Arrow1.Dispose();
                       return false;
                   }
                   //如果XLD是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
                   //如果XLD是圆，有Length2个点，以0度为起点，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
                   for (hv_i = 0; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
                   {
                       if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(hv_Length2 - 1)))) != 0)
                       {
                           //xld的起点和中点坐标相同，为圆
                           HOperatorSet.TupleInt(((1.0 * hv_Length2) / hv_Elements) * hv_i, out hv_j);
                       }
                       else
                       {
                           //否则为圆弧
                           HOperatorSet.TupleInt(((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i, out hv_j);
                           hv_ArcType = "arc";
                       }
                       //索引越界，强制赋值为最后一个索引
                       if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                       {
                           hv_j = hv_Length2 - 1;
                          
                       }
                       //获取卡尺工具中心
                       hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                       hv_ColE = hv_ColXLD.TupleSelect(hv_j);
                       //超出图像区域不检测，否则容易报异常
                       if ((int)((new HTuple((new HTuple((new HTuple(hv_RowE.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowE.TupleLess(0))))).TupleOr(new HTuple(hv_ColE.TupleGreater(
                           hv_Width - 1))))).TupleOr(new HTuple(hv_ColE.TupleLess(0)))) != 0)
                       {
                           continue;
                       }
                       //边缘搜索方向类型：'inner'搜索方向由圆外指向圆心：‘outer’搜索方向由圆心指向圆外
                       if ((int)(new HTuple(hv_Direct.TupleEqual("inner"))) != 0)
                       {
                           //求卡尺工具的边缘搜索方向
                           //求圆心指向边缘的矢量角度
                           HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                           //角度反向
                           hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                       }
                       else
                       {
                           HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                       }
                       //产生卡尺XLD,并保存到显示对象
                       ho_Rectangle1.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle1, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                       //用箭头XLD指示边缘搜索方向，并保持到显示对象
                       //if (i=0)
                       hv_RowL2 = hv_RowE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                       hv_RowL1 = hv_RowE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                       hv_ColL2 = hv_ColE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                       hv_ColL1 = hv_ColE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                       ho_Arrow1.Dispose();
                       GenArrowContourXld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2, 25, 10);
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Arrow1, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                       //endif
                       //产生测量对象句柄
                       HOperatorSet.GenMeasureRectangle2(hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2, hv_Width, hv_Height, "nearest_neighbor", out hv_MsrHandle_Measure);
                       if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("negative"))) != 0)
                       {
                           hv_Transition_COPY_INP_TMP = "negative";
                       }
                       else
                       {
                           if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("positive"))) != 0)
                           {
                               hv_Transition_COPY_INP_TMP = "positive";
                           }
                           else
                           {
                               hv_Transition_COPY_INP_TMP = "all";
                           }
                       }
                       //设置边缘位置。最强点是从所有边缘中选择幅值绝对值最大点，需要设置为'all'
                       if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("first"))) != 0)
                       {
                           hv_Select_COPY_INP_TMP = "first";
                       }
                       else
                       {
                           if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("last"))) != 0)
                           {
                               hv_Select_COPY_INP_TMP = "last";
                           }
                           else
                           {
                               hv_Select_COPY_INP_TMP = "all";
                           }
                       }
                       HOperatorSet.MeasurePos(ho_Image, hv_MsrHandle_Measure, hv_Sigma, hv_Threshold, hv_Transition_COPY_INP_TMP, hv_Select_COPY_INP_TMP, out hv_RowEdge, out hv_ColEdge, out hv_Amplitude, out hv_Distance);
                       HOperatorSet.CloseMeasure(hv_MsrHandle_Measure);
                       hv_tRow = 0;
                       hv_tCol = 0;
                       //保存边缘幅度绝对值
                       hv_t = 0;
                       //找到的边缘至少为1个
                       HOperatorSet.TupleLength(hv_RowEdge, out hv_Number);
                       if ((int)(new HTuple(hv_Number.TupleLess(1))) != 0)
                       {
                           continue;
                       }
                       //有多个边缘时，选择幅度最大的边缘
                       for (hv_j = 0; hv_j.Continue(hv_Number - 1, 1); hv_j = hv_j.TupleAdd(1))
                       {
                           if ((int)(new HTuple(((((hv_Amplitude.TupleSelect(hv_j))).TupleAbs())).TupleGreater(
                               hv_t))) != 0)
                           {
                               hv_tRow = hv_RowEdge.TupleSelect(hv_j);
                               hv_tCol = hv_ColEdge.TupleSelect(hv_j);
                               hv_t = ((hv_Amplitude.TupleSelect(hv_j))).TupleAbs();
                           }
                       }
                       //把找到的边缘保存到输出数组
                       if ((int)(new HTuple(hv_t.TupleGreater(0))) != 0)
                       {
                           hv_ResultRow = hv_ResultRow.TupleConcat(hv_tRow);
                           hv_ResultColumn = hv_ResultColumn.TupleConcat(hv_tCol);
                       }
                   }
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_Arrow1.Dispose();
                   return  true;
               }
               catch (HalconException HDevExpDefaultException)
               {
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_Arrow1.Dispose();
                   Logger.PopError(HDevExpDefaultException.ToString() + HDevExpDefaultException.Source);
                   return false;
               }
               #endregion
           }
       }

       public static bool  gen_spoke_ROI(HObject ho_Image, out HObject ho_Regions, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_ROIRows, HTuple hv_ROICols,
                          HTuple hv_Direct)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_Contour, ho_ContCircle, ho_Rectangle1 = null;
           HObject ho_Arrow1 = null;
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_RowC, hv_ColumnC;
           HTuple hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder;
           HTuple hv_RowXLD, hv_ColXLD, hv_Length2, hv_WindowHandle1 = new HTuple();
           HTuple hv_i, hv_j = new HTuple(), hv_RowE = new HTuple(), hv_ColE = new HTuple();
           HTuple hv_ATan = new HTuple(), hv_RowL2 = new HTuple(), hv_RowL1 = new HTuple();
           HTuple hv_ColL2 = new HTuple(), hv_ColL1 = new HTuple();
           HTuple hv_ROICols_COPY_INP_TMP = hv_ROICols.Clone();
           HTuple hv_ROIRows_COPY_INP_TMP = hv_ROIRows.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_Contour);
           HOperatorSet.GenEmptyObj(out ho_ContCircle);
           HOperatorSet.GenEmptyObj(out ho_Rectangle1);
           HOperatorSet.GenEmptyObj(out ho_Arrow1);
           try
           {
               HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
               //1.产生一个现实对象用于显示
               ho_Regions.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Regions);
               //3.产生xld
               ho_Contour.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows_COPY_INP_TMP,  hv_ROICols_COPY_INP_TMP);
               //用回归线法（不抛出异常点，所有点的权重一样）拟合圆
               HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowC, out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
               //根据拟合结果产生xld，并保持到显示图像
               ho_ContCircle.Dispose();
               HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius,  hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
               OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
               SP_O++;
               ho_Regions.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ContCircle, out ho_Regions);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
               //获取圆或圆弧xld上的点坐标
               HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);
               //显示图像和圆弧
               //求圆或者圆弧XLD上点的数量
               HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
               //判断检测边缘的数量是否过少
               if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
               {
                   hv_ROIRows_COPY_INP_TMP = new HTuple();
                   hv_ROICols_COPY_INP_TMP = new HTuple();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_Arrow1.Dispose();
                   return false; 
               }
               //如果XLD是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
               //如果XLD是圆，有Length2个点，以0度为起点，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
               for (hv_i = 0; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
               {
                   if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect( hv_Length2 - 1)))) != 0)
                   {
                       //xld的起点和中点坐标相同，为圆
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / hv_Elements) * hv_i, out hv_j);
                   }
                   else
                   {
                       //否则为圆弧
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i, out hv_j);
                   }
                   //索引越界，强制赋值为最后一个索引
                   if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                   {
                       hv_j = hv_Length2 - 1;
                       continue;
                   }
                   //获取卡尺工具中心
                   hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                   hv_ColE = hv_ColXLD.TupleSelect(hv_j);
                   //超出图像区域不检测，否则容易报异常
                   if ((int)((new HTuple((new HTuple((new HTuple(hv_RowE.TupleGreater(hv_Height - 1))).TupleOr( new HTuple(hv_RowE.TupleLess(0))))).TupleOr(new HTuple(hv_ColE.TupleGreater(
                       hv_Width - 1))))).TupleOr(new HTuple(hv_ColE.TupleLess(0)))) != 0)
                   {
                       continue;
                   }
                   //边缘搜索方向类型：'inner'搜索方向由圆外指向圆心：‘outer’搜索方向由圆心指向圆外
                   if ((int)(new HTuple(hv_Direct.TupleEqual("inner"))) != 0)
                   {
                       //求卡尺工具的边缘搜索方向
                       //求圆心指向边缘的矢量角度
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                       //角度反向
                       hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                   }
                   else
                   {
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                   }
                   //产生卡尺XLD,并保存到显示对象
                   ho_Rectangle1.Dispose();
                   HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle1, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   //用箭头XLD指示边缘搜索方向，并保持到显示对象
                   if ((int)((new HTuple(hv_i.TupleEqual(0))).TupleOr(new HTuple(((hv_i % 4)).TupleEqual(0)))) != 0)
                   {
                       hv_RowL2 = hv_RowE + (((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin())) * 1.5);
                       hv_RowL1 = hv_RowE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                       hv_ColL2 = hv_ColE + (((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos())) * 1.5);
                       hv_ColL1 = hv_ColE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                       ho_Arrow1.Dispose();
                       GenArrowContourXld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2, 5, 5);

                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Arrow1, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                   }
               }
               ho_Contour.Dispose();
               ho_ContCircle.Dispose();
               ho_Rectangle1.Dispose();
               ho_Arrow1.Dispose();
               return true ;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_Contour.Dispose();
               ho_ContCircle.Dispose();
               ho_Rectangle1.Dispose();
               ho_Arrow1.Dispose();
               Logger.Pop(HDevExpDefaultException.Message + HDevExpDefaultException.Source);
               return false;            
           }
       }

       public static bool VectorAngleToTransPt(HTuple Row1, HTuple Col1, HTuple Angle1, HTuple Row2, HTuple Col2, HTuple Angle2, List<double> InRows, List<double> InCols,
                                                out  List<double> OutRows, out  List<double> OutCols)
       {
           OutRows = new List<double>();
           OutCols = new List<double>();
           HTuple HomMat = new HTuple();
           HOperatorSet.VectorAngleToRigid(Row1, Col1, Angle1, Row2, Col2, Angle2, out HomMat);
           HTuple InRow = new HTuple(), InCol = new HTuple();
           HTuple OutRow = new HTuple(), OutCol = new HTuple();
           ListDoubToHtuple(InRows, out InRow);
           ListDoubToHtuple(InCols, out InCol);
           HOperatorSet.AffineTransPoint2d(HomMat, InRow, InCol, out OutRow, out OutCol);
           HTupleToList(OutRow, out OutRows);
           HTupleToList(OutCol, out OutCols);

           return true;
       }


       public static void list_image_files(HTuple hv_ImageDirectory, HTuple hv_Extensions, HTuple hv_Options, out HTuple hv_ImageFiles)
       {
           // Local control variables 
           HTuple hv_HalconImages, hv_OS, hv_Directories;
           HTuple hv_Index, hv_FileExists = new HTuple(), hv_AllFiles = new HTuple();
           HTuple hv_i = new HTuple(), hv_Selection = new HTuple();

           HTuple hv_Extensions_COPY_INP_TMP = hv_Extensions.Clone();
           HTuple hv_ImageDirectory_COPY_INP_TMP = hv_ImageDirectory.Clone();

           // Initialize local and output iconic variables 
           //This procedure returns all files in a given directory
           //with one of the suffixes specified in Extensions.
           //
           //input parameters:
           //ImageDirectory: as the name says
           //   If a tuple of directories is given, only the images in the first
           //   existing directory are returned.
           //   If a local directory is not found, the directory is searched
           //   under %HALCONIMAGES%/ImageDirectory. If %HALCONIMAGES% is not set,
           //   %HALCONROOT%/images is used instead.
           //Extensions: A string tuple containing the extensions to be found
           //   e.g. ['png','tif',jpg'] or others
           //If Extensions is set to 'default' or the empty string '',
           //   all image suffixes supported by HALCON are used.
           //Options: as in the operator list_files, except that the 'files'
           //   option is always used. Note that the 'directories' option
           //   has no effect but increases runtime, because only files are
           //   returned.
           //
           //output parameter:
           //ImageFiles: A tuple of all found image file names
           //
           if ((int)((new HTuple((new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
               new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
               "default")))) != 0)
           {
               hv_Extensions_COPY_INP_TMP = new HTuple();
               hv_Extensions_COPY_INP_TMP[0] = "ima";
               hv_Extensions_COPY_INP_TMP[1] = "bmp";
               hv_Extensions_COPY_INP_TMP[2] = "jpg";
               hv_Extensions_COPY_INP_TMP[3] = "png";
               hv_Extensions_COPY_INP_TMP[4] = "tiff";
               hv_Extensions_COPY_INP_TMP[5] = "tif";
               hv_Extensions_COPY_INP_TMP[6] = "gif";
               hv_Extensions_COPY_INP_TMP[7] = "jpeg";
               hv_Extensions_COPY_INP_TMP[8] = "pcx";
               hv_Extensions_COPY_INP_TMP[9] = "pgm";
               hv_Extensions_COPY_INP_TMP[10] = "ppm";
               hv_Extensions_COPY_INP_TMP[11] = "pbm";
               hv_Extensions_COPY_INP_TMP[12] = "xwd";
               hv_Extensions_COPY_INP_TMP[13] = "pnm";
           }
           if ((int)(new HTuple(hv_ImageDirectory_COPY_INP_TMP.TupleEqual(""))) != 0)
           {
               hv_ImageDirectory_COPY_INP_TMP = ".";
           }
           HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
           HOperatorSet.GetSystem("operating_system", out hv_OS);
           if ((int)(new HTuple((((hv_OS.TupleStrFirstN(2)).TupleStrLastN(0))).TupleEqual(
               "Win"))) != 0)
           {
               hv_HalconImages = hv_HalconImages.TupleSplit(";");
           }
           else
           {
               hv_HalconImages = hv_HalconImages.TupleSplit(":");
           }
           hv_Directories = new HTuple();
           hv_Directories = hv_Directories.TupleConcat(hv_ImageDirectory_COPY_INP_TMP);
           hv_Directories = hv_Directories.TupleConcat((hv_HalconImages + "/") + hv_ImageDirectory_COPY_INP_TMP);
           hv_ImageFiles = new HTuple();
           for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Directories.TupleLength()
               )) - 1); hv_Index = (int)hv_Index + 1)
           {
               HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
               if ((int)(hv_FileExists) != 0)
               {
                   HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HTuple("files")).TupleConcat(
                       hv_Options), out hv_AllFiles);
                   hv_ImageFiles = new HTuple();
                   for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Extensions_COPY_INP_TMP.TupleLength()
                       )) - 1); hv_i = (int)hv_i + 1)
                   {
                       HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                           hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                       hv_ImageFiles = hv_ImageFiles.TupleConcat(hv_Selection);
                   }
                   HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("\\\\")).TupleConcat(
                       "replace_all"), "/", out hv_ImageFiles);
                   HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("//")).TupleConcat(
                       "replace_all"), "/", out hv_ImageFiles);

                   return;
               }
           }

           return;
       }

       public static void HtupleAddValue(HTuple ValueIn, HTuple Add, out HTuple OutValue)
       {
           OutValue = new HTuple();
           for (int i = 0; i < ValueIn.TupleLength(); i++)
           {
               OutValue[i] = ValueIn[i] + Add[0].D;
           
           }

       }


       /// <summary>
       /// 定位圆形区域的中心
       /// </summary>
       /// <param name="Img">输入图像</param>
       /// <param name="MinGray">阈值</param>
       /// <param name="MaxGray">阈值</param>
       /// <param name="Radius">亮斑半径</param>
       /// <param name="Row">中心行坐标</param>
       /// <param name="Col">中心列坐标</param>
       public static void FindCircleCenter(HObject Img, HTuple MinGray, HTuple MaxGray, HTuple Radius, out HTuple Row, out HTuple Col)
       {
           Row = new HTuple();
           Col = new HTuple();
           HObject Region = new HObject();
           HObject ConnectedRegions = new HObject();
           HObject SelectedRegions = new HObject();
           HObject SelectedRegions1 = new HObject();
           HObject RegionFillUp = new HObject();
           HObject Contours = new HObject();
           HTuple StartPhi = new HTuple(), EndPhi = new HTuple(), PointOrder = new HTuple();
           HTuple RegionDist = new HTuple(), RegionRound = new HTuple(), RegionSigma = new HTuple(), RegionSide = new HTuple();
           HOperatorSet.Threshold(Img, out Region, MinGray, MaxGray);
           HObject SelectedRegions2 = new HObject(), SelectedRegions3 = new HObject();
           HOperatorSet.Connection(Region, out ConnectedRegions);
           //HOperatorSet.SelectShape(ConnectedRegions, out SelectedRegions, "width", "and", 130,Radius);
           HOperatorSet.SelectShape(ConnectedRegions, out SelectedRegions, "width", "and", Radius - 50, Radius + 50);
           HOperatorSet.SelectShape(ConnectedRegions, out SelectedRegions1, "height", "and", Radius - 50, Radius + 50);
           HOperatorSet.SelectShape(SelectedRegions1, out SelectedRegions2, "area", "and", (3.14 * (Radius) * (Radius) / 4 - 5000), (3.14 * (Radius) * (Radius) / 4 + 5000));
           HOperatorSet.Roundness(SelectedRegions2, out RegionDist, out RegionSigma, out RegionRound, out RegionSide);
           bool IsNull;
           for (int i = 0; i < (RegionRound.TupleLength()); i++)
           {
               if (RegionRound[i] > 0.90) SelectedRegions3 = SelectedRegions2[i + 1];
           }
           IsNull = SelectedRegions3.IsInitialized();
           if (IsNull)
           {
               HOperatorSet.FillUp(SelectedRegions3, out RegionFillUp);
               HOperatorSet.GenContourRegionXld(RegionFillUp, out Contours, "border");
               HOperatorSet.FitCircleContourXld(Contours, "algebraic", -1, 0, 0, 3, 10, out Row, out Col, out  Radius, out StartPhi, out  EndPhi, out PointOrder);
           }
           else
           {
               Row = 0;
               Col = 0;
               Radius = 0;
               StartPhi = 0;
               EndPhi = 0;
               PointOrder = 0;
           }
           Region.Dispose();
           ConnectedRegions.Dispose();
           SelectedRegions.Dispose();
           SelectedRegions1.Dispose();
           RegionFillUp.Dispose();
           Contours.Dispose();
           SelectedRegions2.Dispose();
           SelectedRegions3.Dispose();
       }


       public static void AdjUpHandEyeMat(ST_UpCamMarkPos LastMark2PosP, ST_UpCamMarkPos NowMark2PosP ,MyHomMat2D  LastHandEyeMat ,out MyHomMat2D NewHandEyeHomMat  )
       {
           NewHandEyeHomMat = new MyHomMat2D();
           HTuple Px=new HTuple(),Py =new HTuple(),Qx =new HTuple(),Qy =new HTuple();
           Qx[0]=LastMark2PosP.MarkPt2D1.Col;
           Qx[1] = LastMark2PosP.MarkPt2D2.Col;
           Qy[0] = LastMark2PosP.MarkPt2D1.Row;
           Qy[1] = LastMark2PosP.MarkPt2D2.Row;
           Qx[0] = NowMark2PosP.MarkPt2D1.Col;
           Px[1] = NowMark2PosP.MarkPt2D2.Col;
           Py[0] = NowMark2PosP.MarkPt2D1.Row;
           Py[1] = NowMark2PosP.MarkPt2D2.Row;
           HTuple LastToNowEyeMat = new HTuple();
           //计算
           HOperatorSet.VectorToRigid(Px, Py, Qx, Qy, out LastToNowEyeMat);
           HTuple P1x = new HTuple(), P1y = new HTuple(), P2x = new HTuple(), P2y = new HTuple(),P3x =new HTuple(),P3y =new HTuple() ;
           for (int i = 0; i < 3; i++)
           {
               for (int j = 0; j < 3; j++)
               {
                   P1x[i * 3 + j] = 800 * i;
                   P1y[i * 3 + j] = 800 * j;
               }
           }
           HOperatorSet.AffineTransPoint2d(LastToNowEyeMat, P1x, P1y, out P2x, out P2y);
           HTuple HandEyeMat0 =new HTuple();
           MyHomMatToHalcon(LastHandEyeMat, out  HandEyeMat0);
           HOperatorSet.AffineTransPoint2d(HandEyeMat0, P2x, P2y, out P3x, out P3y);
           HTuple hvNowHandEyeHomMat =new HTuple();
           HOperatorSet.VectorToHomMat2d(P1x, P1y, P3x, P3y, out hvNowHandEyeHomMat);
           HalconToMyHomMat(hvNowHandEyeHomMat, out  NewHandEyeHomMat);              
       }


       public static bool Calculate2PtPos(double Px ,double Py ,double Qx,double Qy, out double Wx,out double Wy ,out  double WoTheta) 
       {
           Wx = 0;
           Wy = 0;
           WoTheta = 0;
           HTuple  Theta =new HTuple();
           HOperatorSet.AngleLl(0, 0, 1, 0, Px, Py, Qx, Qy, out Theta);
           WoTheta = Theta.D;
           Wx = (Qx + Px) / 2;
           Wy = (Px + Py) / 2;
           return true;
       
       }

       public static void  ArrayPts(ref  HTuple Cols,ref HTuple Rows)
       {
           HTuple Cols0 = Cols;
           HTuple Rows0 = Rows;
           HTuple OutCols = new HTuple();
           HTuple OutRows = new HTuple();

           HTuple RowMinIndex = new HTuple();
           GetMinIndex(Rows0, out  RowMinIndex);
           for (int i = 0; i < Cols.TupleLength(); i++)
           {
               GetMinIndex(Rows0, out  RowMinIndex);
               HTuple Remove1Rows = Rows0.TupleRemove(RowMinIndex);
               HTuple Remove1Cols = Cols0.TupleRemove(RowMinIndex);
               GetMinIndex(Remove1Cols, out  RowMinIndex);
               HTuple Remove2Cols = OutCols.TupleRemove(RowMinIndex);
               
           }
     
       
       }

       public static void GetMinIndex(HTuple HtupleIN ,out HTuple  MinIndex )
     {

         MinIndex = new HTuple();
         HTuple Min = HtupleIN.TupleMin();
         MinIndex = HtupleIN.TupleFind(Min);

     }


       public static void AdjPt(List<Point2Db> Ptin, out List<Point2Db> OutPt)
       {
           OutPt = new List<Point2Db>();
           for (int i = 0; i < Ptin.Count(); i++)
           {
               Point2Db pT = new Point2Db();
               OutPt.Add(pT);

           }

           List<double> Now = new List<double>();
           Now.Add(Ptin[0].Col);
           Now.Add(Ptin[1].Col);
           Now.Add(Ptin[2].Col);

           // int Index = Now.FindIndex(   Now.Max());
           int T1, T2, T3;
           T1 = 0;
           T2 = 1;
           T3 = 2;

           Array0(Ptin[0].Col, Ptin[1].Col, Ptin[2].Col, out T1, out T2, out T3);
          
           OutPt[0] = Ptin[T1];
           OutPt[1] = Ptin[T2];
           OutPt[2] = Ptin[T3];

           Array0(Ptin[3].Col, Ptin[4].Col, Ptin[5].Col, out T1, out T2, out T3);

           OutPt[3] = Ptin[3+T1];
           OutPt[4] = Ptin[3+T2];
           OutPt[5] = Ptin[3+T3];

           Array0(Ptin[6].Col, Ptin[7].Col, Ptin[8].Col, out T1, out T2, out T3);

           OutPt[6] = Ptin[6 + T1];
           OutPt[7] = Ptin[6 + T2];
           OutPt[8] = Ptin[6 + T3];
       }

       public static void Calcute2PtCenterAngle(Point2Db Mark1Pos, Point2Db Mark2Pos, out HTuple CenterX, out HTuple CenterY, out HTuple AngleLx)
       {
           CenterX = 0;
           CenterY = 0;
           AngleLx = 0;

           HOperatorSet.AngleLl(0, 0, 1, 0, Mark1Pos.Col, Mark1Pos.Row, Mark2Pos.Col, Mark2Pos.Row, out AngleLx);
           CenterX = (Mark1Pos.Col + Mark2Pos.Col) / 2;
           CenterY = (Mark1Pos.Row + Mark2Pos.Row) / 2;
       }

       public static void Array0( double Col1,double Col2,double Col3,out int T1 ,out int T2,out int T3  )
       {
           T1 = 0;
           T2 = 1;
           T3 = 2;
           if (Col1 > Col2 && Col2 > Col3)
           {
               T1 = 0;
               T2 = 1;
               T3 = 2;         
           }

           if (Col2 > Col1 && Col1> Col3)
           {
               T1 = 1;
               T2 = 0;
               T3 = 2;
           }
           if (Col3 > Col1 && Col1 > Col2)
           {
               T1 = 1;
               T2 = 2;
               T3 = 0;
           }

           if (Col1 > Col3 && Col3 > Col2)
           {
               T1 = 0;
               T2 = 2;
               T3 = 1;
           }

           if (Col2 > Col3 && Col3 > Col1)
           {
               T1 = 2;
               T2 = 0;
               T3 = 1;
           }
           if (Col3 > Col2 && Col2 > Col1)
           {
               T1 = 2;
               T2 = 1;
               T3 = 0;
           }

       }

       public static void GetSx(MyHomMat2D HomMat,out double Sx,out double Sy)
       {
           Sx = 0;
           Sy = 0;
           HTuple HvHomMat = new HTuple();
           MyHomMatToHalcon(HomMat,out  HvHomMat);
           HTuple Sx0,Sy0,Pi,Tx,Ty,Theta;
           HOperatorSet.HomMat2dToAffinePar(HvHomMat, out Sx0, out Sy0, out  Pi, out Tx, out  Ty, out  Theta);
           Sx = Sx0.D;
           Sy = Sy0.D;
      }


       //public static void ParaToString(bool isLeftStation, int feederIndex, int index, St_Position teachPos, MyHomMat2D CurHandEyeMat,
       //    TapeVisionBase.Matching.MatchingResult maResult, St_ImageVisionParam param, double Wx, double Wy, double Wtheta, out string MsgList)
       //{
       //    MsgList = "";
       //    try
       //    {
       //        #region    
       //        MsgList +=string.Format(("\nisLeftStation   " + isLeftStation.ToString()));
       //        MsgList +=string.Format(("\nfeederIndex   " + feederIndex.ToString()));
       //        MsgList +=string.Format(("\nindex   " + index.ToString()));
       //        MsgList +=string.Format(("\nteachPos   "));
       //        MsgList +=string.Format(("\nteachPos.CCDX   " + teachPos.CCDX.ToString("f2")));
       //        MsgList +=string.Format(("\nteachPos.X   " + teachPos.X.ToString("f2")));
       //        MsgList +=string.Format(("\nteachPos.Y   " + teachPos.Y.ToString("f2")));
       //        MsgList +=string.Format(("\nteachPos.Z   " + teachPos.Z.ToString("f2")));
       //        MsgList +=string.Format(("\nteachPos.R   " + teachPos.R.ToString("f2")));
       //        MsgList +=string.Format(("\nCurHandEyeMat   "));
       //        MsgList +=string.Format(("\nCurHandEyeMat.c00   " + CurHandEyeMat.c00.ToString()));
       //        MsgList +=string.Format(("\nCurHandEyeMat.c01   " + CurHandEyeMat.c01.ToString()));
       //        MsgList +=string.Format(("\nCurHandEyeMat.c02   " + CurHandEyeMat.c02.ToString()));
       //        MsgList +=string.Format(("\nCurHandEyeMat.c10   " + CurHandEyeMat.c10.ToString()));
       //        MsgList +=string.Format(("\nCurHandEyeMat.c11   " + CurHandEyeMat.c11.ToString()));
       //        MsgList +=string.Format(("\nCurHandEyeMat.c12   " + CurHandEyeMat.c12.ToString()));
       //        MsgList +=string.Format(("\nparam   "));
       //        MsgList +=string.Format(("\nparam.Setting.IsFindCircle   " + param.Setting.IsFindCircle.ToString()));
       //        MsgList +=string.Format(("\nparam.Setting.IsFindLine   " + param.Setting.IsFindLine.ToString()));
       //        MsgList +=string.Format(("\nWx  " + Wx.ToString()));
       //        MsgList +=string.Format(("\nWy  " + Wy.ToString()));
       //        MsgList +=string.Format(("\nWtheta  " + Wtheta.ToString()));
       //        MsgList +=string.Format(("\nmaResult   "));
       //        if ((!maResult.mCol.Equals( null)) && !(maResult.mRow == null) && !(maResult.mAngle == null))
       //        {
       //            MsgList +=string.Format(("\nmaResult.mCol   " + maResult.mCol.ToString()));
       //            MsgList +=string.Format(("\nmaResult.mRow   " + maResult.mRow.ToString()));
       //            MsgList +=string.Format(("\nmaResult.mAngle   " + maResult.mAngle.ToString()));

       //        }
       //        #endregion
       //    }
       //    catch(Exception e0)
       //    {
       //        MsgList += string.Format(("\n" + e0.Message + e0.Source));
       //    }
       //}


       public static void ParaToString( MyHomMat2D CurHandEyeHomMat, bool IsUp, Point2Db CurMotionPos, St_CirclesParam TeachCirclePara0, HTuple Row1, HTuple Col1, HTuple Theta1,
                                    HTuple Row2, HTuple Col2, HTuple Theta2,  double CenterRow,  double CenterCol,  double RotAngle ,out List<string>  MsgList)
       {
           MsgList = new List<string>();
           MsgList.Add("CurHandEyeHomMat   ");
           MsgList.Add("CurHandEyeHomMat.c00   " + CurHandEyeHomMat.c00.ToString());
           MsgList.Add("CurHandEyeHomMat.c01   " + CurHandEyeHomMat.c01.ToString());
           MsgList.Add("CurHandEyeHomMat.c02   " + CurHandEyeHomMat.c02.ToString());
           MsgList.Add("CurHandEyeHomMat.c10   " + CurHandEyeHomMat.c10.ToString());
           MsgList.Add("CurHandEyeHomMat.c11   " + CurHandEyeHomMat.c11.ToString());
           MsgList.Add("CurHandEyeHomMat.c12   " + CurHandEyeHomMat.c12.ToString());
           MsgList.Add("IsUp   " + IsUp.ToString());
           MsgList.Add("CurMotionPos   ");
           MsgList.Add("CurMotionPos.Row   " + CurMotionPos.Row.ToString());
           MsgList.Add("CurMotionPos.Col   " + CurMotionPos.Col.ToString());
           MsgList.Add("TeachCirclePara0   ");
           MsgList.Add("TeachCirclePara0.CenterCols   " + TeachCirclePara0.CenterCols.ToString());
           MsgList.Add("TeachCirclePara0.CenterRows   " + TeachCirclePara0.CenterRows.ToString());

           
           MsgList.Add("Row1  " + Row1.ToString());
           MsgList.Add("Col1  " + Col1.ToString());
           MsgList.Add("Theta1  " + Theta1.ToString());

           MsgList.Add("Row2  " + Row2.ToString());
           MsgList.Add("Col2  " + Col2.ToString());
           MsgList.Add("Theta2  " + Theta2.ToString());

           MsgList.Add("CenterRow  " + CenterRow.ToString());
           MsgList.Add("CenterCol  " + CenterCol.ToString());
           MsgList.Add("RotAngle  " + RotAngle.ToString());
       }


       /// <summary>
       /// 生成卡尺区域，分析卡尺区域的的灰度
       /// </summary>
       /// <param name="ho_Image">图像输入</param>
       /// <param name="ho_Regions">生成检测区域的集合</param>
       /// <param name="ho_Contours">ROI区域的轮廓</param>
       /// <param name="hv_Elements">检测区域的个数</param>
       /// <param name="hv_DetectHeight">ROI的高度</param>
       /// <param name="hv_DetectWidth">ROI的宽度</param>
       /// <param name="hv_Row1">起点Row</param>
       /// <param name="hv_Column1">起点Col</param>
       /// <param name="hv_Row2">终点Row</param>
       /// <param name="hv_Column2">终点Col</param>
       /// <returns></returns>
       public static bool gen_rake_ROI1(HObject ho_Image, out HObject ho_Regions, out HObject ho_Contours, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_RegionLines, ho_Rectangle = null;
           HObject ho_RectRegion = null;
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_ATan, hv_i;
           HTuple hv_RowC = new HTuple(), hv_ColC = new HTuple(), hv_Distance = new HTuple();
           HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_Contours);
           HOperatorSet.GenEmptyObj(out ho_RegionLines);
           HOperatorSet.GenEmptyObj(out ho_Rectangle);
           HOperatorSet.GenEmptyObj(out ho_RectRegion);
           try
           {
               //1.获取图像大小
               HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
               ho_Regions.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Regions);
               ho_Contours.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Contours);
               //2.生成所画的图像并保存为对象
               ho_RegionLines.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_RegionLines, hv_Row1.TupleConcat(hv_Row2), hv_Column1.TupleConcat(hv_Column2));
               OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
               SP_O++;
               ho_Contours.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RegionLines, out ho_Contours);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
               //3.计算所画直线的角度
               HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);
               hv_ATan = hv_ATan + ((new HTuple(90)).TupleRad());
               //4.生成卡尺工具的检测区域
               for (hv_i = 1; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
               {
                   //4.1如果只有一个测量矩形，作为卡尺工具，宽度为检测为直线的长度
                   if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
                   {
                       hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                       hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                       // if(RowC>Height-1 or RowC<0 or ColC>Width-1 or ColC<0 )
                       if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                       {
                           continue;
                       }
                       HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                       hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                       ho_Rectangle.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_Distance / 50);
                       ho_RectRegion.Dispose();
                       HOperatorSet.GenRectangle2(out ho_RectRegion, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_Distance / 50);
                       OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
                       SP_O++;
                       ho_Contours.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Contours);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RectRegion, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                   }
                   //4.2如果有多个测量矩形，产生该测量矩形的XLD
                   else
                   {
                       hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                       hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                       //4.3超出图像区域，跳出当前循环
                       //if(RowC>Height-1 or RowC<0 or ColC>Width-1 or ColC<0 )
                       if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                       {
                           continue;
                       }
                       ho_Rectangle.Dispose();
                       HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                       ho_RectRegion.Dispose();
                       HOperatorSet.GenRectangle2(out ho_RectRegion, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                       OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
                       SP_O++;
                       ho_Contours.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Contours);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                       OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                       SP_O++;
                       ho_Regions.Dispose();
                       HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RectRegion, out ho_Regions);
                       OTemp[SP_O - 1].Dispose();
                       SP_O = 0;
                   }
               }
               ho_RegionLines.Dispose();
               ho_Rectangle.Dispose();
               ho_RectRegion.Dispose();
               return true;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_RegionLines.Dispose();
               ho_Rectangle.Dispose();
               ho_RectRegion.Dispose();
               Logger.PopError(HDevExpDefaultException.Message + HDevExpDefaultException.Source);
               return false;
           }
       }



        /// <summary>
        /// 生成卡尺区域，分析卡尺区域的的灰度
        /// </summary>
        /// <param name="ho_Regions">生成检测区域的集合</param>
        /// <param name="ho_Contours">ROI区域的轮廓</param>
        /// <param name="hv_Elements">检测区域的个数</param>
        /// <param name="hv_DetectHeight">ROI的高度</param>
        /// <param name="hv_DetectWidth">ROI的宽度</param>
        /// <param name="hv_Row1">起点Row</param>
        /// <param name="hv_Column1">起点Col</param>
        /// <param name="hv_Row2">终点Row</param>
        /// <param name="hv_Column2">终点Col</param>
        /// <returns></returns>
        public static bool gen_rake_ROI2( out HObject ho_Regions, out HObject ho_Contours, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth,
                                              HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;
            // Local iconic variables 
            HObject ho_RegionLines, ho_Rectangle = null;
            HObject ho_RectRegion = null;
            // Local control variables 
            HTuple  hv_ATan, hv_i;
            HTuple hv_RowC = new HTuple(), hv_ColC = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RectRegion);
            try
            {
                ho_Regions.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Regions);
                ho_Contours.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Contours);
                //2.生成所画的图像并保存为对象
                ho_RegionLines.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_RegionLines, hv_Row1.TupleConcat(hv_Row2), hv_Column1.TupleConcat(hv_Column2));
                OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
                SP_O++;
                ho_Contours.Dispose();
                HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RegionLines, out ho_Contours);
                OTemp[SP_O - 1].Dispose();
                SP_O = 0;
                //3.计算所画直线的角度
                HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);
                hv_ATan = hv_ATan + ((new HTuple(90)).TupleRad());
                //4.生成卡尺工具的检测区域
                for (hv_i = 1; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
                {
                    //4.1如果只有一个测量矩形，作为卡尺工具，宽度为检测为直线的长度
                    if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
                    {
                        hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                        hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                        // if(RowC>Height-1 or RowC<0 or ColC>Width-1 or ColC<0 )
                        //if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                        //{
                        //    continue;
                        //}
                        HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                        hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                        ho_Rectangle.Dispose();
                        HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_Distance / 50);
                        ho_RectRegion.Dispose();
                        HOperatorSet.GenRectangle2(out ho_RectRegion, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_Distance / 50);
                        OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
                        SP_O++;
                        ho_Contours.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Contours);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                        OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                        SP_O++;
                        ho_Regions.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RectRegion, out ho_Regions);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                    //4.2如果有多个测量矩形，产生该测量矩形的XLD
                    else
                    {
                        hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                        hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                        //4.3超出图像区域，跳出当前循环
                        //if(RowC>Height-1 or RowC<0 or ColC>Width-1 or ColC<0 )
                        //if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                        //{
                        //    continue;
                        //}
                        ho_Rectangle.Dispose();
                        HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                        ho_RectRegion.Dispose();
                        HOperatorSet.GenRectangle2(out ho_RectRegion, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                        OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
                        SP_O++;
                        ho_Contours.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle, out ho_Contours);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                        OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                        SP_O++;
                        ho_Regions.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RectRegion, out ho_Regions);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                }
                ho_RegionLines.Dispose();
                ho_Rectangle.Dispose();
                ho_RectRegion.Dispose();
                return true;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_RegionLines.Dispose();
                ho_Rectangle.Dispose();
                ho_RectRegion.Dispose();
                Logger.PopError(HDevExpDefaultException.Message + HDevExpDefaultException.Source);
                return false;
            }
        }


        /// <summary>
        /// 生成卡尺区域，分析卡尺区域的的灰度
        /// </summary>
        /// <param name="ho_Regions">生成检测区域的集合</param>
        /// <param name="ho_Contours">ROI区域的轮廓</param>
        /// <param name="hv_Elements">检测区域的个数</param>
        /// <param name="hv_DetectHeight">ROI的高度</param>
        /// <param name="hv_DetectWidth">ROI的宽度</param>
        /// <param name="hv_Row1">起点Row</param>
        /// <param name="hv_Column1">起点Col</param>
        /// <param name="hv_Row2">终点Row</param>
        /// <param name="hv_Column2">终点Col</param>
        /// <returns></returns>
        public static bool gen_rake_ROI3(out HObject ho_Regions,  HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth,
                                              HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;
            // Local iconic variables 
            HObject ho_RectRegion = null;
            // Local control variables 
            HTuple hv_ATan, hv_i;
            HTuple hv_RowC = new HTuple(), hv_ColC = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_RectRegion);
            try
            {
                ho_Regions.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Regions);
                //2.生成所画的图像并保存为对象

                //3.计算所画直线的角度
                HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);
                hv_ATan = hv_ATan + ((new HTuple(90)).TupleRad());
                //4.生成卡尺工具的检测区域
                for (hv_i = 1; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
                {
                    //4.1如果只有一个测量矩形，作为卡尺工具，宽度为检测为直线的长度
                    if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
                    {
                        hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                        hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                        // if(RowC>Height-1 or RowC<0 or ColC>Width-1 or ColC<0 )
                        //if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                        //{
                        //    continue;
                        //}
                        HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                        hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                        ho_RectRegion.Dispose();
                        HOperatorSet.GenRectangle2(out ho_RectRegion, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_Distance / 50);
                        SP_O = 0;
                        OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                        SP_O++;
                        ho_Regions.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RectRegion, out ho_Regions);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                    //4.2如果有多个测量矩形，产生该测量矩形的XLD
                    else
                    {
                        hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                        hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                        //4.3超出图像区域，跳出当前循环
                        //if(RowC>Height-1 or RowC<0 or ColC>Width-1 or ColC<0 )
                        //if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                        //{
                        //    continue;
                        //}
                        HOperatorSet.GenRectangle2(out ho_RectRegion, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                        SP_O = 0;
                        OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                        SP_O++;
                        ho_Regions.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RectRegion, out ho_Regions);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                }
                ho_RectRegion.Dispose();
                return true;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_RectRegion.Dispose();
                Logger.PopError(HDevExpDefaultException.Message + HDevExpDefaultException.Source);
                return false;
            }
        }


        /// <summary>
        /// 生成圆弧卡尺工具，分析卡尺区域的灰度
        /// </summary>
        /// <param name="ho_Image">图像输入</param>
        /// <param name="ho_Regions">生成的检测区域的集合</param>
        /// <param name="ho_Contours">ROI区域的轮廓</param>
        /// <param name="hv_Elements">检测区域的个数</param>
        /// <param name="hv_DetectHeight">ROI的高度</param>
        /// <param name="hv_DetectWidth">ROI的宽度</param>
        /// <param name="hv_ROIRows">起点Row</param>
        /// <param name="hv_ROICols">起点Col</param>
        /// <param name="hv_Direct">终点Row</param>
        /// <param name="hv_AddR">终点Col</param>
        /// <returns></returns>
        /// 



        public static bool gen_spoke_ROI1(HObject ho_Image, out HObject ho_Regions, out HObject ho_Contours, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_ROIRows, HTuple hv_ROICols, HTuple hv_Direct, HTuple hv_AddR)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_Contour, ho_ContCircle, ho_Rectangle1 = null;
           HObject ho_RectRegion = null;
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_RowC, hv_ColumnC;
           HTuple hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder;
           HTuple hv_RowXLD, hv_ColXLD, hv_Length2, hv_i, hv_j = new HTuple();
           HTuple hv_RowE = new HTuple(), hv_ColE = new HTuple(), hv_ATan = new HTuple();
           HTuple hv_ROICols_COPY_INP_TMP = hv_ROICols.Clone();
           HTuple hv_ROIRows_COPY_INP_TMP = hv_ROIRows.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_Contours);
           HOperatorSet.GenEmptyObj(out ho_Contour);
           HOperatorSet.GenEmptyObj(out ho_ContCircle);
           HOperatorSet.GenEmptyObj(out ho_Rectangle1);
           HOperatorSet.GenEmptyObj(out ho_RectRegion);
           try
           {
               HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
               //1.产生一个现实对象用于显示
               ho_Regions.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Regions);
               ho_Contours.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Contours);
               //2.产生xld
               ho_Contour.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows_COPY_INP_TMP,hv_ROICols_COPY_INP_TMP);
               //3.用回归线法（不抛出异常点，所有点的权重一样）拟合圆
               HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowC, out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
               //4.根据拟合结果产生xld，并保持到显示图像
               ho_ContCircle.Dispose();
               HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius - hv_AddR, hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
               OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
               SP_O++;
               ho_Contours.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ContCircle, out ho_Contours);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
               //5获取圆或圆弧xld上的点坐标
               HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);
               //6求圆或者圆弧XLD上点的数量
               HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
               //7判断检测边缘的数量是否过少
               if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
               {
                   hv_ROIRows_COPY_INP_TMP = new HTuple();
                   hv_ROICols_COPY_INP_TMP = new HTuple();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_RectRegion.Dispose();
                   return false;
               }
               //8如果XLD是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
               //如果XLD是圆，有Length2个点，以0度为起点，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
               for (hv_i = 0; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
               {
                   // if (RowXLD[0] = RowXLD[Length2 - 1])
                   if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(hv_Length2 - 1)))) != 0)
                   {
                       //xld的起点和中点坐标相同，为圆
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / hv_Elements) * hv_i, out hv_j);
                   }
                   else
                   {
                       //否则为圆弧
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i, out hv_j);
                   }
                   //索引越界，强制赋值为最后一个索引  if(j>=Length2)
                   if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                   {
                       hv_j = hv_Length2 - 1;
                   }
                   //获取卡尺工具中心
                   hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                   hv_ColE = hv_ColXLD.TupleSelect(hv_j);
                   //超出图像区域不检测，否则容易报异常  if(RowE>Height-1 or RowE<0 or ColE>Width-1 or ColE<0)
                   if ((int)((new HTuple((new HTuple((new HTuple(hv_RowE.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowE.TupleLess(0))))).TupleOr(new HTuple(hv_ColE.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColE.TupleLess(0)))) != 0)
                   {
                       continue;
                   }
                   //边缘搜索方向类型：'inner'搜索方向由圆外指向圆心：‘outer’搜索方向由圆心指向圆外
                   if ((int)(new HTuple(hv_Direct.TupleEqual("inner"))) != 0)
                   {
                       //求卡尺工具的边缘搜索方向
                       //求圆心指向边缘的矢量角度
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                       //角度反向
                       hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                   }
                   else
                   {
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                   }
                   //产生卡尺XLD,并保存到显示对象
                   ho_Rectangle1.Dispose();
                   HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                   ho_RectRegion.Dispose();
                   HOperatorSet.GenRectangle2(out ho_RectRegion, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                   OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
                   SP_O++;
                   ho_Contours.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle1, out ho_Contours);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RectRegion, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
               }
               return true;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_Contour.Dispose();
               ho_ContCircle.Dispose();
               ho_Rectangle1.Dispose();
               ho_RectRegion.Dispose();
               Logger.PopError(HDevExpDefaultException.Message + HDevExpDefaultException.Source);
               return false;
           }
       }

       /// <summary>
       /// 生成圆弧卡尺工具，分析卡尺区域的灰度
       /// </summary>
       /// <param name="ho_Regions">生成的检测区域的集合</param>
       /// <param name="ho_Contours">ROI区域的轮廓</param>
       /// <param name="hv_Elements">检测区域的个数</param>
       /// <param name="hv_DetectHeight">ROI的高度</param>
       /// <param name="hv_DetectWidth">ROI的宽度</param>
       /// <param name="hv_ROIRows">起点Row</param>
       /// <param name="hv_ROICols">起点Col</param>
       /// <param name="hv_Direct">默认向外，"inner"</param>
       /// <param name="hv_AddR">终点Col</param>
       /// <returns></returns>
       public static bool gen_spoke_ROI2( out HObject ho_Regions, out HObject ho_Contours, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, 
                                         HTuple hv_ROIRows, HTuple hv_ROICols, HTuple hv_Direct, HTuple hv_AddR)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_Contour, ho_ContCircle, ho_Rectangle1 = null;
           HObject ho_RectRegion = null;
           // Local control variables 
           HTuple hv_RowC, hv_ColumnC;
           HTuple hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder;
           HTuple hv_RowXLD, hv_ColXLD, hv_Length2, hv_i, hv_j = new HTuple();
           HTuple hv_RowE = new HTuple(), hv_ColE = new HTuple(), hv_ATan = new HTuple();
           HTuple hv_ROICols_COPY_INP_TMP = hv_ROICols.Clone();
           HTuple hv_ROIRows_COPY_INP_TMP = hv_ROIRows.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Regions);
           HOperatorSet.GenEmptyObj(out ho_Contours);
           HOperatorSet.GenEmptyObj(out ho_Contour);
           HOperatorSet.GenEmptyObj(out ho_ContCircle);
           HOperatorSet.GenEmptyObj(out ho_Rectangle1);
           HOperatorSet.GenEmptyObj(out ho_RectRegion);
           try
           {
               //1.产生一个现实对象用于显示
               ho_Regions.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Regions);
               ho_Contours.Dispose();
               HOperatorSet.GenEmptyObj(out ho_Contours);
               //2.产生xld
               ho_Contour.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows_COPY_INP_TMP,hv_ROICols_COPY_INP_TMP);
               //3.用回归线法（不抛出异常点，所有点的权重一样）拟合圆
               HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowC, out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
               //4.根据拟合结果产生xld，并保持到显示图像
               ho_ContCircle.Dispose();
               HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius - hv_AddR, hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
               OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
               SP_O++;
               ho_Contours.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ContCircle, out ho_Contours);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
               //5获取圆或圆弧xld上的点坐标
               HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);
               //6求圆或者圆弧XLD上点的数量
               HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
               //7判断检测边缘的数量是否过少
               if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
               {
                   hv_ROIRows_COPY_INP_TMP = new HTuple();
                   hv_ROICols_COPY_INP_TMP = new HTuple();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Rectangle1.Dispose();
                   ho_RectRegion.Dispose();
                   return false;
               }
               //8如果XLD是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
               //如果XLD是圆，有Length2个点，以0度为起点，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
               for (hv_i = 0; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
               {
                   // if (RowXLD[0] = RowXLD[Length2 - 1])
                   if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(hv_Length2 - 1)))) != 0)
                   {
                       //xld的起点和中点坐标相同，为圆
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / hv_Elements) * hv_i, out hv_j);
                   }
                   else
                   {
                       //否则为圆弧
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i, out hv_j);
                   }
                   //索引越界，强制赋值为最后一个索引  if(j>=Length2)
                   if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                   {
                       hv_j = hv_Length2 - 1;
                   }
                   //获取卡尺工具中心
                   hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                   hv_ColE = hv_ColXLD.TupleSelect(hv_j);
                   //超出图像区域不检测，否则容易报异常  if(RowE>Height-1 or RowE<0 or ColE>Width-1 or ColE<0)

                   //边缘搜索方向类型：'inner'搜索方向由圆外指向圆心：‘outer’搜索方向由圆心指向圆外
                   if ((int)(new HTuple(hv_Direct.TupleEqual("inner"))) != 0)
                   {
                       //求卡尺工具的边缘搜索方向
                       //求圆心指向边缘的矢量角度
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                       //角度反向
                       hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                   }
                   else
                   {
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                   }
                   //产生卡尺XLD,并保存到显示对象
                   ho_Rectangle1.Dispose();
                   HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                   ho_RectRegion.Dispose();
                   HOperatorSet.GenRectangle2(out ho_RectRegion, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                   OTemp[SP_O] = ho_Contours.CopyObj(1, -1);
                   SP_O++;
                   ho_Contours.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle1, out ho_Contours);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
                   OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
                   SP_O++;
                   ho_Regions.Dispose();
                   HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_RectRegion, out ho_Regions);
                   OTemp[SP_O - 1].Dispose();
                   SP_O = 0;
               }
               return true;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_Contour.Dispose();
               ho_ContCircle.Dispose();
               ho_Rectangle1.Dispose();
               ho_RectRegion.Dispose();
               Logger.PopError(HDevExpDefaultException.Message + HDevExpDefaultException.Source);
               return false;
           }
       }


        public static void ListToHTuple(List<double>  Lists,out HTuple HvTuple  )
       {
           HvTuple = new HTuple();
           for (int i = 0; i < Lists.Count(); i++)
           {
               HvTuple[i] = Lists[i];
           }      
       }


       public static void gen_roke_ROIi(HObject ho_Image, out HObject ho_Region, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2, HTuple hv_J)
       {
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_ATan, hv_i;
           HTuple hv_RowC = new HTuple(), hv_ColC = new HTuple(), hv_Distance = new HTuple();
           HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Region);
           //1.获取图像大小
           HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
           ho_Region.Dispose();
           HOperatorSet.GenEmptyObj(out ho_Region);
           //3.计算所画直线的角度
           HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);
           hv_ATan = hv_ATan + ((new HTuple(90)).TupleRad());
           //4.生成卡尺工具的检测区域
           for (hv_i = 1; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
           {
               //4.1如果只有一个测量矩形，作为卡尺工具，宽度为检测为直线的长度
               if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
               {
                   hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                   hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                   if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr( new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                   {
                       continue;
                   }
                   HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                   hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                   if ((int)(new HTuple(hv_J.TupleEqual(hv_i))) != 0)
                   {
                       ho_Region.Dispose();
                       HOperatorSet.GenRectangle2(out ho_Region, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2,hv_Distance / 50);
                   }
                   //4.2如果有多个测量矩形，产生该测量矩形的XLD
               }
               else
               {
                   hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                   hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                   //4.3超出图像区域，跳出当前循环
                   if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr( new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                   {
                       continue;
                   }
                   if ((int)(new HTuple(hv_J.TupleEqual(hv_i))) != 0)
                   {
                       ho_Region.Dispose();
                       HOperatorSet.GenRectangle2(out ho_Region, hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                        break;
                   }
               }
           }
           return;
       }

       public static bool  gen_spoke_ROIi(HObject ho_Image, out HObject ho_RegionI, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth,
                                          HTuple hv_ROIRows, HTuple hv_ROICols, HTuple hv_J,HTuple AddR)
       {
           // Stack for temporary objects 
           HObject[] OTemp = new HObject[20];
           long SP_O = 0;
           // Local iconic variables 
           HObject ho_Contour, ho_ContCircle, ho_Regions = null;
           // Local control variables 
           HTuple hv_Width, hv_Height, hv_RowC, hv_ColumnC;
           HTuple hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder;
           HTuple hv_RowXLD, hv_ColXLD, hv_Length2, hv_i, hv_j = new HTuple();
           HTuple hv_RowE = new HTuple(), hv_ColE = new HTuple(), hv_Direct = new HTuple();
           HTuple hv_ATan = new HTuple();
           HTuple hv_ROICols_COPY_INP_TMP = hv_ROICols.Clone();
           HTuple hv_ROIRows_COPY_INP_TMP = hv_ROIRows.Clone();
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_RegionI);
           HOperatorSet.GenEmptyObj(out ho_Contour);
           HOperatorSet.GenEmptyObj(out ho_ContCircle);
           HOperatorSet.GenEmptyObj(out ho_Regions);
           try
           {
               HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
               //1.产生一个现实对象用于显示
               ho_RegionI.Dispose();
               HOperatorSet.GenEmptyObj(out ho_RegionI);
               //3.产生xld
               ho_Contour.Dispose();
               HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows_COPY_INP_TMP,
                   hv_ROICols_COPY_INP_TMP);
               //用回归线法（不抛出异常点，所有点的权重一样）拟合圆
               HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowC, out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
               //根据拟合结果产生xld，并保持到显示图像
               ho_ContCircle.Dispose();
               HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius - AddR, hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
               OTemp[SP_O] = ho_Regions.CopyObj(1, -1);
               SP_O++;
               ho_Regions.Dispose();
               HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ContCircle, out ho_Regions);
               OTemp[SP_O - 1].Dispose();
               SP_O = 0;
               //获取圆或圆弧xld上的点坐标
               HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);
               //显示图像和圆弧
               //求圆或者圆弧XLD上点的数量
               HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
               //判断检测边缘的数量是否过少
               if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
               {
                   hv_ROIRows_COPY_INP_TMP = new HTuple();
                   hv_ROICols_COPY_INP_TMP = new HTuple();
                   ho_Contour.Dispose();
                   ho_ContCircle.Dispose();
                   ho_Regions.Dispose();
                   return false;
               }
               //如果XLD是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
               //如果XLD是圆，有Length2个点，以0度为起点，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
               for (hv_i = 0; hv_i.Continue(hv_Elements, 1); hv_i = hv_i.TupleAdd(1))
               {
                   if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(hv_Length2 - 1)))) != 0)
                   {
                       //xld的起点和中点坐标相同，为圆
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / hv_Elements) * hv_i, out hv_j);
                   }
                   else
                   {
                       //否则为圆弧
                       HOperatorSet.TupleInt(((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i, out hv_j);
                   }
                   //索引越界，强制赋值为最后一个索引
                   if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                   {
                       hv_j = hv_Length2 - 1;
                   }
                   //获取卡尺工具中心
                   hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                   hv_ColE = hv_ColXLD.TupleSelect(hv_j);
                   //超出图像区域不检测，否则容易报异常
                   //  if(RowE>Height-1 or RowE<0 or ColE>Width-1 or ColE<0)
                   if ((int)((new HTuple((new HTuple((new HTuple(hv_RowE.TupleGreater(hv_Height - 1))).TupleOr(new HTuple(hv_RowE.TupleLess(0))))).TupleOr(new HTuple(hv_ColE.TupleGreater(hv_Width - 1))))).TupleOr(new HTuple(hv_ColE.TupleLess(0)))) != 0)
                   {
                       continue;
                   }
                   //边缘搜索方向类型：'inner'搜索方向由圆外指向圆心：‘outer’搜索方向由圆心指向圆外
                   if ((int)(new HTuple(hv_Direct.TupleEqual("inner"))) != 0)
                   {
                       //求卡尺工具的边缘搜索方向
                       //求圆心指向边缘的矢量角度
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                       //角度反向
                       hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                   }
                   else
                   {
                       HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                   }
                   //产生卡尺XLD,并保存到显示对象
                   if ((int)(new HTuple(hv_J.TupleEqual(hv_i))) != 0){
                       ho_RegionI.Dispose();
                       //HOperatorSet.GenRectangle2ContourXld(out ho_RegionI, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                       HOperatorSet.GenRectangle2(out ho_RegionI, hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                   }
               }
               ho_Contour.Dispose();
               ho_ContCircle.Dispose();
               ho_Regions.Dispose();
               return  true;
           }
           catch (HalconException HDevExpDefaultException)
           {
               ho_Contour.Dispose();
               ho_ContCircle.Dispose();
               Logger.PopError(HDevExpDefaultException.ToString());
               ho_Regions.Dispose();
               return false;
           }
       }


        public static bool JudgeAngle( St_LinesParam TeachLinePara ,HTuple LineRow1U ,HTuple LineCol1U,HTuple LineRow2U,HTuple LineCol2U ,double AngleRange)
       {
           List<double> ListRow1 = new List<double>();
           List<double> ListCol1 = new List<double>();
           List<double> ListRow2 = new List<double>();
           List<double> ListCol2 = new List<double>();
           ListRow1 = TeachLinePara.Row1s;
           ListCol1 = TeachLinePara.Col1s;
           ListRow2 = TeachLinePara.Row2s;
           ListCol2 = TeachLinePara.Col2s;
           int Num = ListRow1.Count;
           if (ListCol1.Count != Num || ListRow1.Count != Num || ListCol2.Count != Num || ListRow2.Count != Num)
           {
               Logger.Pop("示教直线保存时,可能保存有问题");
               return false;       
           }
           if (LineRow1U.Length != Num || LineCol1U.Length != Num || LineRow2U.Length != Num || LineCol2U.Length != Num)
           {
               Logger.Pop("可能存在找线失败的问题，使得找到线的数量和示教线的数量不一致。");
               return false;          
           }
           HTuple AngleLlTeachToFind = new HTuple();
           for (int i = 0; i < Num; i++)
           {
               HOperatorSet.AngleLl(ListRow1[i], ListCol1[i], ListRow2[i], ListCol2[i],  LineRow1U[i], LineCol1U[i], LineRow2U[i], LineCol2U[i], out AngleLlTeachToFind);
               if (Math.Abs(AngleLlTeachToFind.D) > AngleRange * Math.PI / 180.0)
                   return false;      
           }
           return true;
       }
       /// <summary>
       /// 图像频域滤波处理，滤波环宽度依次为 FixWidth1 + FixWidth2 + FixWidth3 <=1
       /// </summary>
       /// <param name="ImgIn"></param>
       /// <param name="ImgOut"></param>
       /// <param name="FixWidth1">通过的低频部分</param>
       /// <param name="FixWidth2">滤掉的高频部分</param>
       /// <param name="FixWidth3">通过的高频部分</param>
       /// <returns></returns>
       public static bool FilterImg(HObject ImgIn, out HObject ImgOut, double FixWidth1 = 0.1, double FixWidth2 = 0.2, double FixWidth3 = 0.4)
       {

           ImgOut = new HObject();
           HTuple ImgWid = new HTuple(), ImgHig = new HTuple();
           HTuple tmpWid=new HTuple(), tmpHei=new HTuple();
           HObject FFtImg = new HObject();
           try
           {
               HObject zoomImg;
               
               //HOperatorSet.GrayErosionRect(ImgIn, out  ImgIn, 7, 7);
               //HOperatorSet.WriteImage(ImgIn, "bmp", 0, "D:\\123.bmp");
               HOperatorSet.GetImageSize(ImgIn, out ImgWid, out ImgHig);
               tmpWid=(int)(ImgWid.I/2.0);tmpHei=(int)(ImgHig.I / 2.0);
               HOperatorSet.ZoomImageSize(ImgIn, out zoomImg, tmpWid, tmpHei, "bilinear");

               //1.0图像空域转到频域
               HOperatorSet.FftImage(zoomImg, out FFtImg);
               //2.0生成第一个滤波环
               HObject ImgLowPass0 = new HObject(), ImgLowPass1 = new HObject(), SubImg1 = new HObject(), SubImg2 = new HObject(), ImgResult = new HObject();
               HOperatorSet.GenLowpass(out ImgLowPass0, 0.000001, "none", "dc_center", tmpWid, tmpHei);
               HOperatorSet.GenLowpass(out ImgLowPass1, FixWidth1, "none", "dc_center", tmpWid, tmpHei);
               HOperatorSet.SubImage(ImgLowPass1, ImgLowPass0, out SubImg1, 1, 0);
               //3.0生成第二滤波环
               HOperatorSet.GenLowpass(out ImgLowPass0, FixWidth1 + FixWidth2, "none", "dc_center", tmpWid, tmpHei);
               HOperatorSet.GenLowpass(out ImgLowPass1, FixWidth1 + FixWidth2 + FixWidth3, "none", "dc_center", tmpWid, tmpHei);
               HOperatorSet.SubImage(ImgLowPass1, ImgLowPass0, out SubImg2, 1, 0);
               //4.0频域滤波
               HOperatorSet.AddImage(SubImg1, SubImg2, out ImgResult, 1, 0);
               HObject FFtFilterImg = new HObject();
               HObject FFtImgConvel = new HObject();
               HOperatorSet.ConvolFft(FFtImg, ImgResult, out  FFtImgConvel);
               //5.0频域转换到空域
               HOperatorSet.FftImageInv(FFtImgConvel, out FFtFilterImg);
               //6.0腐蚀，滤掉噪点
               HObject ImgMin0 = new HObject(), ImgMin1 = new HObject(), ImgMin2 = new HObject(), ImgMin3 = new HObject(), ImgMin4 = new HObject(), ScaleImg = new HObject();
               HOperatorSet.GrayErosionRect(FFtFilterImg, out  ImgMin0, 5, 5);
               HOperatorSet.GrayErosionRect(ImgMin0, out  ImgMin1, 5, 5);
               //HOperatorSet.GrayErosionRect(ImgMin1, out  ImgMin2, 5, 5);
               //HOperatorSet.GrayErosionRect(ImgMin2, out  ImgMin3, 5, 5);
               //HOperatorSet.GrayErosionRect(ImgMin3, out  ImgMin4, 10, 2);
               HOperatorSet.ScaleImage(ImgMin1, out ScaleImg, 5, 0);
               //HOperatorSet.GetImageSize(ImgMin1, out ImgWid, out ImgHig);
               //HOperatorSet.GetImageSize(ScaleImg, out ImgWid, out ImgHig);
               HOperatorSet.CopyImage(ScaleImg, out ImgOut);

               HOperatorSet.ZoomImageSize(ImgOut, out ImgOut, ImgWid, ImgHig, "bilinear");
               //HOperatorSet.WriteImage(ImgOut, "bmp", 0, "D:\\123.bmp");
               return true;
           
           }
           catch (Exception e0)
           {
               Logger.PopError(e0.Message + e0.Source + e0.StackTrace);
               return false;
           }      
       }

        /// <summary>
        /// 频域滤波
        /// </summary>
        /// <param name="ImgIn"></param>
        /// <param name="ImgOut"></param>
        /// <param name="FilterC">滤波参数</param>
        /// <returns></returns>
        public static bool FilterImg(HObject ImgIn, out HObject ImgOut, double FilterC)
        {
            ImgOut = new HObject();
            HTuple ImgWid = new HTuple(), ImgHig = new HTuple();
            HObject FFtImg = new HObject();
            //1.获取图像长宽
            HOperatorSet.GetImageSize(ImgIn, out ImgWid, out ImgHig);
            HObject ImgLowPass0 = new HObject();
            //2.生成低通滤波区域
            HOperatorSet.GenLowpass(out ImgLowPass0, FilterC, "none", "dc_center", ImgWid, ImgHig);
            //3.空域转换到频域
            HOperatorSet.FftImage(ImgIn, out FFtImg);

            HObject FFtImgConvel = new HObject();
            //4.频域滤波
            HOperatorSet.ConvolFft(FFtImg, ImgLowPass0, out FFtImgConvel);
            //5.0频域转换到空域
            HOperatorSet.FftImageInv(FFtImgConvel, out ImgOut);

            return true;
        }




        /// <summary>
        /// 最小二乘法拟合直线
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
       public static bool LeastSquaretFitLine(double[] X, double[] Y, out double a, out  double b)
       {
           a = 0;
           b = 0;
           double sum_x2 = 0;
           double sum_y = 0;
           double sum_x = 0;
           double sum_xy = 0;
           int num = X.Length;
           try
           {
               for (int i = 0; i < num; ++i)
               {
                   sum_x2 += X[i] * X[i];
                   sum_y += Y[i];
                   sum_x += X[i];
                   sum_xy += X[i] * Y[i];
               }
           }
           catch (Exception E0)
           {
               Logger.PopError(E0.Message + E0.Source+ E0.StackTrace);
               return false;
           }
           a = (num * sum_xy - sum_x * sum_y) / (num * sum_x2 - sum_x * sum_x);
           b = (sum_x2 * sum_y - sum_x * sum_xy) / (num * sum_x2 - sum_x * sum_x);
           return true;
       }
        /// <summary>
        /// 最小二乘法拟合直线
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
       public static bool LeastAquareFitLine1(double[] X, double[] Y, out double x1, out double y1, out double x2, out double y2)
       {
           x1 = new double();
           y1 = new double();
           x2 = new double();
           y2 = new double();
           double a = 0; 
           double b = 0;
           if (!LeastSquaretFitLine(X, Y, out a, out b))
           {
               return false;
           }
           x1 = X[0];
           y1 = a * x1 +b;
           x2 = X[X.Length - 1];
           y2 = a * x2+b;
           return true;
       }


       public static bool BinImg(HObject ImgIn,double MinGray,double MaxGray ,out HObject ImgOut)
       {
           ImgOut = new HObject();
           try
           {
               HObject Region = new HObject();
               HOperatorSet.Threshold(ImgIn, out  Region, MinGray, MaxGray);
               HOperatorSet.FillUp(Region, out Region);
               HTuple Wid = new HTuple(), Hei = new HTuple();

               HOperatorSet.GetImageSize(ImgIn, out Wid, out Hei);
               HOperatorSet.RegionToBin(Region, out ImgOut, 255, 0, Wid, Hei);
           }
           catch (Exception e0)
           {
               Logger.Pop1(e0.Message + e0.Source + e0.StackTrace);
           }
           return true;
       }

        /// <summary>
        /// 生成直线上的点
        /// </summary>
        /// <param name="LineRow1"></param>
        /// <param name="LineCol1"></param>
        /// <param name="LineRow2"></param>
        /// <param name="LineCol2"></param>
        /// <param name="Elments"></param>
        /// <param name="PtRows"></param>
        /// <param name="PtCols"></param>
        /// <returns></returns>
       public static bool GetLinePts(HTuple LineRow1,HTuple LineCol1,HTuple LineRow2,HTuple LineCol2 ,HTuple Elments,out HTuple PtRows,out HTuple PtCols)
       {
           PtRows = new HTuple();
           PtCols = new HTuple();
           HTuple StartRow = LineRow1[0];
           HTuple StartCol = LineCol1[0];
           HTuple VectorRow = (LineRow2[0].D - LineRow1[0].D) / (Elments[0].D - 1.0);
           HTuple VectorCol = (LineCol2[0].D - LineCol1[0].D) / (Elments[0].D - 1.0);
           for (int i = 0; i < Elments; i++)
           {
             PtRows =  PtRows.TupleConcat( new HTuple(  StartRow.D + VectorRow.D * i));
             PtCols = PtCols.TupleConcat(new HTuple(StartCol.D + VectorCol.D * i));
           }
           return true;
       
       }

        /// <summary>
        /// 计算自动对焦的对焦系数值
        /// </summary>
        /// <param name="ho_Image">输入图像方法 </param>
        /// <param name="hv_Method">计算对焦度的方法，</param>
        /// <param name="hv_Value"></param>
        public static  void evaluate_definition(HObject ho_Image, out HTuple hv_Value, string hv_Method = "Tenegrad")
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            // Local iconic variables 
            HObject ho_ImageMean = null, ho_ImageSub = null;
            HObject ho_ImageResult = null, ho_ImageLaplace4 = null, ho_ImageLaplace8 = null;
            HObject ho_ImageResult1 = null, ho_ImagePart00 = null, ho_ImagePart01 = null;
            HObject ho_ImagePart10 = null, ho_ImageSub1 = null, ho_ImageSub2 = null;
            HObject ho_ImageResult2 = null, ho_ImagePart20 = null, ho_EdgeAmplitude = null;
            HObject ho_Region1 = null, ho_BinImage = null, ho_ImageResult4 = null;
            // Local copy input parameter variables 
            HObject ho_Image_COPY_INP_TMP;
            ho_Image_COPY_INP_TMP = new HObject(ho_Image);
            // Local control variables 
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_Deviation = new HTuple(), hv_Min = new HTuple();
            HTuple hv_Max = new HTuple(), hv_Range = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_ImageSub);
            HOperatorSet.GenEmptyObj(out ho_ImageResult);
            HOperatorSet.GenEmptyObj(out ho_ImageLaplace4);
            HOperatorSet.GenEmptyObj(out ho_ImageLaplace8);
            HOperatorSet.GenEmptyObj(out ho_ImageResult1);
            HOperatorSet.GenEmptyObj(out ho_ImagePart00);
            HOperatorSet.GenEmptyObj(out ho_ImagePart01);
            HOperatorSet.GenEmptyObj(out ho_ImagePart10);
            HOperatorSet.GenEmptyObj(out ho_ImageSub1);
            HOperatorSet.GenEmptyObj(out ho_ImageSub2);
            HOperatorSet.GenEmptyObj(out ho_ImageResult2);
            HOperatorSet.GenEmptyObj(out ho_ImagePart20);
            HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_BinImage);
            HOperatorSet.GenEmptyObj(out ho_ImageResult4);
            hv_Value = new HTuple();
            try
            {
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ScaleImageMax(ho_Image_COPY_INP_TMP, out ExpTmpOutVar_0);
                    ho_Image_COPY_INP_TMP.Dispose();
                    ho_Image_COPY_INP_TMP = ExpTmpOutVar_0;
                }
                HOperatorSet.GetImageSize(ho_Image_COPY_INP_TMP, out hv_Width, out hv_Height);
                if (hv_Method.Equals("Deviation"))
                {
                    //方差法
                    ho_ImageMean.Dispose();
                    HOperatorSet.RegionToMean(ho_Image_COPY_INP_TMP, ho_Image_COPY_INP_TMP, out ho_ImageMean );
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConvertImageType(ho_ImageMean, out ExpTmpOutVar_0, "real");
                        ho_ImageMean.Dispose();
                        ho_ImageMean = ExpTmpOutVar_0;
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConvertImageType(ho_Image_COPY_INP_TMP, out ExpTmpOutVar_0, "real");
                        ho_Image_COPY_INP_TMP.Dispose();
                        ho_Image_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                    ho_ImageSub.Dispose();
                    HOperatorSet.SubImage(ho_Image_COPY_INP_TMP, ho_ImageMean, out ho_ImageSub, 1, 0);
                    ho_ImageResult.Dispose();
                    HOperatorSet.MultImage(ho_ImageSub, ho_ImageSub, out ho_ImageResult, 1, 0);
                    HOperatorSet.Intensity(ho_ImageResult, ho_ImageResult, out hv_Value, out hv_Deviation);
                }
                else if (hv_Method.Equals("laplace"))
                {
                    //拉普拉斯能量函数
                    ho_ImageLaplace4.Dispose();
                    HOperatorSet.Laplace(ho_Image_COPY_INP_TMP, out ho_ImageLaplace4, "signed", 3, "n_4");
                    ho_ImageLaplace8.Dispose();
                    HOperatorSet.Laplace(ho_Image_COPY_INP_TMP, out ho_ImageLaplace8, "signed", 3, "n_8");
                    ho_ImageResult1.Dispose();
                    HOperatorSet.AddImage(ho_ImageLaplace4, ho_ImageLaplace4, out ho_ImageResult1,  1, 0);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.AddImage(ho_ImageLaplace4, ho_ImageResult1, out ExpTmpOutVar_0, 1, 0);
                        ho_ImageResult1.Dispose();
                        ho_ImageResult1 = ExpTmpOutVar_0;
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.AddImage(ho_ImageLaplace8, ho_ImageResult1, out ExpTmpOutVar_0, 1, 0);
                        ho_ImageResult1.Dispose();
                        ho_ImageResult1 = ExpTmpOutVar_0;
                    }
                    ho_ImageResult.Dispose();
                    HOperatorSet.MultImage(ho_ImageResult1, ho_ImageResult1, out ho_ImageResult,  1, 0);
                    HOperatorSet.Intensity(ho_ImageResult, ho_ImageResult, out hv_Value, out hv_Deviation);
                }
                else if (hv_Method.Equals("energy"))
                {
                    //能量梯度函数
                    ho_ImagePart00.Dispose();
                    HOperatorSet.CropPart(ho_Image_COPY_INP_TMP, out ho_ImagePart00, 0, 0, hv_Width - 1, hv_Height - 1);
                    ho_ImagePart01.Dispose();
                    HOperatorSet.CropPart(ho_Image_COPY_INP_TMP, out ho_ImagePart01, 0, 1, hv_Width - 1,  hv_Height - 1);
                    ho_ImagePart10.Dispose();
                    HOperatorSet.CropPart(ho_Image_COPY_INP_TMP, out ho_ImagePart10, 1, 0, hv_Width - 1,   hv_Height - 1);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConvertImageType(ho_ImagePart00, out ExpTmpOutVar_0, "real");
                        ho_ImagePart00.Dispose();
                        ho_ImagePart00 = ExpTmpOutVar_0;
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConvertImageType(ho_ImagePart10, out ExpTmpOutVar_0, "real");
                        ho_ImagePart10.Dispose();
                        ho_ImagePart10 = ExpTmpOutVar_0;
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConvertImageType(ho_ImagePart01, out ExpTmpOutVar_0, "real");
                        ho_ImagePart01.Dispose();
                        ho_ImagePart01 = ExpTmpOutVar_0;
                    }
                    ho_ImageSub1.Dispose();
                    HOperatorSet.SubImage(ho_ImagePart10, ho_ImagePart00, out ho_ImageSub1, 1,  0);
                    ho_ImageResult1.Dispose();
                    HOperatorSet.MultImage(ho_ImageSub1, ho_ImageSub1, out ho_ImageResult1, 1,  0);
                    ho_ImageSub2.Dispose();
                    HOperatorSet.SubImage(ho_ImagePart01, ho_ImagePart00, out ho_ImageSub2, 1, 0);
                    ho_ImageResult2.Dispose();
                    HOperatorSet.MultImage(ho_ImageSub2, ho_ImageSub2, out ho_ImageResult2, 1,  0);
                    ho_ImageResult.Dispose();
                    HOperatorSet.AddImage(ho_ImageResult1, ho_ImageResult2, out ho_ImageResult, 1, 0);
                    HOperatorSet.Intensity(ho_ImageResult, ho_ImageResult, out hv_Value, out hv_Deviation);
                }
                else if (hv_Method.Equals("Brenner")   )
                {
                    //Brenner函数法
                    ho_ImagePart00.Dispose();
                    HOperatorSet.CropPart(ho_Image_COPY_INP_TMP, out ho_ImagePart00, 0, 0, hv_Width,  hv_Height - 2);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConvertImageType(ho_ImagePart00, out ExpTmpOutVar_0, "real");
                        ho_ImagePart00.Dispose();
                        ho_ImagePart00 = ExpTmpOutVar_0;
                    }
                    ho_ImagePart20.Dispose();
                    HOperatorSet.CropPart(ho_Image_COPY_INP_TMP, out ho_ImagePart20, 2, 0, hv_Width,  hv_Height - 2);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConvertImageType(ho_ImagePart20, out ExpTmpOutVar_0, "real");
                        ho_ImagePart20.Dispose();
                        ho_ImagePart20 = ExpTmpOutVar_0;
                    }
                    ho_ImageSub.Dispose();
                    HOperatorSet.SubImage(ho_ImagePart20, ho_ImagePart00, out ho_ImageSub, 1, 0);
                    ho_ImageResult.Dispose();
                    HOperatorSet.MultImage(ho_ImageSub, ho_ImageSub, out ho_ImageResult, 1, 0);
                    HOperatorSet.Intensity(ho_ImageResult, ho_ImageResult, out hv_Value, out hv_Deviation);
                }
                else if (hv_Method.Equals("Tenegrad") )
                {
                    //Tenegrad函数法
                    ho_EdgeAmplitude.Dispose();
                    HOperatorSet.SobelAmp(ho_Image_COPY_INP_TMP, out ho_EdgeAmplitude, "sum_sqrt", 3);
                    HOperatorSet.MinMaxGray(ho_EdgeAmplitude, ho_EdgeAmplitude, 0, out hv_Min, out hv_Max, out hv_Range);
                    ho_Region1.Dispose();
                    HOperatorSet.Threshold(ho_EdgeAmplitude, out ho_Region1, 11.8, 255);
                    ho_BinImage.Dispose();
                    HOperatorSet.RegionToBin(ho_Region1, out ho_BinImage, 1, 0, hv_Width, hv_Height);
                    ho_ImageResult4.Dispose();
                    HOperatorSet.MultImage(ho_EdgeAmplitude, ho_BinImage, out ho_ImageResult4, 1, 0);
                    ho_ImageResult.Dispose();
                    HOperatorSet.MultImage(ho_ImageResult4, ho_ImageResult4, out ho_ImageResult,  1, 0);
                    HOperatorSet.Intensity(ho_ImageResult, ho_ImageResult, out hv_Value, out hv_Deviation);
                }
                ho_Image_COPY_INP_TMP.Dispose();
                ho_ImageMean.Dispose();
                ho_ImageSub.Dispose();
                ho_ImageResult.Dispose();
                ho_ImageLaplace4.Dispose();
                ho_ImageLaplace8.Dispose();
                ho_ImageResult1.Dispose();
                ho_ImagePart00.Dispose();
                ho_ImagePart01.Dispose();
                ho_ImagePart10.Dispose();
                ho_ImageSub1.Dispose();
                ho_ImageSub2.Dispose();
                ho_ImageResult2.Dispose();
                ho_ImagePart20.Dispose();
                ho_EdgeAmplitude.Dispose();
                ho_Region1.Dispose();
                ho_BinImage.Dispose();
                ho_ImageResult4.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Image_COPY_INP_TMP.Dispose();
                ho_ImageMean.Dispose();
                ho_ImageSub.Dispose();
                ho_ImageResult.Dispose();
                ho_ImageLaplace4.Dispose();
                ho_ImageLaplace8.Dispose();
                ho_ImageResult1.Dispose();
                ho_ImagePart00.Dispose();
                ho_ImagePart01.Dispose();
                ho_ImagePart10.Dispose();
                ho_ImageSub1.Dispose();
                ho_ImageSub2.Dispose();
                ho_ImageResult2.Dispose();
                ho_ImagePart20.Dispose();
                ho_EdgeAmplitude.Dispose();
                ho_Region1.Dispose();
                ho_BinImage.Dispose();
                ho_ImageResult4.Dispose();
                throw HDevExpDefaultException;
            }
        }
        /// <summary>
        /// 删除数组中间的元素 ，CenterClipC比例系数
        /// </summary>
        /// <param name="TupleIn">输入数组</param>
        /// <param name="CenterClipC">删除系数，0.0-0.9</param>
        /// <param name="TupleOut">输出数组</param>
        public static void ClipCenterElement(HTuple TupleIn,double  CenterClipC ,out HTuple TupleOut  )
        {
            TupleOut = new HTuple();
            //减去中间不要的点
            int Count = TupleIn.Length;
            int ClipLength = (int)(Count * CenterClipC);
            int StartClipNo = (int)((Count - ClipLength) * 0.5);
            if ((Count - ClipLength) > 4 && ClipLength > 0)
            {
                for (int i = 0; i < ClipLength; i++)
                {
                    TupleIn = TupleIn.TupleRemove(StartClipNo);
                }
            }
            TupleOut = TupleIn;

        }

        public static  void SaveImg(HObject Img, string ImgName = null)
        {
            //string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string EbasePath = @"E:";
            string basePath = @"D:\APILogs";
            if (Directory.Exists(EbasePath))
            {
                basePath = @"E:\APILogs";
            }
            if (!Directory.Exists(basePath + "\\Image"))
            {
                Directory.CreateDirectory(basePath + "\\Image");
            }
            string fileName;
            string dataString = DateTime.Now.ToString("yyyy-MM-dd-HH");//Image文件路径
            string dataString0 = DateTime.Now.ToString("HH-mm-ss-fff"); //log文件名
            if (!Directory.Exists(basePath + "\\Image\\" + dataString + "\\" + ImgName))
            {
                Directory.CreateDirectory(basePath + "\\Image\\" + dataString+ "\\" + ImgName); //每一个小时创建一个文件夹
            }

            if (!string.IsNullOrEmpty(ImgName))
            {
                fileName = dataString0 + "_" + ImgName + "_";
            }
            else
            {
                fileName = dataString0 + "_";
            }
            try
            {
               // Task.Factory.StartNew(() => {
                    HOperatorSet.WriteImage(Img, "jpg", 0, basePath + "\\Image\\" + dataString + "\\" + fileName + ImgName  + ".jpg");
               // });
            }
            catch 
            { }


        }
        public static void SaveImg1(HObject Img, string ImgName = null)
        {
            //string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string EbasePath = @"E:";
            string basePath = @"D:\APILogs";
            if (Directory.Exists(EbasePath))
            {
                basePath = @"E:\APILogs";
            }
            if (!Directory.Exists(basePath + "\\Image"))
            {
                Directory.CreateDirectory(basePath + "\\Image");
            }
            string fileName;
            string dataString = DateTime.Now.ToString("yyyy-MM-dd-HH");//Image文件路径
            string dataString0 = DateTime.Now.ToString("HH-mm-ss-fff"); //log文件名
            if (!Directory.Exists(basePath + "\\Image\\" + dataString + "\\" + ImgName)) {
                Directory.CreateDirectory(basePath + "\\Image\\" + dataString + "\\" + ImgName); //每一个小时创建一个文件夹
            }

            if (!string.IsNullOrEmpty(ImgName)){
                fileName = dataString0 + "_" + ImgName + "_";
            }
            else{
                fileName = dataString0 + "_";
            }
            try {
                HOperatorSet.WriteImage(Img, "jpg", 0, basePath + "\\Image\\" + dataString + "\\" + ImgName + "\\" + fileName + ".jpg");
            }
            catch 
            {   }


        }

        public static void SaveImg2(HObject Img, string ImgName = null,string FileNameIn =null)
        {
            //string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string EbasePath = @"E:";
            string basePath = @"D:\APILogs";
            if (Directory.Exists(EbasePath)){
                basePath = @"E:\APILogs";
            }
            if (!Directory.Exists(basePath + "\\Image")){
                Directory.CreateDirectory(basePath + "\\Image");
            }
            string fileName;
            string dataString = DateTime.Now.ToString("yyyy-MM-dd-HH");//Image文件路径
            string dataString0 = DateTime.Now.ToString("HH-mm-ss-fff"); //log文件名
            if (!Directory.Exists(basePath + "\\Image\\" + dataString + "\\" + FileNameIn)){
                Directory.CreateDirectory(basePath + "\\Image\\" + dataString + "\\" + FileNameIn); //每一个小时创建一个文件夹
            }
            if (!string.IsNullOrEmpty(ImgName)){
                fileName = dataString0 + "_" + ImgName + "_";
            }
            else {
                fileName = dataString0 + "_";
            }
            try{
                HOperatorSet.WriteImage(Img, "jpg", 0, basePath + "\\Image\\" + dataString + "\\" + FileNameIn + "\\" + fileName + ".jpg");
            }
            catch 
            { }

        }

        //示教直线的检测
        /// <summary>
        /// 直线检测
        /// </summary>
        /// <param name="ImgIn"></param>
        /// <param name="InspectLinePara">示教的直线参数</param>
        /// <param name="GrayRange"></param>
        /// <param name="BadRegions"></param>
        /// <returns></returns>
        public static bool DectectLines(HObject ImgIn, St_InsepctLinePara InspectLinePara, out HObject BadRegions, out HObject DetectLineContours)
        {
            HObject DetectReions = new HObject(), DetectContours = new HObject();
            double Element, DetectHeight, DetectWidth, Row1, Col1, Row2, Col2;
            List<double> TeachGray = new List<double>();
            if (InspectLinePara.DetectRegionGrays.Count > 0)
                TeachGray = InspectLinePara.DetectRegionGrays[0];
            HOperatorSet.GenEmptyObj(out BadRegions);
            HOperatorSet.GenEmptyObj(out DetectLineContours);
            bool IsOk = true;
            if (InspectLinePara.Count == 0) return IsOk;
            double Threshold = 10;
            try {
                #region
                for (int i = 0; i < InspectLinePara.Row1s.Count(); i++) {
                    //开始检测第i条线的
                    Element = InspectLinePara.Elements[i];
                    DetectHeight = InspectLinePara.DetectHeights[i];
                    DetectWidth = InspectLinePara.DetectWidths[i];
                    Row1 = InspectLinePara.Row1s[i];
                    Col1 = InspectLinePara.Col1s[i];
                    Row2 = InspectLinePara.Row2s[i];
                    Col2 = InspectLinePara.Col2s[i];
                    Threshold = InspectLinePara.Thresholds[i];
                    TeachGray = InspectLinePara.DetectRegionGrays[i];
                    gen_rake_ROI1(ImgIn, out DetectReions, out DetectContours, Element, DetectHeight, DetectWidth, Row1, Col1, Row2, Col2);
                    HOperatorSet.ConcatObj(DetectLineContours, DetectContours, out DetectLineContours);
                    HTuple GrayMean = new HTuple(), Div = new HTuple();
                    HOperatorSet.Intensity(DetectReions, ImgIn, out GrayMean, out Div);
                    for (int j = 0; j < Element; j++){
                        if (Math.Abs(GrayMean[j].D - TeachGray[j]) > Threshold){
                            HObject RectRegioni = new HObject();
                            MyVisionBase.gen_roke_ROIi(ImgIn, out RectRegioni, Element, DetectHeight, DetectWidth, Row1, Col1, Row2, Col2, j + 1);
                            HOperatorSet.ConcatObj(BadRegions, RectRegioni, out BadRegions);
                            IsOk = false;
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)      {
                Logger.PopError(e.Message + e.Source, false);
                return false;
            }
            return IsOk;
        }

        public static bool DetectCircles(HObject ImgIn, St_InspectCirclePara InspectCirclePara, out HObject BadRegions, out HObject DetectContourU)
        {
            HOperatorSet.GenEmptyObj(out BadRegions);
            HOperatorSet.GenEmptyObj(out DetectContourU);
            HObject DetectReions = new HObject(), DetectContours = new HObject();
            double Element, DetectHeight, DetectWidth;
            List<double> TeachGray = new List<double>();
            if (InspectCirclePara.DetectRegionGrays.Count > 0)
                TeachGray = InspectCirclePara.DetectRegionGrays[0];
            List<double> Rows = new List<double>();
            List<double> Cols = new List<double>();
            string Direct = "";
            double AddR = 0;
            bool IsOk = true;

            if (InspectCirclePara.Count == 0) return IsOk;
            double Threshold = 20;
            try {
                #region
                for (int i = 0; i < InspectCirclePara.Count; i++)  {
                    //开始检测第i条线的
                    Element = InspectCirclePara.Elements[i];
                    DetectHeight = InspectCirclePara.DetectHeights[i];
                    DetectWidth = InspectCirclePara.DetectWidths[i];
                    Rows = InspectCirclePara.ListRows[i];
                    Cols = InspectCirclePara.ListCols[i];
                    Direct = InspectCirclePara.Directs[i];
                    AddR = InspectCirclePara.AddRs[i];
                    TeachGray = InspectCirclePara.DetectRegionGrays[i];
                    Threshold = InspectCirclePara.Thresholds[i];
                    HTuple Row = new HTuple(), Col = new HTuple();
                    ListToHTuple(Rows, out Row);
                    ListToHTuple(Cols, out Col);
                    //生成感兴趣的检测区域
                    gen_spoke_ROI1(ImgIn, out DetectReions, out DetectContours, Element, DetectHeight, DetectWidth, Row, Col, Direct, 0);
                    HOperatorSet.ConcatObj(DetectContourU, DetectContours, out DetectContourU);
                    HTuple GrayMean = new HTuple(), Div = new HTuple();
                    //提取ROI内的平均灰度
                    HOperatorSet.Intensity(DetectReions, ImgIn, out GrayMean, out Div);
                    for (int j = 0; j < Element; j++) {                      
                        if (Math.Abs(GrayMean[j].D - TeachGray[j]) > Threshold) {
                            HObject RectRegionI = new HObject();
                            MyVisionBase.gen_spoke_ROIi(ImgIn, out RectRegionI, Element, DetectHeight, DetectWidth, Row, Col, j, 0);
                            HOperatorSet.ConcatObj(BadRegions, RectRegionI, out BadRegions);
                            IsOk = false;
                        }
                    }
                }
                #endregion
            }
            catch (Exception e0)
            {
                IsOk = false;
                Logger.PopError(e0.Message + e0.Source);
            }
            return IsOk;
        }

        public static bool DetetctRectangles(HObject ImgIn, St_InspectRectanglePara InspectRectPara, out HObject BadRegions, out HObject DetectContourU)
        {
            HOperatorSet.GenEmptyObj(out BadRegions);
            HObject RectRegion = new HObject();
            HOperatorSet.GenEmptyObj(out RectRegion);
            HObject ReduceImg = new HObject();
            HOperatorSet.GenEmptyObj(out ReduceImg);
            HObject ThresholdRegion = new HObject(), ConnectRegion = new HObject(), SelectRegion = new HObject(),DarkThdRegion =new HObject();
            HOperatorSet.GenEmptyObj(out ThresholdRegion);
            HOperatorSet.GenEmptyObj(out ConnectRegion);
            HOperatorSet.GenEmptyObj(out SelectRegion);
            HOperatorSet.GenEmptyObj(out DetectContourU);
            HOperatorSet.GenEmptyObj(out DarkThdRegion);
            bool IsOk = true;
            try{
                #region
                for (int i = 0; i < InspectRectPara.Cols.Count; i++){
                    HOperatorSet.GenRectangle2(out RectRegion, InspectRectPara.Rows[i], InspectRectPara.Cols[i], 
                        InspectRectPara.Phis[i], InspectRectPara.Widths[i], InspectRectPara.Heights[i]);
                    HObject RectContour = new HObject();
                    HOperatorSet.GenRectangle2ContourXld(out RectContour, InspectRectPara.Rows[i], InspectRectPara.Cols[i],
                        InspectRectPara.Phis[i], InspectRectPara.Widths[i], InspectRectPara.Heights[i]);
                    HOperatorSet.ConcatObj(DetectContourU, RectContour, out DetectContourU);
                    HOperatorSet.ReduceDomain(ImgIn, RectRegion, out ReduceImg);  //获取ROI内的图像信息
                    if(InspectRectPara.MeanGrays[i] + InspectRectPara.GrayRanges[i]<255)
                       HOperatorSet.Threshold(ReduceImg, out ThresholdRegion, InspectRectPara.MeanGrays[i] 
                         + InspectRectPara.GrayRanges[i], 255); //阈值分割

                    HOperatorSet.Connection(ThresholdRegion, out ConnectRegion);  //区域连接
                    HOperatorSet.SelectShape(ConnectRegion, out SelectRegion, "area", "and", InspectRectPara.NgAreas[i]+10, 10000000);  //
                    if (SelectRegion.IsInitialized()){
                        if (SelectRegion.CountObj() > 0) {
                            IsOk = false;
                            HOperatorSet.ConcatObj(BadRegions, SelectRegion, out BadRegions);
                        }
                    }
                    //黑色区域检测
                    if (InspectRectPara.MeanGrays[i] - InspectRectPara.DarkGrayRanges[i] > 0)
                        HOperatorSet.Threshold(ReduceImg, out DarkThdRegion, 0, InspectRectPara.MeanGrays[i]
                         - InspectRectPara.DarkGrayRanges[i]); //阈值分割
                    HOperatorSet.Connection(DarkThdRegion, out ConnectRegion);  //区域连接  
                    HOperatorSet.SelectShape(ConnectRegion, out SelectRegion, "area", "and", InspectRectPara.NgAreas[i], 10000000);  //
                    if (SelectRegion.IsInitialized()){
                        if (SelectRegion.CountObj() > 0) {
                            IsOk = false;
                            HOperatorSet.ConcatObj(BadRegions, SelectRegion, out BadRegions);
                        }
                    }
                }
                #endregion
            }
            catch (Exception e0){
                Logger.PopError(e0.Message + e0.Source);
                return false;
            }
            return IsOk;
        }
    }




}
