using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace VisionBase
{
    public partial class FrmUpDnCamCali : Form
    {

        public bool IsSavePara = false;
        private CaliParam TeachCaliPara;  //当前示教的标定参数
        private ViewControl view1 = new ViewControl();
        HObject GrabedImg = new HObject();
        public FrmUpDnCamCali(CaliParam CaliParaIn)
        {
            InitializeComponent();
            TeachCaliPara = CaliParaIn;
            MotionManager.Instance.SetCoordi(TeachCaliPara.coordi);
            view1 = DisplaySystem.GetViewControl("Cali");
            DisplaySystem.AddPanelForCCDView("Cali", panel1);
            this.LocalModelCbx.Items.Clear();
            foreach (LocalModelEnum temp in Enum.GetValues(typeof(LocalModelEnum))){
                this.LocalModelCbx.Items.Add(temp);
            }
            LocalModelCbx.SelectedIndex = (int)TeachCaliPara.localPara.localSetting.localModel;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
            if (CaliPtModelCbx.SelectedIndex == 0) {
                TeachCaliPara.ListPt2D = new List<Point2Db>(4);
            }
        }

        private void LocalParaTeachBtn_Click(object sender, EventArgs e) {
            TeachCaliPara.localPara.localSetting.localModel = (LocalModelEnum)LocalModelCbx.SelectedIndex;
            FrmLocalParaTeach FrmLocal = new FrmLocalParaTeach(TeachCaliPara.localPara);
            FrmLocal.ShowDialog();
            if (FrmLocal.IsSaveVisionPara) TeachCaliPara.localPara = FrmLocal.GetTeachLocalPara();
        }

        private void CamLightParaTeachBtn_Click(object sender, EventArgs e) {
            FrmCamLightCtrl frmCamLightDlg = new FrmCamLightCtrl(TeachCaliPara.camLightPara);
            frmCamLightDlg.ShowDialog();
            TeachCaliPara.camLightPara = frmCamLightDlg.GetTeachPara();
        }

        private void FrmUpDnCamCali_Load(object sender, EventArgs e) {
            view1 = DisplaySystem.GetViewControl("Cali");
            LocalModelCbx.SelectedIndex = (int)TeachCaliPara.localPara.localSetting.localModel;
        }

        private void RectTeachBtn_Click(object sender, EventArgs e){
            int SelectIndex = CaliPtModelCbx.SelectedIndex;
            if (GrabedImg == null||!GrabedImg.IsInitialized()){
                MessageBox.Show("标定前，请先采集图片");
                return;
            }
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() =>{
                try{
                    int row1 = 0, col1 = 0, row2, col2;
                    if (SelectIndex == 0){
                        TeachCaliPara.ListRectRegion = new List<RectangleF>();
                        for (int i = 0; i < 4; i++) {
                            view1.SetString(100, 100, "red", "左击拖动绘制矩形，右击释放");
                            view1.SetString(100, 150, "red", "绘制第" + i.ToString() + "矩形");
                            view1.DrawRect1(out row1, out col1, out row2, out col2);
                            HObject Rect, RectContour;
                            HOperatorSet.GenRectangle1(out Rect, row1, col1, row2, col2);
                            HOperatorSet.GenContourRegionXld(Rect, out RectContour, "border");
                            view1.Refresh();
                            view1.AddViewImage(GrabedImg);
                            view1.AddViewObject(RectContour);
                            view1.Repaint();
                            RectangleF rect = new RectangleF(col1, row1, col2 - col1, row2 - row1);
                            TeachCaliPara.ListRectRegion.Add(rect);
                        }
                    }
                }
                catch
                { }
            }));
        }

        private void GrabImgBtn_Click(object sender, EventArgs e) {
            CameraCtrl.Instance.GrabImg(TeachCaliPara.camLightPara.CamName, out GrabedImg); //采集图像
            view1.Refresh();
            view1.AddViewImage(GrabedImg);
            view1.Repaint();
        }

        Point2Db TeachPt = new Point2Db();  //Mark板上Mark点的实际坐标
        private void PositionTeachBtn_Click(object sender, EventArgs e){
            PositionSaveBtn.Enabled = true;
            decimal TeachX = NumUpDnX.Value;
            decimal TeachY = NumUpDnY.Value;
            TeachPt.Row = (double)TeachY;
            TeachPt.Col = (double)TeachX;
        }

        private void PositionSaveBtn_Click(object sender, EventArgs e){
            if (CaliPtSelectCbx.SelectedIndex >= TeachCaliPara.ListPt2D.Count){
                for (int i = 0; i < CaliPtSelectCbx.SelectedIndex - TeachCaliPara.ListPt2D.Count + 1; i++)
                {
                    Point2Db pt = new Point2Db();
                    TeachCaliPara.ListPt2D.Add(pt);
                }
                TeachCaliPara.ListPt2D[CaliPtSelectCbx.SelectedIndex] = TeachPt;
            }
            else {
                TeachCaliPara.ListPt2D[CaliPtSelectCbx.SelectedIndex] = TeachPt;
            }
            PositionSaveBtn.Enabled = false;
            PositionTeachBtn.Enabled = true;
        }
        private void MotionAdjBtn_Click(object sender, EventArgs e) {
            FrmAxisMotion frm1 = new FrmAxisMotion();
            frm1.ShowDialog();
        }

        private void CaliParaSaveBtn_Click(object sender, EventArgs e){
            IsSavePara = true;
            DialogResult DlgReslult = MessageBox.Show("是否保存参数，关闭窗口", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DlgReslult == DialogResult.No) return;
            this.Close();
        }

        private void StartCaliBtn_Click(object sender, EventArgs e) {
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() => {
                try {
                    #region
                    List<Point2Db> PixelPtList = new List<Point2Db>();//像素点的坐标集合
                    LocalPara NowLocalPara = TeachCaliPara.localPara;
                    RectangleF RectFi = new RectangleF();
                    LocalManager MyLocal = new LocalManager(); //定位类初始化
                    LocalResult MyResult = new LocalResult();
                    Point2Db Pt = new Point2Db();
                    HObject CaliContour = new HObject();
                    HOperatorSet.GenEmptyObj(out CaliContour);
                    if (GrabedImg == null){
                        MessageBox.Show(" 请先加载一张图片");
                        return;
                    }
                    for (int i = 0; i < TeachCaliPara.ListRectRegion.Count; i++){
                        RectFi = TeachCaliPara.ListRectRegion[i];
                        TeachCaliPara.localPara.localSetting.SearchAreaX = (int)RectFi.X;
                        TeachCaliPara.localPara.localSetting.SearchAreaY = (int)RectFi.Y;
                        TeachCaliPara.localPara.localSetting.SearchWidth = (int)RectFi.Width;
                        TeachCaliPara.localPara.localSetting.SearchHeight = (int)RectFi.Height;
                        MyLocal.SetLocalModel(TeachCaliPara.localPara.localSetting.localModel);
                        MyLocal.SetParam(GrabedImg, TeachCaliPara.localPara);
                        MyLocal.doLocal();
                        MyResult = MyLocal.GetResult();
                        CaliContour = CaliContour.ConcatObj(MyResult.ShowContour);
                        //GetCaliMarkPts1(GrabedImg, MinGray, MaxGray, MarkR, out ContourU, out ListMarkCenterI);  //找出Mark点
                        HObject CenterContour = new HObject();
                        HOperatorSet.GenCrossContourXld(out CenterContour, MyResult.row, MyResult.col, 50, 0);  //生成找到的圆心轮廓
                        HOperatorSet.ConcatObj(CaliContour, CenterContour, out CaliContour);
                        Pt.Col = MyResult.col;
                        MyVisionBase.AdjImgRow(GrabedImg, ref MyResult.row);
                        Pt.Row = MyResult.row;
                        PixelPtList.Add(Pt);
                        view1.Refresh();
                        view1.AddViewImage(GrabedImg);
                        view1.AddViewObject(CaliContour);
                        view1.Repaint();                                              
                    }
                    #endregion
                    bool IsTrue = true;
                    MyVisionBase.VectorToHomMat(PixelPtList, TeachCaliPara.ListPt2D, out TeachCaliPara.HomMat, out IsTrue);
                }
                catch
                { }
            }));
        }
        bool IsContinnueGrab = true;
        private void ContinueGrabBtn_Click(object sender, EventArgs e)  {
            ContinueGrabBtn.Enabled = false;
            IsContinnueGrab = true;
            int i = 0;
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() => {
                while (IsContinnueGrab)
                {
                    if (GrabedImg != null) GrabedImg.Dispose();
                    if (CameraCtrl.Instance.GrabImg(TeachCaliPara.camLightPara.CamName, out GrabedImg)){
                        view1.Refresh();
                        view1.AddViewImage(GrabedImg);
                        view1.Repaint();
                    }
                    i++;
                    System.Threading.Thread.Sleep(100);
                    if (i > 200){
                        IsContinnueGrab = false;
                    }
                }
            }));
        }

        public CaliParam GetTeachedCaliPara() {
            return TeachCaliPara;
        }

        private void StopGrabBtn_Click(object sender, EventArgs e) {
            IsContinnueGrab = false;
            ContinueGrabBtn.Enabled = true;
        }

        private void FrmUpDnCamCali_FormClosing(object sender, FormClosingEventArgs e) {
            IsContinnueGrab = false;
        }

        private void LoadImgBtn_Click(object sender, EventArgs e)
        {
            try{
                string fileName;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "All files (*.*)|*.*|bmp files (*.bmp)|*.bmp";
                openFileDialog1.Multiselect = false;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Title = "打开图片文件";
                openFileDialog1.RestoreDirectory = false;
                openFileDialog1.InitialDirectory = @"D:\Image\";
                if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                    fileName = openFileDialog1.FileName;
                    HOperatorSet.ReadImage(out GrabedImg, fileName);
                    view1.ResetView();
                    view1.AddViewImage(GrabedImg);
                    view1.Repaint();
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
