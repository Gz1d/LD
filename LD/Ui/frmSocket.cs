using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace LD.Ui
{
    public partial class frmSocket : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public frmSocket()
        {
            InitializeComponent();
        }

        private void frmSocket_Load(object sender, EventArgs e)
        {
            //this.socketDeviceS.DataSource = new Common.SocketDevice[]{Common.SocketDevice.MES,
            //    Common.SocketDevice.TEST,Common.SocketDevice.VISION};
            //this.socketDeviceS.ValueType = typeof(Common.SocketDevice);
            //this.socketDeviceS.DataPropertyName = "SocketDevice";

            //this.socketDevice.DataSource = new Common.SocketDevice[]{Common.SocketDevice.MES,
            //   Common.SocketDevice.TEST,Common.SocketDevice.VISION};
            //this.socketDevice.ValueType = typeof(Common.SocketDevice);
            //this.socketDevice.DataPropertyName = "SocketDevice";

            this.socketDevice.DataSource = Enum.GetValues(typeof(Common.SocketDevice));
            this.socketDeviceS.DataSource = Enum.GetValues(typeof(Common.SocketDevice));

            this.dgvClient.DataSource = Config.ConfigManager.Instance.ConfigSocketC.SocketCItems;
            this.dgvServer.DataSource = Config.ConfigManager.Instance.ConfigSocketS.SocketSItems;
            //this.dgvClient.DefaultCellStyle.SelectionBackColor = Color.Gray;
        }

        #region 客户端
        private void gvClient_EditingControlShowing(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                this.dgvClient.Controls.Clear();
                //DataGridViewRow curRow = this.dgvClient.CurrentRow;
                //if (this.dgvClient.CurrentCell.ColumnIndex.Equals(11))
                //{
                Button btnSendServe = new Button();
                btnSendServe.BackColor = SystemColors.Control;
                btnSendServe.Text = "发送";
                btnSendServe.Visible = true;
                btnSendServe.AutoSize = true;
                btnSendServe.Width = this.dgvClient.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Height * 2;
                btnSendServe.Height = this.dgvClient.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Height;
                //btnSendServe.Click += new EventHandler(btnSendServe_Click);
                //btnSendServe.Dock = DockStyle.Right;
                this.dgvClient.Controls.Add(btnSendServe);
                btnSendServe.Location = new System.Drawing.Point(((this.dgvClient.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Right) -
                        (btnSendServe.Width)), this.dgvClient.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Top);
                //}
            }
            catch 
            {

            }

        }

        private void dgvClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //当前网络客户端
                    Config.SocketCItem sck = (Config.SocketCItem)this.dgvClient.CurrentRow.DataBoundItem;

                    switch (dgvClient.Columns[e.ColumnIndex].Name)
                    {
                        case "btnDelete":
                            //删除
                            if (MessageBox.Show("是否删除该项设备？    ", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                            Config.ConfigManager.Instance.ConfigSocketC.SocketCItems.Remove(sck);
                            break;

                        case "btnSendClient":
                            //发送
                            Config.SocketCItem item = (Config.SocketCItem)this.dgvClient.CurrentRow.DataBoundItem;
                            Logic.SocketCHandle.Instance.SocketSend(item.SocketDevice, item.BuffSend);
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

        private void dgvClient_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvClient_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvClient.Columns[e.ColumnIndex].Name.Equals("isConnected"))
                {
                    //int i = e.ColumnIndex;
                    Config.SocketCItem sck = (Config.SocketCItem)this.dgvClient.Rows[e.RowIndex].DataBoundItem;
                    if (sck == null)
                        e.Value = this.imageList1.Images[3];
                    else
                        e.Value = (sck.IsConnected == true) ? this.imageList1.Images[1] : this.imageList1.Images[2];
                }
            }
            catch
            { }
        }
        #endregion

        #region 服务器
        private void dgvServer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //当前网络客户端
                    Config.SocketSItem sck = (Config.SocketSItem)this.dgvServer.CurrentRow.DataBoundItem;

                    switch (dgvServer.Columns[e.ColumnIndex].Name)
                    {
                        case "btnDeleteS":
                            //删除
                            if (MessageBox.Show("是否删除该项设备？    ", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                            Config.ConfigManager.Instance.ConfigSocketS.SocketSItems.Remove(sck);
                            break;

                        case "btnSendServer":
                            //发送
                            Config.SocketSItem item = (Config.SocketSItem)this.dgvServer.CurrentRow.DataBoundItem;
                            Logic.SocketSHandle.Instance.SocketSend(item.SocketDevice, item.BuffSend);
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

        private void dgvServer_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvServer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvServer.Columns[e.ColumnIndex].Name.Equals("isConnectedS"))
                {
                    //int i = e.ColumnIndex;
                    Config.SocketSItem sck = (Config.SocketSItem)this.dgvServer.Rows[e.RowIndex].DataBoundItem;
                    if (sck == null)
                        e.Value = this.imageList1.Images[3];
                    else
                        e.Value = (sck.IsConnected == true) ? this.imageList1.Images[1] : this.imageList1.Images[2];
                }
            }
            catch 
            { }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Config.ConfigManager.Instance.ConfigSocketC.Save();
            Config.ConfigManager.Instance.ConfigSocketS.Save();
        }
    }
}
