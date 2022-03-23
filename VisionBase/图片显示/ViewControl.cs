using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using ViewROI;
using System.Threading;
using System.Drawing;

namespace VisionBase
{
   public class ViewControl
    {
        public volatile bool IsShowing = false;
        public static volatile bool PauseShow = false;
        public  HWndCtrl viewController;
        public  ROIController roiController=new ROIController();//ROI控制器
        private HTuple imageWidth;//当前的图像宽度
        private HTuple imageHeight;//当前的图像高度
        private HWindowControl Hwnd;
        private HObject CurrentImageObj;
        private List<HObject> hObjectList = new List<HObject>();
        public volatile object globalLock = new object();

        public void IntialView(HWindowControl hlView, HTuple imageWidth, HTuple imageHeight)
        {
            lock (globalLock) {
                try {
                    if (viewController != null) viewController.clearList();
                    viewController = new HWndCtrl(hlView);
                    viewController.useROIController(roiController);
                    viewController.setViewState(HWndCtrl.MODE_VIEW_MOVE);
                    roiController.setROISign(ROIController.MODE_ROI_NONE);
                    this.imageWidth = imageWidth;
                    this.imageHeight = imageHeight;
                    Hwnd = hlView;
                    SetDraw("red", "margin");
                    HOperatorSet.GenEmptyObj(out AimShow);
                    AddViewCross(imageHeight / 2, imageWidth / 2, imageWidth / 10, 0, true);
                    //AddViewRec2(imageHeight / 2, imageWidth / 2, imageWidth / 30, imageWidth / 30, 0, true);
                    AddViewLine(imageHeight / 15, imageWidth / 15, imageHeight / 15 + imageHeight / 5, imageWidth / 15, true);
                    AddViewLine(imageHeight / 15, imageWidth / 15, imageHeight / 15, imageWidth / 15 + imageWidth / 5, true);
                    AddViewLine(imageHeight - imageHeight / 15 - imageHeight / 5, imageWidth / 15, imageHeight - imageHeight / 15, imageWidth / 15, true);
                    AddViewLine(imageHeight - imageHeight / 15, imageWidth / 15, imageHeight - imageHeight / 15, imageWidth / 15 + imageWidth / 5, true);
                    AddViewLine(imageHeight - imageHeight / 15 - imageHeight / 5, imageWidth - imageWidth / 15, imageHeight - imageHeight / 15, imageWidth - imageWidth / 15, true);
                    AddViewLine(imageHeight - imageHeight / 15, imageWidth - imageWidth / 15 - imageWidth / 5, imageHeight - imageHeight / 15, imageWidth - imageWidth / 15, true);
                    AddViewLine(imageHeight / 15, imageWidth - imageWidth / 15, imageHeight / 15 + imageHeight / 5, imageWidth - imageWidth / 15, true);
                    AddViewLine(imageHeight / 15, imageWidth - imageWidth / 15, imageHeight / 15, imageWidth - imageWidth / 15 - imageWidth / 5, true);
                    foreach (var item in hObjectList){
                        if (item != null) item.Dispose();
                    }
                    hObjectList.Clear();
                    //this.HMouseClick = this.viewController.HMouseClick;
                    this.viewController.HMouseClick += new HMouseEventHandler(HMouseDown)  ;
                }
                catch
                { }
            }
        }
 
        public void Refresh()
        {
            if (this == null || Hwnd == null) return;
            lock (globalLock){
                try{
                    viewController.clearList();
                    viewController.repaint();
                }
                catch
                { }
            }
        }

        public void ResetView()
        {
            lock(globalLock){
                try{
                    foreach (var item in hObjectList){
                        if (item != null) item.Dispose();
                    }
                    hObjectList.Clear();
                    viewController.resetAll();
                    viewController.clearList();
                    viewController.changeGraphicSettings(GraphicsContext.GC_LINESTYLE, new HTuple());
                }
                catch
                { }
            }
        }
        /// <summary>
        /// 清空显示窗体中的Hobject对象
        /// </summary>
        public void ResetWindow()
        {
            lock (globalLock){
                try {
                    foreach (var item in hObjectList){
                        if (item != null) item.Dispose();
                    }
                    hObjectList.Clear();
                    viewController.clearList();
                }
                catch { }
            }
        }

        /// <summary>
        /// 会调整显示窗口的缩放比列
        /// </summary>
        /// <param name="Image"></param>
        public void AddViewImage(HObject Image)
        {
            if (this == null || Hwnd == null || Image == null || !Image.IsInitialized()) return;
            if (!PauseShow) {
                lock (globalLock) {
                    try {
                        if (Image != null){
                            if (CurrentImageObj != null) CurrentImageObj.Dispose();
                            CurrentImageObj = new HImage(Image);
                            viewController.addIconicVar(CurrentImageObj);
                        }
                    }
                    catch (Exception ex) {
                        Logger.PopError(ex.Message.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 不会调整显示窗口的缩放比列
        /// </summary>
        /// <param name="Img"></param>
        public void AddImage(HObject Image)
        {
            if (this == null || Hwnd == null || Image == null || !Image.IsInitialized()) return;
            if (!PauseShow){
                lock (globalLock){
                    try{
                        if (Image != null) {
                            if (CurrentImageObj != null) CurrentImageObj.Dispose();
                            CurrentImageObj = new HImage(Image);
                            viewController.addImg(CurrentImageObj);
                        }
                    }
                    catch (Exception ex){
                        Logger.PopError(ex.Message.ToString());
                    }
                }
            }
        }


        public void AddViewObject(HObject obj)
        {
            if (this == null || Hwnd == null || obj == null || !obj.IsInitialized()) return;
            if (!PauseShow){
                try {
                    if (obj == null) return;
                    if (obj is HImage){  AddViewImage(obj); }
                    else {viewController.addIconicVar(obj); }
                }
                catch(Exception ex){
                    Logger.PopError(ex.Message.ToString());
                }
            }
        }
        /// <summary>
        ///  替换掉最后一个显示对象
        /// </summary>
        /// <param name="obj"></param>
        public void AddViewObjectShow(HObject obj)
        {
            if (this == null || Hwnd == null || obj == null || !obj.IsInitialized()) return;
            if (!PauseShow) {
                try {
                    if (obj == null) return;
                    if (obj is HImage) return;
                    else {viewController.addIconicVarShow(obj); }
                }
                catch (Exception ex){
                    Logger.PopError(ex.Message.ToString());
                }
            }

        }
        private HObject AimShow;
        public void AddViewCross(HTuple row, HTuple col, HTuple len, HTuple angle,bool IsInitial=false)
        {
            if (IsInitial){
                HObject cross;
                HOperatorSet.GenCrossContourXld(out cross, row, col, len, angle);
                HOperatorSet.ConcatObj(cross, AimShow, out AimShow);
                return;
            }
            if (this == null || Hwnd == null ) return;
            if (!PauseShow){
                try{
                    HObject cross;
                    HOperatorSet.GenCrossContourXld(out cross, row, col, len, angle);
                    viewController.addIconicVar(cross);
                    cross.Dispose();
                }
                catch(Exception ex){
                    Logger.PopError(ex.Message.ToString());
                }
            }
        }
        public void Repaint()
        {
            if (this == null || Hwnd == null) return;
            if (viewController == null) return;
            if (!PauseShow){
                lock (globalLock){
                    try{
                        if (CheckObjectIsExistedBeforeRepaint()){
                            viewController.repaint();
                        }
                    }
                    catch (Exception ex){
                        Logger.PopError(ex.Message.ToString());
                    }
                }
            }
        }

        public bool CheckObjectIsExistedBeforeRepaint()
        {
            foreach (var item in hObjectList){
                if (item == null || !item.IsInitialized()) return false;
            }
            return true;
        }

        public void SetString(double x, double y, string  color, string strVal)
        {
            if (this == null || Hwnd == null) return;
            if (!PauseShow){
                lock (globalLock){
                    try{
                        int xm, ym;
                        xm = Convert.ToInt32(x);
                        ym = Convert.ToInt32(y);
                        viewController.writeString(xm, ym, color, strVal);
                    }
                    catch (Exception ex){
                        Logger.PopError(ex.Message.ToString());
                    }
                }
            }
        }

        public void DrawRect1(out int row1, out int col1, out int row2, out int col2)
        {
            lock (globalLock){
                row1 = 0;
                col1 = 0;
                row2 = 100;
                col2 = 100;
                try{
                    viewController.DrawRect1(out row1, out col1, out row2, out col2);
                }
                catch (Exception ex){
                    Logger.PopError(ex.Message.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="model"> "margin、"  </param>
        public void SetDraw(string color, string model = "margin")
        {
            if (this == null || Hwnd == null) return;
            lock (globalLock) {
                try{
                    viewController.changeGraphicSettings(GraphicsContext.GC_COLOR, color);
                    viewController.changeGraphicSettings(GraphicsContext.GC_DRAWMODE, model);
                }
                catch (Exception ex){
                    Logger.PopError(ex.Message.ToString());
                }
            }
        }

        public void AddViewLine(HTuple row1, HTuple col1, HTuple row2, HTuple col2,bool IsInitial=false)
        {
            if (IsInitial){
                HObject line;
                HTuple rows=new HTuple(),cols=new HTuple();
                HOperatorSet.TupleConcat(rows,row1,out rows);
                HOperatorSet.TupleConcat(cols,col1,out cols);
                HOperatorSet.TupleConcat(rows,row2,out rows);
                HOperatorSet.TupleConcat(cols,col2,out cols);
                HOperatorSet.GenContourPolygonXld(out line, rows, cols);
                HOperatorSet.ConcatObj(line, AimShow, out AimShow);
                return;
            }
            if (this == null || Hwnd == null) return;
            if (!PauseShow){
                try {
                    HObject LineRegion;
                    HOperatorSet.GenRegionLine(out LineRegion, row1, col1, row2, col2);
                    viewController.addIconicVar(LineRegion);
                }
                catch
                { }
            }
        }

        public void SetROIShape( ROI r )
        {
            roiController.setROIShape(r);
        }

        public HMouseEventHandler HMouseClick ;

        public void HMouseDown(Object sender   ,HalconDotNet.HMouseEventArgs e)  {
           if(this.HMouseClick!=null)
            this.HMouseClick(sender, e); 
        }

    }
}
