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
            this.components = new System.ComponentModel.Container();
            this.tb_main = new System.Windows.Forms.TextBox();
            this.tb_byte = new System.Windows.Forms.TextBox();
            this.cb_escapeStyle = new System.Windows.Forms.ComboBox();
            this.cb_encoding = new System.Windows.Forms.ComboBox();
            this.ch_keepText = new System.Windows.Forms.CheckBox();
            this.nud_fontsizeDecode = new System.Windows.Forms.NumericUpDown();
            this.tabcontrol_main = new System.Windows.Forms.TabControl();
            this.tabpage_codepoint = new System.Windows.Forms.TabPage();
            this.cb_fontDecode = new System.Windows.Forms.ComboBox();
            this.tabpage_translit = new System.Windows.Forms.TabPage();
            this.nud_fontsizeTranslit = new System.Windows.Forms.NumericUpDown();
            this.cb_fontTranslit = new System.Windows.Forms.ComboBox();
            this.cb_transliterator = new System.Windows.Forms.ComboBox();
            this.tb_nonroman = new System.Windows.Forms.TextBox();
            this.tb_roman = new System.Windows.Forms.TextBox();
            this.encoderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nud_fontsizeDecode)).BeginInit();
            this.tabcontrol_main.SuspendLayout();
            this.tabpage_codepoint.SuspendLayout();
            this.tabpage_translit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fontsizeTranslit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.encoderBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_main
            // 
            this.tb_main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.tb_byte.Location = new System.Drawing.Point(7, 221);
            this.tb_byte.Multiline = true;
            this.tb_byte.Name = "tb_byte";
            this.tb_byte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_byte.Size = new System.Drawing.Size(559, 139);
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
            // 
            // cb_encoding
            // 
            this.cb_encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_encoding.FormattingEnabled = true;
            this.cb_encoding.Location = new System.Drawing.Point(7, 153);
            this.cb_encoding.Name = "cb_encoding";
            this.cb_encoding.Size = new System.Drawing.Size(129, 26);
            this.cb_encoding.TabIndex = 17;
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
            // nud_fontsizeDecode
            // 
            this.nud_fontsizeDecode.DecimalPlaces = 1;
            this.nud_fontsizeDecode.Location = new System.Drawing.Point(415, 185);
            this.nud_fontsizeDecode.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_fontsizeDecode.Name = "nud_fontsizeDecode";
            this.nud_fontsizeDecode.Size = new System.Drawing.Size(69, 28);
            this.nud_fontsizeDecode.TabIndex = 19;
            this.nud_fontsizeDecode.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nud_fontsizeDecode.ValueChanged += new System.EventHandler(this.encodingFontChanged);
            // 
            // tabcontrol_main
            // 
            this.tabcontrol_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabcontrol_main.Controls.Add(this.tabpage_codepoint);
            this.tabcontrol_main.Controls.Add(this.tabpage_translit);
            this.tabcontrol_main.Location = new System.Drawing.Point(-2, -1);
            this.tabcontrol_main.Name = "tabcontrol_main";
            this.tabcontrol_main.SelectedIndex = 0;
            this.tabcontrol_main.Size = new System.Drawing.Size(580, 395);
            this.tabcontrol_main.TabIndex = 20;
            // 
            // tabpage_codepoint
            // 
            this.tabpage_codepoint.Controls.Add(this.cb_fontDecode);
            this.tabpage_codepoint.Controls.Add(this.tb_main);
            this.tabpage_codepoint.Controls.Add(this.nud_fontsizeDecode);
            this.tabpage_codepoint.Controls.Add(this.tb_byte);
            this.tabpage_codepoint.Controls.Add(this.ch_keepText);
            this.tabpage_codepoint.Controls.Add(this.cb_encoding);
            this.tabpage_codepoint.Controls.Add(this.cb_escapeStyle);
            this.tabpage_codepoint.Location = new System.Drawing.Point(4, 28);
            this.tabpage_codepoint.Name = "tabpage_codepoint";
            this.tabpage_codepoint.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage_codepoint.Size = new System.Drawing.Size(572, 363);
            this.tabpage_codepoint.TabIndex = 0;
            this.tabpage_codepoint.Text = "Codepoint";
            this.tabpage_codepoint.UseVisualStyleBackColor = true;
            // 
            // cb_fontDecode
            // 
            this.cb_fontDecode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_fontDecode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_fontDecode.FormattingEnabled = true;
            this.cb_fontDecode.Location = new System.Drawing.Point(7, 184);
            this.cb_fontDecode.Name = "cb_fontDecode";
            this.cb_fontDecode.Size = new System.Drawing.Size(402, 26);
            this.cb_fontDecode.TabIndex = 20;
            // 
            // tabpage_translit
            // 
            this.tabpage_translit.Controls.Add(this.nud_fontsizeTranslit);
            this.tabpage_translit.Controls.Add(this.cb_fontTranslit);
            this.tabpage_translit.Controls.Add(this.cb_transliterator);
            this.tabpage_translit.Controls.Add(this.tb_nonroman);
            this.tabpage_translit.Controls.Add(this.tb_roman);
            this.tabpage_translit.Location = new System.Drawing.Point(4, 28);
            this.tabpage_translit.Name = "tabpage_translit";
            this.tabpage_translit.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage_translit.Size = new System.Drawing.Size(572, 363);
            this.tabpage_translit.TabIndex = 1;
            this.tabpage_translit.Text = "Translit";
            this.tabpage_translit.UseVisualStyleBackColor = true;
            // 
            // nud_fontsizeTranslit
            // 
            this.nud_fontsizeTranslit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud_fontsizeTranslit.DecimalPlaces = 1;
            this.nud_fontsizeTranslit.Location = new System.Drawing.Point(440, 305);
            this.nud_fontsizeTranslit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_fontsizeTranslit.Name = "nud_fontsizeTranslit";
            this.nud_fontsizeTranslit.Size = new System.Drawing.Size(80, 28);
            this.nud_fontsizeTranslit.TabIndex = 4;
            this.nud_fontsizeTranslit.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nud_fontsizeTranslit.ValueChanged += new System.EventHandler(this.translitFontChanged);
            // 
            // cb_fontTranslit
            // 
            this.cb_fontTranslit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_fontTranslit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_fontTranslit.FormattingEnabled = true;
            this.cb_fontTranslit.Location = new System.Drawing.Point(164, 305);
            this.cb_fontTranslit.Name = "cb_fontTranslit";
            this.cb_fontTranslit.Size = new System.Drawing.Size(270, 26);
            this.cb_fontTranslit.TabIndex = 3;
            this.cb_fontTranslit.SelectedIndexChanged += new System.EventHandler(this.translitFontChanged);
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
            // 
            // tb_nonroman
            // 
            this.tb_nonroman.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // encoderBindingSource
            // 
            this.encoderBindingSource.DataSource = typeof(LoveStringH.Encoder);
            // 
            // LoveStringHForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 394);
            this.Controls.Add(this.tabcontrol_main);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "LoveStringHForm";
            this.Text = "LoveStringH";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.form_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.nud_fontsizeDecode)).EndInit();
            this.tabcontrol_main.ResumeLayout(false);
            this.tabpage_codepoint.ResumeLayout(false);
            this.tabpage_codepoint.PerformLayout();
            this.tabpage_translit.ResumeLayout(false);
            this.tabpage_translit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fontsizeTranslit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.encoderBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_main;
        private System.Windows.Forms.TextBox tb_byte;
        private System.Windows.Forms.ComboBox cb_escapeStyle;
        private System.Windows.Forms.ComboBox cb_encoding;
        private System.Windows.Forms.CheckBox ch_keepText;
        private System.Windows.Forms.NumericUpDown nud_fontsizeDecode;
        private System.Windows.Forms.TabControl tabcontrol_main;
        private System.Windows.Forms.TabPage tabpage_codepoint;
        private System.Windows.Forms.TabPage tabpage_translit;
        private System.Windows.Forms.TextBox tb_roman;
        private System.Windows.Forms.TextBox tb_nonroman;
        private System.Windows.Forms.ComboBox cb_transliterator;
        private System.Windows.Forms.ComboBox cb_fontDecode;
        private System.Windows.Forms.BindingSource encoderBindingSource;
        private System.Windows.Forms.ComboBox cb_fontTranslit;
        private System.Windows.Forms.NumericUpDown nud_fontsizeTranslit;
    }
}

