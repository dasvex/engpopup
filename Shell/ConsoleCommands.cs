using ElapsedTimer;
using CommandParser;
using SqliteGateWay;
//все сообщения надо бы в кучку . толи эксепшинами толи хмл
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
        public abstract string ExecuteAndGetResponse(ImputCommand param);
    }
    //command for Timer
    public class CommandTimer : AConsoleCommand {
        TimerControl Timer;
        public CommandTimer(TimerControl timer  )
            : base(".timer",@"set / get timer elapsed (sec)") {
               this.Timer = timer;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            switch(param.Count) {
                case 0:
                    return GetCurrentTimerInterval();
                case 1:
                    return GetResponseTrySetInterval(param[0]);
                default:
                    return "#invalid numbers of arg";
            }
        }
        private string GetCurrentTimerInterval(){
                return Timer.Interval.ToString();
        }
        private string GetResponseTrySetInterval(string interval) {
            double _interval;
            double.TryParse(interval,out _interval);
            if(_interval <= 0.001 || _interval.ToString() == "")
                return "#invalid time elapsed. minimal value 0.001";
            Timer.Interval = _interval;
            return "#timer is set";
        }
    }  
    public class CommandStop  : AConsoleCommand {
        TimerControl Timer;
        public CommandStop(TimerControl timer)
            : base(".stop","program pause") {
             this.Timer = timer;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            return this.OffTimerAndGetResponse();
        }
        private string OffTimerAndGetResponse() {
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
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            return this.OnTimerAndGetResponse();
        }
        private string OnTimerAndGetResponse() {
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
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            return this.CallTimerAndGetResponse();
        }
        private string CallTimerAndGetResponse() {
            Timer.CallTimer();
            return @"#popup is called";
        }
    } 
    //command for Diction
    public class CommandSelectWord : AConsoleCommand {
        public CommandSelectWord()
            : base(".select","view word information") {
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count!=1)
                return "#invalid numbers of arg";
            return GetRowByWord(param[0]);
        }
        private string GetRowByWord(string word) {
            string response_row=this.GetRowFromUserDic(word);
            if(response_row != "")
                response_row = response_row + "\n";
            response_row = response_row+this.GetRowFromStandartDic(word);
            return response_row == "" ? "not found" : response_row;
        }
        private string GetRowFromUserDic(string word){
            User_dic row = new User_dic();
            row.word = word;
            return row.SelectByWord()?row.word+"  "+row.trans+"  "+row.priority:""; 
        }
        private string GetRowFromStandartDic(string word){
            Standart_2500Record s_row = new Standart_2500Record();
            s_row.word = word;
            return s_row.SelectByWord()?s_row.word+"  "+s_row.trans+"":@"N\A";
        }
    }
    public class CommandInsertWord : AConsoleCommand {
        public CommandInsertWord()
            : base(".insert","insert new word") {
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count<=1 || param.Count>3)
                return "#invalid numbers of arg";
            return InsertRowAndGetResponse(param);
        }
        private string InsertRowAndGetResponse(ImputCommand param) {
            User_dic row = new User_dic();
            row = this.SetWordForRow(row,param[0]);
            row = this.SetTransForRow(row,param[1]);
            if(param.Count > 2) 
                if ( !this.TrySetPriorForRow(row , param[2]) )
                    return "#Invalid priopity";
            return row.Insert() ? "#done" : "#error inserting";
        }
        private User_dic SetWordForRow(User_dic row,string word) {
            row.word = word;
            return row;
        }
        private User_dic SetTransForRow(User_dic row,string word) {
            row.trans = word;
            return row;
        }
        private bool TrySetPriorForRow(User_dic row,string prior) {
            int _prior;
            int.TryParse(prior,out _prior);
            if(_prior < 0 || _prior.ToString() == "")
                return false;
            row.priority = (uint)_prior;
            return true;
        }
    } 
    public class CommandDeleteWord : AConsoleCommand {
        public CommandDeleteWord()
            : base(".delete","delete word by template") {
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count != 1)
                return "#ivalid number of args";
            return DeleteRowByWordAndGetResponse(param[0]);
        }
        private string DeleteRowByWordAndGetResponse(string word) {
            User_dic row = new User_dic();
            row.word = word;
            return row.Delete() ? "#done" : "#not deleted";   
        }
    }  
    public class CommandSetPriority : AConsoleCommand {
        public CommandSetPriority()
            : base(".priority","set priority call for word") {
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if (param.Count!=2)
                return "#ivalid number of args";
            User_dic row = new User_dic();
            row.word = param[0];
            if(!row.SelectByWord())
                return "#word not found";
            if(!row.Delete())
                return "#something wrong :(";
            if( !this.TrySetPriorForRow(row , param[1]))
                return "#invalid arg priority";
            return row.Insert()?"#done":"#something wrong :(";
        }
        private bool TrySetPriorForRow(User_dic row,string prior) {
            int _prior;
            int.TryParse(prior,out _prior);
            if(_prior < 0 || _prior.ToString() == "")
                return false;
            row.priority = (uint)_prior;
            return true;
        }

    } 
    // command for Shell
    public class CommandQuitShell : AConsoleCommand {
        ShellPromt Shell;
        public CommandQuitShell(ShellPromt shell)
            : base(".quit","close command promt") {
            this.Shell = shell;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            return QuitFromShellAndGetResponse();
        }
        private string QuitFromShellAndGetResponse() {
            Shell.Print("#aborted");
            System.Threading.Thread.Sleep(1000);
            Shell.Dispose();
            return "";
        }
    }
    public class CommandHelp : AConsoleCommand {
        CommandStore store;
        public CommandHelp(CommandStore store)
            : base(".help","commands guide") {
                this.store = store;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count != 0)
                return @"#to many args";
            return this.GetCommandsAndDescription();
        }
        private string GetCommandsAndDescription() {
            string response="";
            foreach(var item in store.GetAllCommandsAndDescription()) {
                response = response + item.ToString() + "\n";
            }
            return response;   
        }
    } 
}



