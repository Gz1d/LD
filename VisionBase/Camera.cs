using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;

namespace VisionBase
{
    public class Camera
    {
        public string Cam1Name = "Cam1";
        public string Cam2Name = "Cam2";
        private static  Camera MyCamera = null;
        private static object LockObj =new object();
        private HTuple AcqHandle1;
        private HTuple AcqHandle2;
        public bool Cam1IsOpen = false;
        public bool Cam2IsOpen = false;
        public static Camera Instance
        {
            get           
            {
                lock (LockObj)
                {
                    return  MyCamera = MyCamera ?? new Camera();
                }                  
          
            }
       
        }
        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public bool  DoInit()
        {
            bool IsOk = true;
            try
            {
                HOperatorSet.CloseAllFramegrabbers();
                HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", Cam1Name, 0, -1, out AcqHandle1);
            }
            catch (Exception e0)
            {
                MessageBox.Show("打开相机1失败"+e0.ToString());
                IsOk = false;               
            }
            try
            {
                HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", Cam2Name, 0, -1, out AcqHandle2);            
            }
            catch 
            {
                MessageBox.Show("打开相机2失败");
                IsOk = false;
            }
            return IsOk;
        }


        public bool GrabImg(string CcdName,   out HObject Img   )
        {
            HTuple NowAcqHandle = new HTuple();
            bool Isok = true;
            switch(CcdName)
            {
                case "Cam1":
                    NowAcqHandle = AcqHandle1;
                    break;
                case "Cam2":
                    NowAcqHandle = AcqHandle2;
                    break;
                default:
                    Isok = false;
                    break;
            }
            Img = new HObject();
            if (!Isok) return false;
            if (AcqHandle1 == null) return false; 
            try
            {
                HOperatorSet.GrabImage(out Img, NowAcqHandle);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ExposSet(string CcdName,  HTuple ExposTime)
        {
            HTuple NowAcqHandle = new HTuple();
            switch (CcdName)
            {
                case "Cam1":
                    NowAcqHandle = AcqHandle1;
                    break;
                case "Cam2":
                    NowAcqHandle = AcqHandle2;
                    break;
            }
            try 
            {
                HOperatorSet.SetFramegrabberParam(NowAcqHandle, "ExposureTime", ExposTime);
                return true;
            }
            catch
            {
                return false;           
            }           
        }
        /// <summary>
        /// 设置相机图像模式
        /// </summary>
        /// <param name="Model"> rgb ， default,gray,raw,yuv</param>
        /// <param name="CcdName"></param>
        /// <returns></returns>
        public bool SetColorModel(string    Model="default", string CcdName="Cam1")
        {
            HTuple NowAcqHandle = new HTuple();
            switch (CcdName)
            {
                case "Cam1":
                    NowAcqHandle = AcqHandle1;
                    break;
                case "Cam2":
                    NowAcqHandle = AcqHandle2;
                    break;
            }
            try
            {
                if (NowAcqHandle = null) return false;
                HOperatorSet.SetFramegrabberParam(NowAcqHandle, "color_space", Model);
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 设置相机的触发模式
        /// </summary>
        /// <param name="Model"> Off,On</param>
        /// <param name="CcdName"></param>
        /// <returns></returns>
        public bool SetTrigerModel(string Model = "Off",   string  CcdName= "Cam1")
        {


            HTuple NowAcqHandle = new HTuple();
            switch (CcdName)
            {
                case "Cam1":
                    NowAcqHandle = AcqHandle1;
                    break;
                case "Cam2":
                    NowAcqHandle = AcqHandle2;
                    break;
            }
            try
            {
                HOperatorSet.SetFramegrabberParam(NowAcqHandle, "TriggerMode", Model);
                return true;
            }
            catch
            {
                return false;
            }

        }


        public bool TriggerModeSet(string CcdName, HTuple Model)
        {
            HTuple NowAcqHandle = new HTuple();
            switch (CcdName)
            {
                case "Cam1":
                    NowAcqHandle = AcqHandle1;
                    break;
                case "Cam2":
                    NowAcqHandle = AcqHandle2;
                    break;
            }
            try
            {
                HOperatorSet.SetFramegrabberParam(NowAcqHandle, "ExposureTime", Model);
                return true;
            }
            catch
            {
                return false;
            }

        }


    }
}
