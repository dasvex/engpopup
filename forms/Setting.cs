using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shell;

namespace EngPopup.forms {
    public partial class Setting : Form {
        private WordSelector.WordSelector selector;
        public Setting(WordSelector.WordSelector selector) {
            this.selector = selector;
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender,EventArgs e) {

        }

        private void textBox3_TextChanged(object sender,EventArgs e) {

        }

        private void button1_Click(object sender,EventArgs e) {
            try {
                AConsoleCommand command = new CommandChangeDistribution(selector);
                command.ExecuteAndGetResponse(new CommandParser.ImputCommand(" " + LeftBorderTextBox.Text + " " + RightBorderTextBox.Text + " " + MedianTextBox.Text));
            } catch(Exception e1) {
                MessageBox.Show(e1.Message);
            }
        }
    }
}
