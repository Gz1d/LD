namespace VisionBase
{
    partial class SubFrmRecheck
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
            this.panelOperator = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnRecheckTest = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ThresholdBar = new System.Windows.Forms.TrackBar();
            this.DetectHeightBar = new System.Windows.Forms.TrackBar();
            this.SaveParaBtn = new System.Windows.Forms.Button();
            this.StopTeachBtn = new System.Windows.Forms.Button();
            this.StartTeachBtn1 = new System.Windows.Forms.Button();
            this.BarAddR = new System.Windows.Forms.TrackBar();
            this.ElementsBar = new System.Windows.Forms.TrackBar();
            this.ThresholdTxt = new System.Windows.Forms.Label();
            this.DetectHeightTxt = new System.Windows.Forms.Label();
            this.AddRtxt = new System.Windows.Forms.Label();
            this.ElementsTxt = new System.Windows.Forms.Label();
            this.ThresholdLabel = new System.Windows.Forms.Label();
            this.lbDetHeight = new System.Windows.Forms.Label();
            this.lbAddR = new System.Windows.Forms.Label();
            this.lbElements = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listViewItem = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnEdit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnItemTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbObjectSelect = new System.Windows.Forms.ComboBox();
            this.panelOperator.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetectHeightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarAddR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementsBar)).BeginInit();
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
            this.groupBox4.Controls.Add(this.richTextBox1);
            this.groupBox4.Controls.Add(this.btnRecheckTest);
            this.groupBox4.Location = new System.Drawing.Point(7, 534);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(481, 103);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "4、结果：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(60, 19);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(182, 53);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "NG";
            // 
            // btnRecheckTest
            // 
            this.btnRecheckTest.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRecheckTest.ForeColor = System.Drawing.Color.Blue;
            this.btnRecheckTest.Location = new System.Drawing.Point(341, 20);
            this.btnRecheckTest.Name = "btnRecheckTest";
            this.btnRecheckTest.Size = new System.Drawing.Size(107, 55);
            this.btnRecheckTest.TabIndex = 2;
            this.btnRecheckTest.Text = "复检测试";
            this.btnRecheckTest.UseVisualStyleBackColor = true;
            this.btnRecheckTest.Click += new System.EventHandler(this.btnRecheckTest_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ThresholdBar);
            this.groupBox3.Controls.Add(this.DetectHeightBar);
            this.groupBox3.Controls.Add(this.SaveParaBtn);
            this.groupBox3.Controls.Add(this.StopTeachBtn);
            this.groupBox3.Controls.Add(this.StartTeachBtn1);
            this.groupBox3.Controls.Add(this.BarAddR);
            this.groupBox3.Controls.Add(this.ElementsBar);
            this.groupBox3.Controls.Add(this.ThresholdTxt);
            this.groupBox3.Controls.Add(this.DetectHeightTxt);
            this.groupBox3.Controls.Add(this.AddRtxt);
            this.groupBox3.Controls.Add(this.ElementsTxt);
            this.groupBox3.Controls.Add(this.ThresholdLabel);
            this.groupBox3.Controls.Add(this.lbDetHeight);
            this.groupBox3.Controls.Add(this.lbAddR);
            this.groupBox3.Controls.Add(this.lbElements);
            this.groupBox3.Location = new System.Drawing.Point(7, 249);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 279);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3、对象参数：";
            // 
            // ThresholdBar
            // 
            this.ThresholdBar.AutoSize = false;
            this.ThresholdBar.Location = new System.Drawing.Point(175, 156);
            this.ThresholdBar.Maximum = 100;
            this.ThresholdBar.Name = "ThresholdBar";
            this.ThresholdBar.Size = new System.Drawing.Size(182, 23);
            this.ThresholdBar.TabIndex = 32;
            this.ThresholdBar.TickFrequency = 10;
            this.ThresholdBar.Value = 10;
            this.ThresholdBar.ValueChanged += new System.EventHandler(this.ThresholdBar_ValueChanged);
            // 
            // DetectHeightBar
            // 
            this.DetectHeightBar.AutoSize = false;
            this.DetectHeightBar.Location = new System.Drawing.Point(165, 115);
            this.DetectHeightBar.Maximum = 200;
            this.DetectHeightBar.Name = "DetectHeightBar";
            this.DetectHeightBar.Size = new System.Drawing.Size(182, 23);
            this.DetectHeightBar.TabIndex = 33;
            this.DetectHeightBar.TickFrequency = 20;
            this.DetectHeightBar.Value = 20;
            this.DetectHeightBar.ValueChanged += new System.EventHandler(this.DetectHeightBar_ValueChanged);
            // 
            // SaveParaBtn
            // 
            this.SaveParaBtn.Location = new System.Drawing.Point(354, 221);
            this.SaveParaBtn.Name = "SaveParaBtn";
            this.SaveParaBtn.Size = new System.Drawing.Size(94, 43);
            this.SaveParaBtn.TabIndex = 4;
            this.SaveParaBtn.Text = "保存参数";
            this.SaveParaBtn.UseVisualStyleBackColor = true;
            this.SaveParaBtn.Click += new System.EventHandler(this.SaveParaBtn_Click);
            // 
            // StopTeachBtn
            // 
            this.StopTeachBtn.Location = new System.Drawing.Point(190, 221);
            this.StopTeachBtn.Name = "StopTeachBtn";
            this.StopTeachBtn.Size = new System.Drawing.Size(94, 43);
            this.StopTeachBtn.TabIndex = 4;
            this.StopTeachBtn.Text = "停止示教";
            this.StopTeachBtn.UseVisualStyleBackColor = true;
            this.StopTeachBtn.Click += new System.EventHandler(this.StopTeachBtn_Click);
            // 
            // StartTeachBtn1
            // 
            this.StartTeachBtn1.Location = new System.Drawing.Point(35, 221);
            this.StartTeachBtn1.Name = "StartTeachBtn1";
            this.StartTeachBtn1.Size = new System.Drawing.Size(94, 43);
            this.StartTeachBtn1.TabIndex = 4;
            this.StartTeachBtn1.Text = "开始示教";
            this.StartTeachBtn1.UseVisualStyleBackColor = true;
            this.StartTeachBtn1.Click += new System.EventHandler(this.StartTeachBtn1_Click);
            // 
            // BarAddR
            // 
            this.BarAddR.AutoSize = false;
            this.BarAddR.Location = new System.Drawing.Point(168, 35);
            this.BarAddR.Maximum = 20;
            this.BarAddR.Minimum = -20;
            this.BarAddR.Name = "BarAddR";
            this.BarAddR.Size = new System.Drawing.Size(182, 23);
            this.BarAddR.TabIndex = 34;
            this.BarAddR.TickFrequency = 4;
            this.BarAddR.ValueChanged += new System.EventHandler(this.BarAddR_ValueChanged);
            // 
            // ElementsBar
            // 
            this.ElementsBar.AutoSize = false;
            this.ElementsBar.Location = new System.Drawing.Point(165, 74);
            this.ElementsBar.Maximum = 200;
            this.ElementsBar.Name = "ElementsBar";
            this.ElementsBar.Size = new System.Drawing.Size(182, 23);
            this.ElementsBar.TabIndex = 34;
            this.ElementsBar.TickFrequency = 20;
            this.ElementsBar.Value = 32;
            this.ElementsBar.ValueChanged += new System.EventHandler(this.ElementsBar_ValueChanged);
            // 
            // ThresholdTxt
            // 
            this.ThresholdTxt.AutoSize = true;
            this.ThresholdTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThresholdTxt.Location = new System.Drawing.Point(370, 163);
            this.ThresholdTxt.Name = "ThresholdTxt";
            this.ThresholdTxt.Size = new System.Drawing.Size(24, 16);
            this.ThresholdTxt.TabIndex = 26;
            this.ThresholdTxt.Text = "10";
            // 
            // DetectHeightTxt
            // 
            this.DetectHeightTxt.AutoSize = true;
            this.DetectHeightTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DetectHeightTxt.Location = new System.Drawing.Point(370, 115);
            this.DetectHeightTxt.Name = "DetectHeightTxt";
            this.DetectHeightTxt.Size = new System.Drawing.Size(24, 16);
            this.DetectHeightTxt.TabIndex = 27;
            this.DetectHeightTxt.Text = "20";
            // 
            // AddRtxt
            // 
            this.AddRtxt.AutoSize = true;
            this.AddRtxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddRtxt.Location = new System.Drawing.Point(373, 35);
            this.AddRtxt.Name = "AddRtxt";
            this.AddRtxt.Size = new System.Drawing.Size(16, 16);
            this.AddRtxt.TabIndex = 28;
            this.AddRtxt.Text = "0";
            // 
            // ElementsTxt
            // 
            this.ElementsTxt.AutoSize = true;
            this.ElementsTxt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ElementsTxt.Location = new System.Drawing.Point(370, 75);
            this.ElementsTxt.Name = "ElementsTxt";
            this.ElementsTxt.Size = new System.Drawing.Size(24, 16);
            this.ElementsTxt.TabIndex = 28;
            this.ElementsTxt.Text = "32";
            // 
            // ThresholdLabel
            // 
            this.ThresholdLabel.AutoSize = true;
            this.ThresholdLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThresholdLabel.Location = new System.Drawing.Point(42, 156);
            this.ThresholdLabel.Name = "ThresholdLabel";
            this.ThresholdLabel.Size = new System.Drawing.Size(96, 16);
            this.ThresholdLabel.TabIndex = 29;
            this.ThresholdLabel.Text = "DetectWidth";
            // 
            // lbDetHeight
            // 
            this.lbDetHeight.AutoSize = true;
            this.lbDetHeight.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDetHeight.Location = new System.Drawing.Point(42, 116);
            this.lbDetHeight.Name = "lbDetHeight";
            this.lbDetHeight.Size = new System.Drawing.Size(104, 16);
            this.lbDetHeight.TabIndex = 30;
            this.lbDetHeight.Text = "DetectHeight";
            // 
            // lbAddR
            // 
            this.lbAddR.AutoSize = true;
            this.lbAddR.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAddR.Location = new System.Drawing.Point(45, 35);
            this.lbAddR.Name = "lbAddR";
            this.lbAddR.Size = new System.Drawing.Size(40, 16);
            this.lbAddR.TabIndex = 31;
            this.lbAddR.Text = "AddR";
            // 
            // lbElements
            // 
            this.lbElements.AutoSize = true;
            this.lbElements.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbElements.Location = new System.Drawing.Point(42, 75);
            this.lbElements.Name = "lbElements";
            this.lbElements.Size = new System.Drawing.Size(72, 16);
            this.lbElements.TabIndex = 31;
            this.lbElements.Text = "Elements";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.listViewItem);
            this.groupBox2.Controls.Add(this.btnEdit);
            this.groupBox2.Location = new System.Drawing.Point(7, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(481, 168);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1、复检参数：";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(373, 96);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 47);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(234, 96);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(81, 47);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(234, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 50);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listViewItem
            // 
            this.listViewItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.listViewItem.FullRowSelect = true;
            this.listViewItem.GridLines = true;
            this.listViewItem.HideSelection = false;
            this.listViewItem.Location = new System.Drawing.Point(6, 20);
            this.listViewItem.MultiSelect = false;
            this.listViewItem.Name = "listViewItem";
            this.listViewItem.Size = new System.Drawing.Size(190, 139);
            this.listViewItem.SmallImageList = this.imageList1;
            this.listViewItem.TabIndex = 0;
            this.listViewItem.UseCompatibleStateImageBehavior = false;
            this.listViewItem.View = System.Windows.Forms.View.Details;
            this.listViewItem.SelectedIndexChanged += new System.EventHandler(this.listViewItem_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "序号";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "数量";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(35, 35);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Location = new System.Drawing.Point(372, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(81, 50);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnAutoCali_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnItemTest);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbObjectSelect);
            this.groupBox1.Location = new System.Drawing.Point(7, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 61);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "2、编辑项：";
            // 
            // btnItemTest
            // 
            this.btnItemTest.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnItemTest.ForeColor = System.Drawing.Color.Blue;
            this.btnItemTest.Location = new System.Drawing.Point(354, 13);
            this.btnItemTest.Name = "btnItemTest";
            this.btnItemTest.Size = new System.Drawing.Size(81, 40);
            this.btnItemTest.TabIndex = 3;
            this.btnItemTest.Text = "测试";
            this.btnItemTest.UseVisualStyleBackColor = true;
            this.btnItemTest.Click += new System.EventHandler(this.btnItemTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "名称：";
            // 
            // cmbObjectSelect
            // 
            this.cmbObjectSelect.FormattingEnabled = true;
            this.cmbObjectSelect.Location = new System.Drawing.Point(87, 23);
            this.cmbObjectSelect.Name = "cmbObjectSelect";
            this.cmbObjectSelect.Size = new System.Drawing.Size(100, 20);
            this.cmbObjectSelect.TabIndex = 0;
            this.cmbObjectSelect.SelectedIndexChanged += new System.EventHandler(this.cmbObjectSelect_SelectedIndexChanged);
            // 
            // SubFrmRecheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 640);
            this.Controls.Add(this.panelOperator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubFrmRecheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mark点示教";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubFrmRecheck_FormClosing);
            this.Load += new System.EventHandler(this.SubFrmRecheck_Load);
            this.panelOperator.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetectHeightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarAddR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementsBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOperator;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRecheckTest;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listViewItem;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cmbObjectSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar ThresholdBar;
        private System.Windows.Forms.TrackBar DetectHeightBar;
        private System.Windows.Forms.Button SaveParaBtn;
        private System.Windows.Forms.Button StopTeachBtn;
        private System.Windows.Forms.Button StartTeachBtn1;
        private System.Windows.Forms.TrackBar ElementsBar;
        private System.Windows.Forms.Label ThresholdTxt;
        private System.Windows.Forms.Label DetectHeightTxt;
        private System.Windows.Forms.Label ElementsTxt;
        private System.Windows.Forms.Label ThresholdLabel;
        private System.Windows.Forms.Label lbDetHeight;
        private System.Windows.Forms.Label lbElements;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnItemTest;
        private System.Windows.Forms.TrackBar BarAddR;
        private System.Windows.Forms.Label AddRtxt;
        private System.Windows.Forms.Label lbAddR;
    }
}