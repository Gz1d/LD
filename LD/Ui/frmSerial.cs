using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LD.Ui
{
    public partial class frmSerial : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public frmSerial()
        {
            //if (LD.Config.ConfigManager.Instance.ConfigLog.SelectLanguage)
            //{
            //    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            //}
            InitializeComponent();
        }

        private void frmSerial_Load(object sender, EventArgs e)
        {
            try
            {
                this.serialDeviceDataGridViewComBotBoxColumn.Items.Clear();
                foreach (Common.SerialDevice temp in Enum.GetValues(typeof(Common.SerialDevice)))
                {
                    this.serialDeviceDataGridViewComBotBoxColumn.Items.Add(temp);
                }
                this.dgvSerial.DataSource = Config.ConfigManager.Instance.ConfigSerial.SerialItems;
            }
            catch { }
        }

        private void dgvSerial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //当前串口
                    Config.SerialItem item = (Config.SerialItem)this.dgvSerial.CurrentRow.DataBoundItem;

                    switch (dgvSerial.Columns[e.ColumnIndex].Name)
                    {
                        case "btnDelete":   //删除
                            if (MessageBox.Show("是否删除该项设备？    ", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                            Config.ConfigManager.Instance.ConfigSerial.SerialItems.Remove(item);
                            break;

                        case "btnSend":   //发送
                            if (item.IsHex)
                                Logic.SearialHandle.Instance.SerialSend(item.SerialDevice, Common.HexStringToByteArray(item.BuffSend));
                            else
                                Logic.SearialHandle.Instance.SerialSend(item.SerialDevice, item.BuffSend);
                            break;

                        case "btnReceive":   //读取
                            item.BuffReceive = Logic.SearialHandle.Instance.SerialReadLine(item.SerialDevice);
                            this.dgvSerial.UpdateCellValue(e.ColumnIndex,e.RowIndex);
                            break;

                        default:
                            break;
                    }
                }
            }
            catch
            {

            }
        }

        private void dgvSerial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvSerial.Columns[e.ColumnIndex].Name.Equals("isConnected"))
                {
                    Config.SerialItem item = (Config.SerialItem)this.dgvSerial.Rows[e.RowIndex].DataBoundItem;
                    if (item == null)
                        e.Value = this.imageList1.Images[3];
                    else
                        e.Value = (item.IsConnected == true) ? this.imageList1.Images[1] : this.imageList1.Images[2];
                }
            }
            catch
            { }
        }

        private void dgvSerial_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Config.ConfigManager.Instance.ConfigSerial.Save();
        }

        private void frmSerial_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult TS = MessageBox.Show("请勿关闭此页面，点“否”取消关闭", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (TS == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
    }
}
