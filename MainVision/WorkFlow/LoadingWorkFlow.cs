using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VisionBase;

namespace MainVision
{
    public  class LoadingWorkFlow
    {
        private LD.Common.PlcDevice FeederCamPlcTrigAddr = LD.Common.PlcDevice.V_02_TriggerGrab;
        private LD.Common.PlcDevice FeederCamGrabedAddr = LD.Common.PlcDevice.V_02_GrabFinish;
        private LD.Common.PlcDevice FeederCamVisionFinishAddr = LD.Common.PlcDevice.V_02_VisionFinish;
        private LD.Common.PlcDevice FeederCamStateNumAddr = LD.Common.PlcDevice.V_02_GrabNum;

        ProjectPara MyProjectPara = new ProjectPara();
        VisionPara LeftUpVisionPara = new VisionPara();
        VisionPara RightDnVisionPara = new VisionPara();

        LocalResult LeftUpLocalRlt = new LocalResult();
        LocalResult RightDnLocalRlt = new LocalResult();

        private Thread LoadingThread;
        private ViewControl LeftUpView, RightDnView;
        private VisionStateBase VisionStateLeftUp, VisionStateRightDn;
        private ManualResetEventSlim LoadingCamTrigEvent = new ManualResetEventSlim(false); //供料相机触发事件
        private bool LeftUpLocalIsOK = false, RightDnLocalIsOk = false;
        private bool LoadingThreadSwitch = false;
        private static object SyncObj = new object();
        private static LoadingWorkFlow _Instance;


        public static LoadingWorkFlow Instance
        {
            get
            {
                lock (SyncObj)
                {
                    if (_Instance == null) _Instance = new LoadingWorkFlow();
                    return _Instance;
                }
            }
        }

        public void Init()
        {
            MyProjectPara = ProjectParaManager.Instance.GetProjectPara(1);
            LeftUpVisionPara = MyProjectPara.GetVisionPara("Ipad左上角拍照位");
            RightDnVisionPara = MyProjectPara.GetVisionPara("Ipad右下角拍照位");

            LeftUpView = DisplaySystem.GetViewControl("Ipad左上角拍照位2");
            RightDnView = DisplaySystem.GetViewControl("Ipad右下角拍照位2");

            VisionStateLeftUp = new VisionStateBase(LeftUpVisionPara, "Ipad左上角拍照位", LeftUpView);
            VisionStateLeftUp = new VisionStateBase(RightDnVisionPara, "Ipad右下角拍照位", RightDnView);

            VisionStateLeftUp.SetCamLight();
            VisionStateRightDn.SetCamLight();
        }

        private void IpadLcoalThread(object Obj)
        {
            LoadingThreadSwitch = true;
            LoadingCamTrigEvent.Reset();

            while (true)
            {
                if (LoadingCamTrigEvent.IsSet)
                {
                    int StateNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(FeederCamStateNumAddr);
                    if (StateNum == 0) //Ipad左上角
                    {
                        VisionStateLeftUp.GrabingImg();
                        LD.Logic.PlcHandle.Instance.WriteValue(FeederCamGrabedAddr, 1);
                        VisionStateLeftUp.DoLocal();
                        VisionStateLeftUp.ShowRlt();
                        LeftUpLocalRlt = VisionStateLeftUp.GetLocalRlt();
                        LeftUpLocalIsOK = true;
                    }
                    else if (StateNum == 1)  //Ipad右下角
                    {
                        VisionStateRightDn.GrabingImg();
                        LD.Logic.PlcHandle.Instance.WriteValue(FeederCamGrabedAddr, 1);
                        VisionStateRightDn.DoLocal();
                        VisionStateRightDn.ShowRlt();
                        RightDnLocalRlt = VisionStateRightDn.GetLocalRlt();
                        RightDnLocalIsOk = true;
                    }
                }
                if (LeftUpLocalIsOK && RightDnLocalIsOk)
                {
                    //1.0Ipad放置时的示教坐标
                    Point2Db IpadLeftUpTeachPos = new Point2Db(LeftUpLocalRlt.TeachPosToRot.Col, LeftUpLocalRlt.TeachPosToRot.Row);
                    Point2Db IpadRightDnTeachPos = new Point2Db(RightDnLocalRlt.TeachPosToRot.Col, RightDnLocalRlt.TeachPosToRot.Row);
                    //2.0Ipad的当前坐标
                    Point2Db IpadLeftUpNowPos = new Point2Db(LeftUpLocalRlt.PosToRot.Col, LeftUpLocalRlt.PosToRot.Row);
                    Point2Db IpadRightDnNowPos = new Point2Db(RightDnLocalRlt.PosToRot.Col, RightDnLocalRlt.PosToRot.Row);
                    //3.0计算出偏移量
                    double AddX = 0, AddY = 0, AddTheta = 0;
                    MyVisionBase.CalculateThreeTapePos(IpadLeftUpNowPos, IpadRightDnNowPos, IpadLeftUpTeachPos, IpadRightDnTeachPos, out AddX, out AddY, out AddTheta);
                    FileLib.Logger.Pop(" 计算出的偏移补偿量：" + AddX.ToString("f3") + "  " + AddY.ToString("f3") + "  " + AddTheta.ToString("f3"), false, "运行日志");
                    int SystemOffSetX = 0, SystemOffsetY = 0;
                    SystemOffSetX = (int)(LeftUpVisionPara.localPara.localSetting.Offset_x * 1000); //系统误差补偿
                    SystemOffsetY = (int)(LeftUpVisionPara.localPara.localSetting.Offset_y * 1000); //系统误差补偿
                    int NewAddX = (int)(AddX * 1000) + SystemOffSetX;
                    int NewAddY = (int)(AddY * 1000) + SystemOffsetY;
                    int NewAddTheta = (int)(AddTheta * 1000);
                    LD.Logic.PlcHandle.Instance.WriteValue(FeederCamVisionFinishAddr, 1);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_02_Offset_X, NewAddX);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_02_Offset_Y, NewAddY); //移动屏幕
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_02_Offset_R, NewAddTheta);

                    //4.0 清空此次定位的数据，进入下次定位
                    LeftUpLocalIsOK = false;
                    RightDnLocalIsOk = false;
                }
                Thread.Sleep(1);
                if (!LoadingThreadSwitch) break;
            }
        }

        public void WaitPlcTrigger(LD.Config.PlcDataItem PlcItem, Object Obj)
        {
            LD.Common.PlcDevice plcDevice = PlcItem.PlcDevice;
            if (plcDevice == FeederCamPlcTrigAddr)
            {
                LoadingCamTrigEvent.Set();
            }
        }

        public void Start()
        {
            LoadingThread = new Thread(new ParameterizedThreadStart(IpadLcoalThread));
            LoadingThread.Priority = ThreadPriority.Normal;
            LoadingThread.Name = "供料工位视觉线程FeederThread";
            LoadingThread.IsBackground = true;
            LoadingThread.Start();
        }


        public void Stop()
        {
            LoadingThreadSwitch = false;
            LeftUpLocalIsOK = false;
            RightDnLocalIsOk = false;

        }
    }
}
