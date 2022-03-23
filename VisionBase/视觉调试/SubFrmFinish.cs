using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using TapeVisionBase.外部接口;
using HalconDotNet;
using System.Threading;


namespace VisionBase
{
    public partial class SubFrmFinish : Form
    {
        public St_Position StationCurrentPos = new St_Position();
        /// <summary> 示教的定位参数 </summary>
        public  LocalPara LocalPara0;
        public HObject CurImg = new HObject();
        public ViewControl view1;
        private St_Position OriginPos;
        private int CurrentMarkIndex;
        public SubFrmFinish( ViewControl viewIn)
        {
            InitializeComponent();
            InitCombobox();
            view1 = viewIn;
        }

        public SubFrmFinish(St_Position pos, int markIndex)
        {
            InitializeComponent();
            
            CurrentMarkIndex = markIndex;
        }

        public void UpdateCurrImage(HObject ImgIn)
        {
            if (ImgIn != null && ImgIn.IsInitialized())
                HOperatorSet.CopyImage(ImgIn, out CurImg);
        }
        public void UpDatePara(LocalPara ParaIn)
        {
            LocalPara0 = ParaIn;
            if (LocalPara0 == null) LocalPara0 = new LocalPara();
            if (LocalPara0.localSetting.TeachCoordi == null) LocalPara0.localSetting.TeachCoordi = CoordiEmum.Coordi0;
            this.CoordiCbx.SelectedIndex = (int)LocalPara0.localSetting.TeachCoordi;
            
            NumUpDn_offset_x_range.Value = (decimal)LocalPara0.localSetting.Offset_x_range;
            NumUpDn_offset_y_range.Value = (decimal)LocalPara0.localSetting.Offset_y_range;
            NumUpDn_offset_theta_range.Value = (decimal)LocalPara0.localSetting.Offset_theta_range;
            NumUpDn_Offset_x.Value = (decimal)LocalPara0.localSetting.Offset_x;
            NumUpDn_Offset_y.Value = (decimal)LocalPara0.localSetting.Offset_y;
            NumUpDn_Offset_thta.Value = (decimal)LocalPara0.localSetting.Offset_theta;
            GrabPosTbx.Text = "X:" + LocalPara0.localSetting.GrabPosTeach.x.ToString("f2")
                 + "   Y:" + LocalPara0.localSetting.GrabPosTeach.y.ToString("f2")
                 + "   Theta:" + LocalPara0.localSetting.GrabPosTeach.angle.ToString("f2");
        }
        private void FrmSubFrmFindLine_Load(object sender, EventArgs e)
        {
            
            if (LocalPara0 == null) LocalPara0 = new LocalPara();
            if (LocalPara0.localSetting.TeachCoordi == null) LocalPara0.localSetting.TeachCoordi = CoordiEmum.Coordi0;
            this.CoordiCbx.SelectedIndex = (int)LocalPara0.localSetting.TeachCoordi;
            this.NumUpDn_offset_x_range.Value = (decimal)LocalPara0.localSetting.Offset_x_range;
            this.NumUpDn_offset_y_range.Value = (decimal)LocalPara0.localSetting.Offset_y_range;
            this.NumUpDn_offset_theta_range.Value = (decimal)LocalPara0.localSetting.Offset_theta_range;
            this.NumUpDn_Offset_x.Value =(decimal)LocalPara0.localSetting.Offset_x;
            this.NumUpDn_Offset_y.Value = (decimal)LocalPara0.localSetting.Offset_y;
            this.NumUpDn_Offset_thta.Value = (decimal)LocalPara0.localSetting.Offset_theta;
            this.GrabPosTbx.Text = "X:" + LocalPara0.localSetting.GrabPosTeach.x.ToString("f2")
                 + "   Y:" + LocalPara0.localSetting.GrabPosTeach.y.ToString("f2") 
                 + "   Theta:" + LocalPara0.localSetting.GrabPosTeach.angle.ToString("f2");
            this.CoordiCbx.SelectedIndexChanged += new System.EventHandler(this.CoordiCbx_SelectedIndexChanged);
        }


        private void SubFrmFinish_FormClosing(object sender, FormClosingEventArgs e)
        {

            //StationCurrentPos = MotionController.GetCurrentPos(IsLeftStation);

            //  if(pos.X!=OriginPos.X)
        }

        private void ProductPixelPosTeachBtn_Click(object sender, EventArgs e)
        {
            GetProductPixelPosBtn.Enabled = true;
            ProductPixelPosTeachBtn.Enabled = false;
        }

        private void GetProductPixelPosBtn_Click(object sender, EventArgs e)  //计算示教产品的像素的坐标，并保存到配置文件
        {
            this.GetProductPixelPosBtn.Enabled = false;
            this.ProductPixelPosTeachBtn.Enabled = true;
            try
            {
                LocalResult localResult = new LocalResult();
                LocalManager myLocal = new LocalManager();
                //1.0设置定位模式
                myLocal.SetLocalModel(LocalPara0.localSetting.localModel);
                //2.0设置定位参数
                myLocal.SetParam(CurImg, LocalPara0);
                //3.执行定位
                myLocal.doLocal();
                localResult = myLocal.GetResult();
                this.LocalPara0.localSetting.TeachImgLocal = new St_VectorAngle(localResult.row, localResult.col, localResult.angle);
                view1.ResetWindow();
                view1.AddImage(CurImg.CopyObj(1,-1));
                view1.SetDraw("blue", "margin");
                view1.AddViewObject(localResult.ShowContour.CopyObj(1,-1));
                view1.Repaint();
                this.ProductPixelPosTbx.Text = "Col:" + localResult.col.ToString("f2") + "   Row:" + localResult.row.ToString("f2") +
                    "   Angle:" + localResult.angle.ToString("f2");
            }
            catch 
            { }
            }

        private void GrabPosTeachBtn_Click(object sender, EventArgs e)  //抓图坐标的示教
        {
            GrabPosTeachBtn.Enabled = false;
            GrabPosGetBtn.Enabled = true;
        }

        private void GrabPosGetBtn_Click(object sender, EventArgs e) //获取抓图坐标
        {
            GrabPosTeachBtn.Enabled  =true;
            GrabPosGetBtn.Enabled = false;
            double X=0, Y=0,Z=0, Theta = 0;
            MotionManager.Instance.SetCoordi(LocalPara0.localSetting.TeachCoordi);
            MotionManager.Instance.GetCoordiPos(out X, out Y, out Z, out Theta);
            GrabPosTbx.Text = "X:" + X.ToString("f2") + "   Y:" + Y.ToString("f2") + "   Theta:" + Theta.ToString("f2");
            LocalPara0.localSetting.GrabPosTeach = new Point3Db(X, Y, Theta);
        }
        private void InitCombobox()
        {
            this.CoordiCbx.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(CoordiEmum))){
                this.CoordiCbx.Items.Add(item);
            }
            this.CoordiCbx.SelectedIndex = 0;
        }

        private void CoordiCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocalPara0.localSetting.TeachCoordi= (CoordiEmum)Enum.Parse(typeof(CoordiEmum), CoordiCbx.SelectedValue.ToString(), false);
            MotionManager.Instance.SetCoordi(LocalPara0.localSetting.TeachCoordi);
        }
        private void NumUpDn_offset_x_range_ValueChanged(object sender, EventArgs e)
        {
            LocalPara0.localSetting.Offset_x_range = (double )NumUpDn_offset_x_range.Value;
        }

        private void NumUpDn_offset_y_range_ValueChanged(object sender, EventArgs e)
        {
            LocalPara0.localSetting.Offset_y_range = (double)NumUpDn_offset_y_range.Value;
        }

        private void NumUpDn_offset_theta_range_ValueChanged(object sender, EventArgs e)
        {
            LocalPara0.localSetting.Offset_theta_range = (double)NumUpDn_offset_theta_range.Value;
        }

        private void NumUpDn_Offset_x_ValueChanged(object sender, EventArgs e)
        {
            LocalPara0.localSetting.Offset_x = (double)NumUpDn_Offset_x.Value;
        }

        private void NumUpDn_Offset_y_ValueChanged(object sender, EventArgs e)
        {
            LocalPara0.localSetting.Offset_y = (double)NumUpDn_Offset_y.Value;
        }

        private void NumUpDn_Offset_thta_ValueChanged(object sender, EventArgs e)
        {
            LocalPara0.localSetting.Offset_theta = (double)NumUpDn_Offset_thta.Value;
        }
    }
}
