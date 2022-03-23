namespace LD.Ui
{
    partial class frmSerial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSerial));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSerial = new System.Windows.Forms.DataGridView();
            this.portNameDataGridViewComBoBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.isConnected = new System.Windows.Forms.DataGridViewImageColumn();
            this.serialDeviceDataGridViewComBotBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.machineIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baudRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newLineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.callReceiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SleepMs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receivedBytesThresholdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isHexDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.readTimeoutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.writeTimeoutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buffReceiveDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReceive = new System.Windows.Forms.DataGridViewButtonColumn();
            this.buffSendDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSend = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.stopBitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataBitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resetMsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serialItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvSerial);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            // 
            // dgvSerial
            // 
            this.dgvSerial.AutoGenerateColumns = false;
            this.dgvSerial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSerial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.portNameDataGridViewComBoBoxColumn,
            this.isConnected,
            this.serialDeviceDataGridViewComBotBoxColumn,
            this.machineIDDataGridViewTextBoxColumn,
            this.stationIDDataGridViewTextBoxColumn,
            this.descripDataGridViewTextBoxColumn,
            this.baudRateDataGridViewTextBoxColumn,
            this.newLineDataGridViewTextBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn,
            this.callReceiveDataGridViewCheckBoxColumn,
            this.SleepMs,
            this.receivedBytesThresholdDataGridViewTextBoxColumn,
            this.isHexDataGridViewCheckBoxColumn,
            this.readTimeoutDataGridViewTextBoxColumn,
            this.writeTimeoutDataGridViewTextBoxColumn,
            this.buffReceiveDataGridViewTextBoxColumn,
            this.btnReceive,
            this.buffSendDataGridViewTextBoxColumn,
            this.btnSend,
            this.btnDelete,
            this.stopBitsDataGridViewTextBoxColumn,
            this.parityDataGridViewTextBoxColumn,
            this.dataBitsDataGridViewTextBoxColumn,
            this.tagDataGridViewTextBoxColumn,
            this.resetMsDataGridViewTextBoxColumn});
            this.dgvSerial.DataSource = this.serialItemBindingSource;
            resources.ApplyResources(this.dgvSerial, "dgvSerial");
            this.dgvSerial.Name = "dgvSerial";
            this.dgvSerial.RowTemplate.Height = 23;
            this.dgvSerial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSerial_CellContentClick);
            this.dgvSerial.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSerial_CellFormatting);
            this.dgvSerial.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSerial_DataError);
            // 
            // portNameDataGridViewComBoBoxColumn
            // 
            this.portNameDataGridViewComBoBoxColumn.DataPropertyName = "PortName";
            this.portNameDataGridViewComBoBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.portNameDataGridViewComBoBoxColumn, "portNameDataGridViewComBoBoxColumn");
            this.portNameDataGridViewComBoBoxColumn.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20",
            "COM21",
            "COM22",
            "COM23",
            "COM24",
            "COM25",
            "COM26",
            "COM27",
            "COM28",
            "COM29",
            "COM30"});
            this.portNameDataGridViewComBoBoxColumn.Name = "portNameDataGridViewComBoBoxColumn";
            this.portNameDataGridViewComBoBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.portNameDataGridViewComBoBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // isConnected
            // 
            this.isConnected.DataPropertyName = "IsConnected";
            resources.ApplyResources(this.isConnected, "isConnected");
            this.isConnected.Name = "isConnected";
            this.isConnected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // serialDeviceDataGridViewComBotBoxColumn
            // 
            this.serialDeviceDataGridViewComBotBoxColumn.DataPropertyName = "SerialDevice";
            this.serialDeviceDataGridViewComBotBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.serialDeviceDataGridViewComBotBoxColumn, "serialDeviceDataGridViewComBotBoxColumn");
            this.serialDeviceDataGridViewComBotBoxColumn.Name = "serialDeviceDataGridViewComBotBoxColumn";
            this.serialDeviceDataGridViewComBotBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.serialDeviceDataGridViewComBotBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // machineIDDataGridViewTextBoxColumn
            // 
            this.machineIDDataGridViewTextBoxColumn.DataPropertyName = "MachineID";
            resources.ApplyResources(this.machineIDDataGridViewTextBoxColumn, "machineIDDataGridViewTextBoxColumn");
            this.machineIDDataGridViewTextBoxColumn.Name = "machineIDDataGridViewTextBoxColumn";
            // 
            // stationIDDataGridViewTextBoxColumn
            // 
            this.stationIDDataGridViewTextBoxColumn.DataPropertyName = "StationID";
            resources.ApplyResources(this.stationIDDataGridViewTextBoxColumn, "stationIDDataGridViewTextBoxColumn");
            this.stationIDDataGridViewTextBoxColumn.Name = "stationIDDataGridViewTextBoxColumn";
            // 
            // descripDataGridViewTextBoxColumn
            // 
            this.descripDataGridViewTextBoxColumn.DataPropertyName = "Descrip";
            resources.ApplyResources(this.descripDataGridViewTextBoxColumn, "descripDataGridViewTextBoxColumn");
            this.descripDataGridViewTextBoxColumn.Name = "descripDataGridViewTextBoxColumn";
            // 
            // baudRateDataGridViewTextBoxColumn
            // 
            this.baudRateDataGridViewTextBoxColumn.DataPropertyName = "BaudRate";
            resources.ApplyResources(this.baudRateDataGridViewTextBoxColumn, "baudRateDataGridViewTextBoxColumn");
            this.baudRateDataGridViewTextBoxColumn.Name = "baudRateDataGridViewTextBoxColumn";
            // 
            // newLineDataGridViewTextBoxColumn
            // 
            this.newLineDataGridViewTextBoxColumn.DataPropertyName = "NewLine";
            resources.ApplyResources(this.newLineDataGridViewTextBoxColumn, "newLineDataGridViewTextBoxColumn");
            this.newLineDataGridViewTextBoxColumn.Name = "newLineDataGridViewTextBoxColumn";
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            resources.ApplyResources(this.isActiveDataGridViewCheckBoxColumn, "isActiveDataGridViewCheckBoxColumn");
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            // 
            // callReceiveDataGridViewCheckBoxColumn
            // 
            this.callReceiveDataGridViewCheckBoxColumn.DataPropertyName = "CallReceive";
            resources.ApplyResources(this.callReceiveDataGridViewCheckBoxColumn, "callReceiveDataGridViewCheckBoxColumn");
            this.callReceiveDataGridViewCheckBoxColumn.Name = "callReceiveDataGridViewCheckBoxColumn";
            // 
            // SleepMs
            // 
            this.SleepMs.DataPropertyName = "SleepMs";
            resources.ApplyResources(this.SleepMs, "SleepMs");
            this.SleepMs.Name = "SleepMs";
            // 
            // receivedBytesThresholdDataGridViewTextBoxColumn
            // 
            this.receivedBytesThresholdDataGridViewTextBoxColumn.DataPropertyName = "ReceivedBytesThreshold";
            resources.ApplyResources(this.receivedBytesThresholdDataGridViewTextBoxColumn, "receivedBytesThresholdDataGridViewTextBoxColumn");
            this.receivedBytesThresholdDataGridViewTextBoxColumn.Name = "receivedBytesThresholdDataGridViewTextBoxColumn";
            // 
            // isHexDataGridViewCheckBoxColumn
            // 
            this.isHexDataGridViewCheckBoxColumn.DataPropertyName = "IsHex";
            resources.ApplyResources(this.isHexDataGridViewCheckBoxColumn, "isHexDataGridViewCheckBoxColumn");
            this.isHexDataGridViewCheckBoxColumn.Name = "isHexDataGridViewCheckBoxColumn";
            // 
            // readTimeoutDataGridViewTextBoxColumn
            // 
            this.readTimeoutDataGridViewTextBoxColumn.DataPropertyName = "ReadTimeout";
            resources.ApplyResources(this.readTimeoutDataGridViewTextBoxColumn, "readTimeoutDataGridViewTextBoxColumn");
            this.readTimeoutDataGridViewTextBoxColumn.Name = "readTimeoutDataGridViewTextBoxColumn";
            // 
            // writeTimeoutDataGridViewTextBoxColumn
            // 
            this.writeTimeoutDataGridViewTextBoxColumn.DataPropertyName = "WriteTimeout";
            resources.ApplyResources(this.writeTimeoutDataGridViewTextBoxColumn, "writeTimeoutDataGridViewTextBoxColumn");
            this.writeTimeoutDataGridViewTextBoxColumn.Name = "writeTimeoutDataGridViewTextBoxColumn";
            // 
            // buffReceiveDataGridViewTextBoxColumn
            // 
            this.buffReceiveDataGridViewTextBoxColumn.DataPropertyName = "BuffReceive";
            resources.ApplyResources(this.buffReceiveDataGridViewTextBoxColumn, "buffReceiveDataGridViewTextBoxColumn");
            this.buffReceiveDataGridViewTextBoxColumn.Name = "buffReceiveDataGridViewTextBoxColumn";
            // 
            // btnReceive
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "Read";
            this.btnReceive.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.btnReceive, "btnReceive");
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnReceive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnReceive.Text = "Read";
            // 
            // buffSendDataGridViewTextBoxColumn
            // 
            this.buffSendDataGridViewTextBoxColumn.DataPropertyName = "BuffSend";
            resources.ApplyResources(this.buffSendDataGridViewTextBoxColumn, "buffSendDataGridViewTextBoxColumn");
            this.buffSendDataGridViewTextBoxColumn.Name = "buffSendDataGridViewTextBoxColumn";
            // 
            // btnSend
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "Send";
            this.btnSend.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.Name = "btnSend";
            this.btnSend.Text = "Send";
            // 
            // btnDelete
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "Delete";
            this.btnDelete.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnDelete.Text = "Delete";
            // 
            // stopBitsDataGridViewTextBoxColumn
            // 
            this.stopBitsDataGridViewTextBoxColumn.DataPropertyName = "StopBits";
            resources.ApplyResources(this.stopBitsDataGridViewTextBoxColumn, "stopBitsDataGridViewTextBoxColumn");
            this.stopBitsDataGridViewTextBoxColumn.Name = "stopBitsDataGridViewTextBoxColumn";
            // 
            // parityDataGridViewTextBoxColumn
            // 
            this.parityDataGridViewTextBoxColumn.DataPropertyName = "Parity";
            resources.ApplyResources(this.parityDataGridViewTextBoxColumn, "parityDataGridViewTextBoxColumn");
            this.parityDataGridViewTextBoxColumn.Name = "parityDataGridViewTextBoxColumn";
            // 
            // dataBitsDataGridViewTextBoxColumn
            // 
            this.dataBitsDataGridViewTextBoxColumn.DataPropertyName = "DataBits";
            resources.ApplyResources(this.dataBitsDataGridViewTextBoxColumn, "dataBitsDataGridViewTextBoxColumn");
            this.dataBitsDataGridViewTextBoxColumn.Name = "dataBitsDataGridViewTextBoxColumn";
            // 
            // tagDataGridViewTextBoxColumn
            // 
            this.tagDataGridViewTextBoxColumn.DataPropertyName = "Tag";
            resources.ApplyResources(this.tagDataGridViewTextBoxColumn, "tagDataGridViewTextBoxColumn");
            this.tagDataGridViewTextBoxColumn.Name = "tagDataGridViewTextBoxColumn";
            // 
            // resetMsDataGridViewTextBoxColumn
            // 
            this.resetMsDataGridViewTextBoxColumn.DataPropertyName = "ResetMs";
            resources.ApplyResources(this.resetMsDataGridViewTextBoxColumn, "resetMsDataGridViewTextBoxColumn");
            this.resetMsDataGridViewTextBoxColumn.Name = "resetMsDataGridViewTextBoxColumn";
            // 
            // serialItemBindingSource
            // 
            this.serialItemBindingSource.DataSource = typeof(LD.Config.SerialItem);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
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
            // frmSerial
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "frmSerial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSerial_FormClosing);
            this.Load += new System.EventHandler(this.frmSerial_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serialItemBindingSource)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.BindingSource serialItemBindingSource;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSerial;
        private System.Windows.Forms.DataGridViewComboBoxColumn portNameDataGridViewComBoBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn isConnected;
        private System.Windows.Forms.DataGridViewComboBoxColumn serialDeviceDataGridViewComBotBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baudRateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn newLineDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn callReceiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SleepMs;
        private System.Windows.Forms.DataGridViewTextBoxColumn receivedBytesThresholdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isHexDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn readTimeoutDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn writeTimeoutDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn buffReceiveDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn btnReceive;
        private System.Windows.Forms.DataGridViewTextBoxColumn buffSendDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn btnSend;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn stopBitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataBitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resetMsDataGridViewTextBoxColumn;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button1;
    }
}