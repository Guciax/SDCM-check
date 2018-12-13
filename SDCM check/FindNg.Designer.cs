namespace SDCM_check
{
    partial class FindNg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelNg = new System.Windows.Forms.Panel();
            this.panelOk = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkedListBoxOK = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxNG = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelNgCount = new System.Windows.Forms.Label();
            this.labelOkCount = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panelNg.SuspendLayout();
            this.panelOk.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 57);
            this.panel1.TabIndex = 2;
            // 
            // panelNg
            // 
            this.panelNg.BackColor = System.Drawing.Color.White;
            this.panelNg.Controls.Add(this.labelNgCount);
            this.panelNg.Controls.Add(this.label1);
            this.panelNg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNg.Location = new System.Drawing.Point(403, 3);
            this.panelNg.Name = "panelNg";
            this.panelNg.Size = new System.Drawing.Size(394, 74);
            this.panelNg.TabIndex = 4;
            // 
            // panelOk
            // 
            this.panelOk.BackColor = System.Drawing.Color.White;
            this.panelOk.Controls.Add(this.label2);
            this.panelOk.Controls.Add(this.labelOkCount);
            this.panelOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOk.Location = new System.Drawing.Point(3, 3);
            this.panelOk.Name = "panelOk";
            this.panelOk.Size = new System.Drawing.Size(394, 74);
            this.panelOk.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(189, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "NG";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.checkedListBoxNG, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelOk, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelNg, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkedListBoxOK, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 57);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 393);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // checkedListBoxOK
            // 
            this.checkedListBoxOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxOK.FormattingEnabled = true;
            this.checkedListBoxOK.Location = new System.Drawing.Point(3, 83);
            this.checkedListBoxOK.MultiColumn = true;
            this.checkedListBoxOK.Name = "checkedListBoxOK";
            this.checkedListBoxOK.Size = new System.Drawing.Size(394, 307);
            this.checkedListBoxOK.TabIndex = 5;
            this.checkedListBoxOK.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxOK_ItemCheck);
            // 
            // checkedListBoxNG
            // 
            this.checkedListBoxNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxNG.FormattingEnabled = true;
            this.checkedListBoxNG.Location = new System.Drawing.Point(403, 83);
            this.checkedListBoxNG.MultiColumn = true;
            this.checkedListBoxNG.Name = "checkedListBoxNG";
            this.checkedListBoxNG.Size = new System.Drawing.Size(394, 307);
            this.checkedListBoxNG.TabIndex = 6;
            this.checkedListBoxNG.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxNG_ItemCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(155, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "OK";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(739, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelNgCount
            // 
            this.labelNgCount.AutoSize = true;
            this.labelNgCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNgCount.Location = new System.Drawing.Point(167, 42);
            this.labelNgCount.Name = "labelNgCount";
            this.labelNgCount.Size = new System.Drawing.Size(29, 31);
            this.labelNgCount.TabIndex = 1;
            this.labelNgCount.Text = "0";
            // 
            // labelOkCount
            // 
            this.labelOkCount.AutoSize = true;
            this.labelOkCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOkCount.Location = new System.Drawing.Point(138, 39);
            this.labelOkCount.Name = "labelOkCount";
            this.labelOkCount.Size = new System.Drawing.Size(29, 31);
            this.labelOkCount.TabIndex = 2;
            this.labelOkCount.Text = "0";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(25, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(408, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FindNg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "FindNg";
            this.Text = "FindNg";
            this.Load += new System.EventHandler(this.FindNg_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelNg.ResumeLayout(false);
            this.panelNg.PerformLayout();
            this.panelOk.ResumeLayout(false);
            this.panelOk.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelNg;
        private System.Windows.Forms.Panel panelOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckedListBox checkedListBoxNG;
        private System.Windows.Forms.CheckedListBox checkedListBoxOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelOkCount;
        private System.Windows.Forms.Label labelNgCount;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
    }
}