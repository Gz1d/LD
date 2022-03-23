using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace VisionBase
{
    public partial class FrmAutoFocus : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FrmAutoFocus()
        {
            InitializeComponent();
        }
        ViewControl view ;
        int row1 = 1000;
        int col1 = 1000;
        int row2 = 1500;
        int col2 = 1500;
        HObject Img;
        bool IsContinue = true;
        string CcdName = "Cam1";

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchY_TextChanged(object sender, EventArgs e)
        {

        }



        private void FrmAutoFocus_Load(object sender, EventArgs e)
        {
            view = DisplaySystem.GetViewControl("FocusPanel");
            DisplaySystem.AddPanelForCCDView("FocusPanel", panel1) ;
            CamCbx.SelectedIndex = 0;
            LightPanelCbx.SelectedItem = 0;
            ////增加处理，显示
            //view.Repaint();
            //view.ResetView();
            //view.Refresh();
            //view.AddViewImage(img);
            //view.Repaint();
            ColorPanelCbx.SelectedItem = 0;
            this.FormClosing += MyFormClosing;

        }

        private void DrawRectBtn_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("请先加载一张图片");
                return;          
            }
            DrawRectBtn.Enabled = false;
            view.DrawRect1(out row1, out col1, out row2, out col2);
            txtSearchX.Text = col1.ToString();
            txtSearchY.Text = row1.ToString();
            txtSearchWidth.Text = (col2- col1).ToString();
            txtSearchHeight.Text =( row2- row1).ToString();
            DrawRectBtn.Enabled = true;
            HObject Rect, RectContour;
            HOperatorSet.GenRectangle1(out Rect, row1, col1, row2, col2);
            HOperatorSet.GenContourRegionXld(Rect, out RectContour, "border");
            view.Refresh();
            view.AddViewImage(Img);
            view.AddViewObject(RectContour);
            view.Repaint();
        }

        private void AddImgeBtn_Click(object sender, EventArgs e)
        {
            AddImgeBtn.Enabled = false;
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.*)|*.*|bmp files (*.bmp)|*.bmp";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "打开图片文件";
            openFileDialog1.RestoreDirectory = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                HTuple wid, hei;
                if(Img!=null)
                Img.Dispose();
                HOperatorSet.ReadImage(out Img, fileName);
                HOperatorSet.GetImageSize(Img, out wid, out hei);
                view.Refresh();
                view.AddViewImage(Img);
                view.Repaint();
            }

            MyVisionBase.SaveImg(Img, "尺寸测量");
            AddImgeBtn.Enabled = true;
        }

        private void StartFocusBtn_Click(object sender, EventArgs e)
        {
            StartFocusBtn.Enabled = false;
            StopFocusBtn.Enabled = true;
            AddImgeBtn.Enabled = false;
            DrawRectBtn.Enabled = false;
           IsContinue = true;
            Task.Factory.StartNew(new Action(GetFocusValueProcess));
            
        }

        private void StopFocusBtn_Click(object sender, EventArgs e)
        {
            StartFocusBtn.Enabled = true;
            StopFocusBtn.Enabled = false;
            AddImgeBtn.Enabled = true;
            DrawRectBtn.Enabled = true;
            IsContinue = false;
        }

        public void GetFocusValueProcess()
        {
            HTuple Value = new HTuple();
            HObject Rect = new HObject();
            HObject ReduceImg = new HObject();
            HObject CropImg = new HObject();
            HObject RectContour = new HObject();
            HTuple Width, Hei;
            while (IsContinue)
            {
                try
                {
                    #region
                    if (Img != null&& Img.IsInitialized()) Img.Dispose();
                    //if (RectContour != null && RectContour.IsInitialized()) RectContour.Dispose();
                    Camera.Instance.GrabImg(CcdName,out Img);
                    HOperatorSet.GenRectangle1(out Rect, row1, col1, row2, col2);
                    HOperatorSet.GenContourRegionXld(Rect, out RectContour, "border");
                    HOperatorSet.ReduceDomain(Img, Rect, out ReduceImg);
                    Rect.Dispose();
                    HOperatorSet.CropDomain(ReduceImg, out CropImg);
                    ReduceImg.Dispose();
                    MyVisionBase.evaluate_definition(CropImg, out Value);
                    CropImg.Dispose();
                    view.ResetView();
                    view.AddViewImage(Img);
                    view.AddViewObject(RectContour);
                    HOperatorSet.GetImageSize(Img, out Width, out Hei);
                    
                    view.Repaint();
                    view.SetString(Width.D / 2, Hei.D / 2, "red", Value.ToString());
                    System.Threading.Thread.Sleep(100);
                    //Img.Dispose();
                    //RectContour.Dispose();
                    #endregion

                }
                catch  {     }
            }


        }

        private void SetColorModelBtn_Click(object sender, EventArgs e)
        {
            // camera.instance.SetColorModel(string    Model = "default", string CcdName = "Cam1")
            Camera.Instance.SetColorModel("gray", CcdName);
        }

        private void LightValueBar_Scroll(object sender, EventArgs e)
        {
            LabelLightValue.Text = LightValueBar.Value.ToString();
            WordLightForm.Instance.OpenLightPanel(LightPanelCbx.SelectedIndex +1);
            WordLightForm.Instance.SetLightValue(LightValueBar.Value, LightPanelCbx.SelectedIndex + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Camera.Instance.SetColorModel("color", CcdName);
        }

        bool IsGrab = true;
        private void StartGrabBtn_Click(object sender, EventArgs e)
        {
            StartGrabBtn.Enabled = false;
            Camera.Instance.SetColorModel("color", CcdName);
            HObject ColorImg = new HObject(),RedImg =new HObject(),GreenImg =new HObject(),BlueImg =new HObject();
            IsGrab = true;
            Task.Factory.StartNew(new Action(() => {
                while(IsGrab)
                {
                    try
                    {
                        Thread.Sleep(100);
                        if (ColorImg != null) ColorImg.Dispose();
                        if (!Camera.Instance.GrabImg(CcdName, out ColorImg))
                        {
                            MessageBox.Show("相机采图失败，请检查相机");
                            break;
                        
                        }
                        if (RedImg != null) RedImg.Dispose();
                        if (GreenImg != null) GreenImg.Dispose();
                        if (BlueImg != null) BlueImg.Dispose();
                        if (ColorImg == null) break;
                        HOperatorSet.Decompose3(ColorImg, out RedImg, out GreenImg, out BlueImg);
                        view.ResetView();
                        switch (ColorPanelCbx.SelectedIndex)
                        {
                            case 0:
                                view.AddViewImage(RedImg);
                                break;
                            case 1:
                                view.AddViewImage(GreenImg);
                                break;
                            case 2:
                                view.AddViewImage(BlueImg);
                                break;
                            case 3:
                                view.AddViewImage(ColorImg);
                                break;
                        }
                        view.Repaint();
                    }
                    catch(Exception e0)
                    {

                        MessageBox.Show(e0.ToString());
                        break;
                    
                    }
                }




            }));

            
        }

        private void CamCbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StopGrabBtn_Click(object sender, EventArgs e)
        {
            IsGrab = false;
            StartGrabBtn.Enabled = true;
        }

        private void FrmAutoFocus_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsGrab = false;

        }
        private void MyFormClosing(object sender, FormClosingEventArgs e)
        {
            //Dialog MyDlg = 
            DialogResult DlgReslult = MessageBox.Show("是否关闭程序", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DlgReslult == DialogResult.No) e.Cancel = true;
        }

        private void trackBarExposure_Scroll(object sender, EventArgs e)
        {
            int value = trackBarExposure.Value;
            txtExposureValue.Text = value.ToString();
            Camera.Instance.ExposSet(CcdName, value);
        }

        private void txtExposureValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtExposureValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(13))
            {
                int iValue;
                if (!int.TryParse(txtExposureValue.Text, out iValue))
                {
                    Logger.PopError("输入曝光时间字符错误！", true);
                    txtExposureValue.Text = trackBarExposure.Value.ToString();
                    txtExposureValue.Focus();
                    return;
                }

                if (iValue < trackBarExposure.Minimum)
                {
                    Logger.PopError("输入曝光时间不能小于" + trackBarExposure.Minimum.ToString(), true);
                    txtExposureValue.Text = trackBarExposure.Minimum.ToString();
                    txtExposureValue.Focus();
                    return;
                }

                if (iValue > trackBarExposure.Maximum)
                {
                    Logger.PopError("输入曝光时间不能大于" + trackBarExposure.Maximum.ToString(), true);
                    txtExposureValue.Text = trackBarExposure.Maximum.ToString();
                    txtExposureValue.Focus();
                    return;
                }
                trackBarExposure.Value = iValue;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //FrmCameraManager frm = new FrmCameraManager();
            //frm.ShowDialog();
            //  ProjectParaManager.Instance.Save();

            FrmAxisMotion frm1 = new FrmAxisMotion();
            frm1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ProjectParaManager.Instance.Read();
        }

    }
}
