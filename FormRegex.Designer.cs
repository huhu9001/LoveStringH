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
            cb_savedRegex = new ComboBox();
            b_saveRegex = new Button();
            tb_regex = new TextBox();
            tb_repl = new TextBox();
            b_findNext = new Button();
            b_replace = new Button();
            b_replaceAll = new Button();
            lb_msgRegex = new Label();
            SuspendLayout();
            // 
            // cb_savedRegex
            // 
            cb_savedRegex.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_savedRegex.DisplayMember = "regex";
            cb_savedRegex.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_savedRegex.FormattingEnabled = true;
            cb_savedRegex.Location = new Point(12, 16);
            cb_savedRegex.Margin = new Padding(4, 4, 4, 4);
            cb_savedRegex.Name = "cb_savedRegex";
            cb_savedRegex.Size = new Size(574, 32);
            cb_savedRegex.TabIndex = 0;
            // 
            // b_saveRegex
            // 
            b_saveRegex.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            b_saveRegex.Location = new Point(594, 9);
            b_saveRegex.Margin = new Padding(4, 4, 4, 4);
            b_saveRegex.Name = "b_saveRegex";
            b_saveRegex.Size = new Size(122, 45);
            b_saveRegex.TabIndex = 1;
            b_saveRegex.Text = "Save";
            b_saveRegex.UseVisualStyleBackColor = true;
            b_saveRegex.Click += b_saveRegex_Click;
            // 
            // tb_regex
            // 
            tb_regex.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_regex.Location = new Point(13, 60);
            tb_regex.Margin = new Padding(4, 4, 4, 4);
            tb_regex.Name = "tb_regex";
            tb_regex.Size = new Size(702, 30);
            tb_regex.TabIndex = 2;
            tb_regex.Text = "(regex)";
            tb_regex.TextChanged += tb_regex_TextChanged;
            // 
            // tb_repl
            // 
            tb_repl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_repl.Location = new Point(13, 107);
            tb_repl.Margin = new Padding(4, 4, 4, 4);
            tb_repl.Name = "tb_repl";
            tb_repl.Size = new Size(702, 30);
            tb_repl.TabIndex = 3;
            tb_repl.Text = "(replace)";
            tb_repl.TextChanged += tb_regex_TextChanged;
            // 
            // b_findNext
            // 
            b_findNext.Location = new Point(13, 153);
            b_findNext.Margin = new Padding(4, 4, 4, 4);
            b_findNext.Name = "b_findNext";
            b_findNext.Size = new Size(122, 45);
            b_findNext.TabIndex = 5;
            b_findNext.Text = "Find next";
            b_findNext.UseVisualStyleBackColor = true;
            b_findNext.Click += b_findNext_Click;
            // 
            // b_replace
            // 
            b_replace.Location = new Point(143, 153);
            b_replace.Margin = new Padding(4, 4, 4, 4);
            b_replace.Name = "b_replace";
            b_replace.Size = new Size(122, 45);
            b_replace.TabIndex = 6;
            b_replace.Text = "Replace";
            b_replace.UseVisualStyleBackColor = true;
            b_replace.Click += b_replace_Click;
            // 
            // b_replaceAll
            // 
            b_replaceAll.Location = new Point(273, 153);
            b_replaceAll.Margin = new Padding(4, 4, 4, 4);
            b_replaceAll.Name = "b_replaceAll";
            b_replaceAll.Size = new Size(159, 45);
            b_replaceAll.TabIndex = 7;
            b_replaceAll.Text = "Replace all";
            b_replaceAll.UseVisualStyleBackColor = true;
            b_replaceAll.Click += b_replaceAll_Click;
            // 
            // lb_msgRegex
            // 
            lb_msgRegex.AutoSize = true;
            lb_msgRegex.Location = new Point(439, 164);
            lb_msgRegex.Margin = new Padding(4, 0, 4, 0);
            lb_msgRegex.Name = "lb_msgRegex";
            lb_msgRegex.Size = new Size(0, 24);
            lb_msgRegex.TabIndex = 8;
            // 
            // FormRegex
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(731, 205);
            ControlBox = false;
            Controls.Add(tb_regex);
            Controls.Add(b_saveRegex);
            Controls.Add(cb_savedRegex);
            Controls.Add(tb_repl);
            Controls.Add(b_findNext);
            Controls.Add(b_replaceAll);
            Controls.Add(b_replace);
            Controls.Add(lb_msgRegex);
            Margin = new Padding(4, 4, 4, 4);
            Name = "FormRegex";
            Text = "Regular expression";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
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