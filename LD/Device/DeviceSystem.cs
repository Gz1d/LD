using System;
using System.Threading;
using System.IO;


namespace LD.Device
{

    /// <summary>
    /// Plc设备
    /// </summary>
    public class DeviceSystem : Device
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        public DeviceSystem(Config.ConfigSystem config)
        {
            this.config = config;
        }

        /// <summary>
        /// plc配置表
        /// </summary>
        private Config.ConfigSystem config
        {
            set;
            get;
        }

        /// <summary>
        /// 取数线程
        /// </summary>
        private Thread UpdateThread { set; get; }


        /// <summary>
        /// 关闭标志
        /// </summary>
        private bool isClose
        {
            set;
            get;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void DoInit()
        {
            try
            {
                this.isClose = false;
            }
            catch (Exception ex)
            {
                throw new InitException(this.ToString(), ex.ToString());
            }
        }




        /// <summary>
        /// 启动
        /// </summary>
        public override void DoStart()
        {
            try
            {
                //ThreadStart thread = new ThreadStart(this.UpdateIsConnected);
                //this.UpdateThread = new Thread(thread);
                //this.UpdateThread.IsBackground = true;
                //this.UpdateThread.Start();
            }
            catch (Exception ex)
            {
                throw new StartException(this.ToString(), ex.ToString());
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void DoStop()
        {
            try
            {
                this.isClose = true;
            }
            catch (Exception ex)
            {
                throw new StopException(this.ToString(), ex.ToString());
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void DoRelease()
        {
            try
            {
                if (this.UpdateThread == null) return;
                this.UpdateThread.DisableComObjectEagerCleanup();
            }
            catch (Exception ex)
            {
                throw new ReleaseException(this.ToString(), ex.ToString());
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public override string DeviceType
        {
            get
            {
                return "DeviceSystem";
            }
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.DeviceType;
        }

        #region 连接更新线程




        #endregion


    }
}
