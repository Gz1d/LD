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


namespace VisionBase
{
    public partial class SubFrmFindLine : Form
    {
        public ViewControl view1;
        public string CameraName = "";
        public St_LinesParam LinesParam { get { return TeachLinesPara; } }
        public St_VectorAngle TeachVectorAngle = new St_VectorAngle();
        public St_VectorAngle TempFindVectorAngle = new St_VectorAngle();
        private St_LinesParam TeachLinesPara = new St_LinesParam();
        public bool ShowRoiFlag = false;
        private HObject GrabedImg = new HObject();

        private int Element = 64;
        private int DetectHeight = 50;
        private int Threshold = 20;
        private double CenterClip = 0;
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
        public double OffSetPixelX = 0;
        public double OffSetPixelY = 0;
        public double OffSetPixelTh = 0;

        public MyHomMat2D CurHandEyeMat = new MyHomMat2D(true);
        private Action<bool> ActionIsDrawingRoi;
        //private St_ImageVisionParam VisionPara0;
        private LocalPara LocalPara0;

        public SubFrmFindLine(St_LinesParam lineParam, ViewControl viewIn, HObject ImgIn,  Action<bool> drawRoiInHWindow)
        {
            InitializeComponent();
            TeachLinesPara = lineParam;
            view1 = viewIn;
            ActionIsDrawingRoi = drawRoiInHWindow;
            if (ImgIn != null && ImgIn.IsInitialized())
            {
                HOperatorSet.CopyImage(ImgIn, out GrabedImg);
            }
        }
        public void UpDatePara(LocalPara ParaIn)
        {
            LocalPara0 = ParaIn;
        }
        private void FrmSubFrmFindLine_Load(object sender, EventArgs e)
        {
            InitCombox();
        }

        private void InitCombox()
        {
            int count = TeachLinesPara.Count;
            LineSelectComBox.Items.Clear();
            for (int i =1; i< count+1;i++)
            {              
                LineSelectComBox.Items.Add("第" +i.ToString()+"条直线");
            }
            LineSelectComBox.SelectedIndex = 0;
        }

        public void UpdateCurrImage(HObject img, St_VectorAngle TempFindVectorAngleIn,LocalPara LocalParaIn)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
            HOperatorSet.CopyImage(img, out GrabedImg);
            TempFindVectorAngle = TempFindVectorAngleIn;
            LocalPara0 = LocalParaIn;
        }

        /// <summary>
        /// 拟合参数的示教函数
        /// </summary>
        public void TeachProcess()
        {
            ShowRoiFlag = true;
            HObject RoiContour = new HObject();
            //1.0获取图像，调节曝光时间
            HTuple ResultRows = new HTuple(), ResultCols = new HTuple();
            //2.0示教参数
            #region  //直线参数示教
            //2.1画出直线的检测区域
            bool CurIsOk = true;
            view1.AddViewImage(GrabedImg);
            while (ShowRoiFlag) {  
                //2.2显示直线的检测ROI,调整ROI的参数
                Thread.Sleep(50);
                view1.Refresh();
                view1.AddImage(GrabedImg);
                CurIsOk = MyVisionBase.gen_rake_ROI(GrabedImg, out RoiContour, Element, DetectHeight, 2, 
                    LineRow1, LineCol1, LineRow2, LineCol2);
                if (!CurIsOk) break;            
                view1.SetDraw("blue", "margin");
                view1.AddViewObject(RoiContour);
                //2.3利用卡尺工具找出边界点
                CurIsOk = MyVisionBase.Rake(GrabedImg, out RoiContour, Element, DetectHeight, 2, 2, Threshold,
                    "all", "first", LineRow1, LineCol1,
                    LineRow2, LineCol2, out ResultRows, out ResultCols);
                //减去中间不要的点
                int Count = ResultRows.Length;
                int ClipLength =(int ) (Count * CenterClip);
                int StartClipNo = (int)((Count - ClipLength) * 0.5);
                if ((Count - ClipLength) > 4 && ClipLength > 0) {
                    for (int i = 0; i < ClipLength; i++){
                        ResultRows= ResultRows.TupleRemove(StartClipNo);
                        ResultCols = ResultCols.TupleRemove(StartClipNo);
                    }
                }
                if (!CurIsOk) break;
                //2.4
                HObject LinePtCross = new HObject();
                //2.5显示拟合的点
                HOperatorSet.GenCrossContourXld(out LinePtCross, ResultRows, ResultCols, 12, 0.7);
                view1.SetDraw("green", "margin");
                view1.AddViewObject(LinePtCross);
                //2.6直线拟合
                HObject FitLineCotour = new HObject();
                HTuple FitLineRow1 = new HTuple(), FitLineCol1 = new HTuple(), FitLineRow2 = new HTuple(), 
                    FitLineCol2 = new HTuple(), PtToLineDist = new HTuple();
                CurIsOk = MyVisionBase.PtsToBestLine(out FitLineCotour, ResultRows, ResultCols, 3, 
                    out FitLineRow1, out FitLineCol1, out FitLineRow2, out FitLineCol2, out PtToLineDist);
                if (CurIsOk) {
                    HTuple AngleLx = new HTuple();
                    HOperatorSet.AngleLl(0, 0, 1, 0, FitLineCol1, FitLineRow1, FitLineCol2, FitLineRow2, out AngleLx);  //直线1与X轴之间的夹角
                    HTuple DistP1L = new HTuple(), DistP2L = new HTuple();
                    HOperatorSet.DistancePl(LineRow10, LineCol10, FitLineRow1, FitLineCol1, FitLineRow2, FitLineCol2, out DistP1L);
                    HOperatorSet.DistancePl(LineRow20, LineCol20, FitLineRow1, FitLineCol1, FitLineRow2, FitLineCol2, out DistP2L);
                    AngleLx = AngleLx.D / Math.PI * 180.0;
                    if (Math.Abs(AngleLx.D) % 90 < 10 && DistP1L.D < 10 && DistP2L.D < 10) {
                        HOperatorSet.ProjectionPl(LineRow1, LineCol1, FitLineRow1, FitLineCol1, FitLineRow2, FitLineCol2, 
                            out LineRow1, out LineCol1);
                        HOperatorSet.ProjectionPl(LineRow2, LineCol2, FitLineRow1, FitLineCol1, FitLineRow2, FitLineCol2,
                            out LineRow2, out LineCol2);
                    }
                }
                if (!CurIsOk) break;
                //2.7显示拟合直线
                view1.SetDraw("red", "margin");
                view1.AddViewObject(FitLineCotour);
                view1.Repaint();
                Thread.Sleep(1500);
            }
            #endregion
        }
        private void FindLineBtn_Click(object sender, EventArgs e)
        {
            txtFindTime.Clear();
            Stopwatch sw = new Stopwatch();
            
            HObject RoiContour = new HObject();
            view1.ResetView();
            view1.AddViewImage(GrabedImg);
            HTuple ResultRows = new HTuple(), ResultCols = new HTuple();
            if ((TeachLinesPara.Row1s[0] == 0 || TeachLinesPara.Col1s[0] == 0)){
                MessageBox.Show("示教未完成，请重新示教。");
                return;
            }
            int lineCount = TeachLinesPara.Col1s.Count;
            sw.Start();
            for (int i = 0; i < lineCount; i++){
                if (TeachLinesPara.Row1s[i] == 0 || TeachLinesPara.Row1s[i] == 0)
                {
                    MessageBox.Show("示教未完成，请重新示教。");
                    return;
                }
                //1.0显示直线的检测ROI
                MyVisionBase.gen_rake_ROI(GrabedImg, out RoiContour, TeachLinesPara.Elements[i], TeachLinesPara.DetectHeights[i], 2,
                             TeachLinesPara.Row1s[i], TeachLinesPara.Col1s[i], TeachLinesPara.Row2s[i], TeachLinesPara.Col2s[i]);
                view1.SetDraw("blue", "margin");
                view1.AddViewObject(RoiContour);
                //2.0利用卡尺工具找出边界点
                MyVisionBase.Rake(GrabedImg, out RoiContour, TeachLinesPara.Elements[i], TeachLinesPara.DetectHeights[i], 2, 2,
                                 TeachLinesPara.Thresholds[i], "all", "first",  TeachLinesPara.Row1s[i], TeachLinesPara.Col1s[i],
                                 TeachLinesPara.Row2s[i], TeachLinesPara.Col2s[i], out ResultRows, out ResultCols);
                MyVisionBase.ClipCenterElement(ResultRows, TeachLinesPara.CenterClips[i], out ResultRows);
                MyVisionBase.ClipCenterElement(ResultCols, TeachLinesPara.CenterClips[i], out ResultCols);
                HObject LinePtCross = new HObject();
                //3.显示拟合的点
                HOperatorSet.GenCrossContourXld(out LinePtCross, ResultRows, ResultCols, 12, 0.7);
                view1.SetDraw("green", "margin");
                view1.AddViewObject(LinePtCross);                
            }
            sw.Stop();
            this.view1.Repaint();
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

        private void ThresholdBar_Scroll(object sender, EventArgs e)
        {
            ThresholdTxt.Text = ThresholdBar.Value.ToString();
            Threshold = ThresholdBar.Value;
        }

        private void StartTeachBtn1_Click(object sender, EventArgs e)
        {
            bool IsTrue = true;
            if (!GrabedImg.IsInitialized()) {
                MessageBox.Show("图片为空，请先加载一张图片。");
                return;
            }
            if (StartTeachBtn1.Text == "开始示教"){
                this.StartTeachBtn1.Text = "保存";
                this.EnableGroupbox(groupBox3, false);
                this.StartTeachBtn1.Enabled = true;
                this.view1.Refresh();
                this.view1.AddImage(GrabedImg);
                HObject RoiContour = new HObject();
                ActionIsDrawingRoi(true);
                this.view1.roiController.reset();
                this.view1.roiController.setROIShape(new ViewROI.ROILine());
            }
            else if (StartTeachBtn1.Text == "保存"){
                this.EnableGroupbox(groupBox3, true);
                ViewROI.ROILine myline = new ViewROI.ROILine();
                if (view1.roiController.getActiveROI() is ViewROI.ROILine) {
                    StartTeachBtn1.Text = "开始示教";
                    groupBox3.Enabled = true;
                    myline =(ViewROI.ROILine) view1.roiController.getActiveROI();
                    double row1, col1, row2, col2;
                    myline.GetLine(out row1, out col1, out row2, out col2);
                    LineRow10 = row1;
                    LineCol10 = col1;
                    LineRow20 = row2;
                    LineCol20 = col2;
                    LineRow1 = row1;
                    LineCol1 = col1;
                    LineRow2 = row2;
                    LineCol2 = col2;
                    ActionIsDrawingRoi(false);
                    if (!IsTrue){
                        StartTeachBtn1.Enabled = true;
                        return;
                    }
                    Thread TeachThd = new Thread(TeachProcess);
                    TeachThd.IsBackground = true;
                    TeachThd.Start();
                    StartTeachBtn1.Enabled = false;
                }
                else {
                    MessageBox.Show("请先选中示教直线");                              
                }               
            }       
        }

        private void EnableGroupbox(GroupBox groupBoxIn,bool IsEnable  )
        {
            foreach (Control item in groupBoxIn.Controls)
            {
                item.Enabled = IsEnable;         
            }
        }


        private void SaveParaBtn_Click(object sender, EventArgs e)
        {
            if (LineCol1 == null || LineCol1.Length == 0)
            {
                if (DialogResult.Yes != MessageBox.Show("未设置找线ROI，是否继续保存参数？", "参数保存", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question))
                    return;
            }
            if (TeachLinesPara.Col1s.Count - 1 < LineNum)  {
                if (LineCol1 != null && LineCol2 != null && LineCol1.Length > 0) {
                    TeachLinesPara.Col1s.Add(LineCol1);
                    TeachLinesPara.Col2s.Add(LineCol2);
                    TeachLinesPara.Row1s.Add(LineRow1);
                    TeachLinesPara.Row2s.Add(LineRow2);
                }
                TeachLinesPara.Thresholds.Add(Threshold);
                TeachLinesPara.DetectHeights.Add(DetectHeight);
                TeachLinesPara.Elements.Add(Element);
            }
            else if (TeachLinesPara.Col1s.Count - 1 >= LineNum) {
                if (LineCol1 != null && LineCol2 != null && LineCol1.Length > 0){
                    TeachLinesPara.Col1s[LineNum] = LineCol1;
                    TeachLinesPara.Col2s[LineNum] = LineCol2;
                    TeachLinesPara.Row1s[LineNum] = LineRow1;
                    TeachLinesPara.Row2s[LineNum] = LineRow2;
                }
                TeachLinesPara.Elements[LineNum] = Element;
                TeachLinesPara.Thresholds[LineNum] = Threshold;
                TeachLinesPara.DetectHeights[LineNum] = DetectHeight;
                TeachLinesPara.Count = TeachLinesPara.Col1s.Count();
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
            if (TeachLinesPara.Elements.Count > LineNum){
                ElementsBar.Value = (int)TeachLinesPara.Elements[LineNum];
                Element = ElementsBar.Value;
                ElementsTxt.Text = Element.ToString();
            }
            if (TeachLinesPara.DetectHeights.Count > LineNum) {
                DetectHeightBar.Value = (int)TeachLinesPara.DetectHeights[LineNum];
                DetectHeight = DetectHeightBar.Value;
                DetectHeightTxt.Text = DetectHeight.ToString();
            }
            if (TeachLinesPara.Thresholds.Count > LineNum) {
                ThresholdBar.Value = (int)TeachLinesPara.Thresholds[LineNum];
                Threshold = ThresholdBar.Value;
                ThresholdTxt.Text = Threshold.ToString();
            }
            if (TeachLinesPara.Col1s.Count > LineNum) {
                LineCol1 = TeachLinesPara.Col1s[LineNum];
                LineCol2 = TeachLinesPara.Col2s[LineNum];
                LineRow1 = TeachLinesPara.Row1s[LineNum];
                LineRow2 = TeachLinesPara.Row2s[LineNum];
            }
            if (TeachLinesPara.CenterClips.Count > LineNum) {
                CenterClipBar.Value = (int)(TeachLinesPara.CenterClips[LineNum] * 100.0);
                CenterClip = TeachLinesPara.CenterClips[LineNum];
                ClipCenterText.Text = CenterClip.ToString();
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
            //St_VectorAngle VectorAngle0 = new St_VectorAngle(LocalPara0.Template.CenterY, LocalPara0.Template.CenterX, LocalPara0.Template.TemplateAngle);
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
            HTuple ImgHomMat = new HTuple();
            HObject AffineImg = new HObject();
            switch (LocalPara0.localSetting.localModel)
            {
                case LocalModelEnum.TempTwoLine:
                case LocalModelEnum.TempThreeLine:
                case LocalModelEnum.TempFourLine:
                case LocalModelEnum.TempLineCircle:
                    HOperatorSet.VectorAngleToRigid(TempFindVectorAngle.Row, TempFindVectorAngle.Col, TempFindVectorAngle.Angle
                        , LocalPara0.Template.CenterY, LocalPara0.Template.CenterX, LocalPara0.Template.TemplateAngle, out ImgHomMat);
                    HOperatorSet.AffineTransImage(GrabedImg, out AffineImg, ImgHomMat, "constant", "false");
                    view1.AddImage(AffineImg);
                    break;
            }

            txtFindTime.Text = sw.ElapsedMilliseconds.ToString();
        }

        private void ClearParaBtn_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("真的要清除参数？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt != DialogResult.Yes) return;
            int initLineCount = 1;
            int i = 1;
            while (true)
            {
                rlt = MessageBox.Show("示教"+i.ToString()+"条线？", "线数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rlt == DialogResult.Yes)
                {
                    initLineCount = i;
                    break;
                }
                i++;
            }
            TeachLinesPara = new St_LinesParam(initLineCount);
            InitCombox();
        }

        private void SubFrmFindLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
        }


        private void CenterClipBar_Scroll(object sender, EventArgs e)
        {
            double value = ((double)CenterClipBar.Value) / 100.0;
            ClipCenterText.Text = value.ToString();
            CenterClip = value;
        }
    }
}
