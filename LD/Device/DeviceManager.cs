using System;
using System.Collections;

namespace LD.Device
{
    public class DeviceManager
    {

        #region 单例.....
        private static object syncObj = new object();
        private static DeviceManager _instance;
        public static DeviceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new DeviceManager();
                        }
                    }
                }

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }


        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeviceManager()
        {           
            try
            {
                //创建设备
                this.CreateDevices();
            }
            catch (Exception ex)
            {
                Log.LogWriter.WriteException(ex);
            }
        }


        /// <summary>
        /// 创建所有设备
        /// </summary>
        private void CreateDevices()
        {
            //camera
            //this.DeviceCamera = new DeviceCamera(Config.ConfigManager.Instance.ConfigCamera);
            //Opc
           // this.DeviceOpc = new DeviceOpc(Config.ConfigManager.Instance.ConfigOpc);
            //Socket client
            this.DeviceSocketC = new DeviceSocketC(Config.ConfigManager.Instance.ConfigSocketC);
            //Socket server
            this.DeviceSocketS = new DeviceSocketS(Config.ConfigManager.Instance.ConfigSocketS);
            //Serial
            this.DeviceSerial = new DeviceSerial(Config.ConfigManager.Instance.ConfigSerial);
            //PLC
            this.DevicePlc = new DevicePlc(Config.ConfigManager.Instance.ConfigPlc);
            //system
            this.DeviceSystem = new DeviceSystem(Config.ConfigManager.Instance.ConfigSystem);

            //入队列
            //this.DevicesCollection.Add(this.DeviceCamera.DeviceType, this.DeviceCamera);
            this.DevicesCollection.Add(this.DeviceSocketS.DeviceType, this.DeviceSocketS);
            this.DevicesCollection.Add(this.DeviceSerial.DeviceType, this.DeviceSerial);
            this.DevicesCollection.Add(this.DeviceSocketC.DeviceType, this.DeviceSocketC);
            this.DevicesCollection.Add(this.DevicePlc.DeviceType, this.DevicePlc);
            this.DevicesCollection.Add(this.DeviceSystem.DeviceType, this.DeviceSystem);
        }

        /// <summary>
        /// 设备列表
        /// </summary>
        private SortedList DevicesCollection = new SortedList();

        /// <summary>
        /// 创建窗体
        /// </summary>
        public System.Windows.Forms.Form Sender
        {
            set;
            get;
        }



        /// <summary>
        /// Camera设备
        /// </summary>
        //public DeviceCamera DeviceCamera
        //{
        //    set;
        //    get;
        //}




        /// <summary>
        /// Socket客户端设备
        /// </summary>
        public DeviceSocketC DeviceSocketC
        {
            set;
            get;
        }

        /// <summary>
        /// Socket服务端设备
        /// </summary>
        public DeviceSocketS DeviceSocketS
        {
            set;
            get;
        }

        /// <summary>
        /// 串口设备
        /// </summary>
        public DeviceSerial DeviceSerial
        {
            set;
            get;
        }

        /// <summary>
        /// Plc设备
        /// </summary>
        public DevicePlc DevicePlc
        {
            set;
            get;
        }

        /// <summary>
        /// 系统设备，心跳线程
        /// </summary>
        public DeviceSystem DeviceSystem
        {
            set;
            get;
        }

        /// <summary>
        /// 初始设备
        /// </summary>
        public void DeviceInit()
        {    
            IDictionaryEnumerator enumerator = this.DevicesCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                try
                {
                    IDevice device = enumerator.Value as IDevice;
                    device.Init();
                }
                catch (InitException ex)
                {
                    Log.LogWriter.WriteException(ex);
                    Log.LogWriter.WriteLog("{0}", ex.ToString());
                }
            }
        }

        /// <summary>
        /// 启动设备
        /// </summary>
        public void DeviceStart()
        {
            IDictionaryEnumerator enumerator = this.DevicesCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                try
                {
                    IDevice device = enumerator.Value as IDevice;
                    device.Start();
                }
                catch (StartException ex)
                {
                    Log.LogWriter.WriteException(ex);
                    Log.LogWriter.WriteLog("{0}", ex.ToString());
                }
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public void DeviceStop()
        {
            IDictionaryEnumerator enumerator = this.DevicesCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                try{
                    IDevice device = enumerator.Value as DeviceSocketC;
                    if(device!=null)   device.Stop();
                }
                catch
                { }        
            }
            while (enumerator.MoveNext())
            {
                try
                {
                    IDevice device = enumerator.Value as IDevice;
                    device.Stop();
                }
                catch (StopException ex)
                {
                    Log.LogWriter.WriteException(ex);
                    Log.LogWriter.WriteLog("{0}", ex.ToString());
                }
            }
        }


        /// <summary>
        /// 释放设备
        /// </summary>
        public void DeviceRelease()
        {
            IDictionaryEnumerator enumerator = this.DevicesCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                try
                {
                    IDevice device = enumerator.Value as IDevice;
                    device.Release();
                }
                catch (ReleaseException ex)
                {
                    Log.LogWriter.WriteException(ex);
                    Log.LogWriter.WriteLog("{0}", ex.ToString());
                }
            }
        }
    }
}
