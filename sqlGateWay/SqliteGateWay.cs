using System.Data.SQLite;
using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

//todo тру юзер эксепшионы , различные методы поиска селект


namespace SqliteGateWay {
<<<<<<< HEAD
=======

>>>>>>> gateway comlite
    public abstract class DictionRecord {
        private const  string BaseFile =  @"G:\eng popup\EngPopup\bin\Debug\diction.lite";
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
                    return new NameValueCollection();
                }
            }
            public void Dispose() {
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
        protected abstract bool SetRecord(NameValueCollection record);
        protected string SqlLiteToString(string value) {
            return Convert.ToString(value);
        }
        protected int SqlLiteToInt(string value) {
            return value == "" ? 0 : Convert.ToInt32(value);
        }
        public abstract string TableName {
            get;
        }
        public abstract bool Insert();
        public abstract bool Delete();
        public abstract bool Select();


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
            this.id = 0;
            this.word = "";
            this.trans = "";
            this.freq = "null";
            this.call = 0;
            this.priority = 0;
        }
        private const string Table=@"Standart_2500";
        public override string TableName {
            get {
                return Table;
            }
        }
        public override bool Insert() {
            if(!this.IsAvalibleInsert())
                return false;
            QueryText = "insert into " + TableName + " values (null,'" + word + "','" + trans + "','" + freq + "', 0 ," + priority + ")";
            ExecuteNonQuery();
            return true;
        }
        public override bool Delete() {
            if(!this.IsAvalibleDelete())
                return false;
            QueryText = "delete from " + TableName + " where word='" + word + "'";
            ExecuteNonQuery();
            return true;
        }
        public override bool Select() {
            if(!this.IsAvalibleSelect())
                return false;
            QueryText = "select * from " + TableName + " where id=" + id;
            return SetRecord(ExecuteQuery());
        }
        public bool SelectByWord() {
            if(!this.IsAvalibleSelectByWord())
                return false;
            QueryText = "select * from " + TableName + " where word='" + word + "'";
            return SetRecord(ExecuteQuery());
        }
        protected override bool SetRecord(NameValueCollection reader) {
            if(!this.IsAvalibeSetRecord(reader))
                return false;
            id = this.SqlLiteToInt(reader[0]);
            word = this.SqlLiteToString(reader[1]);
            trans = this.SqlLiteToString(reader[2]);
            freq = this.SqlLiteToString(reader[3]);
            call = this.SqlLiteToInt(reader[4]);
            priority = (uint)this.SqlLiteToInt(reader[5]);
            return true;
        }
        private bool IsAvalibeSetRecord(NameValueCollection reader) {
            return reader[0].ToString() == "" ? false : true;
        }
        private bool IsAvalibleSelectByWord() {
            return word == "" ? false : true;
        }
        private bool IsAvalibleSelect() {
            return id == 0 ? false : true;
        }
        private bool IsAvalibleDelete() {
            return true;
        }
        private bool IsAvalibleInsert() {
            return (this.word != "" && this.trans != "") ? true : false;
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
        public User_dic() {
            this.id = 0;
            this.word = "";
            this.trans = "";
            this.call = 0;
            this.priority = 0;
        }
        public override string TableName {
            get {
                return Table;
            }
        }
        public override bool Insert() {
            if(!this.IsAvalibleInsert())
                return false;
            QueryText = "insert into " + TableName + " values (null,'" + word + "','" + trans + "', 0 ," + priority + ")";
            ExecuteNonQuery();
            return true;
        }
        public override bool Delete() {
            if(!this.IsAvalibleDelete())
                return false;
            QueryText = "delete from " + TableName + " where word='" + word + "'";
            ExecuteNonQuery();
            return true;
        }
        public override bool Select() {
            if(!IsAvalibleSelect())
                return false;
            QueryText = "select * from " + TableName + " where id=" + id;
            return SetRecord(ExecuteQuery());
        }
        public bool SelectByWord() {
            if(!this.IsAvalibleSelectByWord())
                return false;
            QueryText = "select * from " + TableName + " where word='" + word + "'";
            return SetRecord(ExecuteQuery());
        }
        protected override bool SetRecord(NameValueCollection reader) {
            if(!this.IsAvalibeSetRecord(reader))
                return false;
            id = this.SqlLiteToInt(reader[0]);
            word = this.SqlLiteToString(reader[1]);
            trans = this.SqlLiteToString(reader[2]);
            call = this.SqlLiteToInt(reader[3]);
            priority = (uint)this.SqlLiteToInt(reader[4]);
            return true;
        }

        private bool IsAvalibeSetRecord(NameValueCollection reader) {
            return reader[0].ToString() == "" ? false : true;
        }
        private bool IsAvalibleSelectByWord() {
            return word == "" ? false : true;
        }
        private bool IsAvalibleSelect() {
            return id == 0 ? false : true;
        }
        private bool IsAvalibleDelete() {
            return true;
        }
        private bool IsAvalibleInsert() {
            return (this.word != "" && this.trans != "") ? true : false;
        }
    }
}


