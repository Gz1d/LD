using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainVision
{
    public partial class LightForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public LightForm()
        {
            InitializeComponent();
        }
        private void trackBar_LED1_Scroll(object sender, EventArgs e)
        {
            try {
                string m = this.trackBar_LED1.Value.ToString("X2");
                this.textBox_LED1.Text = this.trackBar_LED1.Value.ToString();
                string m1 = m.Substring(0, 1);//取第一位
                int m2 = Convert.ToInt32(m1, 16) + 6;
                string m3 = m2.ToString("X2").Substring(1, 1);
                string m4 = m.Substring(m.Length - 1, 1);//取最后一位
                string mCode = "40 05 01 00 1A 00" + " " + m + " " + m3 + m4;
                LD.Logic.SearialHandle.Instance.SerialSend(LD.Common.SerialDevice.LIGHTT201, LD.Common.HexStringToByteArray(mCode));
                ShowRecCode(LD.Logic.SearialHandle.Instance.SerialReadLine(LD.Common.SerialDevice.LIGHTT201));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错提示");
            }
        }

        private void ShowRecCode(string str)
        {
            try {
                DateTime dt = DateTime.Now;
                tbxRecvData.Text += dt.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                tbxRecvData.SelectAll();
                tbxRecvData.SelectionColor = Color.Blue;
                tbxRecvData.Text += str + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错提示");
            }
        }

        private void trackBar_LED2_Scroll(object sender, EventArgs e)
        {
            try{
                string m = this.trackBar_LED2.Value.ToString("X2");
                this.textBox_LED2.Text = this.trackBar_LED2.Value.ToString();
                string m1 = m.Substring(0, 1);//取第一位
                int m2 = Convert.ToInt32(m1, 16) + 6;
                string m3 = m2.ToString("X2").Substring(1, 1);
                string m4 = m.Substring(m.Length - 1, 1);//取最后一位
                string mCode = "40 05 01 00 1A 00" + " " + m + " " + m3 + m4;
                LD.Logic.SearialHandle.Instance.SerialSend(LD.Common.SerialDevice.LIGHTT201, LD.Common.HexStringToByteArray(mCode));
                ShowRecCode(LD.Logic.SearialHandle.Instance.SerialReadLine(LD.Common.SerialDevice.LIGHTT201));
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message, "出错提示");
            }
        }

        private void Btn_OpenCh1_Click(object sender, EventArgs e){
            LD.Logic.SearialHandle.Instance.SerialSend(LD.Common.SerialDevice.LIGHTT201, LD.Common.HexStringToByteArray("40 05 01 00 2A 00 01 71"));
            ShowRecCode(LD.Logic.SearialHandle.Instance.SerialReadLine(LD.Common.SerialDevice.LIGHTT201));
        }

        private void Btn_CloseCh1_Click(object sender, EventArgs e) {
            LD.Logic.SearialHandle.Instance.SerialSend(LD.Common.SerialDevice.LIGHTT201, LD.Common.HexStringToByteArray("40 05 01 00 2A 00 00 70"));
            ShowRecCode(LD.Logic.SearialHandle.Instance.SerialReadLine(LD.Common.SerialDevice.LIGHTT201));
        }

        private void Btn_OpenCh2_Click(object sender, EventArgs e)
        {
            LD.Logic.SearialHandle.Instance.SerialSend(LD.Common.SerialDevice.LIGHTT201, LD.Common.HexStringToByteArray("40 05 01 00 2A 01 01 72"));
            ShowRecCode(LD.Logic.SearialHandle.Instance.SerialReadLine(LD.Common.SerialDevice.LIGHTT201));
        }

        private void Btn_CloseCh2_Click(object sender, EventArgs e)
        {
            LD.Logic.SearialHandle.Instance.SerialSend(LD.Common.SerialDevice.LIGHTT201, LD.Common.HexStringToByteArray("40 05 01 00 2A 01 00 71"));
            ShowRecCode(LD.Logic.SearialHandle.Instance.SerialReadLine(LD.Common.SerialDevice.LIGHTT201));
        }

        private void Btn_ReSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
