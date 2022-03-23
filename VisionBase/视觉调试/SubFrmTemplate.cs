using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using VisionBase.Matching;
using System.Threading;
using System.Threading.Tasks;


namespace VisionBase
{
    public partial class SubFrmTemplate : Form
    {
        public St_VectorAngle TempFindVecvtorAngle = new St_VectorAngle();
        public Point2Db XYOffset = new Point2Db();
        public St_TemplateParam TemplateParam;
        public LocalSettingPara Setting;
        private HObject CurrentImage;

        private Action<bool> ActionIsDrawingRoi;
        private HRegion ModelRoi;
        private string RoiStr = "Rect1";
        private bool IsDelete = false;
        private int EraserSize = 10;
        public ViewControl view1;

        public SubFrmTemplate()
        {
            InitializeComponent();
        }

        public SubFrmTemplate(St_TemplateParam param, LocalSettingPara setting, ViewControl viewIn, HObject srcImage,Action<bool> drawRoiInHWindow)
        {
            InitializeComponent();
            TemplateParam = param;
            Setting = setting;
            view1 = viewIn;
            //ShowWindow.HMouseMove += HMouseMove;
            if (CurrentImage != null) CurrentImage.Dispose();
            ActionIsDrawingRoi = drawRoiInHWindow;
            
        }

        private void FrmSubFrmTemplate_Load(object sender, EventArgs e)
        {
            cmbTempType.SelectedIndex = 0;
            cmbTempRegion.SelectedIndex = 0;
            cmbLevel.SelectedIndex = 0;
            EraserCbx.SelectedIndex = 1;
            InitUI();
        }

        public void UpdateCurrImage(HObject img)
        {
            if (img == null) return;
            if (CurrentImage != null) CurrentImage.Dispose();
            if (TemplateParam.Scale > 0.09 && TemplateParam.Scale < 2.1)
                HOperatorSet.CopyImage(img, out CurrentImage);
            else{
                MessageBox.Show("图像的缩放系数设置错误，请修改Scale参数");
                HOperatorSet.CopyImage(img, out CurrentImage);
                TemplateParam = new St_TemplateParam(true);
                TemplateParam.Scale = 1.0;
            }
            if (CurrentImage == null || !CurrentImage.IsInitialized()){
                MessageBox.Show("请先加载一张图片");
                return;
            }
            InitUI();
        }

        public void UpdateSetting(LocalSettingPara setting)
        {
            Setting = setting;
        }

        private void InitUI()
        {
            txtSearchX.Text = Setting.SearchAreaX.ToString();
            txtSearchY.Text = Setting.SearchAreaY.ToString();
            txtSearchWidth.Text = Setting.SearchWidth.ToString();
            txtSearchHeight.Text = Setting.SearchHeight.ToString();

            txtStartAngle.Text = TemplateParam.StartAngle.ToString();
            txtMaxAngle.Text = TemplateParam.EndAngle.ToString();
            txtMinScore.Text = TemplateParam.Score.ToString();
            TxtScale.Text = TemplateParam.Scale.ToString();
            cmbLevel.SelectedIndex = TemplateParam.Level;
            txtError.Text = TemplateParam.MaxError.ToString();

            txtRegionArea.Text = TemplateParam.Area.ToString();
            txtRegionLength.Text = TemplateParam.ContLength.ToString();
            txtLimitYMax.Text = TemplateParam.OffsetYMax.ToString();
            txtLimitYMin.Text = TemplateParam.OffsetYMin.ToString();
        }

        private void btnCreateTemplate_Click(object sender, EventArgs e)
        {
            if (btnCreateTemplate.Text == "新建")
            {
                btnCreateTemplate.Enabled = true;

                panelOperator.Enabled = true;
                groupBox1.Enabled = true;
                this.Enabled = true;
                btnCreateTemplate.Text = "保存";
                view1.roiController.reset();
                view1.roiController.NotifyRCObserver += UpdateViewData;
                view1.roiController.setROISign(ViewROI.ROIController.MODE_ROI_POS);
                if (CurrentImage == null){
                    Logger.PopError("请先采集图片！", true);
                    return;
                }
                view1.SetDraw("red", "margin");
                //1.0界面去使能
                this.BeginInvoke(new Action(() =>{ ActionIsDrawingRoi(true);}));
                view1.Refresh();
                view1.AddImage(CurrentImage);
                if (RoiStr.Equals("Rect1"))
                {
                    view1.SetString(12, 12, "red", "绘制矩形区域作为模板区域");
                    Thread.Sleep(10);                  
                    view1.roiController.setROIShape(new ViewROI.ROIRectangle1(200));
                }
                #region
                //else if (RoiStr.Equals("Rect2"))
                //{
                //    HOperatorSet.ClearWindow(ShowWindow);
                //    MyVisionBase.hDispObj(ShowWindow, CurrentImage);
                //    MyVisionBase.disp_message(ShowWindow, "绘制矩形区域作为模板区域", "window", 12, 12, "red", "false");
                //    HTuple CenterRow, CenterCol, Rect2Phi, RectL1, RectL2;
                //    HOperatorSet.DrawRectangle2(ShowWindow, out CenterRow, out CenterCol, out Rect2Phi, out RectL1, out RectL2);
                //    ModelRoi = new HRegion();
                //    ModelRoi.GenRectangle2(CenterRow, CenterCol, Rect2Phi, RectL1, RectL2);
                //}
                //else if (RoiStr.Equals("Circle"))
                //{
                //    MyVisionBase.disp_message(ShowWindow, "绘制圆形区域作为模板区域", "window", 12, 12, "red", "false");
                //    HTuple CircleRow, CircleCol, CircleR;
                //    HOperatorSet.DrawCircle(ShowWindow, out CircleRow, out CircleCol, out CircleR);
                //    ModelRoi = new HRegion();
                //    ModelRoi.GenCircle(CircleRow, CircleCol, CircleR);
                //}
                //else if (RoiStr.Equals("CircleMod"))
                //{
                //    HTuple CircleRowIn, CircleColIn, CircleRIn, CircleRow, CircleCol, CircleR;
                //    MyVisionBase.disp_message(ShowWindow, "绘制外圆区域作为圆环模板区域的外圆", "window", 12, 12, "red", "false");
                //    HOperatorSet.DrawCircle(ShowWindow, out CircleRowIn, out CircleColIn, out CircleRIn);
                //    HOperatorSet.ClearWindow(ShowWindow);
                //    MyVisionBase.hDispObj(ShowWindow, CurrentImage);
                //    MyVisionBase.disp_message(ShowWindow, "绘制内圆区域作为圆环模板区域的内圆", "window", 12, 12, "red", "false");
                //    HOperatorSet.DrawCircleMod(ShowWindow, CircleRowIn, CircleColIn, CircleRIn, out CircleRow, out CircleCol, out CircleR);
                //    HRegion InnerCirRegion = new HRegion();
                //    ModelRoi = new HRegion();
                //    InnerCirRegion.GenCircle(CircleRowIn, CircleColIn, CircleRIn);
                //    ModelRoi.GenCircle(CircleRow, CircleCol, CircleR);
                //    ModelRoi.Difference(InnerCirRegion);
                //}
                //else if (RoiStr.Equals("Rect2+Rect2"))
                //{
                //    HTuple CenterRow, CenterCol, Rect2Phi, RectL1, RectL2;
                //    HRegion Rect2ROI = new HRegion();
                //    MyVisionBase.disp_message(ShowWindow, "绘制矩形1作为矩形联合区域的第一个矩形区域", "window", 12, 12, "red", "false");
                //    HOperatorSet.DrawRectangle2(ShowWindow, out CenterRow, out CenterCol, out Rect2Phi, out RectL1, out RectL2);
                //    ModelRoi = new HRegion();
                //    ModelRoi.GenRectangle2(CenterRow, CenterCol, Rect2Phi, RectL1, RectL2);
                //    HOperatorSet.ClearWindow(ShowWindow);
                //    MyVisionBase.hDispObj(ShowWindow, CurrentImage);
                //    MyVisionBase.disp_message(ShowWindow, "绘制矩形2作为矩形联合区域的第二个矩形区域", "window", 12, 12, "red", "false");
                //    HOperatorSet.DrawRectangle2(ShowWindow, out CenterRow, out CenterCol, out Rect2Phi, out RectL1, out RectL2);
                //    Rect2ROI.GenRectangle2(CenterRow, CenterCol, Rect2Phi, RectL1, RectL2);
                //    ModelRoi = ModelRoi.Union2(Rect2ROI);
                //}
                //else
                //{
                //    HOperatorSet.DrawRectangle1(ShowWindow, out row1, out column1, out row2, out column2);
                //    ModelRoi = new HRegion(row1, column1, row2, column2);
                //}
                #endregion
            }
            else if (btnCreateTemplate.Text == "保存")
            {
                btnCreateTemplate.Text = "新建";
                view1.roiController.NotifyRCObserver -= UpdateViewData;
                view1.roiController.reset();
                this.BeginInvoke(new Action(() => { ActionIsDrawingRoi(false); }));
                string files = TemplateParam.TemplatePath;
                if (false){
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK){
                        files = saveFileDialog1.FileName;
                        TemplateParam.Save(files);
                    }
                }
                else{
                    TemplateParam.Save(files);
                    TemplateParam.Load(files);
                }
            }
        }
      
        public void UpdateViewData(int val) 
        {
            switch (val)
            {
                case ViewROI.ROIController.EVENT_CHANGED_ROI_SIGN:
                case ViewROI.ROIController.EVENT_DELETED_ACTROI:
                case ViewROI.ROIController.EVENT_UPDATE_ROI:
                    bool genROI = view1.roiController.defineModelROI();
                    ModelRoi = view1.roiController.getModelRegion();
                    if (ModelRoi == null) return;
                    if (!TemplateParam.CreateShapeModel(CurrentImage, ModelRoi)){
                        Logger.PopError("模板创建失败！", true);
                        return;
                    }
                    view1.ResetWindow();
                    view1.AddViewObject(CurrentImage);
                    view1.SetDraw("blue", "margin");
                    view1.AddViewObject(ModelRoi);
                    HXLD ModelContour = new HXLD();
                    TemplateParam.GetShapeModelContour(out ModelContour);
                    view1.SetDraw("green", "margin");
                    view1.AddViewObject(ModelContour);
                    view1.Repaint();
                    break;
            }
        }


        private void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            try{
                string fileName;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "All files (*.*)|*.*|bmp files (*.bmp)|*.bmp";
                openFileDialog1.Multiselect = false;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Title = "打开图片文件";
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK){
                    fileName = openFileDialog1.FileName;
                    if (!TemplateParam.Load(fileName)){
                        MessageBox.Show("读取模板失败！");
                    }
                }
                openFileDialog1.Dispose();
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
            }
        }
        ViewROI.ROI SerachRoi = new ViewROI.ROI();
        ViewROI.ROIRectangle1 SerachRect1 = new ViewROI.ROIRectangle1();
        private void btnSaveSerach_Click(object sender, EventArgs e)
        {
            if (btnSaveSerach.Text == "新建") {
                btnSaveSerach.Text = "保存";              
                if (CurrentImage == null || !CurrentImage.IsInitialized()){
                    MessageBox.Show("请先加载一张图片");
                    return;
                }
                ActionIsDrawingRoi(true);                
                btnSaveSerach.Enabled = true;
                groupBox2.Enabled = true;
                view1.roiController.reset();
                view1.roiController.setROIShape(new ViewROI.ROIRectangle1(100));
            }
            else if (btnSaveSerach.Text == "保存"){
                SerachRoi = view1.roiController.getActiveROI();
                if (SerachRoi is ViewROI.ROIRectangle1){
                    btnSaveSerach.Text = "新建";
                    SerachRect1 = (ViewROI.ROIRectangle1)SerachRoi;
                    double row11, col11, row21, col21;
                    SerachRect1.GetRect1(out row11, out col11, out row21, out col21);
                    DialogResult rlt = MessageBox.Show("创建搜索框成功，是否替换原有参数？", "参数覆盖",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rlt != DialogResult.Yes) return;
                    Setting.SearchAreaX = (int)col11;
                    Setting.SearchAreaY = (int)row11;
                    Setting.SearchWidth = (int)(col21 - col11);
                    Setting.SearchHeight = (int)(row21 - row11);
                    this.BeginInvoke(new Action(() =>{
                        txtSearchX.Text = Setting.SearchAreaX.ToString();
                        txtSearchY.Text = Setting.SearchAreaY.ToString();
                        txtSearchWidth.Text = Setting.SearchWidth.ToString();
                        txtSearchHeight.Text = Setting.SearchHeight.ToString();
                        Logger.Pop("创建搜索框成功");
                    }));
                }
                else{
                    MessageBox.Show("请选中roi");         
                }
            }
        }

        private void btnSaveTempPar_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("是否更改设置？", "参数变更", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == rlt) {
                double dbValue, dbError;
                double ScaleValue = 1.0;
                double yMin, yMax;
                int area, length;
                if (!double.TryParse(txtStartAngle.Text.ToString(), out dbValue)){
                    MessageBox.Show("输入格式错误");
                    txtStartAngle.Text = "0";
                    txtStartAngle.Focus();
                    return;
                }
                if (Math.Abs(dbValue) > 360){
                    MessageBox.Show("输入数据错误（<360度）");
                    txtStartAngle.Text = "10";
                    txtStartAngle.Focus();
                    return;
                }
                TemplateParam.StartAngle = dbValue;
                if (!double.TryParse(txtMaxAngle.Text.ToString(), out dbValue)){
                    MessageBox.Show("输入格式错误");
                    txtMaxAngle.Text = "0";
                    txtMaxAngle.Focus();
                    return;
                }
                if (Math.Abs(dbValue) > 360){
                    MessageBox.Show("输入数据错误（<360度）");
                    txtMaxAngle.Text = "360";
                    txtMaxAngle.Focus();
                    return;
                }
                TemplateParam.EndAngle = dbValue;
                if (!double.TryParse(txtMinScore.Text.ToString(), out dbValue)){
                    MessageBox.Show("输入格式错误");
                    txtMinScore.Text = "0";
                    txtMinScore.Focus();
                    return;
                }
                if (!double.TryParse(TxtScale.Text.ToString(), out ScaleValue)) {
                    MessageBox.Show("输入格式错误,参数应该设置为0.1-2.0");
                    TxtScale.Text = "1.0";
                    TxtScale.Focus();
                    return;
                }
                if (Math.Abs(dbValue) < 30) {
                    MessageBox.Show("输入数据错误（>=60）");
                    txtMinScore.Text = "70";
                    txtMinScore.Focus();
                    return;
                }

                if (Math.Abs(dbValue) > 100)  {
                    MessageBox.Show("输入数据错误（<=100）");
                    txtMinScore.Text = "100";
                    txtMinScore.Focus();
                    return;
                }
                if (!double.TryParse(txtError.Text.ToString(), out dbError)) {
                    MessageBox.Show("输入格式错误");
                    txtError.Text = "50";
                    txtError.Focus();
                    return;
                }
                if (!double.TryParse(txtLimitYMax.Text.ToString(), out yMax)) {
                    MessageBox.Show("输入格式错误");
                    txtLimitYMax.Text = "1";
                    txtLimitYMax.Focus();
                    return;
                }
                if (!double.TryParse(txtLimitYMin.Text.ToString(), out yMin)) {
                    MessageBox.Show("输入格式错误");
                    txtLimitYMin.Text = "-1";
                    txtLimitYMin.Focus();
                    return;
                }
                if (!int.TryParse(txtRegionArea.Text.ToString(), out area)){
                    MessageBox.Show("输入格式错误");
                    txtRegionArea.Text = "1";
                    txtRegionArea.Focus();
                    return;
                }
                if (!int.TryParse(txtRegionLength.Text.ToString(), out length)) {
                    MessageBox.Show("输入格式错误");
                    txtRegionLength.Text = "-1";
                    txtRegionLength.Focus();
                    return;
                }
                if (Math.Abs(dbError) > 255) dbError = 255;
                TemplateParam.MaxError = Math.Abs(dbError);
                TemplateParam.Score = (int)(Math.Abs(dbValue));
                if (ScaleValue > 0.09 && ScaleValue < 2.1)
                    TemplateParam.Scale = ScaleValue;
                else {
                    MessageBox.Show("缩放参数设置错误");
                    TemplateParam.Scale = 1.0;
                }
                TemplateParam.Level = cmbLevel.SelectedIndex;
                TemplateParam.OffsetYMax = yMax;
                TemplateParam.OffsetYMin = yMin;
                TemplateParam.Area = area;
                TemplateParam.ContLength = length;
            }
        }

        private void btnFindTemplate_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("是否重新示教模板,点击YES,需要重新示教后面的线和圆？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt != DialogResult.Yes) return;
            if (CurrentImage == null) {
                Logger.PopError("请先采集图片！", true);
                return;
            }
            RectangleF roi = new RectangleF();
            roi.X = Setting.SearchAreaX;
            roi.Y = Setting.SearchAreaY;
            roi.Width = Setting.SearchWidth;
            roi.Height = Setting.SearchHeight;
            MatchingResult result;
            TemplateParam.FindSharpTemplate(CurrentImage, roi, TemplateParam, out result);
            if (result.mScore != null && result.mScore > 0) {
                XYOffset.Col = result.mCol - TemplateParam.CenterX;
                XYOffset.Row = result.mRow - TemplateParam.CenterY;
                TemplateParam.CenterX = result.mCol;
                TemplateParam.CenterY = result.mRow;
                TemplateParam.TemplateAngle = result.mAngle;
                TempFindVecvtorAngle = new St_VectorAngle(TemplateParam.CenterY, TemplateParam.CenterX, TemplateParam.TemplateAngle);
                view1.Refresh();
                this.view1.AddImage(this.CurrentImage);
                view1.Repaint();
                Thread.Sleep(100);
                HXLD contour;
                TemplateParam.GetDetectionContour(out contour);
                view1.SetDraw("blue", "margin");
                view1.AddViewObject(contour.CopyObj(1, -1));
                view1.Repaint();
                Thread.Sleep(500);    
                string str = string.Format("匹配度：{0}%", (100 * result.mScore.D).ToString("f2"));
                view1.SetString( 100,500, "red", str);
                str = string.Format("耗时：{0}ms", (result.mTime).ToString("f1"));
                view1.SetString( 100,250, "red", str);
                contour.Dispose();
            }
            else{
                view1.SetString(50, 100, "red", "匹配失败！");
            }
        }
        private void SubFrmTemplate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CurrentImage != null) CurrentImage.Dispose();
        }

        private void TempFindTestBtn_Click(object sender, EventArgs e)
        {
            if (CurrentImage == null) {
                Logger.PopError("请先采集图片！", true);
                return;
            }
            RectangleF roi = new RectangleF();
            roi.X = Setting.SearchAreaX;
            roi.Y = Setting.SearchAreaY;
            roi.Width = Setting.SearchWidth;
            roi.Height = Setting.SearchHeight;
            MatchingResult result;
            TemplateParam.FindSharpTemplate(CurrentImage, roi, TemplateParam, out result);
            view1.Refresh();
            view1.AddImage(CurrentImage);
            if (result.mScore != null && result.mScore > 0){
                TempFindVecvtorAngle = new St_VectorAngle(result.mRow, result.mCol, result.mAngle);
                HXLD contour;
                TemplateParam.GetDetectionContour(out contour);
                view1.SetDraw("blue", "margin");
                view1.AddViewObject(contour.CopyObj(1, -1));
                view1.Repaint();
                string str = string.Format("匹配度：{0}%", (100 * result.mScore.D).ToString("f2"));
                view1.SetString(100,150, "red", str);
                str = string.Format("耗时：{0}ms", (result.mTime).ToString("f1"));
                view1.SetString( 100,200, "red", str);
                str = string.Format("Row：{0}", (result.mRow.D).ToString("f1"));
                view1.SetString(100, 250, "red", str);
                str = string.Format("Col：{0}", (result.mCol.D).ToString("f1"));
                view1.SetString(100, 300, "red", str);
                str = string.Format("Angle：{0}", (result.mAngle.D*180.0/Math.PI).ToString("f1"));
                view1.SetString(100, 350, "red", str);
                contour.Dispose();
            }
            else{
                view1.SetString(50, 100, "red", "匹配失败！");
            }
        }

        private void cmbTempRegion_SelectedIndexChanged(object sender, EventArgs e){
            RoiStr = cmbTempRegion.SelectedItem.ToString();
        }

        private void DeleteRoiBtn_Click(object sender, EventArgs e) {
            IsDelete = true;
            DeleteRoiBtn.Visible = false;
            DeleteRoiBtn.Enabled = false;
            StopDeleteBtn.BackColor = Color.Red;
        }

        private void StopDeleteBtn_Click(object sender, EventArgs e)
        {
            IsDelete = false;
            DeleteRoiBtn.Visible = true;
            DeleteRoiBtn.Enabled = true;
            StopDeleteBtn.BackColor = Color.Gray;
        }

        private void EraserCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
             string tt = EraserCbx.SelectedItem.ToString();
             int.TryParse(tt, out   EraserSize);
        }

        private void txtRegionLength_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
