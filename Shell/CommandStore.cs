using System.Collections.Generic;
using CommandParser;

namespace Shell {
    public class CommandStore { //invoker
        private Dictionary<string , AConsoleCommand> comands;
        private int DescriptionPosition;
        private string DescriptionPrefix;

        public CommandStore() {
            DescriptionPosition = 20;
            comands = new Dictionary<string,AConsoleCommand>();
            DescriptionPrefix = ">>> ";
        }
        public void Add(AConsoleCommand comand) {
            comands.Add(comand.CallName,comand);
        }
        public void Remove(string key) {
            comands.Remove(key);
        }
        public System.Collections.IEnumerable GetAllCommands() {
            foreach(KeyValuePair<string , AConsoleCommand> item in comands) 
                yield return item.Key;
        }
        public System.Collections.IEnumerable GetAllDescriptions() {
            foreach(KeyValuePair<string , AConsoleCommand> item in comands)
                yield return item.Value.Description;
        }
        public System.Collections.IEnumerable GetAllCommandsAndDescription() {
            var _commands     = GetAllCommands().GetEnumerator();
            var _descriptions = GetAllDescriptions().GetEnumerator();
            string _spaces=" ";
            while(_commands.MoveNext() && _descriptions.MoveNext()) {
                for(int free_spaces = DescriptionPosition - GetLengthName(_commands.Current); free_spaces > 0; --free_spaces)
                    _spaces = _spaces + " ";
                yield return GetName(_commands.Current) + _spaces + DescriptionPrefix + GetName(_descriptions.Current);
                _spaces = " ";
            }            
        }
        public string ExecuteAndGetResponse(ImputCommand param) {
            try {
                return comands[param.CallNameCommand].ExecuteAndGetResponse(param);
            } catch(KeyNotFoundException) {
                return "#Command not found";
            } catch(System.Exception e) {
                return "Undefined error :" + e.Message + "\n.Source :" + e.Source;
            }
        }
        public void Clear() {
            comands.Clear();
        }

        private string GetName(object _obj) {
            return System.Convert.ToString(_obj);
        }
        private int GetLengthName(object _obj) {
            return System.Convert.ToString(_obj).Length;
        }
    }
}
