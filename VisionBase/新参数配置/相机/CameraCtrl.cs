using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HalconDotNet;
using System.Windows.Forms;

namespace VisionBase
{
    public  class CameraCtrl
    {
        public List<CameraBase> CameraList = new List<CameraBase>();
        public static object LockObj = new object();
        private static CameraCtrl _instance;
        public static CameraCtrl Instance
        {
            get{
                if (_instance == null){
                    lock (LockObj){
                        if (_instance == null) _instance = new CameraCtrl();
                    }
                }
                return _instance;
            }
        }

        public bool Init()
        {
            HOperatorSet.CloseAllFramegrabbers();//关闭所有相机
            foreach (CameraPara item in CameraParaManager.Instance.CameraParaList){
                item.IsOpen = false;            
            }
            CameraList = new List<CameraBase>();
            CameraBase CurCam = null;
            foreach (CameraPara item in CameraParaManager.Instance.CameraParaList) //打开所有相机
            {
                System.Threading.Thread.Sleep(20);
                switch (item.CameInterface) {
                    case CamInterfaceEnum.HalconGigeE:
                        CurCam = new CameraBase();
                        break;
                    case CamInterfaceEnum.Pylon:
                        CurCam = new CameraPylon();
                        break;
                    case CamInterfaceEnum.HMV3rdParty:
                        CurCam = new CameraHMV3();
                        break;
                    case CamInterfaceEnum.CameraBasler:
                        CurCam = new CameraBasler();
                        break;
                    case CamInterfaceEnum.SaperaLT:
                        CurCam = new CameraSaperaLT();
                        break;
                    case CamInterfaceEnum.CameraCLDalsa:
                        CurCam = new CameraCLDalsa();
                        break;
                    default:
                        CurCam = new CameraBase();
                        break;
                }
                CurCam.SetCamPara(item);
                if (item.IsActive) {
                    if (!CurCam.DoInit()) {
                        item.IsOpen = false;
                        FileLib.Logger.Pop( string.Format("打开相机:{0}打开失败", item.CamDecribe));
                        MessageBox.Show(item.CameraName.ToString()+item.CamDecribe + "打开失败！");             
                    }
                    else{
                        item.IsOpen = true;
                        CurCam.IsOpen = true;
                    }                  
                }
                CameraList.Add(CurCam);
            }
            return true;
        }

        /// <summary>
        /// 原始参数重启相机，主要针对Dalsa相机
        /// </summary>
        /// <param name="CameraName"></param>
        /// <returns></returns>
        public bool Init(CameraEnum CameraName)
        {
            bool IsOk = true;
            foreach (var item in CameraList) {
                if (item.myCamPara.CameraName == CameraName) {
                    item.CloseCamera();
                    IsOk = item.DoInit();
                    break;
                }            
            }
            return true;
        }

        public bool CloseAllCamera()
        {
            foreach (var item in CameraList){
                if (item.IsOpen) {
                    item.CloseCamera();
                }         
            }
            return true;
        }

        public bool GrabImg(CameraEnum CameraName,out  HObject ImgOut)
        {
            ImgOut = new HObject();
            bool IsOk = true;
            foreach (var item in CameraList)  {
                if(item.myCamPara.CameraName == CameraName)
                    IsOk= item.GrabImg(out ImgOut,10000,item.myCamPara.Height);
                break;
            }
            return IsOk;
        }

        public bool GrabImg(CameraEnum CameraName, out HObject LightImgOut,out HObject DarkImgOut, int TimeOut = 10000, int Rows = 10000)
        {
            LightImgOut = new HObject();
            DarkImgOut = new HObject();
            bool IsOk = true;
            foreach (var item in CameraList) {
                if (item.myCamPara.CameraName == CameraName)
                    IsOk = item.GrabImg(out LightImgOut,out DarkImgOut, TimeOut, Rows);
                break;
            }
            return IsOk;
        }

        /// <summary>
        /// 用配置文件重新初始化相机的配置参数
        /// </summary>
        /// <param name="CameraName"></param>
        /// <param name="CamParaFileNanme"></param>
        /// <returns></returns>
        public bool ResetCamera(CameraEnum CameraName,string  CamParaFileNanme)
        {
            bool IsOk = true;
            foreach (var item in CameraList)  {
                if (item.myCamPara.CameraName == CameraName)
                    IsOk = item.ResetCamera(CamParaFileNanme);
                break;
            }
            return true;
        }


        public bool SetTriggerModel(CameraEnum CameraName, bool IsTrigger)
        {
            bool IsOk = true;
            foreach (var item in CameraList)
            {
                if (item.myCamPara.CameraName == CameraName)
                {
                    if (item.IsOpen) IsOk = item.SetTriggerModel(IsTrigger);
                    break;
                }
            }
            return IsOk;
        }

        public bool SetExpos(CameraEnum CameraName, int Expostime)
        {
            bool IsOk = true;
            foreach (var item in CameraList)
            {
                if (item.myCamPara.CameraName == CameraName)
                {
                    if (item.IsOpen) IsOk = item.SetExpos(Expostime);
                    break;
                }
            }
            return IsOk;
        }

    }
}
