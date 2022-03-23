using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionBase;
using System.Threading;
using HalconDotNet;
using LD.Config;
using LD.Ui;

namespace MainVision
{
    public partial class MainForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public MainForm()
        {
            InitializeComponent();
            this.LeftTapeRightPanel.Visible = false;
        }

     
        /// <summary>
        /// PLC
        /// </summary>
        //public LD.Ui.frmSiemens frmSiemens ;
        public LD.Ui.frmSocket frmSocket ;
        public LD.Ui.frmSerial frmSerial;
        public LD.Ui.frmReport frmReport ;
        public PinReport PinReport;

        private void MainForm_Load(object sender, EventArgs e)
        {
           // frmSiemens =  LD.Ui.frmSiemens.Instance;
            frmSocket = new LD.Ui.frmSocket();
            frmSerial = new LD.Ui.frmSerial();
            frmReport = new LD.Ui.frmReport();
            PinReport = new PinReport();

            InitCCDPanel();//显示系统初始化
            InitLogListView();
            FileLib.Logger.OnLogHappenedEvent += Logger_OnLogHappenedEvent;
            Thread.Sleep(100);
            FileLib.Logger.Pop("打开软件");
            LD.Config.ConfigManager.Instance.Load();
            CameraParaManager.Instance.Read();
            CameraCtrl.Instance.Init();
            LightCrlParaManager.Instance.Read();
            LightCtrlManager.Instance.DoInit();
            LightCtrlManager.Instance.DoStart();
            
            //if( !VisionBase.Camera.Instance.DoInit())
            //    FileLib.Logger.Pop("打开相机失败");            
            PlcDockPanel.Dock = DockStyle.Fill;
            ReportDockPanel1.Dock = DockStyle.Fill;
            LD.Ui.frmSiemens.Instance.Show(this.PlcDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            frmSocket.Show(this.PlcDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            frmSerial.Show(this.PlcDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            PinReport.Show(this.ReportDockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            LD.Device.DeviceManager.Instance.DeviceInit();
            LD.Device.DeviceManager.Instance.DeviceStart();
            FrmLightCrlManager.Instance.Show(this.LightDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            VisionBase.FrmCameraManager.Instance.Show(this.LightDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            VisionBase.ProjectParaManager.Instance.Read();
            VisionBase.FrmVisionProjectPara.Instance.Show(this.ProjectPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            CaliParaManager.Instance.Read();
            VisionBase.FrmCaliParaManager.Instance.Show(this.ProjectPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            StartBtn.BackColor = Color.LightGray;
            this.FormClosing += MyFormClosing;
        }


        private void getXmlInfo()
        {
            string ConfigName = string.Format(@"config\{0}", "PinReport.xml");
            DataSet myds = new DataSet();
            myds.ReadXml(ConfigName);
            //dataGridView1.DataSource = myds.Tables[0];
        }


        private int LogIndex = 0;
        private void InitLogListView()
        {
            foreach (var item in FileLib.Logger.LogContentList)
            {
                string dateTm = string.Format(DateTime.Now.ToString("HH:mm:ss;fff"));
                ListViewItem vitem = new ListViewItem();
                vitem.Text = (++LogIndex).ToString();
                vitem.SubItems.Add(item);
                listViewLog.Items.Add(vitem);
                int index = listViewLog.Items.Count;
                listViewLog.EnsureVisible(index - 1);
            }
        }

        void Logger_OnLogHappenedEvent(string content, bool isError)
        {
            if (listViewLog.IsDisposed) return;

            listViewLog.BeginInvoke(new Action(() => {
                string dateTm = string.Format(DateTime.Now.ToString("HH:mm:ss;fff"));
                ListViewItem item = new ListViewItem();
                item.Text = (++LogIndex).ToString();
                item.SubItems.Add(content);
                listViewLog.Items.Add(item);
                int index = listViewLog.Items.Count;
                listViewLog.EnsureVisible(index - 1);
            }));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileLib.Logger.OnLogHappenedEvent -= Logger_OnLogHappenedEvent;
            try{
                //PLC读取线程
                LD.Config.ConfigManager.Instance.ConfigPlc.bReadThread = false;
                //释放设备
                LD.Device.DeviceManager.Instance.DeviceStop();
                LD.Device.DeviceManager.Instance.DeviceRelease();
                LD.Config.ConfigManager.Instance.Save();
                HOperatorSet.CloseAllFramegrabbers();
            }
            catch
            { }
            //this.Close();
        }

        /// <summary>
        /// 手动加载一张图片到控件上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = false;
            StartBtn.BackColor = Color.Green;
            StopBtn.BackColor = Color.Red;
            LeftWorkFlow.Intance.Init();
            RightWorkFlow.Instance.Init();
           
            LD.Logic.PlcHandle.Instance.PlcValueChangedEvent += LeftWorkFlow.Intance.WaitPlcTrigger;
            LD.Logic.PlcHandle.Instance.PlcValueChangedEvent += RightWorkFlow.Instance.WaitPlcTrigger;
            LD.Logic.PlcHandle.Instance.PlcValueChangedEvent += FeederWorkFlow.Instance.WaitPlcTrigger;
            LD.Logic.PlcHandle.Instance.PlcValueChangedEvent += LoadingWorkFlow.Instance.WaitPlcTrigger;
            LeftWorkFlow.Intance.Start();
            RightWorkFlow.Instance.Start();
            FeederWorkFlow.Instance.Start();
            LoadingWorkFlow.Instance.Start();
        }


        private void InitCCDPanel()
        {
            DisplaySystem.InitCCDPanel();
            DisplaySystem.AddPanelForCCDView("左侧下相机左上角拍照位", PadLeftUpPanel);
            DisplaySystem.AddPanelForCCDView("左侧下相机右下角拍照位", PadRightUpPanel);
            DisplaySystem.AddPanelForCCDView("右侧下相机左上角拍照位", PadLeftDnPanel);
            DisplaySystem.AddPanelForCCDView("右侧下相机右下角拍照位", PadRightDnPanel);

            DisplaySystem.AddPanelForCCDView("上相机左上角拍照位1", LeftTapeLeftPanel);
            DisplaySystem.AddPanelForCCDView("上相机右下角拍照位1", LeftTapeRightPanel);
            DisplaySystem.AddPanelForCCDView("上相机左上角拍照位2", RightTapeLeftPanel);
            DisplaySystem.AddPanelForCCDView("上相机右下角拍照位2", RightTapeRightPanel);

            DisplaySystem.AddPanelForCCDView("Ipad左上角拍照位1", FeederLeftUpPanel);
            DisplaySystem.AddPanelForCCDView("Ipad右下角拍照位1", FeederRightDnPanel);
            DisplaySystem.AddPanelForCCDView("Ipad左上角拍照位2", LoadingLeftUpPanel);
            DisplaySystem.AddPanelForCCDView("Ipad右下角拍照位2", LoadingRightPanel); ;

            //DisplaySystem.AddPanelForCCDView(CameraTest.DnCam1, panelLeftDnCCD);
            //DisplaySystem.AddPanelForCCDView(CameraTest.DnCam2, panelRightDnCCD);
        }

        HObject NowImg = new HObject();


        public void Changejurisdiction()
        {
            ConfigSystem cfg = ConfigManager.Instance.ConfigSystem;
            MainForm1.mainfrm.Invoke(new EventHandler(delegate
            {
                try {
                    if (cfg.State == 3)//操作员operator无示教和保存参数权限
                    {
                        //VisionTeachBtn.Enabled = false;
                        //VisionParaSaveBtn.Enabled = false;
                        //dataGridView1.Enabled = false;
                        //dataGridView2.Enabled = false;
                    }
                    if (cfg.State == 2)//用户User只能示教熟悉流程，不能保存参数权限
                    {
                        //VisionTeachBtn.Enabled = true;
                        //VisionParaSaveBtn.Enabled = false;
                    }
                    if (cfg.State == 1)//管理员Administrator具有示教和保存参数的权限
                    {
                        //VisionTeachBtn.Enabled = true;
                        //VisionParaSaveBtn.Enabled = true;
                    }
                }
                catch (Exception)
                { }
            }));
        }

        //抓图
        private HTuple AcqHandle1;
        private HObject Img;

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopBtn.BackColor = Color.DarkGray;
            StartBtn.Enabled = true;
            StartBtn.BackColor = Color.LightGray;
            IsContinue = false;
            LD.Logic.PlcHandle.Instance.PlcValueChangedEvent -= LeftWorkFlow.Intance.WaitPlcTrigger;
            LD.Logic.PlcHandle.Instance.PlcValueChangedEvent -= RightWorkFlow.Instance.WaitPlcTrigger;
            LD.Logic.PlcHandle.Instance.PlcValueChangedEvent -= FeederWorkFlow.Instance.WaitPlcTrigger;
            LD.Logic.PlcHandle.Instance.PlcValueChangedEvent -= LoadingWorkFlow.Instance.WaitPlcTrigger;
            LoadingWorkFlow.Instance.Stop();
            FeederWorkFlow.Instance.Stop();
            LeftWorkFlow.Intance.Stop();
            RightWorkFlow.Instance.Stop();

            LightCtrlManager.Instance.SetAllLightTo0();
            FileLib.Logger.Pop("  关闭光源：", false,  "运行日志");
        }

        bool IsContinue = true;
        private void MyFormClosing(object sender, FormClosingEventArgs e)
        {
            //Dialog MyDlg = 
            DialogResult DlgReslult = MessageBox.Show("是否关闭程序", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DlgReslult == DialogResult.No) e.Cancel = true;
            CameraCtrl.Instance.CloseAllCamera();
           // CameraManager.Instance.Close();
            LightCtrlManager.Instance.DoStop();
            LD.Device.DeviceManager.Instance.DeviceStop();
            LD.Device.DeviceManager.Instance.DeviceRelease();
        }

        private void Button1_Click_2(object sender, EventArgs e)
        {           
            LD.Logic.PlcHandle.Instance.WriteValue(LD.Common.PlcDevice.V_02_TriggerGrab, 1); 
        }

        private void SwitchPanelBtn_Click(object sender, EventArgs e)
        {
            if (LeftTapeRightPanel.Visible) {
                LeftTapeRightPanel.Visible = false;
                LeftTapeLeftPanel.Visible = true;
            }
            else {
                LeftTapeRightPanel.Visible = true;
                LeftTapeLeftPanel.Visible = false;

            }
        }
    }
}
