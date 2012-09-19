using System;
using System.Windows.Forms;
using EngPopup.forms;
using SqliteGateWay;
using Shell;
using System.Threading;
using ElapsedTimer;
using PopupWindow;
using WordSelector;

namespace EngPopup
{   
    // todoglobal //randomize select whith priority   
    // эксепшны свои  ю собрать в одном месте сообщения и ошибки
    // проблемы с кодировками . старые записи с вопросами 

    class MainContext : ApplicationContext{   
        private TrayForm      Tray     = new TrayForm();
        private TimerControl  timer    = new TimerControl();
        private CommandStore  commands = new CommandStore();//юзинг
        private ShellPromt    promt    = new ShellPromt(); // может юзинг

        public MainContext() {
           this.Initilize();
        }

        private void RunConsole(){
            commands.Add(new CommandTimer(timer));
            commands.Add(new CommandStop(timer));
            commands.Add(new CommandCallPopup(timer));
            commands.Add(new CommandStart(timer));
            commands.Add(new CommandSelectWord());
            commands.Add(new CommandInsertWord());
            commands.Add(new CommandDeleteWord());
            commands.Add(new CommandSetPriority());
            commands.Add(new CommandHelp(commands));
            commands.Add(new CommandQuitShell(promt));
            promt.UserInput+=new EventHandler<EventConsoleArgs>(promt_UserInput);
            promt.Run();
            promt.UserInput -= promt_UserInput;
            promt.Dispose();
            commands.Clear();
            System.Diagnostics.Debug.WriteLine("stop console");
        }
        private void promt_UserInput(object sender,EventConsoleArgs args) {
            CommandParser.ImputCommand input = new  CommandParser.ImputCommand(args.UserInput);
            promt.Print(commands.ExecuteAndGetResponse(input));
        }
        private void timer_Elapsed(object sender,System.EventArgs args) {
                WordSelector.WordSelector selector = new WordSelector.WordSelector();
                Standart_2500Record row = selector.GetWord();
                PopupControll popup    = new PopupControll();
                //popup.Show(row.word + " >>> " + row.trans + "\n >>> freq #" + row.freq + " call #" + row.call);
                popup.Show(row.word + " >>> " + row.trans + "\n >>> freq #" + row.freq + " call #" + row.call+"\n"+System.DateTime.Now.ToLongTimeString());
        }   /// все что тут переписывать 
        private void Initilize() {
            this.InitilizeTrayForm();
            timer.Interval = 3;
            timer.Elapsed  +=new EventHandler(timer_Elapsed);
            timer.Enabled  = true;
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