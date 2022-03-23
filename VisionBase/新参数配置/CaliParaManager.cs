using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FileLib;
using System.IO;

namespace VisionBase
{
  public   class CaliParaManager
  {
        public BindingList<CaliParam> CaliParaList = new BindingList<CaliParam>();
        private static string ParaPath = @"D\标定参数";
        private static object sycnObj = new object();
        private static CaliParaManager _Instance;
        public static CaliParaManager Instance
        {
            get {
                if (_Instance == null) {
                    lock (sycnObj){
                        _Instance = new CaliParaManager(); 
                    }              
                }
                return _Instance;
            }  
        }

        public bool Save()
        {
            bool IsOk = true;
            //DirectoryEx.Delete(ParaPath);
            if (!DirectoryEx.Exist(ParaPath)) DirectoryEx.Create(ParaPath);
            int Index = 0;
            foreach (CaliParam item in CaliParaList){
                string Pathi = ParaPath + @"\" + Index.ToString() + @"\";
                if (!FileLib.DirectoryEx.Exist(Pathi))  FileLib.DirectoryEx.Create(Pathi);
                IsOk = IsOk&&item.Save(ParaPath + @"\"+Index.ToString() + @"\");
                Index++;
            }
            return IsOk;     
        }

        public bool Read()
        {
            bool IsOk = true;
            string[] paths = new string[0];
            CaliParaList = new BindingList<CaliParam>();
            DirectoryInfo myDirectory = new DirectoryInfo(ParaPath);
            if (!FileLib.DirectoryEx.Exist(ParaPath)) FileLib.DirectoryEx.Create(ParaPath);
            DirectoryInfo[] Directs = myDirectory.GetDirectories();
            CaliParam caliPara = new CaliParam();
            foreach (DirectoryInfo item in Directs) {
                caliPara = CaliParam.Load(item.FullName);
                CaliParaList.Add(caliPara);
            }
            return IsOk;
        }



        public MyHomMat2D GetHomMAT(CoordiCamHandEyeMatEnum CoordiCamIn) //获取标定矩阵
        {
            foreach (CaliParam item in CaliParaList)
            {
                if (item.CoordiCam == CoordiCamIn)
                {
                    return item.HomMat;
                }
            }
            return new MyHomMat2D();
        }

        public CaliModelEnum GetCaliMode(CoordiEmum CoordiIn)
        {
            foreach (CaliParam item in CaliParaList) {
                if (item.coordi == CoordiIn) {
                    return item.caliModel;
                }
            }
            return CaliModelEnum.HandEyeCali;
        }


        public CaliModelEnum GetCaliMode(CoordiCamHandEyeMatEnum CoordiCamIn)
        {
            foreach (CaliParam item in CaliParaList) {
                if (item.CoordiCam == CoordiCamIn) {
                    return item.caliModel;
                }
            }
            return CaliModelEnum.HandEyeCali;
        }

        /// <summary>
        /// 通过坐标系枚举获取标定参数，这个方法存在的问题是，可能一个坐标有两个相机需要标定
        /// </summary>
        /// <param name="CoordiCamIn"></param>
        /// <returns></returns>
        public CaliValue GetCaliValue(CoordiCamHandEyeMatEnum CoordiCamIn)
        {
            CaliValue value = new CaliValue();
            foreach (CaliParam item in CaliParaList) {
                if (item.CoordiCam == CoordiCamIn) {
                    return value = item.GetCaliValue();
                }
            }
            return value;
        }
        /// <summary>
        /// 通过标定描述获取对应的标定矩阵
        /// </summary>
        /// <param name="CaliDescribe"></param>
        /// <returns></returns>
        public CaliValue GetCaliValue(string CaliDescribe)
        {
            CaliValue value = new CaliValue();
            foreach (var item in this.CaliParaList) {
                if (item.describe == CaliDescribe)  return value = item.GetCaliValue();          
            }
            return value;     
        }
    
  }




}
