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
    public partial class FrmCaliParaManager :  WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FrmCaliParaManager()
        {
            InitializeComponent();
            this.caliModelDataGridViewComboBoxColumn.Items.Clear();
            foreach (CaliModelEnum temp in Enum.GetValues(typeof(CaliModelEnum))){
                this.caliModelDataGridViewComboBoxColumn.Items.Add(temp);
            }
            this.caliModelDataGridViewComboBoxColumn.ValueType = typeof(CaliModelEnum);
            this.coordiDataGridViewComboBoxColumn.Items.Clear();
            foreach (CoordiEmum temp in Enum.GetValues(typeof(CoordiEmum))){
                this.coordiDataGridViewComboBoxColumn.Items.Add(temp);
            }
            this.coordiDataGridViewComboBoxColumn.ValueType = typeof(CoordiEmum);
            this.camDataGridViewComboBoxColumn.Items.Clear();
            foreach (CameraEnum temp in Enum.GetValues(typeof(CameraEnum))){
                this.camDataGridViewComboBoxColumn.Items.Add(temp);
            }
            this.camDataGridViewComboBoxColumn.ValueType = typeof(CameraEnum);
            this.coordiCamDataGridViewComboBoxColumn.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(CoordiCamHandEyeMatEnum))){
                this.coordiCamDataGridViewComboBoxColumn.Items.Add(item);
            }
            this.coordiCamDataGridViewComboBoxColumn.ValueType = typeof(CoordiCamHandEyeMatEnum);
        }

        private static object LockObj = new object();
        private static FrmCaliParaManager MyFrmCaliParaManager;

        public static FrmCaliParaManager Instance
        {
            get {
                lock (LockObj) {
                    return MyFrmCaliParaManager = MyFrmCaliParaManager ?? new FrmCaliParaManager();
                }
            }
        }


        private void FrmCaliParaManager_Load(object sender, EventArgs e) {
            this.dataGridView1.DataSource = CaliParaManager.Instance.CaliParaList;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e){
            try {
                if (e.RowIndex >= 0) {
                    switch (dataGridView1.Columns[e.ColumnIndex].Name){
                        case "ParaTeachBtn":
                            CaliParam NowCaliPara = CaliParaManager.Instance.CaliParaList[e.RowIndex];
                            FrmCaliPara frmCali;
                            switch (NowCaliPara.caliModel) {
                                case CaliModelEnum.UpDnCamCali:
                                    FrmUpDnCamCali  frmCaliNow = new FrmUpDnCamCali(NowCaliPara);
                                    frmCaliNow.ShowDialog();
                                    if (frmCaliNow.IsSavePara) CaliParaManager.Instance.CaliParaList[e.RowIndex] = frmCaliNow.GetTeachedCaliPara();
                                    break;
                                case CaliModelEnum.HandEyeCali:
                                    frmCali = new FrmCaliPara(NowCaliPara);
                                    frmCali.ShowDialog();
                                    if (frmCali.IsSavePara)  CaliParaManager.Instance.CaliParaList[e.RowIndex] = frmCali.GetTeachedCaliPara();
                                    break;
                                case CaliModelEnum.Cali9PtCali:
                                    FrmCali9PtPara frmCali1 = new FrmCali9PtPara(NowCaliPara);
                                    frmCali1.ShowDialog();
                                    if (frmCali1.IsSavePara)  CaliParaManager.Instance.CaliParaList[e.RowIndex] = frmCali1.GetTeachedCaliPara();
                                    break;
                                default:
                                    frmCali = new FrmCaliPara(NowCaliPara);
                                    break;
                            }
                            break;                   
                    }
                }
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CaliParaManager.Instance.Save();
            dataGridView1.Enabled = false;
        }

        private void FrmCaliParaManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult TS = MessageBox.Show("请勿关闭此页面，点“否”取消关闭", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (TS == DialogResult.Yes) e.Cancel = false;
            else e.Cancel = true;
        }
    }
}
