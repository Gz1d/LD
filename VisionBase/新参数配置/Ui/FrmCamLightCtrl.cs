using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace VisionBase
{
    public partial class FrmCamLightCtrl : Form
    {
        ViewControl view1 = new ViewControl();
        public FrmCamLightCtrl(CameraLightPara camLightParaIn    )
        {
            InitializeComponent();
            InitCombobox();//初始化相机Cbx
            this.TeachCamLightPara = camLightParaIn;
            this.SaveCamLightPara = camLightParaIn;
            this.panelDataGridComboBoxColumn.Items.Clear();
            foreach (LightPanelEnum temp in Enum.GetValues(typeof(LightPanelEnum))) {
                this.panelDataGridComboBoxColumn.Items.Add(temp);
            }
            this.panelDataGridComboBoxColumn.ValueType = typeof(LightPanelEnum);
            this.lightCtrlDataGridViewComboBoxColumn.Items.Clear();
            foreach (LightCtrlEmun temp in Enum.GetValues(typeof(LightCtrlEmun))){
                this.lightCtrlDataGridViewComboBoxColumn.Items.Add(temp);
            }
            this.lightCtrlDataGridViewComboBoxColumn.ValueType = typeof(LightCtrlEmun);
            this.dataGridView1.DataSource = this.TeachCamLightPara.lightPara;
            this.ExposureBar.Value = camLightParaIn.Exposure; //同步曝光时间
            this.TriggerCheck.Checked = camLightParaIn.TriggerModel;//同步触发模式
            this.CamCbx.SelectedValue = camLightParaIn.CamName;//选定相机
            this.FilterCheck.Checked = this.TeachCamLightPara.IsFilter;//是否滤波
            this.FilterCNumUpDn.Value = (decimal)TeachCamLightPara.FilterC;
        }

        private CameraLightPara TeachCamLightPara =new CameraLightPara();
        private CameraLightPara SaveCamLightPara = new CameraLightPara();

        public CameraLightPara GetTeachPara(){
            return SaveCamLightPara;       
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 3) {
                LightPara item =(LightPara) this.dataGridView1.CurrentRow.DataBoundItem;
                LightCtrlManager.Instance.SetLightValue(item);//设置光源亮度
            }
            if (e.ColumnIndex == 4) {
                if(e.RowIndex<TeachCamLightPara.lightPara.Count()) 
                    this.TeachCamLightPara.lightPara.RemoveAt(e.RowIndex);
            }
        }
        private void FrmCamLightCtrl_Load(object sender, EventArgs e){
            view1 = DisplaySystem.GetViewControl("Test");
            DisplaySystem.AddPanelForCCDView("Test", panel1);
            this.CamCbx.SelectedIndexChanged += new System.EventHandler(this.CamCbx_SelectedIndexChanged);
        }

        private void ParaSaveBtn_Click(object sender, EventArgs e) {
            DialogResult dlt = MessageBox.Show("是否保存","参数保存",MessageBoxButtons.YesNo);
            if(dlt == DialogResult.Yes)  this.SaveCamLightPara = TeachCamLightPara;
        }
        private void ExposureBar_Scroll(object sender, EventArgs e) {
            TeachCamLightPara.Exposure = ExposureBar.Value;
            CameraCtrl.Instance.SetExpos(TeachCamLightPara.CamName, TeachCamLightPara.Exposure);
        }

        private void ExposureBar_ValueChanged(object sender, EventArgs e)  {
            this.ExposTxt.Text = ExposureBar.Value.ToString();
        }

        private void TriggerCheck_CheckedChanged(object sender, EventArgs e){
            this.TeachCamLightPara.TriggerModel = TriggerCheck.Checked;
        }

        /// <summary>初始化相机ComBox </summary>
        private void InitCombobox() {
            //Array arr = System.Enum.GetValues(typeof(CameraEnum));    // 获取枚举的所有值
            //DataTable dt = new DataTable();
            //dt.Columns.Add("String", Type.GetType("System.String"));
            //dt.Columns.Add("Value", typeof(int));
            //foreach (var a in arr){
            //    string strText = EnumTextByDescription.GetEnumDesc((CameraEnum)a);
            //    DataRow aRow = dt.NewRow();
            //    aRow[0] = strText;
            //    aRow[1] = (int)a;
            //    dt.Rows.Add(aRow);
            //}
            //this.CamCbx.DataSource = dt;
            //this.CamCbx.DisplayMember = "String";
            //this.CamCbx.ValueMember = "Value";
            this.CamCbx.Items.Clear();
            foreach (CameraEnum item in Enum.GetValues(typeof(CameraEnum))) {
                this.CamCbx.Items.Add(item);
            }           
        }

        private void CamCbx_SelectedIndexChanged(object sender, EventArgs e) {
            //string str = CamCbx.SelectedValue.ToString();
            //this.TeachCamLightPara.CamName = (CameraEnum)Enum.Parse(typeof(CameraEnum), CamCbx.SelectedValue.ToString(), false);
            //this.TeachCamLightPara.CamName = (CameraEnum)Enum.Parse(typeof(CameraEnum), CamCbx.SelectedValue.ToString(), false);
            this.TeachCamLightPara.CamName = (CameraEnum)CamCbx.SelectedItem;
        }
        HalconDotNet.HObject GrabedImg = new HalconDotNet.HObject();
        HalconDotNet.HObject FFtImg = new HalconDotNet.HObject();
        private void OneGrabBtn_Click(object sender, EventArgs e)   //采集一张图片
        {
            if (this.GrabedImg != null) this.GrabedImg.Dispose();
            if (CameraCtrl.Instance.GrabImg(TeachCamLightPara.CamName, out GrabedImg)) {
                this.view1.Refresh();
                if (TeachCamLightPara.IsFilter){
                    MyVisionBase.FilterImg(GrabedImg, out FFtImg, TeachCamLightPara.FilterC);
                    this.view1.AddViewImage(FFtImg);
                }
                else  {
                    this.view1.AddViewImage(GrabedImg);
                }
                this.view1.Repaint();
            }
        }
        bool IsContinnueGrab = true;
        private void ContinueGrabBtn_Click(object sender, EventArgs e) //连续采集
        {
            ContinueGrabBtn.Enabled = false;
            IsContinnueGrab = true;
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() => {
                int i = 0;
                while (IsContinnueGrab) {
                    i++;
                    if (GrabedImg != null) GrabedImg.Dispose();
                    if (CameraCtrl.Instance.GrabImg(TeachCamLightPara.CamName, out GrabedImg)){
                        view1.Refresh();
                        view1.AddViewImage(GrabedImg);
                        view1.Repaint();
                    }
                    System.Threading.Thread.Sleep(100);
                    if (i > 500) break;
                }                       
            }));
        }

        private void StopGrabBtn_Click(object sender, EventArgs e){
            ContinueGrabBtn.Enabled = true;
            IsContinnueGrab = false;
        }

        private void FrmCamLightCtrl_FormClosing(object sender, FormClosingEventArgs e){
            IsContinnueGrab = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
                string fileName;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.Title = "保存图片文件";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                    fileName = saveFileDialog1.FileName;
                    if (GrabedImg == null){
                        MessageBox.Show("图片为空，请先采集一张图片");
                        return;
                    }
                    HalconDotNet.HOperatorSet.WriteImage(GrabedImg, "bmp", 0, fileName);
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void MotionAdjBtn_Click(object sender, EventArgs e) {
            //Task.Factory.StartNew(new Action(() =>
            //{
                FrmAxisMotion frm1 = new FrmAxisMotion();
                frm1.ShowDialog();
            //}));
        }

        private void FilterCheck_CheckedChanged(object sender, EventArgs e) {
            TeachCamLightPara.IsFilter = FilterCheck.Checked;
        }

        private void FilterCNumUpDn_ValueChanged(object sender, EventArgs e) {
            TeachCamLightPara.FilterC = (double)FilterCNumUpDn.Value;
        }
    }
}
