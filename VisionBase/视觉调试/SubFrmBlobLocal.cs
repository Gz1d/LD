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
    public partial class SubFrmBlobLocal : Form
    {
        public int CCDIndex = 0;

        public ViewControl view1;
        public string CameraName = "";
        public St_BlobLocalParam BlobInspectPara { get { return TeachBlobLocaltPara; } }
        public St_VectorAngle TeachVectorAngle = new St_VectorAngle();
        public St_VectorAngle TempFindVectorAngle = new St_VectorAngle();
        private St_BlobLocalParam TeachBlobLocaltPara = new St_BlobLocalParam(2,true);
        public bool ShowRoiFlag = false;
        private HObject GrabedImg = new HObject();
        private RectangleF RectF = new RectangleF(0, 0, 100, 100);
        private int MinGray = 50;
        private int MaxGray = 200;
        private int AreaMin = 100;
        private int AreaMax = 2000;
        public MyHomMat2D CurHandEyeMat = new MyHomMat2D(true);
        private Action<bool> ActionIsDrawingRoi;
        private LocalPara TeachLoclPara;

        public SubFrmBlobLocal( St_BlobLocalParam BlobParaIn, ViewControl viewIn, HObject ImgIn, Action<bool> drawRoiInHWindow)
        {
            InitializeComponent();
            TeachBlobLocaltPara = BlobParaIn;
            view1 = viewIn;
            ActionIsDrawingRoi = drawRoiInHWindow;
            if (ImgIn != null && ImgIn.IsInitialized())
            {
                HOperatorSet.CopyImage(ImgIn, out GrabedImg);
            }
            PixelSizeNumUpDn.Value = (decimal)TeachBlobLocaltPara.PixelSize;
        }
        public void UpDatePara(LocalPara ParaIn)
        {
            TeachLoclPara = ParaIn;
        }


        private void FrmSubFrmFindLine_Load(object sender, EventArgs e)
        {
            InitCombox();
        }

        private void InitCombox()
        {
            if (TeachBlobLocaltPara.Count == 0) TeachBlobLocaltPara = new St_BlobLocalParam(2, true);
            int count = TeachBlobLocaltPara.Count;           
            LineSelectComBox.Items.Clear();
            if (count == 1)
            {
                LineSelectComBox.Items.Add("第1个定位区域");
            }
            else if (count == 2)
            {
                LineSelectComBox.Items.Add("第1个定位区域");
                LineSelectComBox.Items.Add("第2个定位区域");
            }
            else if (count == 3)
            {
                LineSelectComBox.Items.Add("第1个定位区域");
                LineSelectComBox.Items.Add("第2个定位区域");
                LineSelectComBox.Items.Add("第3个定位区域");
            }
            else if (count == 4)
            {
                LineSelectComBox.Items.Add("第1个定位区域");
                LineSelectComBox.Items.Add("第2个定位区域");
                LineSelectComBox.Items.Add("第3个定位区域");
                LineSelectComBox.Items.Add("第4个定位区域");
            }
            else if (count == 5)
            {
                LineSelectComBox.Items.Add("第1个定位区域");
                LineSelectComBox.Items.Add("第2个定位区域");
                LineSelectComBox.Items.Add("第3个定位区域");
                LineSelectComBox.Items.Add("第4个定位区域");
                LineSelectComBox.Items.Add("第5个定位区域");
            }
            else if (count == 6)
            {
                LineSelectComBox.Items.Add("第1个检测区域");
                LineSelectComBox.Items.Add("第2个检测区域");
                LineSelectComBox.Items.Add("第3个检测区域");
                LineSelectComBox.Items.Add("第4个检测区域");
                LineSelectComBox.Items.Add("第5个检测区域");
                LineSelectComBox.Items.Add("第6个检测区域");
            }
            else if (count == 7)
            {
                LineSelectComBox.Items.Add("第1个检测区域");
                LineSelectComBox.Items.Add("第2个检测区域");
                LineSelectComBox.Items.Add("第3个检测区域");
                LineSelectComBox.Items.Add("第4个检测区域");
                LineSelectComBox.Items.Add("第5个检测区域");
                LineSelectComBox.Items.Add("第6个检测区域");
                LineSelectComBox.Items.Add("第7个检测区域");
            }
            else if (count == 8)
            {
                LineSelectComBox.Items.Add("第1个检测区域");
                LineSelectComBox.Items.Add("第2个检测区域");
                LineSelectComBox.Items.Add("第3个检测区域");
                LineSelectComBox.Items.Add("第4个检测区域");
                LineSelectComBox.Items.Add("第5个检测区域");
                LineSelectComBox.Items.Add("第6个检测区域");
                LineSelectComBox.Items.Add("第7个检测区域");
                LineSelectComBox.Items.Add("第8个检测区域");
            }
            else return;
            LineSelectComBox.SelectedIndex = 0;
        }

    public void UpdateCurrImage(HObject img, St_VectorAngle TempFindVectorAngleIn,LocalPara VisionParaIn)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
            HOperatorSet.CopyImage(img, out GrabedImg);
            TempFindVectorAngle = TempFindVectorAngleIn;
            TeachLoclPara = VisionParaIn;           
        }

        /// <summary>
        /// 拟合参数的示教函数
        /// </summary>
        public void TeachProcess()
        {
            ShowRoiFlag = true;
            HObject RoiContour = new HObject();
            HObject Roi = new HObject();
            HObject RectContour = new HObject();
            HObject ReduceImg = new HObject();
            HObject ThresholdRegion = new HObject();
            HObject ConnectRegion = new HObject();
            HObject SelectRegion = new HObject();
            HTuple Area = new HTuple(), Row =new HTuple(), Col = new HTuple();
            HObject RegionContour = new HObject();
            HTuple RectRow1 = new HTuple(), RectRow2 = new HTuple(), RectCol1 = new HTuple(), RectCol2 = new HTuple();
            HObject MinRectContour = new HObject();
            while (ShowRoiFlag)
            {
                try
                {
                    #region                
                    view1.AddImage(GrabedImg);
                    Thread.Sleep(50);
                    HOperatorSet.GenRectangle1(out Roi, RectF.Y, RectF.X, RectF.Y + RectF.Height, RectF.X + RectF.Width);//生成检测区域
                    HOperatorSet.GenContourRegionXld(Roi, out RectContour, "border");
                    view1.SetDraw("green");
                    view1.AddViewObject(RectContour);
                    view1.Repaint();
                    System.Threading.Thread.Sleep(100);
                    HOperatorSet.ReduceDomain(GrabedImg, Roi, out ReduceImg);//裁剪图像
                    Roi.Dispose();
                    HOperatorSet.Threshold(ReduceImg, out ThresholdRegion, MinGray, MaxGray);//阈值分割
                    ReduceImg.Dispose();
                    HOperatorSet.Connection(ThresholdRegion, out ConnectRegion); //区域连接
                    ThresholdRegion.Dispose();
                    HOperatorSet.SelectShape(ConnectRegion, out SelectRegion, "area", "and", AreaMin, AreaMax);
                    ConnectRegion.Dispose();
                    HOperatorSet.AreaCenter(SelectRegion, out Area, out Row, out Col);
                    HOperatorSet.GenContourRegionXld(SelectRegion, out RegionContour, "border");
                    view1.SetDraw("red");
                    view1.AddViewObject(RegionContour);
                    // HOperatorSet.SmallestRectangle2(SelectRegion,out Row ,out Col,out P)
                    HOperatorSet.SmallestRectangle1(SelectRegion, out RectRow1, out RectCol1, out RectRow2, out RectCol2);
                    HObject RectRegion = new HObject();
                    HOperatorSet.GenRectangle1(out RectRegion, RectRow1, RectCol1, RectRow2, RectCol2);
                    HOperatorSet.GenContourRegionXld(RectRegion, out MinRectContour, "border");               
                    view1.AddViewObject(MinRectContour);
                    view1.Repaint();
                    System.Threading.Thread.Sleep(500);
                    #endregion
                }
                catch
                { }
            }
            Thread.Sleep(500);
        }
        private void FindLineBtn_Click(object sender, EventArgs e)
        {
            txtFindTime.Clear();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            txtFindTime.Text = sw.ElapsedMilliseconds.ToString();
        }
        private void MinGrayBar_Scroll(object sender, EventArgs e)
        {
            MinGrayTxt.Text = MinGrayBar.Value.ToString();
            MinGray = MinGrayBar.Value;
        }
        private void MaxGrayBar_Scroll(object sender, EventArgs e)
        {
            MaxGrayTxt.Text = MaxGrayBar.Value.ToString();
            MaxGray = MaxGrayBar.Value;
        }

        private void AreaMinBar_Scroll(object sender, EventArgs e)
        {
            AreaMinTxt.Text = AreaMinBar.Value.ToString();
            AreaMin = AreaMinBar.Value;
        }

        private void AreaMaxBar_Scroll(object sender, EventArgs e)
        {
            AreaMaxTxt.Text = AreaMaxBar.Value.ToString();
            AreaMax = AreaMaxBar.Value;
        }

        private void StartTeachBtn1_Click(object sender, EventArgs e)
        {
            StartTeachBtn1.Enabled = false;
            view1.AddViewImage(GrabedImg);
            if (!GrabedImg.IsInitialized())
            {
                MessageBox.Show("图片为空，请先加载一张图片。");
                return;
            }
            Thread TeachThd = new Thread(TeachProcess);
            TeachThd.IsBackground = true;
            TeachThd.Start();
        }

        private void SaveParaBtn_Click(object sender, EventArgs e)
        {
            TeachBlobLocaltPara.InspectRois[LineSelectComBox.SelectedIndex] =  RectF ;
            TeachBlobLocaltPara.MinGrays[LineSelectComBox.SelectedIndex] = MinGray;
            TeachBlobLocaltPara.MaxGrays[LineSelectComBox.SelectedIndex] = MaxGray;
            TeachBlobLocaltPara.AreaMins[LineSelectComBox.SelectedIndex] = AreaMin;
            TeachBlobLocaltPara.AreaMaxs[LineSelectComBox.SelectedIndex] = AreaMax;
            SaveParaBtn.Enabled = false;
        }

        private void StopTeachBtn_Click(object sender, EventArgs e)
        {
            StartTeachBtn1.Enabled = true;
            ShowRoiFlag = false;
            SaveParaBtn.Enabled = true;
        }


        private void LineSelectComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TeachBlobLocaltPara.InspectRois.Count == 0) TeachLoclPara.Blobs = new St_BlobLocalParam(2, true);
            RectF = TeachBlobLocaltPara.InspectRois[LineSelectComBox.SelectedIndex];
            MinGray = TeachBlobLocaltPara.MinGrays[LineSelectComBox.SelectedIndex];
            MaxGray = TeachBlobLocaltPara.MaxGrays[LineSelectComBox.SelectedIndex];
            AreaMin = TeachBlobLocaltPara.AreaMins[LineSelectComBox.SelectedIndex];
            AreaMax = TeachBlobLocaltPara.AreaMaxs[LineSelectComBox.SelectedIndex];
            UpdataFrameTxt();
        }

        private void TryDebugBtn_Click(object sender, EventArgs e)
        {
            txtFindTime.Clear();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TeachLoclPara.Blobs = TeachBlobLocaltPara;
            LocalManager LocalCtrl = new LocalManager();
            LocalCtrl.SetLocalModel(TeachLoclPara.localSetting.localModel);
            LocalCtrl.SetParam(GrabedImg, TeachLoclPara);
            LocalCtrl.doLocal();
            sw.Stop();
            LocalResult MyRlt = new LocalResult();
            MyRlt = LocalCtrl.GetResult();
            BlobLocalRlt BlobRlt = (BlobLocalRlt)MyRlt;
            if (GrabedImg != null && GrabedImg.IsInitialized())
            {
                view1.ResetView();
                view1.AddViewImage(GrabedImg);
                view1.SetDraw("blue");
                view1.AddViewObject(MyRlt.ShowContour);
                view1.Repaint();
            }
            if (BlobRlt.ListWid != null && BlobRlt.ListWid.Count > 0)
            {
                double X = BlobRlt.ListCol[0] * TeachLoclPara.Blobs.PixelSize;
                double H = BlobRlt.ListRow[0] * TeachLoclPara.Blobs.PixelSize;
                view1.SetString(100, 150, "red", "坐标X（mm）：" + X.ToString() + "     Y（mm）：" + H.ToString());
                if (BlobRlt.ListWid.Count > 1)
                {
                    X = BlobRlt.ListWid[1] * TeachLoclPara.Blobs.PixelSize;
                    H = BlobRlt.ListHei[1] * TeachLoclPara.Blobs.PixelSize;
                    view1.SetString(100, 750, "blue", "高度（mm）：" + H.ToString());
                }           
            }
            txtFindTime.Text = sw.ElapsedMilliseconds.ToString();
        }

        private void ClearParaBtn_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("真的要清除参数？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt != DialogResult.Yes) return;
            int initLineCount = 1;
            rlt = MessageBox.Show("即将示教3个定位区域？", "区域数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.Yes) initLineCount = 3;
            if (initLineCount != 3)
            {
                rlt = MessageBox.Show("即将示教2个定位区域？", "区域数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rlt == DialogResult.Yes) initLineCount = 2;
                else
                {
                    rlt = MessageBox.Show("即将示教1个定位区域？", "区域数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rlt == DialogResult.Yes) initLineCount = 1;
                    else
                    {
                        rlt = MessageBox.Show("即将示教4个定位区域？", "区域数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rlt == DialogResult.Yes) initLineCount = 4;
                        else
                        {
                            rlt = MessageBox.Show("即将示教5个定位区域？", "区域数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (rlt == DialogResult.Yes) initLineCount = 5;
                            else
                            {
                                rlt = MessageBox.Show("即将示教6个定位区域？", "区域数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (rlt == DialogResult.Yes) initLineCount = 6;
                                else
                                {
                                    rlt = MessageBox.Show("即将示教7个定位区域？", "区域数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (rlt == DialogResult.Yes) initLineCount = 7;
                                    else
                                    {
                                        rlt = MessageBox.Show("即将示教8个定位区域？", "区域数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (rlt == DialogResult.Yes) initLineCount = 8;
                                        else return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            TeachBlobLocaltPara = new St_BlobLocalParam(initLineCount);
            InitCombox();
        }

        private void SubFrmFindLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GrabedImg != null) GrabedImg.Dispose();
        }

        ViewROI.ROI SerachRoi = new ViewROI.ROI();
        ViewROI.ROIRectangle1 SerachRect1 = new ViewROI.ROIRectangle1();
        private void btnSaveSerach_Click(object sender, EventArgs e)
        {
            

            if (btnSaveSerach.Text == "新建")
            {
                btnSaveSerach.Text = "保存";

                if (GrabedImg == null || !GrabedImg.IsInitialized())
                {
                    MessageBox.Show("请先加载一张图片");
                    return;
                }
                ActionIsDrawingRoi(true);

                btnSaveSerach.Enabled = true;
                groupBox2.Enabled = true;
                view1.roiController.reset();
                view1.roiController.resetROI();
                view1.roiController.setROIShape(new ViewROI.ROIRectangle1(100));
            }
            else if (btnSaveSerach.Text == "保存")
            {
                SerachRoi = view1.roiController.getActiveROI();
                if (SerachRoi is ViewROI.ROIRectangle1)
                {
                    btnSaveSerach.Text = "新建";
                    SerachRect1 = (ViewROI.ROIRectangle1)SerachRoi;
                    double row11, col11, row21, col21;
                    SerachRect1.GetRect1(out row11, out col11, out row21, out col21);
                    //ActionIsDrawingRoi(false);
                    DialogResult rlt1 = MessageBox.Show("创建搜索框成功，是否替换原有参数？", "参数覆盖",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rlt1 != DialogResult.Yes) return;
                    RectF.X = (int)col11;
                    RectF.Y = (int)row11;
                    RectF.Width = (int)(col21 - col11);
                    RectF.Height = (int)(row21 - row11);

                    this.BeginInvoke(new Action(() =>
                    {
                        txtSearchX.Text = RectF.X.ToString();
                        txtSearchY.Text = RectF.Y.ToString();
                        txtSearchWidth.Text = RectF.Width.ToString();
                        txtSearchHeight.Text = RectF.Height.ToString();
                        Logger.Pop("创建搜索框成功");
                    }));
                }
                else
                {
                    MessageBox.Show("请选中roi");

                }
            }
            #region
            //HTuple row1, row2, column1, column2;
            //view1.SetDraw("red");
            //ActionIsDrawingRoi(true);
            //view1.roiController.reset();
            //view1.roiController.setROIShape(new ViewROI.ROIRectangle1(100));
            //ActionIsDrawingRoi(false);
            //DialogResult rlt = MessageBox.Show("创建搜索框成功，是否替换原有参数？", "参数覆盖", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (rlt != DialogResult.Yes) return;
            //RectF.X= (int)column1.D;
            //RectF.Y = (int)row1.D;
            //RectF.Width = (int)(column2 - column1).D;
            //RectF.Height = (int)(row2 - row1).D;
            //txtSearchX.Text = RectF.X.ToString();
            //txtSearchY.Text = RectF.Y.ToString();
            //txtSearchWidth.Text = RectF.Width.ToString();
            //txtSearchHeight.Text = RectF.Height.ToString();
            //TeachBlobLocaltPara.InspectRois[LineSelectComBox.SelectedIndex] = RectF;
            #endregion
        }

        /// <summary> 刷新界面测参数 </summary>
        private void UpdataFrameTxt()
        {
            txtSearchX.Text = RectF.X.ToString();
            txtSearchY.Text = RectF.Y.ToString();
            txtSearchWidth.Text = RectF.Width.ToString();
            txtSearchHeight.Text = RectF.Height.ToString();

            MinGrayBar.Value = MinGray;
            MaxGrayBar.Value = MaxGray;
            AreaMinBar.Value = AreaMin;
            AreaMaxBar.Value = AreaMax;
           // MinGrayBar.Scroll(null, new EventArgs());
            MinGrayBar_Scroll(null, new EventArgs());
            MaxGrayBar_Scroll(null, new EventArgs());
            AreaMinBar_Scroll(null, new EventArgs());
            AreaMaxBar_Scroll(null, new EventArgs());
        }

        private void FindLineBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void PixelSizeNumUpDn_ValueChanged(object sender, EventArgs e)
        {

        }

        private void PixelSizeTeachBtn_Click(object sender, EventArgs e)
        {
            PixelSizeNumUpDn.Enabled = true;
            PiexlSizeSaveBtn.Enabled = true;
            PixelSizeTeachBtn.Enabled = false;
        }

        private void PiexlSizeSaveBtn_Click(object sender, EventArgs e)
        {
            PixelSizeNumUpDn.Enabled = false;
            PiexlSizeSaveBtn.Enabled = false;
            PixelSizeTeachBtn.Enabled = true;
            TeachBlobLocaltPara.PixelSize = (double)PixelSizeNumUpDn.Value;

        }
    }
}
