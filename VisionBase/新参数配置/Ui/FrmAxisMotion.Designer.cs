
namespace VisionBase
{
    partial class FrmAxisMotion
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRealR = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRealZ1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRealY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRealX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRltDis = new System.Windows.Forms.TextBox();
            this.radioJog = new System.Windows.Forms.RadioButton();
            this.radioRlt = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRealR);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtRealZ1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtRealY);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtRealX);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(470, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(126, 187);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "实时位置：（mm）";
            // 
            // txtRealR
            // 
            this.txtRealR.Location = new System.Drawing.Point(39, 146);
            this.txtRealR.Name = "txtRealR";
            this.txtRealR.ReadOnly = true;
            this.txtRealR.Size = new System.Drawing.Size(75, 21);
            this.txtRealR.TabIndex = 12;
            this.txtRealR.Text = "999.999";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "R：";
            // 
            // txtRealZ1
            // 
            this.txtRealZ1.Location = new System.Drawing.Point(39, 105);
            this.txtRealZ1.Name = "txtRealZ1";
            this.txtRealZ1.ReadOnly = true;
            this.txtRealZ1.Size = new System.Drawing.Size(75, 21);
            this.txtRealZ1.TabIndex = 10;
            this.txtRealZ1.Text = "999.999";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Z：";
            // 
            // txtRealY
            // 
            this.txtRealY.Location = new System.Drawing.Point(39, 63);
            this.txtRealY.Name = "txtRealY";
            this.txtRealY.ReadOnly = true;
            this.txtRealY.Size = new System.Drawing.Size(75, 21);
            this.txtRealY.TabIndex = 8;
            this.txtRealY.Text = "999.999";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Y：";
            // 
            // txtRealX
            // 
            this.txtRealX.Location = new System.Drawing.Point(39, 22);
            this.txtRealX.Name = "txtRealX";
            this.txtRealX.ReadOnly = true;
            this.txtRealX.Size = new System.Drawing.Size(75, 21);
            this.txtRealX.TabIndex = 6;
            this.txtRealX.Text = "999.999";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "X：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRltDis);
            this.groupBox2.Controls.Add(this.radioJog);
            this.groupBox2.Controls.Add(this.radioRlt);
            this.groupBox2.Location = new System.Drawing.Point(464, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 97);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运动模式：";
            // 
            // txtRltDis
            // 
            this.txtRltDis.Location = new System.Drawing.Point(64, 66);
            this.txtRltDis.Name = "txtRltDis";
            this.txtRltDis.Size = new System.Drawing.Size(59, 21);
            this.txtRltDis.TabIndex = 3;
            this.txtRltDis.Text = "1";
            // 
            // radioJog
            // 
            this.radioJog.AutoSize = true;
            this.radioJog.Enabled = false;
            this.radioJog.Location = new System.Drawing.Point(11, 27);
            this.radioJog.Name = "radioJog";
            this.radioJog.Size = new System.Drawing.Size(47, 16);
            this.radioJog.TabIndex = 1;
            this.radioJog.Text = "连续";
            this.radioJog.UseVisualStyleBackColor = true;
            // 
            // radioRlt
            // 
            this.radioRlt.AutoSize = true;
            this.radioRlt.Checked = true;
            this.radioRlt.Location = new System.Drawing.Point(11, 70);
            this.radioRlt.Name = "radioRlt";
            this.radioRlt.Size = new System.Drawing.Size(47, 16);
            this.radioRlt.TabIndex = 2;
            this.radioRlt.TabStop = true;
            this.radioRlt.Text = "相对";
            this.radioRlt.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 68);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "坐标系选择";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button9
            // 
            this.button9.BackgroundImage = global::VisionBase.Properties.Resources.right;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.Location = new System.Drawing.Point(275, 182);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(68, 40);
            this.button9.TabIndex = 17;
            this.button9.Tag = "X";
            this.button9.Text = "X+";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.BackgroundImage = global::VisionBase.Properties.Resources.left;
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button10.Location = new System.Drawing.Point(111, 182);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(68, 40);
            this.button10.TabIndex = 16;
            this.button10.Tag = "X";
            this.button10.Text = "X-";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisNegMotionMouseDn_Click);
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::VisionBase.Properties.Resources.up1;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.Location = new System.Drawing.Point(384, 81);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(45, 60);
            this.button7.TabIndex = 15;
            this.button7.Tag = "Z";
            this.button7.Text = "Z+";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisPosMotionMouseDn_Click);
            // 
            // button8
            // 
            this.button8.BackgroundImage = global::VisionBase.Properties.Resources.down1;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button8.Location = new System.Drawing.Point(383, 250);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(45, 60);
            this.button8.TabIndex = 14;
            this.button8.Tag = "Z";
            this.button8.Text = "Z-";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisNegMotionMouseDn_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImage = global::VisionBase.Properties.Resources.right_rotate;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Location = new System.Drawing.Point(273, 288);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(68, 40);
            this.button5.TabIndex = 13;
            this.button5.Tag = "R";
            this.button5.Text = "R+";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisPosMotionMouseDn_Click);
            // 
            // button6
            // 
            this.button6.BackgroundImage = global::VisionBase.Properties.Resources.left_rotate;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.Location = new System.Drawing.Point(110, 287);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(68, 40);
            this.button6.TabIndex = 12;
            this.button6.Tag = "R";
            this.button6.Text = "R-";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisNegMotionMouseDn_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::VisionBase.Properties.Resources.down;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(205, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(45, 60);
            this.button2.TabIndex = 11;
            this.button2.Tag = "Y";
            this.button2.Text = "Y-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisNegMotionMouseDn_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::VisionBase.Properties.Resources.up;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(205, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 60);
            this.button1.TabIndex = 10;
            this.button1.Tag = "Y";
            this.button1.Text = "Y+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisPosMotionMouseDn_Click);
            // 
            // FrmAxisMotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 418);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FrmAxisMotion";
            this.Text = "FrmAxisMotion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAxisMotion_FormClosing);
            this.Load += new System.EventHandler(this.FrmAxisMotion_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRealR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRealZ1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRealY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRealX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRltDis;
        private System.Windows.Forms.RadioButton radioJog;
        private System.Windows.Forms.RadioButton radioRlt;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}