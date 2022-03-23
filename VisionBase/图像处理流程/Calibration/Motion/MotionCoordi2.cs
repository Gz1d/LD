using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace VisionBase
{
     public  class MotionCoordi2:MotionBase
    {
        public override bool SetCoordiPos(double X, double Y, double Theta)
        {
            bool IsOk = true;
            IsOk = IsOk && SetAixsPos(AxisEnum.Coordi2_X1, X);
            IsOk = IsOk && SetAixsPos(AxisEnum.Coordi2_Y1, Y);
            IsOk = IsOk && SetAixsPos(AxisEnum.Coordi2_Theta1, Theta);
            if (MoveEvent == null) MoveEvent = new ManualResetEventSlim();
            MoveEvent.Reset();
            double NowX = 0, NowY = 0, NowTheta = 0;
            Task.Factory.StartNew(new Action(() => {
                while (IsOk)
                {
                    GetAxisPos(AxisEnum.Coordi2_X, out NowX);
                    System.Threading.Thread.Sleep(10);
                    GetAxisPos(AxisEnum.Coordi2_Y, out NowY);
                    System.Threading.Thread.Sleep(10);
                    GetAxisPos(AxisEnum.Coordi2_Theta, out NowTheta);
                    Thread.Sleep(10);
                    if (Math.Abs(NowX - X) < 0.01 && Math.Abs(NowY - Y) < 0.01 && Math.Abs(NowTheta - Theta) < 0.01){
                        MoveEvent.Set();
                    }
                }
            }));
            if (MoveEvent.Wait(5000)){
                IsOk = false;
                return true;
            }
            else{
                IsOk = false;
                return false;
            }
        }

        public override bool GetCoordiPos(out double X, out double Y,out double Z, out double Theta)
        {
            bool IsOk = true;
            X = 0;
            Y = 0;
            Z = 0;
            Theta = 0;
            IsOk = IsOk && GetAxisPos(AxisEnum.Coordi2_X, out X);
            System.Threading.Thread.Sleep(10);
            IsOk = IsOk && GetAxisPos(AxisEnum.Coordi2_Y, out Y);
            System.Threading.Thread.Sleep(10);
            IsOk = IsOk && GetAxisPos(AxisEnum.Coordi2_Z, out Z);
            System.Threading.Thread.Sleep(10);
            IsOk = IsOk && GetAxisPos(AxisEnum.Coordi2_Theta, out Theta);
            return IsOk;
        }



    }
}
