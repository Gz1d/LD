using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
//using TapeVision.CameraSDK;
using System.Threading;
//using TapeVisionBase.外部接口;
using System.Diagnostics;



namespace VisionBase
{
    public partial class SubFrmFindCircle : Form
    {
        public St_CirclesParam CircleParam { get { return TeachCirclePara; } }
        public St_VectorAngle TeachFindVectorAngle = new St_VectorAngle();
        public HObject GrabedImg = new HObject();
        
        public string CameraName = "";
        public double DistOffsetPixelX = 0;
        public double DistOffsetPixelY = 0;
        public double DistOffsetPixelTh = 0;
        private int Element = 32;
        private int DetectHeight = 20;
        private int Threshold = 10;
        private int CircleNum = 0;
        public bool ShowRoiFlag = false;
        private double TeachCenterRow = 0;
        private double TeachCenterCol = 0;
        private double TeachCircleR = 0;
        private double TeachStartPhi = 0;
        private double TeachEndPhi = 0;
        private string TeachPointOrderStr = "positive";
        private string   TeachArcType = "";
        private HTuple  Direct = new HTuple();
        private St_CirclesParam TeachCirclePara = new St_CirclesParam();
        private Action<bool> ActionIsDrawingRoi;
        
        private LocalPara LocalPara0;
        public HTuple FindCircleRow = new HTuple();
        public HTuple FindCircleCol = new HTuple();
        public HTuple FindCircleR = new HTuple();
        private ViewControl view1;

        public SubFrmFindCircle(St_CirclesParam circleParam,ViewControl viewIn,  HObject ImgIn, Action<bool> drawRoiInHWindow)
        {
            InitializeComponent();
            TeachCirclePara = circleParam;
            view1 = viewIn;
            ActionIsDrawingRoi = drawRoiInHWindow;
            if (ImgIn != null && ImgIn.IsInitialized()){
                HOperatorSet.CopyImage(ImgIn, out GrabedImg);
            }
            CircleSelectComBox.Items.Clear();
            if (circleParam.Count == 1){
                CircleSelectComBox.Items.Add("Circle1");
            }
            else if (circleParam.Count == 2){
                CircleSelectComBox.Items.Add("Circle1");
                CircleSelectComBox.Items.Add("Circle2");
            }
            else if (circleParam.Count == 3) {
                CircleSelectComBox.Items.Add("Circle1");
                CircleSelectComBox.Items.Add("Circle2");
                CircleSelectComBox.Items.Add("Circle3");
            }
            else if (circleParam.Count == 4){
                CircleSelectComBox.Items.Add("Circle1");
                CircleSelectComBox.Items.Add("Circle2");
                CircleSelectComBox.Items.Add("Circle3");
                CircleSelectComBox.Items.Add("Circle4");
            }
        }
        public void UpDatePara(LocalPara ParaIn){
            LocalPara0 = ParaIn;
        }

        private void FrmSubFrmFindLine_Load(object sender, EventArgs e){
            HTuple Sx = new HTuple(), Sy = new HTuple(), Phi = new HTuple(), Theta = new HTuple(), Tx = new HTuple(), Ty = new HTuple();
            CircleSelectComBox.SelectedIndex = 0;
            CircleSelectComBox_SelectedIndexChanged(null, EventArgs.Empty);
        }

        public void UpdateCurrImage(HObject img, St_VectorAngle TempFindVectorAngleIn, LocalPara LocalParaIn)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
            HOperatorSet.CopyImage(img, out GrabedImg);
            TeachFindVectorAngle = TempFindVectorAngleIn;
            LocalPara0 = LocalParaIn;
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

        private void SelectCircleBtn_Click(object sender, EventArgs e) {
            CircleNum = CircleSelectComBox.SelectedIndex;
            DialogResult rlt = MessageBox.Show("真的要清除参数？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt != DialogResult.Yes) return;
            rlt = MessageBox.Show("示教2个圆？", "圆数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.Yes) {
                CircleNum = 1;
                CircleSelectComBox.Items.Clear();
                CircleSelectComBox.Items.Add("Circle1");
                CircleSelectComBox.Items.Add("Circle2");
                TeachCirclePara = new St_CirclesParam(CircleNum + 1);
                return;
            }
            rlt = MessageBox.Show("示教1个圆？", "圆数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.Yes){
                CircleSelectComBox.Items.Clear();
                CircleSelectComBox.Items.Add("Circle1");
                CircleNum = 0;
                TeachCirclePara = new St_CirclesParam(CircleNum + 1);
                return;
            }
            rlt = MessageBox.Show("示教3个圆？", "圆数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.Yes) {
                CircleNum = 2;
                CircleSelectComBox.Items.Clear();
                CircleSelectComBox.Items.Add("Circle1");
                CircleSelectComBox.Items.Add("Circle2");
                CircleSelectComBox.Items.Add("Circle3");
                TeachCirclePara = new St_CirclesParam(CircleNum + 1);
                return;
            }
            rlt = MessageBox.Show("示教4个圆？", "圆数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.Yes){
                CircleNum = 3;
                CircleSelectComBox.Items.Clear();
                CircleSelectComBox.Items.Add("Circle1");
                CircleSelectComBox.Items.Add("Circle2");
                CircleSelectComBox.Items.Add("Circle3");
                CircleSelectComBox.Items.Add("Circle4");
                TeachCirclePara = new St_CirclesParam(CircleNum + 1);
                return;
            }
        }

        private void StartTeachBtn1_Click(object sender, EventArgs e)
        {
            HObject RoiContour = new HObject();
            HTuple ArcType = new HTuple();       
            if (StartTeachBtn1.Text == "开始示教")  {
                GroupBoxEnable(groupBox3, false);
                StartTeachBtn1.Enabled = true;
                StartTeachBtn1.Text = "保存";
                view1.Refresh();
                view1.AddImage(GrabedImg);
                ActionIsDrawingRoi(true);
                view1.roiController.reset();
                view1.roiController.setROIShape(new ViewROI.ROICircularArc());
            }
            else if (StartTeachBtn1.Text == "保存") {
                ActionIsDrawingRoi(false);
                GroupBoxEnable(groupBox3, true);            
                ViewROI.ROICircularArc arc = new ViewROI.ROICircularArc();
                if (view1.roiController.getActiveROI() is ViewROI.ROICircularArc)
                {
                    StartTeachBtn1.Enabled = false;
                    StartTeachBtn1.Text = "开始示教";
                    arc = (ViewROI.ROICircularArc)view1.roiController.getActiveROI();
                    arc.GetCircleArc(out TeachCenterRow, out TeachCenterCol, out TeachCircleR, out TeachStartPhi, out TeachEndPhi);
                    if (ArcType.Length > 0)
                        TeachArcType = ArcType.S;
                    Thread TeachThd = new Thread(TeachProcess);
                    TeachThd.IsBackground = true;
                    TeachThd.Start();
                }
                else {
                    MessageBox.Show("请选中形状");
                    return;
                }
            }   
        }

        private void GroupBoxEnable(GroupBox groupBoxIn,bool IsEnable)
        {
            foreach (Control item in groupBoxIn.Controls) {
                item.Enabled = IsEnable;
            }        
        }

        public void TeachProcess()
        {
            ShowRoiFlag = true;
            HObject GrabImge = new HObject();
            HObject RoiContour = new HObject();
            HTuple ResultRows = new HTuple();
            HTuple ResultCols = new HTuple();
            #region //圆参数示教
            HTuple ArcType = new HTuple();
            HTuple DrawRows = new HTuple(), DrawCols = new HTuple();
            while (ShowRoiFlag) {
                Thread.Sleep(50);
                //2.2显示圆的拟合ROI，条正边界检测参数
                bool CurIsOk = true;
                this.view1.Refresh();
                this.view1.AddImage(this.GrabedImg);
                CurIsOk = MyVisionBase.GenCirclePts2(TeachCenterRow, TeachCenterCol, TeachCircleR, TeachStartPhi, 
                    TeachEndPhi, TeachPointOrderStr, out DrawRows, out DrawCols);
                if (!CurIsOk){
                    StartTeachBtn1.Enabled = true;
                    break;
                }
                CurIsOk = MyVisionBase.gen_spoke_ROI(GrabedImg, out RoiContour, Element, DetectHeight, 2, DrawRows, DrawCols, Direct);
                if (!CurIsOk) {
                    StartTeachBtn1.Enabled = true;
                    break;
                }
                view1.SetDraw("blue", "margin");
                view1.AddViewObject(RoiContour);
                //2.3利用卡尺工具找出拟合边界点
                CurIsOk = MyVisionBase.spoke(GrabedImg, out RoiContour, view1, Element, DetectHeight, 2, 2, Threshold, "all", "first", 
                    DrawRows, DrawCols, Direct, out ResultRows, out ResultCols, out ArcType);
                if (!CurIsOk) break;
                //2.4
                HObject CirclePtCross = new HObject();
                //2.5显示拟合的点
                HOperatorSet.GenCrossContourXld(out CirclePtCross, ResultRows, ResultCols, 12, 0.7);
                view1.SetDraw("green", "margin");
                view1.AddViewObject(CirclePtCross);
                //2.6圆拟合
                HTuple CenterRow = new HTuple(), CenterCol = new HTuple(), hv_Radius = new HTuple(), hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple(),
                    hv_PointOrder = new HTuple(), hv_Flag = new HTuple();
                HObject FitCircleContour = new HObject();
                CurIsOk = MyVisionBase.PtsToBestCircle(out FitCircleContour, view1, ResultRows, ResultCols, 4, TeachArcType, out CenterRow, out CenterCol, 
                    out hv_Radius, out hv_StartPhi,out hv_EndPhi, out hv_PointOrder, out hv_Flag);
                if (!CurIsOk) break;
                //2.7显示拟合圆
                view1.SetDraw("red", "margin");
                view1.AddViewObject(FitCircleContour);
                view1.Repaint();
                Thread.Sleep(1000);
            }
            #endregion
        }

        private void SaveParaBtn_Click(object sender, EventArgs e)
        {
            if (TeachCirclePara.CenterRows.Count - 1 < CircleNum) {
                this.TeachCirclePara.CenterRows.Add(TeachCenterRow);
                this.TeachCirclePara.CenterCols.Add(TeachCenterCol);
                this.TeachCirclePara.CircleRs.Add(TeachCircleR);
                this.TeachCirclePara.DetectHeights.Add(DetectHeight);
                this.TeachCirclePara.Elements.Add(Element);
                this.TeachCirclePara.Thresholds.Add(Threshold);
                this.TeachCirclePara.Directs.Add(Direct.ToString());
                this.TeachCirclePara.StartPhis.Add(TeachStartPhi);
                this.TeachCirclePara.EndPhis.Add(TeachEndPhi);
                this.TeachCirclePara.PointOrders.Add(TeachPointOrderStr);
                this.TeachCirclePara.Directs.Add(Direct.S);
            }
            else if (TeachCirclePara.CenterRows.Count - 1 >= CircleNum) {
                this.TeachCirclePara.CenterRows[CircleNum] = TeachCenterRow;
                this.TeachCirclePara.CenterCols[CircleNum] = TeachCenterCol;
                this.TeachCirclePara.CircleRs[CircleNum] = TeachCircleR;
                this.TeachCirclePara.DetectHeights[CircleNum] = DetectHeight;
                this.TeachCirclePara.Elements[CircleNum] = Element;
                this.TeachCirclePara.Thresholds[CircleNum] = Threshold;
                this.TeachCirclePara.Directs[CircleNum] = Direct.ToString();
                this.TeachCirclePara.StartPhis[CircleNum] = TeachStartPhi;
                this.TeachCirclePara.EndPhis[CircleNum] = TeachEndPhi;
                this.TeachCirclePara.PointOrders[CircleNum] = TeachPointOrderStr;
                this.TeachCirclePara.Directs[CircleNum] = Direct.S;
            }
        }
        private void StopTeachBtn_Click(object sender, EventArgs e) {
            StartTeachBtn1.Enabled = true;
            ShowRoiFlag = false;
        }

        private void FindCircleBtn_Click(object sender, EventArgs e) {
            try{
                Stopwatch sw = new Stopwatch();
                sw.Start();
                txtFindTime.Text = "";
                #region
                view1.Refresh();               
                HTuple CircleRows = new HTuple(), CircleCols = new HTuple();
                HObject RoiContour = new HObject();
                HTuple ResultRows = new HTuple(), ResultCols = new HTuple(), ArcType0 = new HTuple();              
                view1.AddImage(GrabedImg);
                HTuple FitCirRows = new HTuple(),FitCirCols =new HTuple(),FitCirRs=new HTuple(),CirStartPhis =new HTuple(),CirEndPhi =new HTuple();
                for (int i = 0; i < TeachCirclePara.CenterRows.Count; i++) {
                    //利用存下来的圆的信息，创建圆的边缘点坐标                 
                    MyVisionBase.GenCirclePts2(TeachCirclePara.CenterRows[i], TeachCirclePara.CenterCols[i], TeachCirclePara.CircleRs[i],
                        TeachCirclePara.StartPhis[i], TeachCirclePara.EndPhis[i], TeachCirclePara.PointOrders[i], out CircleRows, out CircleCols);
                    HObject CrossContour = new HObject();
                    HOperatorSet.GenCrossContourXld(out CrossContour, CircleRows, CircleCols, 6, 0.6);
                    view1.SetDraw("blue", "margin");
                    view1.AddViewObject(CrossContour);
                    //生成圆的边缘ROI
                    MyVisionBase.gen_spoke_ROI(GrabedImg, out RoiContour, TeachCirclePara.Elements[i], TeachCirclePara.DetectHeights[i],
                        2, CircleRows, CircleCols, TeachCirclePara.Directs[i]);
                    view1.AddViewObject(RoiContour);
                    //3.0找出圆的边缘点
                    MyVisionBase.spoke(GrabedImg, out RoiContour, view1, TeachCirclePara.Elements[i], TeachCirclePara.DetectHeights[i], 
                        2, 2, TeachCirclePara.Thresholds[i], "all", "first", CircleRows, CircleCols, TeachCirclePara.Directs[i],
                        out ResultRows, out ResultCols, out ArcType0);
                    HObject CirclePtCross = new HObject();
                    //2.5显示拟合的点
                    HOperatorSet.GenCrossContourXld(out CirclePtCross, ResultRows, ResultCols, 12, 0.7);
                    if (ResultRows.Length>i&&ResultCols.Length>i) {
                        FitCirRows[i] = ResultRows.D;
                        FitCirCols[i] = ResultCols.D;                   
                    }         
                    view1.SetDraw("green", "margin");
                    view1.AddViewObject(CirclePtCross);
                    txtFindTime.Text = sw.ElapsedMilliseconds.ToString();
                }
                HObject CircleCont=new HObject(),CirContour =new HObject(),CirCross = new HObject();
                CircleTypePos.FindCircle(GrabedImg, TeachCirclePara, out  FitCirRows, out FitCirCols, out  FitCirRs,
                    out CirStartPhis, out CirEndPhi, out CircleCont, out CirContour, out CirCross);
                this.view1.SetDraw("red", "margin");
                this.view1.AddViewObject(CircleCont);
                this.view1.Repaint();
                HTuple PlDist = new HTuple(), DistPp = new HTuple(), DistPp1=new HTuple();
                if (FitCirRows.Length >= 3 && FitCirCols.Length>=3){
                    HOperatorSet.DistancePl(FitCirRows[2].D, FitCirCols[2].D, FitCirRows[0].D, FitCirCols[0].D, FitCirRows[1].D, FitCirCols[1].D, out PlDist);
                    HOperatorSet.DistancePp(FitCirRows[0].D, FitCirCols[0].D, FitCirRows[1].D, FitCirCols[1].D, out DistPp);
                    HOperatorSet.DistancePp(FitCirRows[0].D, FitCirCols[0].D, FitCirRows[2].D, FitCirCols[2].D, out DistPp1);
                    PlDist = 3.2 * 3.0 / DistPp.D * DistPp1.D;
                    txtFindTime.Text = PlDist.D.ToString();
                }
                #endregion
            }
            catch (Exception e0) {
                Logger.PopError(e0.Message.ToString(), true);
            }
        }



        private void CircleSelectComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CircleNum = CircleSelectComBox.SelectedIndex;
            if (TeachCirclePara.Elements.Count > CircleNum){
                ElementsBar.Value = (int)TeachCirclePara.Elements[CircleNum];
                Element = ElementsBar.Value;
                ElementsTxt.Text = Element.ToString();
            }
            if (TeachCirclePara.DetectHeights.Count > CircleNum){
                DetectHeightBar.Value = (int)TeachCirclePara.DetectHeights[CircleNum];
                DetectHeight = DetectHeightBar.Value;
                DetectHeightTxt.Text = DetectHeight.ToString();
            }
            if (TeachCirclePara.Thresholds.Count > CircleNum){
                ThresholdBar.Value = (int)TeachCirclePara.Thresholds[CircleNum];
                Threshold = ThresholdBar.Value;
                ThresholdTxt.Text = Threshold.ToString();
            }
            if (TeachCirclePara.Thresholds.Count > CircleNum) { 
                TeachCenterRow = TeachCirclePara.CenterRows[CircleNum];
                TeachCenterCol = TeachCirclePara.CenterCols[CircleNum];
                TeachCircleR = TeachCirclePara.CircleRs[CircleNum];              
                TeachStartPhi = TeachCirclePara.StartPhis[CircleNum];
                TeachEndPhi = TeachCirclePara.EndPhis[CircleNum];
                TeachPointOrderStr = TeachCirclePara.PointOrders[CircleNum];
            }
            if (TeachCirclePara.Thresholds.Count > CircleNum)
            {
                Direct = new HTuple(TeachCirclePara.Directs[CircleNum]);
                if (TeachCirclePara.Directs[CircleNum] == "inner"){
                    DirectCbx.SelectedIndex = 0;}
                else
                    DirectCbx.SelectedIndex = 1;
            }
           
        }
        private void TryDebugBtn_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try{
                #region
                Point2Db MotionPos = new Point2Db(0, 0);
                HObject CirContour = new HObject();
                HObject circle = new HObject();
                HObject centerCont = new HObject();
                //模板的VectorAngle
                double TempTeachRow = LocalPara0.Template.CenterY;
                double TempTeachCol = LocalPara0.Template.CenterX;
                double TempTeachAngle = LocalPara0.Template.TemplateAngle;
                view1.Refresh();
                HTuple Y = new HTuple(), X = new HTuple();
                //找出要定位的的圆
                St_CirclesParam AdjCirclePara = new St_CirclesParam(2);
                St_VectorAngle VectorAngle0 = new St_VectorAngle(LocalPara0.Template.CenterY, LocalPara0.Template.CenterX,
                    LocalPara0.Template.TemplateAngle);
                HTuple CircleStartPhi =new HTuple(), CircleEndPhi =new HTuple();
                CircleTypePos.FindCircle(GrabedImg, AdjCirclePara, out Y, out X, out FindCircleR,out  CircleStartPhi,
                    out  CircleEndPhi, out circle, out CirContour, out centerCont);
                FindCircleRow = Y;
                FindCircleCol = X;
                view1.AddImage(GrabedImg);
                Thread.Sleep(100);
                view1.SetDraw("green", "margin");
                view1.AddViewObject(circle);
                view1.SetDraw("blue", "margin");
                view1.AddViewObject(CirContour);
                view1.SetDraw("red", "margin");
                view1.AddViewObject(centerCont);
                HObject CirCenterCross = new HObject();
                HOperatorSet.GenCrossContourXld(out CirCenterCross, Y, X, 120, 0.6);
                view1.AddViewObject(CirCenterCross);
                #endregion
            }
            catch (Exception e0){
                Logger.PopError(e0.Message.ToString(), true);
            }
        }

        private void SubFrmFindCircle_FormClosed(object sender, FormClosedEventArgs e) {
            if (GrabedImg != null) GrabedImg.Dispose();
        }

        private void GenVirtualBtn_Click(object sender, EventArgs e){
            TryDebugBtn_Click(null, new EventArgs());
            if (FindCircleRow.Length >= 2 && FindCircleCol.Length >= 2){
                HTuple OutLineRow1 = new HTuple(), OutLineCol1 = new HTuple(), OutLineRow2 = new HTuple(), OutLineCol2 = new HTuple();
                view1.ResetView();
                view1.AddImage(GrabedImg);
                //1.0找线
                TryDebugBtn_Click(null, new EventArgs());
                HTuple LineAngle = new HTuple();
                HObject LineArrow = new HObject();
                HObject LitCirCross = new HObject();
                //2.0计算圆心连线与相机X轴的夹角
                OutLineRow1 = FindCircleRow[0].D;
                OutLineCol1 = FindCircleCol[0].D;
                OutLineRow2 = FindCircleRow[1].D;
                OutLineCol2 = FindCircleCol[1].D;
                LineTypePos.CalculateLineAng(OutLineRow1, OutLineCol1, OutLineRow2, OutLineCol2, out LineAngle, out LineArrow);
                LineAngle = LineAngle.D / Math.PI * 180.0;
                HTuple LitCirRow = new HTuple(), LitCircol = new HTuple();
                HTuple DistRr = (7.78 / 2.0) / 2.8 * FindCircleR[0].D;
                //3.0生成虚拟圆的圆心
                CircleTypePos.GenCirCenter(FindCircleRow[0].D, FindCircleCol[0].D, DistRr, out LitCirRow, out LitCircol);
                HOperatorSet.GenCrossContourXld(out LitCirCross, LitCirRow, LitCircol, 60, 0);
                view1.SetDraw("blue", "margin");
                view1.AddViewObject(LineArrow);
                view1.AddViewObject(LitCirCross);
                HHomMat2D MyHomMat = new HHomMat2D();
                MyHomMat = MyHomMat.HomMat2dRotate((TeachCirclePara.OffSetPixelTh + LineAngle) / 180.0 * Math.PI, FindCircleRow[0], FindCircleCol[0]);
                //4.0旋转生成圆的圆心
                MyHomMat.AffineTransPixel(LitCirRow, LitCircol, out LitCirRow, out LitCircol);
                HOperatorSet.GenCrossContourXld(out LitCirCross, LitCirRow, LitCircol, 60, 0);
                HObject FirstCirCont = new HObject(), SecondCirCont = new HObject();
                HOperatorSet.GenCircleContourXld(out FirstCirCont, LitCirRow[0].D, LitCircol[0].D, 60, 0, 2 * Math.PI, "positive", 0.1);
                HOperatorSet.GenCircleContourXld(out SecondCirCont, LitCirRow[1].D, LitCircol[1].D, 40, 0, 2 * Math.PI, "positive", 0.1);
                HOperatorSet.ConcatObj(LitCirCross, FirstCirCont, out LitCirCross);
                HOperatorSet.ConcatObj(LitCirCross, SecondCirCont, out LitCirCross);
                view1.SetDraw("red", "margin");
                view1.AddViewObject(LitCirCross);
            }
        }

        private void DirectCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Direct = new HTuple(DirectCbx.SelectedItem.ToString());
        }
    }
}
