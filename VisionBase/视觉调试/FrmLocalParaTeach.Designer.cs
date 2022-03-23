namespace VisionBase
{
    partial class FrmLocalParaTeach 
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelOperator2 = new System.Windows.Forms.Panel();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnLoadLocalImage = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panelOperator = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMotorDebug = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLastStep = new System.Windows.Forms.Button();
            this.btnNextStep = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelOperator2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.btnMotorDebug);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.btnLastStep);
            this.splitContainer1.Panel2.Controls.Add(this.btnNextStep);
            this.splitContainer1.Size = new System.Drawing.Size(1207, 771);
            this.splitContainer1.SplitterDistance = 677;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.CausesValidation = false;
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            this.splitContainer2.Panel1.Controls.Add(this.panelOperator2);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelOperator);
            this.splitContainer2.Size = new System.Drawing.Size(1207, 677);
            this.splitContainer2.SplitterDistance = 690;
            this.splitContainer2.TabIndex = 0;
            // 
            // panelOperator2
            // 
            this.panelOperator2.Controls.Add(this.btnSaveImage);
            this.panelOperator2.Controls.Add(this.btnLoadLocalImage);
            this.panelOperator2.Location = new System.Drawing.Point(3, 597);
            this.panelOperator2.Name = "panelOperator2";
            this.panelOperator2.Size = new System.Drawing.Size(674, 77);
            this.panelOperator2.TabIndex = 0;
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(467, 12);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(105, 53);
            this.btnSaveImage.TabIndex = 18;
            this.btnSaveImage.Text = "保存图片";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnLoadLocalImage
            // 
            this.btnLoadLocalImage.Location = new System.Drawing.Point(22, 12);
            this.btnLoadLocalImage.Name = "btnLoadLocalImage";
            this.btnLoadLocalImage.Size = new System.Drawing.Size(105, 53);
            this.btnLoadLocalImage.TabIndex = 15;
            this.btnLoadLocalImage.Text = "加载图片";
            this.btnLoadLocalImage.UseVisualStyleBackColor = true;
            this.btnLoadLocalImage.Click += new System.EventHandler(this.btnLoadLocalImage_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(683, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(5, 677);
            this.label6.TabIndex = 16;
            // 
            // panelOperator
            // 
            this.panelOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOperator.Location = new System.Drawing.Point(0, 0);
            this.panelOperator.Name = "panelOperator";
            this.panelOperator.Size = new System.Drawing.Size(513, 677);
            this.panelOperator.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(346, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 62);
            this.button1.TabIndex = 16;
            this.button1.Text = "释放窗口";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btnMotorDebug
            // 
            this.btnMotorDebug.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMotorDebug.ForeColor = System.Drawing.Color.Blue;
            this.btnMotorDebug.Location = new System.Drawing.Point(627, 16);
            this.btnMotorDebug.Name = "btnMotorDebug";
            this.btnMotorDebug.Size = new System.Drawing.Size(105, 62);
            this.btnMotorDebug.TabIndex = 16;
            this.btnMotorDebug.Text = "位置示教";
            this.btnMotorDebug.UseVisualStyleBackColor = true;
            this.btnMotorDebug.Click += new System.EventHandler(this.btnMotorDebug_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1204, 5);
            this.label1.TabIndex = 2;
            // 
            // btnLastStep
            // 
            this.btnLastStep.Location = new System.Drawing.Point(91, 16);
            this.btnLastStep.Name = "btnLastStep";
            this.btnLastStep.Size = new System.Drawing.Size(105, 62);
            this.btnLastStep.TabIndex = 1;
            this.btnLastStep.Text = "上一步";
            this.btnLastStep.UseVisualStyleBackColor = true;
            this.btnLastStep.Click += new System.EventHandler(this.btnLastStep_Click);
            // 
            // btnNextStep
            // 
            this.btnNextStep.Location = new System.Drawing.Point(957, 16);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(105, 62);
            this.btnNextStep.TabIndex = 0;
            this.btnNextStep.Text = "下一步";
            this.btnNextStep.UseVisualStyleBackColor = true;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 588);
            this.panel1.TabIndex = 17;
            // 
            // FrmLocalParaTeach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 771);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLocalParaTeach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mark点示教";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImageParaTeach_FormClosing);
            this.Load += new System.EventHandler(this.FrmImageParaTeach_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelOperator2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnLastStep;
        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadLocalImage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnMotorDebug;
        private System.Windows.Forms.Panel panelOperator2;
        private System.Windows.Forms.Panel panelOperator;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}