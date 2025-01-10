namespace LoveStringH
{
    partial class FormRegex
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.cb_savedRegex = new System.Windows.Forms.ComboBox();
            this.b_saveRegex = new System.Windows.Forms.Button();
            this.tb_regex = new System.Windows.Forms.TextBox();
            this.tb_repl = new System.Windows.Forms.TextBox();
            this.b_findNext = new System.Windows.Forms.Button();
            this.b_replace = new System.Windows.Forms.Button();
            this.b_replaceAll = new System.Windows.Forms.Button();
            this.lb_msgRegex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_savedRegex
            // 
            this.cb_savedRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_savedRegex.DisplayMember = "regex";
            this.cb_savedRegex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_savedRegex.FormattingEnabled = true;
            this.cb_savedRegex.Location = new System.Drawing.Point(10, 12);
            this.cb_savedRegex.Name = "cb_savedRegex";
            this.cb_savedRegex.Size = new System.Drawing.Size(470, 26);
            this.cb_savedRegex.TabIndex = 0;
            // 
            // b_saveRegex
            // 
            this.b_saveRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_saveRegex.Location = new System.Drawing.Point(486, 7);
            this.b_saveRegex.Name = "b_saveRegex";
            this.b_saveRegex.Size = new System.Drawing.Size(100, 34);
            this.b_saveRegex.TabIndex = 1;
            this.b_saveRegex.Text = "Save";
            this.b_saveRegex.UseVisualStyleBackColor = true;
            this.b_saveRegex.Click += new System.EventHandler(this.b_saveRegex_Click);
            // 
            // tb_regex
            // 
            this.tb_regex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_regex.Location = new System.Drawing.Point(11, 45);
            this.tb_regex.Name = "tb_regex";
            this.tb_regex.Size = new System.Drawing.Size(575, 28);
            this.tb_regex.TabIndex = 2;
            this.tb_regex.Text = "(regex)";
            this.tb_regex.TextChanged += new System.EventHandler(this.tb_regex_TextChanged);
            // 
            // tb_repl
            // 
            this.tb_repl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_repl.Location = new System.Drawing.Point(11, 80);
            this.tb_repl.Name = "tb_repl";
            this.tb_repl.Size = new System.Drawing.Size(575, 28);
            this.tb_repl.TabIndex = 3;
            this.tb_repl.Text = "(replace)";
            this.tb_repl.TextChanged += new System.EventHandler(this.tb_regex_TextChanged);
            // 
            // b_findNext
            // 
            this.b_findNext.Location = new System.Drawing.Point(11, 115);
            this.b_findNext.Name = "b_findNext";
            this.b_findNext.Size = new System.Drawing.Size(100, 34);
            this.b_findNext.TabIndex = 5;
            this.b_findNext.Text = "Find next";
            this.b_findNext.UseVisualStyleBackColor = true;
            this.b_findNext.Click += new System.EventHandler(this.b_findNext_Click);
            // 
            // b_replace
            // 
            this.b_replace.Location = new System.Drawing.Point(117, 115);
            this.b_replace.Name = "b_replace";
            this.b_replace.Size = new System.Drawing.Size(100, 34);
            this.b_replace.TabIndex = 6;
            this.b_replace.Text = "Replace";
            this.b_replace.UseVisualStyleBackColor = true;
            this.b_replace.Click += new System.EventHandler(this.b_replace_Click);
            // 
            // b_replaceAll
            // 
            this.b_replaceAll.Location = new System.Drawing.Point(223, 115);
            this.b_replaceAll.Name = "b_replaceAll";
            this.b_replaceAll.Size = new System.Drawing.Size(130, 34);
            this.b_replaceAll.TabIndex = 7;
            this.b_replaceAll.Text = "Replace all";
            this.b_replaceAll.UseVisualStyleBackColor = true;
            this.b_replaceAll.Click += new System.EventHandler(this.b_replaceAll_Click);
            // 
            // lb_msgRegex
            // 
            this.lb_msgRegex.AutoSize = true;
            this.lb_msgRegex.Location = new System.Drawing.Point(359, 123);
            this.lb_msgRegex.Name = "lb_msgRegex";
            this.lb_msgRegex.Size = new System.Drawing.Size(0, 18);
            this.lb_msgRegex.TabIndex = 8;
            // 
            // FormRegex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 154);
            this.ControlBox = false;
            this.Controls.Add(this.tb_regex);
            this.Controls.Add(this.b_saveRegex);
            this.Controls.Add(this.cb_savedRegex);
            this.Controls.Add(this.tb_repl);
            this.Controls.Add(this.b_findNext);
            this.Controls.Add(this.b_replaceAll);
            this.Controls.Add(this.b_replace);
            this.Controls.Add(this.lb_msgRegex);
            this.Name = "FormRegex";
            this.Text = "Regular expression";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.TextBox tb_regex;
        private System.Windows.Forms.Button b_saveRegex;
        private System.Windows.Forms.ComboBox cb_savedRegex;
        private System.Windows.Forms.TextBox tb_repl;
        private System.Windows.Forms.Button b_findNext;
        private System.Windows.Forms.Button b_replaceAll;
        private System.Windows.Forms.Button b_replace;
        private System.Windows.Forms.Label lb_msgRegex;
    }
}