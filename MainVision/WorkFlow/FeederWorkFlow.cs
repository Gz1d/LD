using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VisionBase;


namespace MainVision
{
    /// <summary>
    /// 供料自动视觉流程
    /// </summary>
    public  class FeederWorkFlow
    {
        private LD.Common.PlcDevice FeederCamPlcTrigAddr = LD.Common.PlcDevice.V_01_TriggerGrab;
        private LD.Common.PlcDevice FeederCamGrabedAddr = LD.Common.PlcDevice.V_01_GrabFinish;
        private LD.Common.PlcDevice FeederCamVisionFinishAddr = LD.Common.PlcDevice.V_01_VisionFinish;
        private LD.Common.PlcDevice FeederCamStateNumAddr = LD.Common.PlcDevice.V_01_GrabNum;

        ProjectPara MyProjectPara = new ProjectPara();
        VisionPara LeftUpVisionPara = new VisionPara();
        VisionPara RightDnVisionPara = new VisionPara();

        LocalResult LeftUpLocalRlt = new LocalResult();
        LocalResult RightDnLocalRlt = new LocalResult();

        private Thread FeederThread;
        private ViewControl LeftUpView, RightDnView;
        private VisionStateBase VisionStateLeftUp, VisionStateRightDn;
        private ManualResetEventSlim FeerderCamTrigEvent = new ManualResetEventSlim(false); //供料相机触发事件
        private bool LeftUpLocalIsOK = false, RightDnLocalIsOk = false;
        private bool FeederThreadSwitch = false;
        private static object SyncObj = new object();
        private static FeederWorkFlow _Instance;

        public static FeederWorkFlow Instance
        {
            get
            {
                lock (SyncObj)
                {
                    if (_Instance == null) _Instance = new FeederWorkFlow();
                    return _Instance;
                }
            }
        }

        public void Init()
        {
            MyProjectPara = ProjectParaManager.Instance.GetProjectPara(0);
            LeftUpVisionPara = MyProjectPara.GetVisionPara("Ipad左上角拍照位");
            RightDnVisionPara = MyProjectPara.GetVisionPara("Ipad右下角拍照位");

            LeftUpView = DisplaySystem.GetViewControl("Ipad左上角拍照位1");
            RightDnView = DisplaySystem.GetViewControl("Ipad右下角拍照位1");

            VisionStateLeftUp = new VisionStateBase(LeftUpVisionPara, "Ipad左上角拍照位", LeftUpView);
            VisionStateLeftUp = new VisionStateBase(RightDnVisionPara, "Ipad右下角拍照位", RightDnView);

            VisionStateLeftUp.SetCamLight();
            VisionStateRightDn.SetCamLight();
        }


        private void IpadLcoalThread(object Obj)
        {
            FeederThreadSwitch = true;
            while (true){
                if (FeerderCamTrigEvent.IsSet) {
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
                    Point2Db IpadLeftUpTeachPos = new Point2Db(LeftUpLocalRlt.TeachPosToRot.Col,LeftUpLocalRlt.TeachPosToRot.Row);
                    Point2Db IpadRightDnTeachPos = new Point2Db(RightDnLocalRlt.TeachPosToRot.Col,RightDnLocalRlt.TeachPosToRot.Row);

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
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_01_Offset_X, NewAddX);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_01_Offset_Y, NewAddY); //移动屏幕
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_01_Offset_R, NewAddTheta);

                    //4.0 清空此次定位的数据，进入下次定位
                    LeftUpLocalIsOK = false;
                    RightDnLocalIsOk = false;
                }
                Thread.Sleep(1);
                if (!FeederThreadSwitch) break;
            }     
        }

        public void WaitPlcTrigger(LD.Config.PlcDataItem PlcItem, Object Obj)
        {
            LD.Common.PlcDevice plcDevice = PlcItem.PlcDevice;
            if (plcDevice == FeederCamPlcTrigAddr){
                FeerderCamTrigEvent.Set();
            }
         }


        public void Start(){
            FeederThread = new Thread(new ParameterizedThreadStart(IpadLcoalThread));
            FeederThread.Priority = ThreadPriority.Normal;
            FeederThread.Name = "供料工位视觉线程FeederThread";
            FeederThread.IsBackground = true;
            FeederThread.Start();
        }


        public void Stop() {
            FeederThreadSwitch = false;
            LeftUpLocalIsOK = false;
            RightDnLocalIsOk = false;
        }
    }
}
