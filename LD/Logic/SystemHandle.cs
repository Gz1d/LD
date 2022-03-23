using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LD.Config;

namespace LD.Logic
{

    /// <summary>
    /// 系统处理
    /// </summary>
    public class SystemHandle
    {
        #region 单例....
        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static SystemHandle instance = new SystemHandle();
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SystemHandle()
        {
          
           
         }

        /// <summary>
        /// 静态属性
        /// </summary>
        public static SystemHandle Instance
        {
            get { return instance; }
        }
        #endregion

        public Config.ConfigSystem Config
        {
            get
            {
                return ConfigManager.Instance.ConfigSystem;
            }
        }

        ///// <summary>
        ///// 获取站号
        ///// </summary>
        ///// <param name="machine"></param>
        ///// <returns></returns>
        //public string GetStationID(string machine)
        //{
        //    IEnumerable ie = from lst in this.Config.MachineItems
        //                     where lst.MachineID == machine
        //                     select lst;
        //    List<Config.MachineItem> ioLst = ie.Cast<Config.MachineItem>().ToList();
        //    if (ioLst.Count > 0)
        //        return ioLst[0].StationID + "" ;
        //    else
        //        return "0000";
        //}

        ///// <summary>
        ///// 获取机台配置
        ///// </summary>
        ///// <param name="machine"></param>
        ///// <returns></returns>
        //public Config.MachineItem GetMachineItem(string machine)
        //{
        //    IEnumerable ie = from lst in this.Config.MachineItems
        //                     where lst.MachineID == machine
        //                     select lst;
        //    List<Config.MachineItem> ioLst = ie.Cast<Config.MachineItem>().ToList();
        //    if (ioLst.Count > 0)
        //        return ioLst[0];
        //    else
        //        return null;
        //}

        ///// <summary>
        ///// 获取机台配置（多）
        ///// </summary>
        ///// <param name="active"></param>
        ///// <returns></returns>
        //public List<Config.MachineItem> GetMachineItems(bool active)
        //{
        //    IEnumerable ie = from lst in this.Config.MachineItems
        //                     where lst.IsActive == active
        //                     select lst;

        //    List<Config.MachineItem> ioLst = ie.Cast<Config.MachineItem>().ToList();
        //    return ioLst;
        //}


        /// <summary>
        /// 获取机台Kylin业务
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        public Logic.business GetMachineKylin(string machine)
        {
            business kylin = null;
            switch (machine)
            {
                case "01":
                    kylin = business01.Instance;
                    break;
                case "02":
                    kylin = business02.Instance;
                    break;
            }

            return kylin;
        }


        #region 更新连接状态





  


        /// <summary>
        /// 创建Opc设备枚举
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Common.OpcDevice GetOpc(string device, params object[] args)
        {
             Common.OpcDevice r_device= Common.OpcDevice.NULL ;
             try
             {
                 r_device = (Common.OpcDevice)Enum.Parse(typeof(Common.OpcDevice), string.Format(device, args));
             }
             catch { }
             return r_device;
        }

        private Common.SocketDevice  GetSocket(string device, params object[] args)
        {
            Common.SocketDevice r_device = Common.SocketDevice.NULL;
            try
            {
                r_device = (Common.SocketDevice)Enum.Parse(typeof(Common.SocketDevice), string.Format(device, args));
            }
            catch { }
            return r_device;
        }

        #endregion 


    }
}
