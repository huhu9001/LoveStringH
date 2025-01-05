namespace LoveStringH
{
    partial class LoveStringHForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_main = new System.Windows.Forms.TextBox();
            this.tb_byte = new System.Windows.Forms.TextBox();
            this.cb_escapeStyle = new System.Windows.Forms.ComboBox();
            this.cb_encoding = new System.Windows.Forms.ComboBox();
            this.ch_keepText = new System.Windows.Forms.CheckBox();
            this.nud_fontsize = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cb_transliterator = new System.Windows.Forms.ComboBox();
            this.tb_nonroman = new System.Windows.Forms.TextBox();
            this.tb_roman = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fontsize)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_main
            // 
            this.tb_main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_main.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_main.Location = new System.Drawing.Point(6, 6);
            this.tb_main.Multiline = true;
            this.tb_main.Name = "tb_main";
            this.tb_main.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_main.Size = new System.Drawing.Size(558, 145);
            this.tb_main.TabIndex = 0;
            this.tb_main.TextChanged += new System.EventHandler(this.tb_main_TextChanged);
            // 
            // tb_byte
            // 
            this.tb_byte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_byte.Location = new System.Drawing.Point(7, 185);
            this.tb_byte.Multiline = true;
            this.tb_byte.Name = "tb_byte";
            this.tb_byte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_byte.Size = new System.Drawing.Size(559, 175);
            this.tb_byte.TabIndex = 9;
            this.tb_byte.TextChanged += new System.EventHandler(this.tb_byte_TextChanged);
            // 
            // cb_escapeStyle
            // 
            this.cb_escapeStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_escapeStyle.FormattingEnabled = true;
            this.cb_escapeStyle.Location = new System.Drawing.Point(142, 153);
            this.cb_escapeStyle.Name = "cb_escapeStyle";
            this.cb_escapeStyle.Size = new System.Drawing.Size(70, 26);
            this.cb_escapeStyle.TabIndex = 16;
            this.cb_escapeStyle.SelectedIndexChanged += new System.EventHandler(this.cb_escapeStyle_SelectedIndexChanged);
            // 
            // cb_encoding
            // 
            this.cb_encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_encoding.FormattingEnabled = true;
            this.cb_encoding.Location = new System.Drawing.Point(7, 153);
            this.cb_encoding.Name = "cb_encoding";
            this.cb_encoding.Size = new System.Drawing.Size(129, 26);
            this.cb_encoding.TabIndex = 17;
            this.cb_encoding.SelectedIndexChanged += new System.EventHandler(this.cb_encoding_SelectedIndexChanged);
            // 
            // ch_keepText
            // 
            this.ch_keepText.AutoSize = true;
            this.ch_keepText.Checked = true;
            this.ch_keepText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_keepText.Location = new System.Drawing.Point(218, 157);
            this.ch_keepText.Name = "ch_keepText";
            this.ch_keepText.Size = new System.Drawing.Size(205, 22);
            this.ch_keepText.TabIndex = 18;
            this.ch_keepText.Text = "Keep Text Unchanged";
            this.ch_keepText.UseVisualStyleBackColor = true;
            // 
            // nud_fontsize
            // 
            this.nud_fontsize.DecimalPlaces = 1;
            this.nud_fontsize.Location = new System.Drawing.Point(429, 154);
            this.nud_fontsize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_fontsize.Name = "nud_fontsize";
            this.nud_fontsize.Size = new System.Drawing.Size(69, 28);
            this.nud_fontsize.TabIndex = 19;
            this.nud_fontsize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nud_fontsize.ValueChanged += new System.EventHandler(this.nud_fontsize_ValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-2, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(580, 395);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tb_main);
            this.tabPage1.Controls.Add(this.nud_fontsize);
            this.tabPage1.Controls.Add(this.tb_byte);
            this.tabPage1.Controls.Add(this.ch_keepText);
            this.tabPage1.Controls.Add(this.cb_encoding);
            this.tabPage1.Controls.Add(this.cb_escapeStyle);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(572, 363);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Codepoint";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cb_transliterator);
            this.tabPage2.Controls.Add(this.tb_nonroman);
            this.tabPage2.Controls.Add(this.tb_roman);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(572, 363);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Translit";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cb_transliterator
            // 
            this.cb_transliterator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_transliterator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_transliterator.FormattingEnabled = true;
            this.cb_transliterator.Location = new System.Drawing.Point(17, 305);
            this.cb_transliterator.Name = "cb_transliterator";
            this.cb_transliterator.Size = new System.Drawing.Size(141, 26);
            this.cb_transliterator.TabIndex = 2;
            this.cb_transliterator.SelectedIndexChanged += new System.EventHandler(this.tb_roman_TextChanged);
            // 
            // tb_nonroman
            // 
            this.tb_nonroman.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_nonroman.Font = new System.Drawing.Font("Arial Narrow", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_nonroman.Location = new System.Drawing.Point(14, 156);
            this.tb_nonroman.Multiline = true;
            this.tb_nonroman.Name = "tb_nonroman";
            this.tb_nonroman.ReadOnly = true;
            this.tb_nonroman.Size = new System.Drawing.Size(544, 138);
            this.tb_nonroman.TabIndex = 1;
            // 
            // tb_roman
            // 
            this.tb_roman.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_roman.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_roman.Location = new System.Drawing.Point(13, 10);
            this.tb_roman.Multiline = true;
            this.tb_roman.Name = "tb_roman";
            this.tb_roman.Size = new System.Drawing.Size(546, 131);
            this.tb_roman.TabIndex = 0;
            this.tb_roman.TextChanged += new System.EventHandler(this.tb_roman_TextChanged);
            this.tb_roman.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_KeyUp);
            // 
            // LoveStringHForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 394);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "LoveStringHForm";
            this.Text = "LoveStringH";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.form_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.nud_fontsize)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_main;
        private System.Windows.Forms.TextBox tb_byte;
        private System.Windows.Forms.ComboBox cb_escapeStyle;
        private System.Windows.Forms.ComboBox cb_encoding;
        private System.Windows.Forms.CheckBox ch_keepText;
        private System.Windows.Forms.NumericUpDown nud_fontsize;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tb_roman;
        private System.Windows.Forms.TextBox tb_nonroman;
        private System.Windows.Forms.ComboBox cb_transliterator;
    }
}

