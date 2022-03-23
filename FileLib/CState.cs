using System;
//using System.Linq;
using System.Threading;
using FileLib;

namespace FileLib
{
    public abstract class CState
    {
        protected bool ThreadLive { get; set; }
        protected bool ThreadSwitch = false;

        private Thread wkThread=null;
        string name;

        public CState()
        {
        }

        ~CState()
        {
            ThreadSwitch = false;

            if ((wkThread!=null)&&(wkThread.IsAlive))
                wkThread.Join();

            int nTimes = 0;

            name = this.GetType().ToString();

            do
            {
                ThreadSwitch = false;

                if (nTimes++ > 500)
                {
                    Logger.Pop(name + " run thread end timeout..");
                    break;
                }

                Thread.Sleep(1);

            } while (ThreadLive);

            if (!ThreadLive) Logger.Pop(name + " thread end ok..");
        }

        protected virtual void StartThread()
        {
            if (ThreadLive) return;

            Thread selfThread;
            selfThread = new Thread(new ParameterizedThreadStart(RunProcess));
            selfThread.IsBackground = true;
            selfThread.Name = this.GetType().ToString();
            selfThread.Start(this);
            ThreadSwitch = true;
            name = this.GetType().ToString();

            wkThread = selfThread;
        }

        protected virtual void StopMachine()
        {
            ThreadSwitch = false;
        }

        protected virtual void RunProcess(object obj)
        {
            Logger.Pop(this.GetType().FullName.ToString() + "线程Start........");

            ThreadLive = true;
            Run();
            ThreadLive = false;
            ThreadSwitch = false;

            Logger.Pop(this.GetType().FullName.ToString() + "线程Exit........");
        }

        protected virtual void Run()
        {

        }

        protected virtual void PrintOutTask(string str)
        {

        }
    }
}
