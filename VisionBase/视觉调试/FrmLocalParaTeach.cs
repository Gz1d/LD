using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using HalconDotNet;


namespace VisionBase
{
    public partial class FrmLocalParaTeach : Form 
    {
        private int SubFormIndex = 0;
        private LocalPara TeachLocalPara;
        private HObject CurrentImage = null;
        private HObject TempSrcImage = null;
        private int OrgImageWid, OrgImageHei;
        private object ObjImageLock = new object();
        public int CCDNum = 0;
        private List<Form> VisionSubFormList = new List<Form>();
        private Action<bool> ActionDrawRoiInHWindow = null;
        private bool IsDrawingRoi = false;

        private St_VectorAngle TemplateFindVectorAngle = new St_VectorAngle();

        public bool IsSaveVisionPara = false;
        public bool ContinueSnapImage = false;

        public ViewControl view1;
        public FrmLocalParaTeach()
        {
            InitializeComponent();
        }
        public FrmLocalParaTeach(LocalPara LocalParaIn)
        {
            InitializeComponent();
            TeachLocalPara = LocalParaIn;
        }
        public LocalPara GetTeachLocalPara()
        {
            return TeachLocalPara;
        }
        private void FrmImageParaTeach_Load(object sender, EventArgs e)
        {
            view1 = DisplaySystem.GetViewControl("Local");
            DisplaySystem.AddPanelForCCDView("Local", panel1);
            InitSubFrmList();
            SwitchSubFrm(SubFormIndex);
            SwitchStepButton(SubFormIndex);
        }

        private void InitSubFrmList()
        {
            ActionDrawRoiInHWindow = new Action<bool>((isDrawingRoi) => {
                this.Invoke(new Action(() => {
                    panelOperator.Enabled = !isDrawingRoi;
                    foreach (Control item in panelOperator.Controls){
                        item.Enabled = !isDrawingRoi;
                    }
                    foreach (Control item in panelOperator.Controls) {
                        item.Enabled = !isDrawingRoi;
                    }
                    button1.Enabled = true;
                    IsDrawingRoi = isDrawingRoi;
                }));
            });
            Form form;
            HObject NowImg = new HObject();
            if (CurrentImage != null) {
                if (CurrentImage.IsInitialized())   HOperatorSet.CopyImage(CurrentImage, out NowImg);
            }
            if (TeachLocalPara.localSetting.ToString().Contains("Temp")) //模板界面
            {
                form = new SubFrmTemplate(TeachLocalPara.Template, TeachLocalPara.localSetting, view1, CurrentImage, ActionDrawRoiInHWindow);
                form.TopLevel = false;
                VisionSubFormList.Add(form);
            }
            if (TeachLocalPara.localSetting.ToString().Contains("Blob"))  //blob界面
            {
                form = new SubFrmBlobLocal(TeachLocalPara.Blobs, view1, NowImg, ActionDrawRoiInHWindow);
                form.TopLevel = false;
                VisionSubFormList.Add(form);
            }
            if (TeachLocalPara.localSetting.localModel.ToString().Contains("Line"))   //Line界面
            { 
                form = new SubFrmFindLine(TeachLocalPara.Lines, view1, NowImg, ActionDrawRoiInHWindow);
                form.TopLevel = false;
                VisionSubFormList.Add(form);
            }
            if (TeachLocalPara.localSetting.localModel.ToString().Contains("Circle")) //Line界面
            {
                form = new SubFrmFindCircle(TeachLocalPara.Circles, view1, NowImg, ActionDrawRoiInHWindow);
                form.TopLevel = false;
                VisionSubFormList.Add(form);
            }
            if (TeachLocalPara.localSetting.localModel.ToString().Contains("LineCirRectInsp"))//检测界面
            {
                form = new SubFrmRecheck(TeachLocalPara, TeachLocalPara.LineCirRectInspParam, view1, NowImg, ActionDrawRoiInHWindow);
                form.TopLevel = false;
                VisionSubFormList.Add(form);
            }
            form = new SubFrmFinish(view1);
            ((SubFrmFinish)form).TopLevel = false;
             VisionSubFormList.Add(form);
        }

        private void SwitchSubFrm(int index)
        {
            panelOperator.Controls.Clear();
            panelOperator.Controls.Add(VisionSubFormList[index]);
            VisionSubFormList[index].Show();
        }

        private void SwitchStepButton(int step)
        {
            int total = VisionSubFormList.Count;
            if (step == 0){
                btnLastStep.Visible = false;
                btnNextStep.Text = "下一步";
            }
            else if (step < total - 1){
                btnLastStep.Visible = true;
                btnLastStep.Text = "上一步";
                btnNextStep.Text = "下一步";
            }
            else{
                btnLastStep.Visible = true;
                btnLastStep.Text = "上一步";
                btnNextStep.Text = "保存";
            }
        }

        private void btnLastStep_Click(object sender, EventArgs e)
        {
            SubFormIndex--;
            SwitchSubFrm(SubFormIndex);
            SwitchStepButton(SubFormIndex);
        }
        private void btnNextStep_Click(object sender, EventArgs e)
        {
            SubFormIndex++;
            if (SubFormIndex == VisionSubFormList.Count){
                DialogResult rlt = MessageBox.Show("即将保存参数 &&退出，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                foreach (var item in panelOperator.Controls){
                    if (item is SubFrmTemplate) {
                        TeachLocalPara.localSetting.SearchAreaX = ((SubFrmTemplate)item).Setting.SearchAreaX;
                        TeachLocalPara.localSetting.SearchAreaY = ((SubFrmTemplate)item).Setting.SearchAreaY;
                        TeachLocalPara.localSetting.SearchWidth = ((SubFrmTemplate)item).Setting.SearchWidth;
                        TeachLocalPara.localSetting.SearchHeight = ((SubFrmTemplate)item).Setting.SearchHeight;
                        TeachLocalPara.Template = ((SubFrmTemplate)item).TemplateParam;
                        TemplateFindVectorAngle = ((SubFrmTemplate)item).TempFindVecvtorAngle;
                    }
                    else if (item is SubFrmBlobLocal) {
                        TeachLocalPara.Blobs = ((SubFrmBlobLocal)item).BlobInspectPara;
                        break;
                    }
                    else if (item is SubFrmFindLine){
                        TeachLocalPara.Lines = ((SubFrmFindLine)item).LinesParam;
                    }
                    else if (item is SubFrmFindCircle){
                        TeachLocalPara.Circles = ((SubFrmFindCircle)item).CircleParam;
                    }

                    else if (item is SubFrmRecheck)
                    {
                        TeachLocalPara.LineCirRectInspParam = ((SubFrmRecheck)item).RecheckParam;
                    }
                    else if (item is SubFrmFinish)
                    {
                        TeachLocalPara.localSetting = ((SubFrmFinish)item).LocalPara0.localSetting;
                    }
                    else { }
                }
                if (rlt == DialogResult.Yes) {
                    IsSaveVisionPara = true;
                    this.Close();
                    this.Dispose();
                    return;
                }
                else{
                    SubFormIndex--;
                    return;
                }
            }
            else {
                foreach (var item in panelOperator.Controls){
                    if (item is SubFrmTemplate) {
                        TeachLocalPara.localSetting.SearchAreaX = ((SubFrmTemplate)item).Setting.SearchAreaX;
                        TeachLocalPara.localSetting.SearchAreaY = ((SubFrmTemplate)item).Setting.SearchAreaY;
                        TeachLocalPara.localSetting.SearchWidth = ((SubFrmTemplate)item).Setting.SearchWidth;
                        TeachLocalPara.localSetting.SearchHeight = ((SubFrmTemplate)item).Setting.SearchHeight;
                        TeachLocalPara.Template = ((SubFrmTemplate)item).TemplateParam;
                        TemplateFindVectorAngle = ((SubFrmTemplate)item).TempFindVecvtorAngle;
                    }
                    else if (item is SubFrmBlobLocal) {
                        TeachLocalPara.Blobs = ((SubFrmBlobLocal)item).BlobInspectPara;
                        break;
                    }
                    else if (item is SubFrmFindLine) {
                        TeachLocalPara.Lines = ((SubFrmFindLine)item).LinesParam;
                    }
                    else if (item is SubFrmFindCircle) {
                        TeachLocalPara.Circles = ((SubFrmFindCircle)item).CircleParam;
                    }

                    else if (item is SubFrmRecheck) {
                        TeachLocalPara.LineCirRectInspParam = ((SubFrmRecheck)item).RecheckParam;
                    }
                    else if (item is SubFrmFinish) {
                        TeachLocalPara.localSetting = ((SubFrmFinish)item).LocalPara0.localSetting;
                    }
                    else { }
                }
                SwitchSubFrm(SubFormIndex);
                SwitchStepButton(SubFormIndex);
            }
            SwitchStepButton(SubFormIndex);
            if (CurrentImage != null) {
                if (CurrentImage.IsInitialized())
                    UpdateImageToSubForm();            
            }
            UpdateParaToSubForm();
        }


        private void UpdateImageToSubForm()
        {
            HObject NowImg = new HObject();
            if (CurrentImage != null){
                if (CurrentImage.IsInitialized())  HOperatorSet.CopyImage(CurrentImage, out NowImg);
            }
            foreach (var item in VisionSubFormList) {
                if (item is SubFrmTemplate) {
                    lock (ObjImageLock){
                        ((SubFrmTemplate)item).UpdateCurrImage(TempSrcImage);
                    }
                }
                if (item is SubFrmFindLine) {
                    lock (ObjImageLock) {
                        ((SubFrmFindLine)item).UpdateCurrImage(NowImg, TemplateFindVectorAngle,TeachLocalPara);
                    }
                }
                if (item is SubFrmBlobLocal) {
                    lock (ObjImageLock) {
                        ((SubFrmBlobLocal)item).UpdateCurrImage(NowImg, TemplateFindVectorAngle, TeachLocalPara);
                    }
                }
                if (item is SubFrmFindCircle){
                    lock (ObjImageLock){
                        ((SubFrmFindCircle)item).UpdateCurrImage(NowImg, TemplateFindVectorAngle, TeachLocalPara);
                    }
                }
                if (item is SubFrmRecheck) {
                    lock (ObjImageLock){
                        ((SubFrmRecheck)item).UpdateCurrImage(NowImg, TemplateFindVectorAngle, TeachLocalPara);
                    }
                 }
                if (item is SubFrmFinish){
                    lock (ObjImageLock){
                        ((SubFrmFinish)item).UpdateCurrImage(CurrentImage);
                    }
                }
            }
        }


       
        private void UpdateParaToSubForm()
        {
            foreach (var item in VisionSubFormList) {
                if (item is SubFrmTemplate) {
                    ((SubFrmTemplate)item).UpdateSetting(TeachLocalPara.localSetting);
                }
                if (item is SubFrmFindLine){
                    ((SubFrmFindLine)item).UpDatePara(TeachLocalPara);
                }
                if (item is SubFrmFindCircle){
                    ((SubFrmFindCircle)item).UpDatePara(TeachLocalPara);
                }
                if (item is SubFrmBlobLocal) {
                    ((SubFrmBlobLocal)item).UpDatePara(TeachLocalPara);
                }
                if (item is SubFrmRecheck){
                    ((SubFrmRecheck)item).UpdatePara(TeachLocalPara.Lines, TeachLocalPara.Circles,
                        TeachLocalPara.LineCirRectInspParam.VectorAngle0);
                }
                if (item is SubFrmFinish) {
                    ((SubFrmFinish)item).UpDatePara(TeachLocalPara);
                }
            }
        }



        private void btnLoadLocalImage_Click(object sender, EventArgs e)
        {
            try {
                string fileName;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "All files (*.*)|*.*|bmp files (*.bmp)|*.bmp";
                openFileDialog1.Multiselect = false;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Title = "打开图片文件";
                openFileDialog1.RestoreDirectory = false;
                openFileDialog1.InitialDirectory = @"D:\Image\" ;
                if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                    fileName = openFileDialog1.FileName;
                    HObject img;
                    HTuple wid, hei;
                    HOperatorSet.GenEmptyObj(out img);
                    HOperatorSet.ReadImage(out img, fileName);
                    HOperatorSet.GetImageSize(img, out wid, out hei);
                    //hWindowControl1.ImagePart = new Rectangle(0, 0, wid, hei);
                    OrgImageWid = wid;
                    OrgImageHei = hei;
                    TeachLocalPara.Template.SetTrainImage(fileName);
                    if (CurrentImage != null) CurrentImage.Dispose();
                    if (TempSrcImage != null) TempSrcImage.Dispose();
                    HOperatorSet.CopyImage(img, out CurrentImage);
                    HOperatorSet.CopyImage(img, out TempSrcImage);
                    view1.Refresh();
                    view1.ResetView();
                    view1.AddViewImage(CurrentImage);
                    view1.Repaint();
                    UpdateImageToSubForm();
                    if (img != null) img.Dispose();                  
                }
                openFileDialog1.Dispose();
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
            }
        }


        /// <summary>
        /// 保存图片到本地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            try {
                string fileName;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.Title = "保存图片文件";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK){
                    fileName = saveFileDialog1.FileName;
                    HOperatorSet.WriteImage(CurrentImage, "bmp", 0, fileName);
                    
                }
                saveFileDialog1.Dispose();
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void btnMotorDebug_Click(object sender, EventArgs e)
        {
            //MotionController.ShowMotionDebugFrm(IsLeftStation);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ActionDrawRoiInHWindow(false);
        }

        private void FrmImageParaTeach_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isTeachRunning = false;
            foreach (var item in VisionSubFormList) {
                if (item is SubFrmFindLine){
                    isTeachRunning = ((SubFrmFindLine)item).ShowRoiFlag;
                    if (isTeachRunning) break;
                }
                if (item is SubFrmFindCircle) {
                    isTeachRunning = ((SubFrmFindCircle)item).ShowRoiFlag;
                    if (isTeachRunning) break;
                }
                if (item is SubFrmRecheck) {
                    isTeachRunning = ((SubFrmRecheck)item).ShowRoiFlag;
                    if (isTeachRunning) break;
                }
            }
            if (isTeachRunning) {
                MessageBox.Show("正在示教参数，请停止！");
                e.Cancel = true;
                return;
            }

            if (ContinueSnapImage) {
                MessageBox.Show("连续采集图片中，请停止！");
                e.Cancel = true;
                return;
            }

            if (IsDrawingRoi) {
                MessageBox.Show("正在设置ROI，请在绘图区域右键释放鼠标！");
                e.Cancel = true;
                return;
            }
            ContinueSnapImage = false;
            Thread.Sleep(100);
            foreach (var item in VisionSubFormList) {
                if (item != null && !item.IsDisposed) item.Dispose();
            }
        }










    }
}
