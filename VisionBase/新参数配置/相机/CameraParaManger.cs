using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FileLib;
using HalconDotNet;
using System.Windows.Forms;

namespace VisionBase
{
   public  class CameraParaManager
    {
        public BindingList<CameraPara> CameraParaList = new BindingList<CameraPara>();
        private static string ParaPath = @"config";
        private static object sycnObj = new object();
        private static CameraParaManager _Instance;
        private static object LockObj = new object();
        public static CameraParaManager Instance
        {
            get{
                if (_Instance == null){
                    lock (sycnObj){
                        _Instance = new CameraParaManager();                                        
                    }                              
                }
                return _Instance;                      
            }                
        }
        public bool Save()
        {
            bool IsOk = true;
            DirectoryEx.Delete(ParaPath);
            if (!DirectoryEx.Exist(ParaPath)) DirectoryEx.Create(ParaPath);
            IsOk = IsOk&& XML<BindingList<CameraPara>>.Write(CameraParaList, ParaPath + @"\" + "CameraParaList.xml");
            return IsOk;       
        }
        public BindingList<CameraPara> Read()
        {
            BindingList<CameraPara> CameraList0 = new BindingList<CameraPara>();
            CameraList0 = XML<BindingList<CameraPara>>.Read(ParaPath + @"\" + "CameraParaList.xml");
            if (CameraList0 == null) CameraList0 = new BindingList<CameraPara>();
            this.CameraParaList = CameraList0;
            foreach (CameraPara item in CameraParaList){
                item.IsOpen = false;
                HTuple MyHandle = new HTuple();
            }
            return CameraList0;
        }
    }
}
