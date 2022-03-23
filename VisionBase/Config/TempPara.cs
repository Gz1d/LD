using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using HalconDotNet;
using VisionBase.Matching;

namespace VisionBase.Config
{
    /// <summary>
    /// 模板参数配置类
    /// </summary>
    [Serializable]
    public class TempParaItem : INotifyPropertyChanged

    {
        //属性改变时向外界发送通知
        public virtual event PropertyChangedEventHandler PropertyChanged;


        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(info));
            }
        }
        public En_TemplateMatchingType MatchingType
        {
            get;
            set;     
        }
        public double StartAngle
        {
            get;
            set;     
        }
        public double EndAngle
        {
            get;
            set;
        }
        
        public int Level
        {
            get;
            set;
        }

        public double Score
        {
            get;
            set;
        }

        public double Scale
        {
            get;
            set;
        }

        public int SearchStartRow
        {
            get;
            set;
        }

        public int SearchStartCol
        {
            get;
            set;
        }
        public int SearchEndRow
        {
            get;
            set;
        }
        public int SearchEndCol
        {
            get;
            set;
        }
        public bool IsLoadOk
        {
            get;
            set;
        }


        public St_VectorAngle TeachVector
        {
            get;
            set;
        }

        private MatchingModule Matching
        {
            get;
            set;
        }


        //public bool MatchingLoad(string path)
        //{
        //    if (Matching == null)
        //    {
        //        Matching = new MatchingModule();
        //        Matching.InitMatchingParam(this);
        //    }
        //    TemplatePath = path;
        //    if (!FileLib.DirectoryEx.Exist(TemplatePath, false)) return false;
        //    IsLoadOK = Matching.LoadShapeModel(TemplatePath);

        //    return IsLoadOk;
        //}



    }



}
