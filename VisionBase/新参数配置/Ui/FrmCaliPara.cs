using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.Threading;

namespace VisionBase
{
    public partial class FrmCaliPara : Form
    {
        ViewControl view1 = new ViewControl();
        public bool IsSavePara = false;
        public FrmCaliPara( CaliParam CaliParaIn) {
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
            FindCirCenterCbx.SelectedIndex = 0;
        }
        private CaliParam TeachCaliPara = new CaliParam();
        private void CamLightParaTeachBtn_Click(object sender, EventArgs e) {
            FrmCamLightCtrl frmCamLightDlg = new FrmCamLightCtrl(TeachCaliPara.camLightPara);
            frmCamLightDlg.ShowDialog();
            TeachCaliPara.camLightPara = frmCamLightDlg.GetTeachPara();
        }

        private void LocalParaTeachBtn_Click(object sender, EventArgs e)
        {
            TeachCaliPara.localPara.localSetting.localModel = (LocalModelEnum)LocalModelCbx.SelectedIndex;
            FrmLocalParaTeach FrmLocal = new FrmLocalParaTeach(TeachCaliPara.localPara);
            FrmLocal.ShowDialog();
            if (FrmLocal.IsSaveVisionPara) TeachCaliPara.localPara = FrmLocal.GetTeachLocalPara();
        }
        double X = 0, Y = 0, Z = 0, Theta = 0;
        private void RotPosGetBtn_Click(object sender, EventArgs e) {
            MotionManager.Instance.GetCoordiPos(out X, out Y, out Z, out Theta);
            RotPosTbx.Text = "X:" + X.ToString("f2") +"   Y:"+ Y.ToString("f2") + "   Theta:"+Theta.ToString("f2");
            TeachCaliPara.StartRotPt = new Point3Db(X, Y, Theta);
        }

        private void UpDataToFrm() {
            RotPosTbx.Text = "X:" + TeachCaliPara.StartRotPt.x.ToString("f2") + "   Y:" + 
                TeachCaliPara.StartRotPt.y.ToString("f2") + "   Theta:" + TeachCaliPara.StartRotPt.angle.ToString("f2");
            StartCaliPtTeachTbx.Text = "X:" + TeachCaliPara.StartCaliPt.x.ToString("f2") +
                "   Y:" + TeachCaliPara.StartCaliPt.y.ToString("f2") + "   Theta:" + TeachCaliPara.StartCaliPt.angle.ToString("f2");
            EndCaliPtTeachTbx.Text = "X:" + TeachCaliPara.EndCaliPt.x.ToString("f2") + 
                "   Y:" + TeachCaliPara.EndCaliPt.y.ToString("f2") + "   Theta:" + TeachCaliPara.EndCaliPt.angle.ToString("f2");
            AngleRangeNumUpDn.Value = (decimal)TeachCaliPara.AngleRange;
            RotCountNumUpDn.Value = (decimal)TeachCaliPara.AngleStep;
        }
        private void timer1_Tick(object sender, EventArgs e) {
            UpDataToFrm();
        }
        private void MoveToRotPosBtn_Click(object sender, EventArgs e) {
            MotionManager.Instance.SetCoordiPos(TeachCaliPara.StartRotPt.x, TeachCaliPara.StartRotPt.y, TeachCaliPara.StartRotPt.angle);
        }
        private void AngleRangeNumUpDn_ValueChanged(object sender, EventArgs e) {
            TeachCaliPara.AngleRange = (double)AngleRangeNumUpDn.Value;
        }
        private void RotCountNumUpDn_ValueChanged(object sender, EventArgs e) {
            TeachCaliPara.AngleStep = (int)RotCountNumUpDn.Value;
        }
     
        public Point2Db VectorCaliMarkToRot = new Point2Db();
        /// <summary> 计算旋转中心的定位点集合 </summary>
        public List<Point2Db> ListMarkCenterU = new List<Point2Db>();
        public HObject FindCenterCont = new HObject();
        public double RotR = 0;
        public double RotCenterRow = 0;
        public double RotCenterCol = 0;

        private void CaliRotCenterBtn_Click(object sender, EventArgs e) {
            DialogResult dr = MessageBox.Show("是否示开始旋转中心标定？", "提示", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() => {
                ListMarkCenterU = new List<Point2Db>();
                HObject GrabedImg = new HObject();
                LocalManager MyLocal = new LocalManager(); //定位类初始化
                LocalResult MyResult = new LocalResult();
                MyLocal.SetLocalModel(TeachCaliPara.localPara.localSetting.localModel);
                HOperatorSet.GenEmptyObj(out FindCenterCont);
                //旋转一个角度拍一次照片，用定位的点拟合圆，计算出0度时标定点到旋转中心的偏移量
                for (int i = 0; i < TeachCaliPara.AngleStep; i++)  {
                    if (MotionManager.Instance.SetCoordiPos(TeachCaliPara.StartRotPt.x, TeachCaliPara.StartRotPt.y, TeachCaliPara.StartRotPt.angle
                        + TeachCaliPara.AngleRange / TeachCaliPara.AngleStep * i)){
                        if (GrabedImg != null) GrabedImg.Dispose();
                        CameraCtrl.Instance.GrabImg(TeachCaliPara.cam, out GrabedImg); //采集图像
                        MyResult = new LocalResult();
                        MyLocal.SetParam(GrabedImg, TeachCaliPara.localPara);
                        MyLocal.doLocal();
                        MyResult = MyLocal.GetResult();
                        if (!MyResult.IsLocalOk) {
                            MessageBox.Show("定位失败");
                            return;
                        }
                        view1.ResetView();
                        view1.Refresh();
                        view1.AddViewImage(GrabedImg);
                        view1.AddViewObject(MyResult.ShowContour);
                        view1.Repaint();
                        //view1.Refresh();
                        //this.Refresh();
                        Thread.Sleep(50);
                        Point2Db NowPt = new Point2Db();
                        NowPt.Col = MyResult.col;
                        NowPt.Row = MyResult.row;
                        ListMarkCenterU.Add(NowPt);
                        FindCenterCont = FindCenterCont.ConcatObj(MyResult.ShowContour);
                    }
                    else{
                        MessageBox.Show("移动失败");
                        return;
                    }
                }
                #region 零度时候的定位坐标
                Point2Db ZeoroDegreePos = new Point2Db();
                if (MotionManager.Instance.SetCoordiPos(TeachCaliPara.StartRotPt.x, TeachCaliPara.StartRotPt.y, 0.0)) {
                    if (GrabedImg != null) GrabedImg.Dispose();
                    CameraCtrl.Instance.GrabImg(TeachCaliPara.cam, out GrabedImg); //采集图像
                    MyResult = new LocalResult();
                    MyLocal.SetParam(GrabedImg, TeachCaliPara.localPara);
                    MyLocal.doLocal();
                    MyResult = MyLocal.GetResult();
                    if (!MyResult.IsLocalOk){
                        MessageBox.Show("定位失败");
                        return;
                    }
                    ZeoroDegreePos = new Point2Db(MyResult.col ,MyResult.row);
                }
                #endregion
                //通过找到的标定mark中心点集合，拟合出旋转中心
                HTuple CircleRows = new HTuple();
                HTuple CircleCols = new HTuple();
                MyVisionBase.ListPt2dToHTuple(ListMarkCenterU, out CircleRows, out CircleCols);
                HObject CircleContourU = new HObject();
                HTuple CenterRow = new HTuple(), CenterCol = new HTuple();
                HTuple RotRadius = new HTuple(), StartPhi = new HTuple(), EndPhi = new HTuple(), FitFlag = new HTuple();
                if (FindCirCenterCbx.SelectedValue.ToString() == "拟合圆") {
                    MyVisionBase.PtsToBestCircle1(out CircleContourU, CircleRows, CircleCols, 4, "arc", out CenterRow, out CenterCol,
                        out RotRadius, out StartPhi, out EndPhi, out FitFlag);//通过边界点拟合圆
                    RotR = RotRadius.D;
                    RotCenterRow = CenterRow.D;
                    RotCenterCol = CenterCol.D;
                }
                else if (FindCirCenterCbx.SelectedValue.ToString() == "中垂线找圆心") {
                    FindCircleCenter(ListMarkCenterU, out RotCenterRow, out RotCenterCol, out RotR);
                    HOperatorSet.GenCircleContourXld(out CircleContourU, RotCenterRow, RotCenterCol,RotR, 0, Math.PI, "positive", 1);
                }
                HObject PtContour = new HObject();
                HOperatorSet.GenCrossContourXld(out PtContour, CircleRows, CircleCols, 20, 0.6);
                HOperatorSet.ConcatObj(PtContour, CircleContourU, out CircleContourU);
                // MyVisionBase.hDispObj(hWindowControl1.HalconWindow, CircleContourU);
                view1.AddViewObject(CircleContourU);
                view1.Repaint();
                MyVisionBase.HTupleToListPt2d(CircleRows, CircleCols, out ListMarkCenterU);
                //计算0度标定Mark中心到旋转中心的偏移量
                VectorCaliMarkToRot.Row = (CenterRow - ZeoroDegreePos.Row);
                VectorCaliMarkToRot.Col = (CenterCol - ZeoroDegreePos.Col);
                HObject MarkToRotArrow = new HObject();
                MyVisionBase.GenArrowContourXld(out MarkToRotArrow, ZeoroDegreePos.Row, ZeoroDegreePos.Col , CenterRow, CenterCol, 30, 30);
                view1.AddViewObject(MarkToRotArrow);
                view1.Repaint();
            }));           
        }

        private void StartCaliPtTeachBtn_Click(object sender, EventArgs e) {
            MotionManager.Instance.GetCoordiPos(out X, out Y, out Z, out Theta);
            StartCaliPtTeachTbx.Text = "X:" + X.ToString("f2") + "   Y:" + Y.ToString("f2") + "   Theta:" + Theta.ToString("f2");
            StartCaliPtSaveBtn.Enabled = true;
            TeachCaliPara.StartCaliPt = new Point3Db(X, Y, Theta);
        }

        private void EndCaliPtTeachBtn_Click(object sender, EventArgs e) {
            MotionManager.Instance.GetCoordiPos(out X, out Y, out Z, out Theta);
            EndCaliPtTeachTbx.Text = "X:" + X.ToString("f2") + "   Y:" + Y.ToString("f2") + "   Theta:" + Theta.ToString("f2");
            TeachCaliPara.EndCaliPt = new Point3Db(X, Y, Theta);
        }
        private void StartCaliPtSaveBtn_Click(object sender, EventArgs e){
            TeachCaliPara.StartCaliPt = new Point3Db(X, Y, Theta);
            StartCaliPtSaveBtn.Enabled = false;
        }
        private void EndCaliPtSaveBtn_Click(object sender, EventArgs e){
            EndCaliPtSaveBtn.Enabled = false;
        }
        private void MoveToCaliStartPtBtn_Click(object sender, EventArgs e){
            MotionManager.Instance.SetCoordiPos(TeachCaliPara.StartCaliPt.x, TeachCaliPara.StartCaliPt.y, TeachCaliPara.StartCaliPt.angle);
        }
        private void MoveToCaliEndPtBtn_Click(object sender, EventArgs e) {
            MotionManager.Instance.SetCoordiPos(TeachCaliPara.EndCaliPt.x, TeachCaliPara.EndCaliPt.y, TeachCaliPara.EndCaliPt.angle);
        }

        private void StopRotPosTeachBtn_Click(object sender, EventArgs e) {
            RotPosGetBtn.Enabled = false;
            AngleRangeNumUpDn.Enabled = false;
            RotCountNumUpDn.Enabled = false;
        }

        private void RotTeachBtn_Click(object sender, EventArgs e) {
            RotPosGetBtn.Enabled = true;
        }

        private void MotionAdjBtn_Click(object sender, EventArgs e) {
            FrmAxisMotion frm1 = new FrmAxisMotion();
            frm1.ShowDialog();
        }
        MyHomMat2D NowHandEyeHomMat = new MyHomMat2D();
        //开始9点标定
        private void CaliBtn_Click(object sender, EventArgs e){
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() =>{
                DialogResult dr = MessageBox.Show("是否开始旋转中心的九点标定？", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) return;
                try  {
                    #region
                    if (VectorCaliMarkToRot.Row == 0 && VectorCaliMarkToRot.Col == 0){
                        MessageBox.Show("请先完成旋转中心标定");
                        return;
                    }
                    if (TeachCaliPara.StartCaliPt.x == 0 && TeachCaliPara.StartCaliPt.y == 0 &&
                    TeachCaliPara.EndCaliPt.x == 0 && TeachCaliPara.EndCaliPt.y == 0) {
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
                    bool isOK =false ;
                    if (CaliModelCbx.SelectedIndex == 0){
                        isOK = GetHandEyeCaliPt(NowHandEyeCaliStartPt, NowHandEyeCaliEndPt, TeachCaliPara.IsMoveX, 
                            TeachCaliPara.IsMoveY, out ListPosP, out ListPosW);
                    }                       
                    else if (CaliModelCbx.SelectedIndex == 1){
                        isOK = GetHandEyeCaliPt2(NowHandEyeCaliStartPt, NowHandEyeCaliEndPt, TeachCaliPara.IsMoveX, 
                            TeachCaliPara.IsMoveY,3,1, out ListPosP, out ListPosW);                  
                    }
                    //获取9点标定的坐标
                    if (isOK){
                        HTuple PixelRows = new HTuple(), PixelCols = new HTuple(), MotionX = new HTuple(), MotionY = new HTuple();
                        MyVisionBase.Pt2dToHTuple(ListPosP, out PixelRows, out PixelCols);
                        ///新增8-22  //调整
                        MyVisionBase.HtupleAddValue(PixelRows, VectorCaliMarkToRot.Row, out PixelRows); //将Mark中心对应坐标，变换到旋转中心对应的坐标
                        MyVisionBase.HtupleAddValue(PixelCols, VectorCaliMarkToRot.Col, out PixelCols);
                        MyVisionBase.Pt2dToHTuple(ListPosW, out MotionY, out MotionX);
                        //机械坐标9点减去标定中点的坐标
                        MyVisionBase.HtupleAddValue(MotionX, -0.5 * (NowHandEyeCaliStartPt.Col + NowHandEyeCaliEndPt.Col), out MotionX);
                        MyVisionBase.HtupleAddValue(MotionY, -0.5 * (NowHandEyeCaliStartPt.Row + NowHandEyeCaliEndPt.Row), out MotionY);
                        MyVisionBase.AdjImgRow(GrabedImg, ref PixelRows);
                        HTuple hv_HandEyeHomMat = new HTuple();
                        if (CaliModelCbx.SelectedIndex == 0)
                            MyVisionBase.Calibra9(PixelCols, PixelRows, MotionX, MotionY, out hv_HandEyeHomMat);
                        else if (CaliModelCbx.SelectedIndex == 1)
                            MyVisionBase.Calibra9PtSimilar(PixelCols, PixelRows, MotionX, MotionY, out hv_HandEyeHomMat);
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


        public CaliParam GetTeachedCaliPara()
        {
            return TeachCaliPara;       
        }


        private void CaliParaSaveBtn_Click(object sender, EventArgs e) {
            IsSavePara = true;
        }
        bool IsContinnueGrab = true;
        private void ContinueGrabBtn_Click(object sender, EventArgs e) {
            ContinueGrabBtn.Enabled = false;
            IsContinnueGrab = true;
            System.Threading.Tasks.Task.Factory.StartNew(new Action(() => {
                while (IsContinnueGrab) {
                    if (GrabedImg != null) GrabedImg.Dispose();
                    if (CameraCtrl.Instance.GrabImg(TeachCaliPara.camLightPara.CamName, out GrabedImg))
                    {
                        view1.ResetView();
                        view1.Refresh();
                        view1.AddViewImage(GrabedImg);
                        view1.Repaint();
                    }
                    System.Threading.Thread.Sleep(100);
                }
            }));
        }

        private void StopGrabBtn_Click(object sender, EventArgs e) {
            IsContinnueGrab = false;
            ContinueGrabBtn.Enabled = true;
        }

        private void GrabImgBtn_Click(object sender, EventArgs e)  {
            if (GrabedImg != null) GrabedImg.Dispose();
            CameraCtrl.Instance.GrabImg(TeachCaliPara.cam, out GrabedImg); //采集图像
            view1.Refresh();
            view1.AddViewImage(GrabedImg);
            view1.Repaint();
        }

        private void FrmCaliPara_Load(object sender, EventArgs e) {
            LocalModelCbx.SelectedIndex = (int) TeachCaliPara.localPara.localSetting.localModel;
            CaliModelCbx.SelectedIndex = 0;
        }
        private void SaveImgBtn_Click(object sender, EventArgs e)
        {
            try{
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

        private void FrmCaliPara_FormClosing(object sender, FormClosingEventArgs e) {
            IsContinnueGrab = false;
        }

        private void FrmCaliPara_FormClosed(object sender, FormClosedEventArgs e) {
            IsContinnueGrab = false;
        }
        HObject GrabedImg = new HObject();
        HObject CaliContour = new HObject();
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
        public bool GetHandEyeCaliPt(Point2Db StartPt, Point2Db EndPt,  bool IsCameraMovingWithAxisX, bool IsCameraMovingWithAxisY, 
            out List<Point2Db> PixelPosP, out List<Point2Db> MotionPosW)
        {
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
            for (int row = 0; row <= StepCount - 1; row += 1) {
                for (int col = 0; col <= StepCount - 1; col += 1){
                    Point2Db movePt = new Point2Db();  //九点标定时，相机移动到一个点，然后开始拍照
                    Point2Db CaliMovePt = new Point2Db();//九点标定时的点，坐下角为第一个点，右上角为第九个点
                    if (IsCameraMovingWithAxisX && IsCameraMovingWithAxisY)          //相机跟随XY周移动
                        movePt = new Point2Db(startPt.Col + (StepCount-1 - col) * stepX, startPt.Row + (StepCount-1 - row) * stepY);
                    else if ((IsCameraMovingWithAxisX) && (!IsCameraMovingWithAxisY))    //相机跟随X轴移动
                        movePt = new Point2Db(startPt.Col + (StepCount -1 - col) * stepX, startPt.Row + row * stepY);
                    else if ((!IsCameraMovingWithAxisX) && IsCameraMovingWithAxisY)    //相机跟随Y轴移动
                        movePt = new Point2Db(startPt.Col + col * stepX, startPt.Row + (StepCount-1 - row) * stepY);
                    else if ((!IsCameraMovingWithAxisX) && (!IsCameraMovingWithAxisY))   //相机固定
                        movePt = new Point2Db(startPt.Col + col * stepX, startPt.Row +  row * stepY);
                    CaliMovePt = new Point2Db(startPt.Col + col * stepX, startPt.Row + row * stepY);
                    // 控制平台移动到目标位置
                    if (!MotionManager.Instance.SetCoordiPos(movePt.Col,movePt.Row, TeachCaliPara.EndCaliPt.angle)){
                        MessageBox.Show("X Y轴运动失败!");
                        return false;
                    }
                    Thread.Sleep(2000);
                    GrabedImg = new HObject();
                    CameraCtrl.Instance.GrabImg(TeachCaliPara.cam, out GrabedImg);  //相机拍照抓取图片
                    Point2Db Pt = new Point2Db();
                    if (GrabedImg.IsInitialized()) {
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
                    else{
                        MessageBox.Show("采集图片失败");
                        return false;                    
                    }
                }
            }
            return true;
        }



        /// <summary>  单轴的情况下
        /// 获取九点标定的九个世界坐标点，九个像素坐标点
        /// </summary>
        /// <param name="StartPt">示教起始点</param>
        /// <param name="EndPt">示教终点</param>
        /// <param name="IsCameraMovingWithAxisX"></param>
        /// <param name="IsCameraMovingWithAxisY"></param>
        /// <param name="PixelPosP"></param>
        /// <param name="MotionPosW"></param>
        /// <returns></returns>
        public bool GetHandEyeCaliPt2(Point2Db StartPt, Point2Db EndPt, bool IsCameraMovingWithAxisX, bool IsCameraMovingWithAxisY,
            int StepCountX,int StepCountY,out List<Point2Db> PixelPosP, out List<Point2Db> MotionPosW){
            int StepCount = 3;
            PixelPosP = new List<Point2Db>();
            MotionPosW = new List<Point2Db>();
            Point2Db startPt = new Point2Db(Math.Min(StartPt.Col, EndPt.Col), Math.Min(StartPt.Row, EndPt.Row));
            Point2Db endPt = new Point2Db(Math.Max(StartPt.Col, EndPt.Col), Math.Max(StartPt.Row, EndPt.Row));
            double recWidth = endPt.Col - startPt.Col;
            double recHeight = endPt.Row - startPt.Row;
            double stepX = recWidth / (StepCount - 1);
            double stepY = recHeight / (StepCount - 1);
            if (StepCountX == 1){
                stepX = 0;
            }
            if (StepCountY == 1){
                stepX = 0;
            }
            List<double> supposeX = new List<double>(), supposeY = new List<double>();
            List<double> currentX = new List<double>(), currentY = new List<double>();
            LocalManager MyLocal = new LocalManager(); //定位类初始化
            LocalResult MyResult = new LocalResult();
            MyLocal.SetLocalModel(TeachCaliPara.localPara.localSetting.localModel);
            HOperatorSet.GenEmptyObj(out CaliContour);
            for (int row = 0; row <= StepCountY - 1; row += 1) {
                for (int col = 0; col <= StepCountX - 1; col += 1){
                    Point2Db movePt = new Point2Db();  //九点标定时，相机移动到一个点，然后开始拍照
                    Point2Db CaliMovePt = new Point2Db();//九点标定时的点，坐下角为第一个点，右上角为第九个点
                    if (IsCameraMovingWithAxisX && IsCameraMovingWithAxisY)          //相机跟随XY周移动
                        movePt = new Point2Db(startPt.Col + (StepCount - 1 - col) * stepX, startPt.Row + (StepCount - 1 - row) * stepY);
                    else if ((IsCameraMovingWithAxisX) && (!IsCameraMovingWithAxisY))    //相机跟随X轴移动
                        movePt = new Point2Db(startPt.Col + (StepCount - 1 - col) * stepX, startPt.Row + row * stepY);
                    else if ((!IsCameraMovingWithAxisX) && IsCameraMovingWithAxisY)    //相机跟随Y轴移动
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
                    bool IsTrue = false;
                    GrabedImg = new HObject();
                    CameraCtrl.Instance.GrabImg(TeachCaliPara.cam, out GrabedImg);  //相机拍照抓取图片
                    Point2Db Pt = new Point2Db();
                    if (GrabedImg.IsInitialized()) {
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
                    else{
                        MessageBox.Show("采集图片失败");
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 小角度找圆心
        /// </summary>
        /// <param name="ListPtIn"> </param>
        /// <param name="CenterX"></param>
        /// <param name="CenterY"></param>
        /// <param name="CirR"></param>
        /// <returns></returns>
        public bool FindCircleCenter( List<Point2Db> ListPtIn,out double  CenterX,out double CenterY ,out double CirR)
        {
            CenterX = 0;
            CenterY = 0;
            CirR = 0;
            int a = 0; int b = 0;
            List<ST_Line> line = new List<ST_Line>();
            List<PointXYZ> input = new List<PointXYZ>();
            HTuple phi = new HTuple();

            //1.0 获取中垂线
            for (int i = 0; i < ListPtIn.Count(); i++){
                for (int j = i + 1; j < ListPtIn.Count(); j++){
                    double midX = (ListPtIn[i].Col + ListPtIn[j].Col) / 2;
                    double midY = (ListPtIn[i].Row + ListPtIn[j].Row) / 2;
                    HOperatorSet.LineOrientation(ListPtIn[i].Row, ListPtIn[i].Col, ListPtIn[j].Row, ListPtIn[j].Col, out phi);
                    line.Add(new ST_Line(true));//求中垂线
                    ST_Line MyLine = new ST_Line(true);
                    MyLine.startX = midX + phi.TupleSin().D * 200.0;
                    MyLine.startY = midY + phi.TupleCos().D * 200.0;
                    MyLine.endX = midX - phi.TupleSin().D * 200.0;
                    MyLine.endY = midY - phi.TupleCos().D * 200.0;
                    line[a] = MyLine;
                    a++;
                }
            }
            //2.0 找交点
            for (int i = 0; i < a; i++){
                for (int j = i + 1; j < a; j++){
                    HOperatorSet.IntersectionLl(line[i].startY, line[i].startX, line[i].endY, line[i].endX,line[j].startY, line[j].startX,
                        line[j].endY, line[j].endX, out HTuple row, out HTuple column, out HTuple isParallel);
                    if (isParallel.I == 1) { continue; }
                    input.Add(new PointXYZ());
                    input[b].X = column.D;
                    input[b].Y = row.D;
                    b++;
                }
            }
            //3.0 按权重找出圆心
            PointXY xy = new PointXY(0, 0);
            double cx = 0.0; double cy = 0.0;
            //for (int i = 0; i < b; i++){
            //    if (input[i].X > 10000 || input[i].Y > 10000) {
            //        input.Remove(input[i]);
            //        i--;
            //        b--;
            //    }
            //}
            for (int i = 0; i < b; i++){
                cx += input[i].X;
                cy += input[i].Y;
            }
            xy.X = cx / b;
            xy.Y = cy / b;
            InverseDistanceWeighted(input, xy);
            double X = 0; double Y = 0;
            for (int i = 0; i < b; i++){
                X += input[i].X * input[i].Z;
                Y += input[i].Y * input[i].Z;
            }
            CenterX = X;
            CenterY = Y;
            double DistR = 0;
            for (int i = 0; i < ListPtIn.Count(); i++) { 
                 DistR  = DistR +  Math.Sqrt(Math.Pow((ListPtIn[0].Col - X), 2) + Math.Pow((ListPtIn[0].Row - Y), 2));
            }
            DistR = DistR / ListPtIn.Count();
            return true;
        }
        

        public class PointXYZ
        {
            public double X;
            public double Y;
            public double Z;
        }

        public class PointXY
        {
            public double X;
            public double Y;
            public PointXY(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        public class Point
        {
            public double startX;
            public double startY;
            public double endX;
            public double endY;
        }

        public struct ST_Line
        {
            public double startX { get; set; }
            public double startY { get; set; }
            public double endX { get; set; }
            public double endY { get; set; }

            public ST_Line(bool IsConti = true)
            {
                startX = 0;
                startY = 0;
                endX = 0;
                endY = 0;
            }
        
        }

        /// <summary>
        /// 插值算法 反距离加权法IDW 
        /// </summary>  
        /// <param name="input">离散点的XYZ</param>
        /// <param name="outpoint">平均点的XY</param> 
        /// <returns></returns>
        public static bool InverseDistanceWeighted(List<PointXYZ> input, PointXY outpoint)
        {
            try{
                double r = 0.0;    //距离的倒数和             
                double ri = 0.0;    //i点的权重
                foreach (PointXYZ inputpoint in input) {
                    r += 1.0 / (Math.Pow(inputpoint.X - outpoint.X, 2) + Math.Pow(inputpoint.Y - outpoint.Y, 2));
                }
                foreach (PointXYZ inputpoint in input){
                    ri = 1.0 / (Math.Pow(inputpoint.X - outpoint.X, 2) + Math.Pow(inputpoint.Y - outpoint.Y, 2)) / r;
                    inputpoint.Z = ri;
                }
                return true;
            }
            catch{
                return false;
            }
        }
    }
}
