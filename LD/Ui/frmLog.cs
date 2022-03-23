using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace LD.Ui
{
    public partial class frmLog : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public frmLog()
        {
            if (LD.Config.ConfigManager.Instance.ConfigLog.SelectLanguage)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
            InitializeComponent();
        }

        

        private void frmLog_Load(object sender, EventArgs e)
        {
            try
            {
                this.dgvRunlog.DataSource = Config.ConfigManager.Instance.ConfigLog.RunLogs;
                this.chkRunlog.Checked = Config.ConfigManager.Instance.ConfigLog.WriteLogRun;
            }
            catch 
            {

            }
        }


        #region 运行日志
        private void chkRunlog_CheckedChanged(object sender, EventArgs e)
        {
            Config.ConfigManager.Instance.ConfigLog.WriteLogRun = chkRunlog.Checked;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string file = string.Format(@"log\Run\Run_{0}.txt", DateTime.Now.Day.ToString("00"));
            System.Diagnostics.Process.Start("notepad", file);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Config.ConfigManager.Instance.ConfigLog.RunLogs.Clear();
        }

        #endregion
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Config.ConfigManager.Instance.ConfigLog.Save();
        }
    }
}
