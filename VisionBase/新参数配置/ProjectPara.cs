using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileLib;

namespace VisionBase
{
   public  class ProjectPara:Configuration
    {

        public List<ProjectVisionEnum> ProjectVisionNames = new List<ProjectVisionEnum>();
        // [System.Xml.Serialization.XmlIgnore]
        public List<VisionPara> VisionParas = new List<VisionPara>();
        public ProjectEnum ProjectName = ProjectEnum.PinLocal0;

        private static string ConfigName = string.Format(@"\{0}", "ProjectPara");
        public ProjectPara()
        {
            ProjectVisionNames = new List<ProjectVisionEnum>();
            VisionParas = new List<VisionPara>();
            ProjectVisionNames.Add(ProjectVisionEnum.ProjectVision0);
            VisionParas.Add(new VisionPara());
            //ProjectVisionNames.Add(ProjectVisionEnum.ProjectVision0);
            //VisionParas.Add(new VisionPara());
            ProjectName = ProjectEnum.PinLocal0;
        }


        public ProjectPara(ProjectModelEnum ProjectModel)
        {
            ProjectVisionNames = new List<ProjectVisionEnum>();
            VisionParas = new List<VisionPara>();
            ProjectName = ProjectEnum.PinLocal0;
            ProjectVisionNames.Add(ProjectVisionEnum.ProjectVision0);
            VisionParas.Add(new VisionPara());
            switch (ProjectModel) {
                case ProjectModelEnum.SingleCamLocal:
                    ProjectName = ProjectEnum.PinLocal0;
                    break;
                case ProjectModelEnum.TwoCamLocal:
                    ProjectVisionNames.Add(ProjectVisionEnum.ProjectVision0);
                    VisionParas.Add(new VisionPara());
                    ProjectName = ProjectEnum.FofOffSet;
                    break;
            }

        }

        /// <summary>
        /// 获取视觉工程下第N个视觉参数
        /// </summary>
        /// <param name="GrabNum"></param>
        /// <returns></returns>
        public VisionPara   GetVisionPara( int  Num )
        {
            ProjectVisionEnum NowNum = (ProjectVisionEnum)Enum.Parse(typeof( ProjectVisionEnum), Num.ToString(), false);
            foreach (VisionPara item in VisionParas){
                if (item.ProjectVisionItem == NowNum)  return item;           
            }
            return new VisionPara();
        }

        public VisionPara GetVisionPara(string VisionName)
        {
            foreach (VisionPara item in VisionParas) {
                if (item.ProjectVisionName == VisionName) return item;
            }
            return new VisionPara();
        }

        public bool Save(string Path)
        {
            bool IsOk = true;
            string path =  Path  + @"\";
            if (!FileLib.DirectoryEx.Exist(path )) FileLib.DirectoryEx.Create(Path);
            IsOk = IsOk && XML<List<ProjectVisionEnum>>.Write(this.ProjectVisionNames, Path + "ProjectVisionName.xml");
            for (int i = 0; i < this.ProjectVisionNames.Count; i++) {
                if (!FileLib.DirectoryEx.Exist(Path + i.ToString() + this.ProjectVisionNames[i].ToString() + @"\"))
                    FileLib.DirectoryEx.Create(Path + i.ToString()  + this.ProjectVisionNames[i].ToString() + @"\");
                IsOk = IsOk && VisionParas[i].Save(Path + i.ToString()  + this.ProjectVisionNames[i].ToString() + @"\");       
            }
            return IsOk;
        }
        public bool Read(string Path)
        {
            bool IsOk = true;
            //string path =  Path + ConfigName + @"\";
            this.ProjectVisionNames = XML<List<ProjectVisionEnum>>.Read(Path + "ProjectVisionName.xml");
            this.VisionParas = new List<VisionPara>();           
            for (int i = 0; i < this.ProjectVisionNames.Count; i++)
            {
                VisionPara VisionParaI = new VisionPara();
                VisionParaI.Read(Path + i.ToString() + this.ProjectVisionNames[i].ToString() + @"\");
                this.VisionParas.Add(VisionParaI);
            }
            return IsOk;
        }

    }

    /// <summary>
    /// 视觉项目类型枚举
    /// </summary>
    public enum ProjectModelEnum
    {
        /// <summary> 单相机定位模式</summary>
        SingleCamLocal = 0,
        /// <summary> 双相机定位模式 </summary>
        TwoCamLocal,
        /// <summary> 单相机双视野定位</summary>
        TwoMarkLocal,
        /// <summary>单Mark定位检测</summary>
        SingleMarkInspect,
        /// <summary>双Mark定位检测</summary>
        TwoMarkInspect,
    }
    /// <summary>
    /// 工程系统视觉参数集枚举
    /// </summary>
    public enum ProjectVisionEnum
    {
        /// <summary>工程系统中第1套视觉参数 </summary>
        ProjectVision0 = 0,
        /// <summary>工程系统中第2套视觉参数 </summary>
        ProjectVision1,
        /// <summary>工程系统中第3套视觉参数 </summary>
        ProjectVision2,
        /// <summary>工程系统中第4套视觉参数 </summary>
        ProjectVision3
    }

    
}
