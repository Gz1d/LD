using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionBase
{
    public  class DoUpDnVisionProcess : DoVisionProcess
    {
        //ViewControl view1 = new ViewControl();

        HalconDotNet.HObject FilterImg = new HalconDotNet.HObject();
        public bool  StartGrabImg()
        {
            //1.打开光源
            FileLib.Logger.Pop("  打开光源：", false, "FOF运行日志");
            foreach (LightPara item in MyVisionPara.camLightPara.lightPara)
            {
                LightCtrlManager.Instance.SetLightValue(item);
            }
            //2.采图
            FileLib.Logger.Pop("  开始采图：", false, "FOF运行日志");
            if (GrabImg != null) GrabImg.Dispose();
            CameraCtrl.Instance.SetExpos(MyVisionPara.camLightPara.CamName, MyVisionPara.camLightPara.Exposure);
            CameraCtrl.Instance.GrabImg(MyVisionPara.camLightPara.CamName, out GrabImg);
            ViewControl view0 = new ViewControl();
            view0 = DisplaySystem.GetViewControl(CameraTest.UpCam2);
            if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam2)
            {
                view0 = DisplaySystem.GetViewControl(CameraTest.UpCam3);
            }
            view0.ResetView();
            view0.Refresh();

            //3.关闭光源
            LightCtrlManager.Instance.SetAllLightTo0();
            FileLib.Logger.Pop("  关闭光源：", false, "FOF运行日志");
            return true;
        }

        public override void Do()
        {
            //1.打开光源
            FileLib.Logger.Pop("  打开光源：", false, "FOF运行日志");
            foreach (LightPara item in MyVisionPara.camLightPara.lightPara)
            {
                LightCtrlManager.Instance.SetLightValue(item);
            }
            //2.采图
            FileLib.Logger.Pop("  开始采图：", false, "FOF运行日志");
            if (GrabImg != null) GrabImg.Dispose();
            if (FilterImg != null) FilterImg.Dispose();
            CameraCtrl.Instance.SetExpos(MyVisionPara.camLightPara.CamName, MyVisionPara.camLightPara.Exposure);
            if (CameraCtrl.Instance.GrabImg(MyVisionPara.camLightPara.CamName, out GrabImg))
            {
                if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam2) 
                LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_ok, 1); //拍照OK
                FileLib.Logger.Pop("  采图OK：", false, "FOF运行日志");
            }
            else
            {
                if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam2)
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_ok, 2); //拍照NG
                FileLib.Logger.Pop("  采图NG：", false, "FOF运行日志");
            }
            ViewControl view1 = new ViewControl();
            view1 = DisplaySystem.GetViewControl(CameraTest.UpCam2);
            if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam2)
            {
                view1 = DisplaySystem.GetViewControl(CameraTest.UpCam3);
            }
            view1.ResetView();
            view1.Refresh();

            //3.关闭光源
            //LightCtrlManager.Instance.SetAllLightTo0();
            //FileLib.Logger.Pop("  关闭光源：", false, "FOF运行日志");

            //4.0设置定位模式
            MyLocal.SetLocalModel(MyVisionPara.localPara.localSetting.localModel);
            //5.0设置定位参数
            FileLib.Logger.Pop("  开始偏移检测：", false, "FOF运行日志");
            MyVisionBase.SaveImg(GrabImg, "FOF偏移检测图片");

            
            if (MyVisionPara.camLightPara.IsFilter)
            {               
                FileLib.Logger.Pop("  开始频域滤波，FilterC："+ MyVisionPara.camLightPara.FilterC.ToString(), false, "FOF运行日志");
                MyVisionBase.FilterImg(GrabImg, out FilterImg, MyVisionPara.camLightPara.FilterC);
                view1.AddViewImage(FilterImg);
                MyVisionBase.SaveImg(FilterImg, "FOF偏移检测图片");
                MyLocal.SetParam(FilterImg, MyVisionPara.localPara);
            }
            else
            {
                view1.AddViewImage(GrabImg);
                MyLocal.SetParam(GrabImg, MyVisionPara.localPara);
            }
            
            view1.Repaint();           
            //6.执行定位
            try
            {
                FileLib.Logger.Pop("  开始定位：", false, "FOF运行日志");
                MyLocal.doLocal();
                if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam2)
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_result, 1);
                else if (MyVisionPara.localPara.localSetting.localModel == LocalModelEnum.TempBlob)
                {
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_result, 1);
                }
                FileLib.Logger.Pop("  告诉PLC拍照结果OK（告诉PLC定位结果OK）：",false, "FOF运行日志");
            }
            catch (Exception e0)
            {
                if (MyVisionPara.camLightPara.CamName == CameraEnum.Cam2)
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_result, 2);
                else if (MyVisionPara.localPara.localSetting.localModel == LocalModelEnum.TempBlob)
                {
                    LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.FOF_Grabed_result, 2);
                }
                Logger.PopError1(e0, false, "视觉错误日志");
                FileLib.Logger.Pop("  告诉PLC拍照结果OK（告诉PLC定位结果NG）：",false, "FOF运行日志");
            }        
            MyLocalResult = MyLocal.GetResult();
            view1.AddViewObject(MyLocalResult.ShowContour);
            view1.Repaint();
            //8.结合标定坐标计算出产品的实际位置
            MyHomMat2D HomMat = new MyHomMat2D();
            HomMat = CaliParaManager.Instance.GetHomMAT(MyVisionPara.localPara.localSetting.CoordiCam); //获取示教的标定矩阵
            FileLib.Logger.Pop("  获取标定矩阵(CoordiEnumItem)：" + MyVisionPara.localPara.localSetting.CoordiCam.ToString(),
                false, "FOF运行日志");
            FileLib.Logger.Pop(HomMat.ToString(),false, "FOF运行日志");
            HalconDotNet.HTuple HHomMat = new HalconDotNet.HTuple();
            //9.标定矩阵的转换
            MyVisionBase.MyHomMatToHalcon(HomMat, out HHomMat);
            //10.图像坐标系的原点由左上角变到左下角
            MyVisionBase.AdjImgRow(GrabImg, ref MyLocalResult.row);
            MyVisionBase.AffineTransPt(MyLocalResult.col, MyLocalResult.row, HHomMat, out MyLocalResult.x, out MyLocalResult.y);
            FileLib.Logger.Pop("col: " + MyLocalResult.col.ToString("f3") +  " row: " + MyLocalResult.row.ToString("f3"),false, "FOF运行日志");
            FileLib.Logger.Pop("X: "  +  MyLocalResult.x.ToString("f3")   +  " Y:   "   + MyLocalResult.y.ToString("f3"),false, "FOF运行日志");
            MyLocalResult.Theta = MyLocalResult.angle;
            if(FilterImg!=null) FilterImg.Dispose();
            if (GrabImg != null) GrabImg.Dispose();

        }








    }
}
