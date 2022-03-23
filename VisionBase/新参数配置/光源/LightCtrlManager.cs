using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VisionBase
{
   public   class LightCtrlManager
   {
        /// <summary> 光源控制器集 </summary>
        public SortedList LightCtrls = new SortedList();
        private static object lockObj = new object();
        private static LightCtrlManager _instance;

        public static LightCtrlManager Instance
        {
            get {
                lock (lockObj) {
                    if (_instance == null) {
                        _instance = new LightCtrlManager();
                    }
                }
                return _instance;                 
            } 
        }

        /// <summary>光源控制器参数 </summary>
        private BindingList<LightCrlParaItem> LightCtrlParaItems { get; set;  }

        public LightCtrlManager()
        {
            this.LightCtrlParaItems = LightCrlParaManager.Instance.LightCtrParaItems;      
        }

        /// <summary>
        /// 初始化串口
        /// </summary>
        public void DoInit()
        {
            try {
                if (LightCtrlParaItems == null) return ;
                foreach (LightCrlParaItem item in LightCtrlParaItems) {
                    LightCtrlBase LightCtrl;
                    switch (item.LightCtrlType)  {
                        case LightCtrlTypeEnum.WordP:
                            LightCtrl = new LightCtrlWordp();
                            break;
                        case LightCtrlTypeEnum.MengT:
                            LightCtrl = new LightCtrlMT();
                            break;
                        default:
                            LightCtrl = new LightCtrlBase();
                            break;
                    }
                    LightCtrl.SetPara(item);
                    LightCtrl.DoInit();
                    item.Tag = LightCtrl;
                    LightCtrls.Add(item.LightCtrlName, LightCtrl);
                }
            }
            catch (Exception ex) {
                throw ex;                     
            }
        }
        /// <summary>
        /// 开启串口
        /// </summary>
        public void DoStart()
        {
            IDictionaryEnumerator enumerator = this.LightCtrls.GetEnumerator();
            while (enumerator.MoveNext())  {
                LightCtrlBase lightCtrl = enumerator.Value as LightCtrlBase;
                lightCtrl.DoStart();         
            }        
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void DoStop()
        {
            IDictionaryEnumerator enumerator = this.LightCtrls.GetEnumerator();
            while (enumerator.MoveNext())  {
                LightCtrlBase lightCtrl = enumerator.Value as LightCtrlBase;
                lightCtrl.DoStop();
            }            
        }
        /// <summary>
        /// 释放串口
        /// </summary>
        public void DoRelease()
        {
            IDictionaryEnumerator enumerator = this.LightCtrls.GetEnumerator();
            while (enumerator.MoveNext()) {
                LightCtrlBase lightCtrl = enumerator.Value as LightCtrlBase;
                lightCtrl.DoRelease();
            }
        }


        public bool SetLightValue(  LightPara LightParaIn  )
        {
            IDictionaryEnumerator enumerator = this.LightCtrls.GetEnumerator();
            if (LightParaIn == null) return false;
            while (enumerator.MoveNext())  {
                LightCtrlBase lightCtrl = enumerator.Value as LightCtrlBase;
                if (lightCtrl.lightCtrlParaItem.LightCtrlName == LightParaIn.LightCtrl) {
                    lightCtrl.SetLightValue((int)LightParaIn.Panel, LightParaIn.LightValue);                                   
                }
            }
             return true;
        }

        /// <summary>
        /// 把光源控制器的
        /// </summary>
        /// <returns></returns>
        public bool SetAllLightTo0()
        {
            IDictionaryEnumerator enumerator = this.LightCtrls.GetEnumerator();
            while (enumerator.MoveNext())  {
                LightCtrlBase lightCtrl = enumerator.Value as LightCtrlBase;
                if (lightCtrl.lightCtrlParaItem.IsConnect) {
                    foreach (LightPanelEnum item in lightCtrl.Panels) {
                        lightCtrl.SetLightValue((int)item, 0);                   
                    }
                }
            }
            return true;     
        }



    }
}
