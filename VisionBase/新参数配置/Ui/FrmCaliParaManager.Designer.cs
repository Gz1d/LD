
namespace VisionBase
{
    partial class FrmCaliParaManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ParaTeachBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.isMoveXDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isMoveYDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.caliModelDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.coordiDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.camDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.coordiCamDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.describeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isMoveXDataGridViewCheckBoxColumn,
            this.isMoveYDataGridViewCheckBoxColumn,
            this.caliModelDataGridViewComboBoxColumn,
            this.coordiDataGridViewComboBoxColumn,
            this.camDataGridViewComboBoxColumn,
            this.coordiCamDataGridViewComboBoxColumn,
            this.describeDataGridViewTextBoxColumn,
            this.ParaTeachBtn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(942, 450);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(194, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 48);
            this.button1.TabIndex = 1;
            this.button1.Text = "示教";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(665, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 48);
            this.button2.TabIndex = 1;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ParaTeachBtn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "示教";
            this.ParaTeachBtn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ParaTeachBtn.HeaderText = "Teach";
            this.ParaTeachBtn.Name = "ParaTeachBtn";
            this.ParaTeachBtn.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(429, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "手眼标定";
            // 
            // isMoveXDataGridViewCheckBoxColumn
            // 
            this.isMoveXDataGridViewCheckBoxColumn.DataPropertyName = "IsMoveX";
            this.isMoveXDataGridViewCheckBoxColumn.HeaderText = "IsMoveX";
            this.isMoveXDataGridViewCheckBoxColumn.Name = "isMoveXDataGridViewCheckBoxColumn";
            // 
            // isMoveYDataGridViewCheckBoxColumn
            // 
            this.isMoveYDataGridViewCheckBoxColumn.DataPropertyName = "IsMoveY";
            this.isMoveYDataGridViewCheckBoxColumn.HeaderText = "IsMoveY";
            this.isMoveYDataGridViewCheckBoxColumn.Name = "isMoveYDataGridViewCheckBoxColumn";
            // 
            // caliModelDataGridViewComboBoxColumn
            // 
            this.caliModelDataGridViewComboBoxColumn.DataPropertyName = "caliModel";
            this.caliModelDataGridViewComboBoxColumn.HeaderText = "caliModel";
            this.caliModelDataGridViewComboBoxColumn.Name = "caliModelDataGridViewComboBoxColumn";
            this.caliModelDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.caliModelDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // coordiDataGridViewComboBoxColumn
            // 
            this.coordiDataGridViewComboBoxColumn.DataPropertyName = "coordi";
            this.coordiDataGridViewComboBoxColumn.HeaderText = "coordi";
            this.coordiDataGridViewComboBoxColumn.Name = "coordiDataGridViewComboBoxColumn";
            this.coordiDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.coordiDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // camDataGridViewComboBoxColumn
            // 
            this.camDataGridViewComboBoxColumn.DataPropertyName = "cam";
            this.camDataGridViewComboBoxColumn.HeaderText = "cam";
            this.camDataGridViewComboBoxColumn.Name = "camDataGridViewComboBoxColumn";
            this.camDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.camDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // coordiCamDataGridViewComboBoxColumn
            // 
            this.coordiCamDataGridViewComboBoxColumn.DataPropertyName = "CoordiCam";
            this.coordiCamDataGridViewComboBoxColumn.HeaderText = "CoordiCam";
            this.coordiCamDataGridViewComboBoxColumn.Name = "coordiCamDataGridViewComboBoxColumn";
            this.coordiCamDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.coordiCamDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // describeDataGridViewTextBoxColumn
            // 
            this.describeDataGridViewTextBoxColumn.DataPropertyName = "describe";
            this.describeDataGridViewTextBoxColumn.HeaderText = "describe";
            this.describeDataGridViewTextBoxColumn.Name = "describeDataGridViewTextBoxColumn";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(VisionBase.CaliParam);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.58823F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.41177F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(948, 585);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 513);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(942, 69);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // FrmCaliParaManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 585);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmCaliParaManager";
            this.TabText = "FrmCaliParaManager";
            this.Text = "FrmCaliParaManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCaliParaManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmCaliParaManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMoveXDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMoveYDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn caliModelDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn coordiDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn camDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn coordiCamDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn describeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ParaTeachBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}