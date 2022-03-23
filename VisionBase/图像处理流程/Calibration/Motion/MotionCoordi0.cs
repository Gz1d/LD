using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VisionBase
{
    public class MotionCoordi0:MotionBase
    {

        public override bool SetCoordiPos(double X, double Y, double Theta)
        {
            bool IsOk = true;
            IsOk = IsOk && SetAixsPos(AxisEnum.Coordi0_X1, X);
            Thread.Sleep(50);
            IsOk = IsOk && SetAixsPos(AxisEnum.Coordi0_Y1, Y);
            Thread.Sleep(50);
            IsOk = IsOk && SetAixsPos(AxisEnum.Coordi0_Theta1, Theta);
            Thread.Sleep(50);
             MoveEvent = new ManualResetEventSlim();
            MoveEvent.Reset();
            double NowX = 0, NowY = 0, NowTheta = 0;
            Task.Factory.StartNew(new Action(() => {
                int i = 0;
                while (IsOk) {
                    GetAxisPos(AxisEnum.Coordi0_X, out NowX);
                    System.Threading.Thread.Sleep(10);
                    GetAxisPos(AxisEnum.Coordi0_Y, out NowY);
                    System.Threading.Thread.Sleep(10);
                    GetAxisPos(AxisEnum.Coordi0_Theta, out NowTheta);
                    Thread.Sleep(10);
                    if (Math.Abs(NowX - X) < 0.01 && Math.Abs(NowY - Y) < 0.01 && Math.Abs(NowTheta - Theta) < 0.01){
                        this.MoveEvent.Set();
                        break;
                    }
                    if (i > 200) break;
                    i++;
                }
            }));
            Thread.Sleep(100);
            if (MoveEvent.Wait(10000))  {
                IsOk = false;
                return true;
            }
            else{
                IsOk = false;
                return false;
            }
        }

        public override bool GetCoordiPos(out double X, out double Y, out double Z,out double Theta)
        {
            bool IsOk = true;
            X = 0;
            Y = 0;
            Z = 0;
            Theta = 0;
            IsOk = IsOk && GetAxisPos(AxisEnum.Coordi0_X, out X);
            IsOk = IsOk && GetAxisPos(AxisEnum.Coordi0_Y, out Y);
            IsOk = IsOk && GetAxisPos(AxisEnum.Coordi0_Z, out Z);
            IsOk = IsOk && GetAxisPos(AxisEnum.Coordi0_Theta, out Theta);
            return IsOk;
        }

    }
}
