using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionBase
{
    public class DoProductProcess
    {
        private static  object LockObj = new object();
        private static  DoProductProcess _instance;
        public static  DoProductProcess Instance
        {
            get {
                lock (LockObj){
                    return _instance= _instance ?? new DoProductProcess();
                }
            }
        }
        ProjectPara NowProjectPara =new ProjectPara();
        VisionPara NowVisionPara = new VisionPara();
        DoVisionProcess NowVisionProcess = new DoVisionProcess();
        LocalResult FirstLocalResult = new LocalResult();
        LocalResult SecondLocalResult = new LocalResult();

        /// <summary>
        /// 上相机视觉处理流程
        /// </summary>
        DoVisionProcess NowUpVisionProcess = new DoUpDnVisionProcess();
        LocalResult NowUpLocalRlt = new LocalResult();
        /// <summary>
        /// 下相机视觉处理流程
        /// </summary>
        DoVisionProcess NowDnVisionProcess = new DoUpDnVisionProcess();
        LocalResult NowDnLocalRlt = new LocalResult();
        /// <summary>
        /// 扎针定位
        /// </summary>
        /// <param name="PlcItem"></param>
        /// <param name="Obj"></param>
        public void DoProject(LD.Config.PlcDataItem PlcItem, Object Obj)
        {
            LD.Common.PlcDevice plcDevice = PlcItem.PlcDevice;
            if (plcDevice == LD.Common.PlcDevice.V_01_TriggerGrab)  //
            {
                NowVisionProcess = new DoVisionProcess();
                //起始信号清零
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_01_TriggerGrab, 0);
                FileLib. Logger.Pop("接受到PLC信号，开始扎针对位",false, "运行日志");
                short ClearValue = 1;
                object obj = new object();
                while (true){
                    obj = LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.V_01_TriggerGrab);
                    ClearValue = (short)obj;
                    if(ClearValue == 0) break;
                    else  System.Threading.Thread.Sleep(10);
                }                
                System.Threading.Thread.Sleep(1);
                //读取机台编号
                int StageNum =(int)LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestStageNumRead);
                FileLib.Logger.Pop("  读取机台编号："+ StageNum.ToString(), false, "运行日志");
                System.Threading.Thread.Sleep(1);
                //读取拍照位编号
                short GrabNum = (short)LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestGrabNum);
                System.Threading.Thread.Sleep(1);
                //获取视觉示教的定位参数
                NowProjectPara = ProjectParaManager.Instance.GetProjectPara(StageNum);
                //获取当前拍照点的视觉坐标
                 NowVisionPara = NowProjectPara.GetVisionPara(GrabNum);
                 NowVisionProcess.SetVisionPara(NowVisionPara);         
                NowVisionProcess.Do();
                if (GrabNum == 0){
                    FirstLocalResult = NowVisionProcess.MyLocalResult;
                    FileLib.Logger.Pop(" 左边拍照定位完成，开始右边拍照定位：", false, "运行日志");
                }
                else{
                    SecondLocalResult = NowVisionProcess.MyLocalResult;
                    //示教点到旋转中心的坐标
                    Point2Db TeachPt2d1 = new Point2Db(FirstLocalResult.TeachPosToRot.Col, FirstLocalResult.TeachPosToRot.Row);
                    Point2Db TeachPt2d2 = new Point2Db(SecondLocalResult.TeachPosToRot.Col, SecondLocalResult.TeachPosToRot.Row);
                    FileLib.Logger.Pop(" 示教产品到旋转中心的坐标(左右两点)："  + TeachPt2d1.Col.ToString("f3") + "  " + TeachPt2d1.Row.ToString("f3") +"  "
                                  + TeachPt2d2.Col.ToString("f3") + "  " + TeachPt2d2.Row.ToString("f3"), false, "运行日志");
                    //当前产品到旋转中心的坐标
                    Point2Db NowPosPt2d1 = new Point2Db(FirstLocalResult.PosToRot.Col, FirstLocalResult.PosToRot.Row);
                    Point2Db NowPosPt2d2 = new Point2Db(SecondLocalResult.PosToRot.Col, SecondLocalResult.PosToRot.Row);
                    FileLib.Logger.Pop(" 当前产品到旋转中心的坐标(左右两点)：" + NowPosPt2d1.Col.ToString("f3") + "  " + NowPosPt2d1.Row.ToString("f3") + "  "
                                 + NowPosPt2d2.Col.ToString("f3") + "  " + NowPosPt2d2.Row.ToString("f3"), false, "运行日志");
                    List<Point2Db> TeachPtList = new List<Point2Db>();
                    List<Point2Db> NowPtList = new List<Point2Db>();
                    //当前产品移动到产品示教时的位置，先计算出偏移矩阵
                    TeachPtList.Add(TeachPt2d1);
                    TeachPtList.Add(TeachPt2d2);
                    NowPtList.Add(NowPosPt2d1);
                    NowPtList.Add(NowPosPt2d2);
                    MyHomMat2D NowHomat = new MyHomMat2D();
                    bool IsTrue = false;
                    MyVisionBase.VectorToRigidHomMat(NowPtList, TeachPtList, out NowHomat,out  IsTrue);
                    double AddX = 0, AddY = 0, AddTheta = 0;
                    MyVisionBase.CalculateThreeTapePos(NowPosPt2d1, NowPosPt2d2, TeachPt2d1, TeachPt2d2, out AddX, out AddY, out AddTheta);
                    FileLib.Logger.Pop(" 计算出的偏移补偿量："+ AddX.ToString("f3")+"  "+  AddY.ToString("f3") +"  "+AddTheta.ToString("f3"), false, "运行日志");
                    ///再根据偏移矩阵计算出偏移量
                    double AddX1 = 0, AddY1 = 0, AddTheta1 = 0;
                    MyVisionBase.HomMat2dToAffinePara(NowHomat, out AddTheta1, out AddY1, out AddX1);
                     ViewControl view1 = DisplaySystem.GetViewControl(CameraTest.UpCam1);
                    view1.SetString(500, 500, "red", "   AddX： " + AddX.ToString("f4") + " mm ");
                    view1.SetString(500, 600, "red", "   AddY： " + AddY.ToString("f4") + " mm ");
                    view1.SetString(500, 700, "red", "   AddTheta： " + AddTheta.ToString("f4") + " 度 ");
                    ///
                    double  addx0 = -AddX * 1000;
                    double addy0 = AddY * 1000;
                    double addtheta0 = AddTheta * 1000;
                    int addx = (int)addx0;
                    int addy = (int)addy0;
                    int addtheta = (int)addtheta0;
                    NowVisionPara = NowProjectPara.GetVisionPara(0);
                    int offsetx = (int)(NowVisionPara.localPara.localSetting.Offset_x * 1000);
                    int offsety = (int)(NowVisionPara.localPara.localSetting.Offset_y * 1000);
                    int  offsettheta = (int)(NowVisionPara.localPara.localSetting.Offset_theta * 1000);

                    int newAddX = addx + offsetx;
                    int NewAddY = addy + offsety;
                    int NewAddTheta = -addtheta + offsettheta;
                    if (StageNum == 0|| StageNum == 2)//左边两个平台X轴为正常方向，后面两个平台为反方向
                    {
                        newAddX = addx + offsetx;
                    }
                    else{
                        newAddX = -addx + offsetx;
                    }
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddX, newAddX);
                    System.Threading.Thread.Sleep(5);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddY, NewAddY);
                    System.Threading.Thread.Sleep(5);
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddTheta, NewAddTheta);//图像坐标系为逆时针，机械坐标系为顺时针
                    System.Threading.Thread.Sleep(5);
                    int ReadAddX = 0;
                    int ReadAddY = 0;
                    int ReadAddTheta = 0;
                    object ObjRead = 0;
                    int WriteCount = 0;
                    while (true){
                        ObjRead =LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestAddX);
                        System.Threading.Thread.Sleep(5);
                        ReadAddX = (int)ObjRead;
                        ObjRead = LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestAddY);
                        System.Threading.Thread.Sleep(5);
                        ReadAddY = (int)ObjRead;
                        ObjRead = LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestAddTheta);
                        System.Threading.Thread.Sleep(5);
                        ReadAddTheta = (int)ObjRead;
                        if (ReadAddX == newAddX && ReadAddY == NewAddY && ReadAddTheta == NewAddTheta){
                            FileLib.Logger.Pop(" 补偿值成功写入", false, "运行日志");
                            break;
                        }
                        else{
                            WriteCount++;
                            if (WriteCount % 10 == 0){
                                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddX, newAddX);
                                System.Threading.Thread.Sleep(5);
                                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddY, NewAddY);
                                System.Threading.Thread.Sleep(5);
                                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddTheta, NewAddTheta);//图像坐标系为逆时针，机械坐标系为顺时针
                                System.Threading.Thread.Sleep(5);
                            }
                            if (WriteCount > 300) break;
                        }
                    }
                    if (SecondLocalResult.IsLocalOk){
                        LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestCamLocalResult, 1); //告诉PLC定位结果OK
                        FileLib.Logger.Pop(" 告诉PLC补偿量已经发给PLC：", false, "运行日志");
                    }                   
                }                              
            }
           
        }

        /// <summary>
        /// FOF偏移检测
        /// </summary>
        /// <param name="PlcItem"></param>
        /// <param name="Obj"></param>
        public void DoFOFProject(LD.Config.PlcDataItem PlcItem, Object Obj)
        {
            LD.Common.PlcDevice plcDevice = PlcItem.PlcDevice;          
            if (plcDevice == LD.Common.PlcDevice.V_02_TriggerGrab){
                FileLib.Logger.Pop("接受到PLC信号，偏移检测",false, "FOF运行日志");
                NowUpVisionProcess = new DoUpDnVisionProcess();
                NowUpLocalRlt = new LocalResult();
                NowDnLocalRlt = new LocalResult();
                //起始信号清零
                FileLib.Logger.Pop("启动信号清零", false, "FOF运行日志");
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_02_TriggerGrab, 0);
                System.Threading.Thread.Sleep(1);
                short ClearValue = 1;
                object obj = new object();
                FileLib.Logger.Pop("启动地址清零成功", false, "FOF运行日志");
                //读取机台编号 4 代表FOF左侧FOF偏移检测 ，5代表FOF右侧偏移检测
                int StageNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestStageNumRead);
                FileLib.Logger.Pop("  读取机台编号：" + StageNum.ToString(), false, "FOF运行日志");
                System.Threading.Thread.Sleep(1);
                //获取视觉示教的定位参数
                NowProjectPara = ProjectParaManager.Instance.GetProjectPara(StageNum);
                //获取当前拍照点的视觉坐标
                double OffsetX = 0;
                double OffsetY = 0;
                try {
                    FileLib.Logger.Pop("开始FOF偏移检测上相机定位", false, "FOF运行日志");
                    NowVisionPara = NowProjectPara.GetVisionPara(0);
                    NowUpVisionProcess.SetVisionPara(NowVisionPara);
                    NowUpVisionProcess.Do();
                    NowUpLocalRlt = NowUpVisionProcess.MyLocalResult;
                    FileLib.Logger.Pop("FOF偏移检测上相机定位完成", false, "FOF运行日志");
                    FileLib.Logger.Pop("开始FOF偏移检测下相机定位", false, "FOF运行日志");
                    NowVisionPara = NowProjectPara.GetVisionPara(1);
                    NowDnVisionProcess = new DoUpDnVisionProcess();
                    NowDnVisionProcess.SetVisionPara(NowVisionPara);
                    NowDnVisionProcess.Do();
                    NowDnLocalRlt = NowDnVisionProcess.MyLocalResult;
                    FileLib.Logger.Pop("开始FOF偏移检测下相机定位完成", false, "FOF运行日志");
                    NowVisionPara = NowProjectPara.GetVisionPara(0);
                    OffsetX = (NowUpLocalRlt.x - NowDnLocalRlt.x)*1.20 + NowVisionPara.localPara.localSetting.Offset_x;
                    OffsetY = (NowUpLocalRlt.y - NowDnLocalRlt.y)*1.20 + NowVisionPara.localPara.localSetting.Offset_y;
                    FileLib.Logger.Pop("偏移量： "+ "OffsetX: " + OffsetX.ToString("f4") +"  " + "OffsetY: " + OffsetY.ToString("f4"), false, "FOF运行日志");
                }
                catch(Exception e0){
                    Logger.PopError1(e0, false, "视觉错误日志");
                    OffsetX = 999999;
                    OffsetY = 999999;
                }
                ViewControl view1 = DisplaySystem.GetViewControl(CameraTest.UpCam2);
                view1.SetString(500, 500, "red", "OffsetX:  " + OffsetX.ToString("f4") + " mm");
                view1.SetString(500, 600, "red", "OffsetY:  " + OffsetY.ToString("f4") + " mm");

                if (Math.Abs(OffsetX )< NowVisionPara.localPara.localSetting.Offset_x_range && Math.Abs(OffsetY) < 
                    NowVisionPara.localPara.localSetting.Offset_y_range) {
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_inspect_result, 1); //检测结果Ok  
                    FileLib.Logger.Pop("检测结果写给Plc(FOF_inspect_result)：OK", false, "FOF运行日志");
                }
                else {
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_inspect_result, 2); //检测结果NG
                    FileLib.Logger.Pop("检测结果写给Plc(FOF_inspect_result)：NG", false, "FOF运行日志");
                }
                FileLib.Logger.Pop("循环读取启动地址，保证清零成功", false, "FOF运行日志");
                int ReadCount = 1;
                while (true) {
                    obj = LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.V_02_TriggerGrab);
                    ClearValue = (short)obj;
                    if (ClearValue == 0)   break;
                    else { 
                        System.Threading.Thread.Sleep(10);
                        if (ReadCount % 10 == 0) {
                            FileLib.Logger.Pop("重新执行一次清零", false, "FOF运行日志");
                            ReadCount = 1;
                            LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_02_TriggerGrab, 0);
                        }
                        if(ReadCount>100) FileLib.Logger.Pop("清零次数超过10，清零失败", false, "FOF运行日志");
                    }
                    ReadCount++;
                }
                FileLib.Logger.Pop("将偏移量写给PLC", false, "FOF运行日志");
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_offset_x, (int)(OffsetX*1000));
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_offset_y, (int)(OffsetY * 1000));
                FileLib.Logger.Pop("偏移量写给PLC完成", false, "FOF运行日志");
                FileLib.Logger.Pop("。", false, "FOF运行日志");
                FileLib.Logger.Pop("。", false, "FOF运行日志");
                FileLib.Logger.Pop("。", false, "FOF运行日志");
            }
        }
        public void DoFOFProject1(LD.Config.PlcDataItem PlcItem, Object Obj)
        {
            LD.Common.PlcDevice plcDevice = PlcItem.PlcDevice;
            if (plcDevice == LD.Common.PlcDevice.V_02_TriggerGrab)  {
                FileLib.Logger.Pop("接受到PLC信号，偏移检测", false, "FOF运行日志");
                NowUpVisionProcess = new DoUpDnVisionProcess();
                NowUpLocalRlt = new LocalResult();
                NowDnLocalRlt = new LocalResult();
                //起始信号清零
                FileLib.Logger.Pop("启动信号清零", false, "FOF运行日志");
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_02_TriggerGrab, 0);
                System.Threading.Thread.Sleep(1);
                short ClearValue = 1;
                object obj = new object();

                FileLib.Logger.Pop("启动地址清零成功", false, "FOF运行日志");
                //读取机台编号 4 代表FOF左侧FOF偏移检测 ，5代表FOF右侧偏移检测
                int StageNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestStageNumRead);
                FileLib.Logger.Pop("  读取机台编号：" + StageNum.ToString(), false, "FOF运行日志");
                System.Threading.Thread.Sleep(1);
                //获取视觉示教的定位参数
                NowProjectPara = ProjectParaManager.Instance.GetProjectPara(StageNum);
                //获取当前拍照点的视觉坐标
                double OffsetX = 0;
                double OffsetY = 0;
                try {
                    FileLib.Logger.Pop("开始FOF偏移检测上相机定位", false, "FOF运行日志");
                    NowVisionPara = NowProjectPara.GetVisionPara(0);
                    NowUpVisionProcess.SetVisionPara(NowVisionPara);
                    NowUpVisionProcess.Do();
                    NowUpLocalRlt = NowUpVisionProcess.MyLocalResult;
                    BlobLocalRlt FofRlt = (BlobLocalRlt)NowUpLocalRlt;
                    double x = FofRlt.ListWid[0] * NowVisionPara.localPara.Blobs.PixelSize;
                    double y = FofRlt.ListHei[1] * NowVisionPara.localPara.Blobs.PixelSize;
                    OffsetX = x + NowVisionPara.localPara.localSetting.Offset_x;
                    OffsetY = y + NowVisionPara.localPara.localSetting.Offset_y;
                    FileLib.Logger.Pop("偏移量： " + "OffsetX: " + OffsetX.ToString("f4") + "  " + "OffsetY: " 
                        + OffsetY.ToString("f4"), false, "FOF运行日志");
                }
                catch (Exception e0) {
                    Logger.PopError1(e0, false, "视觉错误日志");
                    OffsetX = 999999;
                    OffsetY = 999999;
                }
                ViewControl view1 = DisplaySystem.GetViewControl(CameraTest.UpCam2);
                view1.SetString(500, 500, "red", "OffsetX:  " + OffsetX.ToString("f4") + " mm");
                view1.SetString(500, 600, "red", "OffsetY:  " + OffsetY.ToString("f4") + " mm");

                if (Math.Abs(OffsetX) < NowVisionPara.localPara.localSetting.Offset_x_range && Math.Abs(OffsetY) 
                    < NowVisionPara.localPara.localSetting.Offset_y_range){
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_inspect_result, 1); //检测结果Ok  
                    FileLib.Logger.Pop("检测结果写给Plc(FOF_inspect_result)：OK", false, "FOF运行日志");
                }
                else
                {
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_inspect_result, 2); //检测结果NG
                    FileLib.Logger.Pop("检测结果写给Plc(FOF_inspect_result)：NG", false, "FOF运行日志");
                }
                FileLib.Logger.Pop("循环读取启动地址，保证清零成功", false, "FOF运行日志");
                int ReadCount = 1;
                while (true)
                {
                    obj = LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.V_02_TriggerGrab);
                    ClearValue = (short)obj;
                    if (ClearValue == 0)   break;
                    else {
                        System.Threading.Thread.Sleep(10);
                        if (ReadCount % 10 == 0) {
                            FileLib.Logger.Pop("重新执行一次清零", false, "FOF运行日志");
                            ReadCount = 1;
                            LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_02_TriggerGrab, 0);
                        }
                        if (ReadCount > 100) FileLib.Logger.Pop("清零次数超过10，清零失败", false, "FOF运行日志");
                    }
                    ReadCount++;
                }
                FileLib.Logger.Pop("将偏移量写给PLC", false, "FOF运行日志");
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_offset_x, (int)(OffsetX * 1000));
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_offset_y, (int)(OffsetY * 1000));
                FileLib.Logger.Pop("偏移量写给PLC完成", false, "FOF运行日志");
                FileLib.Logger.Pop("。", false, "FOF运行日志");
                FileLib.Logger.Pop("。", false, "FOF运行日志");
                FileLib.Logger.Pop("。", false, "FOF运行日志");
            }
        }



        public void NewDoProject(LD.Config.PlcDataItem PlcItem, Object Obj )
        {
            LD.Common.PlcDevice plcDevice = PlcItem.PlcDevice;
            if (plcDevice == LD.Common.PlcDevice.ArtTestCameraStart)  //
            {
                //读取机台编号
                int StageNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestStageNumRead);
                //读取拍照位编号
                int GrabNum = (int)LD.Logic.PlcHandle.Instance.ReadValue(LD.Common.PlcDevice.ArtTestGrabNum);
                //获取视觉示教的定位参数
                NowProjectPara = ProjectParaManager.Instance.GetProjectPara(StageNum);
                //获取当前拍照点的视觉坐标
                NowVisionPara = NowProjectPara.GetVisionPara(GrabNum);
                NowVisionProcess.SetVisionPara(NowVisionPara);
                NowVisionProcess.Do();
                FirstLocalResult = NowVisionProcess.MyLocalResult;
                St_VectorAngle PosToRot = NowVisionProcess.PosToRot;
                St_VectorAngle TeachPosToRot = NowVisionProcess.TeachPosToRot;
                St_VectorAngle AddVector = new St_VectorAngle();
                MyVisionBase.VectorAngleToMotionXYTh(PosToRot, TeachPosToRot, out AddVector);
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddX, -AddVector.Col);
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddY, AddVector.Row);
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestAddTheta, -AddVector.Angle);
                ViewControl view1 = DisplaySystem.GetViewControl(CameraTest.UpCam1);               
            }
            
        }

    }
}
