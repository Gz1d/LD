using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
namespace LD.Ui
{
    public partial class frmReport : WeifenLuo.WinFormsUI.Docking.DockContent
    {
     
        public frmReport()
        {
            if (LD.Config.ConfigManager.Instance.ConfigLog.SelectLanguage)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
            InitializeComponent();
        }

        Config.ConfigReport cfg;
        private void frmReport_Load(object sender, EventArgs e)
        {

            LD.Config.report rep = new LD.Config.report();

            //获取报表配置类对象
            cfg = Config.ConfigManager.Instance.ConfigReport;
            //获取报表类对象
            BindingList<Config.report> ReportItems = cfg.ReportItems;
            //绑定数据到表格控件
            dataGridView1.DataSource = rep;
            //获取报表配置信息 并把配置信息显示到UI控件
            textBox1.Text = cfg.ClassNumberMax.ToString();//最大信息数量
            comboBox1.Text = cfg.AcquTime.ToString()+"小时";//采集ishijian
            comboBox2.Text = cfg.Class;//班次

            foreach (Config.report item in ReportItems)
            {
                if (item.VisionID!=null)
                {
                    if (!comboBox3.Items.Contains(item.VisionID))
                    {
                        comboBox3.Items.Add(item.VisionID);
                    }
                    comboBox3.Text = item.VisionID;
                }
            }

        }
        /// <summary>
        /// 选择班次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {  
            cfg.Class = comboBox2.Text;
            SaveReport();
            button2.Enabled = true;
        }
        /// <summary>
        /// 启动停止报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Logic.ReportHandle.Instance.ReportStart( );
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                button2.Enabled = false;

            }
            else
            {
                Logic.ReportHandle.Instance.ReportStop();
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                button2.Enabled = true;
            }
            
        }

/// <summary>
/// 报表最大容量设置
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (int.TryParse(textBox1.Text, out a))
            {
                Config.ConfigManager.Instance.ConfigReport.ClassNumberMax = a;
                SaveReport();
            }
            else
            {
                MessageBox.Show("请输入数字!");
            }
        }




        /// <summary>
        /// 更换班次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Logic.ReportHandle.Instance.ChangeeClass();
            button2.Enabled = false;
        }


        /// <summary>
        /// 报表采集时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cfg.AcquTime = comboBox1.SelectedIndex + 1;
            SaveReport();

        }

        /// <summary>
        /// 保存报表数据
        /// </summary>
        private void SaveReport()
        {
            Config.ConfigManager.Instance.ConfigReport.Save();
        }
        /// <summary>
        /// 产生报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = Logic.ReportHandle.Instance.GetReportItemsByDateTime(DateStart + TimeStart, DateEnd + TimeDEnd, comboBox3.Text);
        }

        /// <summary>
        /// 开始结束时间
        /// </summary>
        String DateStart, TimeStart, DateEnd, TimeDEnd;

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dataGridView1, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dataGridView2, true);
        }

        /// <summary>
        /// 时间控件产生查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
             DateStart =  dateTimePicker2.Text.Replace("-","");
             TimeStart = dateTimePicker1.Text.Replace(":", "");
             DateEnd = dateTimePicker3.Text.Replace("-", "");
             TimeDEnd = dateTimePicker4.Text.Replace(":", "");
        }



        #region DataGridView数据显示到Excel   
        /// <summary>    
        /// 打开Excel并将DataGridView控件中数据导出到Excel   
        /// </summary>    
        /// <param name="dgv">DataGridView对象 </param>    
        /// <param name="isShowExcle">是否显示Excel界面 </param>    
        /// <remarks>   
        /// add com "Microsoft Excel 11.0 Object Library"   
        /// using Excel=Microsoft.Office.Interop.Excel;   
        /// </remarks>   
        /// <returns> </returns>    
        private bool DataGridviewShowToExcel(DataGridView dgv, bool isShowExcle)
        {
            if (dgv.Rows.Count == 0)
                return false;
            //建立Excel对象    
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称    
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }
            //填充数据    
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    if (dgv[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" + dgv[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = dgv[j, i].Value.ToString();
                    }
                }
            }
            return true;
        }
        #endregion



    }
}