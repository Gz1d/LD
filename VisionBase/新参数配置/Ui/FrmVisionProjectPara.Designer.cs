
namespace VisionBase
{
    partial class FrmVisionProjectPara
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ProjectDgv = new System.Windows.Forms.DataGridView();
            this.NoTbxCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescribeTbxCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectModelCbxCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ProjectParaClearBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ProjectTeachBtnCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ProjectSaveBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ProjectDeleteBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.ProjectListTeachBtn = new System.Windows.Forms.Button();
            this.ProjectListSaveBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.VisionLocalParaDgv = new System.Windows.Forms.DataGridView();
            this.LocalNoTbXCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CameraLightTeachBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ProjectVisionEnumCbx = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CaliMatCbx = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LoclalModelCbxCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LocalParaTeachBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.VisionLocalSaveBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.VisionLocalDeleteBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DgvTB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.LocalParaSeverOnBtn = new System.Windows.Forms.Button();
            this.AddVisioinParaBtn = new System.Windows.Forms.Button();
            this.VisionParaSaveBtn = new System.Windows.Forms.Button();
            this.TeachGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectDgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VisionLocalParaDgv)).BeginInit();
            this.TeachGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProjectDgv
            // 
            this.ProjectDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProjectDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoTbxCol,
            this.DescribeTbxCol,
            this.ProjectModelCbxCol,
            this.ProjectParaClearBtn,
            this.ProjectTeachBtnCol,
            this.ProjectSaveBtn,
            this.ProjectDeleteBtn});
            this.ProjectDgv.Location = new System.Drawing.Point(2, 42);
            this.ProjectDgv.Name = "ProjectDgv";
            this.ProjectDgv.RowTemplate.Height = 23;
            this.ProjectDgv.Size = new System.Drawing.Size(811, 228);
            this.ProjectDgv.TabIndex = 0;
            this.ProjectDgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProjectDgv_CellContentClick);
            // 
            // NoTbxCol
            // 
            this.NoTbxCol.HeaderText = "编号";
            this.NoTbxCol.Name = "NoTbxCol";
            // 
            // DescribeTbxCol
            // 
            this.DescribeTbxCol.HeaderText = "描述";
            this.DescribeTbxCol.Name = "DescribeTbxCol";
            // 
            // ProjectModelCbxCol
            // 
            this.ProjectModelCbxCol.HeaderText = "视觉项目类型";
            this.ProjectModelCbxCol.Name = "ProjectModelCbxCol";
            this.ProjectModelCbxCol.Width = 150;
            // 
            // ProjectParaClearBtn
            // 
            this.ProjectParaClearBtn.HeaderText = "参数清除";
            this.ProjectParaClearBtn.Name = "ProjectParaClearBtn";
            // 
            // ProjectTeachBtnCol
            // 
            this.ProjectTeachBtnCol.HeaderText = "工程参数示教";
            this.ProjectTeachBtnCol.Name = "ProjectTeachBtnCol";
            // 
            // ProjectSaveBtn
            // 
            this.ProjectSaveBtn.HeaderText = "参数保存";
            this.ProjectSaveBtn.Name = "ProjectSaveBtn";
            // 
            // ProjectDeleteBtn
            // 
            this.ProjectDeleteBtn.HeaderText = "工程删除";
            this.ProjectDeleteBtn.Name = "ProjectDeleteBtn";
            this.ProjectDeleteBtn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProjectDeleteBtn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(295, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "工程参数示教表";
            // 
            // ProjectListTeachBtn
            // 
            this.ProjectListTeachBtn.Location = new System.Drawing.Point(853, 42);
            this.ProjectListTeachBtn.Name = "ProjectListTeachBtn";
            this.ProjectListTeachBtn.Size = new System.Drawing.Size(95, 47);
            this.ProjectListTeachBtn.TabIndex = 2;
            this.ProjectListTeachBtn.Text = "工程集示教";
            this.ProjectListTeachBtn.UseVisualStyleBackColor = true;
            // 
            // ProjectListSaveBtn
            // 
            this.ProjectListSaveBtn.Location = new System.Drawing.Point(853, 135);
            this.ProjectListSaveBtn.Name = "ProjectListSaveBtn";
            this.ProjectListSaveBtn.Size = new System.Drawing.Size(95, 47);
            this.ProjectListSaveBtn.TabIndex = 2;
            this.ProjectListSaveBtn.Text = "工程集保存";
            this.ProjectListSaveBtn.UseVisualStyleBackColor = true;
            this.ProjectListSaveBtn.Click += new System.EventHandler(this.ProjectListSaveBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1037, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "增加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // VisionLocalParaDgv
            // 
            this.VisionLocalParaDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VisionLocalParaDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocalNoTbXCol,
            this.CameraLightTeachBtn,
            this.ProjectVisionEnumCbx,
            this.CaliMatCbx,
            this.LoclalModelCbxCol,
            this.LocalParaTeachBtn,
            this.VisionLocalSaveBtn,
            this.VisionLocalDeleteBtn,
            this.DgvTB});
            this.VisionLocalParaDgv.Location = new System.Drawing.Point(0, 330);
            this.VisionLocalParaDgv.Name = "VisionLocalParaDgv";
            this.VisionLocalParaDgv.RowTemplate.Height = 23;
            this.VisionLocalParaDgv.Size = new System.Drawing.Size(813, 360);
            this.VisionLocalParaDgv.TabIndex = 3;
            this.VisionLocalParaDgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VisionLocalParaDgv_CellContentClick);
            // 
            // LocalNoTbXCol
            // 
            this.LocalNoTbXCol.HeaderText = "编号";
            this.LocalNoTbXCol.Name = "LocalNoTbXCol";
            this.LocalNoTbXCol.Width = 30;
            // 
            // CameraLightTeachBtn
            // 
            this.CameraLightTeachBtn.HeaderText = "相机光源示教";
            this.CameraLightTeachBtn.Name = "CameraLightTeachBtn";
            // 
            // ProjectVisionEnumCbx
            // 
            this.ProjectVisionEnumCbx.HeaderText = "视觉枚举";
            this.ProjectVisionEnumCbx.Name = "ProjectVisionEnumCbx";
            this.ProjectVisionEnumCbx.Width = 150;
            // 
            // CaliMatCbx
            // 
            this.CaliMatCbx.HeaderText = "矩阵示教";
            this.CaliMatCbx.Name = "CaliMatCbx";
            this.CaliMatCbx.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // LoclalModelCbxCol
            // 
            this.LoclalModelCbxCol.HeaderText = "定位模式枚举";
            this.LoclalModelCbxCol.Name = "LoclalModelCbxCol";
            this.LoclalModelCbxCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // LocalParaTeachBtn
            // 
            this.LocalParaTeachBtn.HeaderText = "视觉定位示教";
            this.LocalParaTeachBtn.Name = "LocalParaTeachBtn";
            // 
            // VisionLocalSaveBtn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "参数保存";
            this.VisionLocalSaveBtn.DefaultCellStyle = dataGridViewCellStyle2;
            this.VisionLocalSaveBtn.HeaderText = "参数保存";
            this.VisionLocalSaveBtn.Name = "VisionLocalSaveBtn";
            // 
            // VisionLocalDeleteBtn
            // 
            this.VisionLocalDeleteBtn.HeaderText = "定位参数删除";
            this.VisionLocalDeleteBtn.Name = "VisionLocalDeleteBtn";
            // 
            // DgvTB
            // 
            this.DgvTB.HeaderText = "描述";
            this.DgvTB.Name = "DgvTB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(318, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "定位参数示教表";
            // 
            // LocalParaSeverOnBtn
            // 
            this.LocalParaSeverOnBtn.Location = new System.Drawing.Point(34, 29);
            this.LocalParaSeverOnBtn.Name = "LocalParaSeverOnBtn";
            this.LocalParaSeverOnBtn.Size = new System.Drawing.Size(95, 47);
            this.LocalParaSeverOnBtn.TabIndex = 2;
            this.LocalParaSeverOnBtn.Text = "定位参数示教";
            this.LocalParaSeverOnBtn.UseVisualStyleBackColor = true;
            this.LocalParaSeverOnBtn.Click += new System.EventHandler(this.LocalParaSeverOnBtn_Click);
            // 
            // AddVisioinParaBtn
            // 
            this.AddVisioinParaBtn.Enabled = false;
            this.AddVisioinParaBtn.Location = new System.Drawing.Point(218, 29);
            this.AddVisioinParaBtn.Name = "AddVisioinParaBtn";
            this.AddVisioinParaBtn.Size = new System.Drawing.Size(95, 47);
            this.AddVisioinParaBtn.TabIndex = 2;
            this.AddVisioinParaBtn.Text = "增加定位";
            this.AddVisioinParaBtn.UseVisualStyleBackColor = true;
            this.AddVisioinParaBtn.Click += new System.EventHandler(this.AddVisioinParaBtn_Click);
            // 
            // VisionParaSaveBtn
            // 
            this.VisionParaSaveBtn.Enabled = false;
            this.VisionParaSaveBtn.Location = new System.Drawing.Point(218, 87);
            this.VisionParaSaveBtn.Name = "VisionParaSaveBtn";
            this.VisionParaSaveBtn.Size = new System.Drawing.Size(95, 47);
            this.VisionParaSaveBtn.TabIndex = 2;
            this.VisionParaSaveBtn.Text = "定位参数保存";
            this.VisionParaSaveBtn.UseVisualStyleBackColor = true;
            this.VisionParaSaveBtn.Click += new System.EventHandler(this.VisionParaSaveBtn_Click);
            // 
            // TeachGroupBox
            // 
            this.TeachGroupBox.Controls.Add(this.LocalParaSeverOnBtn);
            this.TeachGroupBox.Controls.Add(this.AddVisioinParaBtn);
            this.TeachGroupBox.Controls.Add(this.VisionParaSaveBtn);
            this.TeachGroupBox.Location = new System.Drawing.Point(819, 330);
            this.TeachGroupBox.Name = "TeachGroupBox";
            this.TeachGroupBox.Size = new System.Drawing.Size(328, 155);
            this.TeachGroupBox.TabIndex = 4;
            this.TeachGroupBox.TabStop = false;
            this.TeachGroupBox.Text = "定位示教";
            // 
            // FrmVisionProjectPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1169, 766);
            this.Controls.Add(this.TeachGroupBox);
            this.Controls.Add(this.VisionLocalParaDgv);
            this.Controls.Add(this.ProjectListSaveBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ProjectListTeachBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProjectDgv);
            this.Name = "FrmVisionProjectPara";
            this.TabText = "FrmVisionProjectPara";
            this.Text = "FrmVisionProjectPara";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVisionProjectPara_FormClosing);
            this.Load += new System.EventHandler(this.FrmVisionProjectPara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProjectDgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VisionLocalParaDgv)).EndInit();
            this.TeachGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ProjectDgv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ProjectListTeachBtn;
        private System.Windows.Forms.Button ProjectListSaveBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView VisionLocalParaDgv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LocalParaSeverOnBtn;
        private System.Windows.Forms.Button AddVisioinParaBtn;
        private System.Windows.Forms.Button VisionParaSaveBtn;
        private System.Windows.Forms.GroupBox TeachGroupBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoTbxCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescribeTbxCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProjectModelCbxCol;
        private System.Windows.Forms.DataGridViewButtonColumn ProjectParaClearBtn;
        private System.Windows.Forms.DataGridViewButtonColumn ProjectTeachBtnCol;
        private System.Windows.Forms.DataGridViewButtonColumn ProjectSaveBtn;
        private System.Windows.Forms.DataGridViewButtonColumn ProjectDeleteBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalNoTbXCol;
        private System.Windows.Forms.DataGridViewButtonColumn CameraLightTeachBtn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProjectVisionEnumCbx;
        private System.Windows.Forms.DataGridViewComboBoxColumn CaliMatCbx;
        private System.Windows.Forms.DataGridViewComboBoxColumn LoclalModelCbxCol;
        private System.Windows.Forms.DataGridViewButtonColumn LocalParaTeachBtn;
        private System.Windows.Forms.DataGridViewButtonColumn VisionLocalSaveBtn;
        private System.Windows.Forms.DataGridViewButtonColumn VisionLocalDeleteBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvTB;
    }
}