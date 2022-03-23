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
    public partial class FrmView : Form
    {
        private Action closeAction = null;
        private object ObjLock = new object();
        private ViewControl ViewCtl = null;
        public FrmView()
        {
            InitializeComponent();
        }
        public FrmView(Action CloseAction)
        {
            InitializeComponent();
            this.closeAction = CloseAction;
        }
        private void FrmView_Load(object sender, EventArgs e)
        {
            //this.OrgImagePart=hWindowControl1.ImagePart;
        }










    }
}
