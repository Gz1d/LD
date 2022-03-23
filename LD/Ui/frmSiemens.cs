using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HslCommunication.Profinet.Siemens;
using HslCommunication;
using HslCommunication.Core;
using System.IO.Ports;
using System.Collections;


namespace LD.Ui
{
    public partial class frmSiemens : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        private static frmSiemens _instance = new frmSiemens();
        public static frmSiemens Instance
        {
            set
            {
                _instance = value;
            }
            get
            {
                return _instance;
            }
        }

        public frmSiemens()
        {
            InitializeComponent();
        }

        private void frmSiemens_Load(object sender, EventArgs e)
        {
            this.btnStop.Enabled = false;
            //Device.DeviceManager.Instance.DeviceInit();
            //Device.DeviceManager.Instance.DeviceStart();

            try
            {
                this.dgvPlcType.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcTypeItems;
                this.devType.Items.Clear();
                foreach (Common.DeviceType temp in Enum.GetValues(typeof(Common.DeviceType)))
                {
                    this.devType.Items.Add(temp);
                }
                //this.plcDataItemBindingSource.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                this.dgvPlcData.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                this.txtCount.Text = (this.dgvPlcData.Rows.Count - 1).ToString();
                this.plcDevice.Items.Clear();
                foreach (Common.PlcDevice data in Enum.GetValues(typeof(Common.PlcDevice)))
                {
                    this.plcDevice.Items.Add(data);
                }
                this.dataType.Items.Clear();
                foreach (Common.DataTypes type in Enum.GetValues(typeof(Common.DataTypes)))
                {
                    this.dataType.Items.Add(type);
                }

                this.dataFormat.Items.Clear();
                foreach (DataFormat dataF in Enum.GetValues(typeof(DataFormat)))
                {
                    this.dataFormat.Items.Add(dataF);
                }

                this.parity.Items.Clear();
                foreach (Parity part in Enum.GetValues(typeof(Parity)))
                {
                    this.parity.Items.Add(part);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //PLC数据变化事件回调
        public void plc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                this.Invoke(new EventHandler(delegate
                {
                    try
                    {
                        if (e.PropertyName == "ValueNew")
                        {
                            Config.PlcDataItem plcdata = (Config.PlcDataItem)sender;

                            dgvPlcData.Refresh();
                        }

                    }
                    catch { }
                }));
            }
            catch { }
        }

        private void frmSiemens_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //释放设备
                //Device.DeviceManager.Instance.DeviceStop();
                //Device.DeviceManager.Instance.DeviceRelease();
                //Config.ConfigManager.Instance.Save();
            }
            catch
            { }
        }

        private void dgvPlcType_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        //聚焦设备项
        private void dgvPlcType_Focus(Config.PlcTypeItem plc)
        {
            if (!this.chkShowAll.Checked)
            {
                //this.dgvPlcData.EndEdit();
                //this.dgvPlcData.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                //List<Config.PlcDataItem> checkItems = this.dgvPlcData.Rows.Cast<DataGridViewRow>().Where
                //    (x => (string)x.Cells["deviceName"].Value == plc.DevName)
                //    .Select(x => x.DataBoundItem).Cast<Config.PlcDataItem>().ToList();

                IEnumerable<Config.PlcDataItem> ie = from lst in Config.ConfigManager.Instance.ConfigPlc.PlcDataItems
                                                     where lst.DeviceName == plc.DevName
                                                     select lst;
                List<Config.PlcDataItem> ioLst = ie.Cast<Config.PlcDataItem>().ToList();
                this.dgvPlcData.DataSource = ioLst;
                this.dgvPlcData.Refresh();
                this.txtCount.Text = dgvPlcData.Rows.Count.ToString();
            }
            else
            {
                //this.dgvPlcData.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                this.txtCount.Text = (dgvPlcData.Rows.Count - 1).ToString();
            }


        }

        private void dgvPlcType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    //当前客户端设备
                    Config.PlcTypeItem plc = (Config.PlcTypeItem)this.dgvPlcType.CurrentRow.DataBoundItem;
                    this.dgvPlcType_Focus(plc);
                    //if (!this.chkShowAll.Checked)
                    //{
                    //    this.dgvPlcData.EndEdit();
                    //    this.dgvPlcData.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                    //    List<Config.PlcDataItem> checkItems = this.dgvPlcData.Rows.Cast<DataGridViewRow>().Where
                    //        (x => (string)x.Cells["deviceName"].Value == plc.DevName)
                    //        .Select(x => x.DataBoundItem).Cast<Config.PlcDataItem>().ToList();
                    //    this.dgvPlcData.DataSource = checkItems;
                    //    this.dgvPlcData.Refresh();
                    //}
                    //else
                    //{
                    //    //this.dgvPlcData.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                    //}

                }
            }
            catch
            {
            }

        }

        private void dgvPlcType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //当前客户端设备
                    Config.PlcTypeItem plc = (Config.PlcTypeItem)this.dgvPlcType.CurrentRow.DataBoundItem;

                    switch (dgvPlcType.Columns[e.ColumnIndex].Name)
                    {
                        case "btnDelPlcType":
                            //删除
                            if (MessageBox.Show("是否删除该项设备？    ", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                            Config.ConfigManager.Instance.ConfigPlc.PlcTypeItems.Remove(plc);
                            break;

                        case "devName":
                            //Config.PlcDataItem newData = new Config.PlcDataItem();
                            //newData.DeviceName = plc.DevName;
                            //newData.ItemName = plc.DevName + "_";
                            //Config.ConfigManager.Instance.ConfigPlc.PlcDataItems.Add(newData);
                            ////this.dgvPlcData.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                            //this.dgvPlcData.Refresh();
                            break;

                        default:
                            break;
                    }

                }
            }
            catch
            {    }
        }

        private void dgvPlcType_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvPlcType.Columns[e.ColumnIndex].Name.Equals("isConnectedPLC"))
                {
                    Config.PlcTypeItem item = (Config.PlcTypeItem)this.dgvPlcType.Rows[e.RowIndex].DataBoundItem;
                    if (item == null)
                        e.Value = this.imageList1.Images[3];
                    else if (item.IsActive)
                    {
                        e.Value = (item.IsConnected == true) ? this.imageList1.Images[1] : this.imageList1.Images[2];
                    }
                    else
                    {
                        e.Value = this.imageList1.Images[0];
                    }
                }
            }
            catch
            { }
        }

        private void dgvPlcData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //当前网络客户端
                    if (this.dgvPlcData.CurrentRow.DataBoundItem == null) return;
                    Config.PlcDataItem plcdata = (Config.PlcDataItem)this.dgvPlcData.CurrentRow.DataBoundItem;
                    Config.PlcTypeItem devType = Logic.PlcHandle.Instance.GetPlcTypeItem(plcdata.DeviceName);
                    //Config.PlcTypeItem devType = (Config.PlcTypeItem)this.dgvPlcType.CurrentRow.DataBoundItem;
                    switch (dgvPlcData.Columns[e.ColumnIndex].Name)
                    {
                        case "btnDelData":
                            //删除
                          //  if (MessageBox.Show("是否删除该项设备？    ", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                            Config.ConfigManager.Instance.ConfigPlc.PlcDataItems.Remove(plcdata);
                            this.dgvPlcType_Focus(devType);
                            break;
                        case "btnReadPlc":   //读值
                            Logic.PlcHandle.Instance.ReadValue(plcdata.PlcDevice);
                            this.dgvPlcData.CurrentRow.Cells["valueNewCol"].Value = plcdata.ValueNew;
                            //int i = this.dgvPlcData.CurrentRow.Index;
                            //this.dgvPlcData.InvalidateRow(i);
                            break;
                        case "btnWritePlc":   //写值
                            Logic.PlcHandle.Instance.WriteValue(plcdata.PlcDevice, plcdata.ValueWrite);
                            //if(!devType.IsConnected && Common.IsSimulator)
                            //{
                            //    //plcdata.ValueNew = false;
                            //    //plcdata.ValueNew = true;
                            //}
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {   }
        }

        private void dgvPlcData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Ui.frmSiemens   opc = new frmSiemens();
            opc.Show();
            //Config.ConfigManager.Instance.ConfigPlc.bReadThread = true;
            //this.btnStart.Enabled = false;
            //this.btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //Config.ConfigManager.Instance.ConfigPlc.bReadThread = false;
            //this.btnStop.Enabled = false;
            //this.btnStart.Enabled = true;
        }

        //导出枚举
        private void btnMj_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "枚举另存";
                save.Filter = "txt files   (*.txt)|*.txt";
                save.FilterIndex = 2;
                save.RestoreDirectory = true;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string file = save.FileName;
                    save.Dispose();
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch { }
                    Log.LogWriter.WriteEnum(file, "/// <summary>");
                    Log.LogWriter.WriteEnum(file, "/// PLC设备枚举");
                    Log.LogWriter.WriteEnum(file, "/// </summary>");
                    Log.LogWriter.WriteEnum(file, "public enum PlcDevice");
                    Log.LogWriter.WriteEnum(file, "{{");

                    Log.LogWriter.WriteEnum(file, "    /// <summary>");
                    Log.LogWriter.WriteEnum(file, "    /// 错误");
                    Log.LogWriter.WriteEnum(file, "    /// </summary>");
                    Log.LogWriter.WriteEnum(file, "    ERROR=-1,");
                    Log.LogWriter.WriteEnum(file, "    ");

                    Log.LogWriter.WriteEnum(file, "    /// <summary>");
                    Log.LogWriter.WriteEnum(file, "    /// 空");
                    Log.LogWriter.WriteEnum(file, "    /// </summary>");
                    Log.LogWriter.WriteEnum(file, "    NULL=0,");
                    Log.LogWriter.WriteEnum(file, "    ");


                    for (int i = 0; i < this.dgvPlcData.RowCount; i++)
                    {
                        Config.PlcDataItem o = (Config.PlcDataItem)this.dgvPlcData.Rows[i].DataBoundItem;
                        if (o != null)
                        {
                            Log.LogWriter.WriteEnum(file, "    /// <summary>");
                            Log.LogWriter.WriteEnum(file, "    /// {0}", o.Descrip);
                            Log.LogWriter.WriteEnum(file, "    /// </summary>");
                            Log.LogWriter.WriteEnum(file, "    {0},", o.ItemName);
                            Log.LogWriter.WriteEnum(file, "    ");
                        }
                    }
                    Log.LogWriter.WriteEnum(file, "}}");
                    System.Threading.Thread.Sleep(100);
                    System.Diagnostics.Process.Start("notepad", file);
                }
            }
            catch { }
        }

        //清空数据
        private void btnClear_Click(object sender, EventArgs e)
        {
            //this.dgvPlcData.DataSource = null;
            if (MessageBox.Show("是否删除该项设备？    ", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (!this.chkShowAll.Checked)
            {
                Config.PlcTypeItem plc = (Config.PlcTypeItem)this.dgvPlcType.CurrentRow.DataBoundItem;
                IEnumerable ie = from lst in Config.ConfigManager.Instance.ConfigPlc.PlcDataItems
                                 where lst.DeviceName == plc.DevName
                                 select lst;
                List<Config.PlcDataItem> ioLst = ie.Cast<Config.PlcDataItem>().ToList();

                foreach (Config.PlcDataItem item in ioLst)
                {
                    Config.ConfigManager.Instance.ConfigPlc.PlcDataItems.Remove(item);
                }
                this.dgvPlcType_Focus(plc);
                return;
            }
            Config.ConfigManager.Instance.ConfigPlc.PlcDataItems.Clear();
        }

        //是否全部显示数据
        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chkShowAll.Checked)
                {
                    this.dgvPlcData.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                    this.txtCount.Text = (this.dgvPlcData.Rows.Count - 1).ToString();
                }
                else
                {
                    Config.PlcTypeItem plc = (Config.PlcTypeItem)this.dgvPlcType.CurrentRow.DataBoundItem;
                    this.dgvPlcType_Focus(plc);
                }

                //Config.ConfigManager.Instance.ConfigPlc.ShowAllItem = this.chkShowAll.Checked;

            }
            catch { }
        }

        //添加新行数据
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Config.PlcTypeItem plc = (Config.PlcTypeItem)this.dgvPlcType.CurrentRow.DataBoundItem;

                Config.PlcDataItem newData = new Config.PlcDataItem();
                newData.DeviceName = plc.DevName;
                newData.ItemName = plc.DevName + "_";
                newData.MachineID = plc.MachineID;
                Config.ConfigManager.Instance.ConfigPlc.PlcDataItems.Add(newData);
                //this.dgvPlcData.DataSource = Config.ConfigManager.Instance.ConfigPlc.PlcDataItems;
                //this.dgvPlcData.Refresh();
                this.dgvPlcType_Focus(plc);
            }
            catch
            {
                //throw;
            }
        }

        List<Config.PlcDataItem> m_plcDataItem = new List<Config.PlcDataItem>();

        //数据匹配
        private void btnMatch_Click(object sender, EventArgs e)
        {

            try
            {
                m_plcDataItem.Clear();
                for (int i = 0; i < this.dgvPlcData.RowCount; i++)
                {
                    Config.PlcDataItem o = (Config.PlcDataItem)this.dgvPlcData.Rows[i].DataBoundItem;
                    if (o != null)
                    {
                        Config.PlcDataItem plc = Logic.PlcHandle.Instance.GetPlcDataItem(o.ItemName);
                        if (plc != null)
                        {
                            try
                            {
                                plc.PlcDevice = (Common.PlcDevice)Enum.Parse(typeof(Common.PlcDevice), plc.ItemName);
                            }
                            catch
                            {
                                m_plcDataItem.Add(plc);
                            }
                        }
                        else
                        {
                            m_plcDataItem.Add(plc);
                        }
                    }
                }
                this.dgvPlcData.Refresh();
                if (m_plcDataItem.Count > 0)
                {

                    SaveFileDialog save = new SaveFileDialog();
                    save.Title = "枚举另存";
                    save.Filter = "txt files   (*.txt)|*.txt";
                    save.FilterIndex = 2;
                    save.RestoreDirectory = true;
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        string file = save.FileName;
                        try
                        {
                            System.IO.File.Delete(file);
                        }
                        catch { }
                        Log.LogWriter.WriteEnum(file, "/// <summary>");
                        Log.LogWriter.WriteEnum(file, "/// PLC设备枚举");
                        Log.LogWriter.WriteEnum(file, "/// </summary>");
                        Log.LogWriter.WriteEnum(file, "public enum PlcDevice");
                        Log.LogWriter.WriteEnum(file, "{{");


                        for (int i = 0; i < this.m_plcDataItem.Count; i++)
                        {
                            Config.PlcDataItem o = this.m_plcDataItem[i];
                            if (o != null)
                            {
                                Log.LogWriter.WriteEnum(file, "    /// <summary>");
                                Log.LogWriter.WriteEnum(file, "    /// {0}", o.Descrip);
                                Log.LogWriter.WriteEnum(file, "    /// </summary>");
                                Log.LogWriter.WriteEnum(file, "    {0},", o.ItemName);
                                Log.LogWriter.WriteEnum(file, "    ");
                            }
                        }
                        Log.LogWriter.WriteEnum(file, "}}");
                        System.Threading.Thread.Sleep(100);
                        System.Diagnostics.Process.Start("notepad", file);
                        m_plcDataItem.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnImportData_Click(object sender, EventArgs e)
        {

            Ui.frmImportPlc frm = new frmImportPlc();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Config.ConfigManager.Instance.ConfigPlc.Save();
        }

        private void plcDataItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void frmSiemens_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult TS = MessageBox.Show("请勿关闭此页面，点“否”取消关闭", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (TS == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
    }
}
