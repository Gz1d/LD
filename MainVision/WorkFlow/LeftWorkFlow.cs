using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionBase;
using System.Threading;

namespace MainVision
{
    public class LeftWorkFlow
    {

        private LD.Common.PlcDevice LeftDnCamPlcTrigAddr = LD.Common.PlcDevice.V_03_TriggerGrab;
        private LD.Common.PlcDevice LeftDnCamGrabedAddr = LD.Common.PlcDevice.V_03_GrabFinish;
        private LD.Common.PlcDevice LeftDnCamVisionFinishAddr = LD.Common.PlcDevice.V_03_VisionFinish;
        private LD.Common.PlcDevice LeftDnCamVisionNumAddr = LD.Common.PlcDevice.V_03_GrabNum;

        private LD.Common.PlcDevice UpCamPlcTrigAddr = LD.Common.PlcDevice.V_04_TriggerGrab;
        private LD.Common.PlcDevice UpCamGrabedAddr = LD.Common.PlcDevice.V_04_GrabFinish;
        private LD.Common.PlcDevice UpCamVisionFinishAddr = LD.Common.PlcDevice.V_04_VisionFinish;
        private LD.Common.PlcDevice UpCamVisionNumAddr = LD.Common.PlcDevice.V_04_GrabNum;

        VisionBase.ProjectPara MyProjectPara = new VisionBase.ProjectPara();
        VisionPara LeftDnCamLeftVisionPara = new VisionPara();
        VisionPara LeftDnCamRightVisionPara = new VisionPara();

        VisionPara UpCamLeftUpVisionPara = new VisionPara();
        VisionPara UpCamRightUpVisionPara = new VisionPara();

        LocalResult LeftDnCamLeftLocalResult = new LocalResult();       //  左下相机左侧定位结果
        LocalResult LeftDnCamRightLocalResult = new LocalResult();  //  左下相机右侧定位结果

        LocalResult UpCamLeftUpLocalRlt = new LocalResult();  //上相机左上角定位结果
        LocalResult UpCamRightUpLocalRlt = new LocalResult();  //右上角右侧定位结果

        private Thread UpLocalThread, DnCamLocalThread;//上相机线程，下相机线程两个线程独立处理定位

        private ViewControl LeftDnCamLeftView, LeftDnCamRightView, UpCamLeftUpView, UpCamRightUpCamView;

        private VisionStateBase VisionStateBaseUpCamLeftUp, VisionStateBaseUpCamRightUp, VisionStateDnLeftCamLeft, VisionStateDnLeftCamRight;

        private ManualResetEventSlim UpCamProcessEvent = new ManualResetEventSlim(false);//上相机触发事件
        private ManualResetEventSlim DnCamProcessEvent = new ManualResetEventSlim(false);//下相机触发事件
        private bool UpCamLeftUpLocalIsok = false, UpCamRightUpLocalIsOk = false, LeftDnCamLeftLocalIsOk = false, LeftDnCamRightLocalIsOk = false;
        private bool UpCamThreadSwitch = false, DncamThreadSwitch = false;

        private  static object SyncObj = new object();
        private  static LeftWorkFlow _Instance;



        public static LeftWorkFlow Intance
        {
            get   
            {
                lock (SyncObj)
                {
                    if (_Instance == null)
                    {
                        _Instance = new LeftWorkFlow();
                    }
                    return _Instance;
                }           
            }
        }


        public void Init()
        {
            MyProjectPara = ProjectParaManager.Instance.GetProjectPara(0);
            LeftDnCamLeftVisionPara = MyProjectPara.GetVisionPara("左侧下相机左上角拍照位");
            LeftDnCamRightVisionPara = MyProjectPara.GetVisionPara("左侧下相机右下角拍照位");
            UpCamLeftUpVisionPara = MyProjectPara.GetVisionPara("上相机左上角拍照位1");
            UpCamRightUpVisionPara = MyProjectPara.GetVisionPara("上相机右下角拍照位1");

            LeftDnCamLeftView = DisplaySystem.GetViewControl("左侧下相机左上角拍照位");
            LeftDnCamRightView = DisplaySystem.GetViewControl("左侧下相机右下角拍照位");
            UpCamLeftUpView = DisplaySystem.GetViewControl("上相机左上角拍照位1");
            UpCamRightUpCamView = DisplaySystem.GetViewControl("上相机右下角拍照位1");

            VisionStateBaseUpCamLeftUp = new VisionStateBase(UpCamLeftUpVisionPara, "左侧下相机左上角拍照位", UpCamLeftUpView);
            VisionStateBaseUpCamRightUp = new VisionStateBase(UpCamRightUpVisionPara, "左侧下相机右下角拍照位", UpCamRightUpCamView);
            VisionStateDnLeftCamLeft = new VisionStateBase(LeftDnCamLeftVisionPara, "上相机左上角拍照位1", LeftDnCamLeftView);
            VisionStateDnLeftCamRight = new VisionStateBase(LeftDnCamRightVisionPara, "上相机右下角拍照位1", LeftDnCamRightView);

            VisionStateDnLeftCamLeft.SetCamLight();
            VisionStateBaseUpCamRightUp.SetCamLight();
            VisionStateDnLeftCamLeft.SetCamLight();
            VisionStateDnLeftCamRight.SetCamLight();
        }

        private void UpCamThread(Object Obj)
        {
            UpCamThreadSwitch = true;
            UpCamProcessEvent.Reset();
            while (true)
            {
                if (UpCamProcessEvent.IsSet)
                {
                    int UpCamStationNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(UpCamVisionNumAddr);
                    if (UpCamStationNum == 0) //上相机定位左上角
                    {
                        //1.0开始采图
                        VisionStateBaseUpCamLeftUp.GrabingImg();
                        //2.0告诉PLC采图完成
                        LD.Logic.PlcHandle.Instance.WriteValue(UpCamGrabedAddr, 1);
                        //3.0执行定位
                        VisionStateBaseUpCamLeftUp.DoLocal();
                        //4.0显示定位结果
                        VisionStateBaseUpCamLeftUp.ShowRlt();
                        //5.0获取定位结果
                        UpCamLeftUpLocalRlt = VisionStateBaseUpCamLeftUp.GetLocalRlt();
                        UpCamLeftUpLocalIsok = true;
                    }
                    else if (UpCamStationNum == 1)//上相机定位右上角
                    {

                        VisionStateBaseUpCamRightUp.GrabingImg();
                        LD.Logic.PlcHandle.Instance.WriteValue(UpCamGrabedAddr, 1);
                        VisionStateBaseUpCamRightUp.DoLocal();
                        VisionStateBaseUpCamRightUp.ShowRlt();
                        UpCamRightUpLocalRlt = VisionStateBaseUpCamLeftUp.GetLocalRlt();
                        UpCamRightUpLocalIsOk = true;
                    }
                    UpCamProcessEvent.Reset();
                }
                else
                {
                    Thread.Sleep(1);
                }
                if (UpCamLeftUpLocalIsok && UpCamRightUpLocalIsOk && LeftDnCamLeftLocalIsOk && LeftDnCamRightLocalIsOk)
                {
                    //1.0计算胶带当前的定位坐标，相对于示教坐标的偏差；
                    double LeftOffSetX = 0, LeftOffSetY = 0, RightOffSetX = 0, RightOffsetY = 0;
                    LeftOffSetX = LeftDnCamLeftLocalResult.PosToRot.Col - LeftDnCamLeftLocalResult.TeachPosToRot.Col;
                    LeftOffSetY = LeftDnCamLeftLocalResult.PosToRot.Row - LeftDnCamLeftLocalResult.TeachPosToRot.Row;
                    RightOffSetX =LeftDnCamRightLocalResult.PosToRot.Col - LeftDnCamRightLocalResult.TeachPosToRot.Col;
                    RightOffsetY = LeftDnCamRightLocalResult.PosToRot.Row - LeftDnCamRightLocalResult.TeachPosToRot.Row;
                    //2.0 用当前的胶带坐标相对于示教的偏移量， 调整Ipad的示教坐标
                    Point2Db TeachIpadPosLeft = new Point2Db(UpCamLeftUpLocalRlt.TeachPosToRot.Col, UpCamLeftUpLocalRlt.TeachPosToRot.Row);
                    Point2Db TeachIpadPosRight = new Point2Db(UpCamRightUpLocalRlt.TeachPosToRot.Col, UpCamRightUpLocalRlt.TeachPosToRot.Row);
                    TeachIpadPosLeft.Col = TeachIpadPosLeft.Col + LeftOffSetX;
                    TeachIpadPosLeft.Row = TeachIpadPosLeft.Row + LeftOffSetY;
                    TeachIpadPosRight.Col = TeachIpadPosRight.Col + RightOffSetX;
                    TeachIpadPosRight.Row = TeachIpadPosRight.Row + RightOffsetY;
                    //3.0 利用调整后的Ipad示教坐标 和 当前的Ipad坐标 计算出 偏移补偿量
                    Point2Db NowIpadLeftPos = new Point2Db(UpCamLeftUpLocalRlt.PosToRot.Col, UpCamLeftUpLocalRlt.PosToRot.Row);
                    Point2Db NowIpadRightPos = new Point2Db(UpCamRightUpLocalRlt.PosToRot.Col, UpCamRightUpLocalRlt.PosToRot.Row);
                    //4.0计算出偏移量
                    double AddX = 0, AddY = 0, AddTheta = 0;
                    MyVisionBase.CalculateThreeTapePos(NowIpadLeftPos, NowIpadRightPos, TeachIpadPosLeft, TeachIpadPosRight, out AddX, out AddY, out AddTheta);
                    FileLib.Logger.Pop(" 计算出的偏移补偿量：" + AddX.ToString("f3") + "  " + AddY.ToString("f3") + "  " + AddTheta.ToString("f3"), false, "运行日志");
                    int SystemOffSetX = 0, SystemOffsetY = 0;
                    SystemOffSetX = (int)(LeftDnCamLeftVisionPara.localPara.localSetting.Offset_x * 1000); //系统误差补偿
                    SystemOffsetY = (int)(LeftDnCamLeftVisionPara.localPara.localSetting.Offset_y * 1000);
                    int NewAddX = (int)(AddX * 1000) + SystemOffSetX;
                    int NewAddY = (int)(AddY * 1000) + SystemOffsetY;
                    int NewAddTheta = (int)(AddTheta * 1000);
                    LD.Logic.PlcHandle.Instance.WriteValue(LeftDnCamVisionFinishAddr, 1);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_03_Offset_X, NewAddX);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_03_Offset_Y, -NewAddY); //移动屏幕
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_03_Offset_R, NewAddTheta);

                    //5.0 清空此次定位的数据，进入下次定位
                    UpCamLeftUpLocalIsok = false;
                    UpCamRightUpLocalIsOk = false;
                    LeftDnCamLeftLocalIsOk = false;
                    LeftDnCamRightLocalIsOk = false;
                }
                if (!UpCamThreadSwitch) break;
            }

        }

        public void DnCamThread(object Obj)
        {
            DncamThreadSwitch = true;
            DnCamProcessEvent.Reset();
            while (true)
            {
                if (DnCamProcessEvent.IsSet)
                {
                    int DnCamStationNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(LeftDnCamVisionNumAddr);
                    if (DnCamStationNum == 0)      //左下相机左侧定位
                    {
                        VisionStateDnLeftCamLeft.GrabingImg();                                //1.0开始采图
                        LD.Logic.PlcHandle.Instance.WriteValue(LeftDnCamGrabedAddr, 1);       //2.0告诉PLC采图完成
                        VisionStateDnLeftCamLeft.DoLocal();                                   //3.0执行定位
                        VisionStateDnLeftCamLeft.ShowRlt();                                   //4.0显示定位结果
                        LeftDnCamLeftLocalResult = VisionStateDnLeftCamLeft.GetLocalRlt();
                        LeftDnCamLeftLocalIsOk = true;
                    }
                    else if (DnCamStationNum == 1) //左下相机右侧定位
                    {
                        VisionStateDnLeftCamRight.GrabingImg();
                        LD.Logic.PlcHandle.Instance.WriteValue(LeftDnCamGrabedAddr, 1);
                        VisionStateDnLeftCamRight.DoLocal();
                        VisionStateDnLeftCamRight.ShowRlt();
                        LeftDnCamRightLocalResult = VisionStateDnLeftCamRight.GetLocalRlt();
                        LeftDnCamRightLocalIsOk = true;
                    }
                }
                DnCamProcessEvent.Reset();
                if (!DncamThreadSwitch) break;

            }

        }

        public void WaitPlcTrigger(LD.Config.PlcDataItem PlcItem, Object Obj)
        {
            LD.Common.PlcDevice plcDevice = PlcItem.PlcDevice;
            if (plcDevice == LeftDnCamPlcTrigAddr)
            {
                DnCamProcessEvent.Set();           
            }
            if (plcDevice == UpCamPlcTrigAddr)
            {
                UpCamProcessEvent.Set();          
            }

        }

        public void Start()
        {
            UpLocalThread = new Thread(new ParameterizedThreadStart(UpCamThread));
            UpLocalThread.Priority = ThreadPriority.Normal;
            UpLocalThread.Name = "上相机左工位定位线程";
            UpLocalThread.IsBackground = true;
            UpLocalThread.Start();

            DnCamLocalThread = new Thread(new ParameterizedThreadStart(DnCamThread));
            DnCamLocalThread.Priority = ThreadPriority.Normal;
            DnCamLocalThread.Name = "左下相机定位线程";
            DnCamLocalThread.IsBackground = true;
            DnCamLocalThread.Start();

        }

        public void Stop()
        {
            DncamThreadSwitch = false;
            UpCamThreadSwitch = false;

            UpCamLeftUpLocalIsok = false;
            UpCamRightUpLocalIsOk = false;
            LeftDnCamLeftLocalIsOk = false;
            LeftDnCamRightLocalIsOk = false;

        }


    }
}
