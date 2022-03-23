using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LD.Config;

namespace MainVision
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        public ConfigSystem cfg
        {
            get;
            set;
        }
        private void UserLogin_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            cfg = ConfigManager.Instance.ConfigSystem;

            foreach (string item in cfg.Admin)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cfg.Admin.Count(); i++)
            {
                if (comboBox1.Text == cfg.Admin[i])
                {
                    if (cfg.Pass[i] == textBox6.Text)
                    {
                        switch (cfg.Admin[i])
                        {
                            case "Admin":
                                cfg.State = 1;
                                break;

                            case "User":
                                cfg.State = 2;
                                break;

                            case "Operator":
                                cfg.State = 3;
                                break;

                        }
                    }
                }
            }
            //MainForm1.mainfrm.mainForm.Close();
            MainForm1.mainfrm.mainForm.Changejurisdiction();
            this.Close();
        }

    }
}
