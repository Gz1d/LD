using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FileLib;

namespace VisionBase
{
    [Serializable]
    public class VisionPara:Configuration
    {

        public VisionPara()
        {
            this.ProjectVisionItem = ProjectVisionEnum.ProjectVision0;
            this.camLightPara = new CameraLightPara();
            this.localPara = new LocalPara();
            this.ProjectVisionName = "";
        }
        /// <summary> 相机光源参数 </summary>
        public CameraLightPara camLightPara;
        /// <summary>视觉定位参数 </summary>
        public LocalPara localPara = new LocalPara();

        private static string ConfigName = string.Format(@"\{0}", "VisionPara");

        /// <summary> 视觉工程中视觉参数集枚举 </summary>
        public  ProjectVisionEnum ProjectVisionItem {set; get; }
        /// <summary> 视觉工程名 </summary>
        public  string ProjectVisionName{set;get; }

        public bool Save(string Path)
        {
            this.ClearNullObj();
            bool IsOk = true;
            //string path = Path + ConfigName + @"\";
            if (!FileLib.DirectoryEx.Exist(Path )) FileLib.DirectoryEx.Create(Path);
            IsOk = IsOk && this.localPara.Save(Path);
            IsOk = IsOk && XML<ProjectVisionEnum>.Write(this.ProjectVisionItem, Path + "ProjectVisionItem.xml");
            if (this.ProjectVisionName == null) this.ProjectVisionName = "";
            IsOk = IsOk && XML<string>.Write(this.ProjectVisionName, Path + "ProjectVisioName.xml");
            IsOk = IsOk && XML<CameraLightPara>.Write(this.camLightPara, Path + "camLightPara.xml");
            return IsOk;             
        }

        public bool Read(string Path)
        {
            bool IsOk = true;
            //string path = Path + ConfigName + @"\";
            this.localPara.Read(Path);
            this.ProjectVisionItem = XML<ProjectVisionEnum>.Read(Path + "ProjectVisionItem.xml");
            this.ProjectVisionName = XML<string>.Read(Path + "ProjectVisioName.xml");
            this.camLightPara = XML<CameraLightPara>.Read(Path + "camLightPara.xml");
            return IsOk;
        }

        /// <summary>
        /// 清除空对象，保证所有写入对象不为空
        /// </summary>
        /// <returns></returns>
        public bool ClearNullObj()
        {
            if (this.ProjectVisionItem == null) this.ProjectVisionItem = ProjectVisionEnum.ProjectVision0;
            if (this.ProjectVisionName == null) this.ProjectVisionName = "";
            if (this.camLightPara == null) this.camLightPara = new CameraLightPara();          
            return true;
        }
        // public MyHomMat2D CaliHomMat = new MyHomMat2D();
        //[System.Xml.Serialization.XmlIgnore]

        //private static string ConfigName = string.Format(@"\{0}", "VisionParameter");
        //private static string ConfigPath = @"\";



        //public void Save(string Path)
        //{
        //    try
        //    {
        //        Serializition.SaveToFile(this, Path + ConfigName);
        //        localPara.Save(Path + ConfigPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //}

        //public static VisionPara Load(string Path)
        //{
        //    try
        //    {
        //        VisionPara obj = (VisionPara)Serializition.LoadFromFile(typeof(VisionPara), Path + ConfigName);
        //        obj.localPara = LocalPara.Load(Path + ConfigPath);
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new LoadException(Path + ConfigName, ex.Message);
        //    }
        //}


    }
}
