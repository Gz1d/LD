using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace VisionBase
{
    public static  class EyeToHandPos
    {
        /// <summary>
        /// 像素坐标转换到机械坐标或者世界坐标系,当标定模式为CaliModelEnum.HandEyeCali时，算出的坐标为相对于旋转中心的坐标，
        /// 物体相对于机械坐标原点的坐标：计算出的坐标+当前拍照的坐标。
        /// </summary>
        /// <param name="PixelVector">图像处理定位出来的像素坐标 </param>
        /// <param name="CaliValueIn">标定参数，包含标定的起点，标定的终点，标定矩阵，相机是否挂在XY轴上</param>
        /// <param name="GrabImgPosPt3d">采集图像时的机械坐标</param>
        /// <param name="WorldVector"></param>
        public static void  TransEyeToHandPos( St_VectorAngle PixelVector ,CaliValue CaliValueIn, 
            VectorAngle GrabImgPosPt3d,   out St_VectorAngle WorldVector)
        {
            WorldVector = new St_VectorAngle();
            double X = 0, Y = 0;
            HTuple HomMat = new HTuple();
            MyVisionBase.MyHomMatToHalcon(CaliValueIn.HomMat, out HomMat); //

            VectorAngle CaliMidPt = new VectorAngle();  //标定起始点到标定终点的中点
            CaliMidPt.X = 0.5*( CaliValueIn.StartCaliPt.x + CaliValueIn.EndCaliPt.x);
            CaliMidPt.Y = 0.5 *(CaliValueIn.StartCaliPt.y + CaliValueIn.EndCaliPt.y);
            double AddX = 0, AddY = 0;  //拍照坐标不和标定中心坐标重合时，带来的偏移量
            if (CaliValueIn.IsMoveX && !CaliValueIn.IsMoveY){ //相机随着X轴移动
                AddX = GrabImgPosPt3d.X - CaliMidPt.X;
                AddY = -(GrabImgPosPt3d.Y - CaliMidPt.Y);
            }
            else if (!CaliValueIn.IsMoveX && CaliValueIn.IsMoveY) { //相机随着Y轴移动
                AddX = -(GrabImgPosPt3d.X - CaliMidPt.X);
                AddY = (GrabImgPosPt3d.Y - CaliMidPt.Y);
            }
            else if (!CaliValueIn.IsMoveX && !CaliValueIn.IsMoveY) { //相机静止
                AddX = -(GrabImgPosPt3d.X - CaliMidPt.X);
                AddY = -(GrabImgPosPt3d.Y - CaliMidPt.Y);
            }
            else if (CaliValueIn.IsMoveX && CaliValueIn.IsMoveY) { //相机随着XY轴移动
                AddX = (GrabImgPosPt3d.X - CaliMidPt.X);
                AddY = (GrabImgPosPt3d.Y - CaliMidPt.Y);
            }
            //像素坐标转换成当前的世界坐标，
            MyVisionBase.AffineTransPt(PixelVector.Col, PixelVector.Row, HomMat, out X, out Y);  
            switch (CaliValueIn.caliModel) {
                case CaliModelEnum.HandEyeCali:
                    X = X + AddX;
                    Y = Y + AddY;
                    break;
                case CaliModelEnum.Cali9PtCali:
                    X = X + AddX;
                    Y = Y + AddY;
                    break;
                default:
                    X = X + AddX;
                    Y = Y + AddY;
                    break;                 
            }
            WorldVector.Angle = PixelVector.Angle;
            WorldVector.Col = X;
            WorldVector.Row = Y;
        }
    }
}
