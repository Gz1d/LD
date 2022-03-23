using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileLib;

namespace VisionBase
{
    public  class LocalPara : Configuration
    {
        /// <summary> 定位模式设定</summary>
        public LocalSettingPara localSetting = new LocalSettingPara();
        public St_TemplateParam Template = new St_TemplateParam(true);
        public St_LinesParam Lines = new St_LinesParam(4);
        public St_CirclesParam Circles = new St_CirclesParam(2);
        public St_BlobLocalParam Blobs = new St_BlobLocalParam(2);
        public St_InspectImageSetting LineCirRectInspParam =new St_InspectImageSetting(true);
        private string BasePath = "";
        private static string ConfigName = string.Format(@"\{0}", "LocalPara");

        public LocalPara() {
            localSetting = new LocalSettingPara();
            Template = new St_TemplateParam(true);
            Lines = new St_LinesParam(4);
            Circles = new St_CirclesParam(2);
            Blobs = new St_BlobLocalParam(2);
        }
        public bool Save(string Path)
        {
            bool Isok = true;     
            string path = Path + ConfigName + @"\";
            if (!FileLib.DirectoryEx.Exist(Path + ConfigName))  FileLib.DirectoryEx.Create(Path + ConfigName);
            if (localSetting == null){
                localSetting = new LocalSettingPara();
            }
            if (localSetting.localModel.ToString().Contains("Temp"))    Isok = Isok && Template.Save(path);
            Isok = Isok && XML<LocalSettingPara>.Write(this.localSetting, path + "LocalSetting.xml");
            Isok = Isok && XML<St_TemplateParam>.Write(this.Template, path + "Template.xml");
            Isok = Isok && XML<St_LinesParam>.Write(this.Lines, path + "Lines.xml");
            Isok = Isok && XML<St_CirclesParam>.Write(this.Circles, path + "Circles.xml");
            Isok = Isok && XML<St_BlobLocalParam>.Write(this.Blobs, path + "Blobs.xml");
            Isok = Isok && XML<St_InspectImageSetting>.Write(this.LineCirRectInspParam, path + "LineCirRectInspParam.xml");
            return Isok;
        }


        public bool Read(string Path)
        {
            bool IsOk = true;
           // BasePath = System.IO.Directory.GetCurrentDirectory();
            string path =   Path + ConfigName + @"\";
            this.localSetting = XML<LocalSettingPara>.Read(path + "LocalSetting.xml");
            this.Template = XML<St_TemplateParam>.Read(path + "Template.xml");
            this.Lines = XML<St_LinesParam>.Read(path + "Lines.xml");
            this.Circles = XML<St_CirclesParam>.Read(path + "Circles.xml");
            this.Blobs = XML<St_BlobLocalParam>.Read(path + "Blobs.xml");
            this.LineCirRectInspParam = XML<St_InspectImageSetting>.Read(path + "LineCirRectInspParam.xml");
            if (localSetting == null) return true;
            if (localSetting.localModel.ToString().Contains("Temp")) {
                Template.Load(path);
            }
            return IsOk;
        }

        public bool ClearNullObj()
        {
            if (localSetting == null) localSetting = new LocalSettingPara();

            return true;
        }
        //public void Save(string Path)
        //{
        //    try
        //    {
        //        //Serializition.SaveToFile(this, Path + ConfigName);
        //        Template.Save(Path );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }

        //}
        //public static   void  Load(string Path)
        //{
        //    try
        //    {
        //       // LocalPara obj = (LocalPara)Serializition.LoadFromFile(typeof(LocalPara), Path + ConfigName);
        //        obj.Template.Load(Path);
        //        return obj;          
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new LoadException(Path + ConfigName, ex.Message);
        //    }           
        //}
    }
}
