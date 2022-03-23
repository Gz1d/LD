using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using HalconDotNet;

namespace VisionBase
{
    public class DisplaySystem
    {
        public static bool IsShowRecheckImage = false;
        private static Dictionary<string, FrmView> CCDImagePaneDict = new Dictionary<string, FrmView>();
        private static Dictionary<string, FrmView> FirstImagePaneDict = new Dictionary<string, FrmView>();
        private static Dictionary<string, object> ShowPanelDictLock = new Dictionary<string, object>();
        private static Dictionary<string, ViewControl> ViewControlDict = new Dictionary<string, ViewControl>();
        private static Dictionary<string, Panel> PanelNameDict = new Dictionary<string, Panel>();
        private static ViewControl[,] RecheckViewControlArr = null; 
        private const int CCDCount = 7;
        private static int ImageWidth=2448, ImageHeight=2048;
        private static int RecheckCCDCount=5, RecheckImageCount = 4;        
        public static void InitCCDPanel()
        {
            CreateView("Ipad左上角拍照位1");
            CreateView("Ipad右下角拍照位1");
            CreateView("Ipad左上角拍照位2");
            CreateView("Ipad右下角拍照位2");

            CreateView("左侧下相机左上角拍照位");
            CreateView("左侧下相机右下角拍照位");
            CreateView("右侧下相机左上角拍照位");
            CreateView("右侧下相机右下角拍照位");
            CreateView("上相机左上角拍照位1");
            CreateView("上相机右下角拍照位1");
            CreateView("上相机左上角拍照位2");
            CreateView("上相机右下角拍照位2");
            CreateView("FocusPanel");
            CreateView("CameraManager");
            CreateView("Cali");
            CreateView("Test");
            CreateView("Local");
        }

        public static void  CreateView(string viewName)
        {
            CCDImagePaneDict.Add(viewName, null);
            FirstImagePaneDict.Add(viewName, null);
            ShowPanelDictLock.Add(viewName, new List<object>());
            ViewControlDict.Add(viewName, new ViewControl());
            PanelNameDict.Add(viewName, null);
        }


        public static ViewControl GetViewControl(string ccdname)
        {
            if (!ViewControlDict.ContainsKey(ccdname)) return new ViewControl();
            if (PanelNameDict[ccdname] != null && !PanelNameDict[ccdname].IsDisposed) {
                Logger.Pop(string.Format("当前相机{0}，显示Panel为{1}，名字：{2}", ccdname, 
                    PanelNameDict[ccdname].AccessibleName, PanelNameDict[ccdname].Name));
            }
            return ViewControlDict[ccdname];
        }

        //新增复检处理
        /// <summary>
        /// 获得复检图片显示控件
        /// </summary>
        /// <param name="upCCDIndex"></param>
        /// <param name="imgIndex"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public static bool GetRecheckViewControl(int upCCDIndex,int imgIndex,out ViewControl view)
        {
            view =new ViewControl();
            if (!IsShowRecheckImage) return false;
            if (upCCDIndex >= RecheckCCDCount || imgIndex >= RecheckImageCount) return false;
            if (upCCDIndex < 0 || imgIndex < 0) return false;
            if (RecheckViewControlArr[upCCDIndex, imgIndex] == null)  return false;
            view = RecheckViewControlArr[upCCDIndex, imgIndex];
            return true;
        }

        public static void SwitchPanelForCCDView(string ccdName, Panel panel)
        {
            if (!CCDImagePaneDict.ContainsKey(ccdName)) return;
            if (CCDImagePaneDict[ccdName] == null) return;
            lock (ShowPanelDictLock[ccdName]){
                FrmView form = CCDImagePaneDict[ccdName];
                panel.Controls.Clear();
                panel.Controls.Add(form);
                form.Show();
                form.BringToFront();
                ViewControlDict[ccdName].IntialView(CCDImagePaneDict[ccdName].hWindowControl1, ImageWidth, ImageHeight);
            }
        }

        public static void AddPanelForCCDView(string ccdName,Panel panel)
        {
            if (!CCDImagePaneDict.ContainsKey(ccdName)) return;
            FrmView form = new FrmView();
            form.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, ImageWidth, ImageHeight);
            form.hWindowControl1.Location = new System.Drawing.Point(0, 0);
            form.Location = new System.Drawing.Point(0, 0);
            form.Size = panel.Size;
            form.hWindowControl1.Size = form.Size;
            form.TopLevel = false;
            panel.Controls.Clear();
            panel.Controls.Add(form);
            form.Show();
            form.BringToFront();
            lock (ShowPanelDictLock[ccdName]) {
                if (CCDImagePaneDict[ccdName]==null){
                    CCDImagePaneDict[ccdName] = form;
                    FirstImagePaneDict[ccdName] = form;
                }
                else {
                    CCDImagePaneDict[ccdName] = form;
                }
                ViewControlDict[ccdName].IntialView(form.hWindowControl1, ImageWidth, ImageHeight);
            }
            ParentForm(panel).Disposed += (bSender, bE) =>{
                if (form != null) form.Dispose();
                CCDImagePaneDict[ccdName] = FirstImagePaneDict[ccdName];
                if (!CCDImagePaneDict[ccdName].IsDisposed) {
                    ViewControlDict[ccdName].IntialView(CCDImagePaneDict[ccdName].hWindowControl1, ImageWidth, ImageHeight);
                }
            };
            ParentForm(panel).FormClosing  +=  (bSender, bE) =>{
                CCDImagePaneDict[ccdName] = FirstImagePaneDict[ccdName];
                if (!CCDImagePaneDict[ccdName].IsDisposed) {
                    ViewControlDict[ccdName].IntialView(CCDImagePaneDict[ccdName].hWindowControl1, ImageWidth, ImageHeight);
                }
            };
            if (PanelNameDict.ContainsKey(ccdName)) {
                PanelNameDict[ccdName] = panel;
            }
        }

        //新增复检处理
        public static void SetPanelForRecheckView(int upCCDIndex, int imgIndex, Panel panel)
        {
            FrmView form = new FrmView();
            form.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, ImageWidth, ImageHeight);
            form.hWindowControl1.Location = new System.Drawing.Point(0, 0);
            form.Location = new System.Drawing.Point(0, 0);
            form.Size = panel.Size;
            form.hWindowControl1.Size = form.Size;
            form.TopLevel = false;
            panel.Controls.Clear();
            panel.Controls.Add(form);
            form.Show();
            form.BringToFront();
            ViewControl viewCtl;
            if (!GetRecheckViewControl(upCCDIndex, imgIndex, out viewCtl)) return;
            viewCtl.IntialView(form.hWindowControl1, ImageWidth, ImageHeight);
            ParentForm(panel).Disposed += (bSender, bE) => { if (form != null) form.Dispose(); };
            ParentForm(panel).FormClosing += (bSender, bE) => { if (form != null) form.Dispose(); };
        }

        private static Form ParentForm(Control ctrl)
        {
            Control bForm = null;
            Control iCtrl = null;
            iCtrl = ctrl;
            while (true)
            {
                bForm = iCtrl.Parent as Form;
                if (bForm != null) break;
                iCtrl = iCtrl.Parent;
            }
            return bForm as Form;
        }
    }
}
