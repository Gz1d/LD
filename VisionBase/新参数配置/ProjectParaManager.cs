using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileLib;

namespace VisionBase
{

    public class ProjectMsg
    { 
        public ProjectModelEnum ProjectModel {get;set; }

        public string ProjectDescribe { get; set; }

        public ProjectMsg()
        {
            ProjectModel = ProjectModelEnum.SingleCamLocal;
            ProjectDescribe = "";
        }   
    }

    /// <summary>
    /// 工程枚举
    /// </summary>
    public enum ProjectEnum
    { 
        /// <summary> 平台0扎针定位 </summary>
        PinLocal0=0,
        /// <summary> 平台1扎针定位 </summary>
        PinLocal1,
        /// <summary> 平台2扎针定位 </summary>
        PinLocal2,
        /// <summary> 平台3扎针定位 </summary>
        PinLocal3,
        /// <summary>FoF偏移检测 </summary>
        FofOffSet
       
    }


   public  class ProjectParaManager
    {
        private static object syncObj = new object();
        private static ProjectParaManager _instance;
        //public List<ProjectModelEnum> ProjectModelList = new List<ProjectModelEnum>();
        public List<ProjectPara> ProjectParaList = new List<ProjectPara>();
        public List<ProjectMsg> ProjectMsgList = new List<ProjectMsg>();
        private static string ParaPath = @"D\VisionConfig";
        public string ProductName = "";

        public static ProjectParaManager Instance{
            get{
                if (_instance == null){
                    lock (syncObj){
                        _instance = new ProjectParaManager();
                    }
                }
                return _instance;
            }
        }


        public bool Save()
        {
            bool IsOk = true;
            string ExePath = System.Environment.CurrentDirectory;
            DirectoryEx.DeleteFolder1(ExePath + @"\"+ParaPath);
            if (!DirectoryEx.Exist(ExePath+ @"\" + ParaPath)) DirectoryEx.Create(ExePath + @"\" + ParaPath);      
            IsOk = IsOk && XML<List<ProjectMsg>>.Write(this.ProjectMsgList, ExePath + @"\" + ParaPath + @"\" + "ProjectMsgList.xml");
            ProjectPara ProjectParaI = new ProjectPara();
            for (int i = 0; i < this.ProjectMsgList.Count(); i++) {
                ProjectParaI = this.ProjectParaList[i];
                IsOk = IsOk && ProjectParaI.Save(ExePath + @"\" + ParaPath + @"\" + i.ToString() + this.ProjectMsgList[i].ProjectDescribe+ @"\" );
            }
            return IsOk;              
        }

        public bool Read()
        {
            bool IsOk = true;
            string ExePath = System.Environment.CurrentDirectory;
            ProjectMsgList = XML<List<ProjectMsg>>.Read(ExePath + @"\" + ParaPath + @"\" + "ProjectMsgList.xml");
            ProjectPara ProjectParaI = new ProjectPara();
            if (this.ProjectMsgList == null) { 
               this.ProjectParaList = new List<ProjectPara>();
                return true;
            }
            for (int i = 0; i < ProjectMsgList.Count(); i++) {
                ProjectParaI = new ProjectPara();
                IsOk = IsOk&& ProjectParaI.Read(ExePath + @"\" + ParaPath + @"\" + i.ToString() + this.ProjectMsgList[i].ProjectDescribe + @"\");
                this.ProjectParaList.Add(ProjectParaI);
            }
            return IsOk;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectNum"></param>
        /// <returns></returns>
        public ProjectPara GetProjectPara(int ProjectNum)
        {
            ProjectEnum NowProject = (ProjectEnum)Enum.Parse(typeof(ProjectEnum), ProjectNum.ToString(), false);
            return this.ProjectParaList[ProjectNum];
            foreach (ProjectPara item in ProjectParaList) {
                if (item.ProjectName == NowProject){
                    return item;            
                }                   
            }
            return new ProjectPara();
        }


    }
}
