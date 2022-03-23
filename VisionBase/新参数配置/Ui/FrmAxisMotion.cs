using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionBase
{
    public partial class FrmAxisMotion : Form
    {
        public FrmAxisMotion()
        {   
            InitializeComponent();
            InitCombobox();
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
        }

        private void FrmAxisMotion_Load(object sender, EventArgs e)
        {
            MotionManager.Instance.SetCoordi(CoordiEmum.Coordi0);
            timer1.Enabled = true;
        }
        private CoordiEmum NowCoordi = CoordiEmum.Coordi0;
        private void InitCombobox()
        {
            Array arr = System.Enum.GetValues(typeof(CoordiEmum));    // 获取枚举的所有值
            DataTable dt = new DataTable();
            dt.Columns.Add("String", Type.GetType("System.String"));
            dt.Columns.Add("Value", typeof(int));
            foreach (var a in arr) {
                string strText = EnumTextByDescription.GetEnumDesc((CoordiEmum)a);
                DataRow aRow = dt.NewRow();
                aRow[0] = strText;
                aRow[1] = (int)a;
                dt.Rows.Add(aRow);
            }
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "String";
            comboBox1.ValueMember = "Value";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataRow dr1 = (DataRow)comboBox1.SelectedItem;
            // string str = comboBox1.SelectedValue.ToString();
             timer1.Enabled = false;
             NowCoordi = (CoordiEmum)Enum.Parse(typeof(CoordiEmum), comboBox1.SelectedValue.ToString(), false);
             MotionManager.Instance.SetCoordi(NowCoordi);
             timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try {
                GetCurPos();
            }
            catch
            { }
        }

        private void GetCurPos()
        {
            double X = 0, Y = 0, Z = 0, Theta = 0;
            MotionManager.Instance.GetCoordiPos(out X, out Y, out Z,out Theta);
            txtRealX.Text = X.ToString();
            txtRealY.Text = Y.ToString();
            txtRealZ1.Text = Z.ToString();
            txtRealR.Text = Theta.ToString();
        }

        private bool  checkAxisTag(Button btn, out int axisIndex)
        {
            axisIndex = 0;
            string axisTag = (string)btn.Tag;
            switch (axisTag) {
                case "X":
                    axisIndex = 0;
                    break;
                case "Y":
                    axisIndex = 1;
                    break;
                case "Z":
                    axisIndex = 2;
                    break;
                case "R":
                    axisIndex = 3;
                    break;
            }
            return true;     
        }


        private void btnAxisPosMotionMouseDn_Click(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int axisIndex = 0;
            checkAxisTag(btn, out axisIndex);
            AxisEnum nowAxis = (AxisEnum) (axisIndex + (int)NowCoordi * 8);         
            double rltDis = 0;
            if (!CheckRltTextValue(ref rltDis)) return;
            double NowPos = 0;
            if( !MotionManager.Instance.GetAxisPos(nowAxis, out NowPos)) return;
            AxisEnum NowAxisWrite = (AxisEnum)(axisIndex + (int)NowCoordi * 8 + 4);
            MotionManager.Instance.SetAxisPos(NowAxisWrite, NowPos+ rltDis);
        }

        private void btnAxisNegMotionMouseDn_Click(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int axisIndex = 0;
            checkAxisTag(btn, out axisIndex);
            AxisEnum nowAxis = (AxisEnum)(axisIndex + (int)NowCoordi * 8);
            double rltDis = 0;
            if (!CheckRltTextValue(ref rltDis)) return;
            double NowPos = 0;
            if (!MotionManager.Instance.GetAxisPos(nowAxis, out NowPos)) return;
            AxisEnum NowAxisWrite = (AxisEnum)(axisIndex + (int)NowCoordi * 8 + 4);
            MotionManager.Instance.SetAxisPos(NowAxisWrite, NowPos - rltDis);
        }
        private bool CheckRltTextValue(ref double rltDis){
            try {
                rltDis = double.Parse(txtRltDis.Text.Trim());
                rltDis = Math.Abs(rltDis);
                txtRltDis.Text = rltDis.ToString("f3");
            }
            catch (System.Exception ex) {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                txtRltDis.Text = "1";
                return false;
            }
            return true;
        }

        private void FrmAxisMotion_FormClosing(object sender, FormClosingEventArgs e){
            timer1.Enabled = false;
        }


    }
}
