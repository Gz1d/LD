namespace VisionBase
{
    partial class SubFrmBlobLocal
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.PiexlSizeSaveBtn = new System.Windows.Forms.Button();
            this.PixelSizeNumUpDn = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.PixelSizeTeachBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFindTime = new System.Windows.Forms.TextBox();
            this.ClearParaBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.LineSelectComBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TryDebugBtn = new System.Windows.Forms.Button();
            this.FindLineBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveSerach = new System.Windows.Forms.Button();
            this.txtSearchHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSearchY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AreaMaxBar = new System.Windows.Forms.TrackBar();
            this.AreaMinBar = new System.Windows.Forms.TrackBar();
            this.SaveParaBtn = new System.Windows.Forms.Button();
            this.StopTeachBtn = new System.Windows.Forms.Button();
            this.StartTeachBtn1 = new System.Windows.Forms.Button();
            this.MinGrayBar = new System.Windows.Forms.TrackBar();
            this.MaxGrayBar = new System.Windows.Forms.TrackBar();
            this.AreaMaxTxt = new System.Windows.Forms.Label();
            this.AreaMinTxt = new System.Windows.Forms.Label();
            this.MinGrayTxt = new System.Windows.Forms.Label();
            this.MaxGrayTxt = new System.Windows.Forms.Label();
            this.ThresholdLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Elements = new System.Windows.Forms.Label();
            this.panelOperator.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PixelSizeNumUpDn)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AreaMaxBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AreaMinBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinGrayBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxGrayBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOperator
            // 
            this.panelOperator.Controls.Add(this.groupBox5);
            this.panelOperator.Controls.Add(this.label6);
            this.panelOperator.Controls.Add(this.groupBox2);
            this.panelOperator.Controls.Add(this.groupBox4);
            this.panelOperator.Controls.Add(this.groupBox3);
            this.panelOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOperator.Location = new System.Drawing.Point(0, 0);
            this.panelOperator.Name = "panelOperator";
            this.panelOperator.Size = new System.Drawing.Size(500, 640);
            this.panelOperator.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.PiexlSizeSaveBtn);
            this.groupBox5.Controls.Add(this.PixelSizeNumUpDn);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.PixelSizeTeachBtn);
            this.groupBox5.Location = new System.Drawing.Point(12, 467);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(461, 80);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "像素当量示教";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(261, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(21, 14);
            this.label20.TabIndex = 45;
            this.label20.Text = "mm";
            // 
            // PiexlSizeSaveBtn
            // 
            this.PiexlSizeSaveBtn.Enabled = false;
            this.PiexlSizeSaveBtn.Location = new System.Drawing.Point(326, 23);
            this.PiexlSizeSaveBtn.Name = "PiexlSizeSaveBtn";
            this.PiexlSizeSaveBtn.Size = new System.Drawing.Size(83, 51);
            this.PiexlSizeSaveBtn.TabIndex = 42;
            this.PiexlSizeSaveBtn.Text = "保存";
            this.PiexlSizeSaveBtn.UseVisualStyleBackColor = true;
            this.PiexlSizeSaveBtn.Click += new System.EventHandler(this.PiexlSizeSaveBtn_Click);
            // 
            // PixelSizeNumUpDn
            // 
            this.PixelSizeNumUpDn.DecimalPlaces = 5;
            this.PixelSizeNumUpDn.Enabled = false;
            this.PixelSizeNumUpDn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PixelSizeNumUpDn.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.PixelSizeNumUpDn.Location = new System.Drawing.Point(158, 44);
            this.PixelSizeNumUpDn.Name = "PixelSizeNumUpDn";
            this.PixelSizeNumUpDn.Size = new System.Drawing.Size(97, 23);
            this.PixelSizeNumUpDn.TabIndex = 44;
            this.PixelSizeNumUpDn.ValueChanged += new System.EventHandler(this.PixelSizeNumUpDn_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(160, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 14);
            this.label10.TabIndex = 43;
            this.label10.Text = "像素当量";
            // 
            // PixelSizeTeachBtn
            // 
            this.PixelSizeTeachBtn.Location = new System.Drawing.Point(18, 23);
            this.PixelSizeTeachBtn.Name = "PixelSizeTeachBtn";
            this.PixelSizeTeachBtn.Size = new System.Drawing.Size(87, 48);
            this.PixelSizeTeachBtn.TabIndex = 41;
            this.PixelSizeTeachBtn.Text = "示教";
            this.PixelSizeTeachBtn.UseVisualStyleBackColor = true;
            this.PixelSizeTeachBtn.Click += new System.EventHandler(this.PixelSizeTeachBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(132, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 20);
            this.label6.TabIndex = 26;
            this.label6.Text = "Blob定位参数设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtFindTime);
            this.groupBox2.Controls.Add(this.ClearParaBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.LineSelectComBox);
            this.groupBox2.Location = new System.Drawing.Point(11, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 86);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1、ROI参数设置：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(212, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "ms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "查找时间：";
            // 
            // txtFindTime
            // 
            this.txtFindTime.Location = new System.Drawing.Point(85, 55);
            this.txtFindTime.Name = "txtFindTime";
            this.txtFindTime.Size = new System.Drawing.Size(121, 21);
            this.txtFindTime.TabIndex = 23;
            // 
            // ClearParaBtn
            // 
            this.ClearParaBtn.Location = new System.Drawing.Point(342, 22);
            this.ClearParaBtn.Name = "ClearParaBtn";
            this.ClearParaBtn.Size = new System.Drawing.Size(92, 50);
            this.ClearParaBtn.TabIndex = 16;
            this.ClearParaBtn.Text = "参数清除";
            this.ClearParaBtn.UseVisualStyleBackColor = true;
            this.ClearParaBtn.Click += new System.EventHandler(this.ClearParaBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "编号：";
            // 
            // LineSelectComBox
            // 
            this.LineSelectComBox.FormattingEnabled = true;
            this.LineSelectComBox.Items.AddRange(new object[] {
            "水平线1（最上方）",
            "垂直线1（最右侧）",
            "水平线2（最下方）",
            "垂直线2（最左侧）"});
            this.LineSelectComBox.Location = new System.Drawing.Point(85, 21);
            this.LineSelectComBox.Name = "LineSelectComBox";
            this.LineSelectComBox.Size = new System.Drawing.Size(121, 20);
            this.LineSelectComBox.TabIndex = 11;
            this.LineSelectComBox.SelectedIndexChanged += new System.EventHandler(this.LineSelectComBox_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TryDebugBtn);
            this.groupBox4.Controls.Add(this.FindLineBtn);
            this.groupBox4.Location = new System.Drawing.Point(12, 553);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(461, 76);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "3、测试查找结果：";
            // 
            // TryDebugBtn
            // 
            this.TryDebugBtn.Location = new System.Drawing.Point(323, 17);
            this.TryDebugBtn.Name = "TryDebugBtn";
            this.TryDebugBtn.Size = new System.Drawing.Size(86, 45);
            this.TryDebugBtn.TabIndex = 4;
            this.TryDebugBtn.Text = "测试";
            this.TryDebugBtn.UseVisualStyleBackColor = true;
            this.TryDebugBtn.Click += new System.EventHandler(this.TryDebugBtn_Click);
            // 
            // FindLineBtn
            // 
            this.FindLineBtn.Location = new System.Drawing.Point(21, 19);
            this.FindLineBtn.Name = "FindLineBtn";
            this.FindLineBtn.Size = new System.Drawing.Size(84, 45);
            this.FindLineBtn.TabIndex = 4;
            this.FindLineBtn.Text = "查找";
            this.FindLineBtn.UseVisualStyleBackColor = true;
            this.FindLineBtn.Click += new System.EventHandler(this.FindLineBtn_Click_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.AreaMaxBar);
            this.groupBox3.Controls.Add(this.AreaMinBar);
            this.groupBox3.Controls.Add(this.SaveParaBtn);
            this.groupBox3.Controls.Add(this.StopTeachBtn);
            this.groupBox3.Controls.Add(this.StartTeachBtn1);
            this.groupBox3.Controls.Add(this.MinGrayBar);
            this.groupBox3.Controls.Add(this.MaxGrayBar);
            this.groupBox3.Controls.Add(this.AreaMaxTxt);
            this.groupBox3.Controls.Add(this.AreaMinTxt);
            this.groupBox3.Controls.Add(this.MinGrayTxt);
            this.groupBox3.Controls.Add(this.MaxGrayTxt);
            this.groupBox3.Controls.Add(this.ThresholdLabel);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.Elements);
            this.groupBox3.Location = new System.Drawing.Point(11, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(462, 337);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2、Blob参数示教：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSaveSerach);
            this.groupBox1.Controls.Add(this.txtSearchHeight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSearchWidth);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtSearchY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSearchX);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(9, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 98);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索框：ROI";
            // 
            // btnSaveSerach
            // 
            this.btnSaveSerach.Location = new System.Drawing.Point(334, 28);
            this.btnSaveSerach.Name = "btnSaveSerach";
            this.btnSaveSerach.Size = new System.Drawing.Size(95, 48);
            this.btnSaveSerach.TabIndex = 16;
            this.btnSaveSerach.Text = "新建";
            this.btnSaveSerach.UseVisualStyleBackColor = true;
            this.btnSaveSerach.Click += new System.EventHandler(this.btnSaveSerach_Click);
            // 
            // txtSearchHeight
            // 
            this.txtSearchHeight.Location = new System.Drawing.Point(213, 60);
            this.txtSearchHeight.Name = "txtSearchHeight";
            this.txtSearchHeight.ReadOnly = true;
            this.txtSearchHeight.Size = new System.Drawing.Size(74, 21);
            this.txtSearchHeight.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Height：";
            // 
            // txtSearchWidth
            // 
            this.txtSearchWidth.Location = new System.Drawing.Point(213, 24);
            this.txtSearchWidth.Name = "txtSearchWidth";
            this.txtSearchWidth.ReadOnly = true;
            this.txtSearchWidth.Size = new System.Drawing.Size(74, 21);
            this.txtSearchWidth.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(160, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "Width：";
            // 
            // txtSearchY
            // 
            this.txtSearchY.Location = new System.Drawing.Point(60, 60);
            this.txtSearchY.Name = "txtSearchY";
            this.txtSearchY.ReadOnly = true;
            this.txtSearchY.Size = new System.Drawing.Size(74, 21);
            this.txtSearchY.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Y：";
            // 
            // txtSearchX
            // 
            this.txtSearchX.Location = new System.Drawing.Point(60, 24);
            this.txtSearchX.Name = "txtSearchX";
            this.txtSearchX.ReadOnly = true;
            this.txtSearchX.Size = new System.Drawing.Size(74, 21);
            this.txtSearchX.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "X：";
            // 
            // AreaMaxBar
            // 
            this.AreaMaxBar.AutoSize = false;
            this.AreaMaxBar.Location = new System.Drawing.Point(158, 238);
            this.AreaMaxBar.Maximum = 4000000;
            this.AreaMaxBar.Minimum = 10000;
            this.AreaMaxBar.Name = "AreaMaxBar";
            this.AreaMaxBar.Size = new System.Drawing.Size(182, 23);
            this.AreaMaxBar.TabIndex = 32;
            this.AreaMaxBar.TickFrequency = 400000;
            this.AreaMaxBar.Value = 200000;
            this.AreaMaxBar.Scroll += new System.EventHandler(this.AreaMaxBar_Scroll);
            // 
            // AreaMinBar
            // 
            this.AreaMinBar.AutoSize = false;
            this.AreaMinBar.Location = new System.Drawing.Point(158, 205);
            this.AreaMinBar.Maximum = 100000;
            this.AreaMinBar.Minimum = 10;
            this.AreaMinBar.Name = "AreaMinBar";
            this.AreaMinBar.Size = new System.Drawing.Size(182, 23);
            this.AreaMinBar.TabIndex = 33;
            this.AreaMinBar.TickFrequency = 10000;
            this.AreaMinBar.Value = 100;
            this.AreaMinBar.Scroll += new System.EventHandler(this.AreaMinBar_Scroll);
            // 
            // SaveParaBtn
            // 
            this.SaveParaBtn.Enabled = false;
            this.SaveParaBtn.Location = new System.Drawing.Point(327, 281);
            this.SaveParaBtn.Name = "SaveParaBtn";
            this.SaveParaBtn.Size = new System.Drawing.Size(83, 43);
            this.SaveParaBtn.TabIndex = 4;
            this.SaveParaBtn.Text = "保存参数";
            this.SaveParaBtn.UseVisualStyleBackColor = true;
            this.SaveParaBtn.Click += new System.EventHandler(this.SaveParaBtn_Click);
            // 
            // StopTeachBtn
            // 
            this.StopTeachBtn.Location = new System.Drawing.Point(171, 280);
            this.StopTeachBtn.Name = "StopTeachBtn";
            this.StopTeachBtn.Size = new System.Drawing.Size(86, 44);
            this.StopTeachBtn.TabIndex = 4;
            this.StopTeachBtn.Text = "停止示教";
            this.StopTeachBtn.UseVisualStyleBackColor = true;
            this.StopTeachBtn.Click += new System.EventHandler(this.StopTeachBtn_Click);
            // 
            // StartTeachBtn1
            // 
            this.StartTeachBtn1.Location = new System.Drawing.Point(21, 278);
            this.StartTeachBtn1.Name = "StartTeachBtn1";
            this.StartTeachBtn1.Size = new System.Drawing.Size(84, 43);
            this.StartTeachBtn1.TabIndex = 4;
            this.StartTeachBtn1.Text = "开始示教";
            this.StartTeachBtn1.UseVisualStyleBackColor = true;
            this.StartTeachBtn1.Click += new System.EventHandler(this.StartTeachBtn1_Click);
            // 
            // MinGrayBar
            // 
            this.MinGrayBar.AutoSize = false;
            this.MinGrayBar.Location = new System.Drawing.Point(158, 133);
            this.MinGrayBar.Maximum = 250;
            this.MinGrayBar.Name = "MinGrayBar";
            this.MinGrayBar.Size = new System.Drawing.Size(182, 23);
            this.MinGrayBar.TabIndex = 34;
            this.MinGrayBar.TickFrequency = 20;
            this.MinGrayBar.Value = 50;
            this.MinGrayBar.Scroll += new System.EventHandler(this.MinGrayBar_Scroll);
            // 
            // MaxGrayBar
            // 
            this.MaxGrayBar.AutoSize = false;
            this.MaxGrayBar.Location = new System.Drawing.Point(158, 167);
            this.MaxGrayBar.Maximum = 255;
            this.MaxGrayBar.Minimum = 10;
            this.MaxGrayBar.Name = "MaxGrayBar";
            this.MaxGrayBar.Size = new System.Drawing.Size(182, 23);
            this.MaxGrayBar.TabIndex = 34;
            this.MaxGrayBar.TickFrequency = 20;
            this.MaxGrayBar.Value = 200;
            this.MaxGrayBar.Scroll += new System.EventHandler(this.MaxGrayBar_Scroll);
            // 
            // AreaMaxTxt
            // 
            this.AreaMaxTxt.AutoSize = true;
            this.AreaMaxTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AreaMaxTxt.Location = new System.Drawing.Point(362, 244);
            this.AreaMaxTxt.Name = "AreaMaxTxt";
            this.AreaMaxTxt.Size = new System.Drawing.Size(48, 16);
            this.AreaMaxTxt.TabIndex = 26;
            this.AreaMaxTxt.Text = "20000";
            // 
            // AreaMinTxt
            // 
            this.AreaMinTxt.AutoSize = true;
            this.AreaMinTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AreaMinTxt.Location = new System.Drawing.Point(360, 208);
            this.AreaMinTxt.Name = "AreaMinTxt";
            this.AreaMinTxt.Size = new System.Drawing.Size(32, 16);
            this.AreaMinTxt.TabIndex = 27;
            this.AreaMinTxt.Text = "100";
            // 
            // MinGrayTxt
            // 
            this.MinGrayTxt.AutoSize = true;
            this.MinGrayTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinGrayTxt.Location = new System.Drawing.Point(363, 135);
            this.MinGrayTxt.Name = "MinGrayTxt";
            this.MinGrayTxt.Size = new System.Drawing.Size(24, 16);
            this.MinGrayTxt.TabIndex = 28;
            this.MinGrayTxt.Text = "50";
            // 
            // MaxGrayTxt
            // 
            this.MaxGrayTxt.AutoSize = true;
            this.MaxGrayTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaxGrayTxt.Location = new System.Drawing.Point(362, 172);
            this.MaxGrayTxt.Name = "MaxGrayTxt";
            this.MaxGrayTxt.Size = new System.Drawing.Size(32, 16);
            this.MaxGrayTxt.TabIndex = 28;
            this.MaxGrayTxt.Text = "200";
            // 
            // ThresholdLabel
            // 
            this.ThresholdLabel.AutoSize = true;
            this.ThresholdLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThresholdLabel.Location = new System.Drawing.Point(49, 240);
            this.ThresholdLabel.Name = "ThresholdLabel";
            this.ThresholdLabel.Size = new System.Drawing.Size(64, 16);
            this.ThresholdLabel.TabIndex = 29;
            this.ThresholdLabel.Text = "AreaMax";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(50, 205);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 16);
            this.label14.TabIndex = 30;
            this.label14.Text = "AreaMin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(50, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "MinGray";
            // 
            // Elements
            // 
            this.Elements.AutoSize = true;
            this.Elements.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Elements.Location = new System.Drawing.Point(50, 170);
            this.Elements.Name = "Elements";
            this.Elements.Size = new System.Drawing.Size(64, 16);
            this.Elements.TabIndex = 31;
            this.Elements.Text = "MaxGray";
            // 
            // SubFrmBlobLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 640);
            this.Controls.Add(this.panelOperator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubFrmBlobLocal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mark点示教";
            this.Load += new System.EventHandler(this.FrmSubFrmFindLine_Load);
            this.panelOperator.ResumeLayout(false);
            this.panelOperator.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PixelSizeNumUpDn)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AreaMaxBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AreaMinBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinGrayBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxGrayBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOperator;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ClearParaBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button FindLineBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox LineSelectComBox;
        private System.Windows.Forms.TrackBar AreaMaxBar;
        private System.Windows.Forms.TrackBar AreaMinBar;
        private System.Windows.Forms.TrackBar MaxGrayBar;
        private System.Windows.Forms.Label AreaMaxTxt;
        private System.Windows.Forms.Label AreaMinTxt;
        private System.Windows.Forms.Label MaxGrayTxt;
        private System.Windows.Forms.Label ThresholdLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label Elements;
        private System.Windows.Forms.Button StopTeachBtn;
        private System.Windows.Forms.Button SaveParaBtn;
        private System.Windows.Forms.Button TryDebugBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFindTime;
        private System.Windows.Forms.TrackBar MinGrayBar;
        private System.Windows.Forms.Label MinGrayTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveSerach;
        private System.Windows.Forms.TextBox txtSearchHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearchWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSearchY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearchX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button StartTeachBtn1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button PiexlSizeSaveBtn;
        private System.Windows.Forms.NumericUpDown PixelSizeNumUpDn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button PixelSizeTeachBtn;
    }
}