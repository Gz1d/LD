using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VisionBase;

namespace MainVision
{
    public class RightWorkFlow
    {
        //右侧下相机对应的PLC触发地址
        private LD.Common.PlcDevice RightDnCamPlcTrigAddr = LD.Common.PlcDevice.V_05_TriggerGrab;    
        private LD.Common.PlcDevice RightDnCamGrabedAddr = LD.Common.PlcDevice.V_05_GrabFinish;          //告诉PLC采图完成
        private LD.Common.PlcDevice RightDnCamVisionFinishAddr = LD.Common.PlcDevice.V_05_VisionFinish;  //告诉Plc视觉定位完成
        private LD.Common.PlcDevice RightDnCamVisionNumAddr = LD.Common.PlcDevice.V_05_GrabNum;          //拍照工位

        private LD.Common.PlcDevice UpCamPlcTrigAddr = LD.Common.PlcDevice.V_06_TriggerGrab;
        private LD.Common.PlcDevice UpCamGrabedAddr = LD.Common.PlcDevice.V_06_GrabFinish;
        private LD.Common.PlcDevice UpCamVisionFinishAddr = LD.Common.PlcDevice.V_06_VisionFinish;
        private LD.Common.PlcDevice UpCamVisionNumAddr = LD.Common.PlcDevice.V_06_GrabNum;

        VisionBase.ProjectPara MyProjectPara = new VisionBase.ProjectPara();
        VisionPara RightDnLeftVisionPara = new VisionPara();             //右侧下相机左拍照位视觉参数
        VisionPara RightDnRightVisionPara = new VisionPara();

        VisionPara UpCamLeftDnVisionPara = new VisionPara();             //上相机左下角视觉定位参数
        VisionPara UpCamRightDnVisionPara = new VisionPara();            //上相机右下角视觉定位参数

        LocalResult RightDnCamLeftLocalRlt = new LocalResult();          //右侧下相机左拍照位视觉定位结果
        LocalResult RightDnCamRightLocalRlt = new LocalResult();         //右侧下相机右拍照位视觉定位结果

        LocalResult UpCamLeftDnLocalRlt = new LocalResult();             //上相机Ipad左下角视觉定位参数
        LocalResult UpCamRightDnLocalRlt = new LocalResult();            //上相机Ipad右下角视觉定位参数

        private Thread UpLocalThread, DnCamLocalThread;//上相机定位线程和下相机线程，两个线程独立处理定位运算
        private ViewControl RightDnCamLeftStaView, RightDnCamRightStaView, UpCamLeftDnStaView, UpCamRightDnStaView; //每个工位的显示控件
        private VisionStateBase VisionStateUpCamLeftDn, VisionStateUpCamRightDn, VisionStateRightDnCamLeft, VisionStateRightDnCamRight;
        private ManualResetEventSlim UpCamProcessEvent = new ManualResetEventSlim(false); //上相机触发事件
        private ManualResetEventSlim DnCamProcessEvent = new ManualResetEventSlim(false); //下相机触发事件
        private bool UpCamLeftDnLocalIsOk = false, UpCamRightDnLocalIsOk = false,RightDnLeftLocalIsOk =false,RightDnRightLocalIsOk = false;
        private bool UpCamThreadSwitch = false, DnCamThreadSwitch = false;

        private static Object SyncObj = new object();
        private static RightWorkFlow _Instance;

        public  static RightWorkFlow Instance {
            get  {
                lock (SyncObj) {
                    if (_Instance == null) _Instance = new RightWorkFlow();
                    return _Instance;
                }               
            }               
        }

        public void Init()
        {
            MyProjectPara = ProjectParaManager.Instance.GetProjectPara(0);
            RightDnLeftVisionPara = MyProjectPara.GetVisionPara("右侧下相机左上角拍照位");
            RightDnRightVisionPara = MyProjectPara.GetVisionPara("右侧下相机右下角拍照位");
            UpCamLeftDnVisionPara = MyProjectPara.GetVisionPara("上相机左上角拍照位2");
            UpCamRightDnVisionPara = MyProjectPara.GetVisionPara("上相机右下角拍照位2");

            RightDnCamLeftStaView = DisplaySystem.GetViewControl("右侧下相机左上角拍照位");
            RightDnCamRightStaView = DisplaySystem.GetViewControl("右侧下相机右下角拍照位");
            UpCamLeftDnStaView = DisplaySystem.GetViewControl("上相机左上角拍照位2");
            UpCamRightDnStaView = DisplaySystem.GetViewControl("上相机右下角拍照位2");

            VisionStateUpCamLeftDn = new VisionStateBase(UpCamLeftDnVisionPara, "右侧下相机左上角拍照位", UpCamLeftDnStaView);
            VisionStateUpCamRightDn = new VisionStateBase(UpCamRightDnVisionPara, "右侧下相机右下角拍照位", UpCamRightDnStaView);
            VisionStateRightDnCamLeft = new VisionStateBase(RightDnLeftVisionPara, "上相机左上角拍照位2", RightDnCamLeftStaView);
            VisionStateRightDnCamRight = new VisionStateBase(RightDnRightVisionPara, "上相机右下角拍照位2", RightDnCamRightStaView);

            VisionStateUpCamLeftDn.SetCamLight();
            VisionStateUpCamRightDn.SetCamLight();
            VisionStateRightDnCamLeft.SetCamLight();
            VisionStateRightDnCamRight.SetCamLight();
        }

        public void UpCamThread(Object Obj)
        {
            UpCamThreadSwitch = true;
            UpCamProcessEvent.Reset();
            while (true) {
                if (UpCamProcessEvent.IsSet){
                    int UpCamStationNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(UpCamVisionNumAddr);
                    if (UpCamStationNum == 0){
                        VisionStateUpCamLeftDn.GrabingImg();                          //1.0 开始采图
                        LD.Logic.PlcHandle.Instance.WriteValue(UpCamGrabedAddr, 1);   //2.0 告诉Plc采图完成
                        VisionStateUpCamLeftDn.DoLocal();                             //3.0 执行定位
                        VisionStateUpCamLeftDn.ShowRlt();                             //4.0 显示定位结果
                        UpCamLeftDnLocalRlt = VisionStateUpCamLeftDn.GetLocalRlt();   //5.0  获取定位结果
                        UpCamLeftDnLocalIsOk = true;
                    }
                    else if (UpCamStationNum == 1){
                        VisionStateUpCamRightDn.GrabingImg();
                        LD.Logic.PlcHandle.Instance.WriteValue(UpCamGrabedAddr, 1);
                        VisionStateUpCamRightDn.DoLocal();
                        VisionStateUpCamRightDn.ShowRlt();
                        UpCamRightDnLocalRlt = VisionStateUpCamRightDn.GetLocalRlt();
                        UpCamRightDnLocalIsOk = true;
                    }
                    UpCamProcessEvent.Reset();
                }
                else{
                    Thread.Sleep(1);
                }
                if (UpCamLeftDnLocalIsOk && UpCamRightDnLocalIsOk && RightDnLeftLocalIsOk && RightDnRightLocalIsOk){
                    //1.0计算胶带当前的定位坐标，相对于示教坐标的偏差；
                    double LeftOffSetX = 0, LeftOffSetY = 0, RightOffSetX = 0, RightOffsetY = 0;
                    LeftOffSetX =  RightDnCamLeftLocalRlt.PosToRot.Col - RightDnCamLeftLocalRlt.TeachPosToRot.Col;
                    LeftOffSetY =  RightDnCamLeftLocalRlt.PosToRot.Row - RightDnCamLeftLocalRlt.TeachPosToRot.Row;
                    RightOffSetX = RightDnCamRightLocalRlt.PosToRot.Col - RightDnCamRightLocalRlt.TeachPosToRot.Col;
                    RightOffsetY = RightDnCamRightLocalRlt.PosToRot.Row - RightDnCamRightLocalRlt.TeachPosToRot.Row;

                    //2.0 用当前的胶带坐标相对于示教的偏移量， 调整Ipad的示教坐标
                    Point2Db TeachIpadPosLeft = new Point2Db(UpCamLeftDnLocalRlt.TeachPosToRot.Col, UpCamLeftDnLocalRlt.TeachPosToRot.Row);
                    Point2Db TeachIpadPosRight = new Point2Db(UpCamRightDnLocalRlt.TeachPosToRot.Col, UpCamRightDnLocalRlt.TeachPosToRot.Row);
                    TeachIpadPosLeft.Col = TeachIpadPosLeft.Col + LeftOffSetX;
                    TeachIpadPosLeft.Row = TeachIpadPosLeft.Row + LeftOffSetY;
                    TeachIpadPosRight.Col = TeachIpadPosRight.Col + RightOffSetX;
                    TeachIpadPosRight.Row = TeachIpadPosRight.Row + RightOffsetY;

                    //3.0 利用调整后的Ipad示教坐标 和 当前的Ipad坐标 计算出 偏移补偿量
                    Point2Db NowIpadLeftPos = new Point2Db(UpCamLeftDnLocalRlt.PosToRot.Col, UpCamLeftDnLocalRlt.PosToRot.Row);
                    Point2Db NowIpadRightPos = new Point2Db(UpCamRightDnLocalRlt.PosToRot.Col, UpCamRightDnLocalRlt.PosToRot.Row);

                    //4.0计算出偏移量
                    double SystemOffsetX = RightDnLeftVisionPara.localPara.localSetting.Offset_x;
                    double SystemOffsetY = RightDnLeftVisionPara.localPara.localSetting.Offset_y;
                    double SystemOffsetTheta = RightDnLeftVisionPara.localPara.localSetting.Offset_theta;
                    double AddX = 0, AddY = 0, AddTheta = 0;
                    MyVisionBase.CalculateTwoPtPos(NowIpadLeftPos, NowIpadRightPos, TeachIpadPosLeft, TeachIpadPosRight,
                         SystemOffsetX, SystemOffsetY, SystemOffsetTheta, out AddX, out AddY, out AddTheta);                   
                    FileLib.Logger.Pop(" 计算出的偏移补偿量：" + AddX.ToString("f3") + "  " + AddY.ToString("f3") + "  " +
                        AddTheta.ToString("f3"), false, "运行日志");

                    int NewAddX = (int)(AddX * 1000);
                    int NewAddY = (int)(AddY * 1000);
                    int NewAddTheta = (int)(AddTheta * 1000);

                    LD.Logic.PlcHandle.Instance.WriteValue(RightDnCamVisionFinishAddr, 1);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_05_Offset_X, -NewAddX);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_05_Offset_Y, NewAddY);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_05_Offset_R, NewAddTheta);
                }
            }
        }

        public void DnCamThread(Object Obj)
        {
            DnCamThreadSwitch = true;
            DnCamProcessEvent.Reset();
            while (true) {
                if (DnCamProcessEvent.IsSet){
                    int DnCamStationNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(RightDnCamVisionNumAddr);
                    if (DnCamStationNum == 0){
                        VisionStateRightDnCamLeft.GrabingImg();
                        LD.Logic.PlcHandle.Instance.WriteValue(RightDnCamGrabedAddr, 1);
                        VisionStateRightDnCamLeft.DoLocal();
                        VisionStateRightDnCamLeft.ShowRlt();
                        RightDnCamLeftLocalRlt = VisionStateRightDnCamLeft.GetLocalRlt();
                        RightDnLeftLocalIsOk = true;
                    }
                    else if (DnCamStationNum == 1) {
                        VisionStateRightDnCamRight.GrabingImg();
                        LD.Logic.PlcHandle.Instance.WriteValue(RightDnCamGrabedAddr, 1);
                        VisionStateRightDnCamLeft.DoLocal();
                        VisionStateRightDnCamLeft.ShowRlt();
                        RightDnCamLeftLocalRlt = VisionStateRightDnCamLeft.GetLocalRlt();
                        RightDnRightLocalIsOk = true;
                    }
                    DnCamProcessEvent.Reset();
                    if (! DnCamThreadSwitch) break;
                }                       
            }               
        }

        public void WaitPlcTrigger(LD.Config.PlcDataItem PlcItem, Object Obj)
        {
            LD.Common.PlcDevice plcDevice = PlcItem.PlcDevice;
            if (plcDevice == RightDnCamPlcTrigAddr)
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
            UpLocalThread  = new Thread(new ParameterizedThreadStart(UpCamThread));
            UpLocalThread.Priority = ThreadPriority.Normal;
            UpLocalThread.Name = "上相机右工位定位线程";
            UpLocalThread.IsBackground = true;
            UpLocalThread.Start();

            DnCamLocalThread = new Thread(new ParameterizedThreadStart(DnCamThread));
            DnCamLocalThread.Priority = ThreadPriority.Normal;
            DnCamLocalThread.Name = "右下相机定位线程";
            DnCamLocalThread.IsBackground = true;
            DnCamLocalThread.Start();
        }


        public void Stop()
        {
            DnCamThreadSwitch = false;
            UpCamThreadSwitch = false;

            UpCamLeftDnLocalIsOk = false; 
            UpCamRightDnLocalIsOk = false;
            RightDnLeftLocalIsOk = false;
            RightDnRightLocalIsOk = false;

        }



    }
}
