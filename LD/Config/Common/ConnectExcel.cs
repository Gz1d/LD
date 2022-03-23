using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace LD
{
    //连接Excel类
    public class ConnectExcel
    {
        private static ConnectExcel instance = new ConnectExcel();

        private ConnectExcel()
        {
        
        }

        public static ConnectExcel Instance
        {
            get { return instance; }
        }
      

        private static OleDbConnection oledconn = new OleDbConnection();
        
        public OleDbConnection Oledconn
        {
            get { return oledconn; }
            
        }

        //执行连接
        public void DoConnect(string path)
        {
            string connstring = null;

            if (path.Contains("xlsx"))   //Excel 2007-2016
            {
                //连接字符串，HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；IMEX=1 把读入数据作为字符
                connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path
                    + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";
            }
            else  //Excel 2007以下
            {
                //连接字符串，HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
                connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path
                    + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";
            }
            this.Oledconn.ConnectionString = connstring;
            this.Oledconn.Open();
        }
    }
}
