using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace VisionBase
{
    public enum En_InspectType
    {
        未定义=-1,
        找线,
        找圆,
        找矩形
    }
    public partial class SubFrmRecheck : Form
    {
        public  St_InspectImageSetting RecheckParam { get { return InspectImageSetting; } }
        private  St_InspectImageSetting InspectImageSetting;
        private En_InspectType InspectType = En_InspectType.未定义;
        private HObject GrabedImg = new HObject();
        private ViewControl myView;
        public bool ShowRoiFlag = false;
        private int Element = 32;
        private int DetectHeight = 20;
        private int Threshold = 10;
        public int ThreeThreshold = 10;
        private List<double> ListGrays = new List<double>();
        private Action<bool> ActionIsDrawingRoi;

        private LocalPara TeachLocalPara;
        St_VectorAngle TempFindVectorAngle = new St_VectorAngle();

        #region 找线参数
        private HTuple LineRow1 = new HTuple();
        private HTuple LineRow2 = new HTuple();
        private HTuple LineCol1 = new HTuple();
        private HTuple LineCol2 = new HTuple();

        private HTuple LineRow10 = new HTuple();
        private HTuple LineRow20 = new HTuple();
        private HTuple LineCol10 = new HTuple();
        private HTuple LineCol20 = new HTuple();
        #endregion

        #region 找圆参数
        private HTuple DrawRows = new HTuple(), DrawCols = new HTuple(), Direct = new HTuple();
        private HTuple ArcType = new HTuple();
        private double AddR = 10;
        #endregion

        #region 找矩形参数
        private HTuple RectRow = new HTuple();
        private HTuple RectCol = new HTuple();
        private HTuple RectPhi = new HTuple();
        private HTuple RectHeight = new HTuple();
        private HTuple RectWidth = new HTuple();
        private HTuple RectMeanGray = new HTuple();
        #endregion
        //LocalPara VisionParaIn,   St_BlobInspectParam1 blobInSpectParam, HWindow InHWindow, HObject ImgIn, Action<bool> drawRoiInHWindow
        public SubFrmRecheck(LocalPara VisionParaIn,St_InspectImageSetting setting, ViewControl viewIn, HObject ImgIn, Action<bool> drawRoiInHWindow)
        {
            InitializeComponent();
            TeachLocalPara = VisionParaIn;
            InspectImageSetting = setting;
            if (ImgIn != null && ImgIn.IsInitialized())    GrabedImg = ImgIn;
            myView = viewIn;
            ActionIsDrawingRoi = drawRoiInHWindow;

        }
        public void UpdatePara(St_LinesParam LineParaIn,St_CirclesParam CircleParaIn,St_VectorAngle VectorAngle0In )
        {
            InspectImageSetting.VectorAngle0 = VectorAngle0In;
            InspectImageSetting.CirclePara = CircleParaIn;
            InspectImageSetting.LinePara = LineParaIn;
        }
        public void UpdateCurrImage(HObject img, St_VectorAngle TempFindVectorAngleIn, LocalPara LocalParaIn)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
            HOperatorSet.CopyImage(img, out GrabedImg);
            TempFindVectorAngle = TempFindVectorAngleIn;
            TeachLocalPara = LocalParaIn;
        }

        private void SubFrmRecheck_Load(object sender, EventArgs e)
        {
            InitListView();
            ListGrays = new List<double>();
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
        }

        private void InitListView()
        {
            listViewItem.Items.Clear();
            ListViewItem item;
            for (int i = 0; i < 3; i++) {
                item = new ListViewItem();
                item.Text = (i + 1).ToString();
                if (i == 0){
                    item.SubItems.Add("找线");
                    item.SubItems.Add(InspectImageSetting.InspectLinePara.Count.ToString());
                }
                else if (i == 1){
                    item.SubItems.Add("找圆");
                    item.SubItems.Add(InspectImageSetting.InspectCirclePara.Count.ToString());
                }
                else if(i==2) {
                    item.SubItems.Add("矩形区域");
                    item.SubItems.Add(InspectImageSetting.InspectRectPara.Count.ToString());
                }
                else {

                }
                listViewItem.Items.Add(item);
            }
        }

        public void UpdateCurrImage(HObject img, Point2Db tempOffsetPt)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
            if (img.IsInitialized())
            HOperatorSet.CopyImage(img, out GrabedImg);

        }

        public void DisableTestBtn(bool isEnable)
        {
            btnItemTest.Enabled = isEnable;
            btnRecheckTest.Enabled = isEnable;
        }

        private void btnAutoCali_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("确定要编辑参数？", "编辑确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.Yes) {
                btnEdit.Enabled = false;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                groupBox1.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
            }
        }

        private void SubFrmRecheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
        }

        private void listViewItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewItem.SelectedItems == null || listViewItem.SelectedItems.Count < 1) return;
            int index = listViewItem.SelectedItems[0].Index;
            string name="";
            int count=0;
            switch ((En_InspectType)index){
                case En_InspectType.找线:
                {
                    name = "线";
                    lbAddR.Text = "Threshold";
                    lbElements.Text = "Elements";
                    lbDetHeight.Text = "DetectHeight";
                    BarAddR.Minimum = 0;
                    BarAddR.Maximum = 255;
                    BarAddR.TickFrequency = 25;
                    DetectHeightBar.Minimum = 0;
                    DetectHeightBar.Maximum = 100;
                    DetectHeightBar.TickFrequency = 10;
                    ThresholdLabel.Text = "DetectWidth";
                    ThresholdBar.Maximum = 20;
                    ThresholdBar.Minimum = 0;
                    ThresholdBar.TickFrequency = 2;
                    count = InspectImageSetting.InspectLinePara.Count;
                    break;
                }
                case En_InspectType.找圆:
                {
                    name = "圆";
                    lbAddR.Text = "Threshold";
                    lbElements.Text = "Elements";
                    lbDetHeight.Text = "DetectHeight";
                    BarAddR.Value = 5;
                    BarAddR.Minimum = 0;
                    BarAddR.Maximum = 255;
                    BarAddR.TickFrequency = 25;
                    BarAddR.Enabled = true;
                    DetectHeightBar.Minimum = 0;
                    DetectHeightBar.Maximum = 20;
                    DetectHeightBar.TickFrequency = 2;
                    ThresholdLabel.Text = "DetectWidth";
                    ThresholdBar.Maximum = 20;
                    ThresholdBar.Minimum = 0;
                    ThresholdBar.TickFrequency = 2;
                    count = InspectImageSetting.InspectCirclePara.Count;
                    break;
                }
                case En_InspectType.找矩形:
                {
                    name = "矩形";
                    lbAddR.Text = "DarkGrayRangge";
                    BarAddR.Value = 5;
                    BarAddR.Minimum = 0;
                    BarAddR.Maximum = 255;
                    lbElements.Text = "DivRange";
                    lbDetHeight.Text = "GrayRange";
                    DetectHeightBar.Minimum = 0;
                    DetectHeightBar.Maximum = 255;
                    DetectHeightBar.TickFrequency = 25;
                    ThresholdLabel.Text = "NG Area";
                    ThresholdBar.Maximum = 2000;
                    ThresholdBar.Minimum = 0;
                    ThresholdBar.TickFrequency = 200;
                    count = InspectImageSetting.InspectRectPara.Count;
                    break;
                }
                default:
                {
                    InspectType = (En_InspectType)index;
                    return;
                }
            }
            InspectType = (En_InspectType)index;
            InitCmbObject(name, count);
        }

        private void InitCmbObject(string name,int count)
        {
            cmbObjectSelect.Items.Clear();
            for (int i = 0; i < count; i++){
                cmbObjectSelect.Items.Add(name + (i + 1).ToString());
            }
            if (count > 0) cmbObjectSelect.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("确定要添加？", "添加确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rlt==DialogResult.Yes) {
                switch (InspectType) {
                    case En_InspectType.找线:
                        {
                            InspectImageSetting.InspectLinePara.Count++;
                            break;
                        }
                    case En_InspectType.找圆:
                        {
                            InspectImageSetting.InspectCirclePara.Count++;
                            break;
                        }
                    case En_InspectType.找矩形:
                        {
                            InspectImageSetting.InspectRectPara.Count++;
                            break;
                        }
                    default: break;
                }
                btnEdit.Enabled = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             DialogResult rlt = MessageBox.Show("确定要删除？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (rlt == DialogResult.Yes) {
                 switch (InspectType) {
                     case En_InspectType.找线:
                         {
                             InspectImageSetting.InspectLinePara.Count--;
                             break;
                         }
                     case En_InspectType.找圆:
                         {
                             InspectImageSetting.InspectCirclePara.Count--;
                             break;
                         }
                     case En_InspectType.找矩形:
                         {
                             InspectImageSetting.InspectRectPara.Count--;
                             break;
                         }
                     default: break;
                 }
                 btnEdit.Enabled = false;
                 btnAdd.Enabled = false;
                 btnDelete.Enabled = false;
             } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("确定要保存参数？", "保存确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.Yes) {
                switch (InspectType) {
                    case En_InspectType.找线:
                        {
                            if (InspectImageSetting.InspectLinePara.Count > InspectImageSetting.InspectLinePara.Row1s.Count){
                                InspectImageSetting.InspectLinePara.Row1s.Add(0);
                                InspectImageSetting.InspectLinePara.Col1s.Add(0);
                                InspectImageSetting.InspectLinePara.Row2s.Add(10);
                                InspectImageSetting.InspectLinePara.Col2s.Add(10);
                                InspectImageSetting.InspectLinePara.Thresholds.Add(10);
                                InspectImageSetting.InspectLinePara.Elements.Add(10);
                                InspectImageSetting.InspectLinePara.DetectHeights.Add(10);
                                InspectImageSetting.InspectLinePara.DetectWidths.Add(10);
                                List<double> LineGray = new List<double>();
                                if (InspectImageSetting.InspectLinePara.Thresholds.Count < InspectImageSetting.InspectLinePara.Count) {
                                    int NunLine = InspectImageSetting.InspectLinePara.Count;
                                    int NumThreshold = InspectImageSetting.InspectLinePara.Thresholds.Count;
                                    for (int i = 0; i < NunLine - NumThreshold; i++){
                                        InspectImageSetting.InspectLinePara.Thresholds.Add(10);
                                    }                                                            
                                }
                                int count = InspectImageSetting.InspectLinePara.Elements.Count;
                                for (int j = 0; j < InspectImageSetting.InspectLinePara.Elements[count - 1]; j++){
                                    LineGray.Add(0);
                                }
                                InspectImageSetting.InspectLinePara.DetectRegionGrays.Add(LineGray);
                            }
                            else if (InspectImageSetting.InspectLinePara.Count < InspectImageSetting.InspectLinePara.Row1s.Count) {
                                int index = InspectImageSetting.InspectLinePara.Count;
                                InspectImageSetting.InspectLinePara.Row1s.RemoveAt(index);
                                InspectImageSetting.InspectLinePara.Col1s.RemoveAt(index);
                                InspectImageSetting.InspectLinePara.Row2s.RemoveAt(index);
                                InspectImageSetting.InspectLinePara.Col2s.RemoveAt(index);
                                InspectImageSetting.InspectLinePara.Elements.RemoveAt(index);
                                InspectImageSetting.InspectLinePara.DetectHeights.RemoveAt(index);
                                InspectImageSetting.InspectLinePara.DetectWidths.RemoveAt(index);
                                InspectImageSetting.InspectLinePara.DetectRegionGrays.RemoveAt(index);
                            }
                            else { }
                            break;
                        }
                    case En_InspectType.找圆:
                        {
                            if (InspectImageSetting.InspectCirclePara.Count > InspectImageSetting.InspectCirclePara.Directs.Count){
                                InspectImageSetting.InspectCirclePara.ListRows.Add(new List<double>());
                                InspectImageSetting.InspectCirclePara.ListCols.Add(new List<double>());
                                InspectImageSetting.InspectCirclePara.Directs.Add("");
                                InspectImageSetting.InspectCirclePara.AddRs.Add(10);
                                InspectImageSetting.InspectCirclePara.Elements.Add(10);
                                InspectImageSetting.InspectCirclePara.DetectHeights.Add(10);
                                InspectImageSetting.InspectCirclePara.DetectWidths.Add(10);
                                InspectImageSetting.InspectCirclePara.Thresholds.Add(10);
                                List<double> LineGray = new List<double>();
                                int count = InspectImageSetting.InspectCirclePara.Elements.Count;
                                for (int j = 0; j < InspectImageSetting.InspectCirclePara.Elements[count - 1]; j++){
                                    LineGray.Add(0);
                                }
                                InspectImageSetting.InspectCirclePara.DetectRegionGrays.Add(LineGray);
                            }
                            else if (InspectImageSetting.InspectCirclePara.Count < InspectImageSetting.InspectCirclePara.Directs.Count) {
                                int index = InspectImageSetting.InspectCirclePara.Count;
                                InspectImageSetting.InspectCirclePara.ListRows.RemoveAt(index);
                                InspectImageSetting.InspectCirclePara.ListCols.RemoveAt(index);
                                InspectImageSetting.InspectCirclePara.Directs.RemoveAt(index);
                                InspectImageSetting.InspectCirclePara.AddRs.RemoveAt(index);
                                InspectImageSetting.InspectCirclePara.Elements.RemoveAt(index);
                                InspectImageSetting.InspectCirclePara.DetectHeights.RemoveAt(index);
                                InspectImageSetting.InspectCirclePara.DetectWidths.RemoveAt(index);
                                InspectImageSetting.InspectCirclePara.DetectRegionGrays.RemoveAt(index);
                            }
                            else 
                            {  }
                            break;
                        }
                    case En_InspectType.找矩形:
                        {
                            if (InspectImageSetting.InspectRectPara.Count > InspectImageSetting.InspectRectPara.Rows.Count){
                                InspectImageSetting.InspectRectPara.Rows.Add(10);
                                InspectImageSetting.InspectRectPara.Cols.Add(10);
                                InspectImageSetting.InspectRectPara.Phis.Add(1);
                                InspectImageSetting.InspectRectPara.Widths.Add(10);
                                InspectImageSetting.InspectRectPara.Heights.Add(10);
                                InspectImageSetting.InspectRectPara.MeanGrays.Add(10);
                                InspectImageSetting.InspectRectPara.DivGrays.Add(10);
                                InspectImageSetting.InspectRectPara.GrayRanges.Add(10);
                                InspectImageSetting.InspectRectPara.NgAreas.Add(100);
                            }
                            else if (InspectImageSetting.InspectRectPara.Count < InspectImageSetting.InspectRectPara.Rows.Count) {
                                int index = InspectImageSetting.InspectRectPara.Count;
                                InspectImageSetting.InspectRectPara.Rows.RemoveAt(index);
                                InspectImageSetting.InspectRectPara.Cols.RemoveAt(index);
                                InspectImageSetting.InspectRectPara.Phis.RemoveAt(index);
                                InspectImageSetting.InspectRectPara.Widths.RemoveAt(index);
                                InspectImageSetting.InspectRectPara.Heights.RemoveAt(index);
                                InspectImageSetting.InspectRectPara.MeanGrays.RemoveAt(index);
                                InspectImageSetting.InspectRectPara.DivGrays.RemoveAt(index);
                                InspectImageSetting.InspectRectPara.GrayRanges.RemoveAt(index);
                                InspectImageSetting.InspectRectPara.NgAreas.RemoveAt(index);
                            }
                            else { }
                            break;
                        }
                    default: break;
                }
                btnEdit.Enabled = true;
                btnSave.Enabled = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                groupBox1.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                if (listViewItem.SelectedItems != null && listViewItem.SelectedItems.Count > 0){
                    int seIndex = listViewItem.SelectedItems[0].Index;
                    InitListView();
                    listViewItem.Items[seIndex].Selected = true;
                    listViewItem_SelectedIndexChanged(null, EventArgs.Empty);
                }
            }
        }

        private void cmbObjectSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbObjectSelect.SelectedIndex < 0) return;
            int index=cmbObjectSelect.SelectedIndex;
            switch(InspectType){
                case En_InspectType.找线:
                    {
                        if (InspectImageSetting.InspectLinePara.Elements.Count> index)
                        ElementsBar.Value=(int)InspectImageSetting.InspectLinePara.Elements[index];
                        DetectHeightBar.Minimum = 0;
                        DetectHeightBar.Maximum = 100;
                        LineCol1 = InspectImageSetting.InspectLinePara.Col1s[index];
                        LineCol2=InspectImageSetting.InspectLinePara.Col2s[index];
                        LineRow1=InspectImageSetting.InspectLinePara.Row1s[index];
                        LineRow2=InspectImageSetting.InspectLinePara.Row2s[index];

                        DetectHeightBar.TickFrequency = 10;
                        if (InspectImageSetting.InspectLinePara.DetectHeights.Count> index)
                        DetectHeightBar.Value = (int)InspectImageSetting.InspectLinePara.DetectHeights[index];
                        ThresholdBar.Maximum = 20;
                        ThresholdBar.Minimum = 0;
                        ThresholdBar.TickFrequency = 2;
                        if (InspectImageSetting.InspectLinePara.DetectWidths.Count> index)
                            ThresholdBar.Value = (int)InspectImageSetting.InspectLinePara.DetectWidths[index];
                        if (InspectImageSetting.InspectLinePara.Thresholds.Count > index)
                            BarAddR.Value = (int)InspectImageSetting.InspectLinePara.Thresholds[index];
                        break;
                    }
                case En_InspectType.找圆:
                    {
                        if (InspectImageSetting.InspectCirclePara.Elements.Count > index) {
                            ElementsBar.Value = (int)InspectImageSetting.InspectCirclePara.Elements[index];
                        }
                        else {
                            ElementsBar.Value = 0;
                        }
                        DetectHeightBar.Minimum = 0;
                        DetectHeightBar.Maximum = 20;
                        DetectHeightBar.TickFrequency =2;
                        if (InspectImageSetting.InspectCirclePara.DetectHeights.Count > index)
                        DetectHeightBar.Value = (int)InspectImageSetting.InspectCirclePara.DetectHeights[index];
                        ThresholdBar.Maximum = 20;
                        ThresholdBar.Minimum = 0;
                        ThresholdBar.TickFrequency = 2;
                        if (InspectImageSetting.InspectCirclePara.DetectWidths.Count > index)
                        ThresholdBar.Value = (int)InspectImageSetting.InspectCirclePara.DetectWidths[index];
                        if (InspectImageSetting.InspectCirclePara.Thresholds.Count > index)
                            BarAddR.Value = (int)InspectImageSetting.InspectCirclePara.Thresholds[index];
                         break;
                    }
                case En_InspectType.找矩形:
                    {
                        if (InspectImageSetting.InspectRectPara.DivGrays.Count > index)
                        ElementsBar.Value = (int)InspectImageSetting.InspectRectPara.DivGrays[index];
                        DetectHeightBar.Minimum = 0;
                        DetectHeightBar.Maximum = 255;
                        DetectHeightBar.TickFrequency = 25;
                        if (InspectImageSetting.InspectRectPara.GrayRanges.Count> index)
                        DetectHeightBar.Value = (int)InspectImageSetting.InspectRectPara.GrayRanges[index];
                        if(InspectImageSetting.InspectRectPara.DarkGrayRanges.Count>index)
                        BarAddR.Value = (int)InspectImageSetting.InspectRectPara.DarkGrayRanges[index];
                        ThresholdBar.Maximum = 2000;
                        ThresholdBar.Minimum = 0;
                        ThresholdBar.TickFrequency = 200;
                        if (InspectImageSetting.InspectRectPara.NgAreas.Count > index)
                        ThresholdBar.Value = (int)InspectImageSetting.InspectRectPara.NgAreas[index];
                        RectRow = InspectImageSetting.InspectRectPara.Rows[index];
                        RectCol = InspectImageSetting.InspectRectPara.Cols[index];
                        RectPhi = InspectImageSetting.InspectRectPara.Phis[index];
                        RectHeight = InspectImageSetting.InspectRectPara.Heights[index];
                        RectWidth = InspectImageSetting.InspectRectPara.Widths[index];
                        RectMeanGray = InspectImageSetting.InspectRectPara.MeanGrays[index];
                        break;
                    }
                default: break;
            }
        }

        private void StartTeachBtn1_Click(object sender, EventArgs e)
        {
            StartTeachBtn1.Enabled = false;
            switch (InspectType) {
                case En_InspectType.找线:
                    {
                        DrawLineRoi();      
                        break;
                    }
                case En_InspectType.找圆:
                    {
                        DrawCircleRoi();
                        break;
                    }
                case En_InspectType.找矩形:
                    {
                        DrawRectangleRoi();
                        break;
                    }
                default: break;
            }
        }

        private  void DrawLineRoi()
        {
            if (!GrabedImg.IsInitialized()){
                MessageBox.Show("请先加载图片");
                return;
            }
            if (this.StartTeachBtn1.Text == "开始示教") {
                this.StartTeachBtn1.Text = "保存";
                this.EnableGroupbox(groupBox3, false);
                this.StartTeachBtn1.Enabled = true;
                this.myView.ResetWindow();
                this.myView.AddImage(this.GrabedImg);
                HObject RoiContour = new HObject();
                this.ActionIsDrawingRoi(true);
                this.myView.roiController.reset();
                this.myView.SetString(12, 12, "red", "点击显示窗口的位置，绘制直线");
                this.myView.roiController.setROIShape(new ViewROI.ROILine());
            }
            else if(StartTeachBtn1.Text == "保存"){
                this.EnableGroupbox(groupBox3, true);
                ViewROI.ROILine myline = new ViewROI.ROILine();
                if (myView.roiController.getActiveROI() is ViewROI.ROILine) {
                    StartTeachBtn1.Text = "开始示教";
                    groupBox3.Enabled = true;
                    myline = (ViewROI.ROILine) myView.roiController.getActiveROI();
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
                    Thread TeachThd = new Thread(DrawLinesProcess);
                    TeachThd.IsBackground = true;
                    TeachThd.Start();
                    StartTeachBtn1.Enabled = false;
                }
                else{
                    MessageBox.Show("请先选中示教直线");
                }
            }
        }

        private void DrawLinesProcess()
        {
            ShowRoiFlag = true;
            HObject RoiContour = new HObject();
            HObject DetectRegions =new HObject();
            HTuple ResultRows = new HTuple(), ResultCols = new HTuple();
            #region  //直线参数示教
            //2.1画出直线的检测区域
            bool CurIsOk = true;
            this.myView.AddViewImage(GrabedImg);
            while (ShowRoiFlag) {
                try  {
                    //2.2显示直线的检测ROI,调整ROI的参数
                    Thread.Sleep(10);
                    myView.ResetWindow();                 
                    myView.AddImage(GrabedImg);
                    CurIsOk = MyVisionBase.gen_rake_ROI1(GrabedImg, out DetectRegions, out RoiContour, 
                        Element, DetectHeight, Threshold, LineRow1, LineCol1, LineRow2, LineCol2);
                    myView.Repaint();
                    Thread.Sleep(100);
                    myView.SetDraw("blue", "margin");
                    myView.AddViewObject(RoiContour);
                    myView.Repaint();
                    Thread.Sleep(300);
                    //2.5显示测量出每个示教小ROI的平均灰度，并保存到ListGrays变量中
                    HTuple MeanGrays = new HTuple(), GrayDivs = new HTuple();
                    HOperatorSet.Intensity(DetectRegions, GrabedImg, out MeanGrays, out GrayDivs);
                    MyVisionBase.HTupleToList(MeanGrays, out ListGrays);
                }
                catch(Exception e0) {
                    MessageBox.Show(e0.Message + e0.Source);
                    StartTeachBtn1.Enabled = true;
                    break;
                }
            }
            #endregion
        }

        private void DrawCircleRoi()
        {
            try{
                if (StartTeachBtn1.Text == "开始示教") {
                    this.StartTeachBtn1.Text = "保存";
                    this.EnableGroupbox(groupBox3, false);
                    this.StartTeachBtn1.Enabled = true;
                    this.myView.Refresh();
                    this.myView.AddImage(GrabedImg);
                    ActionIsDrawingRoi(true);
                    this.myView.roiController.reset();
                    this.myView.roiController.setROIShape(new ViewROI.ROICircularArc());
                }
                else if (StartTeachBtn1.Text == "保存"){                   
                    ViewROI.ROICircularArc myCircleArc = new ViewROI.ROICircularArc();
                    if (myView.roiController.getActiveROI() is ViewROI.ROICircularArc){
                        this.EnableGroupbox(groupBox3, true);
                        StartTeachBtn1.Text = "开始示教";
                        groupBox3.Enabled = true;
                        myCircleArc = (ViewROI.ROICircularArc)myView.roiController.getActiveROI();
                        double CenterRow, CenterCol, CirR, StartPhi, EndPhi;
                        myCircleArc.GetCircleArc(out CenterRow, out CenterCol, out CirR, out StartPhi, out EndPhi);
                        HTuple ListRow = new HTuple(), ListCol = new HTuple();
                        MyVisionBase.GenCirclePts2(CenterRow, CenterCol, CirR, StartPhi, EndPhi, "positive", out DrawRows, out DrawCols);
                        ActionIsDrawingRoi(false);
                        Thread TeachThd = new Thread(DrawCircleProcess);
                        TeachThd.IsBackground = true;
                        TeachThd.Start();
                    }
                    else {
                        MessageBox.Show("请选中示教的圆弧，选中后，圆心的小矩形会变红");
                    }
                }
            }
            catch (Exception e0) {
                Logger.PopError(e0.Message, true);
            }
        }

        public void DrawCircleProcess()
        {
            ShowRoiFlag = true;
            HObject RoiContour = new HObject();
            #region //圆参数示教
            HObject DetectRegions = new HObject();
            HTuple RegionsMean = new HTuple();
            HTuple RegionsDiv = new HTuple();
            HTuple PointOrder = new HTuple();        
            while (ShowRoiFlag) {
                MyVisionBase.gen_spoke_ROI1(GrabedImg, out DetectRegions, out RoiContour, Element,
                    DetectHeight, Threshold, DrawRows, DrawCols, "inner", 0);
                HOperatorSet.Intensity(DetectRegions, GrabedImg, out RegionsMean, out RegionsDiv);
                MyVisionBase.HTupleToList(RegionsMean, out ListGrays);
                this.myView.roiController.reset();
                this.myView.ResetWindow();
                this.myView.AddImage(GrabedImg);
                this.myView.Repaint();
                Thread.Sleep(100);
                this.myView.SetDraw("green");
                this.myView.AddViewObject(RoiContour);
                this.myView.Repaint();
                Thread.Sleep(500);             
            }
            #endregion
        }

        private double rectRow, rectCol, rectPhi, rectHei, rectWidth;

        private void DrawRectangleRoi()
        {
            if (StartTeachBtn1.Text == "开始示教") {
                this.StartTeachBtn1.Text = "保存";
                this.EnableGroupbox(groupBox3, false);
                this.StartTeachBtn1.Enabled = true;
                this.myView.ResetView();
                this.myView.AddViewImage(GrabedImg);
                this.myView.SetString(12, 12, "red", "1.点击显示窗口，拖动鼠标确定矩形的宽，点击右键确认");
                this.ActionIsDrawingRoi(true);
                this.myView.roiController.setROIShape(new ViewROI.ROIRectangle2());
                this.ActionIsDrawingRoi(false);
            }
            else if (StartTeachBtn1.Text == "保存") {
                if (this.myView.roiController.getActiveROI() is ViewROI.ROIRectangle2) {
                    this.StartTeachBtn1.Text = "开始示教";
                    ViewROI.ROIRectangle2 myRect2 = (ViewROI.ROIRectangle2)this.myView.roiController.getActiveROI();
                    myRect2.GetRect2(out rectRow, out rectCol, out rectPhi, out rectWidth, out rectHei);
                    this.EnableGroupbox(groupBox3, true);
                    Thread TeachThd = new Thread(DrawRectProcess);
                    TeachThd.IsBackground = true;
                    TeachThd.Start();
                }
            }
        }

        public void DrawRectProcess()
        {
            ShowRoiFlag = true;
            HObject RectRegion =new HObject();
            HTuple RectDiv =new HTuple();
            HObject RectImg =new HObject();
            HObject CurBadRegion =new HObject();
            HObject CurConnectRegion = new HObject();
            HObject CurSelectRegion = new HObject();
            while (ShowRoiFlag){
                try {
                    HOperatorSet.GenRectangle2(out  RectRegion, rectRow, rectCol, rectPhi, rectWidth, rectHei);
                    HOperatorSet.Intensity(RectRegion, GrabedImg, out RectMeanGray, out RectDiv);
                    St_InspectRectanglePara RectInspPara = new St_InspectRectanglePara(1,true);
                    RectInspPara.Rows[0] = rectRow;
                    RectInspPara.Cols[0] = rectCol;
                    RectInspPara.Phis[0] = rectPhi;
                    RectInspPara.Widths[0] = rectWidth;
                    RectInspPara.Heights[0] = rectHei;
                    RectInspPara.MeanGrays[0] = RectMeanGray.D;
                    RectInspPara.GrayRanges[0] = DetectHeight;
                    double DarkThd = AddR;
                    RectInspPara.DarkGrayRanges[0] = DarkThd;
                    MyVisionBase.DetetctRectangles(GrabedImg, RectInspPara,out CurSelectRegion,out RectRegion);
                    //HOperatorSet.ReduceDomain(GrabedImg, RectRegion, out RectImg);
                    //double thershold=RectMeanGray.D + DetectHeight;
                    //thershold=thershold<=255?thershold:255;
                    //HOperatorSet.Threshold(RectImg, out  CurBadRegion, thershold, 255);
                    //HOperatorSet.Connection(CurBadRegion, out CurConnectRegion);  //区域连接
                    //if (Threshold < 10) Threshold = 10;
                    //HOperatorSet.SelectShape(CurConnectRegion, out CurSelectRegion, "area", "and", Threshold, 10000000);  //
                    this.myView.ResetWindow();
                    this.myView.AddImage(GrabedImg);
                    this.myView.SetDraw("green");
                    this.myView.AddViewObject(RectRegion);
                    this.myView.Repaint();
                    Thread.Sleep(100);
                    this.myView.SetDraw("red");
                    this.myView.AddViewObject(CurSelectRegion);
                    this.myView.Repaint();
                    Thread.Sleep(200);
                }
                catch(Exception  e0) {
                    MessageBox.Show(e0.Message + e0.Source);
                    //StartTeachBtn1.Enabled = true;
                    break;
                }
            }       
        }

        private void StopTeachBtn_Click(object sender, EventArgs e){
            StartTeachBtn1.Enabled = true;
            ShowRoiFlag = false;
            SaveParaBtn.Enabled = true;
        }

        private void SaveParaBtn_Click(object sender, EventArgs e){
            SaveParaBtn.Enabled = false;
            switch (InspectType){
                case En_InspectType.找线:
                    {
                        SaveLineRoi();
                        break;
                    }
                case En_InspectType.找圆:
                    {
                        SaveCircleRoi();
                        break;
                    }
                case En_InspectType.找矩形:
                    {
                        SaveRectangeRoi();
                        break;
                    }
                default: break;
            }
        }

        private void SaveLineRoi()
        {
            int index = cmbObjectSelect.SelectedIndex;
            InspectImageSetting.InspectLinePara.Elements[index] = ElementsBar.Value;
            InspectImageSetting.InspectLinePara.DetectHeights[index] = DetectHeightBar.Value;
            InspectImageSetting.InspectLinePara.DetectWidths[index] = ThresholdBar.Value;
            InspectImageSetting.InspectLinePara.Col1s[index] = LineCol1;
            InspectImageSetting.InspectLinePara.Col2s[index] = LineCol2;
            InspectImageSetting.InspectLinePara.Row1s[index] = LineRow1;
            InspectImageSetting.InspectLinePara.Row2s[index] = LineRow2;
            if (InspectImageSetting.InspectLinePara.Thresholds.Count > index)
                InspectImageSetting.InspectLinePara.Thresholds[index] = BarAddR.Value;
            InspectImageSetting.InspectLinePara.DetectRegionGrays[index] = ListGrays;
        }

        private void SaveCircleRoi()
        {
            int index = cmbObjectSelect.SelectedIndex;
            InspectImageSetting.InspectCirclePara.Elements[index] = ElementsBar.Value;
            InspectImageSetting.InspectCirclePara.DetectHeights[index] = DetectHeightBar.Value;
            InspectImageSetting.InspectCirclePara.DetectWidths[index] = ThresholdBar.Value;
            InspectImageSetting.InspectCirclePara.DetectRegionGrays[index] = ListGrays;
            InspectImageSetting.InspectCirclePara.AddRs[index] = AddR;
            List<double> ListRow =new List<double>(),ListCol =new List<double>();
            MyVisionBase.HTupleToList(DrawRows,out ListRow);
            MyVisionBase.HTupleToList(DrawCols,out ListCol);
            InspectImageSetting.InspectCirclePara.ListRows[index] = ListRow;
            InspectImageSetting.InspectCirclePara.ListCols[index] = ListCol;
            InspectImageSetting.InspectCirclePara.Thresholds[index] =(int) BarAddR.Value;
        }
        private void SaveRectangeRoi()
        {
            int index = cmbObjectSelect.SelectedIndex;
            InspectImageSetting.InspectRectPara.DivGrays[index] = ElementsBar.Value;
            InspectImageSetting.InspectRectPara.GrayRanges[index] = DetectHeightBar.Value;
            InspectImageSetting.InspectRectPara.NgAreas[index] = ThresholdBar.Value;
            InspectImageSetting.InspectRectPara.Rows[index] = rectRow;
            InspectImageSetting.InspectRectPara.Cols[index] = rectCol;
            InspectImageSetting.InspectRectPara.Phis[index] = rectPhi;
            InspectImageSetting.InspectRectPara.Widths[index] = rectWidth;
            InspectImageSetting.InspectRectPara.Heights[index] = rectHei;
            InspectImageSetting.InspectRectPara.MeanGrays[index] = RectMeanGray.D;
            if (InspectImageSetting.InspectRectPara.DarkGrayRanges == null) 
                InspectImageSetting.InspectRectPara.DarkGrayRanges = new List<double>();
            if (InspectImageSetting.InspectRectPara.DarkGrayRanges.Count > index)
                InspectImageSetting.InspectRectPara.DarkGrayRanges[index] = BarAddR.Value;
            else InspectImageSetting.InspectRectPara.DarkGrayRanges.Add(BarAddR.Value);
        }

        private void btnRecheckTest_Click(object sender, EventArgs e)
        {
            btnRecheckTest.Enabled = false;
            if (!GrabedImg.IsInitialized()){
                MessageBox.Show("请先加载图片");          
                return; }
            Task.Factory.StartNew(new Action(() => {
                HObject DetectContours = new HObject(), BadRegions = new HObject();
                bool IsOK = true;
                St_InspectImageSetting tmpInspectParam = new St_InspectImageSetting(true);
                tmpInspectParam = InspectImageSetting;
                LocalManager MyLocalCtrl = new LocalManager();
                MyLocalCtrl.SetLocalModel(TeachLocalPara.localSetting.localModel);
                this.TeachLocalPara.LineCirRectInspParam = InspectImageSetting;
                MyLocalCtrl.SetParam(GrabedImg, TeachLocalPara);
                MyLocalCtrl.doLocal();
                LineCircRectRlt MyLineCircRectInspRlt = new LineCircRectRlt();
                MyLineCircRectInspRlt = (LineCircRectRlt)MyLocalCtrl.GetResult();
                this.myView.roiController.reset();
                this.myView.ResetWindow();
                this.myView.AddImage(GrabedImg);
                Thread.Sleep(200);
                this.myView.SetDraw("green");
                this.myView.AddViewObject(MyLineCircRectInspRlt.DetectContour);
                this.myView.AddViewObject(MyLineCircRectInspRlt.ShowContour);
                this.myView.SetDraw("red");
                this.myView.AddViewObject(MyLineCircRectInspRlt.NgContour);
                this.myView.Repaint();
                if (MyLineCircRectInspRlt.IsOk) this.myView.SetString(20, 50, "green", "复检OK");
                else this.myView.SetString(100, 100, "Red", "复检NG");
            }));

            Thread.Sleep(500);
            btnRecheckTest.Enabled = true;
        }

        private void ElementsBar_ValueChanged(object sender, EventArgs e)
        {
            ElementsTxt.Text = ElementsBar.Value.ToString();
            Element = ElementsBar.Value;
        }

        private void DetectHeightBar_ValueChanged(object sender, EventArgs e)
        {
            DetectHeightTxt.Text = DetectHeightBar.Value.ToString();
            DetectHeight = DetectHeightBar.Value;
        }

        private void ThresholdBar_ValueChanged(object sender, EventArgs e)
        {
            ThresholdTxt.Text = ThresholdBar.Value.ToString();
            Threshold = ThresholdBar.Value;
        }

        private void btnItemTest_Click(object sender, EventArgs e)
        {
            HObject BadRegions = new HObject();
            HObject DetectContours = new HObject();
            bool IsOk = true;
            this.myView.ResetWindow();
            this.myView.AddViewImage(GrabedImg);
            switch (InspectType)
            {
                case En_InspectType.找线:
                    {
                        IsOk = MyVisionBase.DectectLines(GrabedImg, InspectImageSetting.InspectLinePara, 
                            out BadRegions, out DetectContours);             
                        break;
                    }
                case En_InspectType.找圆:
                    {
                        IsOk = MyVisionBase.DetectCircles(GrabedImg, InspectImageSetting.InspectCirclePara,
                            out BadRegions, out DetectContours);
                        break;
                    }
                case En_InspectType.找矩形:
                    {
                        IsOk = MyVisionBase.DetetctRectangles(GrabedImg, InspectImageSetting.InspectRectPara,
                            out BadRegions, out DetectContours);
                        break;
                    }
                default: break;
            }
            this.myView.SetDraw("green");
            this.myView.AddViewObject(DetectContours);
            this.myView.SetDraw("red");
            this.myView.AddViewObject(BadRegions);
            Thread.Sleep(300);
            this.myView.Repaint();
        }

        private void EnableGroupbox(GroupBox groupBoxIn, bool IsEnable)
        {
            foreach (Control item in groupBoxIn.Controls){
                item.Enabled = IsEnable;
            }
        }
        private void BarAddR_ValueChanged(object sender, EventArgs e)
        {
            AddRtxt.Text = BarAddR.Value.ToString();
            AddR = BarAddR.Value;
            ThreeThreshold = BarAddR.Value;
        }



    }
}
