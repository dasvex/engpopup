using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EngPopup;
using SqliteGateWay;
using Shell;

namespace EngPopup.forms {
    public partial class Dictions : Form {
        private DictionsControl Dictions_;
        private DictionsControl currentDiction;
        private bool loaded = false;
        public Dictions(DictionsControl dictions) {
            this.Dictions_ = dictions;
            currentDiction = new DictionsControl(new DictionInfo().GetAllDictions());
            InitializeComponent();
        }

        private void Dictions_Load(object sender,EventArgs e) {
            DictionInfo info = new DictionInfo();
            foreach(var item in info.GetAllDictions()) {
                dataGridView2.Rows.Add(item.ToString());
                if(Dictions_.ContainsInUsingDictions(item.ToString()))
                    dataGridView2.Rows[dataGridView2.Rows.Count-1].Cells[1].Value=true;
            }
            this.Focus();
            this.loaded = true;
        }

        private void checkBox1_CheckedChanged(object sender,EventArgs e) {

        }

        private void dataGridView1_CellContentClick(object sender,DataGridViewCellEventArgs e) {

        }

        private void splitContainer1_Panel2_Paint(object sender,PaintEventArgs e) {

        }

        private void splitContainer1_Panel1_Paint(object sender,PaintEventArgs e) {

        }

        private void splitContainer1_SplitterMoved(object sender,SplitterEventArgs e) {

        }

        private void dataGridView1_CellContentClick_1(object sender,DataGridViewCellEventArgs e) {
            if(dataGridView1.Columns[e.ColumnIndex].Name.Equals("Enable")) {
                DataGridViewCheckBoxCell checkbox = dataGridView1.Rows[e.RowIndex].Cells["Enable"] as DataGridViewCheckBoxCell;
                AConsoleCommand command;
                if((bool)checkbox.Value)
                    command = new CommandDisableWord(currentDiction);
                else
                    command = new CommandEnableWord(currentDiction);
                command.ExecuteAndGetResponse(new CommandParser.ImputCommand(" " + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value.ToString()));
            }
        }

        private void dataGridView2_CellValueChanged(object sender,DataGridViewCellEventArgs e) {
            if(dataGridView2.Columns[e.ColumnIndex].Name.Equals("EnableDiction")) {
                DataGridViewCheckBoxCell checkbox = dataGridView2.Rows[e.RowIndex].Cells["EnableDiction"] as DataGridViewCheckBoxCell;
                DataGridViewButtonCell button = dataGridView2.Rows[e.RowIndex].Cells["DictionButton"] as DataGridViewButtonCell;
                AConsoleCommand command;
                if(!(bool)checkbox.Value)
                    command = new CommandDisableDictions(Dictions_);
                else
                    command = new CommandUsingDictions(Dictions_);
                command.ExecuteAndGetResponse(new CommandParser.ImputCommand(" "+button.Value.ToString()));
            }
        }

        private void dataGridView2_CellEnter(object sender,DataGridViewCellEventArgs e) {
            if(!this.loaded)
                return;
            if(dataGridView2.Columns[e.ColumnIndex].Name.Equals("DictionButton")) {
                BackgroundWorker work = new BackgroundWorker();
                work.DoWork += new DoWorkEventHandler(work_DoWork);
                work.RunWorkerAsync(e.RowIndex);
                List<string> tmp = new List<string>();
                foreach(var item in currentDiction.GetUsingdictions()) {
                    tmp.Add(item.ToString());
                }
                foreach(var item in tmp) {
                    currentDiction.RemoveUsingDictions(item.ToString());
                }
                currentDiction.AddInUsingDictions(dataGridView2.Rows[e.RowIndex].Cells["DictionButton"].Value.ToString());

            }
        }

        void work_DoWork(object sender,DoWorkEventArgs e) {
            Action act1 = () => dataGridView1.Rows.Clear();
            if(this.InvokeRequired)
                this.Invoke(act1);
            else  act1();
            DataGridViewButtonCell button = dataGridView2.Rows[Convert.ToInt32(e.Argument)].Cells["DictionButton"] as DataGridViewButtonCell;
            Table table = new Table(button.Value.ToString());
            System.Collections.Specialized.NameValueCollection[] rows = table.GetTable();
            foreach(var item in rows) {
                Action act = () =>dataGridView1.Rows.Add(new object[] {
                        Convert.ToBoolean(Convert.ToInt32(item[5])),
                        item[1],
                        item[2],
                        item[3],
                        item[4],
                        item[6]
                    });
                if(InvokeRequired)
                    this.Invoke(act);
                else act();
            }
        }

        private void dataGridView2_CellContentClick(object sender,DataGridViewCellEventArgs e) {

        }
    }
}
