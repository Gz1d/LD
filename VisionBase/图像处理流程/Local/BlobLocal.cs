using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Drawing;

namespace VisionBase
{
   public  class BlobLocal :LocalBase
    {
        public List<double> ListRow = new List<double>();
        public List<double> ListCol = new List<double>();
        public List<double> ListWid = new List<double>();
        public List<double> ListHei = new List<double>();
        public List<double> ListArea = new List<double>();
        public override bool  doLocal()
        {
            try
            {
                BlobLocalRlt NowResult0 = new BlobLocalRlt();
                St_BlobLocalParam Blobs = NowVisionPara.Blobs;
                HObject RoiCont = new HObject();
                HObject MinRectCont = new HObject();
                HObject RegionCont = new HObject();
                HOperatorSet.GenEmptyObj(out RoiCont);
                HOperatorSet.GenEmptyObj(out MinRectCont);
                HOperatorSet.GenEmptyObj(out RegionCont);
                BlobLocal1(NowImg, Blobs, out ListRow, out ListCol, out ListWid, out ListHei, out ListArea, out RoiCont, out MinRectCont, out RegionCont);
                HOperatorSet.ConcatObj(RoiCont, MinRectCont, out RoiCont);
                HOperatorSet.ConcatObj(RoiCont, RegionCont, out RoiCont);
                HOperatorSet.CopyObj(RoiCont, out NowResult0.ShowContour, 1, -1);
                if (RoiCont != null) RoiCont.Dispose();
                if (RegionCont != null) RegionCont.Dispose();
                if (MinRectCont != null) MinRectCont.Dispose();
                NowResult0.row = ListRow[0];
                NowResult0.col = ListCol[0];
                NowResult0.angle = 0;
                NowResult0.ListRow = ListRow;
                NowResult0.ListCol = ListCol;
                NowResult0.ListWid = ListWid;
                NowResult0.ListHei = ListHei;
                NowResult0.ListArea = ListArea;
                NowResult = NowResult0;
                return true;
            }
            catch { return false;  }
        }
        public static bool BlobLocal1(HObject ImgIn, St_BlobLocalParam BlobParaIn, out List<double> Rows, out List<double> Cols,
                             out List<double> RectWs, out List<double> RectHs, out List<double> Areas, out HObject RoiContour,
                             out HObject MinRectContour, out HObject RegionContour)
        {
            Rows = new List<double>();
            Cols = new List<double>();
            RectWs = new List<double>();
            RectHs = new List<double>();
            Areas = new List<double>();
            RoiContour = new HObject();
            MinRectContour = new HObject();
            RegionContour = new HObject();
            HObject Roi = new HObject();
            RectangleF NewRectF = new RectangleF(0, 0, 100, 100);
            HObject ReduceImg = new HObject();
            HObject ThresholdRegion = new HObject();
            HObject ConnectRegion = new HObject();
            HObject SelectRegion = new HObject();
            HTuple Area = new HTuple(), Row = new HTuple(), Col = new HTuple();
            HTuple RectRow1 = new HTuple(), RectCol1 = new HTuple(), RectRow2 = new HTuple(), RectCol2 = new HTuple();
            HObject RectContourI = new HObject();
            HObject RegionContourI = new HObject();
            HOperatorSet.GenEmptyObj(out RoiContour);
            HOperatorSet.GenEmptyObj(out RegionContour);
            HOperatorSet.GenEmptyObj(out MinRectContour);
            for (int i = 0; i < BlobParaIn.Count; i++) {
                try{
                    NewRectF = BlobParaIn.InspectRois[i];
                    HOperatorSet.GenRectangle1(out Roi, NewRectF.Y, NewRectF.X, NewRectF.Y + NewRectF.Height, NewRectF.X + NewRectF.Width);//生成检测区域
                    HOperatorSet.GenContourRegionXld(Roi, out RectContourI, "border");
                    HOperatorSet.ConcatObj(RectContourI, RoiContour, out RoiContour);
                    RectContourI.Dispose();
                    HOperatorSet.ReduceDomain(ImgIn, Roi, out ReduceImg);//裁剪图像
                    Roi.Dispose();
                    HOperatorSet.Threshold(ReduceImg, out ThresholdRegion, BlobParaIn.MinGrays[i], BlobParaIn.MaxGrays[i]);//阈值分割
                    ReduceImg.Dispose();
                    HOperatorSet.Connection(ThresholdRegion, out ConnectRegion); //区域连接
                    ThresholdRegion.Dispose();
                    HOperatorSet.SelectShape(ConnectRegion, out SelectRegion, "area", "and", BlobParaIn.AreaMins[i], BlobParaIn.AreaMaxs[i]);
                    ConnectRegion.Dispose();
                    HOperatorSet.AreaCenter(SelectRegion, out Area, out Row, out Col);
                    HOperatorSet.GenContourRegionXld(SelectRegion, out RegionContourI, "border");
                    HOperatorSet.ConcatObj(RegionContour, RegionContourI, out RegionContour);
                    RegionContourI.Dispose();
                }
                catch
                { }
                // HOperatorSet.SmallestRectangle2(SelectRegion,out Row ,out Col,out P)
                HOperatorSet.SmallestRectangle1(SelectRegion, out RectRow1, out RectCol1, out RectRow2, out RectCol2);
                HOperatorSet.GenContourRegionXld(SelectRegion, out RectContourI, "border");
                HOperatorSet.ConcatObj(MinRectContour, RectContourI, out MinRectContour);
                if (Row.Length > 0){
                    Rows.Add(Row.D);
                    Cols.Add(Col.D);
                }
                else{
                    Rows.Add(0);
                    Cols.Add(0);
                }
                if (RectCol2.Length > 0){
                    RectWs.Add(RectCol2.D - RectCol1.D);
                    RectHs.Add(RectRow2.D - RectRow1.D);
                    Areas.Add(Area.D);
                }
                else {
                    RectWs.Add(0);
                    RectHs.Add(0);
                }
                RectContourI.Dispose();
            }
            return true;

        }



    }
}
