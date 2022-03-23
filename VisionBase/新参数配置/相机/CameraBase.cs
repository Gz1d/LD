using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace VisionBase
{
    public  class CameraBase
    {

        public CameraPara myCamPara;//相机参数，相机
        public bool IsOpen = false;
        public HTuple CamHandle;

        public  HObject CurImg;
        /// <summary> 相机采图完成事件</summary>
        public ManualResetEventSlim ImgReadyEvent = new ManualResetEventSlim(false); 

        public virtual bool DoInit() //初始化打开相机
        {
            try{
                HOperatorSet.OpenFramegrabber("GigEVision2", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default",
                this.myCamPara.CameraName.ToString(), 0, -1, out this.CamHandle);
                this.IsOpen = true;
            }
            catch {
                return false;
            }
            return true;
        }



        public void SetCamPara(CameraPara camParaIn)
        {
            this.myCamPara = camParaIn;
        }


        public virtual bool  CloseCamera() //关闭相机
        {
            try {
                HOperatorSet.CloseFramegrabber(this.CamHandle);
            }
            catch {
                return false;
            }
            return true;
        }

        public virtual bool GrabImg(out HObject ImgOut ,int TimeOut =10000,int Rows =10000)  //采集图片
        {
            ImgOut = new HObject();
            HObject CurImg = new HObject();
            HOperatorSet.GrabImage(out CurImg,this.CamHandle);
            bool IsTrue = false;
            IsTrue=AdjImg(CurImg, out ImgOut);
            CurImg.Dispose();
            return IsTrue;
        }

        /// <summary>
        /// 分时频闪，分割图片
        /// </summary>
        /// <param name="LightImgOut">明场图片</param>
        /// <param name="DarkImgOut">暗场图片</param>
        /// <param name="TimeOut"></param>
        public virtual bool GrabImg(out HObject LightImgOut,out HObject DarkImgOut,int TimeOut =10000, int Rows = 10000)
        {
            LightImgOut = new HObject();
            DarkImgOut = new HObject();
            return true;        
        }

        /// <summary>
        /// 利用相机的保存的配置文件，重新设置相机参数
        /// </summary>
        /// <param name="CamParaFileName"> 相机参数配置文件名字  </param>
        /// <returns></returns>
        public virtual bool ResetCamera( string  CamParaFileName )
        {
            return true;
        }

        public virtual bool AdjImg(HObject ImgIn, out HObject ImgOut)
        {
            ImgOut = new HObject();
            HObject RotImg = new HObject(),MirrorXImg =new HObject();
            try{
                if (this.myCamPara.IsRot){
                    HOperatorSet.RotateImage(ImgIn, out RotImg, 90, "constant");
                }
                else{
                    HOperatorSet.CopyObj(ImgIn, out RotImg, 1, -1);
                }
                if (this.myCamPara.IsMirrorX){
                    HOperatorSet.MirrorImage(RotImg, out MirrorXImg, "column");
                }
                else{
                    HOperatorSet.CopyObj(RotImg, out MirrorXImg, 1, -1);
                }
                RotImg.Dispose();
                if (this.myCamPara.IsMirrorY){
                    HOperatorSet.MirrorImage(MirrorXImg, out ImgOut, "row");
                }
                else {
                    HOperatorSet.CopyObj(MirrorXImg, out ImgOut, 1, -1);
                }
                MirrorXImg.Dispose();
            }
            catch{
                return false;
            }
 
            return true;
        }


        public virtual bool GrabImgSync(HObject ImgOut,int TimeOut=5000)  //异步采集图片
        {
            ImgOut = new HObject();
            ImgReadyEvent.Reset();
            HOperatorSet.GrabImageStart(this.CamHandle, TimeOut);
            Task.Factory.StartNew(() =>  {
                try {
                    HOperatorSet.GrabImageAsync(out ImgOut, this.CamHandle, TimeOut);
                    ImgReadyEvent.Set();
                }
                catch{  }                                   
            });

            return true;
        }

        public virtual bool  GetCurImg( out HObject ImgOut ,int TimeOut =5000)  //获取当前图片
        {
            ImgReadyEvent.Wait(TimeOut);
            ImgOut = this.CurImg;
            return ImgReadyEvent.IsSet;
         }

        public virtual bool SetTriggerModel(bool IsTrigger)
        {
            string Model = "Off";
            if (IsTrigger) Model = "On";
            try {
                HOperatorSet.SetFramegrabberParam(this.CamHandle, "TriggerMode", Model);
            }
            catch{
                return false;
            }
            return true;
        }

        public virtual bool SetExpos(int ExposTime)
        {
            HTuple ExposTime0 = (double)ExposTime;
            try{
                HOperatorSet.SetFramegrabberParam(this.CamHandle, "ExposureTime", ExposTime0.D);
            }
            catch(Exception e1){
                MessageBox.Show(e1.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置线阵相机的采集行数
        /// </summary>
        /// <param name="LineCount"></param>
        /// <returns></returns>
        public virtual bool SetCameraLineCount(int LineCount)
        {
            return true;
        }


    }
}
