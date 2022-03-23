using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace VisionBase
{

    public enum ComEnum
    { 
        COM0 = 0,
        COM1,
        COM2,
        COM3,
        COM4,
        COM5,
        COM6,
        COM7,
        COM8,
        COM9,
        COM10,
        COM11,
        COM12,
        COM13    
    }

    public partial class FrmLightCrlManager : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FrmLightCrlManager()
        {
            InitializeComponent();
            string[] serialPorts = SerialPort.GetPortNames();  //获取所有串口打开光源
            List<ComEnum> ComList = new List<ComEnum>();
            ComEnum[] ComS = new ComEnum[ComList.Count()];
            if (serialPorts.Length > 2){         
                foreach (string item in serialPorts) {
                    ComEnum NowEnum = (ComEnum)Enum.Parse(typeof(ComEnum), item, true);
                    ComList.Add(NowEnum);
                }
                ComS = new ComEnum[ComList.Count()];
                for (int i = 0; i < ComList.Count(); i++){
                    ComS[i] = ComList[i];
                }
            }
            else{ 
              ComS = new ComEnum[] { ComEnum.COM0, ComEnum.COM1, ComEnum.COM2, ComEnum.COM3, ComEnum.COM4, ComEnum.COM5 };
            }
            this.portNameDataGridViewTextBoxColumn.DataSource = ComS;
            this.portNameDataGridViewTextBoxColumn.ValueType = typeof(ComEnum);
            //this.portNameDataGridViewTextBoxColumn.DataPropertyName = "EventSeverity";
            this.stopBitsDataGridViewTextBoxColumn.Items.Clear();
            foreach (StopBits temp in Enum.GetValues(typeof(StopBits))) {
                this.stopBitsDataGridViewTextBoxColumn.Items.Add(temp);
            }
            //this.stopBitsDataGridViewTextBoxColumn.DataPropertyName = "EventSeverity";
            this.parityDataGridViewTextBoxColumn.Items.Clear();
            foreach (Parity temp in Enum.GetValues(typeof(Parity))){
                this.parityDataGridViewTextBoxColumn.Items.Add(temp);
            }
            this.parityDataGridViewTextBoxColumn.ValueType = typeof(Parity);
            //this.parityDataGridViewTextBoxColumn.DataPropertyName = "EventSeverity";
            this.lightCtrlTypeDataGridViewTextBoxColumn.Items.Clear();
            foreach (LightCtrlTypeEnum temp in Enum.GetValues(typeof(LightCtrlTypeEnum))){
                this.lightCtrlTypeDataGridViewTextBoxColumn.Items.Add(temp);
            }
            this.lightCtrlTypeDataGridViewTextBoxColumn.ValueType = typeof(LightCtrlTypeEnum);
            //this.lightCtrlTypeDataGridViewTextBoxColumn.DataPropertyName = "EventSeverity";
            this.lightCtrlNameDataGridViewTextBoxColumn.Items.Clear();
            foreach (LightCtrlEmun temp in Enum.GetValues(typeof(LightCtrlEmun))){
                this.lightCtrlNameDataGridViewTextBoxColumn.Items.Add(temp);
            }
            this.lightCtrlNameDataGridViewTextBoxColumn.ValueType = typeof(LightCtrlEmun);
            // this.lightCtrlNameDataGridViewTextBoxColumn.DataPropertyName = "EventSeverity";
        }

        private static object LockObj = new object();
        private static FrmLightCrlManager MyFrmLightCrlManager;

        public static FrmLightCrlManager Instance
        {
            get {
                lock (LockObj) {
                    return MyFrmLightCrlManager = MyFrmLightCrlManager ?? new FrmLightCrlManager();
                }
            }
        }

        private void FrmLightCrlManager_Load(object sender, EventArgs e)
        {           
            dataGridView1.DataSource = LightCrlParaManager.Instance.LightCtrParaItems;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name.Equals("isConnectDataGridViewCheckBoxColumn")) {
                    if (e.RowIndex < LightCrlParaManager.Instance.LightCtrParaItems.Count()){
                        LightCrlParaItem item = LightCrlParaManager.Instance.LightCtrParaItems[e.RowIndex];
                        // item.IsOpen = true;
                        if (item == null) e.Value = this.imageList1.Images[3];
                        else if (item.IsConnect) {
                            e.Value = (item.IsActive == true) ? this.imageList1.Images[2] : this.imageList1.Images[2];
                        }
                        else{
                            e.Value = this.imageList1.Images[1];
                        }
                    }
                }
            }
            catch
            { }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            LightCrlParaManager.Instance.Save();
        }

        private void FrmLightCrlManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult TS = MessageBox.Show("请勿关闭此页面，点“否”取消关闭", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (TS == DialogResult.Yes)  e.Cancel = false;
            else  e.Cancel = true;
        }
    }
}
