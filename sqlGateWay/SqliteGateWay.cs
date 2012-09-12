using System.Data.SQLite;
using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

//todo тру юзер эксепшионы , различные методы поиска селект

namespace SqliteGateWay {

    /*internal static class SqliteGateWay {
        private class BaseGateWay : IDisposable {
            private SQLiteConnection connection;
            private SQLiteCommand command;
            public BaseGateWay(string DBName) {
                connection = new SQLiteConnection();
                connection.ConnectionString = @"Data Source=" + DBName;
                connection.Open();
                command = new SQLiteCommand(connection);
                command.CommandType = CommandType.Text;
            }
            public string QueryText {
                get {
                    return command.CommandText;
                }
                set {
                    command.CommandText = value;
                }
            }
            public void ExecuteNonQuery() {
                command.ExecuteNonQuery();
            }
            public SQLiteDataReader ExecuteQuery() {
                return command.ExecuteReader();
            }
            public void Dispose() {
                command.Dispose();
                connection.Dispose();
            }
        }
        private static BaseGateWay  GateWay;
        public static string QueryText {
            get {
                return GateWay.QueryText;
            }
            set {
                GateWay.QueryText = value;
            }
        }
        private const  string BaseFile =  @"G:\eng popup\EngPopup\bin\Debug\diction.lite";
        public static void ExecuteNonQuery() {
            using(GateWay = new BaseGateWay(BaseFile)) {
                GateWay.ExecuteNonQuery();
            }
        }
        public static SQLiteDataReader ExecuteQuery() {
            using(GateWay = new BaseGateWay(BaseFile)) {
                return GateWay.ExecuteQuery();
            }
        }
    */
    public abstract class DictionRecord {
        private const  string BaseFile =  @"G:\eng popup\EngPopup\bin\Debug\diction.lite";
        private class BaseGateWay : IDisposable {
            private SQLiteConnection connection;
            private SQLiteCommand command;
            //private SQLiteTransaction transaction;
            public BaseGateWay(string DBName) {
                connection = new SQLiteConnection();
                connection.ConnectionString = @"Data Source=" + DBName;
                connection.Open();
                //this.transaction = connection.BeginTransaction();
                //command.Transaction = this.transaction;
                command = new SQLiteCommand(connection);
                command.CommandType = CommandType.Text;
            }
            public string QueryText {
                get {
                    return command.CommandText;
                }
                set {
                    command.CommandText = value;
                }
            }
            public void ExecuteNonQuery() {
                try {
                    command.ExecuteNonQuery();
                } catch(Exception) {
                    throw;// transaction.Rollback();
                }
            }
            public NameValueCollection ExecuteQuery() {
                try {
                    return command.ExecuteReader().GetValues();
                } catch(Exception) {
                    //transaction.Rollback();
                    return new NameValueCollection();
                }
            }
            public void Dispose() {
                //transaction.Dispose();
                command.Dispose();
                connection.Dispose();
            }
        }
        private    BaseGateWay  GateWay;
        protected string QueryText {
            get;
            set;
        }
        protected void ExecuteNonQuery() {
            using(GateWay = new BaseGateWay(BaseFile)) {
                GateWay.QueryText = this.QueryText;
                GateWay.ExecuteNonQuery();
            }
        }
        protected NameValueCollection ExecuteQuery() {
            using(GateWay = new BaseGateWay(BaseFile)) {
                GateWay.QueryText = this.QueryText;
                return GateWay.ExecuteQuery();
            }
        }
        protected abstract void SetRecord(NameValueCollection record);
        public abstract string TableName {
            get;
        }
        public abstract void Insert();
        public abstract void Delete();
        public abstract void Select();
    }

    public class Standart_2500Record : DictionRecord {
        #region property
        public int id {
            get;
            set;
        }
        public string word {
            get;
            set;
        }
        public string trans {
            get;
            set;
        }
        public string freq {
            get;
            private set;
        }
        public int call {
            get;
            private set;
        }
        public uint priority {
            get;
            set;
        }
        #endregion
        public Standart_2500Record() {
            freq = "null";
        }
        private const string Table=@"Standart_2500";
        public override string TableName {
            get {
                return Table;
            }
        }
        public override void Insert() {
            QueryText = "insert into " + TableName + " values (null,'" + word + "','" + trans + "','" + freq + "', 0 ," + priority + ")";
            ExecuteNonQuery();
        }
        public override void Delete() {
            QueryText = "delete from " + TableName + " where word='" + word + "'";
            ExecuteNonQuery();
        }
        public override void Select() {
            QueryText = "select * from " + TableName + " where id=" + id;
            SetRecord(ExecuteQuery());
        }
        public void SelectByWord() {
            QueryText = "select * from " + TableName + " where word='" + word+"'";
            SetRecord(ExecuteQuery());
        }
        protected override void SetRecord(NameValueCollection reader) {
            id = Convert.ToInt32(reader[0]);
            word = Convert.ToString(reader[1]);
            trans = Convert.ToString(reader[2]);
            freq = Convert.ToString(reader[3]);
            call = Convert.ToInt32(reader[4]);
            if(Convert.ToString(reader[5]) == "") {
                priority = 0;
            } else {
                priority = Convert.ToUInt32(reader[5]);
            }
        }
    }
    public class User_dic : DictionRecord {
        #region property
        public int id {
            get;
            set;
        }
        public string word {
            get;
            set;
        }
        public string trans {
            get;
            set;
        }
        public int call {
            get;
            private set;
        }
        public uint priority {
            get;
            set;
        }
        #endregion
        private const string Table=@"user_dic";
        public override string TableName {
            get {
                return Table;
            }
        }
        public override void Insert() {
            QueryText = "insert into " + TableName + " values (null,'" + word + "','" + trans + "', 0 ," + priority + ")";
            ExecuteNonQuery();
        }
        public override void Delete() {
            QueryText = "delete from " + TableName + " where word='" + word + "'";
            ExecuteNonQuery();
        }
        public override void Select() {
            QueryText = "select * from " + TableName + " where id=" + id;
            SetRecord(ExecuteQuery());
        }
        public void SelectByWord() {
            QueryText = "select * from " + TableName + " where word='" + word + "'";
            SetRecord(ExecuteQuery());
        }
        protected override void SetRecord(NameValueCollection reader) {
            id = Convert.ToInt32(reader[0]);
            word = Convert.ToString(reader[1]);
            trans = Convert.ToString(reader[2]);
            call = Convert.ToInt32(reader[3]);
            if(Convert.ToString(reader[4]) == "") {
                priority = 0;
            } else {
                priority = Convert.ToUInt32(reader[5]);
            }
        }
    }
}