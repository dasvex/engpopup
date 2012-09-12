using System;
using System.Windows.Forms;
using EngPopup.forms;
using SqliteGateWay;
using Shell;
using System.Threading;
using ElapsedTimer;

namespace EngPopup
{   //todoglobal //randomize select whith priority  //parse command 
    class MainContext : ApplicationContext
    {   //todo system components in class and send to constructor cinsole
        private TrayForm      Tray  = new TrayForm();
        private PopupControll popup = new PopupControll();
        private TimerControl  time  = new TimerControl();
        public MainContext() {
            Tray.AddMeniItem(new TrayMenuShowForm());
            Tray.AddMeniItem(new TrayMenuRegisterHotKey());
            Tray.AddMeniItem(new TrayMenuCloseApp());
            time.Interval = 1;
            time.Elapsed+=new System.Timers.ElapsedEventHandler(time_Elapsed);
            time.Enabled = true;
            Thread t = new Thread(RunConsole);
                   t.IsBackground = true;
                   t.Start();
        }
        private  void RunConsole(){
            new ShellPromt(time);
            System.Diagnostics.Debug.WriteLine("ppp");
        }
        private void time_Elapsed(object sender , EventArgs args) {
            System.Diagnostics.Debug.WriteLine("ololo");
        }
    }
}
