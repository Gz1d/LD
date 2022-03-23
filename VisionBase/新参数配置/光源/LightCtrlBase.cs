using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionBase
{
    public class LightCtrlBase
    {

        public LightCrlParaItem lightCtrlParaItem;
        public System.IO.Ports.SerialPort port;
        public LightPanelEnum[] Panels;

        public  virtual void SetPara (LightCrlParaItem lightCtrlParaIn)
        {
            this.lightCtrlParaItem = lightCtrlParaIn;  
        }
        /// <summary>
        /// 初始化串口
        /// </summary>
        public virtual void DoInit()
        {
            port = new System.IO.Ports.SerialPort();
            if (lightCtrlParaItem != null){
                port.PortName = lightCtrlParaItem.PortName.ToString();
                port.BaudRate = lightCtrlParaItem.BaudRate;
                port.StopBits = lightCtrlParaItem.StopBits;
                port.Parity = lightCtrlParaItem.Parity;
                port.DataBits = lightCtrlParaItem.DataBits;
                lightCtrlParaItem.Tag = port;
            }     
        
        }

        /// <summary>
        /// 打开，并连接串口
        /// </summary>
        public virtual void DoStart()
        {
            try {
                if (lightCtrlParaItem.IsActive) {
                    port.Open(); //
                                 //正常打开，设置IsConnect =true ;   
                    if (port.IsOpen) {
                        lightCtrlParaItem.IsConnect = true;
                        LightCrlParaItem item = LightCrlParaManager.Instance.GetParaItem(lightCtrlParaItem);
                        item.IsConnect = true;
                    }
                    else {
                        lightCtrlParaItem.IsConnect = false;
                        LightCrlParaItem item = LightCrlParaManager.Instance.GetParaItem(lightCtrlParaItem);
                        item.IsConnect = false;
                    }
                }                 
            }
            catch  {
                lightCtrlParaItem.IsConnect = false;
                LightCrlParaItem item = LightCrlParaManager.Instance.GetParaItem(lightCtrlParaItem);
                item.IsConnect = false;
            }                 
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public virtual void DoStop()
        {
            try{
                port.Close();
                LightCrlParaItem item = LightCrlParaManager.Instance.GetParaItem(lightCtrlParaItem);
                item.IsConnect = false;
            }
            catch
            { }
        }
        /// <summary>
        /// 释放串口
        /// </summary>
        public virtual void DoRelease()
        {
            try{
                port.Dispose();          
            }
            catch
            { }         
        }
        /// <summary>
        /// 数据写入串口
        /// </summary>
        /// <param name="strSendTxt"></param>
        public virtual void Write(string strSendTxt)
        {
            if (port.IsOpen) return;
            String strSend = strSendTxt;
            if (lightCtrlParaItem.IsHex) //16进制发送
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
                for (int i = 0; i < strArray.Length; i++)  {
                    if (strArray[i] == "") {
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
                    if (strArray[i] == ""){
                        //ii--;     //加上此句是错误的，下面的continue以延缓了一个ii，不与i同步
                        continue;
                    }
                    else{
                        decNum = Convert.ToInt32(strArray[i], 16); //atrArray[i] == 12时，temp == 18 
                    }
                    try    //防止输错，使其只能输入一个字节的字符
                    {
                        byteBuffer[ii] = Convert.ToByte(decNum);
                    }
                    catch 
                    {
                        //MessageBox.Show("字节越界，请逐个字节输入！", "Error");
                        //tmSend.Enabled = false;
                        return;
                    }
                    ii++;
                }
                port.Write(byteBuffer, 0, byteBuffer.Length);
            }
            else
            {
                port.WriteLine(strSendTxt);            
            }
        
        }
        /// <summary>
        /// 设置光源的亮度值
        /// </summary>
        /// <param name="Channel">通道编号</param>
        /// <param name="Value">亮度值</param>
        public virtual void SetLightValue(int Channel, int Value)
        {
        
        
        }
        /// <summary>
        /// 打开光源通道
        /// </summary>
        /// <param name="Channel">通道号</param>
        public virtual void OpenLightChannel(int Channel)
        { }

        public virtual void OpenAllChannel()
        {
            if (Panels != null) {
                foreach (  LightPanelEnum item in Panels){
                    if (lightCtrlParaItem.IsConnect) OpenLightChannel((int)item);                           
                }
            }       
        }

        /// <summary>
        /// 关闭光源通道
        /// </summary>
        /// <param name="Channel">通道号</param>
        public virtual void CloseLightChannel(int Channel)
        { }
        public virtual void CloseAllChannel()
        {
            if (Panels != null) {
                foreach (LightCtrlEmun item in Panels) {
                    if (lightCtrlParaItem.IsConnect) CloseLightChannel((int)item);
                }                
            }    
        }





    }
}
