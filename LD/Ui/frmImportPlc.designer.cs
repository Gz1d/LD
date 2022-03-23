namespace LD.Ui
{
    partial class frmImportPlc
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportPlc));
            this.cboSheet = new System.Windows.Forms.ComboBox();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.dgvExcelData = new System.Windows.Forms.DataGridView();
            this.ckbColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ckbAllSelect = new System.Windows.Forms.CheckBox();
            this.btnClearList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelData)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboSheet
            // 
            this.cboSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSheet.FormattingEnabled = true;
            this.cboSheet.Location = new System.Drawing.Point(277, 570);
            this.cboSheet.Name = "cboSheet";
            this.cboSheet.Size = new System.Drawing.Size(167, 20);
            this.cboSheet.TabIndex = 0;
            this.cboSheet.SelectedIndexChanged += new System.EventHandler(this.cboSheet_SelectedIndexChanged);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFile.Location = new System.Drawing.Point(569, 568);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.Text = "LoadFile";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(656, 568);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // dgvExcelData
            // 
            this.dgvExcelData.AllowUserToAddRows = false;
            this.dgvExcelData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExcelData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvExcelData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcelData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ckbColumn});
            this.dgvExcelData.Location = new System.Drawing.Point(3, 3);
            this.dgvExcelData.Name = "dgvExcelData";
            this.dgvExcelData.RowTemplate.Height = 23;
            this.dgvExcelData.Size = new System.Drawing.Size(732, 537);
            this.dgvExcelData.TabIndex = 0;
            this.dgvExcelData.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExcelData_CellValueChanged);
            this.dgvExcelData.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvExcelData_CurrentCellDirtyStateChanged);
            // 
            // ckbColumn
            // 
            this.ckbColumn.HeaderText = "Select";
            this.ckbColumn.Name = "ckbColumn";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(746, 608);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvExcelData);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(738, 582);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Data";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ckbAllSelect
            // 
            this.ckbAllSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbAllSelect.AutoSize = true;
            this.ckbAllSelect.Location = new System.Drawing.Point(12, 574);
            this.ckbAllSelect.Name = "ckbAllSelect";
            this.ckbAllSelect.Size = new System.Drawing.Size(78, 16);
            this.ckbAllSelect.TabIndex = 5;
            this.ckbAllSelect.Text = "AllSelect";
            this.ckbAllSelect.UseVisualStyleBackColor = true;
            this.ckbAllSelect.CheckedChanged += new System.EventHandler(this.ckbAllSelect_CheckedChanged);
            // 
            // btnClearList
            // 
            this.btnClearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearList.Location = new System.Drawing.Point(471, 568);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(86, 23);
            this.btnClearList.TabIndex = 1;
            this.btnClearList.Text = "ClearImport";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // frmImportPlc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(746, 608);
            this.Controls.Add(this.ckbAllSelect);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.cboSheet);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.tabControl2);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImportPlc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelData)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboSheet;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridView dgvExcelData;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox ckbAllSelect;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ckbColumn;
    }
}

