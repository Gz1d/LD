using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD;
using System.Reflection;
using System.ComponentModel;
using System.Threading;

namespace VisionBase
{
    public class MotionBase
    {
        public ManualResetEventSlim MoveEvent;

        public virtual bool GetAxisPos(AxisEnum Axis, out double Pos)
        {
            Pos = 0;
            LD.Common.PlcDevice PLCAxis = (LD.Common.PlcDevice)Enum.Parse(typeof(LD.Common.PlcDevice), Axis.ToString());
            Object obj = LD.Logic.PlcHandle.Instance.ReadValue( PLCAxis);
            if (obj == null) return false;
            double Pos0 = (int)obj;
            Pos = Pos0 / 1000.0;
            return true;
        }


        public virtual bool  SetAixsPos(AxisEnum Axis,  double Pos)
        {
            LD.Common.PlcDevice PLCAxis = (LD.Common.PlcDevice)Enum.Parse(typeof(LD.Common.PlcDevice), Axis.ToString());
            double Pos0 = Pos * 1000.0;
            int PosIn = (int)Pos0;
            LD.Logic.PlcHandle.Instance.WriteValue(PLCAxis, PosIn);
            return true;      
        }

        public  virtual bool SetCoordiPos(double X, double Y, double Theta)
        {
            return true;
        }
        /// <summary>
        /// 从PLC或者机器人读取坐标
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="Theta"></param>
        /// <returns></returns>
        public  virtual bool GetCoordiPos(out double X, out double Y, out double Z,out double Theta)
        {
            X = 0;
            Y = 0;
            Theta = 0;
            Z = 0;
            return true;
        }
    }
    /// <summary>
    /// 坐标系枚举
    /// </summary>
    public enum CoordiEmum
    {
        /// <summary> 坐标系0  </summary>
        Coordi0,
        /// <summary> 坐标系1  </summary>
        Coordi1,
        /// <summary> 坐标系2  </summary>
        Coordi2,
        /// <summary> 坐标系3  </summary>
        Coordi3,
        /// <summary> 坐标系4  </summary>
        Coordi4,
        /// <summary> 坐标系5  </summary>
        Coordi5,
        /// <summary> 坐标系6  </summary>
        Coordi6,
        /// <summary> 坐标系7  </summary>
        Coordi7,
        /// <summary> 坐标系8  </summary>
        Coordi8,
        /// <summary> 坐标系9  </summary>
        Coordi9,
    }

    /// <summary>
    /// 手眼标定矩阵枚举，可以通过此方式获取标定矩阵
    /// </summary>
    public enum CoordiCamHandEyeMatEnum
    {
        Coordi0Cam0,
        Coordi0Cam1,
        Coordi0Cam2,
        Coordi0Cam3,

        Coordi1Cam0,
        Coordi1Cam1,
        Coordi1Cam2,
        Coordi1Cam3,

        Coordi2Cam0,
        Coordi2Cam1,
        Coordi2Cam2,
        Coordi2Cam3,

        Coordi3Cam0,
        Coordi3Cam1,
        Coordi3Cam2,
        Coordi3Cam3,

        Coordi4Cam0,
        Coordi4Cam1,
        Coordi4Cam2,
        Coordi4Cam3,

        Coordi5Cam0,
        Coordi5Cam1,
        Coordi5Cam2,
        Coordi5Cam3,

        Coordi6Cam0,
        Coordi6Cam1,
        Coordi6Cam2,
        Coordi6Cam3,

        Coordi7Cam0,
        Coordi7Cam1,
        Coordi7Cam2,
        Coordi7Cam3,

        Coordi8Cam0,
        Coordi8Cam1,
        Coordi8Cam2,
        Coordi8Cam3,
    }


    public enum AxisEnum
    {

        /// <summary> 坐标系0的X轴</summary>
        Coordi0_X =0,
        /// <summary> 坐标系0的Y轴</summary>
        Coordi0_Y,
        /// <summary> 坐标系0的Z轴</summary>
        Coordi0_Z,
        /// <summary> 坐标系0的Theta轴</summary>
        Coordi0_Theta,

        /// <summary> 设置坐标系0的X轴的坐标</summary>
        Coordi0_X1,
        /// <summary> 设置坐标系0的Y轴的坐标<</summary>
        Coordi0_Y1,
        /// <summary> 设置坐标系0的Z轴的坐标<</summary>
        Coordi0_Z1,
        /// <summary> 设置坐标系0的Theta轴的坐标<</summary>
        Coordi0_Theta1,


        /// <summary> 坐标系1的X轴</summary>
        Coordi1_X,
        /// <summary> 坐标系1的Y轴</summary>
        Coordi1_Y,
        /// <summary> 坐标系1的Z轴</summary>
        Coordi1_Z,
        /// <summary> 坐标系1的Theta轴</summary>
        Coordi1_Theta,

        /// <summary> 坐标系0的X轴</summary>
        Coordi1_X1,
        /// <summary> 坐标系0的Y轴</summary>
        Coordi1_Y1,
        /// <summary> 坐标系0的Z轴</summary>
        Coordi1_Z1,
        /// <summary> 坐标系0的Theta轴</summary>
        Coordi1_Theta1,

        /// <summary> 坐标系2的X轴</summary>
        Coordi2_X,
        /// <summary> 坐标系2的Y轴</summary>
        Coordi2_Y,
        /// <summary> 坐标系2的Z轴</summary>
        Coordi2_Z,
        /// <summary> 坐标系2的Theta轴</summary>
        Coordi2_Theta,

        /// <summary> 坐标系0的X轴</summary>
        Coordi2_X1,
        /// <summary> 坐标系0的Y轴</summary>
        Coordi2_Y1,
        /// <summary> 坐标系0的Z轴</summary>
        Coordi2_Z1,
        /// <summary> 坐标系0的Theta轴</summary>
        Coordi2_Theta1,

        /// <summary> 坐标系3的X轴</summary>
        Coordi3_X,
        /// <summary> 坐标系3的Y轴</summary>
        Coordi3_Y,
        /// <summary> 坐标系3的Z轴</summary>
        Coordi3_Z,
        /// <summary> 坐标系3的Theta轴</summary>
        Coordi3_Theta,
        /// <summary> 设置坐标系3的X轴的坐标</summary>
        Coordi3_X1,
        /// <summary> 设置坐标系3的Y轴的坐标<</summary>
        Coordi3_Y1,
        /// <summary> 设置坐标系3的Z轴的坐标<</summary>
        Coordi3_Z1,
        /// <summary> 设置坐标系3的Theta轴的坐标<</summary>
        Coordi3_Theta1,

        /// <summary> 坐标系4的X轴</summary>
        Coordi4_X,
        /// <summary> 坐标系4的Y轴</summary>
        Coordi4_Y,
        /// <summary> 坐标系4的Z轴</summary>
        Coordi4_Z,
        /// <summary> 坐标系4的Theta轴</summary>
        Coordi4_Theta,
        /// <summary> 设置坐标系4的X轴的坐标</summary>
        Coordi4_X1,
        /// <summary> 设置坐标系4的Y轴的坐标<</summary>
        Coordi4_Y1,
        /// <summary> 设置坐标系4的Z轴的坐标<</summary>
        Coordi4_Z1,
        /// <summary> 设置坐标系4的Theta轴的坐标<</summary>
        Coordi4_Theta1,

        /// <summary> 坐标系5的X轴</summary>
        Coordi5_X,
        /// <summary> 坐标系5的Y轴</summary>
        Coordi5_Y,
        /// <summary> 坐标系5的Z轴</summary>
        Coordi5_Z,
        /// <summary> 坐标系5的Theta轴</summary>
        Coordi5_Theta,
        /// <summary> 设置坐标系5的X轴的坐标</summary>
        Coordi5_X1,
        /// <summary> 设置坐标系5的Y轴的坐标<</summary>
        Coordi5_Y1,
        /// <summary> 设置坐标系5的Z轴的坐标<</summary>
        Coordi5_Z1,
        /// <summary> 设置坐标系5的Theta轴的坐标<</summary>
        Coordi5_Theta1,

        /// <summary> 坐标系6的X轴</summary>
        Coordi6_X,
        /// <summary> 坐标系6的Y轴</summary>
        Coordi6_Y,
        /// <summary> 坐标系6的Z轴</summary>
        Coordi6_Z,
        /// <summary> 坐标系6的Theta轴</summary>
        Coordi6_Theta,
        /// <summary> 设置坐标系6的X轴的坐标</summary>
        Coordi6_X1,
        /// <summary> 设置坐标系6的Y轴的坐标<</summary>
        Coordi6_Y1,
        /// <summary> 设置坐标系6的Z轴的坐标<</summary>
        Coordi6_Z1,
        /// <summary> 设置坐标系6的Theta轴的坐标<</summary>
        Coordi6_Theta1,

        /// <summary> 坐标系7的X轴</summary>
        Coordi7_X,
        /// <summary> 坐标系7的Y轴</summary>
        Coordi7_Y,
        /// <summary> 坐标系7的Z轴</summary>
        Coordi7_Z,
        /// <summary> 坐标系7的Theta轴</summary>
        Coordi7_Theta,
        /// <summary> 设置坐标系7的X轴的坐标</summary>
        Coordi7_X1,
        /// <summary> 设置坐标系7的Y轴的坐标<</summary>
        Coordi7_Y1,
        /// <summary> 设置坐标系7的Z轴的坐标<</summary>
        Coordi7_Z1,
        /// <summary> 设置坐标系7的Theta轴的坐标<</summary>
        Coordi7_Theta1,


        /// <summary> 坐标系8的X轴</summary>
        Coordi8_X,
        /// <summary> 坐标系8的Y轴</summary>
        Coordi8_Y,
        /// <summary> 坐标系8的Z轴</summary>
        Coordi8_Z,
        /// <summary> 坐标系8的Theta轴</summary>
        Coordi8_Theta,
        /// <summary> 设置坐标系8的X轴的坐标</summary>
        Coordi8_X1,
        /// <summary> 设置坐标系8的Y轴的坐标<</summary>
        Coordi8_Y1,
        /// <summary> 设置坐标系8的Z轴的坐标<</summary>
        Coordi8_Z1,
        /// <summary> 设置坐标系8的Theta轴的坐标<</summary>
        Coordi8_Theta1,

        /// <summary> 坐标系9的X轴</summary>
        Coordi9_X,
        /// <summary> 坐标系9的Y轴</summary>
        Coordi9_Y,
        /// <summary> 坐标系9的Z轴</summary>
        Coordi9_Z,
        /// <summary> 坐标系9的Theta轴</summary>
        Coordi9_Theta,
        /// <summary> 设置坐标系9的X轴的坐标</summary>
        Coordi9_X1,
        /// <summary> 设置坐标系9的Y轴的坐标<</summary>
        Coordi9_Y1,
        /// <summary> 设置坐标系9的Z轴的坐标<</summary>
        Coordi9_Z1,
        /// <summary> 设置坐标系9的Theta轴的坐标<</summary>
        Coordi9_Theta1,
    }


    public class EnumTextByDescription
    {
        public static string GetEnumDesc(Enum e) {
            FieldInfo EnumInfo = e.GetType().GetField(e.ToString());
            DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.
                GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (EnumAttributes.Length > 0){
                return EnumAttributes[0].Description;
            }
            return e.ToString();
        }
    }



}
