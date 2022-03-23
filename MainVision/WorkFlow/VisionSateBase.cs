using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionBase;
using HalconDotNet;
namespace MainVision
{
    public  class VisionStateBase
    {
        public ViewControl view1;

        public VisionPara MyVisionPara;


        public LocalResult myLocalrlt = new LocalResult();

        private LocalManager myLocalManager = new LocalManager();

        public HalconDotNet.HObject GrabImg = new HalconDotNet.HObject();

        /// <summary> 示教产品到旋转中心的位置 </summary>
        public St_VectorAngle TeachPosToRot = new St_VectorAngle();
        /// <summary> 当前产品到旋转中心的位置 </summary>
        public St_VectorAngle PosToRot = new St_VectorAngle();
        public string StationDescribe = "";
        /// <summary>
        /// 运动控制接口
        /// </summary>
        public MotionManager MotionCtrl;
        /// <summary>
        /// 对应坐标系的标定信息
        /// </summary>
        private  CaliValue CaliPara;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visionParaIn"></param>
        /// <param name="StateDescribeIn"></param>
        public VisionStateBase(VisionPara visionParaIn ,string  StateDescribeIn,ViewControl viewIn)
        {
            MyVisionPara = visionParaIn;
            myLocalManager.SetLocalModel(visionParaIn.localPara.localSetting.localModel);
            view1 = viewIn;
            myLocalrlt.TeachRow = visionParaIn.localPara.localSetting.TeachImgLocal.Row;
            myLocalrlt.TeachCol = visionParaIn.localPara.localSetting.TeachImgLocal.Col;
            myLocalrlt.angle = visionParaIn.localPara.localSetting.TeachImgLocal.Angle;

            MotionCtrl = new MotionManager();
            MotionCtrl.SetCoordi(visionParaIn.localPara.localSetting.TeachCoordi);

            //CaliPara = CaliParaManager.Instance.GetCaliValue(MyVisionPara.localPara.localSetting.TeachCoordi);
            CaliPara = CaliParaManager.Instance.GetCaliValue(MyVisionPara.localPara.localSetting.CoordiCam);
        }


        public void SetCamLight()
        {

            ////1.关闭光源
            //LightCtrlManager.Instance.SetAllLightTo0();
            //FileLib.Logger.Pop("  关闭光源：", false, StationDescribe + "运行日志");

            //2.打开光源
            FileLib.Logger.Pop("  打开光源：", false, StationDescribe + "运行日志");
            foreach (LightPara item in MyVisionPara.camLightPara.lightPara)
            {
                LightCtrlManager.Instance.SetLightValue(item);
            }
            //3.设置相机参数
           
            FileLib.Logger.Pop("  设置相机曝光时间：", false, StationDescribe + "运行日志");
            CameraCtrl.Instance.SetExpos(MyVisionPara.camLightPara.CamName, MyVisionPara.camLightPara.Exposure);


        }

        public bool GrabingImg()
        {
            if (GrabImg != null) GrabImg.Dispose();
            FileLib.Logger.Pop("  开始采图：", false, StationDescribe + "运行日志");
            this.MotionCtrl.SetCoordi(MyVisionPara.localPara.localSetting.TeachCoordi);
            MotionCtrl.GetCoordiPos(out X, out Y, out Z, out Theta);
            return CameraCtrl.Instance.GrabImg(MyVisionPara.camLightPara.CamName, out GrabImg);          
        }

        public bool DoLocal()
        {
            FileLib.Logger.Pop("  采图完成开始执行定位：", false, StationDescribe + "运行日志");
            myLocalManager.SetParam(GrabImg, MyVisionPara.localPara);

            myLocalManager.doLocal();
            myLocalrlt =   myLocalManager.GetResult();
            FileLib.Logger.Pop("  定位完成，返回定位结果：", false, StationDescribe + "运行日志");
            return true;
        }


        public bool ShowRlt()
        {
            view1.ResetView();
            view1.SetDraw("blue", "margin");
            view1.AddViewImage(GrabImg);
            view1.AddViewObject(myLocalrlt.ShowContour);
            return true;
        }

        //获取当前的机械坐标
        double X = 0, Y = 0, Z = 0, Theta = 0;
        public LocalResult GetLocalRlt()
        {

            //.图像坐标系的原点由左上角变到左下角
            MyVisionBase.AdjImgRow(GrabImg, ref myLocalrlt.row);
            St_VectorAngle PixelVector = new St_VectorAngle(myLocalrlt.row, myLocalrlt.col, myLocalrlt.angle);
            VectorAngle GrabPos = new VectorAngle(X, Y, Theta);
            // 计算出当前产品距离旋转中心的坐标   
            EyeToHandPos.TransEyeToHandPos(PixelVector, CaliPara, GrabPos, out PosToRot);

            //计算出示教产品到旋转中心的坐标
            St_VectorAngle TeachPixelPos = new St_VectorAngle(myLocalrlt.TeachRow, myLocalrlt.TeachCol, myLocalrlt.TeachAngle);
            VectorAngle TeachGrabPos = new VectorAngle(myLocalrlt.TeachX, myLocalrlt.TeachY, myLocalrlt.TeachTheta);

            MyVisionBase.AdjImgRow(GrabImg, ref TeachPixelPos.Row);
            EyeToHandPos.TransEyeToHandPos(TeachPixelPos, CaliPara, TeachGrabPos, out TeachPosToRot);

            myLocalrlt.PosToRot = PosToRot;
            myLocalrlt.TeachPosToRot = TeachPosToRot;
            return myLocalrlt;

        }

    }
}
