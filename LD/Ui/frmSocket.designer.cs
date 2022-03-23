namespace LD.Ui
{
    partial class frmSocket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSocket));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvServer = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isConnectedS = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.socketDeviceS = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSendServer = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnDeleteS = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.socketSItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvClient = new System.Windows.Forms.DataGridView();
            this.iP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isConnected = new System.Windows.Forms.DataGridViewImageColumn();
            this.port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.socketDevice = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.machineID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heartSecond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectSecond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buffReceive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buffSend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSendClient = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.socketCItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cameradeviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketSItemBindingSource)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketCItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameradeviceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl2, "tabControl2");
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvServer);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvServer
            // 
            this.dgvServer.AutoGenerateColumns = false;
            this.dgvServer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.isConnectedS,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn1,
            this.socketDeviceS,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.btnSendServer,
            this.btnDeleteS,
            this.dataGridViewTextBoxColumn11});
            this.dgvServer.DataSource = this.socketSItemBindingSource;
            resources.ApplyResources(this.dgvServer, "dgvServer");
            this.dgvServer.Name = "dgvServer";
            this.dgvServer.RowTemplate.Height = 23;
            this.dgvServer.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServer_CellContentClick);
            this.dgvServer.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvServer_CellFormatting);
            this.dgvServer.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvServer_DataError);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IP";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // isConnectedS
            // 
            this.isConnectedS.DataPropertyName = "IsConnected";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            this.isConnectedS.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.isConnectedS, "isConnectedS");
            this.isConnectedS.Image = global::LD.Properties.Resources.white;
            this.isConnectedS.Name = "isConnectedS";
            this.isConnectedS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isConnectedS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "port";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "IsActive";
            resources.ApplyResources(this.dataGridViewCheckBoxColumn1, "dataGridViewCheckBoxColumn1");
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // socketDeviceS
            // 
            this.socketDeviceS.DataPropertyName = "SocketDevice";
            this.socketDeviceS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.socketDeviceS, "socketDeviceS");
            this.socketDeviceS.Name = "socketDeviceS";
            this.socketDeviceS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.socketDeviceS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "MachineID";
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "StationID";
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "OutTime";
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "HeartSecond";
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "ConnectSecond";
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "BuffReceive";
            this.dataGridViewTextBoxColumn8.FillWeight = 200F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Descrip";
            resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "BuffSend";
            this.dataGridViewTextBoxColumn10.FillWeight = 200F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // btnSendServer
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "Send";
            this.btnSendServer.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.btnSendServer, "btnSendServer");
            this.btnSendServer.Name = "btnSendServer";
            this.btnSendServer.Text = "Send";
            // 
            // btnDeleteS
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "Delete";
            this.btnDeleteS.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.btnDeleteS, "btnDeleteS");
            this.btnDeleteS.Name = "btnDeleteS";
            this.btnDeleteS.Text = "Delete";
            // 
            // dataGridViewTextBoxColumn11
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // socketSItemBindingSource
            // 
            this.socketSItemBindingSource.DataSource = typeof(LD.Config.SocketSItem);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.splitContainer2, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button1);
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
            this.tabPage1.Controls.Add(this.dgvClient);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvClient
            // 
            this.dgvClient.AutoGenerateColumns = false;
            this.dgvClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iP,
            this.isConnected,
            this.port,
            this.isActive,
            this.socketDevice,
            this.machineID,
            this.stationID,
            this.outTime,
            this.heartSecond,
            this.connectSecond,
            this.buffReceive,
            this.descrip,
            this.buffSend,
            this.btnSendClient,
            this.btnDelete,
            this.tail});
            this.dgvClient.DataSource = this.socketCItemBindingSource;
            resources.ApplyResources(this.dgvClient, "dgvClient");
            this.dgvClient.Name = "dgvClient";
            this.dgvClient.RowTemplate.Height = 23;
            this.dgvClient.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClient_CellContentClick);
            this.dgvClient.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvClient_CellFormatting);
            this.dgvClient.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvClient_DataError);
            // 
            // iP
            // 
            this.iP.DataPropertyName = "IP";
            resources.ApplyResources(this.iP, "iP");
            this.iP.Name = "iP";
            // 
            // isConnected
            // 
            this.isConnected.DataPropertyName = "IsConnected";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle4.NullValue")));
            this.isConnected.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.isConnected, "isConnected");
            this.isConnected.Image = global::LD.Properties.Resources.white;
            this.isConnected.Name = "isConnected";
            // 
            // port
            // 
            this.port.DataPropertyName = "port";
            resources.ApplyResources(this.port, "port");
            this.port.Name = "port";
            // 
            // isActive
            // 
            this.isActive.DataPropertyName = "IsActive";
            resources.ApplyResources(this.isActive, "isActive");
            this.isActive.Name = "isActive";
            // 
            // socketDevice
            // 
            this.socketDevice.DataPropertyName = "SocketDevice";
            this.socketDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.socketDevice, "socketDevice");
            this.socketDevice.Name = "socketDevice";
            this.socketDevice.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.socketDevice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // machineID
            // 
            this.machineID.DataPropertyName = "MachineID";
            resources.ApplyResources(this.machineID, "machineID");
            this.machineID.Name = "machineID";
            // 
            // stationID
            // 
            this.stationID.DataPropertyName = "StationID";
            resources.ApplyResources(this.stationID, "stationID");
            this.stationID.Name = "stationID";
            // 
            // outTime
            // 
            this.outTime.DataPropertyName = "OutTime";
            resources.ApplyResources(this.outTime, "outTime");
            this.outTime.Name = "outTime";
            // 
            // heartSecond
            // 
            this.heartSecond.DataPropertyName = "HeartSecond";
            resources.ApplyResources(this.heartSecond, "heartSecond");
            this.heartSecond.Name = "heartSecond";
            // 
            // connectSecond
            // 
            this.connectSecond.DataPropertyName = "ConnectSecond";
            resources.ApplyResources(this.connectSecond, "connectSecond");
            this.connectSecond.Name = "connectSecond";
            // 
            // buffReceive
            // 
            this.buffReceive.DataPropertyName = "BuffReceive";
            this.buffReceive.FillWeight = 200F;
            resources.ApplyResources(this.buffReceive, "buffReceive");
            this.buffReceive.Name = "buffReceive";
            this.buffReceive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // descrip
            // 
            this.descrip.DataPropertyName = "Descrip";
            resources.ApplyResources(this.descrip, "descrip");
            this.descrip.Name = "descrip";
            // 
            // buffSend
            // 
            this.buffSend.DataPropertyName = "BuffSend";
            this.buffSend.FillWeight = 200F;
            resources.ApplyResources(this.buffSend, "buffSend");
            this.buffSend.Name = "buffSend";
            this.buffSend.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // btnSendClient
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = "Send";
            this.btnSendClient.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.btnSendClient, "btnSendClient");
            this.btnSendClient.Name = "btnSendClient";
            this.btnSendClient.Text = "";
            // 
            // btnDelete
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = "Delete";
            this.btnDelete.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "";
            // 
            // tail
            // 
            resources.ApplyResources(this.tail, "tail");
            this.tail.Name = "tail";
            this.tail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // socketCItemBindingSource
            // 
            this.socketCItemBindingSource.DataSource = typeof(LD.Config.SocketCItem);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // frmSocket
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmSocket";
            this.Load += new System.EventHandler(this.frmSocket_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketSItemBindingSource)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketCItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameradeviceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource cameradeviceBindingSource;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.BindingSource socketCItemBindingSource;
        private System.Windows.Forms.DataGridView dgvServer;
        private System.Windows.Forms.BindingSource socketSItemBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvClient;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewImageColumn isConnectedS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn socketDeviceS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewButtonColumn btnSendServer;
        private System.Windows.Forms.DataGridViewButtonColumn btnDeleteS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn iP;
        private System.Windows.Forms.DataGridViewImageColumn isConnected;
        private System.Windows.Forms.DataGridViewTextBoxColumn port;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActive;
        private System.Windows.Forms.DataGridViewComboBoxColumn socketDevice;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineID;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn outTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn heartSecond;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectSecond;
        private System.Windows.Forms.DataGridViewTextBoxColumn buffReceive;
        private System.Windows.Forms.DataGridViewTextBoxColumn descrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn buffSend;
        private System.Windows.Forms.DataGridViewButtonColumn btnSendClient;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn tail;
    }
}