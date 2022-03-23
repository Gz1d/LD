using System;
using System.Windows.Forms;


namespace FileLib
{
    public static class FileEx
    {
        public static string ErrorMessage = "";

        public static bool Exist(string strFile,bool isShowMsgBox=true)
        {
            try
            {
                if (!System.IO.File.Exists(strFile))
                {
                    if (isShowMsgBox)
                        MessageBox.Show("文件: " + strFile + "不存在!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                ErrorMessage = ex.Message.ToString();
                return false;
            }

            return true;
        }

        public static bool Create(string strFile)
        {
            try
            {
                System.IO.File.Create(strFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                ErrorMessage = ex.Message.ToString();
                return false;
            }

            return true;
        }

        public static bool Copy(string src, string dest)
        {
            try
            {
                System.IO.File.Copy(src, dest);

                Exist(dest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                ErrorMessage = ex.Message.ToString();
                return false;
            }

            return true;
        }
    }
}
