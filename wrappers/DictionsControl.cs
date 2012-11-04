using StringCollection = System.Collections.Specialized.StringCollection;
using StringEnumerator = System.Collections.Specialized.StringEnumerator;

namespace EngPopup {
    public class DictionsControl  {
        StringCollection AvalibleDictionsList;
        StringCollection UsingDictionsList = new StringCollection();

        public DictionsControl(StringCollection dictionList){
            this.AvalibleDictionsList = dictionList;
        }
        public bool ContainsInAvalibleDictions(string key) {
            return AvalibleDictionsList.Contains(key);
        }
        public bool ContainsInUsingDictions(string diction) {
            return UsingDictionsList.Contains(diction);
        }
        public int AddInUsingDictions(string diction) {
            if(this.ContainsInAvalibleDictions(diction))
                return UsingDictionsList.Add(diction);
            return 0;
        }
        public int AddInAvalibleDictions(string diction) {
            return AvalibleDictionsList.Add(diction);
        }
        public void RemoveUsingDictions(string diction) {
            UsingDictionsList.Remove(diction);
        }
        public void RemoveAvalibleDictions(string diction){
            AvalibleDictionsList.Remove(diction);
        }
        public System.Collections.IEnumerable GetAvalibleDictions() {
            foreach(var item in AvalibleDictionsList) 
                yield return  item;
        }
        public System.Collections.IEnumerable GetUsingdictions() {
            foreach(var item in UsingDictionsList)
                yield return item;
        }
    }
}
