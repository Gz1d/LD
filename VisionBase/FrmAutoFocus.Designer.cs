namespace VisionBase
{
    partial class FrmAutoFocus
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSearchHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSearchY = new System.Windows.Forms.TextBox();
            this.AddImgeBtn = new System.Windows.Forms.Button();
            this.DrawRectBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.StartFocusBtn = new System.Windows.Forms.Button();
            this.StopFocusBtn = new System.Windows.Forms.Button();
            this.CamCbx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SetColorModelBtn = new System.Windows.Forms.Button();
            this.LightPanelCbx = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LabelLightValue = new System.Windows.Forms.TextBox();
            this.LightValueBar = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ColorPanelCbx = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.StopGrabBtn = new System.Windows.Forms.Button();
            this.StartGrabBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtExposureValue = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBarExposure = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LightValueBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSearchHeight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSearchWidth);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtSearchY);
            this.groupBox1.Controls.Add(this.AddImgeBtn);
            this.groupBox1.Controls.Add(this.DrawRectBtn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSearchX);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(827, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 375);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索框：ROI";
            // 
            // txtSearchHeight
            // 
            this.txtSearchHeight.Location = new System.Drawing.Point(77, 184);
            this.txtSearchHeight.Name = "txtSearchHeight";
            this.txtSearchHeight.ReadOnly = true;
            this.txtSearchHeight.Size = new System.Drawing.Size(74, 21);
            this.txtSearchHeight.TabIndex = 15;
            this.txtSearchHeight.Text = "500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Height：";
            // 
            // txtSearchWidth
            // 
            this.txtSearchWidth.Location = new System.Drawing.Point(77, 133);
            this.txtSearchWidth.Name = "txtSearchWidth";
            this.txtSearchWidth.ReadOnly = true;
            this.txtSearchWidth.Size = new System.Drawing.Size(74, 21);
            this.txtSearchWidth.TabIndex = 13;
            this.txtSearchWidth.Text = "500";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "Width：";
            // 
            // txtSearchY
            // 
            this.txtSearchY.Location = new System.Drawing.Point(77, 85);
            this.txtSearchY.Name = "txtSearchY";
            this.txtSearchY.ReadOnly = true;
            this.txtSearchY.Size = new System.Drawing.Size(74, 21);
            this.txtSearchY.TabIndex = 11;
            this.txtSearchY.Text = "1000";
            this.txtSearchY.TextChanged += new System.EventHandler(this.txtSearchY_TextChanged);
            // 
            // AddImgeBtn
            // 
            this.AddImgeBtn.Location = new System.Drawing.Point(37, 243);
            this.AddImgeBtn.Name = "AddImgeBtn";
            this.AddImgeBtn.Size = new System.Drawing.Size(95, 49);
            this.AddImgeBtn.TabIndex = 16;
            this.AddImgeBtn.Text = "加载图片";
            this.AddImgeBtn.UseVisualStyleBackColor = true;
            this.AddImgeBtn.Click += new System.EventHandler(this.AddImgeBtn_Click);
            // 
            // DrawRectBtn
            // 
            this.DrawRectBtn.Location = new System.Drawing.Point(37, 308);
            this.DrawRectBtn.Name = "DrawRectBtn";
            this.DrawRectBtn.Size = new System.Drawing.Size(95, 49);
            this.DrawRectBtn.TabIndex = 16;
            this.DrawRectBtn.Text = "绘制矩形";
            this.DrawRectBtn.UseVisualStyleBackColor = true;
            this.DrawRectBtn.Click += new System.EventHandler(this.DrawRectBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Y：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtSearchX
            // 
            this.txtSearchX.Location = new System.Drawing.Point(77, 40);
            this.txtSearchX.Name = "txtSearchX";
            this.txtSearchX.ReadOnly = true;
            this.txtSearchX.Size = new System.Drawing.Size(74, 21);
            this.txtSearchX.TabIndex = 9;
            this.txtSearchX.Text = "1000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "X：";
            // 
            // StartFocusBtn
            // 
            this.StartFocusBtn.Location = new System.Drawing.Point(864, 530);
            this.StartFocusBtn.Name = "StartFocusBtn";
            this.StartFocusBtn.Size = new System.Drawing.Size(95, 48);
            this.StartFocusBtn.TabIndex = 16;
            this.StartFocusBtn.Text = "开始对焦";
            this.StartFocusBtn.UseVisualStyleBackColor = true;
            this.StartFocusBtn.Click += new System.EventHandler(this.StartFocusBtn_Click);
            // 
            // StopFocusBtn
            // 
            this.StopFocusBtn.Location = new System.Drawing.Point(864, 623);
            this.StopFocusBtn.Name = "StopFocusBtn";
            this.StopFocusBtn.Size = new System.Drawing.Size(95, 48);
            this.StopFocusBtn.TabIndex = 16;
            this.StopFocusBtn.Text = "停止对焦";
            this.StopFocusBtn.UseVisualStyleBackColor = true;
            this.StopFocusBtn.Click += new System.EventHandler(this.StopFocusBtn_Click);
            // 
            // CamCbx
            // 
            this.CamCbx.FormattingEnabled = true;
            this.CamCbx.Items.AddRange(new object[] {
            "Cam1",
            "Cam2"});
            this.CamCbx.Location = new System.Drawing.Point(864, 76);
            this.CamCbx.Name = "CamCbx";
            this.CamCbx.Size = new System.Drawing.Size(121, 20);
            this.CamCbx.TabIndex = 37;
            this.CamCbx.SelectedIndexChanged += new System.EventHandler(this.CamCbx_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(875, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "选择相机";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(325, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 21);
            this.label2.TabIndex = 38;
            this.label2.Text = "自动对焦调试";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(22, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 618);
            this.panel1.TabIndex = 39;
            // 
            // SetColorModelBtn
            // 
            this.SetColorModelBtn.Location = new System.Drawing.Point(26, 28);
            this.SetColorModelBtn.Name = "SetColorModelBtn";
            this.SetColorModelBtn.Size = new System.Drawing.Size(95, 41);
            this.SetColorModelBtn.TabIndex = 16;
            this.SetColorModelBtn.Text = "设为灰度";
            this.SetColorModelBtn.UseVisualStyleBackColor = true;
            this.SetColorModelBtn.Click += new System.EventHandler(this.SetColorModelBtn_Click);
            // 
            // LightPanelCbx
            // 
            this.LightPanelCbx.FormattingEnabled = true;
            this.LightPanelCbx.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.LightPanelCbx.Location = new System.Drawing.Point(153, 30);
            this.LightPanelCbx.Name = "LightPanelCbx";
            this.LightPanelCbx.Size = new System.Drawing.Size(86, 20);
            this.LightPanelCbx.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 38;
            this.label6.Text = "光源通道设置";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 42;
            this.label7.Text = "光源亮度：";
            // 
            // LabelLightValue
            // 
            this.LabelLightValue.Location = new System.Drawing.Point(282, 70);
            this.LabelLightValue.Name = "LabelLightValue";
            this.LabelLightValue.Size = new System.Drawing.Size(27, 21);
            this.LabelLightValue.TabIndex = 41;
            // 
            // LightValueBar
            // 
            this.LightValueBar.Location = new System.Drawing.Point(87, 69);
            this.LightValueBar.Maximum = 255;
            this.LightValueBar.Name = "LightValueBar";
            this.LightValueBar.Size = new System.Drawing.Size(185, 45);
            this.LightValueBar.TabIndex = 40;
            this.LightValueBar.TickFrequency = 20;
            this.LightValueBar.Value = 250;
            this.LightValueBar.Scroll += new System.EventHandler(this.LightValueBar_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LabelLightValue);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.LightPanelCbx);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.LightValueBar);
            this.groupBox2.Location = new System.Drawing.Point(1034, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 132);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "光源调试";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 41);
            this.button1.TabIndex = 16;
            this.button1.Text = "设为彩色";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ColorPanelCbx
            // 
            this.ColorPanelCbx.FormattingEnabled = true;
            this.ColorPanelCbx.Items.AddRange(new object[] {
            "红",
            "绿",
            "蓝",
            "彩色"});
            this.ColorPanelCbx.Location = new System.Drawing.Point(192, 50);
            this.ColorPanelCbx.Name = "ColorPanelCbx";
            this.ColorPanelCbx.Size = new System.Drawing.Size(86, 20);
            this.ColorPanelCbx.TabIndex = 37;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(197, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 38;
            this.label8.Text = "颜色通道选择";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.StopGrabBtn);
            this.groupBox3.Controls.Add(this.StartGrabBtn);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.ColorPanelCbx);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(1036, 536);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(317, 150);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "不同颜色通道的图片显示";
            // 
            // StopGrabBtn
            // 
            this.StopGrabBtn.Location = new System.Drawing.Point(188, 89);
            this.StopGrabBtn.Name = "StopGrabBtn";
            this.StopGrabBtn.Size = new System.Drawing.Size(95, 47);
            this.StopGrabBtn.TabIndex = 16;
            this.StopGrabBtn.Text = "停止采图";
            this.StopGrabBtn.UseVisualStyleBackColor = true;
            this.StopGrabBtn.Click += new System.EventHandler(this.StopGrabBtn_Click);
            // 
            // StartGrabBtn
            // 
            this.StartGrabBtn.Location = new System.Drawing.Point(26, 89);
            this.StartGrabBtn.Name = "StartGrabBtn";
            this.StartGrabBtn.Size = new System.Drawing.Size(95, 47);
            this.StartGrabBtn.TabIndex = 16;
            this.StartGrabBtn.Text = "开始采图";
            this.StartGrabBtn.UseVisualStyleBackColor = true;
            this.StartGrabBtn.Click += new System.EventHandler(this.StartGrabBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtExposureValue);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.trackBarExposure);
            this.groupBox4.Controls.Add(this.SetColorModelBtn);
            this.groupBox4.Location = new System.Drawing.Point(1029, 253);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(322, 186);
            this.groupBox4.TabIndex = 45;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "相机调试";
            // 
            // txtExposureValue
            // 
            this.txtExposureValue.Location = new System.Drawing.Point(153, 82);
            this.txtExposureValue.Name = "txtExposureValue";
            this.txtExposureValue.Size = new System.Drawing.Size(57, 21);
            this.txtExposureValue.TabIndex = 23;
            this.txtExposureValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExposureValue_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "曝光时间：";
            // 
            // trackBarExposure
            // 
            this.trackBarExposure.Location = new System.Drawing.Point(95, 117);
            this.trackBarExposure.Maximum = 500000;
            this.trackBarExposure.Minimum = 50;
            this.trackBarExposure.Name = "trackBarExposure";
            this.trackBarExposure.Size = new System.Drawing.Size(190, 45);
            this.trackBarExposure.TabIndex = 22;
            this.trackBarExposure.TickFrequency = 10000;
            this.trackBarExposure.Value = 50;
            this.trackBarExposure.Scroll += new System.EventHandler(this.trackBarExposure_Scroll);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1062, 461);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 48);
            this.button2.TabIndex = 16;
            this.button2.Text = "保存参数";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1211, 461);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 48);
            this.button3.TabIndex = 16;
            this.button3.Text = "读取参数";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmAutoFocus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 714);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CamCbx);
            this.Controls.Add(this.StopFocusBtn);
            this.Controls.Add(this.StartFocusBtn);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAutoFocus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TabText = "AutoFocusFrm";
            this.Text = "AutoFocusFrm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAutoFocus_FormClosing);
            this.Load += new System.EventHandler(this.FrmAutoFocus_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LightValueBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSearchHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearchWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSearchY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearchX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button StartFocusBtn;
        private System.Windows.Forms.Button StopFocusBtn;
        private System.Windows.Forms.ComboBox CamCbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button DrawRectBtn;
        private System.Windows.Forms.Button AddImgeBtn;
        private System.Windows.Forms.Button SetColorModelBtn;
        private System.Windows.Forms.ComboBox LightPanelCbx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox LabelLightValue;
        private System.Windows.Forms.TrackBar LightValueBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox ColorPanelCbx;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button StopGrabBtn;
        private System.Windows.Forms.Button StartGrabBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtExposureValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBarExposure;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}