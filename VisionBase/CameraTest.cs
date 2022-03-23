using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace VisionBase
{
    public  class CameraTest
    {
        public const string UpCam1 = "UpCam1";
        public const string UpCam2 = "UpCam2";
        public const string UpCam3 = "UpCam3";
        public const string UpCam4 = "UpCam4";
        public const string UpCam5 = "UpCam5";
        public const string DnCam1 = "DnCam1";
        public const string DnCam2 = "DnCam2";

        private Dictionary<string, ManualResetEventSlim> ImageReadyDictionary = new Dictionary<string, ManualResetEventSlim>();
        private Dictionary<string, object> ImageLockDictionary = new Dictionary<string, object>();
        private Dictionary<string, int> ImageCounterDictionary = new Dictionary<string, int>();
        private Dictionary<string, List<HObject>> ImageObjectListDictionary = new Dictionary<string, List<HObject>>();

        private static HTuple ImageWidthUnder = 2448, ImageHeightUnder = 2048;
        private static CameraTest MyCamera = null;
        private static Object m_obj = new object();
        private static Object lock_Img_Obj = new object();

        private static int ImageTotalNumber=0;
        private int Dn1CCDImageIndex = 0;
        public string CamNameStr = "";


        public static CameraTest Instance
        {
            get
            {
                lock (m_obj)
                {
                    return MyCamera = MyCamera ?? new CameraTest();
                }
            }
        }
        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="Cam1shutter"></param>
        /// <param name="Cam2shutter"></param>
        /// <param name="Cam3shutter"></param>
        /// <param name="Cam4shutter"></param>
        /// <returns></returns>
   
        private void InitVariant()
        {
            ImageReadyDictionary.Add(UpCam1, new ManualResetEventSlim(false));
            ImageReadyDictionary.Add(UpCam2, new ManualResetEventSlim(false));
            ImageReadyDictionary.Add(UpCam3, new ManualResetEventSlim(false));
            ImageReadyDictionary.Add(UpCam4, new ManualResetEventSlim(false));
            ImageReadyDictionary.Add(UpCam5, new ManualResetEventSlim(false));
            ImageReadyDictionary.Add(DnCam1, new ManualResetEventSlim(false));
            ImageReadyDictionary.Add(DnCam2, new ManualResetEventSlim(false));

            ImageLockDictionary.Add(UpCam1, new object());
            ImageLockDictionary.Add(UpCam2, new object());
            ImageLockDictionary.Add(UpCam3, new object());
            ImageLockDictionary.Add(UpCam4, new object());
            ImageLockDictionary.Add(UpCam5, new object());
            ImageLockDictionary.Add(DnCam1, new object());
            ImageLockDictionary.Add(DnCam2, new object());

            ImageCounterDictionary.Add(UpCam1, 0);
            ImageCounterDictionary.Add(UpCam2, 0);
            ImageCounterDictionary.Add(UpCam3, 0);
            ImageCounterDictionary.Add(UpCam4, 0);
            ImageCounterDictionary.Add(UpCam5, 0);
            ImageCounterDictionary.Add(DnCam1, 0);
            ImageCounterDictionary.Add(DnCam2, 0);

            ImageObjectListDictionary.Add(UpCam1, new List<HObject>());
            ImageObjectListDictionary.Add(UpCam2, new List<HObject>());
            ImageObjectListDictionary.Add(UpCam3, new List<HObject>());
            ImageObjectListDictionary.Add(UpCam4, new List<HObject>());
            ImageObjectListDictionary.Add(UpCam5, new List<HObject>());
            ImageObjectListDictionary.Add(DnCam1, new List<HObject>());
            ImageObjectListDictionary.Add(DnCam2, new List<HObject>());

            Thread GCThread = new Thread(new ParameterizedThreadStart((obj) =>
            {
                while (true)
                {
                    if (ImageTotalNumber >= 10)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        ImageTotalNumber = 0;
                    }
                    Thread.Sleep(500);
                }
            }));
            GCThread.IsBackground = true;
            GCThread.Priority = ThreadPriority.Lowest;
            GCThread.Start();
        }

        public string GetCCDName(bool isLeftStation, bool isUpCCD, int feederIndex)
        {
            string str = "";
            if (isLeftStation)
            {
                if (isUpCCD)
                {
                    if (feederIndex == 0) str = UpCam1;
                    else if (feederIndex == 1) str = UpCam2;
                    else if (feederIndex == 2) str = UpCam3;
                    else str = UpCam1;
                }
                else
                {
                    str = DnCam1;
                }
            }
            else
            {
                if (isUpCCD)
                {
                    if (feederIndex == 0 || feederIndex==3) str = UpCam4;
                    else if (feederIndex == 1 || feederIndex==4) str = UpCam5;
                    else str = UpCam4;
                }
                else
                {
                    str = DnCam2;
                }
            }

            return str;
        }

  

  

        /// <summary>
        /// 设置相机的触发源
        /// </summary>
        /// <param name="CamName">相机名</param>
        /// <param name="IsTrig">是否设置触发模式</param>
        /// <param name="TriggerSource">触发源"Software"，</param>
        public void SetTrgModeSource(string CamName, bool IsTrig, string TriggerSource = "All")
        {
        
        }  

        public void SoftTrigger(string CamName)
        {
           
        }

        public void CloseSystem()
        {
          
        }



        void BaumerSDK_ImageGot(string CameraName, HObject img, Bitmap bmp)
        {
            lock (ImageLockDictionary[CameraName])
            {
                try
                {
                    HObject curImage;
                    HOperatorSet.GenEmptyObj(out curImage);
                    HOperatorSet.CopyImage(img, out curImage);
                    if (CameraName == UpCam4 || CameraName == UpCam5)
                        HOperatorSet.MirrorImage(curImage, out curImage, "column");
                    ImageObjectListDictionary[CameraName].Add(curImage);

                    if (IsFlyMotion)// (IsFlyMotion && (CameraName==UpCam2))
                    {
                        string date = DateTime.Now.ToString("yyyy_MM_dd");
                        date = date.Trim();
                        string name = DateTime.Now.Hour + "点" + DateTime.Now.Minute + "分" + DateTime.Now.Second + "秒.bmp";
                        HOperatorSet.WriteImage(curImage, "bmp", 0, "D:\\CameraTest\\" + CameraName + "_Fly_" + name);
                    }
                }
                catch (Exception e) 
                {
                    Logger.PopError1(e);
                    Console.WriteLine("图片采集回调函数异常：" + e.Message.ToString());
                }
            }
            ImageTotalNumber++;
            ImageReadyDictionary[CameraName].Set();
            //Console.WriteLine(CameraName + "回调函数获取图片时间：{0}ms-------------------", SW1.ElapsedMilliseconds);
        }

        public void GrabImge(string CamName, out bool IsTrue, out HObject Img, int TimeOut = 2000)
        {
            ClearImageObjList(CamName);
            SoftTrigger(CamName);
            IsTrue=WaitForImageReady(CamName, out Img, TimeOut);
        }

        private bool IsFlyMotion = false;
        public void ClearImageObjList(string ccdName,bool flying=false)
        {
            if (!ImageReadyDictionary.ContainsKey(ccdName)) return;
            ImageReadyDictionary[ccdName].Reset();

            lock (ImageLockDictionary[ccdName])
            {
                foreach (var item in ImageObjectListDictionary[ccdName])
                {
                    if (item != null) item.Dispose();
                }
                ImageObjectListDictionary[ccdName].Clear();
            }
            IsFlyMotion = flying;
        }

        public bool GetCurrentImgObj(string CamName, out HObject currImg)
        {
            bool isOK = false;
            HOperatorSet.GenEmptyObj(out currImg);
            lock (ImageLockDictionary[CamName])
            {
                if (ImageObjectListDictionary[CamName].Count > 0)
                {
                    HOperatorSet.CopyImage(ImageObjectListDictionary[CamName][0], out currImg);
                    isOK= true;
                }
            }
            return isOK;
        }

        public bool GetCurrentImgObjList(string ccdName, bool isClearOldImg, out List<HObject> currImgList)
        {
            currImgList = new List<HObject>();
            HObject img=null;
            HOperatorSet.GenEmptyObj(out img);

            lock (ImageLockDictionary[ccdName])
            {
                foreach (var item in ImageObjectListDictionary[ccdName])
                {
                    try
                    {
                        if(false)
                        {
                            HOperatorSet.WriteImage(item, "bmp", 0, "D:\\AsyncProcess\\" + (Dn1CCDImageIndex++).ToString() + ".bmp");
                        }
                        HOperatorSet.GenEmptyObj(out img);
                        HOperatorSet.CopyImage(item, out img);
                        currImgList.Add(img);
                    }
                    catch (Exception ex)
                    {
                        Logger.PopError(ex.Message.ToString());
                        throw;
                    }
                }
                if (isClearOldImg)
                {
                    foreach (var item in ImageObjectListDictionary[ccdName])
                    {
                        if (item != null) item.Dispose();
                    }
                    ImageObjectListDictionary[ccdName].Clear();
                }
            }

  
            return currImgList.Count>0;
        }

        public void GetCurrentImgObjListCount(string CamName, out int count)
        {
            count = 0;
            lock (ImageLockDictionary[CamName])
            {
                count=ImageObjectListDictionary[CamName].Count;
            }
        }

        public bool GrabImage(string CamName, bool isWaitReady = false, int milliSecond = 2000)
        {
            ClearImageObjList(CamName);
            SoftTrigger(CamName);

            if (isWaitReady) return ImageReadyDictionary.ContainsKey(CamName) && ImageReadyDictionary[CamName].Wait(milliSecond);
            else return true;
        }

        public bool WaitForImageReady(string ccdName, int milliSecond = 3000)
        {
            bool isWaitOk = true;
            if (!ImageReadyDictionary[ccdName].Wait(milliSecond))
            {
                Logger.PopError("相机" + ccdName + "等待图片Ready失败！");
                isWaitOk = false;
            }

            return isWaitOk;
        }

        public bool WaitForImageReady(string ccdName,out HObject img,int milliSecond=2000)
        {
            bool isWaitOk = false;
            img = new HObject();
            HOperatorSet.GenEmptyObj(out img);

            if(ImageReadyDictionary[ccdName].Wait(milliSecond))
            {
                isWaitOk = GetCurrentImgObj(ccdName, out img);
            }

            if (!isWaitOk)
            {
                Logger.PopError("相机" + ccdName + "等待图片Ready失败！");
            }

            return isWaitOk;
        }
    }
}
