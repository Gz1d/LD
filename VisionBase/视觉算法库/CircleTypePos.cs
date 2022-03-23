using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
//using TapeVisionBase.外部接口;

namespace VisionBase
{
    public static class CircleTypePos
    {
        /// <summary>
        /// 计算胶带或者金手指中心在平台中的坐标
        /// </summary>
        /// <param name="ImgIn"></param>
        /// <param name="CurHandEyeHomMat">手眼标定矩阵</param>
        /// <param name="IsDn">是否是下相机</param>
        /// <param name="CurMotionPos">当前平台的坐标</param>
        /// <param name="TeachCirclePara0">圆的示教参数</param>
        /// <param name="Row1">模板示教时的行坐标</param>
        /// <param name="Col1">模板示教时的列坐标</param>
        /// <param name="Theta1">模板示教时的角度</param>
        /// <param name="Row2"></param>
        /// <param name="Col2"></param>
        /// <param name="Theta2"></param>
        /// <param name="CenterRow">胶带或者金手指的中心在平台中的行坐标</param>
        /// <param name="CenterCol">胶带或者金手指的中心在平台中的列坐标</param>
        /// <param name="RotAngle"></param>
        /// <returns></returns>
        public static En_ImageProcessResult FindCenter(HObject ImgIn, MyHomMat2D CurHandEyeHomMat, bool IsUp, Point2Db CurMotionPos, St_CirclesParam TeachCirclePara0, HTuple Row1, HTuple Col1, HTuple Theta1,
                                      HTuple Row2, HTuple Col2, HTuple Theta2, out double CenterRow, out double CenterCol, out double RotAngle, out HObject circleCont, out HObject CirContour, out HObject centerCont)
        {
            CenterRow = 0;
            CenterCol = 0;
            RotAngle = 0;
            St_CirclesParam TeachCirclePara;
            TeachCirclePara = TeachCirclePara0;
            HObject ObjContour = new HObject();
            CirContour = new HObject();
            HOperatorSet.GenEmptyObj(out CirContour);
            HObject CircleObj = new HObject();
            centerCont = new HObject();
            circleCont = new HObject();
            // string strLog = "";

            //1.0调整示教的坐标
            try
            {
                MyVisionBase.VectorAngleToTransPt(Row1, Col1, Theta1, Row2, Col2, Theta2, TeachCirclePara0.CenterRows, TeachCirclePara0.CenterCols, out TeachCirclePara.CenterRows, out TeachCirclePara.CenterCols);
                //if (TeachCirclePara.IsThreeFeeder)
                //{
                //    TeachCirclePara.CenterRows[2] = TeachCirclePara0.CenterRows[2];
                //    TeachCirclePara.CenterRows[3] = TeachCirclePara0.CenterRows[3];
                //    TeachCirclePara0.CenterCols[2] = TeachCirclePara0.CenterCols[2];
                //    TeachCirclePara0.CenterCols[3] = TeachCirclePara0.CenterCols[3];
                //}
                HTuple CircleRows = new HTuple(), CircleCols = new HTuple();
                HObject RoiContour = new HObject();
                HTuple ResultRows = new HTuple(), ResultCols = new HTuple(), ArcType0 = new HTuple();
                HTuple CircleCenterRow = new HTuple(), CircleCenterCol = new HTuple(), CircleRadius = new HTuple();
                HTuple StartPhi = new HTuple(), EndPhi = new HTuple(), PointOrder = new HTuple(), IsTrueFlag = new HTuple();
                int cirCount;
                #region  //用示教的参数找出圆
                for (int i = 0; i < TeachCirclePara.CenterRows.Count; i++)
                {
                    //2.0利用存下来的圆的信息，创建圆的边缘点坐标
                    MyVisionBase.GenCirclePts2(TeachCirclePara.CenterRows[i], TeachCirclePara.CenterCols[i], TeachCirclePara.CircleRs[i], TeachCirclePara.StartPhis[i], TeachCirclePara.EndPhis[i]
                        , TeachCirclePara.PointOrders[i], out CircleRows, out CircleCols);

                    //3.0找出边缘点用来拟合圆
                    MyVisionBase.spoke2(ImgIn, TeachCirclePara.Elements[i], TeachCirclePara.DetectHeights[i], 2, 2, TeachCirclePara.Thresholds[i], "all", "first", CircleRows, CircleCols,
                                              TeachCirclePara.Directs[i], out ResultRows, out ResultCols, out ArcType0);
                    HOperatorSet.GenCrossContourXld(out CircleObj, ResultRows, ResultCols, TeachCirclePara.CircleRs[i] / 10, 0.6);
                    HOperatorSet.ConcatObj(CircleObj, CirContour, out CirContour);
                    cirCount = CirContour.CountObj();
                    //4.拟合圆找到圆的中心
                    HObject CircleContour = new HObject();
                    HTuple FitRow = new HTuple(), FitCol = new HTuple(), FitR = new HTuple(), SartP = new HTuple(), EndP = new HTuple(), IsFlag = new HTuple();
                    MyVisionBase.PtsToBestCircle1(out CircleObj, ResultRows, ResultCols, 4, "arc", out FitRow, out FitCol, out FitR, out SartP, out EndP, out IsFlag);
                    if (Math.Abs(TeachCirclePara.CircleRs[i] - FitR.D) > TeachCirclePara.CircleRs[i] * 0.5)
                    {
                        if (ObjContour != null) ObjContour.Dispose();
                        if (CircleObj != null) CircleObj.Dispose();
                        Logger.PopError("拟合的圆的半径和理论值偏差过大。");
                        return En_ImageProcessResult.圆半径和理论值偏差过大;
                    }
                    cirCount = CircleObj.CountObj();
                    HOperatorSet.ConcatObj(CircleObj, CirContour, out CirContour);
                    CircleCenterRow[i] = FitRow.D;
                    CircleCenterCol[i] = FitCol.D;
                    CircleRadius[i] = FitR.D;
                    StartPhi[i] = SartP.D;
                    EndPhi[i] = EndP.D;
                    IsTrueFlag[i] = IsFlag[0];
                }
                #endregion
                HTuple AngleLxPixel = new HTuple(), AngleLx = new HTuple();
                if (CircleCenterRow.TupleLength() != TeachCirclePara0.CenterRows.Count)
                {
                    if (ObjContour != null) ObjContour.Dispose();
                    if (CircleObj != null) CircleObj.Dispose();
                    Logger.PopError("CircleTypePos.FindCenter" + "没有找到圆");
                    return En_ImageProcessResult.圆的数量少于示教的数量;
                }
                #region  //生成圆心十字轮廓，非功能性代码
                HTuple cRow = new HTuple(), cColumn = new HTuple();
                int count = CircleCenterRow.TupleLength();
                cRow = 0;
                cColumn = 0;
                if (count < 4)
                {
                    for (int i = 0; i < count; i++)
                    {
                        cRow += CircleCenterRow[i];
                        cColumn += CircleCenterCol[i];
                    }
                    HOperatorSet.GenCrossContourXld(out centerCont, cRow / count, cColumn / count, 150, 0);
                }
                else if (count == 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        cRow += CircleCenterRow[i];
                        cColumn += CircleCenterCol[i];
                    }
                    HOperatorSet.GenCrossContourXld(out centerCont, cRow / 2, cColumn / 2, 150, 0);
                }
                #endregion
                //5.0X轴到圆的中心连线的角度
                HTuple hv_HandEyeHomMat = new HTuple();
                MyVisionBase.MyHomMatToHalcon(CurHandEyeHomMat, out hv_HandEyeHomMat);      //手眼标定矩阵格式的转换
                HTuple FitHomMat = new HTuple();
                HTuple CircleRowW = new HTuple(), CircleColW = new HTuple();
                HTuple CircleRowAdj = new HTuple();
                //5.1调整行坐标
                //CircleRowAdj =CircleCenterRow;
                CircleRowAdj = CircleCenterRow.Clone();
                MyVisionBase.AdjImgRow(ImgIn, ref CircleRowAdj);
                //5.2像素坐标调整到世界坐标，消除相机坐标系与世界坐标系之间的夹角
                HOperatorSet.AffineTransPoint2d(hv_HandEyeHomMat, CircleCenterCol, CircleRowAdj, out CircleColW, out CircleRowW);
                if (CircleColW.Length >= 2)
                {
                    if (CircleColW[0].D < CircleColW[1].D)
                    {
                        HOperatorSet.AngleLl(0, 0, 1, 0, CircleColW[0].D, CircleRowW[0].D, CircleColW[1].D, CircleRowW[1].D, out AngleLx); //圆中心连线与世界坐标X轴之间的夹角
                        HOperatorSet.AngleLl(0, 0, 1, 0, CircleCenterCol[0].D, CircleRowAdj[0].D, CircleCenterCol[1].D, CircleRowAdj[1].D, out AngleLxPixel);
                    }
                    else
                    {
                        HOperatorSet.AngleLl(0, 0, 1, 0, CircleColW[1].D, CircleRowW[1].D, CircleColW[0].D, CircleRowW[0].D, out AngleLx);
                        HOperatorSet.AngleLl(0, 0, 1, 0, CircleCenterCol[1].D, CircleRowAdj[1].D, CircleCenterCol[0].D, CircleRowAdj[0].D, out AngleLxPixel);
                    }
                    if ((Math.PI / 4) < AngleLxPixel && AngleLxPixel < (Math.PI * 0.75))
                    {
                        AngleLxPixel = AngleLxPixel - Math.PI / 2;
                    }
                    else if (AngleLxPixel < (-45.0 / 180.0 * Math.PI))
                    {
                        AngleLxPixel = AngleLxPixel + Math.PI / 2;
                    }
                }
                else if (CircleColW.Length == 1)
                {
                    AngleLxPixel = 0;
                    AngleLx = 0;
                }
                //6.0生成图像坐标原点到示教中心的平移放射矩阵
                if (CircleColW.Length >= 2)
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, (CircleCenterRow[1].D + CircleCenterRow[0].D) / 2, (CircleCenterCol[1].D + CircleCenterCol[0].D) / 2, AngleLxPixel, out FitHomMat);
                else if (CircleColW.Length == 1)
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, CircleCenterRow[0].D, CircleCenterCol[0].D, AngleLxPixel, out FitHomMat);
                HTuple TapeCenterRow = new HTuple(), TapeCenterCol = new HTuple();
                HOperatorSet.AffineTransPoint2d(FitHomMat, TeachCirclePara.OffsetPixelY, TeachCirclePara.OffsetPixelX, out TapeCenterRow, out TapeCenterCol);  //计算出贴标中心在图像坐标系中的位置
                HObject LineContour = new HObject(), OffsetArrowContour = new HObject();
                HOperatorSet.GenContourPolygonXld(out LineContour, CircleCenterRow, CircleCenterCol);
                double Dist = new double();
                Dist = Math.Sqrt(TeachCirclePara.OffsetPixelX * TeachCirclePara.OffsetPixelX + TeachCirclePara.OffsetPixelY * TeachCirclePara.OffsetPixelY);
                //7.0定位中心指向实际贴胶的箭头
                if (CircleColW.Length >= 2)
                    MyVisionBase.GenArrowContourXld(out OffsetArrowContour, (CircleCenterRow[1].D + CircleCenterRow[0].D) / 2, (CircleCenterCol[1].D + CircleCenterCol[0].D) / 2, TapeCenterRow, TapeCenterCol, Dist / 20, Dist / 20);
                else if (CircleColW.Length == 1)
                    MyVisionBase.GenArrowContourXld(out OffsetArrowContour, CircleCenterRow[0].D, CircleCenterCol[0].D, TapeCenterRow, TapeCenterCol, Dist / 20, Dist / 20);

                HOperatorSet.ConcatObj(LineContour, OffsetArrowContour, out OffsetArrowContour);
                HOperatorSet.ConcatObj(OffsetArrowContour, CirContour, out CirContour);
                //8.0需要转换到平台坐标
                HTuple TransRow = new HTuple(), TransCol = new HTuple();
                //8.0图片原点调整到右下角
                MyVisionBase.AdjImgRow(ImgIn, ref TapeCenterRow);
                HOperatorSet.AffineTransPoint2d(hv_HandEyeHomMat, TapeCenterCol, TapeCenterRow, out TransCol, out TransRow);
                //9.0减去平台的坐标，得到产品相对于平台旋转中心的坐标，下相机加上平台坐标，得到胶带在平台中的绝对位置

                if (ObjContour != null) ObjContour.Dispose();
                if (CircleObj != null) CircleObj.Dispose();

                if (IsUp)
                {
                    CenterRow = TransRow.D - CurMotionPos.Row;
                    CenterCol = TransCol.D - CurMotionPos.Col;
                }

                else
                {
                    CenterRow = TransRow.D + CurMotionPos.Row;
                    CenterCol = TransCol.D + CurMotionPos.Col;
                }
                RotAngle = AngleLx.D;
                St_Position NowPos = new St_Position();
                // St_Position NowPos = MotionController.GetCurrentPos(true);
                //if (TeachCirclePara.IsThreeFeeder)
                //{
                //    HTuple DistMarkToFinger = new HTuple();
                //    HOperatorSet.DistancePl((CircleCenterCol[0].D + CircleCenterCol[1].D) / 2.0, (CircleCenterRow[0].D + CircleCenterRow[1].D) / 2.0,
                //                             CircleCenterCol[2].D, CircleCenterRow[2].D, CircleCenterCol[3].D, CircleCenterRow[3].D, out DistMarkToFinger);
                //    HTuple Sx = new HTuple(), Sy = new HTuple(), Phi = new HTuple(), Theta = new HTuple(), Tx = new HTuple(), Ty = new HTuple();
                //    HOperatorSet.HomMat2dToAffinePar(hv_HandEyeHomMat, out Sx, out Sy, out Phi, out Theta, out Tx, out Ty);
                //    DistMarkToFinger = DistMarkToFinger.D * Sx;
                //    double OffSetX0 = 0,OffSetX1 =0;
                //    OffSetX0 = (DistMarkToFinger.D - 3.5) / 2;
                //    //CenterCol = CenterCol - OffSetX0 + 0.3 * OffSetX0;//laobanyaojiade
                //    CenterCol = CenterCol - OffSetX0;
                //    OffSetX1 = DistMarkToFinger.D - 3.5;
                //    //if (DistMarkToFinger.D < TeachCirclePara.DistMarkToFinger)
                //    //    return En_ImageProcessResult.产品三号位置弯曲;
                //   // Logger.Pop1(string.Format("Y轴位置={0},变形量={1}", CurMotionPos.Row, OffSetX0), "产品变形量");
                //    if (Math.Abs(OffSetX1) > 0.3)
                //        return En_ImageProcessResult.产品三号位置弯曲;
                //}
                if (double.IsNaN(CenterCol) || double.IsNaN(CenterRow))
                {
                    Logger.PopError("找圆定位出错坐标非数字");
                    return En_ImageProcessResult.找圆定位出错坐标非数字;
                }

                return En_ImageProcessResult.OK;
            }
            catch (Exception e)
            {
                Logger.PopError("CircleTypePos.FindCenter:" + e.Message + e.Source);
                return En_ImageProcessResult.找圆失败;
            }
        }


        public static En_ImageProcessResult FindCenter1(HObject ImgIn, St_CirclesParam TeachCirclePara0, HTuple Row1, HTuple Col1, HTuple Theta1,
                                HTuple Row2, HTuple Col2, HTuple Theta2, out HTuple CircleCenterRow, out HTuple CircleCenterCol, out HTuple CircleR, out HObject circleCont, out HObject CirContour, out HObject centerCont)
        {
            CircleCenterRow = new HTuple();
            CircleCenterCol = new HTuple();
            CircleR = new HTuple();
            St_CirclesParam TeachCirclePara;
            TeachCirclePara = TeachCirclePara0;
            HObject ObjContour = new HObject();
            CirContour = new HObject();
            HOperatorSet.GenEmptyObj(out CirContour);
            HObject CircleObj = new HObject();
            centerCont = new HObject();
            circleCont = new HObject();           
            try
            {
                //1.0调整示教的坐标
                MyVisionBase.VectorAngleToTransPt(Row1, Col1, Theta1, Row2, Col2, Theta2, TeachCirclePara0.CenterRows, TeachCirclePara0.CenterCols, out TeachCirclePara.CenterRows,
                    out TeachCirclePara.CenterCols);
                HTuple CircleRows = new HTuple(), CircleCols = new HTuple();
                HObject RoiContour = new HObject();
                HTuple ResultRows = new HTuple(), ResultCols = new HTuple(), ArcType0 = new HTuple();
                HTuple CircleRadius = new HTuple();
                HTuple StartPhi = new HTuple(), EndPhi = new HTuple(), PointOrder = new HTuple(), IsTrueFlag = new HTuple();
                int cirCount;
                #region  //用示教的参数找出圆
                for (int i = 0; i < TeachCirclePara.CenterRows.Count; i++)
                {
                    //2.0利用存下来的圆的信息，创建圆的边缘点坐标
                    MyVisionBase.GenCirclePts2(TeachCirclePara.CenterRows[i], TeachCirclePara.CenterCols[i], TeachCirclePara.CircleRs[i], TeachCirclePara.StartPhis[i],
                        TeachCirclePara.EndPhis[i] , TeachCirclePara.PointOrders[i], out CircleRows, out CircleCols);
                    //3.0找出边缘点用来拟合圆
                    MyVisionBase.spoke2(ImgIn, TeachCirclePara.Elements[i], TeachCirclePara.DetectHeights[i], 2, 2, TeachCirclePara.Thresholds[i], "all", "first", CircleRows, CircleCols,
                                              TeachCirclePara.Directs[i], out ResultRows, out ResultCols, out ArcType0);
                    HOperatorSet.GenCrossContourXld(out CircleObj, ResultRows, ResultCols, TeachCirclePara.CircleRs[i] / 10, 0.6);
                    HOperatorSet.ConcatObj(CircleObj, CirContour, out CirContour);
                    cirCount = CirContour.CountObj();
                    //4.拟合圆找到圆的中心
                    HObject CircleContour = new HObject();
                    HTuple FitRow = new HTuple(), FitCol = new HTuple(), FitR = new HTuple(), SartP = new HTuple(), EndP = new HTuple(), IsFlag = new HTuple();
                    MyVisionBase.PtsToBestCircle1(out CircleObj, ResultRows, ResultCols, 4, "arc", out FitRow, out FitCol, out FitR, out SartP, out EndP, out IsFlag);
                    if (Math.Abs(TeachCirclePara.CircleRs[i] - FitR.D) > TeachCirclePara.CircleRs[i] * 0.5)
                    {
                        if (ObjContour != null) ObjContour.Dispose();
                        if (CircleObj != null) CircleObj.Dispose();
                        Logger.PopError("拟合的圆的半径和理论值偏差过大。");
                        return En_ImageProcessResult.圆半径和理论值偏差过大;
                    }
                    cirCount = CircleObj.CountObj();
                    HOperatorSet.ConcatObj(CircleObj, CirContour, out CirContour);
                    CircleCenterRow[i] = FitRow.D;
                    CircleCenterCol[i] = FitCol.D;
                    CircleRadius[i] = FitR.D;
                    StartPhi[i] = SartP.D;
                    EndPhi[i] = EndP.D;
                    IsTrueFlag[i] = IsFlag[0];
                }
                CircleR = CircleRadius;
                #endregion
                HTuple AngleLxPixel = new HTuple();
                if (CircleCenterRow.TupleLength() != TeachCirclePara0.CenterRows.Count)
                {
                    if (ObjContour != null) ObjContour.Dispose();
                    if (CircleObj != null) CircleObj.Dispose();
                    Logger.PopError("CircleTypePos.FindCenter" + "没有找到圆");
                    return En_ImageProcessResult.圆的数量少于示教的数量;
                }
                #region  //生成圆心十字轮廓，非功能性代码
                HTuple cRow = new HTuple(), cColumn = new HTuple();
                int count = CircleCenterRow.TupleLength();
                cRow = 0;
                cColumn = 0;
                if (count < 4)
                {
                    for (int i = 0; i < count; i++)
                    {
                        cRow += CircleCenterRow[i];
                        cColumn += CircleCenterCol[i];
                    }
                    HOperatorSet.GenCrossContourXld(out centerCont, cRow / count, cColumn / count, 150, 0);
                }
                else if (count == 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        cRow += CircleCenterRow[i];
                        cColumn += CircleCenterCol[i];
                    }
                    HOperatorSet.GenCrossContourXld(out centerCont, cRow / 2, cColumn / 2, 150, 0);
                }
                #endregion
                //5.0X轴到圆的中心连线的角度
                HTuple FitHomMat = new HTuple();
                HTuple CircleRowAdj = new HTuple();
                //5.1调整行坐标
                //CircleRowAdj =CircleCenterRow;
                CircleRowAdj = CircleCenterRow.Clone();
                MyVisionBase.AdjImgRow(ImgIn, ref CircleRowAdj);
                //5.2像素坐标调整到世界坐标，消除相机坐标系与世界坐标系之间的夹角
                if (CircleCenterCol.Length >= 2)
                {
                    if (CircleCenterCol[0].D < CircleCenterCol[0].D)
                    {

                        HOperatorSet.AngleLl(0, 0, 1, 0, CircleCenterCol[0].D, CircleRowAdj[0].D, CircleCenterCol[1].D, CircleRowAdj[1].D, out AngleLxPixel); //圆中心连线与相机坐标X轴之间的夹角
                    }
                    else
                    {
                        HOperatorSet.AngleLl(0, 0, 1, 0, CircleCenterCol[1].D, CircleRowAdj[1].D, CircleCenterCol[0].D, CircleRowAdj[0].D, out AngleLxPixel);
                    }
                    if ((Math.PI / 4) < AngleLxPixel && AngleLxPixel < (Math.PI * 0.75))
                    {
                        AngleLxPixel = AngleLxPixel - Math.PI / 2;
                    }
                    else if (AngleLxPixel < (-45.0 / 180.0 * Math.PI))
                    {
                        AngleLxPixel = AngleLxPixel + Math.PI / 2;
                    }
                }
                else if (CircleCenterCol.Length == 1)
                {
                    AngleLxPixel = 0;
                }
                //6.0生成图像坐标原点到示教中心的平移放射矩阵
                if (CircleCenterCol.Length >= 2)
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, (CircleCenterRow[1].D + CircleCenterRow[0].D) / 2, (CircleCenterCol[1].D + CircleCenterCol[0].D) / 2, AngleLxPixel, out FitHomMat);
                else if (CircleCenterCol.Length == 1)
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, CircleCenterRow[0].D, CircleCenterCol[0].D, AngleLxPixel, out FitHomMat);
                HTuple TapeCenterRow = new HTuple(), TapeCenterCol = new HTuple();
                HOperatorSet.AffineTransPoint2d(FitHomMat, TeachCirclePara.OffsetPixelY, TeachCirclePara.OffsetPixelX, out TapeCenterRow, out TapeCenterCol);  //计算出贴标中心在图像坐标系中的位置
                HObject LineContour = new HObject(), OffsetArrowContour = new HObject();
                HOperatorSet.GenContourPolygonXld(out LineContour, CircleCenterRow, CircleCenterCol);
                double Dist = new double();
                Dist = Math.Sqrt(TeachCirclePara.OffsetPixelX * TeachCirclePara.OffsetPixelX + TeachCirclePara.OffsetPixelY * TeachCirclePara.OffsetPixelY);
                ////7.0定位中心指向实际贴胶的箭头
                //if (CircleCenterCol.Length >= 2)
                //    MyVisionBase.GenArrowContourXld(out OffsetArrowContour, (CircleCenterRow[1].D + CircleCenterRow[0].D) / 2, (CircleCenterCol[1].D + CircleCenterCol[0].D) / 2, TapeCenterRow, TapeCenterCol, Dist / 20, Dist / 20);
                //else if (CircleCenterCol.Length == 1)
                //    MyVisionBase.GenArrowContourXld(out OffsetArrowContour, CircleCenterRow[0].D, CircleCenterCol[0].D, TapeCenterRow, TapeCenterCol, Dist / 20, Dist / 20);

                //HOperatorSet.ConcatObj(LineContour, OffsetArrowContour, out OffsetArrowContour);
                //HOperatorSet.ConcatObj(OffsetArrowContour, CirContour, out CirContour);
                ////8.0需要转换到平台坐标
                //HTuple TransRow = new HTuple(), TransCol = new HTuple();
                ////8.0图片原点调整到右下角
                //MyVisionBase.AdjImgRow(ImgIn, ref TapeCenterRow);
                //HOperatorSet.AffineTransPoint2d(hv_HandEyeHomMat, TapeCenterCol, TapeCenterRow, out TransCol, out  TransRow);
                ////9.0减去平台的坐标，得到产品相对于平台旋转中心的坐标，下相机加上平台坐标，得到胶带在平台中的绝对位置
                if (ObjContour != null) ObjContour.Dispose();
                if (CircleObj != null) CircleObj.Dispose();
                return En_ImageProcessResult.OK;
            }
            catch (Exception e)
            {
                Logger.PopError("CircleTypePos.FindCenter:" + e.Message + e.Source);
                return En_ImageProcessResult.找圆失败;
            }
        }


        public static bool CircleLocation(HObject ImgIn, St_CirclesParam TeachCirclePara0, out St_VectorAngle VectorAngleOut, out HObject circleCont, out HObject CirContour, out HObject centerCont)
        {
            St_CirclesParam TeachCirclePara;
            TeachCirclePara = TeachCirclePara0;
            HObject ObjContour = new HObject();
            CirContour = new HObject();
            HOperatorSet.GenEmptyObj(out CirContour);
            HObject CircleObj = new HObject();
            centerCont = new HObject();
            circleCont = new HObject();
            VectorAngleOut = new St_VectorAngle(true);

            HTuple CircleRows = new HTuple(), CircleCols = new HTuple();
            HObject RoiContour = new HObject();
            HTuple ResultRows = new HTuple(), ResultCols = new HTuple(), ArcType0 = new HTuple();
            HTuple CircleCenterRow = new HTuple(), CircleCenterCol = new HTuple(), CircleRadius = new HTuple();
            HTuple StartPhi = new HTuple(), EndPhi = new HTuple(), PointOrder = new HTuple(), IsTrueFlag = new HTuple();
            int cirCount;
            try
            {
                #region
                for (int i = 0; i < TeachCirclePara.Count; i++)
                {
                    //2.0利用存下来的圆的信息，创建圆的边缘点坐标
                    MyVisionBase.GenCirclePts2(TeachCirclePara.CenterRows[i], TeachCirclePara.CenterCols[i], TeachCirclePara.CircleRs[i], TeachCirclePara.StartPhis[i], TeachCirclePara.EndPhis[i]
                        , TeachCirclePara.PointOrders[i], out CircleRows, out CircleCols);
                    //3.0找出边缘点用来拟合圆
                    MyVisionBase.spoke2(ImgIn, TeachCirclePara.Elements[i], TeachCirclePara.DetectHeights[i], 2, 2, TeachCirclePara.Thresholds[i], "all", "first", CircleRows, CircleCols,
                                              TeachCirclePara.Directs[i], out ResultRows, out ResultCols, out ArcType0);
                    HOperatorSet.GenCrossContourXld(out CircleObj, ResultRows, ResultCols, TeachCirclePara.CircleRs[i] / 10, 0.6);
                    HOperatorSet.ConcatObj(CircleObj, CirContour, out CirContour);
                    cirCount = CirContour.CountObj();

                    HTuple ElectRows = ResultRows, ElectCols = ResultCols;
                    int lengthResultRows = ResultRows.Length;
                    int StartNum = (int)(lengthResultRows * 0.3);
                    int RemoveNum = (int)(lengthResultRows * 0.2);
                    for (int j = StartNum; j < StartNum + RemoveNum; j++)
                    {
                        if (j < lengthResultRows && StartNum + RemoveNum < lengthResultRows)
                        {
                            ElectRows = ElectRows.TupleRemove(StartNum);
                            ElectCols = ElectCols.TupleRemove(StartNum);
                        }
                    }
                    //4.拟合圆找到圆的中心
                    HObject CircleContour = new HObject();
                    HTuple FitRow = new HTuple(), FitCol = new HTuple(), FitR = new HTuple(), SartP = new HTuple(), EndP = new HTuple(), IsFlag = new HTuple();
                    MyVisionBase.PtsToBestCircle1(out CircleObj, ElectRows, ElectCols, 4, "arc", out FitRow, out FitCol, out FitR, out SartP, out EndP, out IsFlag);
                    if (Math.Abs(TeachCirclePara.CircleRs[i] - FitR.D) > TeachCirclePara.CircleRs[i] * 0.2)
                    {
                        HOperatorSet.ConcatObj(CircleObj, CirContour, out CirContour);
                        Logger.PopError("找出圆的半径大于理论值。");
                        return false;
                    }
                    cirCount = CircleObj.CountObj();
                    HOperatorSet.ConcatObj(CircleObj, CirContour, out CirContour);
                    CircleCenterRow[i] = FitRow.D;
                    CircleCenterCol[i] = FitCol.D;
                    CircleRadius[i] = FitR.D;
                    StartPhi[i] = SartP.D;
                    EndPhi[i] = EndP.D;
                    IsTrueFlag[i] = IsFlag[0];
                }
                HTuple AngleLxPixel = new HTuple(), AngleLx = new HTuple();
                if (CircleCenterRow.TupleLength() != TeachCirclePara0.Count)
                {
                    Logger.PopError("CircleTypePos.FindCenter" + "没有找到圆");
                    return false;
                }

                #region  //生成圆心十字轮廓，非功能性代码
                HTuple cRow = new HTuple(), cColumn = new HTuple();
                int count = CircleCenterRow.TupleLength();
                cRow = 0;
                cColumn = 0;
                if (count < 4)
                {
                    for (int i = 0; i < count; i++)
                    {
                        cRow += CircleCenterRow[i];
                        cColumn += CircleCenterCol[i];
                    }
                    HOperatorSet.GenCrossContourXld(out centerCont, cRow / count, cColumn / count, 150, 0);
                }
                else if (count == 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        cRow += CircleCenterRow[i];
                        cColumn += CircleCenterCol[i];
                    }
                    HOperatorSet.GenCrossContourXld(out centerCont, cRow / 2, cColumn / 2, 150, 0);
                }
                #endregion
                //5.0X轴到圆的中心连线的角度
                HTuple CircleRowAdj = new HTuple();
                CircleRowAdj = CircleCenterRow.Clone();
                //5.1调整行坐标
                MyVisionBase.AdjImgRow(ImgIn, ref CircleRowAdj);
                if (CircleRowAdj.Length >= 2)
                {
                    if (CircleCenterCol[0].D < CircleCenterCol[1].D)
                    {
                        HOperatorSet.AngleLl(0, 0, 1, 0, CircleCenterCol[0].D, CircleRowAdj[0].D, CircleCenterCol[1].D, CircleRowAdj[1].D, out AngleLxPixel); //圆中心连线与相机坐标X轴之间的夹角
                    }
                    else
                    {
                        HOperatorSet.AngleLl(0, 0, 1, 0, CircleCenterCol[1].D, CircleRowAdj[1].D, CircleCenterCol[0].D, CircleRowAdj[0].D, out AngleLxPixel);
                    }
                }
                else if (CircleRowAdj.Length == 1)
                {
                    AngleLxPixel = 0;
                }
                VectorAngleOut = new St_VectorAngle(cRow / count, cColumn / count, AngleLxPixel);
                if (double.IsNaN(cColumn / count) || double.IsNaN(cRow / count))
                {
                    Logger.PopError("计算出错，非数字");
                    return false;
                }
                return true;
                #endregion
            }
            catch (Exception e0)
            {
                Logger.PopError(e0.Message + e0.Source);
                return false;
            }


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ImgIn">输入图像</param>
        /// <param name="TeachCirclePara0">圆的示教参数</param>
        /// <param name="CircleRow">圆的中心坐标</param>
        /// <param name="CircleCol">圆的中心坐标</param>
        /// <param name="CircleR">圆的半径</param>
        /// <param name="CircleStartPhi">拟合圆的起始角度</param>
        /// <param name="CircleEndPhi">拟合圆的终止角度</param>
        /// <param name="circleCont">找到的圆的轮廓</param>
        /// <param name="CirContour">拟合圆的点的轮廓</param>
        /// <param name="centerCross">圆的中心轮廓</param>
        /// <returns></returns>
        public static En_ImageProcessResult FindCircle(HObject ImgIn, St_CirclesParam TeachCirclePara0, out HTuple CircleRow, out HTuple CircleCol, out HTuple CircleR, out HTuple CircleStartPhi, out HTuple CircleEndPhi,
            out HObject circleCont, out HObject CirContour, out HObject centerCross)
        {
            CircleRow = new HTuple();
            CircleCol = new HTuple();
            CircleR = new HTuple();
            CircleStartPhi = new HTuple();
            CircleEndPhi = new HTuple();
            St_CirclesParam TeachCirclePara;
            TeachCirclePara = TeachCirclePara0;
            HObject ObjContour = new HObject();
            CirContour = new HObject();
            HOperatorSet.GenEmptyObj(out CirContour);
            HObject CircleObj = new HObject();
            HObject cross = new HObject();
            centerCross = new HObject();
            circleCont = new HObject();
            HOperatorSet.GenEmptyObj(out centerCross);
            HOperatorSet.GenEmptyObj(out circleCont);
            HTuple CircleRows = new HTuple(), CircleCols = new HTuple();
            HObject RoiContour = new HObject();
            HTuple ResultRows = new HTuple(), ResultCols = new HTuple(), ArcType0 = new HTuple();
            HTuple CircleCenterRow = new HTuple(), CircleCenterCol = new HTuple(), CircleRadius = new HTuple();
            HTuple StartPhi = new HTuple(), EndPhi = new HTuple(), PointOrder = new HTuple(), IsTrueFlag = new HTuple();
            //string strLog = "";
            int cirCount;
            try
            {
                #region
                for (int i = 0; i < TeachCirclePara.CenterRows.Count; i++)
                {
                    //2.0利用存下来的圆的信息，创建圆的边缘点坐标
                    MyVisionBase.GenCirclePts2(TeachCirclePara.CenterRows[i], TeachCirclePara.CenterCols[i], TeachCirclePara.CircleRs[i], TeachCirclePara.StartPhis[i], TeachCirclePara.EndPhis[i]
                        , TeachCirclePara.PointOrders[i], out CircleRows, out CircleCols); ;
                    //3.0找出边缘点用来拟合圆
                    MyVisionBase.spoke2(ImgIn, TeachCirclePara.Elements[i], TeachCirclePara.DetectHeights[i], 2, 2, TeachCirclePara.Thresholds[i], "all", "first", CircleRows, CircleCols,
                                              TeachCirclePara.Directs[i], out ResultRows, out ResultCols, out ArcType0);
                    HOperatorSet.GenCrossContourXld(out CircleObj, ResultRows, ResultCols, TeachCirclePara.CircleRs[i] / 10, 0.6);
                    HOperatorSet.ConcatObj(CircleObj, CirContour, out CirContour);
                    cirCount = CirContour.CountObj();

                    //4.拟合圆找到圆的中心
                    HObject CircleContour = new HObject();
                    HTuple FitRow = new HTuple(), FitCol = new HTuple(), FitR = new HTuple(), SartP = new HTuple(), EndP = new HTuple(), IsFlag = new HTuple();
                    MyVisionBase.PtsToBestCircle1(out CircleObj, ResultRows, ResultCols, 4, "arc", out FitRow, out FitCol, out FitR, out SartP, out EndP, out IsFlag);
                    if (Math.Abs(TeachCirclePara.CircleRs[i] - FitR.D) > TeachCirclePara.CircleRs[i] * 0.2)
                    {
                        HOperatorSet.ConcatObj(CircleObj, circleCont, out circleCont);
                        Logger.PopError("找出圆的半径大于理论值。");
                        return En_ImageProcessResult.圆半径和理论值偏差过大;
                    }
                    cirCount = CircleObj.CountObj();
                    HOperatorSet.ConcatObj(CircleObj, circleCont, out circleCont);
                    HOperatorSet.GenCrossContourXld(out cross, FitRow, FitCol, FitR / 2, 0);
                    HOperatorSet.ConcatObj(cross, centerCross, out centerCross);
                    CircleCenterRow[i] = FitRow.D;
                    CircleCenterCol[i] = FitCol.D;
                    CircleRadius[i] = FitR.D;
                    StartPhi[i] = SartP.D;
                    EndPhi[i] = EndP.D;
                    IsTrueFlag[i] = IsFlag[0];
                }
                CircleRow = CircleCenterRow;
                CircleCol = CircleCenterCol;
                CircleR = CircleRadius;
                CircleStartPhi = StartPhi;
                CircleEndPhi = EndPhi;
                return En_ImageProcessResult.OK;
                #endregion
            }
            catch (Exception e0)
            {
                Logger.PopError(e0.Message + e0.Source);
                return En_ImageProcessResult.未知NG;
            }
        }

        public static bool GenCirCenter(HTuple RowIN, HTuple ColIN, HTuple Dist, out HTuple RowOut, out HTuple ColOut)
        {
            RowOut = new HTuple();
            ColOut = new HTuple();
            if (RowIN.Length == 1 && ColIN.Length == 1 && Dist.Length == 1)
            {
                RowOut[0] = RowIN[0].D - Dist;
                ColOut[0] = ColIN[0].D - Dist;
                RowOut[1] = RowIN[0].D - Dist;
                ColOut[1] = ColIN[0].D + Dist;
                RowOut[2] = RowIN[0].D + Dist;
                ColOut[2] = ColIN[0].D - Dist;
                RowOut[3] = RowIN[0].D + Dist;
                ColOut[3] = ColIN[0].D + Dist;
            }
            return true;
        }



    }
}
