using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HalconDotNet;
using VisionBase.Matching;

namespace VisionBase
{
    public struct St_TemplateParam
    {
        public En_TemplateMatchingType MatchingType;
        public double StartAngle;
        public double EndAngle;
        public int Level;
        public double  Score;
        public double Scale;
        public double MaxError;
        public double  CenterX;
        public double  CenterY;
        public double  TemplateAngle;
        public string TemplatePath;
        public double OffsetXMax;
        public double OffsetXMin;
        public double OffsetYMax;
        public double OffsetYMin;
        public int Area;
        public int ContLength;
        [System.Xml.Serialization.XmlIgnore]
        private MatchingModule Matching;
        private bool IsLoadOK;
        public St_TemplateParam(bool isInit=true)
        {
            MatchingType = En_TemplateMatchingType.形状匹配;
            StartAngle = -10;
            EndAngle = 10;
            Level = 5;
            Score = 70;
            Scale = 1.0;
            CenterX = 0;
            CenterY = 0;
            TemplateAngle = 0;
            OffsetXMax = 5;
            OffsetXMin = -5;
            OffsetYMax = 5;
            OffsetYMin = -5;     
            Matching = new MatchingModule(); 
            IsLoadOK = false;
            TemplatePath = "";
            MaxError = 50;
            Area = 50;
            ContLength = 0;
            Matching.InitMatchingParam(this);
        }

        public bool Load(string path)
        {
            if (Matching == null) {
                Matching = new MatchingModule();
                Matching.InitMatchingParam(this);
            }
            //string tempName = path + "ModelImage.shm";
            TemplatePath = path;
            if (!FileLib.DirectoryEx.Exist(TemplatePath, false)) return false;
            IsLoadOK = Matching.LoadShapeModel(TemplatePath);
            return IsLoadOK;
        }

        public bool Save(string path)
        {
            if (Matching == null) Matching = new MatchingModule();
            //string tempName = path + "ModelImage.shm";
            return Matching.SaveShapeModel(path);
        }

        public bool SetTrainImage(string path)
        {
            return Matching.SetTrainImage(path);
        }

        public bool CreateShapeModel(HObject srcImg,double row1, double column1, double row2, double column2)
        {
            HObject MeanImg = new HObject();
            HOperatorSet.MeanImage(srcImg, out MeanImg, 3, 3);
            bool isOk=Matching.CreateShapeModel(MeanImg, row1, column1, row2, column2, TemplatePath);
            MeanImg.Dispose();

            return isOk;
        }
        public bool CreateShapeModel(HObject srcImg, HRegion ModelRoi)
        {
            HObject MeanImg = new HObject();
            HOperatorSet.MeanImage(srcImg, out MeanImg, 3, 3);
            HObject ScaleImg = new HObject();
            HOperatorSet.ZoomImageFactor(MeanImg, out ScaleImg, Scale, Scale, "constant");
            ModelRoi= ModelRoi.ZoomRegion(Scale, Scale);
            bool isOk = Matching.CreateShapeModel(ScaleImg, ModelRoi, TemplatePath);         
            MeanImg.Dispose();
            ScaleImg.Dispose();
            return isOk;
        }
        public bool GetShapeModelContour(out HXLD xldContour)
        {
            Matching.GetShapeModelContour(out  xldContour);
            return true;
        
        }
      

        public bool FindSharpTemplate(string ccdName, HObject srcImg, RectangleF searchArea, out MatchingResult result)
        {
            result = new MatchingResult();
           // if (!IsLoadOK) return false;
           Matching.InitMatchingParam(this);
           if (!Matching.FindShapeModel(srcImg, searchArea, out result)){
                Logger.PopError(ccdName+"查找模板失败！");
                return false;
            }
            return true;
        }

        public bool DetectTemplateInfo(HObject srcImg, RectangleF searchArea,out int srcArea,out int srcLength)
        {
            double cx, cy;
            return Matching.FindRegion(srcImg, searchArea, out srcArea, out srcLength,out cx,out cy);
        }

        public bool DetectTemplateInfo(HObject srcImg, HRegion RegionIn, out int srcArea, out int srcLength)
        {
            double cx, cy;
            return Matching.FindRegion(srcImg, RegionIn, out srcArea, out srcLength, out cx, out cy);
        }

        public bool FindRegion(HObject srcImg, RectangleF searchArea, out double cx, out double cy)
        {
            int srcArea=Area ,srcLength=ContLength;
            bool isVerityOk = true;
            double maxLength = srcLength * 1.2;
            double minLength = srcLength * 0.8;
            double maxArea = srcArea * 1.2;
            double minArea = srcArea * 0.8;
            int contLength,area;
            if (!Matching.FindRegion(srcImg, searchArea, out area, out contLength,out cx,out cy))
            {
                return false;
            }

            string errorMsg = "找胶带";
            //if (area < minArea)
            //{
            //    errorMsg += string.Format("轮廓面积{0}<{1},", area, minArea);
            //    isVerityOk = false;
            //}
            //else if (area > maxArea)
            //{
            //    errorMsg += string.Format("轮廓面积{0}>{1},", area, maxArea);
            //    isVerityOk = false;
            //}
            //else
            //{
            //    errorMsg += string.Format("轮廓面积{0}<{1}<{2},", minArea,area, maxArea);
            //}

            //if (contLength<minLength)
            //{
            //    errorMsg += string.Format("轮廓长度{0}<{1}", contLength, minLength);
            //    isVerityOk = false;
            //}
            //else if(contLength>maxLength)
            //{
            //    errorMsg += string.Format("轮廓长度{0}>{1}", contLength, maxLength);
            //    isVerityOk = false;
            //}
            //else
            //{
            //    errorMsg += string.Format("轮廓长度{0}<{1}<{2},", minLength, contLength, maxLength);
            //}

            if (!isVerityOk) Logger.PopError(errorMsg);
            else Logger.Pop(errorMsg);

            return isVerityOk;
        }

        public bool FindSharpTemplate(string ccdName,int tapeIndex, HObject srcImg, RectangleF searchArea, out MatchingResult result)
        {
            result = new MatchingResult();

            // if (!IsLoadOK) return false;

            Matching.InitMatchingParam(this);

            if (!Matching.FindShapeModel(srcImg, searchArea, out result))
            {
                string str = string.Format(ccdName + "查找第{0}个胶带模板失败！",tapeIndex+1);
                Logger.Pop(str);
                return false;
            }

            return true;
        }

        public bool FindSharpTemplate(HObject srcImg ,RectangleF searchArea,St_TemplateParam currParm,out MatchingResult result)
        {
            result = new MatchingResult();

           // if (!IsLoadOK) return false;

            Matching.InitMatchingParam(currParm);
           
            if (!Matching.FindShapeModel(srcImg, searchArea, out result))
            {
                Logger.PopError("查找模板失败！");
                return false;
            }
            return true;
        }

        public void GetDetectionContour(out HXLD xldContour)
        {
            Matching.GetDetectionContour(out xldContour);
        }
    }
}
