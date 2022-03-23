namespace LD.Ui
{
    partial class frmLog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.machineIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkisActiveTemp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.stationIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.captionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isPlc = new System.Windows.Forms.DataGridViewImageColumn();
            this.isMes = new System.Windows.Forms.DataGridViewImageColumn();
            this.isCard = new System.Windows.Forms.DataGridViewImageColumn();
            this.isFrid = new System.Windows.Forms.DataGridViewImageColumn();
            this.productCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkisActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkRunlog = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvRunlog = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runlogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunlog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.runlogBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // machineIDDataGridViewTextBoxColumn
            // 
            this.machineIDDataGridViewTextBoxColumn.DataPropertyName = "MachineID";
            resources.ApplyResources(this.machineIDDataGridViewTextBoxColumn, "machineIDDataGridViewTextBoxColumn");
            this.machineIDDataGridViewTextBoxColumn.Name = "machineIDDataGridViewTextBoxColumn";
            // 
            // chkisActiveTemp
            // 
            this.chkisActiveTemp.DataPropertyName = "IsActiveTemp";
            resources.ApplyResources(this.chkisActiveTemp, "chkisActiveTemp");
            this.chkisActiveTemp.Name = "chkisActiveTemp";
            // 
            // stationIDDataGridViewTextBoxColumn
            // 
            this.stationIDDataGridViewTextBoxColumn.DataPropertyName = "StationID";
            resources.ApplyResources(this.stationIDDataGridViewTextBoxColumn, "stationIDDataGridViewTextBoxColumn");
            this.stationIDDataGridViewTextBoxColumn.Name = "stationIDDataGridViewTextBoxColumn";
            // 
            // captionDataGridViewTextBoxColumn
            // 
            this.captionDataGridViewTextBoxColumn.DataPropertyName = "Caption";
            resources.ApplyResources(this.captionDataGridViewTextBoxColumn, "captionDataGridViewTextBoxColumn");
            this.captionDataGridViewTextBoxColumn.Name = "captionDataGridViewTextBoxColumn";
            // 
            // descripDataGridViewTextBoxColumn
            // 
            this.descripDataGridViewTextBoxColumn.DataPropertyName = "Descrip";
            resources.ApplyResources(this.descripDataGridViewTextBoxColumn, "descripDataGridViewTextBoxColumn");
            this.descripDataGridViewTextBoxColumn.Name = "descripDataGridViewTextBoxColumn";
            // 
            // isPlc
            // 
            this.isPlc.DataPropertyName = "IsPlc";
            resources.ApplyResources(this.isPlc, "isPlc");
            this.isPlc.Name = "isPlc";
            this.isPlc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isPlc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // isMes
            // 
            this.isMes.DataPropertyName = "IsMes";
            resources.ApplyResources(this.isMes, "isMes");
            this.isMes.Name = "isMes";
            this.isMes.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isMes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // isCard
            // 
            this.isCard.DataPropertyName = "IsCard";
            resources.ApplyResources(this.isCard, "isCard");
            this.isCard.Name = "isCard";
            this.isCard.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isCard.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // isFrid
            // 
            this.isFrid.DataPropertyName = "IsFrid";
            resources.ApplyResources(this.isFrid, "isFrid");
            this.isFrid.Name = "isFrid";
            this.isFrid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isFrid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // productCountDataGridViewTextBoxColumn
            // 
            this.productCountDataGridViewTextBoxColumn.DataPropertyName = "ProductCount";
            resources.ApplyResources(this.productCountDataGridViewTextBoxColumn, "productCountDataGridViewTextBoxColumn");
            this.productCountDataGridViewTextBoxColumn.Name = "productCountDataGridViewTextBoxColumn";
            // 
            // chkisActive
            // 
            this.chkisActive.DataPropertyName = "IsActive";
            resources.ApplyResources(this.chkisActive, "chkisActive");
            this.chkisActive.Name = "chkisActive";
            // 
            // tagDataGridViewTextBoxColumn
            // 
            this.tagDataGridViewTextBoxColumn.DataPropertyName = "Tag";
            resources.ApplyResources(this.tagDataGridViewTextBoxColumn, "tagDataGridViewTextBoxColumn");
            this.tagDataGridViewTextBoxColumn.Name = "tagDataGridViewTextBoxColumn";
            // 
            // btnDelete
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "删除";
            this.btnDelete.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "删除";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.dgvRunlog, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.btnOpen, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnClear, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.chkRunlog, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkRunlog
            // 
            resources.ApplyResources(this.chkRunlog, "chkRunlog");
            this.chkRunlog.Name = "chkRunlog";
            this.chkRunlog.UseVisualStyleBackColor = true;
            this.chkRunlog.CheckedChanged += new System.EventHandler(this.chkRunlog_CheckedChanged);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvRunlog
            // 
            this.dgvRunlog.AllowUserToAddRows = false;
            this.dgvRunlog.AllowUserToDeleteRows = false;
            this.dgvRunlog.AutoGenerateColumns = false;
            this.dgvRunlog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRunlog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.machineIDDataGridViewTextBoxColumn1,
            this.deviceDataGridViewTextBoxColumn,
            this.notesDataGridViewTextBoxColumn});
            this.dgvRunlog.DataSource = this.runlogBindingSource;
            resources.ApplyResources(this.dgvRunlog, "dgvRunlog");
            this.dgvRunlog.Name = "dgvRunlog";
            this.dgvRunlog.RowTemplate.Height = 23;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            resources.ApplyResources(this.Time, "Time");
            this.Time.Name = "Time";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "black.png");
            this.imageList1.Images.SetKeyName(1, "green.png");
            this.imageList1.Images.SetKeyName(2, "red.png");
            this.imageList1.Images.SetKeyName(3, "white.png");
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Tag";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // machineIDDataGridViewTextBoxColumn1
            // 
            this.machineIDDataGridViewTextBoxColumn1.DataPropertyName = "MachineID";
            resources.ApplyResources(this.machineIDDataGridViewTextBoxColumn1, "machineIDDataGridViewTextBoxColumn1");
            this.machineIDDataGridViewTextBoxColumn1.Name = "machineIDDataGridViewTextBoxColumn1";
            // 
            // deviceDataGridViewTextBoxColumn
            // 
            this.deviceDataGridViewTextBoxColumn.DataPropertyName = "Device";
            resources.ApplyResources(this.deviceDataGridViewTextBoxColumn, "deviceDataGridViewTextBoxColumn");
            this.deviceDataGridViewTextBoxColumn.Name = "deviceDataGridViewTextBoxColumn";
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            resources.ApplyResources(this.notesDataGridViewTextBoxColumn, "notesDataGridViewTextBoxColumn");
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            // 
            // runlogBindingSource
            // 
            this.runlogBindingSource.DataSource = typeof(LD.Log.Runlog);
            // 
            // frmLog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "frmLog";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLog_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunlog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.runlogBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource runlogBindingSource;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvRunlog;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkRunlog;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkisActiveTemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn captionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn isPlc;
        private System.Windows.Forms.DataGridViewImageColumn isMes;
        private System.Windows.Forms.DataGridViewImageColumn isCard;
        private System.Windows.Forms.DataGridViewImageColumn isFrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn productCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkisActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
    }
}