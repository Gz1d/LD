namespace VisionBase
{
    partial class SubFrmFindCircle
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SelectCircleBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CircleSelectComBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.GenVirtualBtn = new System.Windows.Forms.Button();
            this.TryDebugBtn = new System.Windows.Forms.Button();
            this.txtFindTime = new System.Windows.Forms.TextBox();
            this.FindCircleBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SaveParaBtn = new System.Windows.Forms.Button();
            this.StopTeachBtn = new System.Windows.Forms.Button();
            this.StartTeachBtn1 = new System.Windows.Forms.Button();
            this.ThresholdBar = new System.Windows.Forms.TrackBar();
            this.DetectHeightBar = new System.Windows.Forms.TrackBar();
            this.ElementsBar = new System.Windows.Forms.TrackBar();
            this.ThresholdTxt = new System.Windows.Forms.Label();
            this.DetectHeightTxt = new System.Windows.Forms.Label();
            this.ElementsTxt = new System.Windows.Forms.Label();
            this.ThresholdLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Elements = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DirectCbx = new System.Windows.Forms.ComboBox();
            this.panelOperator.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetectHeightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementsBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOperator
            // 
            this.panelOperator.Controls.Add(this.groupBox2);
            this.panelOperator.Controls.Add(this.groupBox4);
            this.panelOperator.Controls.Add(this.groupBox3);
            this.panelOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOperator.Location = new System.Drawing.Point(0, 0);
            this.panelOperator.Name = "panelOperator";
            this.panelOperator.Size = new System.Drawing.Size(500, 640);
            this.panelOperator.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SelectCircleBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CircleSelectComBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 91);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1、ROI参数设置：";
            // 
            // SelectCircleBtn
            // 
            this.SelectCircleBtn.Location = new System.Drawing.Point(349, 25);
            this.SelectCircleBtn.Name = "SelectCircleBtn";
            this.SelectCircleBtn.Size = new System.Drawing.Size(82, 48);
            this.SelectCircleBtn.TabIndex = 16;
            this.SelectCircleBtn.Text = "清除圆参数";
            this.SelectCircleBtn.UseVisualStyleBackColor = true;
            this.SelectCircleBtn.Click += new System.EventHandler(this.SelectCircleBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "编号：";
            // 
            // CircleSelectComBox
            // 
            this.CircleSelectComBox.FormattingEnabled = true;
            this.CircleSelectComBox.Items.AddRange(new object[] {
            "Circle1",
            "Circle2"});
            this.CircleSelectComBox.Location = new System.Drawing.Point(93, 39);
            this.CircleSelectComBox.Name = "CircleSelectComBox";
            this.CircleSelectComBox.Size = new System.Drawing.Size(91, 20);
            this.CircleSelectComBox.TabIndex = 11;
            this.CircleSelectComBox.Text = "Circle1";
            this.CircleSelectComBox.SelectedIndexChanged += new System.EventHandler(this.CircleSelectComBox_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.GenVirtualBtn);
            this.groupBox4.Controls.Add(this.TryDebugBtn);
            this.groupBox4.Controls.Add(this.txtFindTime);
            this.groupBox4.Controls.Add(this.FindCircleBtn);
            this.groupBox4.Location = new System.Drawing.Point(12, 469);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(461, 123);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "3、测试查找结果：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(163, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "ms";
            // 
            // GenVirtualBtn
            // 
            this.GenVirtualBtn.Location = new System.Drawing.Point(337, 34);
            this.GenVirtualBtn.Name = "GenVirtualBtn";
            this.GenVirtualBtn.Size = new System.Drawing.Size(94, 56);
            this.GenVirtualBtn.TabIndex = 4;
            this.GenVirtualBtn.Text = "生成虚拟圆心";
            this.GenVirtualBtn.UseVisualStyleBackColor = true;
            this.GenVirtualBtn.Click += new System.EventHandler(this.GenVirtualBtn_Click);
            // 
            // TryDebugBtn
            // 
            this.TryDebugBtn.Location = new System.Drawing.Point(186, 34);
            this.TryDebugBtn.Name = "TryDebugBtn";
            this.TryDebugBtn.Size = new System.Drawing.Size(94, 56);
            this.TryDebugBtn.TabIndex = 4;
            this.TryDebugBtn.Text = "测试";
            this.TryDebugBtn.UseVisualStyleBackColor = true;
            this.TryDebugBtn.Click += new System.EventHandler(this.TryDebugBtn_Click);
            // 
            // txtFindTime
            // 
            this.txtFindTime.Location = new System.Drawing.Point(110, 53);
            this.txtFindTime.Name = "txtFindTime";
            this.txtFindTime.ReadOnly = true;
            this.txtFindTime.Size = new System.Drawing.Size(47, 21);
            this.txtFindTime.TabIndex = 17;
            // 
            // FindCircleBtn
            // 
            this.FindCircleBtn.Location = new System.Drawing.Point(16, 34);
            this.FindCircleBtn.Name = "FindCircleBtn";
            this.FindCircleBtn.Size = new System.Drawing.Size(88, 56);
            this.FindCircleBtn.TabIndex = 4;
            this.FindCircleBtn.Text = "查找";
            this.FindCircleBtn.UseVisualStyleBackColor = true;
            this.FindCircleBtn.Click += new System.EventHandler(this.FindCircleBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DirectCbx);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.SaveParaBtn);
            this.groupBox3.Controls.Add(this.StopTeachBtn);
            this.groupBox3.Controls.Add(this.StartTeachBtn1);
            this.groupBox3.Controls.Add(this.ThresholdBar);
            this.groupBox3.Controls.Add(this.DetectHeightBar);
            this.groupBox3.Controls.Add(this.ElementsBar);
            this.groupBox3.Controls.Add(this.ThresholdTxt);
            this.groupBox3.Controls.Add(this.DetectHeightTxt);
            this.groupBox3.Controls.Add(this.ElementsTxt);
            this.groupBox3.Controls.Add(this.ThresholdLabel);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.Elements);
            this.groupBox3.Location = new System.Drawing.Point(12, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(461, 316);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2、边沿参数：";
            // 
            // SaveParaBtn
            // 
            this.SaveParaBtn.Location = new System.Drawing.Point(343, 234);
            this.SaveParaBtn.Name = "SaveParaBtn";
            this.SaveParaBtn.Size = new System.Drawing.Size(94, 56);
            this.SaveParaBtn.TabIndex = 44;
            this.SaveParaBtn.Text = "保存参数";
            this.SaveParaBtn.UseVisualStyleBackColor = true;
            this.SaveParaBtn.Click += new System.EventHandler(this.SaveParaBtn_Click);
            // 
            // StopTeachBtn
            // 
            this.StopTeachBtn.Location = new System.Drawing.Point(189, 234);
            this.StopTeachBtn.Name = "StopTeachBtn";
            this.StopTeachBtn.Size = new System.Drawing.Size(94, 56);
            this.StopTeachBtn.TabIndex = 45;
            this.StopTeachBtn.Text = "停止示教";
            this.StopTeachBtn.UseVisualStyleBackColor = true;
            this.StopTeachBtn.Click += new System.EventHandler(this.StopTeachBtn_Click);
            // 
            // StartTeachBtn1
            // 
            this.StartTeachBtn1.Location = new System.Drawing.Point(31, 234);
            this.StartTeachBtn1.Name = "StartTeachBtn1";
            this.StartTeachBtn1.Size = new System.Drawing.Size(94, 56);
            this.StartTeachBtn1.TabIndex = 46;
            this.StartTeachBtn1.Text = "开始示教";
            this.StartTeachBtn1.UseVisualStyleBackColor = true;
            this.StartTeachBtn1.Click += new System.EventHandler(this.StartTeachBtn1_Click);
            // 
            // ThresholdBar
            // 
            this.ThresholdBar.AutoSize = false;
            this.ThresholdBar.Location = new System.Drawing.Point(165, 170);
            this.ThresholdBar.Maximum = 100;
            this.ThresholdBar.Name = "ThresholdBar";
            this.ThresholdBar.Size = new System.Drawing.Size(182, 23);
            this.ThresholdBar.TabIndex = 41;
            this.ThresholdBar.TickFrequency = 10;
            this.ThresholdBar.Value = 10;
            this.ThresholdBar.Scroll += new System.EventHandler(this.ThresholdBar_Scroll);
            // 
            // DetectHeightBar
            // 
            this.DetectHeightBar.AutoSize = false;
            this.DetectHeightBar.Location = new System.Drawing.Point(165, 125);
            this.DetectHeightBar.Maximum = 200;
            this.DetectHeightBar.Name = "DetectHeightBar";
            this.DetectHeightBar.Size = new System.Drawing.Size(182, 23);
            this.DetectHeightBar.TabIndex = 42;
            this.DetectHeightBar.TickFrequency = 20;
            this.DetectHeightBar.Value = 20;
            this.DetectHeightBar.Scroll += new System.EventHandler(this.DetectHeightBar_Scroll);
            // 
            // ElementsBar
            // 
            this.ElementsBar.AutoSize = false;
            this.ElementsBar.Location = new System.Drawing.Point(165, 78);
            this.ElementsBar.Maximum = 200;
            this.ElementsBar.Name = "ElementsBar";
            this.ElementsBar.Size = new System.Drawing.Size(182, 23);
            this.ElementsBar.TabIndex = 43;
            this.ElementsBar.TickFrequency = 20;
            this.ElementsBar.Value = 32;
            this.ElementsBar.Scroll += new System.EventHandler(this.ElementsBar_Scroll);
            // 
            // ThresholdTxt
            // 
            this.ThresholdTxt.AutoSize = true;
            this.ThresholdTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThresholdTxt.Location = new System.Drawing.Point(370, 172);
            this.ThresholdTxt.Name = "ThresholdTxt";
            this.ThresholdTxt.Size = new System.Drawing.Size(24, 16);
            this.ThresholdTxt.TabIndex = 35;
            this.ThresholdTxt.Text = "10";
            // 
            // DetectHeightTxt
            // 
            this.DetectHeightTxt.AutoSize = true;
            this.DetectHeightTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DetectHeightTxt.Location = new System.Drawing.Point(370, 130);
            this.DetectHeightTxt.Name = "DetectHeightTxt";
            this.DetectHeightTxt.Size = new System.Drawing.Size(24, 16);
            this.DetectHeightTxt.TabIndex = 36;
            this.DetectHeightTxt.Text = "20";
            // 
            // ElementsTxt
            // 
            this.ElementsTxt.AutoSize = true;
            this.ElementsTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ElementsTxt.Location = new System.Drawing.Point(369, 78);
            this.ElementsTxt.Name = "ElementsTxt";
            this.ElementsTxt.Size = new System.Drawing.Size(24, 16);
            this.ElementsTxt.TabIndex = 37;
            this.ElementsTxt.Text = "32";
            // 
            // ThresholdLabel
            // 
            this.ThresholdLabel.AutoSize = true;
            this.ThresholdLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThresholdLabel.Location = new System.Drawing.Point(62, 171);
            this.ThresholdLabel.Name = "ThresholdLabel";
            this.ThresholdLabel.Size = new System.Drawing.Size(80, 16);
            this.ThresholdLabel.TabIndex = 38;
            this.ThresholdLabel.Text = "Threshold";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(52, 125);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 16);
            this.label14.TabIndex = 39;
            this.label14.Text = "DetectHeight";
            // 
            // Elements
            // 
            this.Elements.AutoSize = true;
            this.Elements.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Elements.Location = new System.Drawing.Point(67, 79);
            this.Elements.Name = "Elements";
            this.Elements.Size = new System.Drawing.Size(72, 16);
            this.Elements.TabIndex = 40;
            this.Elements.Text = "Elements";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(79, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 47;
            this.label1.Text = "Direct";
            // 
            // DirectCbx
            // 
            this.DirectCbx.FormattingEnabled = true;
            this.DirectCbx.Items.AddRange(new object[] {
            "inner",
            "outer"});
            this.DirectCbx.Location = new System.Drawing.Point(222, 39);
            this.DirectCbx.Name = "DirectCbx";
            this.DirectCbx.Size = new System.Drawing.Size(87, 20);
            this.DirectCbx.TabIndex = 48;
            this.DirectCbx.Text = "inner";
            this.DirectCbx.SelectedIndexChanged += new System.EventHandler(this.DirectCbx_SelectedIndexChanged);
            // 
            // SubFrmFindCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 640);
            this.Controls.Add(this.panelOperator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubFrmFindCircle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mark点示教";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SubFrmFindCircle_FormClosed);
            this.Load += new System.EventHandler(this.FrmSubFrmFindLine_Load);
            this.panelOperator.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetectHeightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementsBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOperator;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button SelectCircleBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button FindCircleBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox CircleSelectComBox;
        private System.Windows.Forms.TrackBar ThresholdBar;
        private System.Windows.Forms.TrackBar DetectHeightBar;
        private System.Windows.Forms.TrackBar ElementsBar;
        private System.Windows.Forms.Label ThresholdTxt;
        private System.Windows.Forms.Label DetectHeightTxt;
        private System.Windows.Forms.Label ElementsTxt;
        private System.Windows.Forms.Label ThresholdLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label Elements;
        private System.Windows.Forms.Button SaveParaBtn;
        private System.Windows.Forms.Button StopTeachBtn;
        private System.Windows.Forms.Button StartTeachBtn1;
        private System.Windows.Forms.Button TryDebugBtn;
        private System.Windows.Forms.TextBox txtFindTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button GenVirtualBtn;
        private System.Windows.Forms.ComboBox DirectCbx;
        private System.Windows.Forms.Label label1;
    }
}