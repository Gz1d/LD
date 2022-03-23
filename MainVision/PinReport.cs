using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using WeifenLuo.WinFormsUI.Docking;

namespace MainVision
{
    public partial class PinReport : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public PinReport()
        {
            InitializeComponent();
        }

        private void PinReport_Load(object sender, EventArgs e)
        {
            getXmlInfo();

            

        }


        private void getXmlInfo()
        {
            try {
                string ConfigName = string.Format(@"config\{0}", "InspectReport.xml");
                DataSet myds = new DataSet();
                myds.ReadXml(ConfigName);
                dataGridView1.DataSource = myds.Tables[0];
            }
            catch
            { }
        }
        //手动更新数据源到控件
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getXmlInfo();
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dataGridView1, true);
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

        String DateStart, TimeStart, DateEnd, TimeDEnd;

        private void btnToExcel1_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dataGridView2, true);

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 时间控件产生查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string DateStart1 = dateTimePicker2.Text;

            DateStart = dateTimePicker2.Text.Replace("-", "");
            TimeStart = dateTimePicker1.Text.Replace(":", "");
            DateEnd = dateTimePicker3.Text.Replace("-", "");
            TimeDEnd = dateTimePicker4.Text.Replace(":", "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<double> X = new List<double>();
            List<double> Y1 = new List<double>();
            List<double> Y2 = new List<double>();
            List<double> Y3 = new List<double>();
            Random ran = new Random();
            double RandKey = ran.Next(10, 30);
            for (int i = 0; i < 300; i++)
            {
                X.Add(i);
                RandKey = ran.Next(10, 30);
                Y1.Add(RandKey);
                RandKey = ran.Next(10, 30);
                Y2.Add(RandKey);
                RandKey = ran.Next(0, 20);
                Y3.Add(RandKey);
            }

         }

        private void PinReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Dialog MyDlg = 
            DialogResult DlgReslult = MessageBox.Show("请勿关闭当前窗体", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DlgReslult == DialogResult.No) e.Cancel =true;
        }

        private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnToReport_Click(object sender, EventArgs e)
        {
            GetReportItemsByDateTime(DateStart + TimeStart, DateEnd + TimeDEnd);
        }

        private void GetReportItemsByDateTime(string Min, string Max)
        {
            string ConfigName = string.Format(@"config\{0}", "InspectReport.xml");
            XElement xe = XElement.Load(ConfigName);//加载XML文件

            

            //dataGridView1.DataSource = myds1.Tables[0];
            //  ("yy-MM-dd-HH-mm")
            try
            {
                InitShowDataTbx();
                long min = long.Parse(Min.Replace("-", ""));
                long max = long.Parse(Max.Replace("-", ""));

                IEnumerable<XElement> elements = from lst in xe.Elements("InspectResult")

                                                 where long.Parse(( Regex.Replace(lst.Attribute("时间").Value, @"\s", "") ) .Replace("/","").Replace(":",""))>=min
                                                 && long.Parse((Regex.Replace(lst.Attribute("时间").Value, @"\s", "")).Replace("/", "").Replace(":", "")) <=max
                                                 && lst.Element("产品名称").Value.Equals(VisionBase.ProjectParaManager.Instance.ProductName)  //产品名字
                                                 select lst;
                //XmlDocument xmlDoc = new XmlDocument();
                //DataSet myds1 = new DataSet();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("工艺", typeof(string));   //新建第一列
                dt.Columns.Add("产品名称", typeof(string));      //新建第二列
                dt.Columns.Add("X偏移（mm）", typeof(string));      //新建第三列
                dt.Columns.Add("Y偏移（mm）", typeof(string));      //新建第四列
                dt.Columns.Add("角度偏移（度）", typeof(string));      //新建第四列
                dt.Columns.Add("胶水宽(mm)", typeof(string));      //新建第四列
                dt.Columns.Add("胶水高（mm）", typeof(string));      //新建第四列
                dt.Columns.Add("胶水的面积（mm*mm）", typeof(string));      //新建第四列
                int Count = 0;
                //计算CPK用的
                List<double> OffSetXs = new List<double>();
                List<double> OffsetYs = new List<double>();
                List<double> OffSetAngles = new List<double>();
                List<double> GlueWidths = new List<double>();
                List<double> GlueHights = new List<double>();
                List<double> GlueAreaS = new List<double>();
                double OffSetX = 0,OffSetY =0,OffSetAngle =0,GlueWid =0,GlueHei =0,GlueArea =0;
                foreach (XElement element in elements)//遍历查询结果
                {

                    //XNode element1 =element.AddAfterSelf();
                    //element.Save("PinReport1.xml");
                    Count++;
                    string NowStr = "";
                    try {
                        NowStr = element.Element("X偏移").Value;
                        OffSetX = Convert.ToDouble(NowStr);
                    }
                    catch { }
                    try {
                        NowStr = element.Element("Y偏移").Value;
                        OffSetY = Convert.ToDouble(NowStr);
                    }
                    catch { }
                    try{
                        NowStr = element.Element("角度偏移").Value;
                        OffSetAngle = Convert.ToDouble(NowStr);
                    }
                    catch { }


                    try  {
                        NowStr = element.Element("胶水面积").Value;
                        GlueArea = Convert.ToDouble(NowStr);
                    }
                    catch { }

                    try
                    {
                        NowStr = element.Element("胶水宽").Value;
                        GlueWid = Convert.ToDouble(NowStr);
                    }
                    catch { }
                    try
                    {
                        NowStr = element.Element("胶水高").Value;
                        GlueHei = Convert.ToDouble(NowStr);
                    }
                    catch { }
                    OffSetXs.Add(OffSetX);
                    OffsetYs.Add(OffSetY);
                    OffSetAngles.Add(OffSetAngle);
                    GlueAreaS.Add(GlueArea);
                    GlueHights.Add(GlueHei);
                    GlueWidths.Add(GlueWid);

                    dt.Rows.Add(element.Element("工艺").Value ,element.Element("产品名称").Value,element.Element("X偏移").Value,
                        element.Element("Y偏移").Value,element.Element("角度偏移").Value, element.Element("胶水面积").Value
                        , element.Element("胶水宽").Value, element.Element("胶水高").Value);             //新建第一行，并赋值

                    ////myds1.Load(element);
                    //textBox11.Text = element.Element("工艺").Value;//显示工艺名称
                    //comboBox1.SelectedItem = element.Element("产品编号").Value;//显示产品编号
                    //textBox12.Text = element.Element("产品工位").Value;//显示工位
                    //textBox12.Text = element.Element("最大偏移量").Value;//显示偏移量 
                }
                dataGridView2.DataSource = dt;
                //计算FPK；


                //element.Save("PinReport1.xml");
                //XmlDocument xmlDoc = new XmlDocument();
                ////xmlDoc.Load(@"config\{0}", "PinReport.xml");
                //XmlNode root = xmlDoc.SelectSingleNode("pinreport");//查找<bookstore>
                //root.AppendChild(elements.ToList);//添加到<bookstore>节点中
                //dataGridView2.DataSource = elements.ToList();
                //dataGridView2.DataBindings();

            }
            catch
            {   }


        }
        public void InitShowDataTbx()
        {
        
        }
    }
}
