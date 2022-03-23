
namespace VisionBase
{
    partial class FrmCali9PtPara
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.GrabImgBtn = new System.Windows.Forms.Button();
            this.LocalModelCbx = new System.Windows.Forms.ComboBox();
            this.CamLightParaTeachBtn = new System.Windows.Forms.Button();
            this.LocalParaTeachBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ContinueGrabBtn = new System.Windows.Forms.Button();
            this.SaveImgBtn = new System.Windows.Forms.Button();
            this.StopGrabBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button14 = new System.Windows.Forms.Button();
            this.CaliBtn = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.MoveToCaliEndPtBtn = new System.Windows.Forms.Button();
            this.MoveToCaliStartPtBtn = new System.Windows.Forms.Button();
            this.EndCaliPtSaveBtn = new System.Windows.Forms.Button();
            this.StartCaliPtSaveBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.EndCaliPtTeachTbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StartCaliPtTeachTbx = new System.Windows.Forms.TextBox();
            this.StartCaliPtTeachBtn = new System.Windows.Forms.Button();
            this.EndCaliPtTeachBtn = new System.Windows.Forms.Button();
            this.MotionAdjBtn = new System.Windows.Forms.Button();
            this.CaliParaSaveBtn = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(10, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 607);
            this.panel1.TabIndex = 2;
            // 
            // GrabImgBtn
            // 
            this.GrabImgBtn.Location = new System.Drawing.Point(237, 58);
            this.GrabImgBtn.Name = "GrabImgBtn";
            this.GrabImgBtn.Size = new System.Drawing.Size(84, 35);
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
            this.LocalModelCbx.Location = new System.Drawing.Point(56, 20);
            this.LocalModelCbx.Name = "LocalModelCbx";
            this.LocalModelCbx.Size = new System.Drawing.Size(113, 20);
            this.LocalModelCbx.TabIndex = 4;
            // 
            // CamLightParaTeachBtn
            // 
            this.CamLightParaTeachBtn.Location = new System.Drawing.Point(28, 58);
            this.CamLightParaTeachBtn.Name = "CamLightParaTeachBtn";
            this.CamLightParaTeachBtn.Size = new System.Drawing.Size(91, 35);
            this.CamLightParaTeachBtn.TabIndex = 0;
            this.CamLightParaTeachBtn.Text = "相机光源示教";
            this.CamLightParaTeachBtn.UseVisualStyleBackColor = true;
            this.CamLightParaTeachBtn.Click += new System.EventHandler(this.CamLightParaTeachBtn_Click);
            // 
            // LocalParaTeachBtn
            // 
            this.LocalParaTeachBtn.Location = new System.Drawing.Point(137, 58);
            this.LocalParaTeachBtn.Name = "LocalParaTeachBtn";
            this.LocalParaTeachBtn.Size = new System.Drawing.Size(84, 35);
            this.LocalParaTeachBtn.TabIndex = 0;
            this.LocalParaTeachBtn.Text = "定位示教";
            this.LocalParaTeachBtn.UseVisualStyleBackColor = true;
            this.LocalParaTeachBtn.Click += new System.EventHandler(this.LocalParaTeachBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.GrabImgBtn);
            this.groupBox2.Controls.Add(this.LocalModelCbx);
            this.groupBox2.Controls.Add(this.CamLightParaTeachBtn);
            this.groupBox2.Controls.Add(this.LocalParaTeachBtn);
            this.groupBox2.Location = new System.Drawing.Point(686, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 121);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1.视觉示教";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ContinueGrabBtn);
            this.groupBox5.Controls.Add(this.SaveImgBtn);
            this.groupBox5.Controls.Add(this.StopGrabBtn);
            this.groupBox5.Location = new System.Drawing.Point(690, 150);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(336, 91);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "2.连续采集";
            // 
            // ContinueGrabBtn
            // 
            this.ContinueGrabBtn.Location = new System.Drawing.Point(27, 25);
            this.ContinueGrabBtn.Name = "ContinueGrabBtn";
            this.ContinueGrabBtn.Size = new System.Drawing.Size(89, 37);
            this.ContinueGrabBtn.TabIndex = 10;
            this.ContinueGrabBtn.Text = "连续采集";
            this.ContinueGrabBtn.UseVisualStyleBackColor = true;
            this.ContinueGrabBtn.Click += new System.EventHandler(this.ContinueGrabBtn_Click);
            // 
            // SaveImgBtn
            // 
            this.SaveImgBtn.Location = new System.Drawing.Point(240, 23);
            this.SaveImgBtn.Name = "SaveImgBtn";
            this.SaveImgBtn.Size = new System.Drawing.Size(87, 42);
            this.SaveImgBtn.TabIndex = 9;
            this.SaveImgBtn.Text = "保存图片";
            this.SaveImgBtn.UseVisualStyleBackColor = true;
            this.SaveImgBtn.Click += new System.EventHandler(this.SaveImgBtn_Click);
            // 
            // StopGrabBtn
            // 
            this.StopGrabBtn.Location = new System.Drawing.Point(138, 23);
            this.StopGrabBtn.Name = "StopGrabBtn";
            this.StopGrabBtn.Size = new System.Drawing.Size(84, 42);
            this.StopGrabBtn.TabIndex = 9;
            this.StopGrabBtn.Text = "停止采集";
            this.StopGrabBtn.UseVisualStyleBackColor = true;
            this.StopGrabBtn.Click += new System.EventHandler(this.StopGrabBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button14);
            this.groupBox3.Controls.Add(this.CaliBtn);
            this.groupBox3.Controls.Add(this.button16);
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
            this.groupBox3.Location = new System.Drawing.Point(685, 258);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(341, 357);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3.手眼标定";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(238, 268);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(84, 36);
            this.button14.TabIndex = 12;
            this.button14.Text = "显示结果";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // CaliBtn
            // 
            this.CaliBtn.Location = new System.Drawing.Point(135, 264);
            this.CaliBtn.Name = "CaliBtn";
            this.CaliBtn.Size = new System.Drawing.Size(82, 40);
            this.CaliBtn.TabIndex = 13;
            this.CaliBtn.Text = "标定";
            this.CaliBtn.UseVisualStyleBackColor = true;
            this.CaliBtn.Click += new System.EventHandler(this.CaliBtn_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(17, 268);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(83, 36);
            this.button16.TabIndex = 14;
            this.button16.Text = "测试";
            this.button16.UseVisualStyleBackColor = true;
            // 
            // MoveToCaliEndPtBtn
            // 
            this.MoveToCaliEndPtBtn.Location = new System.Drawing.Point(249, 187);
            this.MoveToCaliEndPtBtn.Name = "MoveToCaliEndPtBtn";
            this.MoveToCaliEndPtBtn.Size = new System.Drawing.Size(77, 34);
            this.MoveToCaliEndPtBtn.TabIndex = 11;
            this.MoveToCaliEndPtBtn.Text = "MoveTo";
            this.MoveToCaliEndPtBtn.UseVisualStyleBackColor = true;
            this.MoveToCaliEndPtBtn.Click += new System.EventHandler(this.MoveToCaliEndPtBtn_Click);
            // 
            // MoveToCaliStartPtBtn
            // 
            this.MoveToCaliStartPtBtn.Location = new System.Drawing.Point(229, 72);
            this.MoveToCaliStartPtBtn.Name = "MoveToCaliStartPtBtn";
            this.MoveToCaliStartPtBtn.Size = new System.Drawing.Size(77, 41);
            this.MoveToCaliStartPtBtn.TabIndex = 10;
            this.MoveToCaliStartPtBtn.Text = "MoveTo";
            this.MoveToCaliStartPtBtn.UseVisualStyleBackColor = true;
            this.MoveToCaliStartPtBtn.Click += new System.EventHandler(this.MoveToCaliStartPtBtn_Click);
            // 
            // EndCaliPtSaveBtn
            // 
            this.EndCaliPtSaveBtn.Location = new System.Drawing.Point(135, 187);
            this.EndCaliPtSaveBtn.Name = "EndCaliPtSaveBtn";
            this.EndCaliPtSaveBtn.Size = new System.Drawing.Size(82, 33);
            this.EndCaliPtSaveBtn.TabIndex = 9;
            this.EndCaliPtSaveBtn.Text = "保存";
            this.EndCaliPtSaveBtn.UseVisualStyleBackColor = true;
            this.EndCaliPtSaveBtn.Click += new System.EventHandler(this.EndCaliPtSaveBtn_Click);
            // 
            // StartCaliPtSaveBtn
            // 
            this.StartCaliPtSaveBtn.Enabled = false;
            this.StartCaliPtSaveBtn.Location = new System.Drawing.Point(120, 71);
            this.StartCaliPtSaveBtn.Name = "StartCaliPtSaveBtn";
            this.StartCaliPtSaveBtn.Size = new System.Drawing.Size(82, 41);
            this.StartCaliPtSaveBtn.TabIndex = 9;
            this.StartCaliPtSaveBtn.Text = "保存";
            this.StartCaliPtSaveBtn.UseVisualStyleBackColor = true;
            this.StartCaliPtSaveBtn.Click += new System.EventHandler(this.StartCaliPtSaveBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "X,Y,Theta坐标";
            // 
            // EndCaliPtTeachTbx
            // 
            this.EndCaliPtTeachTbx.Location = new System.Drawing.Point(108, 153);
            this.EndCaliPtTeachTbx.Name = "EndCaliPtTeachTbx";
            this.EndCaliPtTeachTbx.Size = new System.Drawing.Size(214, 21);
            this.EndCaliPtTeachTbx.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "X,Y,Theta坐标";
            // 
            // StartCaliPtTeachTbx
            // 
            this.StartCaliPtTeachTbx.Location = new System.Drawing.Point(109, 42);
            this.StartCaliPtTeachTbx.Name = "StartCaliPtTeachTbx";
            this.StartCaliPtTeachTbx.Size = new System.Drawing.Size(197, 21);
            this.StartCaliPtTeachTbx.TabIndex = 5;
            // 
            // StartCaliPtTeachBtn
            // 
            this.StartCaliPtTeachBtn.Location = new System.Drawing.Point(22, 71);
            this.StartCaliPtTeachBtn.Name = "StartCaliPtTeachBtn";
            this.StartCaliPtTeachBtn.Size = new System.Drawing.Size(70, 41);
            this.StartCaliPtTeachBtn.TabIndex = 0;
            this.StartCaliPtTeachBtn.Text = "起点示教";
            this.StartCaliPtTeachBtn.UseVisualStyleBackColor = true;
            this.StartCaliPtTeachBtn.Click += new System.EventHandler(this.StartCaliPtTeachBtn_Click);
            // 
            // EndCaliPtTeachBtn
            // 
            this.EndCaliPtTeachBtn.Location = new System.Drawing.Point(23, 186);
            this.EndCaliPtTeachBtn.Name = "EndCaliPtTeachBtn";
            this.EndCaliPtTeachBtn.Size = new System.Drawing.Size(81, 34);
            this.EndCaliPtTeachBtn.TabIndex = 0;
            this.EndCaliPtTeachBtn.Text = "终点示教";
            this.EndCaliPtTeachBtn.UseVisualStyleBackColor = true;
            this.EndCaliPtTeachBtn.Click += new System.EventHandler(this.EndCaliPtTeachBtn_Click);
            // 
            // MotionAdjBtn
            // 
            this.MotionAdjBtn.Location = new System.Drawing.Point(266, 632);
            this.MotionAdjBtn.Name = "MotionAdjBtn";
            this.MotionAdjBtn.Size = new System.Drawing.Size(106, 55);
            this.MotionAdjBtn.TabIndex = 15;
            this.MotionAdjBtn.Text = "Motion";
            this.MotionAdjBtn.UseVisualStyleBackColor = true;
            this.MotionAdjBtn.Click += new System.EventHandler(this.MotionAdjBtn_Click);
            // 
            // CaliParaSaveBtn
            // 
            this.CaliParaSaveBtn.Location = new System.Drawing.Point(898, 625);
            this.CaliParaSaveBtn.Name = "CaliParaSaveBtn";
            this.CaliParaSaveBtn.Size = new System.Drawing.Size(113, 56);
            this.CaliParaSaveBtn.TabIndex = 16;
            this.CaliParaSaveBtn.Text = "标定参数保存";
            this.CaliParaSaveBtn.UseVisualStyleBackColor = true;
            this.CaliParaSaveBtn.Click += new System.EventHandler(this.CaliParaSaveBtn_Click);
            // 
            // FrmCali9PtPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 693);
            this.Controls.Add(this.CaliParaSaveBtn);
            this.Controls.Add(this.MotionAdjBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCali9PtPara";
            this.Text = "FrmCali9PtPara";
            this.Load += new System.EventHandler(this.FrmCali9PtPara_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button GrabImgBtn;
        private System.Windows.Forms.ComboBox LocalModelCbx;
        private System.Windows.Forms.Button CamLightParaTeachBtn;
        private System.Windows.Forms.Button LocalParaTeachBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button ContinueGrabBtn;
        private System.Windows.Forms.Button SaveImgBtn;
        private System.Windows.Forms.Button StopGrabBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button CaliBtn;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button MoveToCaliEndPtBtn;
        private System.Windows.Forms.Button MoveToCaliStartPtBtn;
        private System.Windows.Forms.Button EndCaliPtSaveBtn;
        private System.Windows.Forms.Button StartCaliPtSaveBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EndCaliPtTeachTbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StartCaliPtTeachTbx;
        private System.Windows.Forms.Button StartCaliPtTeachBtn;
        private System.Windows.Forms.Button EndCaliPtTeachBtn;
        private System.Windows.Forms.Button MotionAdjBtn;
        private System.Windows.Forms.Button CaliParaSaveBtn;
    }
}