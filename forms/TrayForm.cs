using System.Windows.Forms;
using System.Drawing;
using System;

namespace EngPopup.forms
{
    partial class TrayForm{
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
<<<<<<< HEAD
    partial class TrayForm{
        public TrayForm(){
           InitilizeComponent();
        }
        public void AddMeniItem(TrayMenu menu){
            TrayIcon.ContextMenu.MenuItems.Add(menu);
            TrayIcon.ContextMenu.MenuItems[TrayIcon.ContextMenu.MenuItems.Count-1].Click += new EventHandler(OnMenuItemClick);
        }
        private void OnMenuItemClick(object sender , EventArgs args ){
            try
            {
                if (sender is TrayMenu)
                {
                    TrayMenu trayitem = sender as TrayMenu;
                    trayitem.Action();
                }
                else throw new Exception("not TrayMenu Item");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
=======
    partial class TrayForm 
    {
        public TrayForm()
        {   
           InitilizeComponent();
        }
        public void AddMeniItem(MenuItem menu)
        {
            TrayIcon.ContextMenu.MenuItems.Add(menu);
>>>>>>> gateway comlite
        }
    }   
}
