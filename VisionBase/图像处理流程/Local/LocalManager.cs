using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace VisionBase
{
  public   class LocalManager
   {
        private LocalBase MyLocal;
        /// <summary>
        /// 实例不同定位类，执行不同的定位
        /// </summary>
        /// <param name="LocalIn"></param>
        public void SetLocalModel(LocalBase LocalIn)
        {
            MyLocal = LocalIn;        
        }

        public void SetLocalModel(LocalModelEnum LocalModelIn)
        {
            switch (LocalModelIn) {
                case LocalModelEnum.Temp:  
                    MyLocal = new TempLocal();
                    break;
                case LocalModelEnum.TwoLine:
                case LocalModelEnum.ThreeLine:
                    MyLocal = new TwoLineLocal();
                    break;
                case LocalModelEnum.FourLine:
                    MyLocal = new FourLineLocal();
                    break;
                case LocalModelEnum.TwoCircle:
                    MyLocal = new TwoCycleLocal();
                    break;
                case LocalModelEnum.LineCircle:
                    MyLocal = new LineCircleLocal();
                    break;
                case LocalModelEnum.TempTwoLine:
                case LocalModelEnum.TempThreeLine:
                    MyLocal = new TempTwoLineLocal();
                    break;
                case LocalModelEnum.TempFourLine:
                    MyLocal = new TempFourLineLocal();
                    break;
                case LocalModelEnum.TempOneCircle:
                    MyLocal = new TempOneCircleLocal();
                    break;
                case LocalModelEnum.TempTwoCircle:
                    MyLocal = new TempTwoCircleLocal();
                    break;
                case LocalModelEnum.TempLineCircle:
                    MyLocal = new TempLineCirLocal();
                    break;
                case LocalModelEnum.Blob:
                    MyLocal = new BlobLocal();
                    break;
                case LocalModelEnum.BlobTwoLine:
                    MyLocal = new BlobTwoLineLocal();
                    break;
                case LocalModelEnum.BlobLinCirRectInsp:
                    MyLocal = new BlobLineCirRectInsp();
                    break;
                case LocalModelEnum.TempBlob:
                    MyLocal = new TempBlobLocal();
                    break;
                case LocalModelEnum.TempLinCirRectInsp:
                    MyLocal = new TempLocalLineCircRectInsp();
                    break;
                case LocalModelEnum.TwoLineLocalLinCirRectInsp:
                    MyLocal = new LineLocalLineCirRectInsp();               
                    break;
                case LocalModelEnum.TempTwoLineLocalLinCirRectInsp:
                    MyLocal = new TempTwoLineLocalLineCirRectInsp();
                    break;
                case LocalModelEnum.TempTwoCircleLocalLinCirRectInsp:
                    MyLocal = new TempTwoCircleLocalLineCirRectInsp();
                    break;
                case LocalModelEnum.TempLineCircleLocalLinCirRectInsp:
                    MyLocal = new TempLineCircleLocalLinCirRectInsp();
                    break;

            }     
        }
        /// <summary>
        /// 传入图片和检测参数
        /// </summary>
        /// <param name="ImgIn"></param>
        /// <param name="VisionParaIn"></param>
        public void SetParam(HObject ImgIn, LocalPara VisionParaIn)
        {
            MyLocal.Set(ImgIn, VisionParaIn);
        }

        public void SetLocalPara(LocalPara VisionParaIn)
        {
            MyLocal.SetLocalPara(VisionParaIn);      
        }

        public void SetLocalImg(HObject ImgIn)
        {
            MyLocal.SetLocalImg(ImgIn);       
        }
        /// <summary>
        /// 执行定位
        /// </summary>
        public bool doLocal()
        {
           return  MyLocal.doLocal();
        }
        /// <summary>
        /// 获取定位结果
        /// </summary>
        /// <returns></returns>
        public LocalResult GetResult()
        {
            return MyLocal.GetResult();       
        }

    }
}
