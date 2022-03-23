using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;



namespace LD.Ui
{
    public partial class frmImportPlc : Form
    {

        System.ComponentModel.BindingList<Config.PlcDataItem> plcList = new System.ComponentModel.BindingList<Config.PlcDataItem>();
        public frmImportPlc()
        {
            InitializeComponent();

        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Excel文件|*.xls;*.xlsx";
                file.ValidateNames = true;
                file.CheckPathExists = true;
                file.CheckFileExists = true;
                file.Title = "选择Excel文件";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    this.dgvExcelData.DataSource = null;
                    //默认选择第一个表
                    DataTable ds2 = ReadExcel.Instance.ReadExcelSource(file.FileName).Tables[0];
                    this.dgvExcelData.DataSource = ds2;
                }
                if (file.FileName != null)
                {
                    this.cboSheet.Items.Clear();
                    string[] testSheetList = ReadExcel.Instance.OriginaSheet;
                    foreach (string item in testSheetList)
                    {
                        this.cboSheet.Items.Add(item);
                    }
                    this.cboSheet.SelectedIndex = 0;
                }
                file.Dispose();
            }
            catch 
            {

            }

        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReadExcel.Instance.DataS != null)
            {

                for (int i = 0; i < cboSheet.Items.Count; i++)
                {
                    //切换数据源
                    if (cboSheet.SelectedIndex == i)
                    {
                        DataTable ds2 = ReadExcel.Instance.DataS.Tables[i];
                        dgvExcelData.DataSource = ds2;
                    }
                }
            }
            ckbAllSelect_CheckedChanged(null, null);


        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            bool bFindSame = false;
            for (int i = 0; i < this.plcList.Count; i++)
            {
                Config.PlcDataItem plcData = plcList[i];
                if (plcData != null)
                {
                    Config.PlcDataItem replcData = Logic.PlcHandle.Instance.GetPlcDataItem(plcData.ItemName);
                    if (replcData == null)
                    {
                        plcData.IsActive = false;
                        plcData.MachineID = (plcData.MachineID + "  ").Substring(0, 2).Trim();
                        Logic.PlcHandle.Instance.config.PlcDataItems.Add(plcData);
                    }
                    else
                    {
                        bFindSame = false;
                        for (int j = 0; j < Logic.PlcHandle.Instance.config.PlcDataItems.Count; j++)
                        {
                            Config.PlcDataItem di = Logic.PlcHandle.Instance.config.PlcDataItems[j];
                            if (di.MachineID == plcData.MachineID && di.ItemName == plcData.ItemName &&
                                di.DeviceName == plcData.DeviceName && di.Address == plcData.Address &&
                                di.DataType == plcData.DataType)
                            {
                                bFindSame = true;
                                break;
                            }
                        }
                        if (!bFindSame)
                        {
                            Logic.PlcHandle.Instance.config.PlcDataItems.Add(plcData);
                        }
                    }

                }
            }
        }

        private void dgvExcelData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvExcelData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvExcelData.Rows.Count > 0 && e.RowIndex >= 0)
            {
                try
                {
                    string selectValue = dgvExcelData.Rows[e.RowIndex].Cells["ckbColumn"].EditedFormattedValue.ToString();
                    if (selectValue == "True")
                    {
                        //获取选中值  
                        DataRowView ei = (DataRowView)dgvExcelData.Rows[e.RowIndex].DataBoundItem;
                        if (ei == null)
                        {
                            return;
                        }
                        //导入当前行数据
                        this.plcList.Add(new Config.PlcDataItem
                        {
                            MachineID = ei.Row.ItemArray[0].ToString().Trim(),
                            DeviceName = ei.Row.ItemArray[1].ToString().Trim(),
                            ItemName = ei.Row.ItemArray[2].ToString().Trim(),
                            Address = ei.Row.ItemArray[3].ToString().Trim(),
                            DataType = (Common.DataTypes)Enum.Parse(typeof(Common.DataTypes), ei.Row.ItemArray[4].ToString().Trim().Substring(0, 1).ToUpper() + ei.Row.ItemArray[4].ToString().Trim().Substring(1)),
                            Descrip = ei.Row.ItemArray[5].ToString().Trim(),
                        });

                    }
                }
                catch (Exception ex)
                {
                    Log.LogWriter.WriteException(ex.Message);
                }
            }
        }

        private void ckbAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbAllSelect.CheckState == CheckState.Checked)
            {
                int k = dgvExcelData.RowCount;
                if (k != 0)
                {
                    for (int i = 0; i < k; i++)
                    {
                        //设置表格单选框列为选中
                        dgvExcelData.Rows[i].Cells["ckbColumn"].Value = true;
                    }
                }
            }
            else
            {
                int k = dgvExcelData.RowCount;
                if (k != 0)
                {
                    dgvExcelData.EndEdit();
                    for (int i = 0; i < k; i++)
                    {
                        dgvExcelData.Rows[i].Cells["ckbColumn"].Value = false;
                    }
                }
            }
        }

        private void FrmLoad_Load(object sender, EventArgs e)
        {

        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            if (this.plcList.Count > 0)
            {
                this.plcList.Clear();
                MessageBox.Show("  清空完毕!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
