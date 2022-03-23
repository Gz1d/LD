using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALSA.SaperaLT.SapClassBasic;
using HalconDotNet;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace VisionBase
{
    public  class CameraCLDalsa:CameraBase
    {
        #region /* Sapera SDK 实例化对象*/
        /// <summary>sapera sdk</summary>
        public SapAcquisition m_Acquisition = null; //设备的连接地址
        public SapBuffer m_Buffers = null;          //缓存对象
        public SapTransfer m_Xfer = null;           //传输对象
        public SapAcqDevice m_AcqDevice = null;     //采集设备
        /// <summary>  图像采集卡是否发现 </summary>
        bool bServerFound = false;
        /// <summary> 是否发现相机 </summary>
        bool CameraIsFound = false;
        public static int m_FrmaeCount = -1;
        public int m_RingBufCount = 25;
        public int FrameImgWid, FrameImgHei;

        #endregion
        public override bool DoInit()
        {
            int DalsaCardCount = SapManager.GetServerCount(); //获取图像采集卡的数量
            for (int i = 0; i < DalsaCardCount; i++){
                bool bAcq = false;
                bool bAcqDevice = false;
                if (SapManager.GetResourceCount(i, SapManager.ResourceType.Acq) > 0)    bAcq = true; //卡的数量大于0
                if (SapManager.GetResourceCount(i,SapManager.ResourceType.AcqDevice)>0) bAcqDevice = true;//相机数量大于0
                if (bAcq)  {
                    string ServerName = SapManager.GetServerName(i);
                    if (this.myCamPara.ServerName == ServerName)
                    {
                        bServerFound = true; //发现图像采集卡
                        string DeviceName = SapManager.GetResourceName(ServerName, SapManager.ResourceType.Acq, 0);
                        if (this.myCamPara.DeviceName != DeviceName)
                        {
                            Logger.PopError("采集卡上找到的相机名字和campara里的名字不同");
                            return false;
                        }
                    }
                }
                else if (bAcqDevice) //没有采集卡，相机直接传给电脑
                {
                    CameraIsFound = true;
                    string serverName = SapManager.GetServerName(i);                
                }
            }
            if (!bServerFound && !CameraIsFound)  //至少需要一张采集卡，或者相机装置
            {
                m_Buffers = new SapBuffer();
                return false;
            }
            else{
                SapLocation location = new SapLocation(this.myCamPara.ServerName, 0);
                if (SapManager.GetResourceCount(this.myCamPara.ServerName, SapManager.ResourceType.Acq) > 0){
                    m_Acquisition = new SapAcquisition(location, System.Windows.Forms.Application.StartupPath+ "\\ccf\\"+this.myCamPara.CcfPath + ".ccf");
                    //m_AcqDevice = new SapAcqDevice(location, System.Windows.Forms.Application.StartupPath + "\\ccf\\" + this.myCamPara.CcfPath + ".ccf");                 
                    if (SapBuffer.IsBufferTypeSupported(location, SapBuffer.MemoryType.ScatterGather))
                        m_Buffers = new SapBuffer(m_RingBufCount, m_Acquisition, SapBuffer.MemoryType.ScatterGather); //buffer里有10段内存，用来循环存储从相机采集的图片
                    else
                        m_Buffers = new SapBufferWithTrash(m_RingBufCount, m_Acquisition, SapBuffer.MemoryType.ScatterGatherPhysical);
                    m_Xfer = new SapAcqToBuf(m_Acquisition, m_Buffers);
                    m_Xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
                    m_Xfer.XferNotify += new SapXferNotifyHandler(AcqCallback1);
                    m_Xfer.XferNotifyContext = this;
                    // event for signal status
                    if (!SeparaInterface_CreateObjects()){                     
                        Logger.PopError(" 创建 相关的采集、传输、缓存对象失败");
                        this.SeparaInterface_DisposeObjects();
                        return false;
                    }
                    this. FrameImgHei = this.SeparaInterface_GetImageHeight();
                    this.FrameImgWid = this.SeparaInterface_GetImageWidth();             
                    return true;
                }
            }                     
            return false;
        }

        int SaveFrames = 10;
        public override bool GrabImg(out HObject ImgOut,int TimeOut =100000,int Rows = 10000)
        {            
            ImgOut = new HObject();
            int FrameNo = 0;
            this.SaveFrames = Rows / this.FrameImgHei;
            m_FrmaeCount = -1;
            this.SaperaInterface_GrabContinue(); //开始取像
            if (this.FrameImgWid <= 0 || this.FrameImgHei <= 0 || SaveFrames <= 0){
                //MessageBox.Show("儲存參數錯誤!");
               this.SaperaInterface_GrabStop();//停止取像
                return false;
            }
            byte[] pSaveData = new byte[this.FrameImgWid * this.FrameImgHei * SaveFrames];  //图片的缓存
            int TimeCount = 0;
            while (FrameNo < SaveFrames){
                if (FrameNo <= m_FrmaeCount){
                    IntPtr pImgBufAddress = IntPtr.Zero;  //指针变量
                    this.SeparaInterface_GetBufferAddress(FrameNo % m_RingBufCount, out pImgBufAddress); //获取图片的指针
                    Marshal.Copy(pImgBufAddress, pSaveData, FrameNo * this.FrameImgWid * this.FrameImgHei, this.FrameImgWid * this.FrameImgHei); //将图片片段放到缓存中
                    FrameNo++;
                }
                System.Threading.Thread.Sleep(10);
                TimeCount = TimeCount + 10;
                if (TimeCount > TimeOut){
                    this.SaperaInterface_GrabStop();//停止取像
                    return false;
                }
            }
            this.SaperaInterface_GrabStop();//停止取像        
            Bitmap pBitmap = new Bitmap(this.FrameImgWid, this.FrameImgHei*this.SaveFrames, PixelFormat.Format8bppIndexed);  //创建BMP图像缓存c
            BitmapData pBitmapData = pBitmap.LockBits(new Rectangle(0, 0, this.FrameImgWid, this.FrameImgHei*this.SaveFrames), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            Marshal.Copy(pSaveData, 0, pBitmapData.Scan0, this.FrameImgWid * this.FrameImgHei*this.SaveFrames);  //将图片信息从pSaveData拷贝到pBitmapData
            pBitmap.UnlockBits(pBitmapData);    //将图片信息从pBitmapData拷贝到pBitmap       
            MyVisionBase.BitMapToHobject8(pBitmap, out ImgOut);
            this.myCamPara.IsRot = true;
            this.AdjImg(ImgOut, out ImgOut);
            return true;
        }

        /// <summary>
        /// 分时频闪，分割图片
        /// </summary>
        /// <param name="LightImgOut">明场图片</param>
        /// <param name="DarkImgOut">暗场图片</param>
        /// <param name="TimeOut"></param>
        /// <returns></returns>
        public override bool GrabImg(out HObject LightImgOut, out HObject DarkImgOut, int TimeOut = 10000, int Rows = 10000)
        {
            LightImgOut = new HObject();
            DarkImgOut = new HObject();
            this.SaveFrames = Rows / this.FrameImgHei;
            int FrameNo = 0;
            m_FrmaeCount = -1;
            this.SaperaInterface_GrabContinue(); //开始取像
            if (this.FrameImgWid <= 0 || this.FrameImgHei <= 0 || SaveFrames <= 0) {
                //MessageBox.Show("儲存參數錯誤!");
                this.SaperaInterface_GrabStop();//停止取像
                return false;
            }
            byte[] pSaveData = new byte[this.FrameImgWid * this.FrameImgHei * SaveFrames];  //图片的缓存
            byte[] PSaveDataLight = new byte[this.FrameImgWid * this.FrameImgHei * SaveFrames / 2]; //明场的图片缓存
            byte[] pSaveDataDark = new byte[this.FrameImgWid * this.FrameImgHei * SaveFrames / 2];  //暗场的图片缓存
            byte[] pSaveDataFrameNo = new byte[this.FrameImgWid * this.FrameImgHei];// 当前帧的图片数据缓存
            int TimeCount = 0;
            IntPtr RowPtr = Marshal.AllocHGlobal(this.FrameImgWid);//行指针
            while (FrameNo < SaveFrames) {
                if (FrameNo <= m_FrmaeCount){
                    IntPtr pImgBufAddress = IntPtr.Zero;  //指针变量
                    this.SeparaInterface_GetBufferAddress(FrameNo % m_RingBufCount, out pImgBufAddress); //获取图片的指针
                    Marshal.Copy(pImgBufAddress, pSaveData, FrameNo * this.FrameImgWid * this.FrameImgHei, this.FrameImgWid * this.FrameImgHei); //将图片片段放到缓存中
                    Marshal.Copy(pImgBufAddress, pSaveDataFrameNo, 0, this.FrameImgWid * this.FrameImgHei); //将当前帧的图片数据存在当前图片的数组中
                    for (int i=0;i<this.FrameImgHei;i=i+2)
                    {
                        int SatrtNo = FrameNo * this.FrameImgWid * this.FrameImgHei / 2 + i / 2 * this.FrameImgWid;
                        Marshal.Copy(pSaveDataFrameNo, i * this.FrameImgWid, RowPtr, this.FrameImgWid);     //奇数行的图像指针
                        Marshal.Copy(RowPtr, PSaveDataLight, SatrtNo, this.FrameImgWid);
                        Marshal.Copy(pSaveDataFrameNo, (i+1)* this.FrameImgWid , RowPtr, this.FrameImgWid);  //偶数行的图像指针
                        Marshal.Copy(RowPtr, pSaveDataDark, SatrtNo, this.FrameImgWid);                        
                    }
                    FrameNo++;
                }
                System.Threading.Thread.Sleep(10);
                TimeCount = TimeCount + 10;
                if (TimeCount > TimeOut){
                    this.SaperaInterface_GrabStop();//停止取像
                    return false;
                }
            }
            this.SaperaInterface_GrabStop();//停止取像

            Bitmap pBitmap = new Bitmap(this.FrameImgWid, this.FrameImgHei * this.SaveFrames/2, PixelFormat.Format8bppIndexed);  //创建BMP图像缓存c
            BitmapData pBitmapData = pBitmap.LockBits(new Rectangle(0, 0, this.FrameImgWid, this.FrameImgHei * this.SaveFrames/2),
                                                       ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            Marshal.Copy(PSaveDataLight, 0, pBitmapData.Scan0, this.FrameImgWid * this.FrameImgHei * this.SaveFrames/2);  //将图片信息从pSaveData拷贝到pBitmapData
            pBitmap.UnlockBits(pBitmapData);    //将图片信息从pBitmapData拷贝到pBitmap          
            MyVisionBase.BitMapToHobject8(pBitmap, out LightImgOut);
            this.AdjImg(LightImgOut, out LightImgOut);

            Bitmap pBitmap1 = new Bitmap(this.FrameImgWid, this.FrameImgHei * this.SaveFrames / 2, PixelFormat.Format8bppIndexed);  //创建BMP图像缓存c
            BitmapData pBitmapData1 = pBitmap1.LockBits(new Rectangle(0, 0, this.FrameImgWid, this.FrameImgHei * this.SaveFrames / 2),
                                                       ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            Marshal.Copy(pSaveDataDark, 0, pBitmapData1.Scan0, this.FrameImgWid * this.FrameImgHei * this.SaveFrames / 2);  //将图片信息从pSaveData拷贝到pBitmapData
            pBitmap1.UnlockBits(pBitmapData1);    //将图片信息从pBitmapData拷贝到pBitmap       
            MyVisionBase.BitMapToHobject8(pBitmap1, out DarkImgOut);
            this.AdjImg(DarkImgOut, out DarkImgOut);

            Bitmap pBitmap2 = new Bitmap(this.FrameImgWid, this.FrameImgHei * this.SaveFrames , PixelFormat.Format8bppIndexed);  //创建BMP图像缓存c
            BitmapData pBitmapData2 = pBitmap2.LockBits(new Rectangle(0, 0, this.FrameImgWid, this.FrameImgHei * this.SaveFrames),
                                                       ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            Marshal.Copy(pSaveData, 0, pBitmapData2.Scan0, this.FrameImgWid * this.FrameImgHei * this.SaveFrames);  //将图片信息从pSaveData拷贝到pBitmapData
            pBitmap2.UnlockBits(pBitmapData2);    //将图片信息从pBitmapData拷贝到pBitmap  

           // pBitmap2.Save(@"E:\1.bmp");
            return true;
        }

        public override bool ResetCamera(string CamParaFileName)
        {
            this.CloseCamera();
            int DalsaCardCount = SapManager.GetServerCount(); //获取图像采集卡的数量
            for (int i = 0; i < DalsaCardCount; i++) {
                bool bAcq = false;
                bool bAcqDevice = false;
                if (SapManager.GetResourceCount(i, SapManager.ResourceType.Acq) > 0)   bAcq = true; //卡的数量大于0
                if (SapManager.GetResourceCount(i, SapManager.ResourceType.AcqDevice) > 0) bAcqDevice = true; //相机数量大于0
                if (bAcq)  {
                    string ServerName = SapManager.GetServerName(i);
                    if (this.myCamPara.ServerName == ServerName)  {
                        bServerFound = true; //发现图像采集卡
                        string DeviceName = SapManager.GetResourceName(ServerName, SapManager.ResourceType.Acq, 0);
                        if (this.myCamPara.DeviceName != DeviceName)  {
                            Logger.PopError("采集卡上找到的相机名字和campara里的名字不同");
                            return false;
                        }
                    }
                }
                else if (bAcqDevice) //没有采集卡，相机直接传给电脑
                {
                    CameraIsFound = true;
                    string serverName = SapManager.GetServerName(i);
                }
            }
            if (!bServerFound && !CameraIsFound)  //至少需要一张采集卡，或者相机装置
            {
                m_Buffers = new SapBuffer();
                return false;
            }
            else
            {
                SapLocation location = new SapLocation(this.myCamPara.ServerName, 0);
                if (SapManager.GetResourceCount(this.myCamPara.ServerName, SapManager.ResourceType.Acq) > 0) {
                    m_Acquisition = new SapAcquisition(location, System.Windows.Forms.Application.StartupPath + "\\ccf\\" + CamParaFileName + ".ccf");
                    if (SapBuffer.IsBufferTypeSupported(location, SapBuffer.MemoryType.ScatterGather))
                        m_Buffers = new SapBuffer(m_RingBufCount, m_Acquisition, SapBuffer.MemoryType.ScatterGather); //buffer里有10段内存，用来循环存储从相机采集的图片
                    else
                        m_Buffers = new SapBufferWithTrash(m_RingBufCount, m_Acquisition, SapBuffer.MemoryType.ScatterGatherPhysical);
                    m_Xfer = new SapAcqToBuf(m_Acquisition, m_Buffers);
                    m_Xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
                    m_Xfer.XferNotify += new SapXferNotifyHandler(AcqCallback1);
                    m_Xfer.XferNotifyContext = this;
                    if (!SeparaInterface_CreateObjects()){
                        Logger.PopError(" 创建 相关的采集、传输、缓存对象失败");
                        this.SeparaInterface_DisposeObjects();
                        return false;
                    }
                    this.FrameImgHei = this.SeparaInterface_GetImageHeight();
                    this.FrameImgWid = this.SeparaInterface_GetImageWidth();                
                    return true;
                }
            }
            return false;
        }


        public override bool GrabImgSync(HObject ImgOut, int TimeOut = 5000)
        {
            return base.GrabImgSync(ImgOut, TimeOut);
        }


        public override bool CloseCamera()
        {
            m_Xfer.XferNotify -= new SapXferNotifyHandler(AcqCallback1);
            this.SeparaInterface_DestroyObjects();
            this.SeparaInterface_DisposeObjects();           
            return true;
        }


        private void AcqCallback1(object sender, SapXferNotifyEventArgs argsNotify)
        {
            m_FrmaeCount++;  
        }

        /// <summary>
        /// 重置采集图像计数
        /// </summary>
        public void SeparaInterface_ResetFrameNum()
        {
            m_FrmaeCount = -1;
            if (m_Buffers != null)
                m_Buffers.ResetIndex();

        }

        public bool  SaperaInterface_GrabContinue()
        {
            string Str = string.Empty;
            if (m_Xfer != null && m_Xfer.Initialized) {
                SeparaInterface_ResetFrameNum(); //重置缓存和帧计数
                if (m_Xfer.Grabbing){
                    Logger.PopError(" SaperaCenter_Error_Grabing");
                    return false;
                }
                if (!m_Xfer.Grab()){
                    Logger.PopError("SaperaCenter_Error_GrabFailed ,相机采图失败");
                }
                return true;
            }
            return false;
        }

        public void SaperaInterface_GrabStop()
        {
            string Str = string.Empty;
            if (m_Xfer != null && m_Xfer.Initialized){
                //Form_GrabAbort Abort = new Form_GrabAbort(m_Xfer);
                if (m_Xfer.Freeze()){
                    m_Xfer.Abort();
                }
                else{
                    //Str = Global._localResource.GetString("SaperaCenter_Error_StopGrabFailed");
                    //Global.g_Form_EventLog.Form_EventLog_AddErrorMsg(Str, true);
                    //Global.LogErrorFun(Str);
                }
            }
        }

        /// <summary>
        /// 创建 相关的采集、传输、缓存对象
        /// </summary>
        /// <returns></returns>
        private bool SeparaInterface_CreateObjects()
        {
            // Create acquisition object
            if (m_Acquisition != null && !m_Acquisition.Initialized){
                if (m_Acquisition.Create() == false){
                    SeparaInterface_DestroyObjects();
                    return false;
                }
            }
            // Create buffer object
            if (m_Buffers != null && !m_Buffers.Initialized) {
                if (m_Buffers.Create() == false) {
                    SeparaInterface_DestroyObjects();
                    return false;
                }
                m_Buffers.Clear();
            }
            // Create Xfer object
            if (m_Xfer != null && !m_Xfer.Initialized){
                if (m_Xfer.Create() == false) {
                    SeparaInterface_DestroyObjects();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 销毁 采集、传输、缓存对象
        /// </summary>
        public void SeparaInterface_DestroyObjects()
        {
            if (m_Xfer != null && m_Xfer.Initialized)   m_Xfer.Destroy();
            if (m_Buffers != null && m_Buffers.Initialized)  m_Buffers.Destroy();
            if (m_Acquisition != null && m_Acquisition.Initialized) m_Acquisition.Destroy();
        }

        /// <summary>
        /// 清空 采集、传输、缓存对象
        /// </summary>
        public void SeparaInterface_DisposeObjects()
        {
            if (m_Xfer != null)
            { m_Xfer.Dispose(); m_Xfer = null; }
            if (m_Buffers != null)
            { m_Buffers.Dispose(); m_Buffers = null; }
            if (m_Acquisition != null)
            { m_Acquisition.Dispose(); m_Acquisition = null; }
        }

        public int SeparaInterface_GetImageWidth()
        {
            if (m_Buffers != null)  return m_Buffers.Width;
            return 0;
        }

        public int SeparaInterface_GetImageHeight()
        {
            if (m_Buffers != null)  return m_Buffers.Height;
            return 0;
        }

        public int SeparaInterface_GetBytesPerPixel()
        {
            if (m_Buffers != null) return m_Buffers.BytesPerPixel;
            return 0;
        }
        public int SeparaInterface_GetImageIndex()
        {
            if (m_Buffers != null) return m_Buffers.Index;
            return 0;
        }

        public void SeparaInterface_GetBufferAddress(int Index, out IntPtr pImgBufAddress){
            pImgBufAddress = IntPtr.Zero;
            if (m_Buffers != null)  m_Buffers.GetAddress(Index, out pImgBufAddress);
        }
    }
}
