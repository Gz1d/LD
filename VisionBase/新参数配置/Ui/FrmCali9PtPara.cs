using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using System.Threading;

namespace VisionBase
{
    public partial class FrmCali9PtPara : Form
    {
        public FrmCali9PtPara() {
            InitializeComponent();
        }
        ViewControl view1 = new ViewControl();
        public bool IsSavePara = false;
        private CaliParam TeachCaliPara = new CaliParam();
        HObject GrabedImg = new HObject();
        HObject CaliContour = new HObject();
        bool IsContinnueGrab = true;
        public FrmCali9PtPara(CaliParam CaliParaIn)
        {
            InitializeComponent();
            TeachCaliPara = CaliParaIn;
            MotionManager.Instance.SetCoordi(TeachCaliPara.coordi);
            view1 = DisplaySystem.GetViewControl("Cali");
            DisplaySystem.AddPanelForCCDView("Cali", panel1);
            this.LocalModelCbx.Items.Clear();
            foreach (LocalModelEnum temp in Enum.GetValues(typeof(LocalModelEnum))) {
                this.LocalModelCbx.Items.Add(temp);
            }
            LocalModelCbx.SelectedIndex = (int)TeachCaliPara.localPara.localSetting.localModel;
        }

        private void CamLightParaTeachBtn_Click(object sender, EventArgs e){
            FrmCamLightCtrl frmCamLightDlg = new FrmCamLightCtrl(TeachCaliPara.camLightPara);
            frmCamLightDlg.ShowDialog();
            TeachCaliPara.camLightPara = frmCamLightDlg.GetTeachPara();
        }

        private void LocalParaTeachBtn_Click(object sender, EventArgs e){
            TeachCaliPara.localPara.localSetting.localModel = (LocalModelEnum)LocalModelCbx.SelectedIndex;
            FrmLocalParaTeach FrmLocal = new FrmLocalParaTeach(TeachCaliPara.localPara);
            FrmLocal.ShowDialog();
            if (FrmLocal.IsSaveVisionPara) TeachCaliPara.localPara = FrmLocal.GetTeachLocalPara();
        }

        private void GrabImgBtn_Click(object sender, EventArgs e) {
            if (GrabedImg != null) GrabedImg.Dispose();
            CameraCtrl.Instance.GrabImg(TeachCaliPara.cam, out GrabedImg); //采集图像
            view1.Refresh();
            view1.AddViewImage(GrabedImg);
            view1.Repaint();
        }

        private void FrmCali9PtPara_Load(object sender, EventArgs e) {
            LocalModelCbx.SelectedIndex = (int)TeachCaliPara.localPara.localSetting.localModel;
        }

        private void ContinueGrabBtn_Click(object sender, EventArgs e){
            ContinueGrabBtn.Enabled = false;
            IsContinnueGrab = true;
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() => {
                while (IsContinnueGrab) {
                    if (GrabedImg != null) GrabedImg.Dispose();
                    if (CameraCtrl.Instance.GrabImg(TeachCaliPara.camLightPara.CamName, out GrabedImg)){
                        view1.ResetView();
                        view1.Refresh();
                        view1.AddViewImage(GrabedImg);
                        view1.Repaint();
                    }
                    System.Threading.Thread.Sleep(100);
                }
            }));
        }

        private void StopGrabBtn_Click(object sender, EventArgs e){
            IsContinnueGrab = false;
            ContinueGrabBtn.Enabled = true;
        }

        private void SaveImgBtn_Click(object sender, EventArgs e) {
            try {
                string fileName;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.Title = "保存图片文件";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                    fileName = saveFileDialog1.FileName;
                    HOperatorSet.WriteImage(GrabedImg, "bmp", 0, fileName);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        double X = 0, Y = 0, Z = 0, Theta = 0;

        private void EndCaliPtTeachBtn_Click(object sender, EventArgs e) {
            MotionManager.Instance.GetCoordiPos(out X, out Y, out Z, out Theta);
            EndCaliPtTeachTbx.Text = "X:" + X.ToString("f2") + "   Y:" + Y.ToString("f2") + "   Theta:" + Theta.ToString("f2");
            TeachCaliPara.EndCaliPt = new Point3Db(X, Y, Theta);
        }

        private void EndCaliPtSaveBtn_Click(object sender, EventArgs e) {
            EndCaliPtSaveBtn.Enabled = false;
        }

        private void MoveToCaliEndPtBtn_Click(object sender, EventArgs e) {
            MotionManager.Instance.SetCoordiPos(TeachCaliPara.EndCaliPt.x, TeachCaliPara.EndCaliPt.y, TeachCaliPara.EndCaliPt.angle);
        }
        MyHomMat2D NowHandEyeHomMat = new MyHomMat2D();
        private void CaliBtn_Click(object sender, EventArgs e) {
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() => {
                DialogResult dr = MessageBox.Show("是否开始旋转中心的九点标定？", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) return;
                try{
                    #region
                    if (TeachCaliPara.StartCaliPt.x == 0 && TeachCaliPara.StartCaliPt.y == 0 && TeachCaliPara.EndCaliPt.x == 0 &&
                    TeachCaliPara.EndCaliPt.y == 0){
                        MessageBox.Show("请先示教标定的起始和终止坐标");
                    }
                    List<Point2Db> ListPosP = new List<Point2Db>();
                    List<Point2Db> ListPosW = new List<Point2Db>();
                    Point2Db NowHandEyeCaliStartPt = new Point2Db();
                    Point2Db NowHandEyeCaliEndPt = new Point2Db();
                    NowHandEyeCaliStartPt.Col = TeachCaliPara.StartCaliPt.x;
                    NowHandEyeCaliStartPt.Row = TeachCaliPara.StartCaliPt.y;
                    NowHandEyeCaliEndPt.Col = TeachCaliPara.EndCaliPt.x;
                    NowHandEyeCaliEndPt.Row = TeachCaliPara.EndCaliPt.y;
                    //移动九个点拍照，获得手眼标定的9个机械坐标点，9个像素坐标点
                    bool isOK = GetHandEyeCaliPt(NowHandEyeCaliStartPt, NowHandEyeCaliEndPt, TeachCaliPara.IsMoveX, 
                        TeachCaliPara.IsMoveY, out ListPosP, out ListPosW);               //获取9点标定的坐标
                    if (isOK) {
                        HTuple PixelRows = new HTuple(), PixelCols = new HTuple(), MotionX = new HTuple(), MotionY = new HTuple();
                        MyVisionBase.Pt2dToHTuple(ListPosP, out PixelRows, out PixelCols);
                        MyVisionBase.Pt2dToHTuple(ListPosW, out MotionY, out MotionX);
                        //机械坐标9点减去标定中点的坐标
                        MyVisionBase.HtupleAddValue(MotionX, -0.5 * (NowHandEyeCaliStartPt.Col + NowHandEyeCaliEndPt.Col), out MotionX);
                        MyVisionBase.HtupleAddValue(MotionY, -0.5 * (NowHandEyeCaliStartPt.Row + NowHandEyeCaliEndPt.Row), out MotionY);
                        MyVisionBase.AdjImgRow(GrabedImg, ref PixelRows);//调整像素坐标
                        HTuple hv_HandEyeHomMat = new HTuple();
                        MyVisionBase.Calibra9(PixelCols, PixelRows, MotionX, MotionY, out hv_HandEyeHomMat);
                        MyVisionBase.HalconToMyHomMat(hv_HandEyeHomMat, out NowHandEyeHomMat);
                        TeachCaliPara.HomMat = NowHandEyeHomMat;
                    }
                    #endregion
                }
                catch (Exception e0) {
                    Logger.PopError(e0.Message, true);
                }
            }));
        }

        private void MoveToCaliStartPtBtn_Click(object sender, EventArgs e){
            MotionManager.Instance.SetCoordiPos(TeachCaliPara.StartCaliPt.x, TeachCaliPara.StartCaliPt.y, TeachCaliPara.StartCaliPt.angle);
        }

        private void StartCaliPtSaveBtn_Click(object sender, EventArgs e) {
            TeachCaliPara.StartCaliPt = new Point3Db(X, Y, Theta);
            StartCaliPtSaveBtn.Enabled = false;
        }

        private void StartCaliPtTeachBtn_Click(object sender, EventArgs e){
            MotionManager.Instance.GetCoordiPos(out X, out Y, out Z, out Theta);
            StartCaliPtTeachTbx.Text = "X:" + X.ToString("f2") + "   Y:" + Y.ToString("f2") + "   Theta:" + Theta.ToString("f2");
            StartCaliPtSaveBtn.Enabled = true;
            TeachCaliPara.StartCaliPt = new Point3Db(X, Y, Theta);
        }
        private void CaliParaSaveBtn_Click(object sender, EventArgs e) {
            DialogResult dr = MessageBox.Show("是否保存标定参数？", "提示", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;
            IsSavePara = true;
        }

        public double RotCenterRow = 0;
        private void MotionAdjBtn_Click(object sender, EventArgs e){
            FrmAxisMotion frm1 = new FrmAxisMotion();
            frm1.ShowDialog();
        }

        public double RotCenterCol = 0;

        /// <summary>
        /// 获取九点标定的九个世界坐标点，九个像素坐标点
        /// </summary>
        /// <param name="StartPt">示教起始点</param>
        /// <param name="EndPt">示教终点</param>
        /// <param name="IsCameraMovingWithAxisX"></param>
        /// <param name="IsCameraMovingWithAxisY"></param>
        /// <param name="PixelPosP"></param>
        /// <param name="MotionPosW"></param>
        /// <returns></returns>
        public bool GetHandEyeCaliPt(Point2Db StartPt, Point2Db EndPt, bool IsCameraMovingWithAxisX, bool IsCameraMovingWithAxisY,
            out List<Point2Db> PixelPosP, out List<Point2Db> MotionPosW) {
            int StepCount = 3;
            PixelPosP = new List<Point2Db>();
            MotionPosW = new List<Point2Db>();
            Point2Db startPt = new Point2Db(Math.Min(StartPt.Col, EndPt.Col), Math.Min(StartPt.Row, EndPt.Row));
            Point2Db endPt = new Point2Db(Math.Max(StartPt.Col, EndPt.Col), Math.Max(StartPt.Row, EndPt.Row));
            double recWidth = endPt.Col - startPt.Col;
            double recHeight = endPt.Row - startPt.Row;
            double stepX = recWidth / (StepCount - 1);
            double stepY = recHeight / (StepCount - 1);
            List<double> supposeX = new List<double>(), supposeY = new List<double>();
            List<double> currentX = new List<double>(), currentY = new List<double>();
            LocalManager MyLocal = new LocalManager(); //定位类初始化
            LocalResult MyResult = new LocalResult();
            MyLocal.SetLocalModel(TeachCaliPara.localPara.localSetting.localModel);
            HOperatorSet.GenEmptyObj(out CaliContour);
            for (int row = 0; row <= StepCount - 1; row += 1){
                for (int col = 0; col <= StepCount - 1; col += 1){
                    Point2Db movePt = new Point2Db();  //九点标定时，相机移动到一个点，然后开始拍照
                    Point2Db CaliMovePt = new Point2Db();//九点标定时的点，坐下角为第一个点，右上角为第九个点
                    if (IsCameraMovingWithAxisX && IsCameraMovingWithAxisY)             //相机跟随XY周移动
                        movePt = new Point2Db(startPt.Col + (StepCount - 1 - col) * stepX, startPt.Row + (StepCount - 1 - row) * stepY);
                    else if ((IsCameraMovingWithAxisX) && (!IsCameraMovingWithAxisY))    //相机跟随X轴移动
                        movePt = new Point2Db(startPt.Col + (StepCount - 1 - col) * stepX, startPt.Row + row * stepY);
                    else if ((!IsCameraMovingWithAxisX) && IsCameraMovingWithAxisY)      //相机跟随Y轴移动
                        movePt = new Point2Db(startPt.Col + col * stepX, startPt.Row + (StepCount - 1 - row) * stepY);
                    else if ((!IsCameraMovingWithAxisX) && (!IsCameraMovingWithAxisY))   //相机固定
                        movePt = new Point2Db(startPt.Col + col * stepX, startPt.Row + row * stepY);
                    CaliMovePt = new Point2Db(startPt.Col + col * stepX, startPt.Row + row * stepY);
                    // 控制平台移动到目标位置
                    if (!MotionManager.Instance.SetCoordiPos(movePt.Col, movePt.Row, TeachCaliPara.EndCaliPt.angle)){
                        MessageBox.Show("X Y轴运动失败!");
                        return false;
                    }
                    Thread.Sleep(2000);
                    GrabedImg = new HObject();
                    CameraCtrl.Instance.GrabImg(TeachCaliPara.cam, out GrabedImg);  //相机拍照抓取图片
                    Point2Db Pt = new Point2Db();
                    if (GrabedImg.IsInitialized()){
                        MyLocal.SetParam(GrabedImg, TeachCaliPara.localPara);//设置定位参数
                        MyLocal.doLocal(); //执行定位算法
                        MyResult = MyLocal.GetResult();  //获取定位结果
                        view1.ResetView();
                        CaliContour = CaliContour.ConcatObj(MyResult.ShowContour);
                        view1.AddViewImage(GrabedImg);
                        view1.AddViewObject(MyResult.ShowContour);
                        //GetCaliMarkPts1(GrabedImg, MinGray, MaxGray, MarkR, out ContourU, out ListMarkCenterI);  //找出Mark点
                        HObject CenterContour = new HObject();
                        HOperatorSet.GenCrossContourXld(out CenterContour, MyResult.row, MyResult.col, 50, 0);  //生成找到的圆心轮廓
                        HOperatorSet.ConcatObj(CaliContour, CenterContour, out CaliContour);
                        view1.AddViewObject(CaliContour);
                        view1.Repaint();
                        Pt.Col = MyResult.col;
                        Pt.Row = MyResult.row;
                        PixelPosP.Add(Pt);
                        MotionPosW.Add(CaliMovePt);
                    }
                    else {
                        MessageBox.Show("采集图片失败");
                        return false;
                    }
                }
            }
            return true;
        }

        public CaliParam GetTeachedCaliPara() {
            return TeachCaliPara;
        }
    }
}
