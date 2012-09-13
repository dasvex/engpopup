using System.Runtime.InteropServices;
using ElapsedTimer;
using System;

namespace Shell {
    public class ShellPromt:System.IDisposable { //client
        #region dllImport
        [DllImport("kernel32.dll" , SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();
        #endregion
        private int MaxCallNameLength {
            get;
            set;
        }
        private CommandStore commands;
        private TimerControl timer;

        public void Dispose() {
            FreeConsole();
        }
        public ShellPromt(TimerControl timer) {
            this.MaxCallNameLength = 20;
            this.timer = timer;
            this.commands = new CommandStore();
            commands.Add(new CommandTimer    (timer));
            commands.Add(new CommandStop        (timer));
            commands.Add(new CommandCallPopup   (timer));
            commands.Add(new CommandStart       (timer));
            commands.Add(new CommandHelp        (this));
            commands.Add(new CommandQuitShell   (this));
            commands.Add(new CommandSelectWord  ());
            commands.Add(new CommandInsertWord  ());
            commands.Add(new CommandDeleteWord  ());
            commands.Add(new CommandSetPriority ());
            RunConsole();
        }
        public void Print(string text) {
            System.Console.WriteLine(text);
        }
        public System.Collections.IEnumerable AvalibleCommands() {
            var _commands = commands.GetAllComands().GetEnumerator();
            var _descriptions = commands.GetAllDescriptions().GetEnumerator();
            string _spaces="";
            while(_commands.MoveNext() && _descriptions.MoveNext()){
                for(int _space_num = MaxCallNameLength - _commands.Current.ToString().Length  ; _space_num > 0; --_space_num)
                    _spaces = _spaces + " ";
                yield return Convert.ToString(_commands.Current) +  _spaces + ">>>" + Convert.ToString(_descriptions.Current);
                _spaces = "";
            }
        }
        private void RunConsole() {
            if (AllocConsole()){
                this.WaitingCommand();
            }
        }
        private void WaitingCommand() {
            while(true) {
                try {
                    CommandParser.ImputCommand input = new  CommandParser.ImputCommand(System.Console.ReadLine());
                    Print (commands.Execute(input.CallNameCommand,input) );
                } catch(System.IO.IOException) {
                    break;
                } 
            }
        }
    }
}



