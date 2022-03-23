using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HalconDotNet;



namespace VisionBase.Matching
{
    public struct MatchResult_Ncc
    {
        public int X;
        public int Y;
        public double Angle;
        public double Score;
        public int Width;
        public int Height;
    }

    public struct NccModelName
    {
        public string PartName;
        public HNCCModel ModelID;
    }

    public  class MatchingModule
    {
        private  List<NccModelName> m_listPartModelID = new List<NccModelName>();
        //private static HNCCModel m_currentNccModelId=null;
        private  HTuple m_roiRow, m_roiColumn;
        private  HRegion m_hoRectangle = new HRegion();
        private  MatchingAssistant m_MatchAssistant;
        private  MatchingParam m_MatchingParam=new MatchingParam();
        private  HObject m_hTemplate, m_hMatchImage;
        private  int m_iNumLevels = 4;


        #region 基于形状匹配
               
        /// <summary>
        /// 初始化模板匹配参数
        /// </summary>
        /// <param name="parSet"></param>
        public void InitMatchingParam(MatchingParam parSet)
        {
            m_MatchingParam = parSet;
            m_MatchingParam.mNumMatches = 1;
            m_MatchingParam.mMaxOverlap = 0.5;
            m_MatchingParam.mSubpixel = "least_squares";

            if (m_MatchAssistant == null)
                m_MatchAssistant = new MatchingAssistant(m_MatchingParam);
        }

        public void InitMatchingParam(St_TemplateParam parSet)
        {
            m_MatchingParam.mStartingAngle = parSet.StartAngle*Math.PI/180.0;
            m_MatchingParam.mAngleExtent = (parSet.EndAngle - parSet.StartAngle) * Math.PI / 180.0;
            m_MatchingParam.mNumLevel = parSet.Level;
            m_MatchingParam.mMinScore = parSet.Score/100.0;
            m_MatchingParam.mNumMatches = 1;
            m_MatchingParam.mMaxOverlap = 0.5;
            m_MatchingParam.mSubpixel = "none";//"least_squares";
            m_MatchingParam.mGreediness = 0.9;
            m_MatchingParam.mMaxError = parSet.MaxError;
            m_MatchingParam.mImageSizeScale = parSet.Scale;

            if (m_MatchAssistant==null)
                m_MatchAssistant = new MatchingAssistant(m_MatchingParam);
        }

        public bool SetTrainImage(string path)
        {
            return m_MatchAssistant.setImage(path);
        }

        public  bool LoadShapeModel(string path)
        {
            bool isSuccess = true;
            string modelPath = path;

            m_MatchAssistant.reset();

            if (m_MatchAssistant.loadShapeModel(path))
            {

            }
            else
            {
                isSuccess = false;
                Logger.PopError("加载模板：" + path + "失败",false);
            }

            return isSuccess;
        }

        public  bool SaveShapeModel(string strPath)
        {
            if (m_MatchAssistant == null) return false;
            bool isSuccess = true;

            string filePth = strPath;

            try
            {
                m_MatchAssistant.saveShapeModel(filePth);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                Logger.PopError("保存模板失败：" + ex.Message.ToString(),true);
            }

            return isSuccess;
        }

        public bool CreateShapeModel(HObject srcImg,double row1, double column1, double row2, double column2,string filePath)
        {
            if (srcImg != null)
            {
                m_MatchAssistant.setImage(new HImage(srcImg));
            }
            HRegion tmpRoi = new HRegion(row1, column1, row2, column2);
            m_MatchAssistant.setModelROI(tmpRoi);
            return m_MatchAssistant.createShapeModel(filePath);
        }

        public bool CreateShapeModel(HObject srcImg, HRegion RoiIn, string filePath)
        {
            if (srcImg != null)
            {
                m_MatchAssistant.setImage(new HImage(srcImg));
            }
            HRegion tmpRoi = RoiIn;
            m_MatchAssistant.setModelROI(tmpRoi);
            return m_MatchAssistant.createShapeModel(filePath);
         }

        public bool GetShapeModelContour(out HXLD xldContour)
        {

            xldContour = m_MatchAssistant.getModelContour();
            return true;
        }

        public void InspectShapeModel()
        {
            m_MatchAssistant.inspectShapeModel();
        }

        public  void ClearModel()
        {
            m_MatchAssistant.resetCachedModelParams();
        }

        private int SharpImageNumber = 0;

        public bool FindRegion(HObject srcImg, RectangleF serchArea, out int area, out int contLength, out double centerX, out double centerY)
        {
            contLength = 0; area = 0; centerX = 0; centerY = 0;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            try
            {
                HTuple row1, row2, column1, column2,regionArea;
                HTuple mean, deviation, number,length;
                HObject imgReduced,targetRegion;
                row1 = serchArea.Y;
                row2 = serchArea.Y + serchArea.Height;
                column1 = serchArea.X;
                column2 = serchArea.X + serchArea.Width;
                HObject region, grayRegion, selectedRegion, connectedRegion, regionBorder;
                HOperatorSet.GenRectangle1(out region,row1,column1,row2,column2);
                HOperatorSet.ReduceDomain(srcImg, region, out imgReduced);
                HOperatorSet.Intensity(region, imgReduced, out mean, out deviation);
                mean = mean + 20;
                mean = mean < 200 ? mean : new HTuple(200);
                HOperatorSet.Threshold(imgReduced, out grayRegion, 0, mean);
                HOperatorSet.Connection(grayRegion, out connectedRegion);
                HOperatorSet.SelectShape(connectedRegion, out selectedRegion, "area", "and", 250000, 1000000);
                HOperatorSet.CountObj(selectedRegion, out number);
                connectedRegion.Dispose();
                grayRegion.Dispose();
                imgReduced.Dispose();
                if (number.I ==1)
                {
                    HOperatorSet.SelectObj(selectedRegion, out targetRegion, 1);
                    HOperatorSet.AreaCenter(targetRegion, out regionArea, out row1, out column1);
                    HOperatorSet.Boundary(targetRegion, out regionBorder, "inner");
                    HOperatorSet.Contlength(regionBorder, out length);
                    area=(int)regionArea.D;
                    contLength = (int)length.D;
                    centerX = column1.D;
                    centerY = row1.D;
                    selectedRegion.Dispose();
                    targetRegion.Dispose();
                    regionBorder.Dispose();
                }
                else
                {
                    Logger.PopError("轮廓长度算法异常，轮廓数量=" + number.ToString());  
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.PopError("轮廓长度算法异常：" + ex.Message.ToString());        
                throw;
            }
       
            return true;
        }


        public bool FindRegion(HObject srcImg,HObject RegionIn , out int area, out int contLength, out double centerX, out double centerY)
        {
            contLength = 0; area = 0; centerX = 0; centerY = 0;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            try
            {
                HTuple row1, column1,regionArea;
                HTuple mean, deviation, number, length;
                HObject imgReduced, targetRegion;

                HObject region, grayRegion, selectedRegion, connectedRegion, regionBorder;
                region = RegionIn;
                HOperatorSet.ReduceDomain(srcImg, region, out imgReduced);
                HOperatorSet.Intensity(region, imgReduced, out mean, out deviation);
                mean = mean + 20;
                mean = mean < 200 ? mean : new HTuple(200);
                HOperatorSet.Threshold(imgReduced, out grayRegion, 0, mean);
                HOperatorSet.Connection(grayRegion, out connectedRegion);
                HOperatorSet.SelectShape(connectedRegion, out selectedRegion, "area", "and", 250000, 1000000);
                HOperatorSet.CountObj(selectedRegion, out number);
                connectedRegion.Dispose();
                grayRegion.Dispose();
                imgReduced.Dispose();
                if (number.I == 1)
                {
                    HOperatorSet.SelectObj(selectedRegion, out targetRegion, 1);
                    HOperatorSet.AreaCenter(targetRegion, out regionArea, out row1, out column1);
                    HOperatorSet.Boundary(targetRegion, out regionBorder, "inner");
                    HOperatorSet.Contlength(regionBorder, out length);
                    area = (int)regionArea.D;
                    contLength = (int)length.D;
                    centerX = column1.D;
                    centerY = row1.D;
                    selectedRegion.Dispose();
                    targetRegion.Dispose();
                    regionBorder.Dispose();
                }
                else
                {
                    Logger.PopError("轮廓长度算法异常，轮廓数量=" + number.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.PopError("轮廓长度算法异常：" + ex.Message.ToString());
                throw;
            }

            return true;
        }

        public  bool FindShapeModel(HObject srcImg ,RectangleF searchArea,out MatchingResult result)
        {
            bool isSuccess = true;
            result = new MatchingResult();
            result.mScore = new HTuple();
            HObject reducedImage=new HObject();
            HObject roi=new HObject();
            HOperatorSet.GenEmptyObj(out roi);
            HOperatorSet.GenEmptyObj(out reducedImage);
            
            try
            {
                if (srcImg == null)
                {
                    m_MatchAssistant.setImage();
                }
                else
                {
                    double NowScale = m_MatchingParam.mImageSizeScale;
                    HOperatorSet.GenRectangle1(out roi,searchArea.Y  * NowScale, searchArea.X* NowScale, 
                                                      (searchArea.Y+searchArea.Height)* NowScale, (searchArea.X+searchArea.Width)* NowScale);
                    HObject ScaleImg = new HObject();
                    HOperatorSet.ZoomImageFactor(srcImg, out ScaleImg, NowScale, NowScale, "constant");
                    HOperatorSet.ReduceDomain(ScaleImg, roi, out reducedImage);
                    m_MatchAssistant.setTestImage(reducedImage);
                }
                  //  m_MatchAssistant.setImage(new HImage(srcImg));
                isSuccess = m_MatchAssistant.applyFindModel();
                if(isSuccess)
                {
                    result=m_MatchAssistant.getMatchingResults();
                    if(result.count<1)
                    {
                        Logger.PopError("找模板失败");
                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (reducedImage != null) reducedImage.Dispose();
                if (roi != null) roi.Dispose();
                isSuccess = false;
                Logger.PopError("找模板失败：" + ex.Message.ToString());
            }

            if(isSuccess)
            {
                //string ccdName = CameraTest.Instance.GetCCDName(true, true, 1);
                //ViewControl view = DisplaySystem.GetViewControl(ccdName);
                //HImage img = m_MatchAssistant.getCurrTestImage();
                //view.AddViewImage(img);
                //HXLD contour;
                //GetDetectionContour(out contour);
                //view.AddViewObject(contour);
                //view.AddViewCross(2048/2, 2448 / 2, 2048, 0);
                //view.AddViewLine(0, 2448 / 2, 2048,2448/2);
                //view.AddViewLine(2048 / 2, 0, 2048/2, 2448);
                //view.ShowAiming();
                //view.Repaint();
                //string str = string.Format("匹配度：{0}%", (100 * result.mScore.D).ToString("f2"));
                //view.SetString(20, 50, "red", str);
            }
            else
            {
                if (srcImg == null) return false;
                //HOperatorSet.WriteImage(srcImg, "bmp", 0, "D:\\ReducedImage\\Model_SrcImage_" + SharpImageNumber.ToString() + ".bmp");
                //HOperatorSet.WriteImage(reducedImage, "bmp", 0, "D:\\ReducedImage\\Model_ReducedImage_" + SharpImageNumber.ToString() + ".bmp");
                if (reducedImage != null) reducedImage.Dispose();
                if (roi != null) roi.Dispose();
                SharpImageNumber++;
            }

            return isSuccess;
        }

        public void GetDetectionContour(out HXLD xldContour)
        {
            xldContour=m_MatchAssistant.getDetectionResults();
        }

        #endregion


        #region 基于NCC相关性匹配

        private  bool CheckPartModelIdIsExist(string partName, ref int modelIndex)
        {
            string strTemp;

            for (int i = 0; i < m_listPartModelID.Count; i++)
            {
                strTemp = m_listPartModelID[i].PartName;
                if (partName == strTemp)
                {
                    modelIndex = i;
                    return true;
                }
            }

            return false;
        }

        public  void SetNumLevels(int levels)
        {
            m_iNumLevels = levels;
        }

        public  void CreateNccModel(string partName,System.Drawing.Bitmap srcImage,System.Drawing.Rectangle roi)
        {
            try
            {
                int partIndex = 0;
                NccModelName newModel = new NccModelName();
                if (m_hTemplate != null) m_hTemplate.Dispose();

                if (m_hoRectangle != null) m_hoRectangle.Dispose();
                m_hoRectangle.GenRectangle1(1.0 * roi.Top, roi.Left, roi.Bottom, roi.Right);
                m_hoRectangle.AreaCenter(out m_roiRow, out m_roiColumn);

                if (srcImage != null)
                {
                    HImage hImage = new HImage();//VisionProcess.Bitmap2HImage_8(srcImage);
                    HImage hImageReduced;

                    hImageReduced = hImage.ReduceDomain(m_hoRectangle);

                    if (CheckPartModelIdIsExist(partName, ref partIndex))
                    {
                        newModel.PartName = partName;
                        newModel.ModelID = hImageReduced.CreateNccModel(m_iNumLevels, 0, 0, "auto", "use_polarity");
                        m_listPartModelID.RemoveAt(partIndex);
                    }
                    else
                    {
                        newModel.ModelID = hImageReduced.CreateNccModel("auto", 0, 0, "auto", "use_polarity");
                        newModel.PartName = partName;
                    }
                    m_listPartModelID.Add(newModel);
                }
            }
            catch
            {
                //LogFile.AppendText("CreateNccModel函数Exception:" + ex.Message.ToString());
                //System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }

        public  void LoadNccModel(string partName,string fileName)
        {
            try
            {
                int modelID = 0;
                if (CheckPartModelIdIsExist(partName, ref modelID))
                {
                    m_listPartModelID[modelID].ModelID.ReadNccModel(fileName);
                }
                else
                {
                    NccModelName newModel = new NccModelName();
                    newModel.ModelID =  new HNCCModel(fileName);
                    newModel.PartName = partName;
                    m_listPartModelID.Add(newModel);
                }
            }
            catch 
            {
                //LogFile.AppendText("LoadNccModel函数Exception:" + ex.Message.ToString());
                //System.Windows.MessageBox.Show("模板：" + fileName + "不存在，" + ex.Message.ToString());
            }
        }

        public  bool FindNccModel(string partName,System.Drawing.Bitmap srcImage, System.Drawing.Rectangle roi, ref MatchResult_Ncc result)
        {
            if (srcImage == null) return false;

            bool bIsSuccess = false;
            HTuple row, column, angle, score;
            HTuple hv_HomMat2D;
            HImage hImage = new HImage();

            try
            {
                int modelID = 0;

                GC.Collect();
                if (!CheckPartModelIdIsExist(partName, ref modelID)) return false;

                if (m_hoRectangle != null) m_hoRectangle.Dispose();
                m_hoRectangle.GenRectangle1(1.0 * roi.Top, roi.Left, roi.Bottom, roi.Right);
                m_hoRectangle.AreaCenter(out m_roiRow, out m_roiColumn);

                hImage.GenEmptyObj();
                //hImage = VisionProcess.Bitmap2HImage_8(srcImage);
                hImage.FindNccModel(m_listPartModelID[modelID].ModelID, 0, 0, 0.6, 1, 0.5, "true", m_iNumLevels, out row, out column, out angle, out score);

                if (score > 0.1)
                {
                    HOperatorSet.VectorAngleToRigid(m_roiRow, m_roiColumn, new HTuple(0), row, column, new HTuple(0), out hv_HomMat2D);
                    HRegion affRoi = m_hoRectangle.AffineTransRegion(new HHomMat2D(hv_HomMat2D), "false");

                    int right, bottom;
                    affRoi.SmallestRectangle1(out result.Y, out result.X, out bottom, out right);
                    result.Width = right - result.X;
                    result.Height = bottom - result.Y;
                    result.Score = 100 * score.D;

                    bIsSuccess = true;
                }

                GC.Collect();
            }
            catch
            {
                srcImage.Save("FindNccModelException_" + partName,System.Drawing.Imaging.ImageFormat.Bmp);
                //LogFile.AppendText("FindNccModel函数Exception:"+ex.Message.ToString());
                //System.Windows.MessageBox.Show(ex.Message.ToString());
                bIsSuccess = false;
            }

            return bIsSuccess;
        }

        public  void SaveNccModel(string partName,string fileName)
        {
            try
            {
                int modelID = 0;
                if (!CheckPartModelIdIsExist(partName, ref modelID)) return;

                if (m_listPartModelID[modelID].ModelID != null)
                    m_listPartModelID[modelID].ModelID.WriteNccModel(fileName);
            }
            catch 
            {
                //LogFile.AppendText("SaveNccModel函数Exception:" + ex.Message.ToString());
                //System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }

#endregion
    }
}
