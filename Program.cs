using System;
using System.Windows.Forms;
namespace EngPopup
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainContext());
        }
    }
}
