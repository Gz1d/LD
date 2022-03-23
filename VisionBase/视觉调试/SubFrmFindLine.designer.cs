namespace VisionBase
{
    partial class SubFrmFindLine
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
            this.CenterClipBar = new System.Windows.Forms.TrackBar();
            this.ClipCenterText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ThresholdBar = new System.Windows.Forms.TrackBar();
            this.DetectHeightBar = new System.Windows.Forms.TrackBar();
            this.SaveParaBtn = new System.Windows.Forms.Button();
            this.StopTeachBtn = new System.Windows.Forms.Button();
            this.StartTeachBtn1 = new System.Windows.Forms.Button();
            this.ElementsBar = new System.Windows.Forms.TrackBar();
            this.ThresholdTxt = new System.Windows.Forms.Label();
            this.DetectHeightTxt = new System.Windows.Forms.Label();
            this.ElementsTxt = new System.Windows.Forms.Label();
            this.ThresholdLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Elements = new System.Windows.Forms.Label();
            this.panelOperator.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CenterClipBar)).BeginInit();
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
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtFindTime);
            this.groupBox2.Controls.Add(this.ClearParaBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.LineSelectComBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 110);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1、ROI参数设置：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "ms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "查找时间：";
            // 
            // txtFindTime
            // 
            this.txtFindTime.Location = new System.Drawing.Point(107, 65);
            this.txtFindTime.Name = "txtFindTime";
            this.txtFindTime.Size = new System.Drawing.Size(121, 21);
            this.txtFindTime.TabIndex = 23;
            // 
            // ClearParaBtn
            // 
            this.ClearParaBtn.Location = new System.Drawing.Point(351, 20);
            this.ClearParaBtn.Name = "ClearParaBtn";
            this.ClearParaBtn.Size = new System.Drawing.Size(88, 55);
            this.ClearParaBtn.TabIndex = 16;
            this.ClearParaBtn.Text = "参数清除";
            this.ClearParaBtn.UseVisualStyleBackColor = true;
            this.ClearParaBtn.Click += new System.EventHandler(this.ClearParaBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 34);
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
            this.LineSelectComBox.Location = new System.Drawing.Point(106, 26);
            this.LineSelectComBox.Name = "LineSelectComBox";
            this.LineSelectComBox.Size = new System.Drawing.Size(148, 20);
            this.LineSelectComBox.TabIndex = 11;
            this.LineSelectComBox.SelectedIndexChanged += new System.EventHandler(this.LineSelectComBox_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TryDebugBtn);
            this.groupBox4.Controls.Add(this.FindLineBtn);
            this.groupBox4.Location = new System.Drawing.Point(12, 492);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(461, 120);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "3、测试查找结果：";
            // 
            // TryDebugBtn
            // 
            this.TryDebugBtn.Location = new System.Drawing.Point(170, 32);
            this.TryDebugBtn.Name = "TryDebugBtn";
            this.TryDebugBtn.Size = new System.Drawing.Size(86, 56);
            this.TryDebugBtn.TabIndex = 4;
            this.TryDebugBtn.Text = "测试";
            this.TryDebugBtn.UseVisualStyleBackColor = true;
            this.TryDebugBtn.Click += new System.EventHandler(this.TryDebugBtn_Click);
            // 
            // FindLineBtn
            // 
            this.FindLineBtn.Location = new System.Drawing.Point(26, 33);
            this.FindLineBtn.Name = "FindLineBtn";
            this.FindLineBtn.Size = new System.Drawing.Size(84, 56);
            this.FindLineBtn.TabIndex = 4;
            this.FindLineBtn.Text = "查找";
            this.FindLineBtn.UseVisualStyleBackColor = true;
            this.FindLineBtn.Click += new System.EventHandler(this.FindLineBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CenterClipBar);
            this.groupBox3.Controls.Add(this.ClipCenterText);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.ThresholdBar);
            this.groupBox3.Controls.Add(this.DetectHeightBar);
            this.groupBox3.Controls.Add(this.SaveParaBtn);
            this.groupBox3.Controls.Add(this.StopTeachBtn);
            this.groupBox3.Controls.Add(this.StartTeachBtn1);
            this.groupBox3.Controls.Add(this.ElementsBar);
            this.groupBox3.Controls.Add(this.ThresholdTxt);
            this.groupBox3.Controls.Add(this.DetectHeightTxt);
            this.groupBox3.Controls.Add(this.ElementsTxt);
            this.groupBox3.Controls.Add(this.ThresholdLabel);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.Elements);
            this.groupBox3.Location = new System.Drawing.Point(12, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(461, 314);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2、边沿参数：";
            // 
            // CenterClipBar
            // 
            this.CenterClipBar.AutoSize = false;
            this.CenterClipBar.Location = new System.Drawing.Point(164, 181);
            this.CenterClipBar.Maximum = 90;
            this.CenterClipBar.Name = "CenterClipBar";
            this.CenterClipBar.Size = new System.Drawing.Size(182, 23);
            this.CenterClipBar.TabIndex = 37;
            this.CenterClipBar.TickFrequency = 10;
            this.CenterClipBar.Scroll += new System.EventHandler(this.CenterClipBar_Scroll);
            // 
            // ClipCenterText
            // 
            this.ClipCenterText.AutoSize = true;
            this.ClipCenterText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClipCenterText.Location = new System.Drawing.Point(369, 186);
            this.ClipCenterText.Name = "ClipCenterText";
            this.ClipCenterText.Size = new System.Drawing.Size(32, 16);
            this.ClipCenterText.TabIndex = 35;
            this.ClipCenterText.Text = "0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(51, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "CenterClip";
            // 
            // ThresholdBar
            // 
            this.ThresholdBar.AutoSize = false;
            this.ThresholdBar.Location = new System.Drawing.Point(165, 129);
            this.ThresholdBar.Maximum = 100;
            this.ThresholdBar.Name = "ThresholdBar";
            this.ThresholdBar.Size = new System.Drawing.Size(182, 23);
            this.ThresholdBar.TabIndex = 32;
            this.ThresholdBar.TickFrequency = 10;
            this.ThresholdBar.Value = 10;
            this.ThresholdBar.Scroll += new System.EventHandler(this.ThresholdBar_Scroll);
            // 
            // DetectHeightBar
            // 
            this.DetectHeightBar.AutoSize = false;
            this.DetectHeightBar.Location = new System.Drawing.Point(165, 77);
            this.DetectHeightBar.Maximum = 1000;
            this.DetectHeightBar.Name = "DetectHeightBar";
            this.DetectHeightBar.Size = new System.Drawing.Size(182, 23);
            this.DetectHeightBar.TabIndex = 33;
            this.DetectHeightBar.TickFrequency = 100;
            this.DetectHeightBar.Value = 20;
            this.DetectHeightBar.Scroll += new System.EventHandler(this.DetectHeightBar_Scroll);
            // 
            // SaveParaBtn
            // 
            this.SaveParaBtn.Location = new System.Drawing.Point(345, 244);
            this.SaveParaBtn.Name = "SaveParaBtn";
            this.SaveParaBtn.Size = new System.Drawing.Size(94, 56);
            this.SaveParaBtn.TabIndex = 4;
            this.SaveParaBtn.Text = "保存参数";
            this.SaveParaBtn.UseVisualStyleBackColor = true;
            this.SaveParaBtn.Click += new System.EventHandler(this.SaveParaBtn_Click);
            // 
            // StopTeachBtn
            // 
            this.StopTeachBtn.Location = new System.Drawing.Point(184, 245);
            this.StopTeachBtn.Name = "StopTeachBtn";
            this.StopTeachBtn.Size = new System.Drawing.Size(94, 56);
            this.StopTeachBtn.TabIndex = 4;
            this.StopTeachBtn.Text = "停止示教";
            this.StopTeachBtn.UseVisualStyleBackColor = true;
            this.StopTeachBtn.Click += new System.EventHandler(this.StopTeachBtn_Click);
            // 
            // StartTeachBtn1
            // 
            this.StartTeachBtn1.Location = new System.Drawing.Point(26, 244);
            this.StartTeachBtn1.Name = "StartTeachBtn1";
            this.StartTeachBtn1.Size = new System.Drawing.Size(94, 56);
            this.StartTeachBtn1.TabIndex = 4;
            this.StartTeachBtn1.Text = "开始示教";
            this.StartTeachBtn1.UseVisualStyleBackColor = true;
            this.StartTeachBtn1.Click += new System.EventHandler(this.StartTeachBtn1_Click);
            // 
            // ElementsBar
            // 
            this.ElementsBar.AutoSize = false;
            this.ElementsBar.Location = new System.Drawing.Point(165, 32);
            this.ElementsBar.Maximum = 200;
            this.ElementsBar.Name = "ElementsBar";
            this.ElementsBar.Size = new System.Drawing.Size(182, 23);
            this.ElementsBar.TabIndex = 34;
            this.ElementsBar.TickFrequency = 20;
            this.ElementsBar.Value = 32;
            this.ElementsBar.Scroll += new System.EventHandler(this.ElementsBar_Scroll);
            // 
            // ThresholdTxt
            // 
            this.ThresholdTxt.AutoSize = true;
            this.ThresholdTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThresholdTxt.Location = new System.Drawing.Point(370, 134);
            this.ThresholdTxt.Name = "ThresholdTxt";
            this.ThresholdTxt.Size = new System.Drawing.Size(24, 16);
            this.ThresholdTxt.TabIndex = 26;
            this.ThresholdTxt.Text = "10";
            // 
            // DetectHeightTxt
            // 
            this.DetectHeightTxt.AutoSize = true;
            this.DetectHeightTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DetectHeightTxt.Location = new System.Drawing.Point(370, 77);
            this.DetectHeightTxt.Name = "DetectHeightTxt";
            this.DetectHeightTxt.Size = new System.Drawing.Size(24, 16);
            this.DetectHeightTxt.TabIndex = 27;
            this.DetectHeightTxt.Text = "20";
            // 
            // ElementsTxt
            // 
            this.ElementsTxt.AutoSize = true;
            this.ElementsTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ElementsTxt.Location = new System.Drawing.Point(369, 32);
            this.ElementsTxt.Name = "ElementsTxt";
            this.ElementsTxt.Size = new System.Drawing.Size(24, 16);
            this.ElementsTxt.TabIndex = 28;
            this.ElementsTxt.Text = "32";
            // 
            // ThresholdLabel
            // 
            this.ThresholdLabel.AutoSize = true;
            this.ThresholdLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThresholdLabel.Location = new System.Drawing.Point(52, 131);
            this.ThresholdLabel.Name = "ThresholdLabel";
            this.ThresholdLabel.Size = new System.Drawing.Size(80, 16);
            this.ThresholdLabel.TabIndex = 29;
            this.ThresholdLabel.Text = "Threshold";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(42, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 16);
            this.label14.TabIndex = 30;
            this.label14.Text = "DetectHeight";
            // 
            // Elements
            // 
            this.Elements.AutoSize = true;
            this.Elements.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Elements.Location = new System.Drawing.Point(57, 33);
            this.Elements.Name = "Elements";
            this.Elements.Size = new System.Drawing.Size(72, 16);
            this.Elements.TabIndex = 31;
            this.Elements.Text = "Elements";
            // 
            // SubFrmFindLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 640);
            this.Controls.Add(this.panelOperator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubFrmFindLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mark点示教";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SubFrmFindLine_FormClosed);
            this.Load += new System.EventHandler(this.FrmSubFrmFindLine_Load);
            this.panelOperator.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CenterClipBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetectHeightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementsBar)).EndInit();
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
        private System.Windows.Forms.TrackBar ThresholdBar;
        private System.Windows.Forms.TrackBar DetectHeightBar;
        private System.Windows.Forms.TrackBar ElementsBar;
        private System.Windows.Forms.Label ThresholdTxt;
        private System.Windows.Forms.Label DetectHeightTxt;
        private System.Windows.Forms.Label ElementsTxt;
        private System.Windows.Forms.Label ThresholdLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label Elements;
        private System.Windows.Forms.Button StartTeachBtn1;
        private System.Windows.Forms.Button StopTeachBtn;
        private System.Windows.Forms.Button SaveParaBtn;
        private System.Windows.Forms.Button TryDebugBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFindTime;
        private System.Windows.Forms.TrackBar CenterClipBar;
        private System.Windows.Forms.Label ClipCenterText;
        private System.Windows.Forms.Label label3;
    }
}