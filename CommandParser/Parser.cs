using System.Text.RegularExpressions;

namespace CommandParser {
    public class ImputCommand {
        private MatchCollection Params;
        private string CommandNamePattern = @"^\.\S+";
        private string ParamsNamePattern  = @"(?<=\s)\S+(?=\s*)";

        public string CallNameCommand {
            get;
            set;
        }
        public int Count {
            get;
            private set;
        }
        
        public ImputCommand(string input) {
            CallNameCommand = ParseCommand(input);
            Params = ParseValue(input);
            Count  = Params.Count;
        }
        public string this[int index] {
            get {
                try {
                    return Params[index].Value;
                } catch(System.ArgumentOutOfRangeException) {
                    System.Windows.Forms.MessageBox.Show("out of range");
                    throw;
                }
            }
        }
     
        private MatchCollection GetMatch(string pattern,string text) {
            Regex  _regx = new Regex(pattern,RegexOptions.Singleline);
            return _regx.Matches(text);
        }
        private string ParseCommand(string text) {
            MatchCollection _match = GetMatch(CommandNamePattern,text);
            return _match.Count > 0 ? _match[0].Value.Trim() : "#error parse";
        }
        private MatchCollection ParseValue(string text) {
            MatchCollection _match = GetMatch(ParamsNamePattern,text);
            return _match;
        }
    }
}
