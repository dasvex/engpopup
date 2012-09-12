using System.Windows.Forms;
namespace EngPopup.forms
{
     public abstract class TrayMenu : MenuItem
    {
         public abstract void Action();
         public TrayMenu(string menu_label)
             : base(menu_label){
         }
    }
    public class TrayMenuShowForm : TrayMenu
    {
        public override void Action()
        {
            MessageBox.Show("TrayShowMenu");
        }
        public TrayMenuShowForm() : base("Show") { }
    }
    
    public class TrayMenuCloseApp : TrayMenu
    {
        public override void Action()
        {
            Application.Exit();
            
        }
        public TrayMenuCloseApp() : base("Close") { }
    }
    public class TrayMenuRegisterHotKey : TrayMenu
    {
        public override void Action()
        {
            MessageBox.Show("action");
        }
        public TrayMenuRegisterHotKey() : base ("Hot Key") { }
    }
}
