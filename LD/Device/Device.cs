using System;

namespace LD.Device
{
    
	/// <summary>
	/// 设备接口
	/// </summary>
	public interface IDevice
	{
		/// <summary>
		/// 设备初始化
		/// </summary>
		void Init();

		/// <summary>
		/// 设备启动
		/// </summary>
		void Start();

		/// <summary>
		/// 设备停止
		/// </summary>
		void Stop();

		/// <summary>
		/// 设备释放
		/// </summary>
		void Release();


		/// <summary>
		/// 设备ID
		/// </summary>
		int DeviceID{get;}


        /// <summary>
        /// 设备类型
        /// </summary>
        string  DeviceType { get; }

        /// <summary>
        /// 设备备注
        /// </summary>
        /// <returns></returns>
        string ToString();

	}


	/// <summary>
	/// 设备基类
	/// </summary>
	public abstract class Device : IDevice
	{
		public Device(){}

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
           
            Log.Runlog.Instance.Add(this.ToString(), "初始化");
            DoInit();
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            Log.Runlog.Instance.Add(this.ToString(), "启动");
            DoStart();
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            Log.Runlog.Instance.Add(this.ToString(), "关闭");
            DoStop();
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Release()
        {
            Log.Runlog.Instance.Add(this.ToString(), "释放");
            DoRelease();
        }

		/// <summary>
		/// 设备ID
		/// </summary>
		public virtual int DeviceID
		{
			get { return 0; }
		}


		/// <summary>
		/// 初始化钩子函数
		/// </summary>
		public abstract void DoInit();
		/// <summary>
		/// 启动钩子函数
		/// </summary>
		public abstract void DoStart();
		/// <summary>
		/// 停止钩子函数
		/// </summary>
		public abstract void DoStop();
		/// <summary>
		/// 释放钩子函数
		/// </summary>
		public abstract void DoRelease();
		/// <summary>
		/// 设备类型
		/// </summary>
		public abstract string  DeviceType{ get; }

  
		/// <summary>
		/// ToString
		/// </summary>
		/// <returns></returns>
		public abstract override string ToString();
		

	}

	/// <summary>
	/// 初始化异常类
	/// </summary>
	public class InitException : Exception
	{
		/// <summary>
		/// 设备
		/// </summary>
		private string device;
		/// <summary>
		/// 原因
		/// </summary>
		private string reason;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="device"></param>
		/// <param name="reason"></param>
		public InitException( string device, string reason ) : base( reason )
		{
			this.device = device;
			this.reason = reason;
		}
		/// <summary>
		/// ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format( "设备[{0}]初始化错误:{1}", device , reason );
		}

	}


	/// <summary>
	/// 启动异常类
	/// </summary>
	public class StartException : Exception
	{
		/// <summary>
		/// 设备
		/// </summary>
		private string device;
		/// <summary>
		/// 原因
		/// </summary>
		private string reason;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="device"></param>
		/// <param name="reason"></param>
		public StartException( string device, string reason ) : base( reason )
		{
			this.device = device;
			this.reason = reason;
		}
		/// <summary>
		/// ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format( "设备[{0}]启动错误:{1}", device , reason );
		}

	}


	/// <summary>
	/// 停止异常类
	/// </summary>
	public class StopException : Exception
	{	
		/// <summary>
		/// 设备
		/// </summary>
		private string device;
		/// <summary>
		/// 原因
		/// </summary>
		private string reason;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="device"></param>
		/// <param name="reason"></param>
		public StopException( string device, string reason ) : base( reason )
		{
			this.device = device;
			this.reason = reason;
		}
		/// <summary>
		/// ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format( "设备[{0}]停止错误:{1}", device , reason );
		}

	}


	/// <summary>
	/// 释放异常类
	/// </summary>
	public class ReleaseException : Exception
	{
		/// <summary>
		/// 设备
		/// </summary>
		private string device;
		/// <summary>
		/// 原因
		/// </summary>
		private string reason;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="device"></param>
		/// <param name="reason"></param>
		public ReleaseException( string device, string reason ) : base( reason )
		{
			this.device = device;
			this.reason = reason;
		}
		/// <summary>
		/// ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format( "设备[{0}]释放错误:{1}", device , reason );
		}

	}
}
