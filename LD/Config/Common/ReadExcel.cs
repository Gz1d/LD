using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace LD
{
    public class ReadExcel
    {
        private static ReadExcel instance = new ReadExcel();

        private ReadExcel()
        { }

        public static ReadExcel Instance
        {
            get { return instance; }
        }

        //存表名
        private string[] originalSheet;
        public string[] OriginaSheet
        {
            get { return originalSheet; }
            set { originalSheet = value; }
        }
     

        private DataSet dataS;
        public DataSet DataS
        {
            get { return dataS; }
            set { dataS = value; }
        }

        //读Excel数据
        public DataSet ReadExcelSource(string path)
        {
            this.OriginaSheet = null;
            this.DataS = null;
            if (ConnectExcel.Instance.Oledconn.State==ConnectionState.Closed)
            {
                ConnectExcel.Instance.DoConnect(path);

            }
            DataTable sheetsName = ConnectExcel.Instance.Oledconn .GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                   new object[] { null, null, null, "Table" });
            OleDbDataAdapter ada2 = new OleDbDataAdapter();

            DataSet set2 = new DataSet();
            //存查询语句
            List<string> sqlList = new List<string> { };
            //获取Excel表中的Sheet名
            string[] sheetsNames = new string[sheetsName.Rows.Count];
            for (int i = 0; i < sheetsName.Rows.Count; i++)
            {
                sheetsNames[i] = sheetsName.Rows[i]["TABLE_NAME"].ToString();
                string str = "SELECT * FROM [" + sheetsNames[i] + "]";
                sqlList.Add(str);
                ada2 = new OleDbDataAdapter(sqlList[i], ConnectExcel.Instance.Oledconn.ConnectionString);
                ada2.Fill(set2, "table" + i);
            }
            //ada2 = new OleDbDataAdapter(sqlList[0], ConnectExcel.Instance.Oledconn.ConnectionString);
            ////填充表1
            //ada2.Fill(set2, "table1");  
            //ada2 = new OleDbDataAdapter(sqlList[1], ConnectExcel.Instance.Oledconn.ConnectionString);           
            //ada2.Fill(set2, "table2");
            //存表名
            this.OriginaSheet = sheetsNames;
            this.DataS = set2;
            return set2;
        }

    }
}
