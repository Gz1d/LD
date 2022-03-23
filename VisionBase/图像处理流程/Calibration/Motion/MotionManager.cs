using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionBase
{
   public  class MotionManager
   {

        private static object syncObj = new object();
        private static MotionManager _instance;
        public static MotionManager Instance
        {
            get {
                if (_instance == null) { 
                    lock (syncObj) {
                        if (_instance == null) _instance = new MotionManager();
                    }
                }
                return _instance;
            }             
        }

        private  MotionBase MyMotionBase = new MotionBase();


        public bool GetAxisPos(AxisEnum Axis, out double Pos)
        {
            Pos = 0;
            bool IsTrue = true;
            IsTrue =MyMotionBase.GetAxisPos(Axis, out Pos);
            return IsTrue;
        }

        public bool SetAxisPos(AxisEnum Axis, double Pos)
        {
            bool IsTrue = true;
            IsTrue = MyMotionBase.SetAixsPos(Axis, Pos);
            return IsTrue;      
        }

        public void SetCoordi(CoordiEmum Coordi)
        {
           // NowCoordi = Coordi;
            switch (Coordi){
                case CoordiEmum.Coordi0:
                    MyMotionBase = new MotionCoordi0();
                    break;
                case CoordiEmum.Coordi1:
                    MyMotionBase = new MotionCoordi1();
                    break;
                case CoordiEmum.Coordi2:
                    MyMotionBase = new  MotionCoordi2();
                    break;
                case CoordiEmum.Coordi3:
                    MyMotionBase = new MotionCoordi3();
                    break;
                case CoordiEmum.Coordi4:
                    MyMotionBase = new MotionCoordi4();
                    break;
                case CoordiEmum.Coordi5:
                    MyMotionBase = new  MotionCoordi5();
                    break;
                case CoordiEmum.Coordi6:
                    MyMotionBase = new MotionCoordi6();
                    break;
                case CoordiEmum.Coordi7:
                    MyMotionBase = new MotionCoordi7();
                    break;
                case CoordiEmum.Coordi8:
                    MyMotionBase = new MotionCoordi8();
                    break;
                case CoordiEmum.Coordi9:
                    MyMotionBase = new MotionCoordi9();
                    break;
            }
        }

        public bool SetCoordiPos(double X,double Y,double Theta   )
        {
            bool IsOk = true;
            IsOk = MyMotionBase.SetCoordiPos(X, Y, Theta);
            return IsOk;       
        }

        public bool GetCoordiPos(out double X, out double Y,out double Z, out double Theta)
        {
            X = 0;
            Y = 0;
            Z = 0;
            Theta = 0;
            bool IsOK = true;
            IsOK = MyMotionBase.GetCoordiPos(out X, out Y,out Z, out Theta);
            return IsOK;
        }


    }
}
