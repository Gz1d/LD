using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionBase
{
   public  class DoFofVisionProcess: DoVisionProcess  
   {

        public override void Do()
        {
            //1.打开光源
            foreach (LightPara item in MyVisionPara.camLightPara.lightPara)
            {
                LightCtrlManager.Instance.SetLightValue(item);
            }
            //2.采图
            if (GrabImg != null) GrabImg.Dispose();
            CameraCtrl.Instance.SetExpos(MyVisionPara.camLightPara.CamName, MyVisionPara.camLightPara.Exposure);
            if (CameraCtrl.Instance.GrabImg(MyVisionPara.camLightPara.CamName, out GrabImg))
            {
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_ok, 1); //拍照OK
            }
            else
            {
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_ok, 2); //拍照NG
            }
            ViewControl view1 = DisplaySystem.GetViewControl(CameraTest.UpCam1);
            if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam0)
                view1 = DisplaySystem.GetViewControl(CameraTest.UpCam1);
            if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam1)
                view1 = DisplaySystem.GetViewControl(CameraTest.UpCam2);
            if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam2)
                view1 = DisplaySystem.GetViewControl(CameraTest.UpCam3);
            view1.ResetView();
            view1.Refresh();
            view1.AddViewImage(GrabImg);
            view1.Repaint();
            MyVisionBase.SaveImg(GrabImg, "扎针对位图片");
            //3.关闭光源
            LightCtrlManager.Instance.SetAllLightTo0();
            //4.0设置定位模式
            MyLocal.SetLocalModel(MyVisionPara.localPara.localSetting.localModel);
            //5.0设置定位参数
            MyLocal.SetParam(GrabImg, MyVisionPara.localPara);
            //6.执行定位
            try
            {
                MyLocal.doLocal();
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_result, 1); //告诉PLC定位结果OK
            }
            catch (Exception e0)
            {
                Logger.PopError1(e0, false, "视觉日志");
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_result, 2); //告诉PLC定位结果OK
            }

            //7.告诉PLC定位结果完成

            MyLocalResult = MyLocal.GetResult();
            view1.AddViewObject(MyLocalResult.ShowContour);
            //结合标定坐标计算出产品的实际位置
            MyHomMat2D HomMat = new MyHomMat2D();
            HomMat = CaliParaManager.Instance.GetHomMAT(MyVisionPara.localPara.localSetting.CoordiCam); //获取示教的标定矩阵
            HalconDotNet.HTuple HHomMat = new HalconDotNet.HTuple();
            //标定矩阵的转换
            MyVisionBase.MyHomMatToHalcon(HomMat, out HHomMat);
            //图像坐标系的原点由左上角变到左下角
            MyVisionBase.AdjImgRow(GrabImg, ref MyLocalResult.row);
            St_VectorAngle PixelVector = new St_VectorAngle(MyLocalResult.row, MyLocalResult.col, MyLocalResult.angle);
            CaliValue CaliPara = CaliParaManager.Instance.GetCaliValue(MyVisionPara.localPara.localSetting.CoordiCam);

            //将像素坐标转换到标定坐标
            //MyLocalResult.AffineTransResult()
            HalconDotNet.HTuple HomMatCali = new HalconDotNet.HTuple();
            MyVisionBase.MyHomMatToHalcon(CaliPara.HomMat, out HomMatCali);

            MyVisionBase.AffineTransPt(MyLocalResult.col, MyLocalResult.row, HomMatCali,out MyLocalResult.x, out MyLocalResult.y); 

            //MyVisionBase.AffineTransPt()

            //获取当前坐标



            //double X = 0, Y = 0, Z = 0, Theta = 0;
            //if (CaliParaManager.Instance.GetCaliMode(MyVisionPara.localPara.localSetting.TeachCoordi) == CaliModelEnum.HandEyeCali)
            //{
            //    MotionManager.Instance.SetCoordi(MyVisionPara.localPara.localSetting.TeachCoordi);
            //    MotionManager.Instance.GetCoordiPos(out X, out Y, out Z, out Theta);
            //}
            //VectorAngle GrabPos = new VectorAngle(X, Y, Theta);
            ////9 计算出当前产品距离旋转中心的坐标           
            //EyeToHandPos.TransEyeToHandPos(PixelVector, CaliPara, GrabPos, out PosToRot);
            ////10计算出示教产品到旋转中心的坐标
            //St_VectorAngle TeachPixelPos = new St_VectorAngle(MyLocalResult.TeachRow, MyLocalResult.TeachCol, MyLocalResult.TeachAngle);
            //VectorAngle TeachGrabPos = new VectorAngle(MyLocalResult.TeachX, MyLocalResult.TeachY, MyLocalResult.TeachTheta);

            //MyVisionBase.AdjImgRow(GrabImg, ref TeachPixelPos.Row);
            //EyeToHandPos.TransEyeToHandPos(TeachPixelPos, CaliPara, TeachGrabPos, out TeachPosToRot);
            //view1.Repaint();
            //view1.SetString(100, 100, "red", "PosToRot: " + "   X： " + PosToRot.Col.ToString("f3") + "   Y： " +
            //    PosToRot.Row.ToString("f3") + "   Theta： " + PosToRot.Angle.ToString("f3"));
            //MyLocalResult.PosToRot = PosToRot;
            //MyLocalResult.TeachPosToRot = TeachPosToRot;


        }




    }
}
