using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace VisionBase
{
   public  class DoVisionProcess

    {
        public  VisionPara MyVisionPara;
       
        public void  SetVisionPara(  VisionPara ParaIn   ) //初始化参数
        {
            MyVisionPara = ParaIn;
            MyLocalResult.TeachRow = ParaIn.localPara.localSetting.TeachImgLocal.Row;
            MyLocalResult.TeachCol = ParaIn.localPara.localSetting.TeachImgLocal.Col;
            MyLocalResult.angle = ParaIn.localPara.localSetting.TeachImgLocal.Angle;
        }


        public HalconDotNet.HObject GrabImg = new HalconDotNet.HObject();
        public LocalManager MyLocal = new LocalManager();

        public LocalResult MyLocalResult =new LocalResult();


        public St_VectorAngle TeachPosToRot = new St_VectorAngle();
        public  St_VectorAngle PosToRot = new St_VectorAngle();
        public virtual  void Do()
        {
            //1.打开光源
            FileLib.Logger.Pop("  打开光源：" ,false ,"运行日志");
            foreach (LightPara item in MyVisionPara.camLightPara.lightPara)
            {
                LightCtrlManager.Instance.SetLightValue(item);
            }         
            //2.采图
            if (GrabImg != null) GrabImg.Dispose();
            FileLib.Logger.Pop("  开始采图：", false, "运行日志");
            CameraCtrl.Instance.SetExpos(MyVisionPara.camLightPara.CamName, MyVisionPara.camLightPara.Exposure);
            //10.获取当前的机械坐标
            double X = 0, Y = 0, Z = 0, Theta = 0;
            if (CaliParaManager.Instance.GetCaliMode(MyVisionPara.localPara.localSetting.CoordiCam) == CaliModelEnum.HandEyeCali)
            {
                MotionManager.Instance.SetCoordi(MyVisionPara.localPara.localSetting.TeachCoordi);
                FileLib.Logger.Pop("当前的坐标系为：" + MyVisionPara.localPara.localSetting.TeachCoordi.ToString(), false, "运行日志");
                MotionManager.Instance.GetCoordiPos(out X, out Y, out Z, out Theta);
                FileLib.Logger.Pop("当前的机械坐标", false, "运行日志");
                FileLib.Logger.Pop("X：" + X.ToString("f4") + "  Y：" + Y.ToString("f4") +
                         "  Z：" + Z.ToString("f4") + "  Theta：" + Theta.ToString("f4"), false, "运行日志");
            }

            if (CameraCtrl.Instance.GrabImg(MyVisionPara.camLightPara.CamName, out GrabImg))
            {
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestCameraGrabed, 1); //拍照OK
                FileLib.Logger.Pop("  采图OK：", false, "运行日志");
            }
            else
            {
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestCameraGrabed, 2); //拍照NG
                FileLib.Logger.Pop("  采图NG：", false, "运行日志");
            }          
            ViewControl view1 = DisplaySystem.GetViewControl(CameraTest.UpCam1);
            if(MyVisionPara.camLightPara.CamName== CameraEnum.Cam0)
                view1 = DisplaySystem.GetViewControl(CameraTest.UpCam1);
            if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam1)
                view1 = DisplaySystem.GetViewControl(CameraTest.UpCam2);
            if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam2)
                view1 = DisplaySystem.GetViewControl(CameraTest.UpCam3);
            view1.ResetView();
            view1.Refresh();
            view1.AddViewImage(GrabImg);
            view1.Repaint();
            MyVisionBase.SaveImg(GrabImg,"扎针对位图片");
            //3.关闭光源
            LightCtrlManager.Instance.SetAllLightTo0();
            FileLib.Logger.Pop("  关闭光源：", false, "运行日志");
            //4.0设置定位模式
            MyLocal.SetLocalModel(MyVisionPara.localPara.localSetting.localModel);
            //5.0设置定位参数
            MyLocal.SetParam(GrabImg, MyVisionPara.localPara);
            //6.执行定位
            try
            {
                MyLocal.doLocal();
                //7.告诉PLC定位结果完成
                if (MyVisionPara.ProjectVisionItem == ProjectVisionEnum.ProjectVision1)
                {
                    FileLib.Logger.Pop(" 扎针右侧视觉定位完成，先不发给PLC信号，等计算出偏移量后，将信号发给PLC：", false, "运行日志");
                }
                else
                {
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestCamLocalResult, 1); //告诉PLC定位结果OK
                    FileLib.Logger.Pop("  告诉PLC拍照结果OK（告诉PLC定位结果OK）：", false, "运行日志");

                }

            }
            catch (Exception e0)
            {
                Logger.PopError1(e0, false,  "视觉错误日志");
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.ArtTestCamLocalResult, 2); //告诉PLC定位结果NG
                FileLib.Logger.Pop("  告诉PLC拍照结果OK（告诉PLC定位结果NG）：", false, "运行日志");
                MyLocalResult.IsLocalOk = false;
            }
          
         
            MyLocalResult = MyLocal.GetResult();
            view1.AddViewObject(MyLocalResult.ShowContour);
            //结合标定坐标计算出产品的实际位置
            //8.获取标定矩阵
            MyHomMat2D HomMat = new MyHomMat2D();
            HomMat = CaliParaManager.Instance.GetHomMAT(MyVisionPara.localPara.localSetting.CoordiCam); //获取示教的标定矩阵
            FileLib.Logger.Pop("  获取示教的标定矩阵：" + MyVisionPara.localPara.localSetting.CoordiCam.ToString(), false, "运行日志");
            FileLib.Logger.Pop(HomMat.ToString(), false, "运行日志");
            HalconDotNet.HTuple HHomMat = new HalconDotNet.HTuple();
            //标定矩阵的转换
            MyVisionBase.MyHomMatToHalcon(HomMat, out HHomMat);
            //9.图像坐标系的原点由左上角变到左下角
            MyVisionBase.AdjImgRow(GrabImg, ref MyLocalResult.row);
            St_VectorAngle PixelVector = new St_VectorAngle(MyLocalResult.row, MyLocalResult.col, MyLocalResult.angle);
            FileLib.Logger.Pop("当前的像素坐标", false, "运行日志");
            FileLib.Logger.Pop("Col：" + MyLocalResult.col.ToString("f4") + "  Row：" + MyLocalResult.row.ToString("f4") +
                      "  Theta：" + MyLocalResult.angle.ToString("f4"), false, "运行日志");

            CaliValue CaliPara = CaliParaManager.Instance.GetCaliValue(MyVisionPara.localPara.localSetting.CoordiCam);

            VectorAngle GrabPos = new VectorAngle(X, Y, Theta);
            //9 计算出当前产品距离旋转中心的坐标           
            EyeToHandPos.TransEyeToHandPos(PixelVector, CaliPara, GrabPos, out PosToRot);
            //10计算出示教产品到旋转中心的坐标
            St_VectorAngle TeachPixelPos = new St_VectorAngle(MyLocalResult.TeachRow, MyLocalResult.TeachCol, MyLocalResult.TeachAngle);
            VectorAngle TeachGrabPos = new VectorAngle(MyLocalResult.TeachX, MyLocalResult.TeachY, MyLocalResult.TeachTheta);
           
            MyVisionBase.AdjImgRow(GrabImg, ref TeachPixelPos.Row);
            EyeToHandPos.TransEyeToHandPos(TeachPixelPos, CaliPara, TeachGrabPos, out TeachPosToRot);
            view1.Repaint();
            view1.SetString(100, 100, "red", "PosToRot: " + "   X： " + PosToRot.Col.ToString("f3") + "   Y： " +
                PosToRot.Row.ToString("f3") + "   Theta： " + PosToRot.Angle.ToString("f3"));            
            MyLocalResult.PosToRot = PosToRot;
            MyLocalResult.TeachPosToRot = TeachPosToRot;
            FileLib.Logger.Pop("示教的产品相对旋转中心的偏移量", false, "运行日志");

            FileLib.Logger.Pop("PosToRot: " + "   X： " + PosToRot.Col.ToString("f3") + "   Y： " +
                   PosToRot.Row.ToString("f3") + "   Theta： " + PosToRot.Angle.ToString("f3"), false, "运行日志");

            FileLib.Logger.Pop("当前的产品相对旋转中心的偏移量", false, "运行日志");
            FileLib.Logger.Pop("PosToRot: " + "   X： " + TeachPosToRot.Col.ToString("f3") + "   Y： " +
                   TeachPosToRot.Row.ToString("f3") + "   Theta： " + TeachPosToRot.Angle.ToString("f3"), false, "运行日志");
            //11.计算出X Y Theta的补偿量，

            ////图像坐标转到机械坐标系
            //MyVisionBase.AffineTransPt(MyLocalResult.col, MyLocalResult.row, HHomMat, out MyLocalResult.x, out MyLocalResult.y);
            //// 
            //MyVisionBase.AffineTransPt(MyLocalResult.TeachCol, MyLocalResult.TeachRow, HHomMat, out MyLocalResult.TeachX, out MyLocalResult.TeachY);

        }


        


    }
}
