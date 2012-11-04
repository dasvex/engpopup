using System.Runtime.InteropServices;
using System;
namespace Shell {
    public class EventConsoleArgs:EventArgs{
        public string UserInput{get;private set;}
        internal EventConsoleArgs(string input){
            this.UserInput=input;
        }
    }
    public class ShellPromt:System.IDisposable { //client
        #region dllImport
        [DllImport("kernel32.dll" , SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();
        #endregion
        public event EventHandler<EventConsoleArgs> UserInput;
        private bool ConsoleIsAllocate; 

        public ShellPromt() {
            this.ConsoleIsAllocate = false;
        }
        public void Run() {
            RunConsole();
        }
        public void Print(string text) {
            System.Console.WriteLine(text);
        }
        public void Dispose() {
            try {
                if(this.ConsoleIsAllocate == true)
                    FreeConsole();
                this.ConsoleIsAllocate = false;
            } catch(Exception) {  //ваше говно
            }
        }

        private void RunConsole()
        { 
            if (AllocConsole()){
                this.ConsoleIsAllocate = true;
                this.WaitingCommand();
            }
            this.Dispose();
        }
        private void WaitingCommand() {
            this.UserInput(this,new EventConsoleArgs(".help"));
            while(true) {
                try {
                    string input=System.Console.ReadLine();
                    if(UserInput == null)
                        break;
                    this.UserInput(this,new EventConsoleArgs(input));
                } catch(System.IO.IOException) {
                    break;
                } 
            }
        }
    }
}

    

