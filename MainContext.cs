using System;
using System.Windows.Forms;
using EngPopup.forms;
using SqliteGateWay;
using Shell;
using System.Threading;
using ElapsedTimer;
using PopupWindow;
using WordSelector;

using Dispatcher = System.Windows.Threading.Dispatcher;
using DispatcherPriority = System.Windows.Threading.DispatcherPriority;


namespace EngPopup
{   
    // эксепшны свои  ю собрать в одном месте сообщения и ошибки

    class MainContext : ApplicationContext{
        private TrayForm      Tray     = new TrayForm();
        private TimerControl  timer    = new TimerControl();
        private CommandStore  commands = new CommandStore();//юзинг&
        private WordSelector.WordSelector selector = new WordSelector.WordSelector();// не хочет рандомить 
        private PopupControll popup = new PopupControll();
        private DictionsControl dictions = new DictionsControl(new DictionInfo().GetAllDictions());
        private Dispatcher disp;

        public MainContext() {
            disp = Dispatcher.CurrentDispatcher;
            this.Initilize();
        }

        private void RunConsole(){
            using(ShellPromt    promt    = new ShellPromt()) {
                commands.Add(new CommandTimer(timer));
                commands.Add(new CommandStop(timer));
                commands.Add(new CommandCallPopup(timer));
                commands.Add(new CommandStart(timer));
                commands.Add(new CommandSelectWord());
                commands.Add(new CommandInsertWord());
                commands.Add(new CommandDeleteWord());
                commands.Add(new CommandSetPriority());
                commands.Add(new CommandEnableWord());
                commands.Add(new CommandDisableWord());
                commands.Add(new CommandHelp(commands));
                commands.Add(new CommandQuitShell(promt));
                commands.Add(new CommandResetRandomSequence(selector));
                commands.Add(new CommandChangeDistribution(selector));
                commands.Add(new CommandPopupDalay(popup));
                commands.Add(new CommandUsingDictions(dictions));
                commands.Add(new CommandDisableDictions(dictions));
                promt.UserInput += new EventHandler<EventConsoleArgs>(promt_UserInput);
                promt.Run();
                promt.UserInput -= promt_UserInput;
                commands.Clear();
            }
        }
        private void promt_UserInput(object sender,EventConsoleArgs args) {
            CommandParser.ImputCommand input = new  CommandParser.ImputCommand(args.UserInput);
            ShellPromt promt = sender as ShellPromt;
            promt.Print(commands.ExecuteAndGetResponse(input));
        }
        private void timer_Elapsed(object sender,System.EventArgs args) {
            disp.BeginInvoke(
                new Action(() => {
                    Record row = selector.GetWord();
                    //PopupWindow.PopupWindow popup1    = new PopupWindow.PopupWindow();
                    popup.Show(row.word + " >>> " + row.trans + "\n >>> freq #" + row.freq + " call #" + row.call + "\n" + System.DateTime.Now.ToLongTimeString());
                    row.IncreaseCall();
                })
                ,DispatcherPriority.Background,null);
        }   /// все что тут переписывать 
       
        private void Initilize() {
            this.InitilizeTrayForm();
            timer.Interval = 60;
            timer.Elapsed  +=new EventHandler(timer_Elapsed);
            timer.Start();
        }
        private void InitilizeTrayForm() {
            MenuItem ConsoleMenu = new MenuItem("Console");
            MenuItem CloseMenu = new MenuItem("Close");
            CloseMenu.Click += delegate {
                Tray.Visible = false;
                Application.Exit();
            };
            ConsoleMenu.Click += delegate {
                Thread t = new Thread(RunConsole);
                t.IsBackground = true;
                t.Start();
            };
            Tray.AddMeniItem(ConsoleMenu);
            Tray.AddMeniItem(CloseMenu);
        }
    }
}
