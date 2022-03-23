using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using Basler.Pylon;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VisionBase
{
    public class CameraBasler : CameraBase
    {
        Basler.Pylon.Camera camera1;
        public override bool DoInit()
        {
            try {
                if (this.myCamPara.CameraName == CameraEnum.Cam0){
                    camera1 = new Basler.Pylon.Camera("22954016");
                }
                else{
                    camera1 = new Basler.Pylon.Camera("22970222");
                }
                camera1.CameraOpened += Basler.Pylon.Configuration.AcquireContinuous;
                camera1.Open();
                this.SetCamPara();
                camera1.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
                this.IsOpen = true;
            }
            catch{
                return false;
            }
            return true;
        }

        public void SetCamPara()
        {
            camera1.Parameters[PLCamera.ShaftEncoderModuleLineSelector].SetValue(PLCamera.ShaftEncoderModuleLineSelector.PhaseA);
            camera1.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.LineStart);
            camera1.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
            camera1.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.ShaftEncoderModuleOut);
            camera1.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.RisingEdge);
            camera1.Parameters[PLCamera.AcquisitionStatusSelector].SetValue(PLCamera.AcquisitionStatusSelector.LineTriggerWait);
            camera1.Parameters[PLCamera.FrequencyConverterInputSource].SetValue(PLCamera.FrequencyConverterInputSource.ShaftEncoderModuleOut);
            camera1.Parameters[PLCamera.FrequencyConverterSignalAlignment].SetValue(PLCamera.FrequencyConverterSignalAlignment.RisingEdge);
            camera1.Parameters[PLCamera.ShaftEncoderModuleLineSource].SetValue(PLCamera.ShaftEncoderModuleLineSource.Line1);
            camera1.Parameters[PLCamera.ShaftEncoderModuleMode].SetValue(PLCamera.ShaftEncoderModuleMode.ForwardOnly);//单方向
            camera1.Parameters[PLCamera.FrequencyConverterPreDivider].SetValue(7);//频率减小7倍
            camera1.Parameters[PLCamera.AutoFunctionAOIHeight].SetValue(10000);
        }
        public override bool GrabImg(out HObject ImgOut,int TimeOut =10000,int Rows = 10000)
        {
            ImgOut = new HObject();
            try{
                camera1.StreamGrabber.Start();
                IGrabResult grabResult = camera1.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                using (grabResult) {
                    // Image grabbed successfully?
                    if (grabResult.GrabSucceeded) {
                        byte[] buffer = grabResult.PixelData as byte[];
                        //锁定图像数据
                        GCHandle gCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                        //获取图像数据的指针
                        IntPtr intPtr = gCHandle.AddrOfPinnedObject();
                        //转换成Hobject类型
                        HObject Img;
                        HOperatorSet.GenImage1(out Img, new HTuple("byte"), grabResult.Width, grabResult.Height, intPtr);
                        grabResult.Dispose();
                        HOperatorSet.CopyObj(Img, out ImgOut, 1, 1);
                        return true;
                    }
                    else{
                        Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                        return false;
                    }
                }
            }
            catch (Exception e0){
                camera1.StreamGrabber.Stop();
                return false;
            }
        }
       
        public override bool SetExpos(int ExposTime)
        {
            camera1.Parameters[PLCamera.ExposureTimeAbs].SetValue(ExposTime);//设置曝光
            return true;
        }

        public override bool SetTriggerModel(bool IsTrigger)
        {
            if (IsTrigger)
                camera1.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
            else{
                camera1.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);
                camera1.Parameters[PLCamera.AcquisitionLineRateAbs].SetValue(8000);
            }
            return true;
        }


        public override bool SetCameraLineCount(int LineCount)
        {
            camera1.Parameters[PLCamera.AutoFunctionAOIHeight].SetValue(LineCount);
            return true;
        }
        public override bool CloseCamera()
        {
            camera1.StreamGrabber.Stop();
            camera1.Close();
            camera1.Dispose();
            camera1 = null;
            return true;
        }
    }
}
