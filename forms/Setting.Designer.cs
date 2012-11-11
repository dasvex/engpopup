namespace EngPopup.forms {
    partial class Setting {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.LeftBorderTextBox = new System.Windows.Forms.TextBox();
            this.MedianTextBox = new System.Windows.Forms.TextBox();
            this.RightBorderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(217, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LeftBorderTextBox
            // 
            this.LeftBorderTextBox.Location = new System.Drawing.Point(12, 12);
            this.LeftBorderTextBox.Name = "LeftBorderTextBox";
            this.LeftBorderTextBox.Size = new System.Drawing.Size(92, 20);
            this.LeftBorderTextBox.TabIndex = 1;
            // 
            // MedianTextBox
            // 
            this.MedianTextBox.Location = new System.Drawing.Point(110, 12);
            this.MedianTextBox.Name = "MedianTextBox";
            this.MedianTextBox.Size = new System.Drawing.Size(76, 20);
            this.MedianTextBox.TabIndex = 2;
            this.MedianTextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // RightBorderTextBox
            // 
            this.RightBorderTextBox.Location = new System.Drawing.Point(192, 12);
            this.RightBorderTextBox.Name = "RightBorderTextBox";
            this.RightBorderTextBox.Size = new System.Drawing.Size(100, 20);
            this.RightBorderTextBox.TabIndex = 3;
            this.RightBorderTextBox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Left Border";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Median";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Right Border";
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 87);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RightBorderTextBox);
            this.Controls.Add(this.MedianTextBox);
            this.Controls.Add(this.LeftBorderTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Setting";
            this.Text = "Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox LeftBorderTextBox;
        private System.Windows.Forms.TextBox MedianTextBox;
        private System.Windows.Forms.TextBox RightBorderTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}