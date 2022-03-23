namespace VisionBase
{
    partial class SubFrmFinish
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
            this.panelOperator = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.NumUpDn_Offset_thta = new System.Windows.Forms.NumericUpDown();
            this.NumUpDn_Offset_y = new System.Windows.Forms.NumericUpDown();
            this.NumUpDn_Offset_x = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NumUpDn_offset_theta_range = new System.Windows.Forms.NumericUpDown();
            this.NumUpDn_offset_y_range = new System.Windows.Forms.NumericUpDown();
            this.NumUpDn_offset_x_range = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ProductPixelPosTbx = new System.Windows.Forms.TextBox();
            this.GetProductPixelPosBtn = new System.Windows.Forms.Button();
            this.ProductPixelPosTeachBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CoordiCbx = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GrabPosGetBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GrabPosTbx = new System.Windows.Forms.TextBox();
            this.GrabPosTeachBtn = new System.Windows.Forms.Button();
            this.panelOperator.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_Offset_thta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_Offset_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_Offset_x)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_offset_theta_range)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_offset_y_range)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_offset_x_range)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOperator
            // 
            this.panelOperator.Controls.Add(this.groupBox4);
            this.panelOperator.Controls.Add(this.groupBox3);
            this.panelOperator.Controls.Add(this.groupBox2);
            this.panelOperator.Controls.Add(this.groupBox1);
            this.panelOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOperator.Location = new System.Drawing.Point(0, 0);
            this.panelOperator.Name = "panelOperator";
            this.panelOperator.Size = new System.Drawing.Size(500, 640);
            this.panelOperator.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.NumUpDn_Offset_thta);
            this.groupBox4.Controls.Add(this.NumUpDn_Offset_y);
            this.groupBox4.Controls.Add(this.NumUpDn_Offset_x);
            this.groupBox4.Location = new System.Drawing.Point(23, 506);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(454, 87);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "对位补偿量";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(330, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "Offset_theta";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(187, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "Offset_y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "Offset_x";
            // 
            // NumUpDn_Offset_thta
            // 
            this.NumUpDn_Offset_thta.DecimalPlaces = 5;
            this.NumUpDn_Offset_thta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NumUpDn_Offset_thta.Location = new System.Drawing.Point(323, 42);
            this.NumUpDn_Offset_thta.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.NumUpDn_Offset_thta.Name = "NumUpDn_Offset_thta";
            this.NumUpDn_Offset_thta.Size = new System.Drawing.Size(101, 21);
            this.NumUpDn_Offset_thta.TabIndex = 2;
            this.NumUpDn_Offset_thta.ValueChanged += new System.EventHandler(this.NumUpDn_Offset_thta_ValueChanged);
            // 
            // NumUpDn_Offset_y
            // 
            this.NumUpDn_Offset_y.DecimalPlaces = 5;
            this.NumUpDn_Offset_y.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NumUpDn_Offset_y.Location = new System.Drawing.Point(171, 42);
            this.NumUpDn_Offset_y.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.NumUpDn_Offset_y.Name = "NumUpDn_Offset_y";
            this.NumUpDn_Offset_y.Size = new System.Drawing.Size(101, 21);
            this.NumUpDn_Offset_y.TabIndex = 2;
            this.NumUpDn_Offset_y.ValueChanged += new System.EventHandler(this.NumUpDn_Offset_y_ValueChanged);
            // 
            // NumUpDn_Offset_x
            // 
            this.NumUpDn_Offset_x.DecimalPlaces = 5;
            this.NumUpDn_Offset_x.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NumUpDn_Offset_x.Location = new System.Drawing.Point(24, 42);
            this.NumUpDn_Offset_x.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.NumUpDn_Offset_x.Name = "NumUpDn_Offset_x";
            this.NumUpDn_Offset_x.Size = new System.Drawing.Size(92, 21);
            this.NumUpDn_Offset_x.TabIndex = 1;
            this.NumUpDn_Offset_x.ValueChanged += new System.EventHandler(this.NumUpDn_Offset_x_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.NumUpDn_offset_theta_range);
            this.groupBox3.Controls.Add(this.NumUpDn_offset_y_range);
            this.groupBox3.Controls.Add(this.NumUpDn_offset_x_range);
            this.groupBox3.Location = new System.Drawing.Point(23, 343);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(454, 114);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "偏移范围";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "offset_Theta_range";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "offset_y_range";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Offset_x_range";
            // 
            // NumUpDn_offset_theta_range
            // 
            this.NumUpDn_offset_theta_range.DecimalPlaces = 5;
            this.NumUpDn_offset_theta_range.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NumUpDn_offset_theta_range.Location = new System.Drawing.Point(296, 46);
            this.NumUpDn_offset_theta_range.Name = "NumUpDn_offset_theta_range";
            this.NumUpDn_offset_theta_range.Size = new System.Drawing.Size(103, 21);
            this.NumUpDn_offset_theta_range.TabIndex = 1;
            this.NumUpDn_offset_theta_range.ValueChanged += new System.EventHandler(this.NumUpDn_offset_theta_range_ValueChanged);
            // 
            // NumUpDn_offset_y_range
            // 
            this.NumUpDn_offset_y_range.DecimalPlaces = 5;
            this.NumUpDn_offset_y_range.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NumUpDn_offset_y_range.Location = new System.Drawing.Point(153, 46);
            this.NumUpDn_offset_y_range.Name = "NumUpDn_offset_y_range";
            this.NumUpDn_offset_y_range.Size = new System.Drawing.Size(101, 21);
            this.NumUpDn_offset_y_range.TabIndex = 1;
            this.NumUpDn_offset_y_range.ValueChanged += new System.EventHandler(this.NumUpDn_offset_y_range_ValueChanged);
            // 
            // NumUpDn_offset_x_range
            // 
            this.NumUpDn_offset_x_range.DecimalPlaces = 5;
            this.NumUpDn_offset_x_range.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NumUpDn_offset_x_range.Location = new System.Drawing.Point(16, 46);
            this.NumUpDn_offset_x_range.Name = "NumUpDn_offset_x_range";
            this.NumUpDn_offset_x_range.Size = new System.Drawing.Size(92, 21);
            this.NumUpDn_offset_x_range.TabIndex = 0;
            this.NumUpDn_offset_x_range.ValueChanged += new System.EventHandler(this.NumUpDn_offset_x_range_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ProductPixelPosTbx);
            this.groupBox2.Controls.Add(this.GetProductPixelPosBtn);
            this.groupBox2.Controls.Add(this.ProductPixelPosTeachBtn);
            this.groupBox2.Location = new System.Drawing.Point(23, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(454, 125);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "示教产品的像素坐标";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "Row,Col,Theta坐标：";
            // 
            // ProductPixelPosTbx
            // 
            this.ProductPixelPosTbx.Location = new System.Drawing.Point(135, 83);
            this.ProductPixelPosTbx.Name = "ProductPixelPosTbx";
            this.ProductPixelPosTbx.Size = new System.Drawing.Size(289, 21);
            this.ProductPixelPosTbx.TabIndex = 21;
            // 
            // GetProductPixelPosBtn
            // 
            this.GetProductPixelPosBtn.Enabled = false;
            this.GetProductPixelPosBtn.Location = new System.Drawing.Point(170, 26);
            this.GetProductPixelPosBtn.Name = "GetProductPixelPosBtn";
            this.GetProductPixelPosBtn.Size = new System.Drawing.Size(84, 46);
            this.GetProductPixelPosBtn.TabIndex = 18;
            this.GetProductPixelPosBtn.Text = "计算";
            this.GetProductPixelPosBtn.UseVisualStyleBackColor = true;
            this.GetProductPixelPosBtn.Click += new System.EventHandler(this.GetProductPixelPosBtn_Click);
            // 
            // ProductPixelPosTeachBtn
            // 
            this.ProductPixelPosTeachBtn.Location = new System.Drawing.Point(15, 27);
            this.ProductPixelPosTeachBtn.Name = "ProductPixelPosTeachBtn";
            this.ProductPixelPosTeachBtn.Size = new System.Drawing.Size(84, 46);
            this.ProductPixelPosTeachBtn.TabIndex = 17;
            this.ProductPixelPosTeachBtn.Text = "示教";
            this.ProductPixelPosTeachBtn.UseVisualStyleBackColor = true;
            this.ProductPixelPosTeachBtn.Click += new System.EventHandler(this.ProductPixelPosTeachBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CoordiCbx);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.GrabPosGetBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.GrabPosTbx);
            this.groupBox1.Controls.Add(this.GrabPosTeachBtn);
            this.groupBox1.Location = new System.Drawing.Point(23, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 132);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "拍照坐标示教";
            // 
            // CoordiCbx
            // 
            this.CoordiCbx.FormattingEnabled = true;
            this.CoordiCbx.Location = new System.Drawing.Point(15, 54);
            this.CoordiCbx.Name = "CoordiCbx";
            this.CoordiCbx.Size = new System.Drawing.Size(143, 20);
            this.CoordiCbx.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "坐标系选择";
            // 
            // GrabPosGetBtn
            // 
            this.GrabPosGetBtn.Enabled = false;
            this.GrabPosGetBtn.Location = new System.Drawing.Point(296, 29);
            this.GrabPosGetBtn.Name = "GrabPosGetBtn";
            this.GrabPosGetBtn.Size = new System.Drawing.Size(84, 47);
            this.GrabPosGetBtn.TabIndex = 18;
            this.GrabPosGetBtn.Text = "获取坐标";
            this.GrabPosGetBtn.UseVisualStyleBackColor = true;
            this.GrabPosGetBtn.Click += new System.EventHandler(this.GrabPosGetBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "X,Y,Theta坐标：";
            // 
            // GrabPosTbx
            // 
            this.GrabPosTbx.Location = new System.Drawing.Point(113, 94);
            this.GrabPosTbx.Name = "GrabPosTbx";
            this.GrabPosTbx.Size = new System.Drawing.Size(311, 21);
            this.GrabPosTbx.TabIndex = 19;
            // 
            // GrabPosTeachBtn
            // 
            this.GrabPosTeachBtn.Location = new System.Drawing.Point(184, 29);
            this.GrabPosTeachBtn.Name = "GrabPosTeachBtn";
            this.GrabPosTeachBtn.Size = new System.Drawing.Size(84, 47);
            this.GrabPosTeachBtn.TabIndex = 17;
            this.GrabPosTeachBtn.Text = "示教";
            this.GrabPosTeachBtn.UseVisualStyleBackColor = true;
            this.GrabPosTeachBtn.Click += new System.EventHandler(this.GrabPosTeachBtn_Click);
            // 
            // SubFrmFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 640);
            this.Controls.Add(this.panelOperator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubFrmFinish";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mark点示教";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubFrmFinish_FormClosing);
            this.Load += new System.EventHandler(this.FrmSubFrmFindLine_Load);
            this.panelOperator.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_Offset_thta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_Offset_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_Offset_x)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_offset_theta_range)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_offset_y_range)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDn_offset_x_range)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOperator;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button GetProductPixelPosBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button GrabPosGetBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox GrabPosTbx;
        private System.Windows.Forms.Button GrabPosTeachBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ProductPixelPosTbx;
        private System.Windows.Forms.Button ProductPixelPosTeachBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CoordiCbx;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown NumUpDn_offset_theta_range;
        private System.Windows.Forms.NumericUpDown NumUpDn_offset_y_range;
        private System.Windows.Forms.NumericUpDown NumUpDn_offset_x_range;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown NumUpDn_Offset_y;
        private System.Windows.Forms.NumericUpDown NumUpDn_Offset_x;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown NumUpDn_Offset_thta;
    }
}