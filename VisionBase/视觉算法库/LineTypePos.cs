using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Drawing;
using VisionBase.Matching;

//using TapeVisionBase.外部接口;

namespace VisionBase
{
    public static class LineTypePos
    {
  
        public static bool FindLine(St_LinesParam LineParaIn, HObject ImgIn, out HTuple OutLineRow1, out HTuple OutLineCol1, 
            out HTuple OutLineRow2, out HTuple OutLineCol2, out HObject lines, out HObject linesContour)
        {
            OutLineRow1 = new HTuple();
            OutLineCol1 = new HTuple();
            OutLineRow2 = new HTuple();
            OutLineCol2 = new HTuple();
            HOperatorSet.GenEmptyObj(out lines);
            HOperatorSet.GenEmptyObj(out linesContour);
            St_LinesParam TeachLinePara;
            TeachLinePara = LineParaIn;
            HTuple LineRow1U = new HTuple(), LineCol1U = new HTuple(), LineRow2U = new HTuple(), LineCol2U = new HTuple();
            HObject RoiContour = new HObject();
            HTuple ResultRows = new HTuple(), ResultCols = new HTuple();
            HTuple LengthL = new HTuple();
            HObject LineContourI = new HObject();
            for (int i = 0; i < TeachLinePara.Col2s.Count(); i++)
            {
                //2.0利用卡尺工具找出边界点
                if (!MyVisionBase.Rake1(ImgIn, out RoiContour, TeachLinePara.Elements[i], TeachLinePara.DetectHeights[i], 2, 2,
                    TeachLinePara.Thresholds[i], "all", "first",TeachLinePara.Row1s[i], TeachLinePara.Col1s[i],
                    TeachLinePara.Row2s[i], TeachLinePara.Col2s[i], out ResultRows, out ResultCols))
                {
                    Logger.PopError("直线定位找边界点时Rake1返回false");
                    return false;
                }
                //提出要删除的中心点
                MyVisionBase.ClipCenterElement(ResultRows, TeachLinePara.CenterClips[i], out ResultRows);
                MyVisionBase.ClipCenterElement(ResultCols, TeachLinePara.CenterClips[i], out ResultCols);
                HObject FitLineContour = new HObject();
                HTuple LineRow1 = new HTuple(), LineCol1 = new HTuple(), LineRow2 = new HTuple(), LineCol2 = new HTuple(), DistPtToLine;
                MyVisionBase.PtsToBestLine(out FitLineContour, ResultRows, ResultCols, 3, out LineRow1, out LineCol1,
                    out LineRow2, out LineCol2, out DistPtToLine);
                LineRow1U[i] = LineRow1[0];
                LineCol1U[i] = LineCol1[0];
                LineRow2U[i] = LineRow2[0];
                LineCol2U[i] = LineCol2[0];
                HOperatorSet.GenContourPolygonXld(out LineContourI, LineRow1.TupleConcat(LineRow2), LineCol1.TupleConcat(LineCol2)); //生成直线的轮廓
                HOperatorSet.ConcatObj(LineContourI, lines, out lines);
                HOperatorSet.DistancePp(LineRow1, LineCol1, LineRow2, LineRow2, out LengthL);
                HTuple PtSize = LengthL / 60;
                if (LengthL / 60 < 10) PtSize = 10.0;
                HOperatorSet.GenCrossContourXld(out LineContourI, ResultRows, ResultCols, PtSize, 0.6);  //生成 拟合直线点的轮廓
                HOperatorSet.ConcatObj(LineContourI, linesContour, out linesContour);
                FitLineContour.Dispose();
            }
            if (LineRow1U.TupleLength() != TeachLinePara.Col2s.Count() || LineCol1U.TupleLength() != TeachLinePara.Col2s.Count()
                || LineRow2U.TupleLength() != TeachLinePara.Col2s.Count()){
                Logger.Pop("找出的线的数量和示教的数量不一致");
                return false;
            }
            OutLineRow1 = LineRow1U;
            OutLineCol1 = LineCol1U;
            OutLineRow2 = LineRow2U;
            OutLineCol2 = LineCol2U;

            RoiContour.Dispose();
            LineContourI.Dispose();
            return true;
        }


        /// <summary>
        /// 计算平台的放置坐标
        /// </summary>
        /// <param name="ProductX"></param>
        /// <param name="ProductY"></param>
        /// <param name="ProductTheta"></param>
        /// <param name="TapeX"></param>
        /// <param name="TapeY"></param>
        /// <param name="TapeTheta"></param>
        /// <param name="Wx"></param>
        /// <param name="Wy"></param>
        /// <param name="WoTheta">需要结合工位的平台示教角度，目标角度在示教角度上加上 WoTheta</param>
        /// <returns></returns>
        public static bool CalculateMotionPos1(double ProductX, double ProductY, double ProductTheta, double TapeX, 
            double TapeY, double TapeTheta, out double Wx, out double Wy, out double WoTheta)
        {
            try{
                HTuple HomMat = new HTuple();// 产品旋转的旋转矩阵
                HOperatorSet.VectorAngleToRigid(ProductX, ProductY, ProductTheta, TapeX, TapeY, TapeTheta, out HomMat);
                HTuple Sx = new HTuple(), Sy = new HTuple(), Phi = new HTuple(), Theta = new HTuple(), Tx = new HTuple(), Ty = new HTuple();
                HOperatorSet.HomMat2dToAffinePar(HomMat, out Sx, out Sy, out Phi, out Theta, out Tx, out Ty);
                Wx = Tx.D;
                Wy = Ty.D;
                WoTheta = Phi.D;
            }
            catch (Exception e0)
            {
                Wx = 0;
                Wy = 0;
                WoTheta = 0;
                Logger.PopError(e0.Message + e0.Source);
                return false;
            }
            return true;
        }



        /// <summary>
        /// 判断示教直线与找出直线的角度
        /// </summary>
        /// <param name="TeachLinePara"></param>
        /// <param name="LineRow1U"></param>
        /// <param name="LineCol1U"></param>
        /// <param name="LineRow2U"></param>
        /// <param name="LineCol2U"></param>
        /// <returns></returns>
        public static bool JugeLine(St_LinesParam TeachLinePara, HTuple LineRow1U, HTuple LineCol1U, HTuple LineRow2U, HTuple LineCol2U)
        {
            HTuple Pt1Row = new HTuple(), Pt1Col = new HTuple(), Pt2Row = new HTuple(), Pt2Col = new HTuple();
            HTuple IsParallel = new HTuple();
            if (LineRow1U.Length != 4 || LineCol1U.Length != 4 || LineRow2U.Length != 4 || LineRow2U.Length != 4)
                return false;
            List<double> ListRow1 = new List<double>();
            List<double> ListCol1 = new List<double>();
            List<double> ListRow2 = new List<double>();
            List<double> ListCol2 = new List<double>();
            ListRow1 = TeachLinePara.Row1s;
            ListCol1 = TeachLinePara.Col1s;
            ListRow2 = TeachLinePara.Row2s;
            ListCol2 = TeachLinePara.Col2s;
            try{
                //示教直线的交点
                HOperatorSet.IntersectionLl(ListRow1[0], ListCol1[0], ListRow2[0], ListCol2[0], ListRow1[1],
                    ListCol1[1], ListRow2[1], ListCol2[1], out Pt1Row, out Pt1Col, out IsParallel); //交点1
                HOperatorSet.IntersectionLl(ListRow1[3], ListCol1[3], ListRow2[3], ListCol2[3], ListRow1[0],
                    ListCol1[0], ListRow2[0], ListCol2[0], out Pt2Row, out Pt2Col, out IsParallel); //交点4
                HTuple TeachLength = new HTuple(), NowLength = new HTuple();
                HOperatorSet.DistancePp(Pt1Row, Pt1Col, Pt2Row, Pt2Col, out TeachLength);
                //找出来的直线交点
                HOperatorSet.IntersectionLl(LineRow1U[0], LineCol1U[0], LineRow2U[0], LineCol2U[0], LineRow1U[1],
                    LineCol1U[1], LineRow2U[1], LineCol2U[1], out Pt1Row, out Pt1Col, out IsParallel);
                HOperatorSet.IntersectionLl(LineRow1U[3], LineCol1U[3], LineRow2U[3], LineCol2U[3], LineRow1U[0],
                    LineCol1U[0], LineRow2U[0], LineCol2U[0], out Pt2Row, out Pt2Col, out IsParallel); //交点4
                HOperatorSet.DistancePp(Pt1Row, Pt1Col, Pt2Row, Pt2Col, out NowLength);
                if (Math.Abs((TeachLength - NowLength).D) < 50) {
                    return true;
                }
                else{
                    return false;
                }
            }
            catch (Exception e0){
                Logger.Pop(e0.Source + e0.Message);
                return false;
            }

        }

        /// <summary>
        /// 计算直线1的夹角
        /// </summary>
        /// <param name="Row1"></param>
        /// <param name="Col1"></param>
        /// <param name="Row2"></param>
        /// <param name="Col2"></param>
        /// <param name="CurHandEyeMat"></param>
        /// <param name="Th"></param>
        /// <returns></returns>
        public static bool CalculateLineTh(double Row1, double Col1, double Row2, double Col2, MyHomMat2D CurHandEyeMat, out double Th)
        {
            Th = 0;
            try {
                //调整原点位置
                Row1 = 2448 - Row1;
                Row2 = 2448 - Row2;
                Point2Db Pt1 = new Point2Db(true), Pt2 = new Point2Db(true);
                Pt1.Row = Row1;
                Pt1.Col = Col1;
                Pt2.Row = Row2;
                Pt2.Col = Col2;
                Point2Db WorldPt1 = new Point2Db(true), WorldPt2 = new Point2Db(true);
                MyVisionBase.AffineTransPoint2D(Pt1, CurHandEyeMat, out WorldPt1);
                MyVisionBase.AffineTransPoint2D(Pt2, CurHandEyeMat, out WorldPt2);
                HTuple AngleX = 0;
                if (WorldPt1.Col < WorldPt2.Col){
                    HOperatorSet.AngleLl(0, 0, 1, 0, WorldPt1.Col, WorldPt1.Row, WorldPt2.Col, WorldPt2.Row, out AngleX);
                    Th = AngleX.D;
                }
                else{
                    HOperatorSet.AngleLl(0, 0, 1, 0, WorldPt2.Col, WorldPt2.Row, WorldPt1.Col, WorldPt1.Row, out AngleX);
                    Th = AngleX.D;
                }
                return true;
            }
            catch (Exception e0) {
                Logger.PopError(e0.Message + e0.Source + e0.StackTrace);
                return false;
            }
        }



        public static bool CalculateLineAng(HTuple LineRow1, HTuple LineCol1, HTuple LineRow2, HTuple LineCol2, out HTuple OutAngle, out HObject LineArrow)
        {
            OutAngle = new HTuple();
            LineArrow = new HObject();
            HTuple NewLineRow1 = new HTuple(), NewLineRow2 = new HTuple(), NewLineCol1 = new HTuple(), NewLineCol2 = new HTuple();
            bool IsOk = false;
            HTuple Length = 10;
            try {
                #region
                if (LineRow1.Length == 4 && LineRow2.Length == 4 && LineCol1.Length == 4 && LineCol2.Length == 4){
                    NewLineRow1 = (LineRow1[0].D + LineRow1[1].D + LineRow2[0].D + LineRow2[1].D) / 4;
                    NewLineCol1 = (LineCol1[0].D + LineCol1[1].D + LineCol2[0].D + LineCol2[1].D) / 4;
                    NewLineRow2 = (LineRow1[2].D + LineRow1[3].D + LineRow2[2].D + LineRow2[3].D) / 4;
                    NewLineCol2 = (LineCol1[2].D + LineCol1[3].D + LineCol2[2].D + LineCol2[3].D) / 4;
                    HOperatorSet.DistancePp(NewLineCol1, NewLineRow1, NewLineCol2, NewLineRow2, out Length);
                    MyVisionBase.GenArrowContourXld(out LineArrow, NewLineRow1, NewLineCol1, NewLineRow2, NewLineCol2, Length / 20, Length / 20);
                    NewLineRow1 = 2048 - NewLineRow1[0].D;
                    NewLineRow2 = 2048 - NewLineRow2[0].D;
                    HOperatorSet.AngleLl(0, 0, 1, 0, NewLineCol1, NewLineRow1, NewLineCol2, NewLineRow2, out OutAngle);
                    IsOk = true;
                }
                else if (LineRow1.Length == 2 && LineRow2.Length == 2 && LineCol1.Length == 2 && LineCol2.Length == 2) {
                    NewLineRow1 = (LineRow1[0].D + LineRow2[0].D) / 2;
                    NewLineCol1 = (LineCol1[0].D + LineCol2[0].D) / 2;
                    NewLineRow2 = (LineRow1[1].D + LineRow2[1].D) / 2;
                    NewLineCol2 = (LineCol1[1].D + LineCol2[1].D) / 2;
                    HOperatorSet.DistancePp(NewLineCol1, NewLineRow1, NewLineCol2, NewLineRow2, out Length);
                    MyVisionBase.GenArrowContourXld(out LineArrow, NewLineRow1, NewLineCol1, NewLineRow2, NewLineCol2, Length / 20, Length / 20);
                    NewLineRow1 = 2048 - NewLineRow1[0].D;
                    NewLineRow2 = 2048 - NewLineRow2[0].D;
                    HOperatorSet.AngleLl(0, 0, 1, 0, NewLineCol1, NewLineRow1, NewLineCol2, NewLineRow2, out OutAngle);
                    IsOk = true;
                }
                else if (LineRow1.Length == 1 && LineRow2.Length == 1 && LineCol1.Length == 1 && LineCol2.Length == 1) {
                    NewLineRow1 = LineRow1[0].D;
                    NewLineCol1 = LineCol1[0].D;
                    NewLineRow2 = LineRow2[0].D;
                    NewLineCol2 = LineCol2[0].D;
                    HOperatorSet.DistancePp(NewLineCol1, NewLineRow1, NewLineCol2, NewLineRow2, out Length);
                    MyVisionBase.GenArrowContourXld(out LineArrow, NewLineRow1, NewLineCol1, NewLineRow2, NewLineCol2, Length / 20, Length / 20);
                    NewLineRow1 = 2048 - NewLineRow1[0].D;
                    NewLineRow2 = 2048 - NewLineRow2[0].D;
                    HOperatorSet.AngleLl(0, 0, 1, 0, NewLineCol1, NewLineRow1, NewLineCol2, NewLineRow2, out OutAngle);
                    IsOk = true;
                }
                #endregion
            }
            catch (Exception e0){
                Logger.PopError1(e0);
            }
            return IsOk;
        }


    }
}
