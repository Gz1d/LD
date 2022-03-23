
namespace VisionBase
{
    partial class FrmCamLightCtrl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SetLightValueBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DeleteDgvBtnCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CamCbx = new System.Windows.Forms.ComboBox();
            this.TriggerCheck = new System.Windows.Forms.CheckBox();
            this.ExposureBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ParaSaveBtn = new System.Windows.Forms.Button();
            this.ExposTxt = new System.Windows.Forms.TextBox();
            this.OneGrabBtn = new System.Windows.Forms.Button();
            this.ContinueGrabBtn = new System.Windows.Forms.Button();
            this.StopGrabBtn = new System.Windows.Forms.Button();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.MotionAdjBtn = new System.Windows.Forms.Button();
            this.FilterCheck = new System.Windows.Forms.CheckBox();
            this.FilterCNumUpDn = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lightCtrlDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panelDataGridComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lightValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilterCNumUpDn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 446);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lightCtrlDataGridViewComboBoxColumn,
            this.panelDataGridComboBoxColumn,
            this.lightValueDataGridViewTextBoxColumn,
            this.SetLightValueBtn,
            this.DeleteDgvBtnCol});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 578);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(503, 99);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // SetLightValueBtn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "设置亮度";
            this.SetLightValueBtn.DefaultCellStyle = dataGridViewCellStyle1;
            this.SetLightValueBtn.HeaderText = "设置光亮度";
            this.SetLightValueBtn.Name = "SetLightValueBtn";
            // 
            // DeleteDgvBtnCol
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "删除";
            this.DeleteDgvBtnCol.DefaultCellStyle = dataGridViewCellStyle2;
            this.DeleteDgvBtnCol.HeaderText = "删除";
            this.DeleteDgvBtnCol.Name = "DeleteDgvBtnCol";
            // 
            // CamCbx
            // 
            this.CamCbx.FormattingEnabled = true;
            this.CamCbx.Location = new System.Drawing.Point(73, 477);
            this.CamCbx.Name = "CamCbx";
            this.CamCbx.Size = new System.Drawing.Size(97, 20);
            this.CamCbx.TabIndex = 2;
            // 
            // TriggerCheck
            // 
            this.TriggerCheck.AutoSize = true;
            this.TriggerCheck.Location = new System.Drawing.Point(199, 480);
            this.TriggerCheck.Name = "TriggerCheck";
            this.TriggerCheck.Size = new System.Drawing.Size(66, 16);
            this.TriggerCheck.TabIndex = 3;
            this.TriggerCheck.Text = "外触发?";
            this.TriggerCheck.UseVisualStyleBackColor = true;
            this.TriggerCheck.CheckedChanged += new System.EventHandler(this.TriggerCheck_CheckedChanged);
            // 
            // ExposureBar
            // 
            this.ExposureBar.Location = new System.Drawing.Point(82, 525);
            this.ExposureBar.Maximum = 100000;
            this.ExposureBar.Name = "ExposureBar";
            this.ExposureBar.Size = new System.Drawing.Size(402, 45);
            this.ExposureBar.SmallChange = 10;
            this.ExposureBar.TabIndex = 4;
            this.ExposureBar.TickFrequency = 1000;
            this.ExposureBar.Scroll += new System.EventHandler(this.ExposureBar_Scroll);
            this.ExposureBar.ValueChanged += new System.EventHandler(this.ExposureBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 482);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "相机：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 534);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "曝光时间：";
            // 
            // ParaSaveBtn
            // 
            this.ParaSaveBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ParaSaveBtn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ParaSaveBtn.Location = new System.Drawing.Point(596, 491);
            this.ParaSaveBtn.Name = "ParaSaveBtn";
            this.ParaSaveBtn.Size = new System.Drawing.Size(101, 55);
            this.ParaSaveBtn.TabIndex = 6;
            this.ParaSaveBtn.Text = "参数保存";
            this.ParaSaveBtn.UseVisualStyleBackColor = true;
            this.ParaSaveBtn.Click += new System.EventHandler(this.ParaSaveBtn_Click);
            // 
            // ExposTxt
            // 
            this.ExposTxt.Location = new System.Drawing.Point(490, 531);
            this.ExposTxt.Name = "ExposTxt";
            this.ExposTxt.Size = new System.Drawing.Size(69, 21);
            this.ExposTxt.TabIndex = 7;
            // 
            // OneGrabBtn
            // 
            this.OneGrabBtn.Location = new System.Drawing.Point(521, 587);
            this.OneGrabBtn.Name = "OneGrabBtn";
            this.OneGrabBtn.Size = new System.Drawing.Size(80, 34);
            this.OneGrabBtn.TabIndex = 6;
            this.OneGrabBtn.Text = "采集一张";
            this.OneGrabBtn.UseVisualStyleBackColor = true;
            this.OneGrabBtn.Click += new System.EventHandler(this.OneGrabBtn_Click);
            // 
            // ContinueGrabBtn
            // 
            this.ContinueGrabBtn.Location = new System.Drawing.Point(521, 640);
            this.ContinueGrabBtn.Name = "ContinueGrabBtn";
            this.ContinueGrabBtn.Size = new System.Drawing.Size(80, 34);
            this.ContinueGrabBtn.TabIndex = 6;
            this.ContinueGrabBtn.Text = "连续采集";
            this.ContinueGrabBtn.UseVisualStyleBackColor = true;
            this.ContinueGrabBtn.Click += new System.EventHandler(this.ContinueGrabBtn_Click);
            // 
            // StopGrabBtn
            // 
            this.StopGrabBtn.Location = new System.Drawing.Point(617, 638);
            this.StopGrabBtn.Name = "StopGrabBtn";
            this.StopGrabBtn.Size = new System.Drawing.Size(80, 34);
            this.StopGrabBtn.TabIndex = 6;
            this.StopGrabBtn.Text = "停止采集";
            this.StopGrabBtn.UseVisualStyleBackColor = true;
            this.StopGrabBtn.Click += new System.EventHandler(this.StopGrabBtn_Click);
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.DataPropertyName = "LightCtrl";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "设置亮度";
            this.dataGridViewButtonColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewButtonColumn1.HeaderText = "设置光亮度";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(617, 588);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "保存图片";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MotionAdjBtn
            // 
            this.MotionAdjBtn.Location = new System.Drawing.Point(285, 687);
            this.MotionAdjBtn.Name = "MotionAdjBtn";
            this.MotionAdjBtn.Size = new System.Drawing.Size(94, 52);
            this.MotionAdjBtn.TabIndex = 9;
            this.MotionAdjBtn.Text = "Motion";
            this.MotionAdjBtn.UseVisualStyleBackColor = true;
            this.MotionAdjBtn.Click += new System.EventHandler(this.MotionAdjBtn_Click);
            // 
            // FilterCheck
            // 
            this.FilterCheck.AutoSize = true;
            this.FilterCheck.Location = new System.Drawing.Point(310, 483);
            this.FilterCheck.Name = "FilterCheck";
            this.FilterCheck.Size = new System.Drawing.Size(78, 16);
            this.FilterCheck.TabIndex = 10;
            this.FilterCheck.Text = "是否滤波?";
            this.FilterCheck.UseVisualStyleBackColor = true;
            this.FilterCheck.CheckedChanged += new System.EventHandler(this.FilterCheck_CheckedChanged);
            // 
            // FilterCNumUpDn
            // 
            this.FilterCNumUpDn.DecimalPlaces = 5;
            this.FilterCNumUpDn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FilterCNumUpDn.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.FilterCNumUpDn.Location = new System.Drawing.Point(472, 480);
            this.FilterCNumUpDn.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilterCNumUpDn.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.FilterCNumUpDn.Name = "FilterCNumUpDn";
            this.FilterCNumUpDn.Size = new System.Drawing.Size(97, 23);
            this.FilterCNumUpDn.TabIndex = 45;
            this.FilterCNumUpDn.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.FilterCNumUpDn.ValueChanged += new System.EventHandler(this.FilterCNumUpDn_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(407, 484);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 46;
            this.label3.Text = "滤波系数：";
            // 
            // lightCtrlDataGridViewComboBoxColumn
            // 
            this.lightCtrlDataGridViewComboBoxColumn.DataPropertyName = "LightCtrl";
            this.lightCtrlDataGridViewComboBoxColumn.HeaderText = "LightCtrl";
            this.lightCtrlDataGridViewComboBoxColumn.Name = "lightCtrlDataGridViewComboBoxColumn";
            this.lightCtrlDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lightCtrlDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panelDataGridComboBoxColumn
            // 
            this.panelDataGridComboBoxColumn.DataPropertyName = "Panel";
            this.panelDataGridComboBoxColumn.HeaderText = "Panel";
            this.panelDataGridComboBoxColumn.Name = "panelDataGridComboBoxColumn";
            this.panelDataGridComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.panelDataGridComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.panelDataGridComboBoxColumn.Width = 80;
            // 
            // lightValueDataGridViewTextBoxColumn
            // 
            this.lightValueDataGridViewTextBoxColumn.DataPropertyName = "LightValue";
            this.lightValueDataGridViewTextBoxColumn.HeaderText = "LightValue";
            this.lightValueDataGridViewTextBoxColumn.Name = "lightValueDataGridViewTextBoxColumn";
            this.lightValueDataGridViewTextBoxColumn.Width = 80;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(VisionBase.LightPara);
            // 
            // FrmCamLightCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 751);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FilterCNumUpDn);
            this.Controls.Add(this.FilterCheck);
            this.Controls.Add(this.MotionAdjBtn);
            this.Controls.Add(this.ExposTxt);
            this.Controls.Add(this.StopGrabBtn);
            this.Controls.Add(this.ContinueGrabBtn);
            this.Controls.Add(this.OneGrabBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ParaSaveBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExposureBar);
            this.Controls.Add(this.TriggerCheck);
            this.Controls.Add(this.CamCbx);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCamLightCtrl";
            this.Text = "FrmCamLightCtrl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCamLightCtrl_FormClosing);
            this.Load += new System.EventHandler(this.FrmCamLightCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilterCNumUpDn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ComboBox CamCbx;
        private System.Windows.Forms.CheckBox TriggerCheck;
        private System.Windows.Forms.TrackBar ExposureBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ParaSaveBtn;
        private System.Windows.Forms.TextBox ExposTxt;
        private System.Windows.Forms.Button OneGrabBtn;
        private System.Windows.Forms.Button ContinueGrabBtn;
        private System.Windows.Forms.Button StopGrabBtn;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button MotionAdjBtn;
        private System.Windows.Forms.DataGridViewComboBoxColumn lightCtrlDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn panelDataGridComboBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lightValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn SetLightValueBtn;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteDgvBtnCol;
        private System.Windows.Forms.CheckBox FilterCheck;
        private System.Windows.Forms.NumericUpDown FilterCNumUpDn;
        private System.Windows.Forms.Label label3;
    }
}