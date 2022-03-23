using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

//using System.Linq;

namespace FileLib
{
    public class StreamFile
    {
        public string FilePath { get; set; }

        bool fileExist = false;
        static System.Object obj = new Object();

        public StreamFile(string strPath, bool addTime)
        {
            try
            {
                if (!File.Exists(strPath))
                {
                    using (StreamWriter sw = File.CreateText(strPath))
                    {

                    }
                }
            }
            catch (System.Exception ex)
            {
                string str = ex.Message.ToString();
                str = "Txt文件初始化失败!错误代码:" + str;
                MessageBox.Show(str);
                return;
            }

            FilePath = strPath;
            fileExist = true;
        }

        public void AppendText(string str, bool recordDate=true)
        {
            if (!fileExist) return;
            //if (!CLicense.LicenseOK) return;

            lock (obj)
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        string strTxt = "";
                        if (recordDate)
                            strTxt = DateTime.Now.ToString() + "   ";

                        strTxt = strTxt + str;

                        sw.WriteLine(strTxt);
                    }
                }
                catch (System.Exception)
                {

                }
            }
        }

        public void Save(string str, bool recordDate = true)
        {
            if (!fileExist) return;
            //if (!CLicense.LicenseOK) return;

            lock (obj)
            {
                try
                {
                    File.WriteAllText(FilePath, str);
                }
                catch (System.Exception)
                {

                }
            }
        }

        public void ReadText(out List<string> strList)
        {
            strList = new List<string>();

            if (!fileExist) return;

            lock (obj)
            {
                using (StreamReader sr = File.OpenText(FilePath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        s.Trim();
                        strList.Add(s);
                    }
                }
            }
        }

        public int Read(out byte[] array, int offset, int count)
        {
            array = new byte[count];
            int length = 0;

            lock (obj)
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    length = fs.Read(array, offset, count);
                }
            }

            return length;
        }

        public int Save(ref byte[] array, int offset, int count)
        {
            int length = 0;

            lock (obj)
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(array, offset, count);
                }
            }

            return length;
        }

        public bool DeleteText(string txt)
        {
            lock (obj)
            {
                try
                {
                    List<string> lines = new List<string>(File.ReadAllLines(FilePath));
                    lines.Remove(txt);
                    File.WriteAllLines(FilePath, lines.ToArray());
                }
                catch (System.Exception ex)
                {
                    string str = ex.Message.ToString();
                    str = "删除文本失败！错误代码：" + str;
                    MessageBox.Show(str);
                    return false;
                }
            }

            return true;
        }
    }
}
