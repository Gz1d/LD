using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using FileLib;


namespace VisionBase
{
    internal class Logger
    {
        public static void Pop(string content, string fileName = null)
        {
            Task.Factory.StartNew(() =>
            {
                FileLib.Logger.Pop(content, false, fileName);
            });
        }

        public static void Pop1(string content, string FileName = null)
        {
            Task.Factory.StartNew(() =>
            {
                if (FileName == null)
                {
                    FileLib.Logger.Pop(content, false, "Vision");
                }
                else
                {
                    FileLib.Logger.Pop(content, false, FileName);
                }
            });
           
           
        }

        public static void PopError(string content,bool showMsgBox=false,string fileName=null)
        {
            //GlobalVariable.IsNoErrorHappened = false;
            Task.Factory.StartNew(() =>
            {
                FileLib.Logger.Pop(content, true, fileName);
            });
            if(showMsgBox)
            {
                Task.Factory.StartNew(() => { MessageBox.Show(content, "报警信息"); });
            }
        }

        public static void PopError1(Exception e1, bool showMsgBox = false, string fileName = null)
        {
            //GlobalVariable.IsNoErrorHappened = false;
            Task.Factory.StartNew(() =>
            {
                FileLib.Logger.Pop(e1.ToString()+e1.StackTrace.ToString(), true, fileName);
            });
            if (showMsgBox)
            {
                Task.Factory.StartNew(() => { MessageBox.Show(e1.ToString() + e1.StackTrace.ToString(), "报警信息"); });
            }
        }
    }
}
