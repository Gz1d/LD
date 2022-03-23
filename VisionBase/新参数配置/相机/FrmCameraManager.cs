using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace VisionBase
{
    public partial class FrmCameraManager : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FrmCameraManager()
        {
            InitializeComponent();
        }
        private static object LockObj = new object();
        private static FrmCameraManager MyFrmCameraForm;

        public static FrmCameraManager Instance
        {
            get {
                lock (LockObj) {
                    return MyFrmCameraForm = MyFrmCameraForm ?? new FrmCameraManager();
                }
            }
        }
        ViewControl view = new ViewControl();
        private void FrmCameraManager_Load(object sender, EventArgs e)
        {
            view = DisplaySystem.GetViewControl("CameraManager");
            DisplaySystem.AddPanelForCCDView("CameraManager", panel1);
            this.cameraNameDataGridViewTextBoxColumn.Items.Clear();
            foreach (CameraEnum temp in Enum.GetValues(typeof(CameraEnum))) {
                this.cameraNameDataGridViewTextBoxColumn.Items.Add(temp);
            }
            this.CameInterfaceDataGridViewTextBoxColumn.Items.Clear();
            foreach (CamInterfaceEnum temp in Enum.GetValues(typeof(CamInterfaceEnum))){
                this.CameInterfaceDataGridViewTextBoxColumn.Items.Add(temp);
            }
            this.CamParaDgv.DataSource = CameraParaManager.Instance.CameraParaList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CameraParaManager.Instance.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.CamParaDgv.Refresh();
        }

        HalconDotNet.HObject ImgOut = new HalconDotNet.HObject();
        private void CamParaDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try{
                if (e.RowIndex >= 0) {
                    //当前行对应的相机参数
                    CameraPara CurCamPara = (CameraPara)this.CamParaDgv.CurrentRow.DataBoundItem;
                    if (CurCamPara == null) return;
                    switch (CamParaDgv.Columns[e.ColumnIndex].Name){
                        case "DeleteBtnDgvCol":
                            if (MessageBox.Show("是否删除该项设备？    ", "删除提示", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes) return;
                            CameraParaManager.Instance.CameraParaList.Remove(CurCamPara);
                            break;
                        case "OpenCamBtnDgvCol": 
                            CameraPara camParaI;
                            for (int i = 0; i < CameraParaManager.Instance.CameraParaList.Count() - 1;i++) {
                                camParaI = CameraParaManager.Instance.CameraParaList[i];
                                if (CurCamPara.CameraName == camParaI.CameraName) {
                                    MessageBox.Show("相机已经示教，请重新选择相机");
                                    break;                             
                                }
                            }
                            CameraCtrl.Instance.Init();
                            break;
                        case "GrabImgDgvCol":
                            ImgOut.Dispose();
                            CameraCtrl.Instance.GrabImg(CurCamPara.CameraName, out ImgOut);
                            view.ResetView();
                            view.AddViewImage(ImgOut);
                            //view.Refresh();
                            view.Repaint();
                            break;
                    }
                    this.CamParaDgv.Refresh();
                }
            }
            catch
            { }
        }

        private void CamParaDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try {
                if (e.RowIndex < 0) return;
                if (this.CamParaDgv.Columns[e.ColumnIndex].Name.Equals("isOpen")) {
                    if (e.RowIndex < CameraParaManager.Instance.CameraParaList.Count()) {
                        CameraPara item = CameraParaManager.Instance.CameraParaList[e.RowIndex];
                        if (item == null) e.Value = this.imageList1.Images[3];
                        else if (item.IsOpen) {
                            e.Value = (item.IsActive == true) ? this.imageList1.Images[2] : this.imageList1.Images[2];
                        }
                        else {
                            e.Value = this.imageList1.Images[1];
                        }
                    }
                }          
            }
            catch
            { }
        }

        private void button3_Click(object sender, EventArgs e)
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
                    if (ImgOut == null){
                        MessageBox.Show("图片为空，请先采集一张图片");
                        return;
                    }
                    HalconDotNet.HOperatorSet.WriteImage(ImgOut, "bmp", 0, fileName);
                    saveFileDialog1.Dispose();
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void FrmCameraManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult TS = MessageBox.Show("请勿关闭此页面，点“否”取消关闭", "提示",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (TS == DialogResult.Yes) e.Cancel = false;
            else  e.Cancel = true;
        }

    }
}
