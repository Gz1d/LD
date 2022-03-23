using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace VisionBase
{
    public partial class WordLightForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        SerialPort sp = null;//声明串口类
        bool isOpen = false;//打开串口标志
        bool isSetProperty = false;//属性设置标志
        bool isHex = false;//十六进制显示标志位
        public static  object LockObj = new object();
        public static WordLightForm MyWordLightForm = null;
        public WordLightForm()
        {
            InitializeComponent();
        }

        public static WordLightForm Instance
        {
            get
            {
                lock (LockObj)
                {
                    return MyWordLightForm = MyWordLightForm ?? new WordLightForm();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.MaximizeBox = false;
            for (int i = 0; i < 10; i++)
            {
                cbxCOMPort.Items.Add("COM"+(i+1).ToString());
            }

            Task.Factory.StartNew(new Action(() => {
                Thread.Sleep(1000);
                OpenLight();

            }));
            this.FormClosing += MyFormClosing;
        }

        private void OpenLight()
        {
            this.Invoke(new Action(() => {

                btnOpenCom_Click(null, new EventArgs());

            }));

        }
        /// <summary>
        /// 检测串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckCOM_Click(object sender, EventArgs e)
        {
            bool comExistence = false;
            cbxCOMPort.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    SerialPort sp = new SerialPort("COM"+(i+1).ToString());
                    sp.Open();
                    sp.Close();
                    cbxCOMPort.Items.Add("COM" + (i + 1).ToString());
                    comExistence = true;
                }
                catch (Exception)
                {
                    continue;
                }
                if (comExistence)
                {
                    cbxCOMPort.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("没有找到可用串口！", "错误提示");
                }
            }
        }

        private bool CheckPortSetting()
        {
            if (cbxCOMPort.Text.Trim() == "") return false;
            if (cbxBaudRate.Text.Trim() == "") return false;
            if (cbxDataBits.Text.Trim() == "") return false;
            if (cbxParitv.Text.Trim() == "") return false;
            if (cbxStopBits.Text.Trim() == "") return false;
            return true;

        }
        private bool CheckSendData()
        {
            if (tbxSendData.Text.Trim() == "") return false;
            return true;
        }
        private void SetPortProperty()
        {
            sp = new SerialPort();
            sp.PortName = cbxCOMPort.Text.Trim();
            sp.BaudRate = Convert.ToInt32(cbxBaudRate.Text.Trim());
            float f = Convert.ToSingle(cbxStopBits.Text.Trim());
            //设置停止位对照
            if (f==0)
            {
                sp.StopBits = StopBits.None;
            }
            else if (f==1.5)
            {
                sp.StopBits = StopBits.OnePointFive;
            }
            else if (f==1)
            {
                sp.StopBits = StopBits.One;
            }
            else if (f==2)
            {
                sp.StopBits = StopBits.Two;
            }
            else
            {
                sp.StopBits = StopBits.One;
            }
            sp.DataBits = Convert.ToInt16(cbxDataBits.Text.Trim());//数据位

            string s = cbxParitv.Text.Trim();//校验位
            if (s.CompareTo("无")==0)
            {
                sp.Parity = Parity.None;
            }
            else if (s.CompareTo("奇校验")==0)
            {
                sp.Parity = Parity.Odd;
            }
            else if (s.CompareTo("偶校验")==0)
            {
                sp.Parity = Parity.Even;
            }
            else
            {
                sp.Parity = Parity.None;
            }
            sp.ReadTimeout = -1;
            sp.RtsEnable = true;//超时读取时间

            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            if (rbnHex.Checked)
            {
                isHex = true;
            }
            else
            {
                isHex = false;
            }

          
        }
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            this.Invoke(new Action(() =>
            {
                if (isOpen)
                {
                    //输出当前时间
                    DateTime dt = DateTime.Now;
                    tbxRecvData.Text += dt.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                    tbxRecvData.SelectAll();
                    tbxRecvData.SelectionColor = Color.Blue;         //改变字体的颜色

                    byte[] byteRead = new byte[sp.BytesToRead];    //BytesToRead:sp接收的字符个数
                    if (rbnChar.Checked)                          //'发送字符串'单选按钮
                    {
                        tbxRecvData.Text += sp.ReadLine() + "\r\n"; //注意：回车换行必须这样写，单独使用"\r"和"\n"都不会有效果
                        sp.DiscardInBuffer();                      //清空SerialPort控件的Buffer 
                    }
                    else                                            //'发送16进制按钮'
                    {
                        try
                        {
                            Byte[] receivedData = new Byte[sp.BytesToRead];        //创建接收字节数组
                            sp.Read(receivedData, 0, receivedData.Length);         //读取数据
                                                                                   //string text = sp.Read();   //Encoding.ASCII.GetString(receivedData);
                            sp.DiscardInBuffer();                                  //清空SerialPort控件的Buffer

                            string strRcv = null;
                            //int decNum = 0;//存储十进制
                            for (int i = 0; i < receivedData.Length; i++) //窗体显示
                            {

                                strRcv += receivedData[i].ToString("X2");  //16进制显示
                            }
                            tbxRecvData.Text += strRcv + "\r\n";
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message, "出错提示");
                            tbxSendData.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请打开某个串口", "错误提示");
                }

            }));

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            String s= tbxSendData.Text;
            Write(s);         
        }

        private void btnOpenCom_Click(object sender, EventArgs e)
        {
            if (isOpen==false)
            {
                if (!CheckPortSetting())
                {
                    MessageBox.Show("串口未设置！", "错误提示");
                    return;
                }
                if (!isSetProperty)//串口未设置则设置串口
                {
                    SetPortProperty();
                    isSetProperty = true;
                }
                try
                {
                    sp.Open();
                    isOpen = true;
                    btnOpenCom.Text = "关闭串口";
                    cbxCOMPort.Enabled = false;
                    cbxBaudRate.Enabled = false;
                    cbxDataBits.Enabled = false;
                    cbxParitv.Enabled = false;
                    cbxStopBits.Enabled = false;
                    rbnChar.Enabled = false;
                    rbnHex.Enabled = false;
                }
                catch (Exception)
                {
                    isSetProperty = false;
                    isOpen = false;
                    MessageBox.Show("串口无效或已被占用！", "错误提示");
                    //throw;
                }
            }
            else
            {
                try
                {
                    sp.Close();
                    isOpen = false;
                    btnOpenCom.Text = "打开串口";
                    //关闭串口后，串口设置选项可以继续使用
                    cbxCOMPort.Enabled = true;
                    cbxBaudRate.Enabled = true;
                    cbxDataBits.Enabled = true;
                    cbxParitv.Enabled = true;
                    cbxStopBits.Enabled = true;
                    rbnChar.Enabled = true;
                    rbnHex.Enabled = true;
                }
                catch (Exception)
                {

                    MessageBox.Show("关闭串口时发生错误！", "错误提示");                
                }
            }
        }

        private void btnCleanData_Click(object sender, EventArgs e)
        {
            tbxRecvData.Text = "";
            tbxSendData.Text = "";
        }
        /// <summary>
        /// 打开光源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenLight_Click(object sender, EventArgs e)
        {
            String s = "40 05 01 00 2A 00 01 71";
            Write(s);          
        }

        private void Write(string strSendTxt) 
        {
            if (!isOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }

            String strSend= strSendTxt;
            if (rbnHex.Checked == true)	//“HEX发送” 按钮 
            {
                //处理数字转换
                string sendBuf = strSend;
                string sendnoNull = sendBuf.Trim();
                string sendNOComma = sendnoNull.Replace(',', ' ');    //去掉英文逗号
                string sendNOComma1 = sendNOComma.Replace('，', ' '); //去掉中文逗号
                string strSendNoComma2 = sendNOComma1.Replace("0x", "");   //去掉0x
                strSendNoComma2.Replace("0X", "");   //去掉0X
                string[] strArray = strSendNoComma2.Split(' ');

                int byteBufferLength = strArray.Length;
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i] == "")
                    {
                        byteBufferLength--;
                    }
                }
                // int temp = 0;
                byte[] byteBuffer = new byte[byteBufferLength];
                int ii = 0;
                for (int i = 0; i < strArray.Length; i++)        //对获取的字符做相加运算
                {

                    Byte[] bytesOfStr = Encoding.Default.GetBytes(strArray[i]);

                    int decNum = 0;
                    if (strArray[i] == "")
                    {
                        //ii--;     //加上此句是错误的，下面的continue以延缓了一个ii，不与i同步
                        continue;
                    }
                    else
                    {
                        decNum = Convert.ToInt32(strArray[i], 16); //atrArray[i] == 12时，temp == 18 
                    }

                    try    //防止输错，使其只能输入一个字节的字符
                    {
                        byteBuffer[ii] = Convert.ToByte(decNum);
                    }
                    catch 
                    {
                        MessageBox.Show("字节越界，请逐个字节输入！", "Error");
                        //tmSend.Enabled = false;
                        return;
                    }

                    ii++;
                }
                sp.Write(byteBuffer, 0, byteBuffer.Length);
            }
            else		//以字符串形式发送时 
            {
                sp.WriteLine(tbxSendData.Text);    //写入数据
            }
        }

        private void btnCloseLight_Click(object sender, EventArgs e)
        {
            String s = "40 05 01 00 2A 00 00 70";
            Write(s);
        }

        private void tbaCh1_Scroll(object sender, EventArgs e)
        {
            string m = this.tbaCh1.Value.ToString("X2");
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            string mCode = "40 05 01 00 1A 00" + " " + m + " " + mnnn + mm;
            Write(mCode);
        }

        private void SetLightValue1( int LightVlaue)
        {
            string m = LightVlaue.ToString("X2");
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            string mCode = "40 05 01 00 1A 00" + " " + m + " " + mnnn + mm;
            Write(mCode);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox10.Text);
            
            string m = n.ToString("X2");//转16进制

            string mn = m.Substring(0,1);//取第一位
            int mnn = Convert.ToInt32(mn,16)+6;
            string mnnn = mnn.ToString("X2").Substring(1,1); ;
            
           
            string mm = m.Substring(m.Length-1, 1);//取最后一位

            //string mmm=
            string mCode = "40 05 01 00 1A 00"+" "+m+" "+ mnnn + mm;
            this.textBox9.Text = mCode;
            //Write(mCode);
        }

        private void tbaCh2_Scroll(object sender, EventArgs e)
        {

            //tbaCh2.Value = Convert.ToInt32(textBox6.Text);
            textBox6.Text = tbaCh2.Value.ToString();
            string m = this.tbaCh2.Value.ToString("X2");
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            if (mm == "F")
            {
                mnn = mnn + 1;
                mnnn = mnn.ToString("X2").Substring(1, 1);

            }
            int mm1 = Convert.ToInt32(mm, 16) + 1;
            string mm11 = mm1.ToString("X2").Substring(1, 1);
            string mCode = "40 05 01 00 1A 01" + " " + m + " " + mnnn + mm11;
            this.textBox9.Text = mCode;
            Write(mCode);

        }

        private void SetLightValue2(int LightValue)
        {
            textBox6.Text = tbaCh2.Value.ToString();
            tbaCh2.Value = LightValue;
            string m = LightValue.ToString("X2");
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            if (mm == "F")
            {
                mnn = mnn + 1;
                mnnn = mnn.ToString("X2").Substring(1, 1);

            }
            int mm1 = Convert.ToInt32(mm, 16) + 1;
            string mm11 = mm1.ToString("X2").Substring(1, 1);
            string mCode = "40 05 01 00 1A 01" + " " + m + " " + mnnn + mm11;
            this.textBox9.Text = mCode;
            Write(mCode);
        }

        private void tbaCh3_Scroll(object sender, EventArgs e)
        {
           // tbaCh3.Value = Convert.ToInt32(textBox7.Text);
            string m = this.tbaCh3.Value.ToString("X2");
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            if (mm == "E"| mm == "F")
            {
                mnn = mnn + 1;
                mnnn = mnn.ToString("X2").Substring(1, 1);

            }
            int mm1 = Convert.ToInt32(mm, 16) + 2;
            string mm11 = mm1.ToString("X2").Substring(1, 1);
            string mCode = "40 05 01 00 1A 02" + " " + m + " " + mnnn + mm11;
            this.textBox9.Text = mCode;
            Write(mCode);

        }
        private void SetLightValue3(int LightValue)
        {
            string m = LightValue.ToString("X2");
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            if (mm == "E" | mm == "F")
            {
                mnn = mnn + 1;
                mnnn = mnn.ToString("X2").Substring(1, 1);
            }
            int mm1 = Convert.ToInt32(mm, 16) + 2;
            string mm11 = mm1.ToString("X2").Substring(1, 1);
            string mCode = "40 05 01 00 1A 02" + " " + m + " " + mnnn + mm11;
            this.textBox9.Text = mCode;
            Write(mCode);
        }


        private void tbaCh4_Scroll(object sender, EventArgs e)
        {
            tbaCh4.Value = Convert.ToInt32(textBox8.Text);
            string m = this.tbaCh4.Value.ToString("X2");
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            if (mm == "D" | mm == "E" | mm == "F")
            {
                mnn = mnn + 1;
                mnnn = mnn.ToString("X2").Substring(1, 1);
            }
            int mm1 = Convert.ToInt32(mm, 16) + 3;
            string mm11 = mm1.ToString("X2").Substring(1, 1);


            string mCode = "40 05 01 00 1A 03" + " " + m + " " + mnnn + mm11;
            this.textBox9.Text = mCode;
            Write(mCode);
        }

        private void SetLightValue4(int LightValue)
        {
            string m = LightValue.ToString("X2");
            textBox8.Text = LightValue.ToString();
            tbaCh4.Value = LightValue;
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            if (mm == "D" | mm == "E" | mm == "F")
            {
                mnn = mnn + 1;
                mnnn = mnn.ToString("X2").Substring(1, 1);

            }
            int mm1 = Convert.ToInt32(mm, 16) + 3;
            string mm11 = mm1.ToString("X2").Substring(1, 1);

            string mCode = "40 05 01 00 1A 03" + " " + m + " " + mnnn + mm11;
            this.textBox9.Text = mCode;
            Write(mCode);
        }

        private void btn_OpenCh1_Click(object sender, EventArgs e)
        {
            string mCode;
            if (btn_OpenCh1.Text == "开")
            {
                mCode = "40 05 01 00 2A 00 01 71";//开
                btn_OpenCh1.Text = "关";
            }
            else
            {
                mCode = "40 05 01 00 2A 00 00 70";//关
                btn_OpenCh1.Text = "开";

            }
            Write(mCode);
        }

        private void btn_OpenCh2_Click(object sender, EventArgs e)
        {
            string mCode;
            if (btn_OpenCh2.Text == "开")
            {
                mCode = "40 05 01 00 2A 01 01 72";//开
                btn_OpenCh2.Text = "关";
            }
            else
            {
                mCode = "40 05 01 00 2A 01 00 71";//关
                btn_OpenCh2.Text = "开";

            }
            Write(mCode);
        }

        private void btn_OpenCh3_Click(object sender, EventArgs e)
        {
            string mCode;
            if (btn_OpenCh3.Text == "开")
            {
                mCode = "40 05 01 00 2A 02 01 73";//开
                btn_OpenCh3.Text = "关";
            }
            else
            {
                mCode = "40 05 01 00 2A 02 00 72";//关
                btn_OpenCh3.Text = "开";

            }
            Write(mCode);
        }

        private void btn_OpenCh4_Click(object sender, EventArgs e)
        {
            string mCode;
            if (btn_OpenCh4.Text == "开")
            {
                mCode = "40 05 01 00 2A 03 01 74";//开
                btn_OpenCh4.Text = "关";
            }
            else
            {
                mCode = "40 05 01 00 2A 03 00 73";//关
                btn_OpenCh4.Text = "开";
            }
            Write(mCode);
        }


        public void ConnectLigt()
        {
            btnOpenCom_Click(null, new EventArgs());
        }
        /// <summary>
        /// 打开当前通道的光源
        /// </summary>
        /// <param name="panel">光源通道，1,2,3,4</param>
        public void OpenLightPanel( int panel  )
        {
            string mCode;
            this.Invoke(new Action(() => {
                switch (panel)
                {
                    case 1:
                        if (btn_OpenCh1.Text == "开")
                        {
                            mCode = "40 05 01 00 2A 00 01 71";//开
                            this.Invoke(new Action(() => {
                                btn_OpenCh1.Text = "关";
                            }));

                            Write(mCode);
                        }
                        break;
                    case 2:
                        if (btn_OpenCh2.Text == "开")
                        {
                            mCode = "40 05 01 00 2A 01 01 72";//开
                            this.Invoke(new Action(() => {
                                btn_OpenCh2.Text = "关";
                            }));

                            Write(mCode);
                        }
                        break;
                    case 3:
                        if (btn_OpenCh3.Text == "开")
                        {
                            mCode = "40 05 01 00 2A 02 01 73";//开
                            this.Invoke(new Action(() => {
                                btn_OpenCh3.Text = "关";
                            }));

                            Write(mCode);
                        }
                        break;
                    case 4:
                        if (btn_OpenCh4.Text == "开")
                        {
                            mCode = "40 05 01 00 2A 03 01 74";//开
                            this.Invoke(new Action(() => {
                                btn_OpenCh4.Text = "关";
                            }));

                            Write(mCode);
                        }
                        break;
                }


            }));


        }

        /// <summary>
        /// 关闭当前通道的光源
        /// </summary>
        /// <param name="panel">光源的通道,1,2,3,4</param>
        public void CloseLightPanel(int panel)
        {
            string mCode;
            this.Invoke(new Action(() => {
                switch (panel)
                {
                    case 1:
                        if (btn_OpenCh1.Text == "关")
                        {
                            mCode = "40 05 01 00 2A 00 00 70";//关
                            btn_OpenCh1.Text = "开";
                            Write(mCode);
                        }
                        break;
                    case 2:
                        if (btn_OpenCh2.Text == "关")
                        {
                            mCode = "40 05 01 00 2A 00 00 71";//关
                            btn_OpenCh2.Text = "开";
                            Write(mCode);
                        }
                        break;
                    case 3:
                        if (btn_OpenCh3.Text == "关")
                        {
                            mCode = "40 05 01 00 2A 00 00 72";//关
                            btn_OpenCh3.Text = "开";
                            Write(mCode);
                        }
                        break;
                    case 4:
                        if (btn_OpenCh4.Text == "关")
                        {
                            mCode = "40 05 01 00 2A 00 00 73";//关
                            btn_OpenCh3.Text = "开";
                            Write(mCode);
                        }
                        break;
                }
            }));
  
        }



        public void CloseAllLight()
        {
            CloseLightPanel(1);
            CloseLightPanel(2);
            CloseLightPanel(3);
            CloseLightPanel(4);
        }

        /// <summary>
        /// 设置光源通道的亮度值
        /// </summary>
        /// <param name="LightValue">当前的通道的亮度值</param>
        /// <param name="Panel">通道编号，建议1,2,3,4</param>
        public void SetLightValue(int  LightValue =0 ,int Panel =1)
        {
            this.Invoke(new Action(() => {

                switch (Panel)
                {
                    case 1:
                        SetLightValue1(LightValue);
                        break;
                    case 2:
                        SetLightValue2(LightValue);
                        break;
                    case 3:
                        SetLightValue3(LightValue);
                        break;
                    case 4:
                        SetLightValue4(LightValue);
                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                }
            }));

        }

        private void MyFormClosing(object sender, FormClosingEventArgs e)
        {
            //Dialog MyDlg = 
            DialogResult DlgReslult = MessageBox.Show("是否关闭程序", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DlgReslult == DialogResult.No) e.Cancel = true;
        }

        private void rbnHex_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxParitv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
