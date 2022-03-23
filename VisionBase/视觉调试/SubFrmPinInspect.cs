using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using TapeVision.CameraSDK;
using HalconDotNet;
using System.Threading;
//using TapeVisionBase.外部接口;
using System.Diagnostics;
using VisionBase.Matching;


namespace VisionBase
{
    public partial class SubFrmPinInspect : Form
    {
        public int CCDIndex = 0;
        public HWindow ShowWindow;
        public string CameraName = "";
        public St_InspectPinParam inspectPinParam { get { return TeachInspectPinParam; } }
        public St_VectorAngle TeachVectorAngle = new St_VectorAngle();
        public St_VectorAngle TempFindVectorAngle = new St_VectorAngle();
        private St_InspectPinParam TeachInspectPinParam = new St_InspectPinParam(2,true);
        public bool ShowRoiFlag = false;
        private HObject GrabedImg = new HObject();

        private int Element = 64;
        private int DetectHeight = 20;
        private int DetectWidth = 20;
        private int DnThreshold = 50;
        private int UpThreshold = 255;
        private int MinArea = 10;
        private int MaxArea = 10000;
        private List<double> iPinRows = new List<double>();
        private List<double> iPinCols = new List<double>();
        private HTuple LineRow1 = new HTuple();
        private HTuple LineRow2 = new HTuple();
        private HTuple LineCol1 = new HTuple();
        private HTuple LineCol2 = new HTuple();

        private HTuple LineRow10 = new HTuple();
        private HTuple LineRow20 = new HTuple();
        private HTuple LineCol10 = new HTuple();
        private HTuple LineCol20 = new HTuple();

        private HTuple OutLineRow1 = new HTuple();
        private HTuple OutLineRow2 = new HTuple();
        private HTuple OutLineCol1 = new HTuple();
        private HTuple OutLineCol2 = new HTuple();

        private int LineNum = 0;


        public MyHomMat2D CurHandEyeMat = new MyHomMat2D(true);
        private Action<bool> ActionIsDrawingRoi;
        private VisionPara VisionPara0;

        public SubFrmPinInspect(St_InspectPinParam inspectPinParamIn, HWindow InHWindow, HObject ImgIn, Action<bool> drawRoiInHWindow)
        {
            InitializeComponent();
            TeachInspectPinParam = inspectPinParamIn;
            ShowWindow = InHWindow;
            ActionIsDrawingRoi = drawRoiInHWindow;
            if (ImgIn != null && ImgIn.IsInitialized())
            {
                HOperatorSet.CopyImage(ImgIn, out GrabedImg);
            }
        }

        private void FrmSubFrmFindLine_Load(object sender, EventArgs e)
        {
            InitCombox();
        }

        private void InitCombox()
        {
            int count = TeachInspectPinParam.Count;
            LineSelectComBox.Items.Clear();
            if (false)
            {

            }
            else
            {
                if (count == 3)
                {
                    LineSelectComBox.Items.Add("第一行PIN针");
                    LineSelectComBox.Items.Add("第二行PIN针");
                    LineSelectComBox.Items.Add("第三行PIN针");
                }
                else if (count == 4)
                {
                    LineSelectComBox.Items.Add("第一行PIN针");
                    LineSelectComBox.Items.Add("第二行PIN针");
                    LineSelectComBox.Items.Add("第三行PIN针");
                    LineSelectComBox.Items.Add("第四行PIN针");
                }
                else if (count == 2)
                {
                    LineSelectComBox.Items.Add("第一行PIN针");
                    LineSelectComBox.Items.Add("第二行PIN针");
                }
                else if (count == 1)
                {
                    LineSelectComBox.Items.Add("第一行PIN针");
                }
                else return;
            }
            LineSelectComBox.SelectedIndex = 0;
        }

        public void UpdateCurrImage(HObject img, St_VectorAngle TempFindVectorAngleIn, VisionPara VisionParaIn)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
            HOperatorSet.CopyImage(img, out GrabedImg);
            TempFindVectorAngle = TempFindVectorAngleIn;
            VisionPara0 = VisionParaIn;
        }

        /// <summary>
        /// 拟合参数的示教函数
        /// </summary>
        public void TeachProcess()
        {
            if (!GrabedImg.IsInitialized()){
                MessageBox.Show("请先加载图片");
                return;
            }
            MyVisionBase.hDispObj(ShowWindow, GrabedImg);
            HObject RoiContour = new HObject();
            ActionIsDrawingRoi(true);
            MyVisionBase.draw_rake(GrabedImg, out RoiContour, ShowWindow, Element, DetectHeight, 2,
                out LineRow1,out  LineCol1, out  LineRow2, out  LineCol2);
            ActionIsDrawingRoi(false);
            Thread TeachThd = new Thread(DrawLinesProcess);
            TeachThd.IsBackground = true;
            TeachThd.Start();           
        }

        private void DrawLinesProcess()
        {
            ShowRoiFlag = true;
            HObject RoiContour = new HObject();
            HObject DetectRegions = new HObject();
            HTuple ResultRows = new HTuple(), ResultCols = new HTuple();
            #region  //直线参数示教
            //2.1画出直线的检测区域
            while (ShowRoiFlag)
            {
                try
                {
                    //2.2显示直线的检测ROI,调整ROI的参数
                    MyVisionBase.hDispObj(ShowWindow, GrabedImg);
                    Thread.Sleep(50);
                    //MyVisionBase.gen_rake_ROI1(GrabedImg, )
                    MyVisionBase.gen_rake_ROI1(GrabedImg, out DetectRegions, out RoiContour, Element, DetectHeight,
                        DetectWidth, LineRow1,LineCol1, LineRow2, LineCol2);
                    MyVisionBase.hSetColor(ShowWindow, "blue");
                    //MyVisionBase.hDispObj(ShowWindow, DetectRegions);
                    MyVisionBase.hSetColor(ShowWindow, "white");
                    Thread.Sleep(300);
                    MyVisionBase.hSetColor(ShowWindow, "blue");
                    MyVisionBase.hDispObj(ShowWindow, RoiContour);
                    MyVisionBase.hSetColor(ShowWindow, "white");
                    Thread.Sleep(300);
                    //2.5显示测量出每个示教小ROI的平均灰度，并保存到ListGrays变量中
                    //HTuple MeanGrays = new HTuple(), GrayDivs = new HTuple();
                   // HOperatorSet.Intensity(DetectRegions, GrabedImg, out MeanGrays, out GrayDivs);
                    HObject ReduceImg ;
                    HTuple area =new HTuple() ,row =new HTuple(),col =new HTuple();
                    for (int i = 0; i < DetectRegions.CountObj(); i++)
                    {
                        int cc = DetectRegions.CountObj();
                        HObject RegionI = new HObject();
                        HOperatorSet.CopyObj(DetectRegions, out RegionI, i+1, 1);
                        //HOperatorSet.CopyObj(DetectRegions)
                       // RegionI = ;
                        HOperatorSet.ReduceDomain(GrabedImg, RegionI, out ReduceImg);
                        HObject ThrdRegion;
                        HOperatorSet.Threshold(ReduceImg, out ThrdRegion, DnThreshold, UpThreshold);
                        HObject CircleObj =new HObject();
                        HOperatorSet.GenCircle(out CircleObj, 10, 10, 5);
                        HObject ExpandRegion =new HObject();

                        HOperatorSet.ExpandRegion(ThrdRegion, CircleObj, out ExpandRegion, 3, "image");
                        HObject ConnectRegion ;
                        HOperatorSet.Connection(ExpandRegion, out  ConnectRegion);
                        HObject SelectRegion = new HObject();
                        HOperatorSet.SelectShape(ConnectRegion, out SelectRegion, "area", "and", MinArea, MaxArea);
                        HTuple areaI, RowI, ColI;
                        HOperatorSet.AreaCenter(SelectRegion, out areaI, out  RowI, out ColI);
                        if (RowI.Length > 0){
                            row[i] = RowI[0].D;
                            col[i] = ColI[0].D;
                        }
                        else{
                           row[i] =0;
                           col[i] =0;
                        }
                        //row[i] = RowI[0].D;
                        //col[i] = ColI[0].D;
                    }
                    HObject hoLine = new HObject();
                    HTuple FitLineRow1, FitLineCol1, FitLineRow2, FitLineCol2, dist;
                    MyVisionBase.PtsToBestLine(out hoLine,row,col,3,out FitLineRow1,out FitLineCol1,out FitLineRow2, out FitLineCol2 ,out dist ); //针脚中心点拟合直线
                    HTuple LinePtRows,LinePtCols;
                    MyVisionBase.GetLinePts(FitLineRow1, FitLineCol1, FitLineRow2, FitLineCol2, Element, out LinePtRows, out LinePtCols); //生成 理论的阵脚点

                    MyVisionBase.HTupleToList(LinePtRows, out iPinRows);
                    MyVisionBase.HTupleToList(LinePtCols, out iPinCols);
                   HObject ShowContour = new HObject();
                    HOperatorSet.GenCrossContourXld(out ShowContour, row, col, 50, 0);
                    MyVisionBase.hSetColor(ShowWindow, "blue");
                    MyVisionBase.hDispObj(ShowWindow, ShowContour);
                    Thread.Sleep(300);
                    HOperatorSet.GenCrossContourXld(out ShowContour, LinePtRows, LinePtCols, 50, 0);
                    MyVisionBase.hSetColor(ShowWindow, "red");
                    MyVisionBase.hDispObj(ShowWindow, ShowContour);
                    Thread.Sleep(300);
                }
                catch (Exception e0)
                {
                    MessageBox.Show(e0.Message + e0.Source);
                    this.Invoke(new Action(() => {
                        StartTeachBtn1.Enabled = true;
                    
                    }));
                    
                    break;
                }
            }
            #endregion
        }



        private void FindLineBtn_Click(object sender, EventArgs e)
        {
            txtFindTime.Clear();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (GrabedImg == null)
            {
                Logger.PopError("请先采集图片！", true);
                return;
            }

            //1.0模板匹配
            St_TemplateParam TemplateParam = VisionPara0.localPara.Template;
            RectangleF roi = new RectangleF();
            LocalSettingPara Setting = VisionPara0.localPara.localSetting;
            roi.X = Setting.SearchAreaX;
            roi.Y = Setting.SearchAreaY;
            roi.Width = Setting.SearchWidth;
            roi.Height = Setting.SearchHeight;
            MatchingResult result;
            TemplateParam.FindSharpTemplate(GrabedImg, roi, TemplateParam, out result);
            //2.0调整图像位置
            St_VectorAngle VectorAngle0 = new St_VectorAngle(VisionPara0.localPara.Template.CenterY, 
                VisionPara0.localPara.Template.CenterX, VisionPara0.localPara.Template.TemplateAngle);
            St_VectorAngle TempFindVectorAngle = new St_VectorAngle(result.mRow, result.mCol, result.mAngle);//找到的模板坐标
            HTuple HomMat =new HTuple();
            HOperatorSet.VectorAngleToRigid(result.mRow, result.mCol, result.mAngle, VisionPara0.localPara.Template.CenterY,
                VisionPara0.localPara.Template.CenterX, VisionPara0.localPara.Template.TemplateAngle, out HomMat);
            HObject AffineImg =new HObject();
            HOperatorSet.AffineTransImage(GrabedImg, out AffineImg, HomMat, "constant", "false");

            HOperatorSet.ClearWindow(ShowWindow);
            MyVisionBase.hDispObj(ShowWindow, GrabedImg);

            //3.0找出Pin针坐标
            HObject RoiContour = new HObject();
            MyVisionBase.hDispObj(ShowWindow, AffineImg);
            HTuple PinRows =new HTuple(),PinCols =new HTuple();
            PinInsepct.FindPinPos(TeachInspectPinParam, AffineImg, out PinRows, out PinCols);
            HObject ShowContour = new HObject();
            HOperatorSet.GenCrossContourXld(out ShowContour, PinRows, PinCols, 50, 0);
            MyVisionBase.hSetColor(ShowWindow, "red");
            MyVisionBase.hDispObj(ShowWindow, ShowContour);
            //4.0 计算出Pin针偏移量
            HTuple OffSetRows=new HTuple(), OffSetCols =new HTuple(),OffSetDists =new HTuple();;
            PinInsepct.CalculatePinOffset(TeachInspectPinParam, PinRows, PinCols, out  OffSetRows, out OffSetCols, out OffSetDists);
            HTuple Max = OffSetDists.TupleMax();
            HTuple MaxIndex = OffSetDists.TupleFind(Max);
            HOperatorSet.GenCircleContourXld(out ShowContour, PinRows[MaxIndex[0].I], PinCols[MaxIndex[0].I], 50, 0,
                Math.PI * 2, "positive", 1.0);
            MyVisionBase.hSetColor(ShowWindow, "red");
            MyVisionBase.hDispObj(ShowWindow, ShowContour);
            txtFindTime.Text = sw.ElapsedMilliseconds.ToString();
        }

        private void ElementsBar_Scroll(object sender, EventArgs e)
        {
            ElementsTxt.Text = ElementsBar.Value.ToString();
            Element = ElementsBar.Value;
        }

        private void DetectHeightBar_Scroll(object sender, EventArgs e)
        {
            DetectHeightTxt.Text = DetectHeightBar.Value.ToString();
            DetectHeight = DetectHeightBar.Value;
        }

        private void DnThresholdBar_Scroll(object sender, EventArgs e)
        {
            DnThresholdTxt.Text = DnThresholdBar.Value.ToString();
            DnThreshold = DnThresholdBar.Value;
        }
        private void UpThresholdBar_Scroll(object sender, EventArgs e)
        {
            UpThresholdTxt.Text = UpThresholdBar.Value.ToString();
            UpThreshold = UpThresholdBar.Value;
        }
        private void StartTeachBtn1_Click(object sender, EventArgs e)
        {
            StartTeachBtn1.Enabled = false;
            MyVisionBase.hDispObj(ShowWindow, GrabedImg);
            if (!GrabedImg.IsInitialized()){
                MessageBox.Show("图片为空，请先加载一张图片。");
                return;
            }
            HObject RoiContour = new HObject();
            TeachProcess();
        }

        private void SaveParaBtn_Click(object sender, EventArgs e)
        {
            if (LineCol1 == null || LineCol1.Length == 0)
            {
                if (DialogResult.Yes != MessageBox.Show("未设置找线ROI，是否继续保存参数？", "参数保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;
            }
            if (TeachInspectPinParam.Col1s.Count - 1 < LineNum)
            {
                if (LineCol1 != null && LineCol2 != null && LineCol1.Length > 0)
                {
                    TeachInspectPinParam.Col1s.Add(LineCol1);
                    TeachInspectPinParam.Col2s.Add(LineCol2);
                    TeachInspectPinParam.Row1s.Add(LineRow1);
                    TeachInspectPinParam.Row2s.Add(LineRow2);
                    TeachInspectPinParam.Elements.Add(Element);
                    TeachInspectPinParam.DetectHeights.Add(DetectHeight);
                    TeachInspectPinParam.DetectWidths.Add(DetectWidth);
                    TeachInspectPinParam.DnThresholds.Add(DnThreshold);
                    TeachInspectPinParam.UpThresholds.Add(UpThreshold);
                    TeachInspectPinParam.MinAreas.Add(MinArea);
                    TeachInspectPinParam.MaxAreas.Add(MaxArea);
                    TeachInspectPinParam.ListRows.Add(iPinRows);
                    TeachInspectPinParam.ListCols.Add(iPinCols);
                }

            }
            else if (TeachInspectPinParam.Col1s.Count - 1 >= LineNum)
            {
                if (LineCol1 != null && LineCol2 != null && LineCol1.Length > 0)
                {
                    TeachInspectPinParam.Col1s[LineNum] = LineCol1;
                    TeachInspectPinParam.Col2s[LineNum] = LineCol2;
                    TeachInspectPinParam.Row1s[LineNum] = LineRow1;
                    TeachInspectPinParam.Row2s[LineNum] = LineRow2;
                    TeachInspectPinParam.Elements[LineNum] = Element;
                    TeachInspectPinParam.DnThresholds[LineNum] = DnThreshold;
                    TeachInspectPinParam.UpThresholds[LineNum] = UpThreshold;
                    TeachInspectPinParam.DetectHeights[LineNum] = DetectHeight;
                    TeachInspectPinParam.DetectWidths[LineNum] = DetectWidth;
                    TeachInspectPinParam.MinAreas[LineNum] = MinArea;
                    TeachInspectPinParam.MaxAreas[LineNum] = MaxArea;
                    TeachInspectPinParam.ListRows[LineNum] = iPinRows;
                    TeachInspectPinParam.ListCols[LineNum] = iPinCols;
                    TeachInspectPinParam.Count = TeachInspectPinParam.Col1s.Count();

                }

            }
        }

        private void StopTeachBtn_Click(object sender, EventArgs e)
        {
            StartTeachBtn1.Enabled = true;
            ShowRoiFlag = false;
        }



        private void LineSelectComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LineNum = LineSelectComBox.SelectedIndex;
            if (TeachInspectPinParam.Elements.Count > LineNum)
            {
                ElementsBar.Value = (int)TeachInspectPinParam.Elements[LineNum];
                Element = ElementsBar.Value;
                ElementsTxt.Text = Element.ToString();
            }
            if (TeachInspectPinParam.DetectHeights.Count > LineNum)
            {
                DetectHeightBar.Value = (int)TeachInspectPinParam.DetectHeights[LineNum];
                DetectHeight = DetectHeightBar.Value;
                DetectHeightTxt.Text = DetectHeight.ToString();
            }
            if (TeachInspectPinParam.DetectWidths.Count > LineNum)
            {
                DetectWidBar.Value = (int)TeachInspectPinParam.DetectWidths[LineNum];
                DetectWidth = DetectWidBar.Value;
                DetectWidTxt.Text = DetectWidth.ToString();         
            }
            if (TeachInspectPinParam.DnThresholds.Count > LineNum)
            {
                DnThresholdBar.Value = (int)TeachInspectPinParam.DnThresholds[LineNum];
                DnThreshold = DnThresholdBar.Value;
                DnThresholdTxt.Text = DnThreshold.ToString();
            }
            if (TeachInspectPinParam.UpThresholds.Count > LineNum)
            {
                UpThresholdBar.Value = (int)TeachInspectPinParam.UpThresholds[LineNum];
                UpThreshold = UpThresholdBar.Value;
                UpThresholdTxt.Text = UpThreshold.ToString();         
            }
            if (TeachInspectPinParam.MinAreas.Count > LineNum)
            {
                MinAreaBar.Value = (int)TeachInspectPinParam.MinAreas[LineNum];
                MinArea = MinAreaBar.Value;
                MinAreaTxt.Text = MinArea.ToString();      
            }
            if (TeachInspectPinParam.MaxAreas.Count > LineNum)
            {
                MaxAreaBar.Value = (int)TeachInspectPinParam.MaxAreas[LineNum];
                MaxArea = MaxAreaBar.Value;
                MaxAreaTxt.Text = MaxArea.ToString();
            
            }
            if (TeachInspectPinParam.Col1s.Count > LineNum)
            {
                LineCol1 = TeachInspectPinParam.Col1s[LineNum];
                LineCol2 = TeachInspectPinParam.Col2s[LineNum];
                LineRow1 = TeachInspectPinParam.Row1s[LineNum];
                LineRow2 = TeachInspectPinParam.Row2s[LineNum];
                iPinRows =TeachInspectPinParam.ListRows[LineNum];
                iPinCols = TeachInspectPinParam.ListCols[LineNum];
            }
        }

        private void TryDebugBtn_Click(object sender, EventArgs e)
        {
            txtFindTime.Clear();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //HObject LineContourU = new HObject();
            //HObject across = new HObject();
            //HObject lines = new HObject();
            //MyVisionBase.hDispObj(ShowWindow, GrabedImg);
            //St_LinesParam tempLineParam = new St_LinesParam(TeachLinesPara.Count);
            //tempLineParam = TeachLinesPara;
            ////1.0对于新的图片根据模板匹配的VectorAngle和示教模板的VectorAngle调整直线的示教起始点
            //St_VectorAngle VectorAngle0 = new St_VectorAngle(VisionPara0.Template.CenterY, VisionPara0.Template.CenterX, VisionPara0.Template.TemplateAngle);
            //LineTypePos.AdjLineTeachPara(VectorAngle0.Row, VectorAngle0.Col, VectorAngle0.Angle,
            //  TempFindVectorAngle.Row, TempFindVectorAngle.Col, TempFindVectorAngle.Angle, TeachLinesPara, out tempLineParam);
            ////2.0找出要定位的直线
            //LineTypePos.FindLine(tempLineParam, GrabedImg, out OutLineRow1, out OutLineCol1, out OutLineRow2, out OutLineCol2, out across, out LineContourU);
            ////3.0显示
            //HOperatorSet.SetColor(ShowWindow, "blue");
            //MyVisionBase.hDispObj(ShowWindow, LineContourU);
            //HOperatorSet.SetColor(ShowWindow, "red");
            //MyVisionBase.hDispObj(ShowWindow, across);
            //HOperatorSet.SetColor(ShowWindow, "green");
            //MyVisionBase.hDispObj(ShowWindow, lines);

            //if (LineContourU != null) LineContourU.Dispose();
            //if (lines != null) lines.Dispose();
            //if (across != null) across.Dispose();
            txtFindTime.Text = sw.ElapsedMilliseconds.ToString();
        }

        private void ClearParaBtn_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("真的要清除参数？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt != DialogResult.Yes) return;
            int initLineCount = 1;
            if (true)
            {
                rlt = MessageBox.Show("示教3条线？", "线数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rlt == DialogResult.Yes)
                {
                    initLineCount = 3;
                }
                if (initLineCount != 3)
                {
                    rlt = MessageBox.Show("示教2条线？", "线数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rlt == DialogResult.Yes)
                    {
                        initLineCount = 2;
                    }
                    else
                    {
                        rlt = MessageBox.Show("示教1条线？", "线数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rlt == DialogResult.Yes)
                        {
                            initLineCount = 1;
                        }
                        else
                        {
                            MessageBox.Show("即将示教4条线。", "线数量确认");
                            initLineCount = 4;
                        }
                    }
                }
            }
            TeachInspectPinParam = new St_InspectPinParam(initLineCount, true);
            InitCombox();
        }

        private void SubFrmFindLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
        }



        private void GenVirtualPosBtn_Click(object sender, EventArgs e)
        {
            //if (FindCircleRow.Length == 0 || FindCircleCol.Length == 0)
            //{
            //    MessageBox.Show("请先完成圆定位");
                
            //    return; }
            //MyVisionBase.hClearWindow(ShowWindow);
            //MyVisionBase.hDispObj(ShowWindow, GrabedImg);
            ////1.0找线
            //TryDebugBtn_Click(null, new EventArgs());
            //HTuple LineAngle = new HTuple();
            //HObject LineArrow = new HObject();
            //HObject LitCirCross = new HObject();
            ////2.0计算直线与相机X轴的夹角
            //LineTypePos.CalculateLineAng(OutLineRow1, OutLineCol1, OutLineRow2, OutLineCol2, out LineAngle, out LineArrow);
            //LineAngle = LineAngle.D / Math.PI * 180.0;
            //HTuple LitCirRow = new HTuple(), LitCircol = new HTuple();
            //HTuple DistRr = (7.78 / 2.0) / 2.8 * FindCircleR;
            ////3.0生成虚拟圆的圆心
            //CircleTypePos.GenCirCenter(FindCircleRow, FindCircleCol, DistRr, out LitCirRow, out LitCircol);
            //HOperatorSet.GenCrossContourXld(out LitCirCross, LitCirRow, LitCircol, 60, 0);
            //MyVisionBase.hSetColor(ShowWindow, "blue");
            //MyVisionBase.hDispObj(ShowWindow, LineArrow);
            //MyVisionBase.hDispObj(ShowWindow, LitCirCross);
            //HHomMat2D MyHomMat = new HHomMat2D();
            //MyHomMat = MyHomMat.HomMat2dRotate((TeachLinesPara.OffSetPixelTh + LineAngle) / 180.0 * Math.PI, FindCircleRow, FindCircleCol);
            ////4.0旋转生成圆的圆心
            //MyHomMat.AffineTransPixel(LitCirRow, LitCircol, out LitCirRow, out LitCircol);
            //HOperatorSet.GenCrossContourXld(out LitCirCross, LitCirRow, LitCircol, 60, 0);
            //HObject FirstCirCont = new HObject(), SecondCirCont = new HObject();
            //HOperatorSet.GenCircleContourXld(out FirstCirCont, LitCirRow[0].D, LitCircol[0].D, 60, 0, 2 * Math.PI, "positive", 0.1);
            //HOperatorSet.GenCircleContourXld(out SecondCirCont, LitCirRow[1].D, LitCircol[1].D, 40, 0, 2 * Math.PI, "positive", 0.1);
            //HOperatorSet.ConcatObj(LitCirCross, FirstCirCont, out LitCirCross);
            //HOperatorSet.ConcatObj(LitCirCross, SecondCirCont, out LitCirCross);
            //MyVisionBase.hSetColor(ShowWindow, "red");
            //MyVisionBase.hDispObj(ShowWindow, LitCirCross);
        }

        private void DetectWidBar_Scroll(object sender, EventArgs e)
        {
            DetectWidth = DetectWidBar.Value;
            DetectWidTxt.Text = DetectWidBar.Value.ToString();
            
        }

        private void MinAreaBar_Scroll(object sender, EventArgs e)
        {
            MinAreaTxt.Text = MinAreaBar.Value.ToString();
            MinArea = MinAreaBar.Value;

        }

        private void MaxAreaBar_Scroll(object sender, EventArgs e)
        {
            MaxArea = MaxAreaBar.Value;
            MaxAreaTxt.Text = MaxArea.ToString();
        }





    }
}
