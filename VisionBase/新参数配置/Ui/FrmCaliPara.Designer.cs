
namespace VisionBase
{
    partial class FrmCaliPara
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
            this.CamLightParaTeachBtn = new System.Windows.Forms.Button();
            this.LocalParaTeachBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RotPosGetBtn = new System.Windows.Forms.Button();
            this.StartCaliPtTeachBtn = new System.Windows.Forms.Button();
            this.RotPosTbx = new System.Windows.Forms.TextBox();
            this.EndCaliPtTeachBtn = new System.Windows.Forms.Button();
            this.AngleRangeNumUpDn = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RotCountNumUpDn = new System.Windows.Forms.NumericUpDown();
            this.StopRotPosTeachBtn = new System.Windows.Forms.Button();
            this.MoveToRotPosBtn = new System.Windows.Forms.Button();
            this.CaliRotCenterBtn = new System.Windows.Forms.Button();
            this.RotTeachBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.GrabImgBtn = new System.Windows.Forms.Button();
            this.LocalModelCbx = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CaliModelCbx = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CaliBtn = new System.Windows.Forms.Button();
            this.MoveToCaliEndPtBtn = new System.Windows.Forms.Button();
            this.MoveToCaliStartPtBtn = new System.Windows.Forms.Button();
            this.EndCaliPtSaveBtn = new System.Windows.Forms.Button();
            this.StartCaliPtSaveBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.EndCaliPtTeachTbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StartCaliPtTeachTbx = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MotionAdjBtn = new System.Windows.Forms.Button();
            this.CaliParaSaveBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ContinueGrabBtn = new System.Windows.Forms.Button();
            this.SaveImgBtn = new System.Windows.Forms.Button();
            this.StopGrabBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.FindCirCenterCbx = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.AngleRangeNumUpDn)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RotCountNumUpDn)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CamLightParaTeachBtn
            // 
            this.CamLightParaTeachBtn.Location = new System.Drawing.Point(18, 91);
            this.CamLightParaTeachBtn.Name = "CamLightParaTeachBtn";
            this.CamLightParaTeachBtn.Size = new System.Drawing.Size(91, 44);
            this.CamLightParaTeachBtn.TabIndex = 0;
            this.CamLightParaTeachBtn.Text = "相机光源示教";
            this.CamLightParaTeachBtn.UseVisualStyleBackColor = true;
            this.CamLightParaTeachBtn.Click += new System.EventHandler(this.CamLightParaTeachBtn_Click);
            // 
            // LocalParaTeachBtn
            // 
            this.LocalParaTeachBtn.Location = new System.Drawing.Point(138, 90);
            this.LocalParaTeachBtn.Name = "LocalParaTeachBtn";
            this.LocalParaTeachBtn.Size = new System.Drawing.Size(84, 45);
            this.LocalParaTeachBtn.TabIndex = 0;
            this.LocalParaTeachBtn.Text = "定位示教";
            this.LocalParaTeachBtn.UseVisualStyleBackColor = true;
            this.LocalParaTeachBtn.Click += new System.EventHandler(this.LocalParaTeachBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(4, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 601);
            this.panel1.TabIndex = 1;
            // 
            // RotPosGetBtn
            // 
            this.RotPosGetBtn.Enabled = false;
            this.RotPosGetBtn.Location = new System.Drawing.Point(254, 22);
            this.RotPosGetBtn.Name = "RotPosGetBtn";
            this.RotPosGetBtn.Size = new System.Drawing.Size(90, 43);
            this.RotPosGetBtn.TabIndex = 0;
            this.RotPosGetBtn.Text = "获取坐标";
            this.RotPosGetBtn.UseVisualStyleBackColor = true;
            this.RotPosGetBtn.Click += new System.EventHandler(this.RotPosGetBtn_Click);
            // 
            // StartCaliPtTeachBtn
            // 
            this.StartCaliPtTeachBtn.Location = new System.Drawing.Point(19, 69);
            this.StartCaliPtTeachBtn.Name = "StartCaliPtTeachBtn";
            this.StartCaliPtTeachBtn.Size = new System.Drawing.Size(84, 49);
            this.StartCaliPtTeachBtn.TabIndex = 0;
            this.StartCaliPtTeachBtn.Text = "起点示教";
            this.StartCaliPtTeachBtn.UseVisualStyleBackColor = true;
            this.StartCaliPtTeachBtn.Click += new System.EventHandler(this.StartCaliPtTeachBtn_Click);
            // 
            // RotPosTbx
            // 
            this.RotPosTbx.Location = new System.Drawing.Point(107, 80);
            this.RotPosTbx.Name = "RotPosTbx";
            this.RotPosTbx.Size = new System.Drawing.Size(231, 21);
            this.RotPosTbx.TabIndex = 2;
            // 
            // EndCaliPtTeachBtn
            // 
            this.EndCaliPtTeachBtn.Location = new System.Drawing.Point(19, 190);
            this.EndCaliPtTeachBtn.Name = "EndCaliPtTeachBtn";
            this.EndCaliPtTeachBtn.Size = new System.Drawing.Size(85, 48);
            this.EndCaliPtTeachBtn.TabIndex = 0;
            this.EndCaliPtTeachBtn.Text = "终点示教";
            this.EndCaliPtTeachBtn.UseVisualStyleBackColor = true;
            this.EndCaliPtTeachBtn.Click += new System.EventHandler(this.EndCaliPtTeachBtn_Click);
            // 
            // AngleRangeNumUpDn
            // 
            this.AngleRangeNumUpDn.Enabled = false;
            this.AngleRangeNumUpDn.Location = new System.Drawing.Point(77, 122);
            this.AngleRangeNumUpDn.Name = "AngleRangeNumUpDn";
            this.AngleRangeNumUpDn.Size = new System.Drawing.Size(74, 21);
            this.AngleRangeNumUpDn.TabIndex = 3;
            this.AngleRangeNumUpDn.ValueChanged += new System.EventHandler(this.AngleRangeNumUpDn_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FindCirCenterCbx);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.RotCountNumUpDn);
            this.groupBox1.Controls.Add(this.AngleRangeNumUpDn);
            this.groupBox1.Controls.Add(this.StopRotPosTeachBtn);
            this.groupBox1.Controls.Add(this.MoveToRotPosBtn);
            this.groupBox1.Controls.Add(this.CaliRotCenterBtn);
            this.groupBox1.Controls.Add(this.RotTeachBtn);
            this.groupBox1.Controls.Add(this.RotPosGetBtn);
            this.groupBox1.Controls.Add(this.RotPosTbx);
            this.groupBox1.Location = new System.Drawing.Point(9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 227);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "3.旋转中心示教";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "旋转次数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "角度范围";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "X,Y,Theta坐标";
            // 
            // RotCountNumUpDn
            // 
            this.RotCountNumUpDn.Location = new System.Drawing.Point(259, 120);
            this.RotCountNumUpDn.Name = "RotCountNumUpDn";
            this.RotCountNumUpDn.Size = new System.Drawing.Size(85, 21);
            this.RotCountNumUpDn.TabIndex = 3;
            this.RotCountNumUpDn.ValueChanged += new System.EventHandler(this.RotCountNumUpDn_ValueChanged);
            // 
            // StopRotPosTeachBtn
            // 
            this.StopRotPosTeachBtn.Location = new System.Drawing.Point(256, 166);
            this.StopRotPosTeachBtn.Name = "StopRotPosTeachBtn";
            this.StopRotPosTeachBtn.Size = new System.Drawing.Size(94, 41);
            this.StopRotPosTeachBtn.TabIndex = 0;
            this.StopRotPosTeachBtn.Text = "保存";
            this.StopRotPosTeachBtn.UseVisualStyleBackColor = true;
            this.StopRotPosTeachBtn.Click += new System.EventHandler(this.StopRotPosTeachBtn_Click);
            // 
            // MoveToRotPosBtn
            // 
            this.MoveToRotPosBtn.Location = new System.Drawing.Point(18, 163);
            this.MoveToRotPosBtn.Name = "MoveToRotPosBtn";
            this.MoveToRotPosBtn.Size = new System.Drawing.Size(86, 44);
            this.MoveToRotPosBtn.TabIndex = 0;
            this.MoveToRotPosBtn.Text = "MoveTo";
            this.MoveToRotPosBtn.UseVisualStyleBackColor = true;
            this.MoveToRotPosBtn.Click += new System.EventHandler(this.MoveToRotPosBtn_Click);
            // 
            // CaliRotCenterBtn
            // 
            this.CaliRotCenterBtn.Location = new System.Drawing.Point(130, 164);
            this.CaliRotCenterBtn.Name = "CaliRotCenterBtn";
            this.CaliRotCenterBtn.Size = new System.Drawing.Size(107, 43);
            this.CaliRotCenterBtn.TabIndex = 0;
            this.CaliRotCenterBtn.Text = "标定旋转中心";
            this.CaliRotCenterBtn.UseVisualStyleBackColor = true;
            this.CaliRotCenterBtn.Click += new System.EventHandler(this.CaliRotCenterBtn_Click);
            // 
            // RotTeachBtn
            // 
            this.RotTeachBtn.Location = new System.Drawing.Point(14, 24);
            this.RotTeachBtn.Name = "RotTeachBtn";
            this.RotTeachBtn.Size = new System.Drawing.Size(90, 43);
            this.RotTeachBtn.TabIndex = 0;
            this.RotTeachBtn.Text = "示教";
            this.RotTeachBtn.UseVisualStyleBackColor = true;
            this.RotTeachBtn.Click += new System.EventHandler(this.RotTeachBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.GrabImgBtn);
            this.groupBox2.Controls.Add(this.LocalModelCbx);
            this.groupBox2.Controls.Add(this.CamLightParaTeachBtn);
            this.groupBox2.Controls.Add(this.LocalParaTeachBtn);
            this.groupBox2.Location = new System.Drawing.Point(6, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 156);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1.视觉示教";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(81, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "定位模式";
            // 
            // GrabImgBtn
            // 
            this.GrabImgBtn.Location = new System.Drawing.Point(257, 91);
            this.GrabImgBtn.Name = "GrabImgBtn";
            this.GrabImgBtn.Size = new System.Drawing.Size(84, 44);
            this.GrabImgBtn.TabIndex = 5;
            this.GrabImgBtn.Text = "采集图片";
            this.GrabImgBtn.UseVisualStyleBackColor = true;
            this.GrabImgBtn.Click += new System.EventHandler(this.GrabImgBtn_Click);
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
            this.LocalModelCbx.Location = new System.Drawing.Point(136, 39);
            this.LocalModelCbx.Name = "LocalModelCbx";
            this.LocalModelCbx.Size = new System.Drawing.Size(113, 20);
            this.LocalModelCbx.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(812, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "标定模式";
            // 
            // CaliModelCbx
            // 
            this.CaliModelCbx.FormattingEnabled = true;
            this.CaliModelCbx.Items.AddRange(new object[] {
            "常规标定",
            "单轴标定"});
            this.CaliModelCbx.Location = new System.Drawing.Point(867, 13);
            this.CaliModelCbx.Name = "CaliModelCbx";
            this.CaliModelCbx.Size = new System.Drawing.Size(113, 20);
            this.CaliModelCbx.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CaliBtn);
            this.groupBox3.Controls.Add(this.MoveToCaliEndPtBtn);
            this.groupBox3.Controls.Add(this.MoveToCaliStartPtBtn);
            this.groupBox3.Controls.Add(this.EndCaliPtSaveBtn);
            this.groupBox3.Controls.Add(this.StartCaliPtSaveBtn);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.EndCaliPtTeachTbx);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.StartCaliPtTeachTbx);
            this.groupBox3.Controls.Add(this.StartCaliPtTeachBtn);
            this.groupBox3.Controls.Add(this.EndCaliPtTeachBtn);
            this.groupBox3.Location = new System.Drawing.Point(9, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 305);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "4.手眼标定";
            // 
            // CaliBtn
            // 
            this.CaliBtn.Location = new System.Drawing.Point(150, 250);
            this.CaliBtn.Name = "CaliBtn";
            this.CaliBtn.Size = new System.Drawing.Size(85, 48);
            this.CaliBtn.TabIndex = 13;
            this.CaliBtn.Text = "标定";
            this.CaliBtn.UseVisualStyleBackColor = true;
            this.CaliBtn.Click += new System.EventHandler(this.CaliBtn_Click);
            // 
            // MoveToCaliEndPtBtn
            // 
            this.MoveToCaliEndPtBtn.Location = new System.Drawing.Point(276, 189);
            this.MoveToCaliEndPtBtn.Name = "MoveToCaliEndPtBtn";
            this.MoveToCaliEndPtBtn.Size = new System.Drawing.Size(91, 48);
            this.MoveToCaliEndPtBtn.TabIndex = 11;
            this.MoveToCaliEndPtBtn.Text = "MoveTo";
            this.MoveToCaliEndPtBtn.UseVisualStyleBackColor = true;
            this.MoveToCaliEndPtBtn.Click += new System.EventHandler(this.MoveToCaliEndPtBtn_Click);
            // 
            // MoveToCaliStartPtBtn
            // 
            this.MoveToCaliStartPtBtn.Location = new System.Drawing.Point(276, 69);
            this.MoveToCaliStartPtBtn.Name = "MoveToCaliStartPtBtn";
            this.MoveToCaliStartPtBtn.Size = new System.Drawing.Size(91, 49);
            this.MoveToCaliStartPtBtn.TabIndex = 10;
            this.MoveToCaliStartPtBtn.Text = "MoveTo";
            this.MoveToCaliStartPtBtn.UseVisualStyleBackColor = true;
            this.MoveToCaliStartPtBtn.Click += new System.EventHandler(this.MoveToCaliStartPtBtn_Click);
            // 
            // EndCaliPtSaveBtn
            // 
            this.EndCaliPtSaveBtn.Location = new System.Drawing.Point(143, 189);
            this.EndCaliPtSaveBtn.Name = "EndCaliPtSaveBtn";
            this.EndCaliPtSaveBtn.Size = new System.Drawing.Size(94, 49);
            this.EndCaliPtSaveBtn.TabIndex = 9;
            this.EndCaliPtSaveBtn.Text = "保存";
            this.EndCaliPtSaveBtn.UseVisualStyleBackColor = true;
            this.EndCaliPtSaveBtn.Click += new System.EventHandler(this.EndCaliPtSaveBtn_Click);
            // 
            // StartCaliPtSaveBtn
            // 
            this.StartCaliPtSaveBtn.Enabled = false;
            this.StartCaliPtSaveBtn.Location = new System.Drawing.Point(150, 69);
            this.StartCaliPtSaveBtn.Name = "StartCaliPtSaveBtn";
            this.StartCaliPtSaveBtn.Size = new System.Drawing.Size(85, 49);
            this.StartCaliPtSaveBtn.TabIndex = 9;
            this.StartCaliPtSaveBtn.Text = "保存";
            this.StartCaliPtSaveBtn.UseVisualStyleBackColor = true;
            this.StartCaliPtSaveBtn.Click += new System.EventHandler(this.StartCaliPtSaveBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "X,Y,Theta坐标";
            // 
            // EndCaliPtTeachTbx
            // 
            this.EndCaliPtTeachTbx.Location = new System.Drawing.Point(116, 154);
            this.EndCaliPtTeachTbx.Name = "EndCaliPtTeachTbx";
            this.EndCaliPtTeachTbx.Size = new System.Drawing.Size(244, 21);
            this.EndCaliPtTeachTbx.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "X,Y,Theta坐标";
            // 
            // StartCaliPtTeachTbx
            // 
            this.StartCaliPtTeachTbx.Location = new System.Drawing.Point(116, 33);
            this.StartCaliPtTeachTbx.Name = "StartCaliPtTeachTbx";
            this.StartCaliPtTeachTbx.Size = new System.Drawing.Size(244, 21);
            this.StartCaliPtTeachTbx.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MotionAdjBtn
            // 
            this.MotionAdjBtn.Location = new System.Drawing.Point(261, 624);
            this.MotionAdjBtn.Name = "MotionAdjBtn";
            this.MotionAdjBtn.Size = new System.Drawing.Size(106, 55);
            this.MotionAdjBtn.TabIndex = 7;
            this.MotionAdjBtn.Text = "Motion";
            this.MotionAdjBtn.UseVisualStyleBackColor = true;
            this.MotionAdjBtn.Click += new System.EventHandler(this.MotionAdjBtn_Click);
            // 
            // CaliParaSaveBtn
            // 
            this.CaliParaSaveBtn.Location = new System.Drawing.Point(825, 623);
            this.CaliParaSaveBtn.Name = "CaliParaSaveBtn";
            this.CaliParaSaveBtn.Size = new System.Drawing.Size(113, 56);
            this.CaliParaSaveBtn.TabIndex = 7;
            this.CaliParaSaveBtn.Text = "标定参数保存";
            this.CaliParaSaveBtn.UseVisualStyleBackColor = true;
            this.CaliParaSaveBtn.Click += new System.EventHandler(this.CaliParaSaveBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ContinueGrabBtn);
            this.groupBox5.Controls.Add(this.SaveImgBtn);
            this.groupBox5.Controls.Add(this.StopGrabBtn);
            this.groupBox5.Location = new System.Drawing.Point(3, 244);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(366, 140);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "2.连续采集";
            // 
            // ContinueGrabBtn
            // 
            this.ContinueGrabBtn.Location = new System.Drawing.Point(20, 48);
            this.ContinueGrabBtn.Name = "ContinueGrabBtn";
            this.ContinueGrabBtn.Size = new System.Drawing.Size(89, 52);
            this.ContinueGrabBtn.TabIndex = 10;
            this.ContinueGrabBtn.Text = "连续采集";
            this.ContinueGrabBtn.UseVisualStyleBackColor = true;
            this.ContinueGrabBtn.Click += new System.EventHandler(this.ContinueGrabBtn_Click);
            // 
            // SaveImgBtn
            // 
            this.SaveImgBtn.Location = new System.Drawing.Point(259, 48);
            this.SaveImgBtn.Name = "SaveImgBtn";
            this.SaveImgBtn.Size = new System.Drawing.Size(87, 52);
            this.SaveImgBtn.TabIndex = 9;
            this.SaveImgBtn.Text = "保存图片";
            this.SaveImgBtn.UseVisualStyleBackColor = true;
            this.SaveImgBtn.Click += new System.EventHandler(this.SaveImgBtn_Click);
            // 
            // StopGrabBtn
            // 
            this.StopGrabBtn.Location = new System.Drawing.Point(141, 48);
            this.StopGrabBtn.Name = "StopGrabBtn";
            this.StopGrabBtn.Size = new System.Drawing.Size(84, 52);
            this.StopGrabBtn.TabIndex = 9;
            this.StopGrabBtn.Text = "停止采集";
            this.StopGrabBtn.UseVisualStyleBackColor = true;
            this.StopGrabBtn.Click += new System.EventHandler(this.StopGrabBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(696, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(402, 574);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(394, 548);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "视觉示教";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(394, 548);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "标定示教";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FindCirCenterCbx
            // 
            this.FindCirCenterCbx.FormattingEnabled = true;
            this.FindCirCenterCbx.Items.AddRange(new object[] {
            "拟合圆",
            "中垂线找圆心"});
            this.FindCirCenterCbx.Location = new System.Drawing.Point(124, 34);
            this.FindCirCenterCbx.Name = "FindCirCenterCbx";
            this.FindCirCenterCbx.Size = new System.Drawing.Size(113, 20);
            this.FindCirCenterCbx.TabIndex = 8;
            // 
            // FrmCaliPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 688);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CaliModelCbx);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.CaliParaSaveBtn);
            this.Controls.Add(this.MotionAdjBtn);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCaliPara";
            this.Text = "FrmCaliPara";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCaliPara_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCaliPara_FormClosed);
            this.Load += new System.EventHandler(this.FrmCaliPara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AngleRangeNumUpDn)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RotCountNumUpDn)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CamLightParaTeachBtn;
        private System.Windows.Forms.Button LocalParaTeachBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button RotPosGetBtn;
        private System.Windows.Forms.Button StartCaliPtTeachBtn;
        private System.Windows.Forms.TextBox RotPosTbx;
        private System.Windows.Forms.Button EndCaliPtTeachBtn;
        private System.Windows.Forms.NumericUpDown AngleRangeNumUpDn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown RotCountNumUpDn;
        private System.Windows.Forms.Button StopRotPosTeachBtn;
        private System.Windows.Forms.Button MoveToRotPosBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button CaliRotCenterBtn;
        private System.Windows.Forms.Button CaliBtn;
        private System.Windows.Forms.Button MoveToCaliEndPtBtn;
        private System.Windows.Forms.Button MoveToCaliStartPtBtn;
        private System.Windows.Forms.Button EndCaliPtSaveBtn;
        private System.Windows.Forms.Button StartCaliPtSaveBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EndCaliPtTeachTbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StartCaliPtTeachTbx;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button RotTeachBtn;
        private System.Windows.Forms.Button MotionAdjBtn;
        private System.Windows.Forms.Button CaliParaSaveBtn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button ContinueGrabBtn;
        private System.Windows.Forms.Button StopGrabBtn;
        private System.Windows.Forms.ComboBox LocalModelCbx;
        private System.Windows.Forms.Button GrabImgBtn;
        private System.Windows.Forms.Button SaveImgBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CaliModelCbx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox FindCirCenterCbx;
    }
}