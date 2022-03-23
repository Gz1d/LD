
namespace VisionBase
{
    partial class FrmUpDnCamCali
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LocalModelCbx = new System.Windows.Forms.ComboBox();
            this.CamLightParaTeachBtn = new System.Windows.Forms.Button();
            this.LoadImgBtn = new System.Windows.Forms.Button();
            this.GrabImgBtn = new System.Windows.Forms.Button();
            this.LocalParaTeachBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.NumUpDnY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.NumUpDnX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PositionSaveBtn = new System.Windows.Forms.Button();
            this.PositionTeachBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CaliPtSelectCbx = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CaliPtModelCbx = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.RectTeachBtn = new System.Windows.Forms.Button();
            this.StartCaliBtn = new System.Windows.Forms.Button();
            this.CaliParaSaveBtn = new System.Windows.Forms.Button();
            this.MotionAdjBtn = new System.Windows.Forms.Button();
            this.StopGrabBtn = new System.Windows.Forms.Button();
            this.ContinueGrabBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDnY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDnX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(21, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 600);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LocalModelCbx);
            this.groupBox1.Controls.Add(this.CamLightParaTeachBtn);
            this.groupBox1.Controls.Add(this.LoadImgBtn);
            this.groupBox1.Controls.Add(this.GrabImgBtn);
            this.groupBox1.Controls.Add(this.LocalParaTeachBtn);
            this.groupBox1.Location = new System.Drawing.Point(629, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 122);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1.视觉示教";
            // 
            // LocalModelCbx
            // 
            this.LocalModelCbx.FormattingEnabled = true;
            this.LocalModelCbx.Items.AddRange(new object[] {
            "Temp",
            "TwoLine",
            "ThreeLine",
            "FourLine,",
            "TwoCircle",
            "LineCircle",
            "TempTwoLine",
            "TempThreeLine",
            "TempFourLine",
            "TempTwoCircle",
            "TempLineCircle"});
            this.LocalModelCbx.Location = new System.Drawing.Point(142, 27);
            this.LocalModelCbx.Name = "LocalModelCbx";
            this.LocalModelCbx.Size = new System.Drawing.Size(113, 20);
            this.LocalModelCbx.TabIndex = 3;
            // 
            // CamLightParaTeachBtn
            // 
            this.CamLightParaTeachBtn.Location = new System.Drawing.Point(20, 72);
            this.CamLightParaTeachBtn.Name = "CamLightParaTeachBtn";
            this.CamLightParaTeachBtn.Size = new System.Drawing.Size(91, 38);
            this.CamLightParaTeachBtn.TabIndex = 1;
            this.CamLightParaTeachBtn.Text = "相机光源示教";
            this.CamLightParaTeachBtn.UseVisualStyleBackColor = true;
            this.CamLightParaTeachBtn.Click += new System.EventHandler(this.CamLightParaTeachBtn_Click);
            // 
            // LoadImgBtn
            // 
            this.LoadImgBtn.Location = new System.Drawing.Point(293, 13);
            this.LoadImgBtn.Name = "LoadImgBtn";
            this.LoadImgBtn.Size = new System.Drawing.Size(84, 44);
            this.LoadImgBtn.TabIndex = 2;
            this.LoadImgBtn.Text = "加载图片";
            this.LoadImgBtn.UseVisualStyleBackColor = true;
            this.LoadImgBtn.Click += new System.EventHandler(this.LoadImgBtn_Click);
            // 
            // GrabImgBtn
            // 
            this.GrabImgBtn.Location = new System.Drawing.Point(294, 72);
            this.GrabImgBtn.Name = "GrabImgBtn";
            this.GrabImgBtn.Size = new System.Drawing.Size(84, 38);
            this.GrabImgBtn.TabIndex = 2;
            this.GrabImgBtn.Text = "采集图片";
            this.GrabImgBtn.UseVisualStyleBackColor = true;
            this.GrabImgBtn.Click += new System.EventHandler(this.GrabImgBtn_Click);
            // 
            // LocalParaTeachBtn
            // 
            this.LocalParaTeachBtn.Location = new System.Drawing.Point(149, 71);
            this.LocalParaTeachBtn.Name = "LocalParaTeachBtn";
            this.LocalParaTeachBtn.Size = new System.Drawing.Size(97, 38);
            this.LocalParaTeachBtn.TabIndex = 2;
            this.LocalParaTeachBtn.Text = "定位示教";
            this.LocalParaTeachBtn.UseVisualStyleBackColor = true;
            this.LocalParaTeachBtn.Click += new System.EventHandler(this.LocalParaTeachBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NumUpDnY);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.NumUpDnX);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.PositionSaveBtn);
            this.groupBox2.Controls.Add(this.PositionTeachBtn);
            this.groupBox2.Location = new System.Drawing.Point(112, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 126);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "区域参数示教";
            // 
            // NumUpDnY
            // 
            this.NumUpDnY.Location = new System.Drawing.Point(169, 86);
            this.NumUpDnY.Name = "NumUpDnY";
            this.NumUpDnY.Size = new System.Drawing.Size(74, 21);
            this.NumUpDnY.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y";
            // 
            // NumUpDnX
            // 
            this.NumUpDnX.Location = new System.Drawing.Point(39, 86);
            this.NumUpDnX.Name = "NumUpDnX";
            this.NumUpDnX.Size = new System.Drawing.Size(74, 21);
            this.NumUpDnX.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "X";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewButtonColumn1});
            this.dataGridView2.Location = new System.Drawing.Point(55, 273);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(307, 218);
            this.dataGridView2.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "示教";
            this.dataGridViewButtonColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewButtonColumn1.HeaderText = "示教";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            // 
            // PositionSaveBtn
            // 
            this.PositionSaveBtn.Enabled = false;
            this.PositionSaveBtn.Location = new System.Drawing.Point(148, 26);
            this.PositionSaveBtn.Name = "PositionSaveBtn";
            this.PositionSaveBtn.Size = new System.Drawing.Size(97, 44);
            this.PositionSaveBtn.TabIndex = 0;
            this.PositionSaveBtn.Text = "坐标保存";
            this.PositionSaveBtn.UseVisualStyleBackColor = true;
            this.PositionSaveBtn.Click += new System.EventHandler(this.PositionSaveBtn_Click);
            // 
            // PositionTeachBtn
            // 
            this.PositionTeachBtn.Location = new System.Drawing.Point(21, 27);
            this.PositionTeachBtn.Name = "PositionTeachBtn";
            this.PositionTeachBtn.Size = new System.Drawing.Size(94, 43);
            this.PositionTeachBtn.TabIndex = 0;
            this.PositionTeachBtn.Text = "坐标示教";
            this.PositionTeachBtn.UseVisualStyleBackColor = true;
            this.PositionTeachBtn.Click += new System.EventHandler(this.PositionTeachBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CaliPtSelectCbx);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(629, 261);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(391, 164);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3.参数示教";
            // 
            // CaliPtSelectCbx
            // 
            this.CaliPtSelectCbx.FormattingEnabled = true;
            this.CaliPtSelectCbx.Items.AddRange(new object[] {
            "第一个点",
            "第二个点",
            "第三个点",
            "第四个点",
            "第五个点",
            "第六个点",
            "第七个点",
            "第八个点",
            "第九个点"});
            this.CaliPtSelectCbx.Location = new System.Drawing.Point(6, 70);
            this.CaliPtSelectCbx.Name = "CaliPtSelectCbx";
            this.CaliPtSelectCbx.Size = new System.Drawing.Size(100, 20);
            this.CaliPtSelectCbx.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "区域";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.CaliPtModelCbx);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.RectTeachBtn);
            this.groupBox4.Location = new System.Drawing.Point(629, 158);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(391, 86);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "2.标定模式 区域示教";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "模式选择";
            // 
            // CaliPtModelCbx
            // 
            this.CaliPtModelCbx.FormattingEnabled = true;
            this.CaliPtModelCbx.Items.AddRange(new object[] {
            "四点标定",
            "九点标定",
            "三点标定"});
            this.CaliPtModelCbx.Location = new System.Drawing.Point(6, 48);
            this.CaliPtModelCbx.Name = "CaliPtModelCbx";
            this.CaliPtModelCbx.Size = new System.Drawing.Size(100, 20);
            this.CaliPtModelCbx.TabIndex = 1;
            this.CaliPtModelCbx.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(265, 27);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(97, 44);
            this.button6.TabIndex = 0;
            this.button6.Text = "初始化";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // RectTeachBtn
            // 
            this.RectTeachBtn.Location = new System.Drawing.Point(130, 27);
            this.RectTeachBtn.Name = "RectTeachBtn";
            this.RectTeachBtn.Size = new System.Drawing.Size(102, 44);
            this.RectTeachBtn.TabIndex = 0;
            this.RectTeachBtn.Text = "示教";
            this.RectTeachBtn.UseVisualStyleBackColor = true;
            this.RectTeachBtn.Click += new System.EventHandler(this.RectTeachBtn_Click);
            // 
            // StartCaliBtn
            // 
            this.StartCaliBtn.Location = new System.Drawing.Point(678, 564);
            this.StartCaliBtn.Name = "StartCaliBtn";
            this.StartCaliBtn.Size = new System.Drawing.Size(95, 43);
            this.StartCaliBtn.TabIndex = 6;
            this.StartCaliBtn.Text = "开始标定";
            this.StartCaliBtn.UseVisualStyleBackColor = true;
            this.StartCaliBtn.Click += new System.EventHandler(this.StartCaliBtn_Click);
            // 
            // CaliParaSaveBtn
            // 
            this.CaliParaSaveBtn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CaliParaSaveBtn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.CaliParaSaveBtn.Location = new System.Drawing.Point(870, 564);
            this.CaliParaSaveBtn.Name = "CaliParaSaveBtn";
            this.CaliParaSaveBtn.Size = new System.Drawing.Size(100, 43);
            this.CaliParaSaveBtn.TabIndex = 6;
            this.CaliParaSaveBtn.Text = "保存";
            this.CaliParaSaveBtn.UseVisualStyleBackColor = true;
            this.CaliParaSaveBtn.Click += new System.EventHandler(this.CaliParaSaveBtn_Click);
            // 
            // MotionAdjBtn
            // 
            this.MotionAdjBtn.Location = new System.Drawing.Point(242, 625);
            this.MotionAdjBtn.Name = "MotionAdjBtn";
            this.MotionAdjBtn.Size = new System.Drawing.Size(94, 55);
            this.MotionAdjBtn.TabIndex = 8;
            this.MotionAdjBtn.Text = "Motion";
            this.MotionAdjBtn.UseVisualStyleBackColor = true;
            this.MotionAdjBtn.Click += new System.EventHandler(this.MotionAdjBtn_Click);
            // 
            // StopGrabBtn
            // 
            this.StopGrabBtn.Location = new System.Drawing.Point(252, 27);
            this.StopGrabBtn.Name = "StopGrabBtn";
            this.StopGrabBtn.Size = new System.Drawing.Size(95, 40);
            this.StopGrabBtn.TabIndex = 9;
            this.StopGrabBtn.Text = "停止采集";
            this.StopGrabBtn.UseVisualStyleBackColor = true;
            this.StopGrabBtn.Click += new System.EventHandler(this.StopGrabBtn_Click);
            // 
            // ContinueGrabBtn
            // 
            this.ContinueGrabBtn.Location = new System.Drawing.Point(50, 27);
            this.ContinueGrabBtn.Name = "ContinueGrabBtn";
            this.ContinueGrabBtn.Size = new System.Drawing.Size(95, 42);
            this.ContinueGrabBtn.TabIndex = 10;
            this.ContinueGrabBtn.Text = "连续采集";
            this.ContinueGrabBtn.UseVisualStyleBackColor = true;
            this.ContinueGrabBtn.Click += new System.EventHandler(this.ContinueGrabBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ContinueGrabBtn);
            this.groupBox5.Controls.Add(this.StopGrabBtn);
            this.groupBox5.Location = new System.Drawing.Point(629, 441);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(391, 89);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "采图";
            // 
            // FrmUpDnCamCali
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 696);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.MotionAdjBtn);
            this.Controls.Add(this.CaliParaSaveBtn);
            this.Controls.Add(this.StartCaliBtn);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmUpDnCamCali";
            this.Text = "FrmUpDnCamCali";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmUpDnCamCali_FormClosing);
            this.Load += new System.EventHandler(this.FrmUpDnCamCali_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDnY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDnX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CamLightParaTeachBtn;
        private System.Windows.Forms.Button LocalParaTeachBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button PositionTeachBtn;
        private System.Windows.Forms.NumericUpDown NumUpDnY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumUpDnX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.Button PositionSaveBtn;
        private System.Windows.Forms.Button GrabImgBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox CaliPtSelectCbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button RectTeachBtn;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CaliPtModelCbx;
        private System.Windows.Forms.Button StartCaliBtn;
        private System.Windows.Forms.Button CaliParaSaveBtn;
        private System.Windows.Forms.Button MotionAdjBtn;
        private System.Windows.Forms.Button StopGrabBtn;
        private System.Windows.Forms.Button ContinueGrabBtn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox LocalModelCbx;
        private System.Windows.Forms.Button LoadImgBtn;
    }
}