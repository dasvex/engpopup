using System.Collections.Generic;
using CommandParser;

namespace Shell {
    public class CommandStore { //invoker
        private Dictionary<string , AConsoleCommand> comands;
        public CommandStore() {
            comands = new Dictionary<string,AConsoleCommand>();
        }
        public void Add(AConsoleCommand comand) {
            comands.Add(comand.CallName,comand);
        }
        public void Remove(string key) {
            comands.Remove(key);
        }
        public System.Collections.IEnumerable GetAllComands() {
            foreach(KeyValuePair<string , AConsoleCommand> item in comands) 
                yield return item.Key;
        }
        public System.Collections.IEnumerable GetAllDescriptions() {
            foreach(KeyValuePair<string , AConsoleCommand> item in comands)
                yield return item.Value.Description;
        }
        public string Execute(string command,ImputCommand param) {
            try {
                return comands[command].Execute(param);
            } catch(KeyNotFoundException) {
                return "Command not found";
            } catch(System.Exception e) {
                return "Undefined error :" + e.Message + "\n.Source :" + e.Source;
            }
        }
    }
}
