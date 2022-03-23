using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LD
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //Config.ConfigManager.Instance.Load();
            //Ui.frmMdi.frm = new Ui.frmMdi();
            //Application.Run(new Ui.frmSplash());
            //Application.Run(Ui.frmMdi.frm);
        }
    }
}
