
namespace VisionBase
{
    partial class FrmLightCrlManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLightCrlManager));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SaveBtn = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.baudRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stopBitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.parityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataBitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isConnectDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.describeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isHexDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lightCtrlTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lightCtrlNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.portNameDataGridViewTextBoxColumn,
            this.baudRateDataGridViewTextBoxColumn,
            this.stopBitsDataGridViewTextBoxColumn,
            this.parityDataGridViewTextBoxColumn,
            this.dataBitsDataGridViewTextBoxColumn,
            this.isConnectDataGridViewCheckBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn,
            this.describeDataGridViewTextBoxColumn,
            this.isHexDataGridViewCheckBoxColumn,
            this.lightCtrlTypeDataGridViewTextBoxColumn,
            this.lightCtrlNameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(31, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1148, 542);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "black.png");
            this.imageList1.Images.SetKeyName(1, "red.png");
            this.imageList1.Images.SetKeyName(2, "green.png");
            this.imageList1.Images.SetKeyName(3, "white.png");
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(1209, 266);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(102, 57);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "保存";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn1.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // portNameDataGridViewTextBoxColumn
            // 
            this.portNameDataGridViewTextBoxColumn.DataPropertyName = "PortName";
            this.portNameDataGridViewTextBoxColumn.HeaderText = "PortName";
            this.portNameDataGridViewTextBoxColumn.Name = "portNameDataGridViewTextBoxColumn";
            this.portNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.portNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // baudRateDataGridViewTextBoxColumn
            // 
            this.baudRateDataGridViewTextBoxColumn.DataPropertyName = "BaudRate";
            this.baudRateDataGridViewTextBoxColumn.HeaderText = "BaudRate";
            this.baudRateDataGridViewTextBoxColumn.Name = "baudRateDataGridViewTextBoxColumn";
            // 
            // stopBitsDataGridViewTextBoxColumn
            // 
            this.stopBitsDataGridViewTextBoxColumn.DataPropertyName = "StopBits";
            this.stopBitsDataGridViewTextBoxColumn.HeaderText = "StopBits";
            this.stopBitsDataGridViewTextBoxColumn.Name = "stopBitsDataGridViewTextBoxColumn";
            this.stopBitsDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.stopBitsDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // parityDataGridViewTextBoxColumn
            // 
            this.parityDataGridViewTextBoxColumn.DataPropertyName = "Parity";
            this.parityDataGridViewTextBoxColumn.HeaderText = "Parity";
            this.parityDataGridViewTextBoxColumn.Name = "parityDataGridViewTextBoxColumn";
            this.parityDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.parityDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataBitsDataGridViewTextBoxColumn
            // 
            this.dataBitsDataGridViewTextBoxColumn.DataPropertyName = "DataBits";
            this.dataBitsDataGridViewTextBoxColumn.HeaderText = "DataBits";
            this.dataBitsDataGridViewTextBoxColumn.Name = "dataBitsDataGridViewTextBoxColumn";
            // 
            // isConnectDataGridViewCheckBoxColumn
            // 
            this.isConnectDataGridViewCheckBoxColumn.DataPropertyName = "IsConnect";
            this.isConnectDataGridViewCheckBoxColumn.HeaderText = "IsConnect";
            this.isConnectDataGridViewCheckBoxColumn.Name = "isConnectDataGridViewCheckBoxColumn";
            this.isConnectDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            // 
            // describeDataGridViewTextBoxColumn
            // 
            this.describeDataGridViewTextBoxColumn.DataPropertyName = "Describe";
            this.describeDataGridViewTextBoxColumn.HeaderText = "Describe";
            this.describeDataGridViewTextBoxColumn.Name = "describeDataGridViewTextBoxColumn";
            // 
            // isHexDataGridViewCheckBoxColumn
            // 
            this.isHexDataGridViewCheckBoxColumn.DataPropertyName = "IsHex";
            this.isHexDataGridViewCheckBoxColumn.HeaderText = "IsHex";
            this.isHexDataGridViewCheckBoxColumn.Name = "isHexDataGridViewCheckBoxColumn";
            // 
            // lightCtrlTypeDataGridViewTextBoxColumn
            // 
            this.lightCtrlTypeDataGridViewTextBoxColumn.DataPropertyName = "LightCtrlType";
            this.lightCtrlTypeDataGridViewTextBoxColumn.HeaderText = "LightCtrlType";
            this.lightCtrlTypeDataGridViewTextBoxColumn.Name = "lightCtrlTypeDataGridViewTextBoxColumn";
            this.lightCtrlTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lightCtrlTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // lightCtrlNameDataGridViewTextBoxColumn
            // 
            this.lightCtrlNameDataGridViewTextBoxColumn.DataPropertyName = "LightCtrlName";
            this.lightCtrlNameDataGridViewTextBoxColumn.HeaderText = "LightCtrlName";
            this.lightCtrlNameDataGridViewTextBoxColumn.Name = "lightCtrlNameDataGridViewTextBoxColumn";
            this.lightCtrlNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lightCtrlNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(VisionBase.LightCrlParaItem);
            // 
            // FrmLightCrlManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 683);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmLightCrlManager";
            this.TabText = "FrmLightCrlManager";
            this.Text = "FrmLightCrlManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLightCrlManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmLightCrlManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.DataGridViewComboBoxColumn portNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baudRateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn stopBitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn parityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataBitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn isConnectDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn describeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isHexDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn lightCtrlTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn lightCtrlNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}