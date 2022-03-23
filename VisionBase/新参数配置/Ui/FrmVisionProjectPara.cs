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
    public partial class FrmVisionProjectPara : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FrmVisionProjectPara(){
            InitializeComponent();
        }


        private static object LockObj = new object();
        private static FrmVisionProjectPara MyFrmVisionProjectPara;

        public static FrmVisionProjectPara Instance
        {
            get{
                lock (LockObj) {
                    return MyFrmVisionProjectPara = MyFrmVisionProjectPara ?? new FrmVisionProjectPara();
                }
            }
        }
        ProjectPara TeachProjectPara = new ProjectPara();
        ProjectMsg TeachProjectMsg = new ProjectMsg();
        private int TeachProjectItem = 0;
        private void FrmVisionProjectPara_Load(object sender, EventArgs e)
        {
            this.ProjectModelCbxCol.Items.Clear();
            foreach (ProjectModelEnum item in Enum.GetValues(typeof(ProjectModelEnum))){
                this.ProjectModelCbxCol.Items.Add(item);
            }
            this.ProjectModelCbxCol.ValueType = typeof(ProjectModelEnum);
            //this.ProjectModelCbxCol.DataPropertyName = "EventSeverity";
            this.ProjectVisionEnumCbx.Items.Clear();
            foreach (ProjectVisionEnum temp in Enum.GetValues(typeof(ProjectVisionEnum))){
                this.ProjectVisionEnumCbx.Items.Add(temp);
            }
            this.ProjectVisionEnumCbx.ValueType = typeof(ProjectVisionEnum);
            //this.ProjectVisionEnumCbx.DataPropertyName = "EventSeverity";
            this.CaliMatCbx.Items.Clear();
            foreach (var temp in Enum.GetValues(typeof(CoordiCamHandEyeMatEnum))) {
                this.CaliMatCbx.Items.Add(temp);
            }
            this.CaliMatCbx.ValueType = typeof(CoordiCamHandEyeMatEnum);
            //this.CaliMatCbx.DataPropertyName = "EventSeverity";
            this.LoclalModelCbxCol.Items.Clear();
            foreach (LocalModelEnum item in Enum.GetValues(typeof(LocalModelEnum))){
                this.LoclalModelCbxCol.Items.Add(item);
            }
            this.LoclalModelCbxCol.ValueType = typeof(LocalModelEnum);
            //this.LocalNoTbXCol.ValueType = typeof(LocalModelEnum);
            //this.LocalNoTbXCol.DataPropertyName = "EventSeverity";
            UpDataProjectDgv();
            TeachProjectPara = ProjectParaManager.Instance.ProjectParaList[TeachProjectItem];
            TeachProjectMsg = ProjectParaManager.Instance.ProjectMsgList[TeachProjectItem];
            UpDataVisionLocalParaDgv();
        }

        private void ProjectDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0||e.RowIndex>=ProjectParaManager.Instance.ProjectMsgList.Count()) return;
            if (ProjectParaManager.Instance.ProjectMsgList == null) return;
            if (ProjectParaManager.Instance.ProjectMsgList.Count() == 0) return;
         
            if (e.ColumnIndex == 2)//视觉项目类型
            {
                ProjectParaManager.Instance.ProjectMsgList[e.RowIndex].ProjectModel = (ProjectModelEnum)ProjectDgv.Rows[e.RowIndex].Cells[2].Value;
            }
            if (e.ColumnIndex == 3) //参数清除 
            {
               DialogResult  Rlt= MessageBox.Show("是否清除参数","工程参数清除",MessageBoxButtons.YesNo);
                if (Rlt ==DialogResult.No) return;
                ProjectModelEnum obj0 = (ProjectModelEnum)ProjectDgv.Rows[e.RowIndex].Cells[e.ColumnIndex-1].Value; 
                if(Rlt == DialogResult.Yes) {
                    if (e.RowIndex < ProjectParaManager.Instance.ProjectMsgList.Count())
                          ProjectParaManager.Instance.ProjectParaList[e.RowIndex] = new ProjectPara(obj0);
                    else  ProjectParaManager.Instance.ProjectParaList.Add(new ProjectPara(obj0));
                }
            }
            if (e.ColumnIndex == 4)//工程参数示教
            {
                if (ProjectParaManager.Instance.ProjectParaList.Count == 0) return;
                if (ProjectParaManager.Instance.ProjectParaList.Count > e.RowIndex){ 
                    TeachProjectPara = ProjectParaManager.Instance.ProjectParaList[e.RowIndex];
                    UpDataVisionLocalParaDgv();
                }
            }
            if (e.ColumnIndex == 5)//参数保存
            {              
                DialogResult Rlt = MessageBox.Show("是否保存参数", "工程参数保存", MessageBoxButtons.YesNo);
                if (Rlt == DialogResult.No) return;
                ProjectParaManager.Instance.ProjectMsgList[e.RowIndex].ProjectDescribe = ProjectDgv.Rows[e.RowIndex].Cells[1].Value.ToString(); 
                ProjectParaManager.Instance.Save();
            }
            if (e.ColumnIndex == 6)//参数删除
            {
                ProjectParaManager.Instance.ProjectParaList.RemoveAt(e.RowIndex);
                ProjectParaManager.Instance.ProjectMsgList.RemoveAt(e.RowIndex);
                UpDataProjectDgv();
            }
            TeachProjectItem = e.RowIndex;
            if (e.RowIndex < ProjectParaManager.Instance.ProjectParaList.Count){
                TeachProjectMsg = ProjectParaManager.Instance.ProjectMsgList[e.RowIndex];
                TeachProjectPara = ProjectParaManager.Instance.ProjectParaList[e.RowIndex];
                UpDataVisionLocalParaDgv();
            }

        }
        private void VisionLocalParaDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!(e.RowIndex < TeachProjectPara.VisionParas.Count())) return;
            if (e.ColumnIndex == 1)//光源参数示教；
            {
                FrmCamLightCtrl frmCamLightDlg = new FrmCamLightCtrl(TeachProjectPara.VisionParas[e.RowIndex].camLightPara);
                frmCamLightDlg.ShowDialog();             
                TeachProjectPara.VisionParas[e.RowIndex].camLightPara = frmCamLightDlg.GetTeachPara();
            }
            if (e.ColumnIndex == 2) //视觉枚举，用来表述工程第几组视觉参数，
            {
                TeachProjectPara.VisionParas[e.RowIndex].ProjectVisionItem 
                    = (ProjectVisionEnum)VisionLocalParaDgv.Rows[e.RowIndex].Cells[2].Value;
            }
            if (e.ColumnIndex == 3) //标定矩阵示教
            {
                TeachProjectPara.VisionParas[e.RowIndex].localPara.localSetting.CoordiCam
                    = (CoordiCamHandEyeMatEnum)VisionLocalParaDgv.Rows[e.RowIndex].Cells[3].Value;
            }
            if (e.ColumnIndex == 4)  //定位模式示教
            {
                TeachProjectPara.VisionParas[e.RowIndex].localPara.localSetting.localModel 
                    = (LocalModelEnum)VisionLocalParaDgv.Rows[e.RowIndex].Cells[4].Value;
            }
            if(e.ColumnIndex ==5) //视觉参数示教
            {
                //将视觉参数传给视觉示教界面，
                TeachProjectPara.VisionParas[e.RowIndex].localPara.localSetting.localModel
                    = (LocalModelEnum)VisionLocalParaDgv.Rows[e.RowIndex].Cells[4].Value;
                FrmLocalParaTeach FrmLocal = new FrmLocalParaTeach(TeachProjectPara.VisionParas[e.RowIndex].localPara);
                FrmLocal.ShowDialog();
                if (FrmLocal.IsSaveVisionPara)
                     TeachProjectPara.VisionParas[e.RowIndex].localPara = FrmLocal.GetTeachLocalPara();
            }
            if (e.ColumnIndex == 6) //定位参数保存；
            {
                TeachProjectPara.VisionParas[e.RowIndex].ProjectVisionItem 
                    = (ProjectVisionEnum)VisionLocalParaDgv.Rows[e.RowIndex].Cells[2].Value;
                TeachProjectPara.VisionParas[e.RowIndex].localPara.localSetting.CoordiCam
                    =  (CoordiCamHandEyeMatEnum)VisionLocalParaDgv.Rows[e.RowIndex].Cells[3].Value;
                TeachProjectPara.VisionParas[e.RowIndex].localPara.localSetting.localModel 
                    = (LocalModelEnum)VisionLocalParaDgv.Rows[e.RowIndex].Cells[4].Value;
                TeachProjectPara.VisionParas[e.RowIndex].ProjectVisionName = VisionLocalParaDgv.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            if (e.ColumnIndex == 7) //删除当前组的视觉参数；
            {
                TeachProjectPara.VisionParas.RemoveAt(e.RowIndex);
                TeachProjectPara.ProjectVisionNames.RemoveAt(e.RowIndex);
            }
            if (e.ColumnIndex == 8)//保存描述参数
            {
                return;
            }
            UpDataVisionLocalParaDgv();
        }
        public void UpDataProjectDgv()
        {
            DataGridView dgvView = ProjectDgv;
            int Index = 0;
            dgvView.Rows.Clear();
            if (ProjectParaManager.Instance.ProjectMsgList == null){
                ProjectParaManager.Instance.ProjectMsgList = new List<ProjectMsg>();
                return;
            }
            for (int i = 0; i < ProjectParaManager.Instance.ProjectMsgList.Count; i++) {
                dgvView.Rows.Add(
                    ++Index
                    , ProjectParaManager.Instance.ProjectMsgList[i].ProjectDescribe
                    , ProjectParaManager.Instance.ProjectMsgList[i].ProjectModel
                    ,"清除"
                    ,"示教"
                    ,"保存"
                    ,"删除"
                    ) ;          
            }
        }


        public void UpDataVisionLocalParaDgv()
        {
            DataGridView dgvView2 = VisionLocalParaDgv;
            int Index = 0;
            dgvView2.Rows.Clear();
            for (int i = 0; i < TeachProjectPara.ProjectVisionNames.Count(); i++){
                dgvView2.Rows.Add(
                    ++Index,
                    "图片示教",
                    TeachProjectPara.VisionParas[i].ProjectVisionItem,
                    TeachProjectPara.VisionParas[i].localPara.localSetting.CoordiCam,
                    TeachProjectPara.VisionParas[i].localPara.localSetting.localModel,
                    "定位参数示教",
                    "保存",
                    " 删除行",
                     TeachProjectPara.VisionParas[i].ProjectVisionName
                    ) ;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            ProjectParaManager.Instance.ProjectMsgList.Add(new ProjectMsg());
            ProjectParaManager.Instance.ProjectParaList.Add(new ProjectPara());
            UpDataProjectDgv();
        }

        private void ProjectListSaveBtn_Click(object sender, EventArgs e){
            DialogResult Rlt = MessageBox.Show("是否保存参数", "工程参数保存", MessageBoxButtons.YesNo);
            if (Rlt == DialogResult.No) return;
            ProjectParaManager.Instance.Save();
        }
        private void LocalParaSeverOnBtn_Click(object sender, EventArgs e){
            LocalParaSeverOnBtn.Enabled = false;
            AddVisioinParaBtn.Enabled = true;
            VisionParaSaveBtn.Enabled = true;
        }

        private void AddVisioinParaBtn_Click(object sender, EventArgs e)  {
            TeachProjectPara.VisionParas.Add(new VisionPara());
            TeachProjectPara.ProjectVisionNames.Add( ProjectVisionEnum.ProjectVision0);
            UpDataVisionLocalParaDgv();
        }

        private void VisionParaSaveBtn_Click(object sender, EventArgs e) {
            DialogResult DlgReslult = MessageBox.Show("是否关闭程序", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DlgReslult == DialogResult.No) return;
            ProjectParaManager.Instance.ProjectMsgList[TeachProjectItem] = TeachProjectMsg;
            ProjectParaManager.Instance.ProjectParaList[TeachProjectItem] = TeachProjectPara;
            VisionParaSaveBtn.Enabled = false;
            AddVisioinParaBtn.Enabled = false;
            LocalParaSeverOnBtn.Enabled = true;
        }

        private void FrmVisionProjectPara_FormClosing(object sender, FormClosingEventArgs e){
            DialogResult TS = MessageBox.Show("请勿关闭此页面，点“否”取消关闭", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (TS == DialogResult.Yes) e.Cancel = false;
            else   e.Cancel = true;
        }
    }
}
