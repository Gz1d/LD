using System;
using System.ComponentModel;

namespace LD.Config
{
	/// <summary>
	/// 配置接口
	/// </summary>
	public interface IConfiguration 
    {

    }
	/// <summary>
	/// Configuration 的摘要说明。
	/// </summary>
	public class Configuration : IConfiguration,  INotifyPropertyChanged
	{	
		public Configuration(){}

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
	}
    
	/// <summary>
	/// 加载异常类
	/// </summary>
	public class LoadException : Exception
	{
		private string configName;
		private string reason;

        public LoadException(string configName, string reason)
            : base(reason)
		{
			this.configName = configName;
			this.reason = reason;
		}

		public override string ToString()
		{
			return String.Format( "配置[{0}]加载错误:{1}", this.configName, this.reason );
		}
	}
}
