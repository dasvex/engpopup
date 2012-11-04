using ElapsedTimer;
using CommandParser;
using SqliteGateWay;
//все сообщения надо бы в кучку . толи эксепшинами толи хмл
//команды работы с базой кривые особенно ремарка

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
            bool _timerStatus = this.Timer.Enabled;
            this.Timer.Stop();
            this.Timer.Interval = _interval;
            this.Timer.Start();
            this.Timer.Enabled = _timerStatus;
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
    public class CommandUsingDictions : AConsoleCommand {
        private EngPopup.DictionsControl dictions;
        public CommandUsingDictions(EngPopup.DictionsControl dictions)
            : base(".using","add / get using tables") {
                this.dictions = dictions;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            switch(param.Count) {
                case 0:
                    return this.getUsingDictionList();
                case 1:
                    return this.TrySetDiction(param[0]);
                default:
                    return @"#to many args";
            }
        }
        private string TrySetDiction(string p) {
            if(!dictions.ContainsInAvalibleDictions(p))
                return "#diction not found";
            dictions.AddInUsingDictions(p);
            return "#" + p + " added";

        }
        private string getUsingDictionList() {
            string response="";
            foreach(var item in dictions.GetUsingdictions()) {
                response += item+"\n";
            }
            return response;
        }
    }
    public class CommandDisableDictions : AConsoleCommand {
        private EngPopup.DictionsControl dictions;
        public CommandDisableDictions(EngPopup.DictionsControl dictions)
            : base(".dising","get not usings / remove from using tables") {
            this.dictions = dictions;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            switch(param.Count) {
                case 0 :
                    return this.GetNotUsingDictions();
                case 1 :
                    return this.TryDisableDictions(param[0]);
                default:
                    return @"#to many args";
            }
        }
        private string TryDisableDictions(string p) {
            dictions.RemoveUsingDictions(p);
            return @"#removed";
        }
        private string GetNotUsingDictions() {
            string response ="";
            foreach(var item in dictions.GetAvalibleDictions()) {
                if(dictions.ContainsInUsingDictions(item.ToString()))
                    continue;
                response += item + "\n";
            }
            return response;
        }
    }
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
            return response_row == "" ? "#not found" : response_row;
        }
        private string GetRowFromUserDic(string word){
            Record row = new Record(DictionList.userdic);
            row.word = word;
            return row.SelectByWord()?row.word+"  "+row.trans+"  "+row.remark+"  "+row.freq:""; 
        }
        private string GetRowFromStandartDic(string word){
            Record s_row = new Record(DictionList.standart);
            s_row.word = word;
            return s_row.SelectByWord() ? s_row.word + "  " + s_row.remark + "  " + s_row.trans + "" : @"N\A";
        }
    }
    public class CommandInsertWord : AConsoleCommand {
        public CommandInsertWord()
            : base(".insert","insert new word (word , translate , remark* , priority*)") {
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count<=1 || param.Count>4)
                return "#invalid numbers of arg";
            return InsertRowAndGetResponse(param);
        }
        private string InsertRowAndGetResponse(ImputCommand param) {
            Record row = new Record(DictionList.userdic);
            row = this.SetWordForRow(row,param[0]);
            row = this.SetTransForRow(row,param[1]);
            if(param.Count > 2)
                row = this.SetRemarkForRow(row,param[2]);
            if(param.Count > 3) 
                if ( !this.TrySetPriorForRow(row , param[3]) )
                    return "#Invalid priopity";
            return row.Insert() ? "#done" : "#error inserting";
        }
        private Record SetWordForRow(Record row,string word) {
            row.word = word;
            return row;
        }
        private Record SetTransForRow(Record row,string word) {
            row.trans = word;
            return row;
        }
        private Record SetRemarkForRow(Record row,string word) {
            row.remark = word;
            return row;
        }
        private bool TrySetPriorForRow(Record row,string prior) {
            int _prior;
            int.TryParse(prior,out _prior);
            if(_prior < 0 || _prior.ToString() == "")
                return false;
            row.freq = (int)_prior;
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
            Record row = new Record(DictionList.standart);
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
            Record row = new Record(DictionList.standart);
            row.word = param[0];
            if(!row.SelectByWord())
                return "#word not found";
            if(!row.Delete())
                return "#something wrong :(";
            if( !this.TrySetPriorForRow(row , param[1]))
                return "#invalid arg priority";
            return row.Insert()?"#done":"#something wrong :(";
        }
        private bool TrySetPriorForRow(Record row,string prior) {
            int _prior;
            int.TryParse(prior,out _prior);
            if(_prior < 0 || _prior.ToString() == "")
                return false;
            row.freq = (int)_prior;
            return true;
        }

    }
    public class CommandEnableWord : AConsoleCommand {
        public CommandEnableWord()
            : base(".enable","enable word") {
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count!=1)
                return "#ivalid number of args";
            return EnableWord(param[0]);
        }
        private string EnableWord(string p) {
            Record row = new Record(DictionList.standart);
            row.word = p;
            row.EnableWord();
            Record row1 = new Record(DictionList.userdic);
            row.word = p;
            row.EnableWord();
            return p + "#word now avalible";
        }
    }
    public class CommandDisableWord : AConsoleCommand {
        public CommandDisableWord()
            : base(".disable","disable word") {
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count != 1)
                return "#ivalid number of args";
            return DisableWord(param[0]);
        }
        private string DisableWord(string p) {
            Record row = new Record(DictionList.standart);
            row.word = p;
            row.DisableWord();
            Record row1 = new Record(DictionList.userdic);
            row.word = p;
            row.DisableWord();
            return p + "#word now is not avalible";
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
    //command for Popup Window
    public class CommandPopupDalay : AConsoleCommand {
        PopupWindow.PopupControll popup;
        public CommandPopupDalay(PopupWindow.PopupControll popup)
            : base(".dalay","set/get popup window delay") {
                this.popup = popup;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            switch(param.Count) {
                case 0 :
                    return this.GetCurrentPopupDalay();
                case 1:
                    return this.TrySetPopupDelay(param[0]);
                default:
                    return @"#to many args";
            }
        }
        private string TrySetPopupDelay(string delay) {
            int del;
            if(int.TryParse(delay,out del)) {
                popup.Delay = del;
                return "#delay changed";
            }
            return "#invalid delay";
                
        }
        private string GetCurrentPopupDalay() {
            return popup.Delay.ToString();
        }
    }
    //command for Generator
    public class CommandResetRandomSequence : AConsoleCommand {
        private WordSelector.WordSelector selector;
        public CommandResetRandomSequence(WordSelector.WordSelector selector)
            : base(".reset","reset random sequence") {
            this.selector=selector;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count!=0)
                return @"#to many args";
            return ResetGenerator();

        }
        private string ResetGenerator() {
            selector.Reset();
            return "#sequence reseted";
        }
    }
    public class CommandChangeDistribution : AConsoleCommand {
        private WordSelector.WordSelector selector;
        public CommandChangeDistribution(WordSelector.WordSelector selector)
            : base(".distribution","change distribution (left  , right , median)") {
            this.selector = selector;
        }
        public override string ExecuteAndGetResponse(ImputCommand param) {
            if(param.Count != 3)
                return "#ivalid number of args";
            return TryChangeBordersandMedian(param);

        }
        private string TryChangeBordersandMedian(ImputCommand param) {
            int leftBorder;
            int rightBorder;
            int median;
            if(!int.TryParse(param[0],out leftBorder))
                return "#invalid left border";
            if(!int.TryParse(param[1],out rightBorder))
                return "#invelid right border";
            if(!int.TryParse(param[2],out median))
                return "#invalid median";
            try {
                selector.SetBordersAndMedian(leftBorder,rightBorder,median);
            } catch(System.Exception e) {
                return e.Message;
            }
            return "#distribution changed";
        }
    }

}



