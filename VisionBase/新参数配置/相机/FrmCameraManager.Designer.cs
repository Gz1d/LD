
namespace VisionBase
{
    partial class FrmCameraManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCameraManager));
            this.CamParaDgv = new System.Windows.Forms.DataGridView();
            this.cameraNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CameInterfaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.camDecribeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CcfPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Height = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.triggerModelDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isMirrorXDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isMirrorYDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isRotDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isOpen = new System.Windows.Forms.DataGridViewImageColumn();
            this.DeleteBtnDgvCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.OpenCamBtnDgvCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.GrabImgDgvCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CameraParaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.CamParaDgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraParaBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CamParaDgv
            // 
            this.CamParaDgv.AutoGenerateColumns = false;
            this.CamParaDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cameraNameDataGridViewTextBoxColumn,
            this.CameInterfaceDataGridViewTextBoxColumn,
            this.camDecribeDataGridViewTextBoxColumn,
            this.ServerName,
            this.DeviceName,
            this.CcfPath,
            this.Width,
            this.Height,
            this.triggerModelDataGridViewCheckBoxColumn,
            this.isMirrorXDataGridViewCheckBoxColumn,
            this.isMirrorYDataGridViewCheckBoxColumn,
            this.isRotDataGridViewCheckBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn,
            this.isOpen,
            this.DeleteBtnDgvCol,
            this.OpenCamBtnDgvCol,
            this.GrabImgDgvCol});
            this.CamParaDgv.DataSource = this.CameraParaBindingSource;
            this.CamParaDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CamParaDgv.Location = new System.Drawing.Point(3, 532);
            this.CamParaDgv.Name = "CamParaDgv";
            this.CamParaDgv.RowTemplate.Height = 23;
            this.CamParaDgv.Size = new System.Drawing.Size(1119, 220);
            this.CamParaDgv.TabIndex = 0;
            this.CamParaDgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CamParaDgv_CellContentClick);
            this.CamParaDgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CamParaDgv_CellFormatting);
            // 
            // cameraNameDataGridViewTextBoxColumn
            // 
            this.cameraNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cameraNameDataGridViewTextBoxColumn.DataPropertyName = "CameraName";
            this.cameraNameDataGridViewTextBoxColumn.HeaderText = "CameraName";
            this.cameraNameDataGridViewTextBoxColumn.Name = "cameraNameDataGridViewTextBoxColumn";
            this.cameraNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cameraNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // CameInterfaceDataGridViewTextBoxColumn
            // 
            this.CameInterfaceDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CameInterfaceDataGridViewTextBoxColumn.DataPropertyName = "CameInterface";
            this.CameInterfaceDataGridViewTextBoxColumn.HeaderText = "CameInterface";
            this.CameInterfaceDataGridViewTextBoxColumn.Name = "CameInterfaceDataGridViewTextBoxColumn";
            this.CameInterfaceDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CameInterfaceDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // camDecribeDataGridViewTextBoxColumn
            // 
            this.camDecribeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.camDecribeDataGridViewTextBoxColumn.DataPropertyName = "CamDecribe";
            this.camDecribeDataGridViewTextBoxColumn.HeaderText = "CamDecribe";
            this.camDecribeDataGridViewTextBoxColumn.Name = "camDecribeDataGridViewTextBoxColumn";
            // 
            // ServerName
            // 
            this.ServerName.DataPropertyName = "ServerName";
            this.ServerName.HeaderText = "ServerName";
            this.ServerName.Name = "ServerName";
            // 
            // DeviceName
            // 
            this.DeviceName.DataPropertyName = "DeviceName";
            this.DeviceName.HeaderText = "DeviceName";
            this.DeviceName.Name = "DeviceName";
            // 
            // CcfPath
            // 
            this.CcfPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CcfPath.DataPropertyName = "CcfPath";
            this.CcfPath.HeaderText = "CcfPath";
            this.CcfPath.Name = "CcfPath";
            // 
            // Width
            // 
            this.Width.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Width.DataPropertyName = "Width";
            this.Width.HeaderText = "Width";
            this.Width.Name = "Width";
            // 
            // Height
            // 
            this.Height.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Height.DataPropertyName = "Height";
            this.Height.HeaderText = "Height";
            this.Height.Name = "Height";
            // 
            // triggerModelDataGridViewCheckBoxColumn
            // 
            this.triggerModelDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.triggerModelDataGridViewCheckBoxColumn.DataPropertyName = "TriggerModel";
            this.triggerModelDataGridViewCheckBoxColumn.HeaderText = "TriggerModel";
            this.triggerModelDataGridViewCheckBoxColumn.Name = "triggerModelDataGridViewCheckBoxColumn";
            // 
            // isMirrorXDataGridViewCheckBoxColumn
            // 
            this.isMirrorXDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isMirrorXDataGridViewCheckBoxColumn.DataPropertyName = "IsMirrorX";
            this.isMirrorXDataGridViewCheckBoxColumn.HeaderText = "IsMirrorX";
            this.isMirrorXDataGridViewCheckBoxColumn.Name = "isMirrorXDataGridViewCheckBoxColumn";
            // 
            // isMirrorYDataGridViewCheckBoxColumn
            // 
            this.isMirrorYDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isMirrorYDataGridViewCheckBoxColumn.DataPropertyName = "IsMirrorY";
            this.isMirrorYDataGridViewCheckBoxColumn.HeaderText = "IsMirrorY";
            this.isMirrorYDataGridViewCheckBoxColumn.Name = "isMirrorYDataGridViewCheckBoxColumn";
            // 
            // isRotDataGridViewCheckBoxColumn
            // 
            this.isRotDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isRotDataGridViewCheckBoxColumn.DataPropertyName = "IsRot";
            this.isRotDataGridViewCheckBoxColumn.HeaderText = "IsRot";
            this.isRotDataGridViewCheckBoxColumn.Name = "isRotDataGridViewCheckBoxColumn";
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            // 
            // isOpen
            // 
            this.isOpen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isOpen.DataPropertyName = "IsOpen";
            this.isOpen.HeaderText = "IsOpen";
            this.isOpen.Name = "isOpen";
            this.isOpen.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DeleteBtnDgvCol
            // 
            this.DeleteBtnDgvCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "删除";
            this.DeleteBtnDgvCol.DefaultCellStyle = dataGridViewCellStyle1;
            this.DeleteBtnDgvCol.HeaderText = "";
            this.DeleteBtnDgvCol.Name = "DeleteBtnDgvCol";
            this.DeleteBtnDgvCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DeleteBtnDgvCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DeleteBtnDgvCol.Text = "删除";
            this.DeleteBtnDgvCol.UseColumnTextForButtonValue = true;
            // 
            // OpenCamBtnDgvCol
            // 
            this.OpenCamBtnDgvCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "打开相机";
            this.OpenCamBtnDgvCol.DefaultCellStyle = dataGridViewCellStyle2;
            this.OpenCamBtnDgvCol.HeaderText = "";
            this.OpenCamBtnDgvCol.Name = "OpenCamBtnDgvCol";
            this.OpenCamBtnDgvCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OpenCamBtnDgvCol.Text = "打开相机";
            this.OpenCamBtnDgvCol.UseColumnTextForButtonValue = true;
            // 
            // GrabImgDgvCol
            // 
            this.GrabImgDgvCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "采集";
            this.GrabImgDgvCol.DefaultCellStyle = dataGridViewCellStyle3;
            this.GrabImgDgvCol.HeaderText = "";
            this.GrabImgDgvCol.Name = "GrabImgDgvCol";
            this.GrabImgDgvCol.Text = "采集图片";
            this.GrabImgDgvCol.UseColumnTextForButtonValue = true;
            // 
            // CameraParaBindingSource
            // 
            this.CameraParaBindingSource.DataSource = typeof(VisionBase.CameraPara);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.Location = new System.Drawing.Point(3, 223);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 47);
            this.button1.TabIndex = 1;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button2.Location = new System.Drawing.Point(3, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 47);
            this.button2.TabIndex = 1;
            this.button2.Text = "刷新测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "black.png");
            this.imageList1.Images.SetKeyName(1, "red.png");
            this.imageList1.Images.SetKeyName(2, "green.png");
            this.imageList1.Images.SetKeyName(3, "white.png");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1119, 523);
            this.panel1.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button3.Location = new System.Drawing.Point(3, 403);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 46);
            this.button3.TabIndex = 1;
            this.button3.Text = "保存图片";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewComboBoxColumn1.DataPropertyName = "CameInterface";
            this.dataGridViewComboBoxColumn1.HeaderText = "CameInterface";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewComboBoxColumn2.DataPropertyName = "CameInterface";
            this.dataGridViewComboBoxColumn2.HeaderText = "CameInterface";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewComboBoxColumn3.DataPropertyName = "CameInterface";
            this.dataGridViewComboBoxColumn3.HeaderText = "CameInterface";
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn4
            // 
            this.dataGridViewComboBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewComboBoxColumn4.DataPropertyName = "CameInterface";
            this.dataGridViewComboBoxColumn4.HeaderText = "CameInterface";
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            this.dataGridViewComboBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.61829F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.3817F));
            this.tableLayoutPanel1.Controls.Add(this.CamParaDgv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.06622F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.93377F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1284, 755);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1128, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.10616F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.89384F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 193F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(153, 523);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // FrmCameraManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 755);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmCameraManager";
            this.TabText = "FrmCameraManager";
            this.Text = "FrmCameraManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCameraManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmCameraManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CamParaDgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraParaBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CamParaDgv;
        private System.Windows.Forms.BindingSource CameraParaBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn cameraNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn CameInterfaceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn camDecribeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CcfPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Width;
        private System.Windows.Forms.DataGridViewTextBoxColumn Height;
        private System.Windows.Forms.DataGridViewCheckBoxColumn triggerModelDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMirrorXDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMirrorYDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isRotDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn isOpen;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteBtnDgvCol;
        private System.Windows.Forms.DataGridViewButtonColumn OpenCamBtnDgvCol;
        private System.Windows.Forms.DataGridViewButtonColumn GrabImgDgvCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}