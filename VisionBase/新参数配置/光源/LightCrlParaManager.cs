using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FileLib;
using System.Collections;

namespace VisionBase
{
    public   class LightCrlParaManager
    {
        /// <summary> 光源参数列表 </summary>
        public BindingList<LightCrlParaItem> LightCtrParaItems { set;get; }
        private static string Path = @"D\光源控制器参数";

        private static LightCrlParaManager _instance;
        private static object lockObj = new object();

        public static LightCrlParaManager Instance
        {
            get{
                lock (lockObj)  {
                    return _instance = _instance ?? new LightCrlParaManager();                       
                } 
            }   
        }
        private LightCrlParaManager()
        {
            this.LightCtrParaItems = new BindingList<LightCrlParaItem>();
        }

        public bool  Save()
        {
            bool IsOk = true;
            DirectoryEx.DeleteFolder1(Path);
            if (!DirectoryEx.Exist(Path)) DirectoryEx.Create(Path);
            IsOk = IsOk && XML<LightCrlParaManager>.Write(this, Path + @"\" + "LightCrlParaManager.xml");
            return IsOk;
        }

        public bool Read()
        {
            bool IsOk = true;
            LightCrlParaManager Obj = XML<LightCrlParaManager>.Read(Path + @"\" + "LightCrlParaManager.xml");
            if (Obj == null) Obj =  LightCrlParaManager.Instance;
            if (Obj.LightCtrParaItems.Count()==0) {
                LightCrlParaItem item = new LightCrlParaItem();
                Obj.LightCtrParaItems = new BindingList<LightCrlParaItem>();
                Obj.LightCtrParaItems.Add(item);
            }
            _instance = Obj;
            return IsOk;   
        }



        public LightCrlParaItem GetParaItem(LightCrlParaItem lightCtrlParaIn)
        {
            IEnumerable ie = from lst in this.LightCtrParaItems
                             where lst.PortName == lightCtrlParaIn.PortName
                             select lst;
            List<LightCrlParaItem> ioLst = ie.Cast<LightCrlParaItem>().ToList();
            if (ioLst.Count > 0)  return ioLst[0];
            else   return null;           
        
        }


    }
}
