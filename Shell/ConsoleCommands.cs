using ElapsedTimer;
using CommandParser;
using SqliteGateWay;

namespace Shell {
    public abstract class AConsoleCommand {
        public string CallName {
            get;
            private set;
        }
        public string Description {
            get;
            private set;
        }
        public AConsoleCommand(string CallName , string Description) {
            this.CallName = CallName;
            this.Description = Description;
        }
        public abstract string Execute(ImputCommand param);
    }
    //command for Timer
    public class CommandTimer : AConsoleCommand {
        TimerControl Timer;
        public CommandTimer(TimerControl timer  )
            : base(".timer",@"set / get timer elapsed (sec)") {
               this.Timer = timer;
        }
        public override string Execute(ImputCommand param) {
            if(param.Count < 1) 
                return Timer.Interval.ToString();
            if(param.Count == 1) {
                double _interval;
                double.TryParse(param[0],out _interval);
                if(_interval <= 0   ||  _interval.ToString() == "")
                    return "#invalid time elapsed";
                Timer.Interval = _interval;
                return "#timer is set";
            }
            return "#invalid numbers of arg";
            
        }
    }  
    public class CommandStop  : AConsoleCommand {
        TimerControl Timer;
        public CommandStop(TimerControl timer)
            : base(".stop","program pause") {
             this.Timer = timer;
        }
        public override string Execute(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            Timer.Enabled = false;
            return @"#application stoped";
        }
    }
    public class CommandStart : AConsoleCommand {
        TimerControl Timer;
        public CommandStart(TimerControl timer)
            : base(".start","run program") {
                this.Timer = timer;
        }
        public override string Execute(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            Timer.Enabled = true;
            return @"#applicationis run";
        }
    }
    public class CommandCallPopup : AConsoleCommand {
        TimerControl Timer;
        public CommandCallPopup(TimerControl timer)
            : base(".callpopup","show next popup window") {
            this.Timer = timer;
        }
        public override string Execute(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            Timer.CallTimer();
            return @"#popup is called";
        }
    } 
    //command for Diction
    public class CommandSelectWord : AConsoleCommand {
        public CommandSelectWord()
            : base(".select","view word information") {
        }
        public override string Execute(ImputCommand param) {
            if(param.Count!=1)
                return "#invalid numbers of arg";
            User_dic row = new User_dic();
            row.word = param[0];
            row.SelectByWord();
            return row.word;
        }
    }//select word table?
    public class CommandInsertWord : AConsoleCommand {
        public CommandInsertWord()
            : base(".insert","insert new word") {
        }
        public override string Execute(ImputCommand param) {
            if(param.Count<=1 || param.Count>3)
                return "#invalid numbers of arg";
            User_dic row = new User_dic();
            if(param.Count > 1)
                row.word = param[0];
                row.trans = param[1];
            if(param.Count > 2) {
                int _prior;
                int.TryParse(param[2],out _prior);
                if(_prior < 0 || _prior.ToString() == "")
                    return "#invalid arg priority";
                row.priority = (uint)_prior;
            }
            row.Insert();
            return "#done";
        }
    }
    public class CommandDeleteWord : AConsoleCommand {
        public CommandDeleteWord()
            : base(".delete","delete word by template") {
        }
        public override string Execute(ImputCommand param) {
            if(param.Count != 1)
                return "#ivalid number of args";
            User_dic row = new User_dic();
            row.word = param[0];
            row.Delete();
            return "#done";
        }
    }
    public class CommandSetPriority : AConsoleCommand {
        public CommandSetPriority()
            : base(".priority","set priority call for word") {
        }
        public override string Execute(ImputCommand param) {
            if (param.Count!=2)
                return "#ivalid number of args";
            User_dic row = new User_dic();
            row.word = param[0];
            int _prior;
            int.TryParse(param[1],out _prior);
            if(_prior < 0 || _prior.ToString() == "")
                return "#invalid arg priority";
            row.priority = (uint)_prior;
            row.Insert();
            return "#done";
        }
    }
    // command for Shell
    public class CommandQuitShell : AConsoleCommand {
        ShellPromt Shell;
        public CommandQuitShell(ShellPromt shell)
            : base(".quit","close command promt") {
            this.Shell = shell;
        }
        public override string Execute(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            Shell.Print("#aborted");
            System.Threading.Thread.Sleep(1000);
            Shell.Dispose();
            return "";
        }
    }
    public class CommandHelp : AConsoleCommand {
        ShellPromt Shell;
        public CommandHelp(ShellPromt shell)
            : base(".help","commands guide") {
                this.Shell = shell;
        }
        public override string Execute(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            foreach(var item in Shell.AvalibleCommands()) {
                Shell.Print(item.ToString());
            }
            return "";
        }
    } 
}



