using System.Windows.Forms;
using System.Drawing;
using System;

namespace EngPopup.forms
{
    partial class TrayForm
    {
        private const string TRAY_NAME=@"EngEngIner";
        private const string ICON_NAME=@"TrayIcon.ico";
        //private string ICON_PATH=System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData );
        private const string ICON_PATH = @"G:\eng popup\EngPopup\forms";
        private NotifyIcon TrayIcon;
        public bool Visible {
            get {
                return TrayIcon.Visible;
            }
            set {
                TrayIcon.Visible = value;
            }
        }

        private void InitilizeComponent(){
            TrayIcon=new NotifyIcon();
            TrayIcon.Icon=new Icon(ICON_PATH+@"\"+ICON_NAME);
            TrayIcon.ContextMenu = new ContextMenu();
            TrayIcon.Text=TRAY_NAME;
            TrayIcon.Visible=true;
        }

    }
    partial class TrayForm 
    {
        public TrayForm()
        {   
           InitilizeComponent();
        }
        public void AddMeniItem(MenuItem menu)
        {
            TrayIcon.ContextMenu.MenuItems.Add(menu);
        }
    }   
}
