using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using System.Reflection;
using System.ComponentModel;




namespace LD.Logic
{
    /// <summary>
    /// PLC处理类，
    /// </summary>
    public class UiHandle
    {
        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static UiHandle instance = new UiHandle();
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private UiHandle()
        {

        }

        /// <summary>
        /// 静态属性
        /// </summary>
        public static UiHandle Instance
        {
            get { return instance; }
        }
        #endregion

    }
}
