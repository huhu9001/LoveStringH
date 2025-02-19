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
        private void InitializeComponent() {
            tb_main = new TextBox();
            tb_byte = new TextBox();
            cb_escapeStyle = new ComboBox();
            cb_encoding = new ComboBox();
            ch_keepText = new CheckBox();
            nud_fontsizeDecode = new NumericUpDown();
            tabcontrol_main = new TabControl();
            tabpage_codepoint = new TabPage();
            lb_doNotEncodeEnd = new Label();
            tb_doNotEncode = new TextBox();
            lb_doNotEncode = new Label();
            cb_fontDecode = new ComboBox();
            tabpage_translit = new TabPage();
            nud_fontsizeTranslit = new NumericUpDown();
            cb_fontTranslit = new ComboBox();
            cb_transliterator = new ComboBox();
            tb_nonroman = new TextBox();
            tb_roman = new TextBox();
            tabpage_regex = new TabPage();
            tb_regexTarget = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nud_fontsizeDecode).BeginInit();
            tabcontrol_main.SuspendLayout();
            tabpage_codepoint.SuspendLayout();
            tabpage_translit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nud_fontsizeTranslit).BeginInit();
            tabpage_regex.SuspendLayout();
            SuspendLayout();
            // 
            // tb_main
            // 
            tb_main.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_main.Location = new Point(7, 8);
            tb_main.Margin = new Padding(4);
            tb_main.Multiline = true;
            tb_main.Name = "tb_main";
            tb_main.ScrollBars = ScrollBars.Both;
            tb_main.Size = new Size(681, 144);
            tb_main.TabIndex = 0;
            tb_main.TextChanged += tb_main_TextChanged;
            tb_main.KeyUp += input_KeyUp_char;
            // 
            // tb_byte
            // 
            tb_byte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tb_byte.Location = new Point(7, 276);
            tb_byte.Margin = new Padding(4);
            tb_byte.Multiline = true;
            tb_byte.Name = "tb_byte";
            tb_byte.ScrollBars = ScrollBars.Vertical;
            tb_byte.Size = new Size(684, 203);
            tb_byte.TabIndex = 9;
            tb_byte.TextChanged += tb_byte_TextChanged;
            tb_byte.KeyUp += input_KeyUp_byte;
            // 
            // cb_escapeStyle
            // 
            cb_escapeStyle.DisplayMember = "name";
            cb_escapeStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_escapeStyle.FormattingEnabled = true;
            cb_escapeStyle.Location = new Point(172, 160);
            cb_escapeStyle.Margin = new Padding(4);
            cb_escapeStyle.Name = "cb_escapeStyle";
            cb_escapeStyle.Size = new Size(85, 32);
            cb_escapeStyle.TabIndex = 16;
            // 
            // cb_encoding
            // 
            cb_encoding.DisplayMember = "name";
            cb_encoding.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_encoding.FormattingEnabled = true;
            cb_encoding.Location = new Point(7, 160);
            cb_encoding.Margin = new Padding(4);
            cb_encoding.Name = "cb_encoding";
            cb_encoding.Size = new Size(157, 32);
            cb_encoding.TabIndex = 17;
            // 
            // ch_keepText
            // 
            ch_keepText.AutoSize = true;
            ch_keepText.Checked = true;
            ch_keepText.CheckState = CheckState.Checked;
            ch_keepText.Location = new Point(265, 162);
            ch_keepText.Margin = new Padding(4);
            ch_keepText.Name = "ch_keepText";
            ch_keepText.Size = new Size(224, 28);
            ch_keepText.TabIndex = 18;
            ch_keepText.Text = "Keep Text Unchanged";
            ch_keepText.UseVisualStyleBackColor = true;
            // 
            // nud_fontsizeDecode
            // 
            nud_fontsizeDecode.DecimalPlaces = 1;
            nud_fontsizeDecode.Location = new Point(505, 201);
            nud_fontsizeDecode.Margin = new Padding(4);
            nud_fontsizeDecode.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nud_fontsizeDecode.Name = "nud_fontsizeDecode";
            nud_fontsizeDecode.Size = new Size(84, 30);
            nud_fontsizeDecode.TabIndex = 19;
            nud_fontsizeDecode.Value = new decimal(new int[] { 12, 0, 0, 0 });
            nud_fontsizeDecode.ValueChanged += encodingFontChanged;
            // 
            // tabcontrol_main
            // 
            tabcontrol_main.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabcontrol_main.Controls.Add(tabpage_codepoint);
            tabcontrol_main.Controls.Add(tabpage_translit);
            tabcontrol_main.Controls.Add(tabpage_regex);
            tabcontrol_main.Location = new Point(-2, -1);
            tabcontrol_main.Margin = new Padding(4);
            tabcontrol_main.Name = "tabcontrol_main";
            tabcontrol_main.SelectedIndex = 0;
            tabcontrol_main.Size = new Size(709, 527);
            tabcontrol_main.TabIndex = 20;
            tabcontrol_main.Selected += tabcontrol_main_Selected;
            tabcontrol_main.Deselected += tabcontrol_main_Deselected;
            // 
            // tabpage_codepoint
            // 
            tabpage_codepoint.Controls.Add(lb_doNotEncodeEnd);
            tabpage_codepoint.Controls.Add(tb_doNotEncode);
            tabpage_codepoint.Controls.Add(lb_doNotEncode);
            tabpage_codepoint.Controls.Add(cb_fontDecode);
            tabpage_codepoint.Controls.Add(tb_main);
            tabpage_codepoint.Controls.Add(nud_fontsizeDecode);
            tabpage_codepoint.Controls.Add(tb_byte);
            tabpage_codepoint.Controls.Add(ch_keepText);
            tabpage_codepoint.Controls.Add(cb_encoding);
            tabpage_codepoint.Controls.Add(cb_escapeStyle);
            tabpage_codepoint.Location = new Point(4, 33);
            tabpage_codepoint.Margin = new Padding(4);
            tabpage_codepoint.Name = "tabpage_codepoint";
            tabpage_codepoint.Padding = new Padding(4);
            tabpage_codepoint.Size = new Size(701, 490);
            tabpage_codepoint.TabIndex = 0;
            tabpage_codepoint.Text = "Codepoint";
            tabpage_codepoint.UseVisualStyleBackColor = true;
            // 
            // lb_doNotEncodeEnd
            // 
            lb_doNotEncodeEnd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lb_doNotEncodeEnd.AutoSize = true;
            lb_doNotEncodeEnd.Location = new Point(625, 242);
            lb_doNotEncodeEnd.Name = "lb_doNotEncodeEnd";
            lb_doNotEncodeEnd.Size = new Size(16, 24);
            lb_doNotEncodeEnd.TabIndex = 23;
            lb_doNotEncodeEnd.Text = "]";
            // 
            // tb_doNotEncode
            // 
            tb_doNotEncode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_doNotEncode.Location = new Point(178, 239);
            tb_doNotEncode.Name = "tb_doNotEncode";
            tb_doNotEncode.Size = new Size(441, 30);
            tb_doNotEncode.TabIndex = 22;
            tb_doNotEncode.Text = "\\x20\\r\\n";
            tb_doNotEncode.TextChanged += cb_escapeStyle_SelectedIndexChanged;
            // 
            // lb_doNotEncode
            // 
            lb_doNotEncode.AutoSize = true;
            lb_doNotEncode.Location = new Point(7, 242);
            lb_doNotEncode.Name = "lb_doNotEncode";
            lb_doNotEncode.Size = new Size(165, 24);
            lb_doNotEncode.TabIndex = 21;
            lb_doNotEncode.Text = "Do not encode: [^";
            // 
            // cb_fontDecode
            // 
            cb_fontDecode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_fontDecode.DisplayMember = "Name";
            cb_fontDecode.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_fontDecode.FormattingEnabled = true;
            cb_fontDecode.Location = new Point(7, 200);
            cb_fontDecode.Margin = new Padding(4);
            cb_fontDecode.Name = "cb_fontDecode";
            cb_fontDecode.Size = new Size(490, 32);
            cb_fontDecode.TabIndex = 20;
            cb_fontDecode.SelectedIndexChanged += encodingFontChanged;
            // 
            // tabpage_translit
            // 
            tabpage_translit.Controls.Add(nud_fontsizeTranslit);
            tabpage_translit.Controls.Add(cb_fontTranslit);
            tabpage_translit.Controls.Add(cb_transliterator);
            tabpage_translit.Controls.Add(tb_nonroman);
            tabpage_translit.Controls.Add(tb_roman);
            tabpage_translit.Location = new Point(4, 33);
            tabpage_translit.Margin = new Padding(4);
            tabpage_translit.Name = "tabpage_translit";
            tabpage_translit.Padding = new Padding(4);
            tabpage_translit.Size = new Size(701, 490);
            tabpage_translit.TabIndex = 1;
            tabpage_translit.Text = "Translit";
            tabpage_translit.UseVisualStyleBackColor = true;
            // 
            // nud_fontsizeTranslit
            // 
            nud_fontsizeTranslit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            nud_fontsizeTranslit.DecimalPlaces = 1;
            nud_fontsizeTranslit.Location = new Point(538, 407);
            nud_fontsizeTranslit.Margin = new Padding(4);
            nud_fontsizeTranslit.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nud_fontsizeTranslit.Name = "nud_fontsizeTranslit";
            nud_fontsizeTranslit.Size = new Size(98, 30);
            nud_fontsizeTranslit.TabIndex = 4;
            nud_fontsizeTranslit.Value = new decimal(new int[] { 16, 0, 0, 0 });
            nud_fontsizeTranslit.ValueChanged += translitFontChanged;
            // 
            // cb_fontTranslit
            // 
            cb_fontTranslit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cb_fontTranslit.DisplayMember = "Name";
            cb_fontTranslit.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_fontTranslit.FormattingEnabled = true;
            cb_fontTranslit.Location = new Point(200, 407);
            cb_fontTranslit.Margin = new Padding(4);
            cb_fontTranslit.Name = "cb_fontTranslit";
            cb_fontTranslit.Size = new Size(329, 32);
            cb_fontTranslit.TabIndex = 3;
            cb_fontTranslit.SelectedIndexChanged += translitFontChanged;
            // 
            // cb_transliterator
            // 
            cb_transliterator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cb_transliterator.DisplayMember = "name";
            cb_transliterator.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_transliterator.FormattingEnabled = true;
            cb_transliterator.Location = new Point(21, 407);
            cb_transliterator.Margin = new Padding(4);
            cb_transliterator.Name = "cb_transliterator";
            cb_transliterator.Size = new Size(171, 32);
            cb_transliterator.TabIndex = 2;
            // 
            // tb_nonroman
            // 
            tb_nonroman.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tb_nonroman.Location = new Point(17, 208);
            tb_nonroman.Margin = new Padding(4);
            tb_nonroman.Multiline = true;
            tb_nonroman.Name = "tb_nonroman";
            tb_nonroman.ReadOnly = true;
            tb_nonroman.Size = new Size(664, 183);
            tb_nonroman.TabIndex = 1;
            // 
            // tb_roman
            // 
            tb_roman.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tb_roman.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_roman.Location = new Point(16, 13);
            tb_roman.Margin = new Padding(4);
            tb_roman.Multiline = true;
            tb_roman.Name = "tb_roman";
            tb_roman.Size = new Size(666, 173);
            tb_roman.TabIndex = 0;
            tb_roman.TextChanged += tb_roman_TextChanged;
            tb_roman.KeyUp += input_KeyUp;
            // 
            // tabpage_regex
            // 
            tabpage_regex.Controls.Add(tb_regexTarget);
            tabpage_regex.Location = new Point(4, 33);
            tabpage_regex.Margin = new Padding(4);
            tabpage_regex.Name = "tabpage_regex";
            tabpage_regex.Size = new Size(701, 490);
            tabpage_regex.TabIndex = 2;
            tabpage_regex.Text = "Regex";
            tabpage_regex.UseVisualStyleBackColor = true;
            // 
            // tb_regexTarget
            // 
            tb_regexTarget.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tb_regexTarget.HideSelection = false;
            tb_regexTarget.Location = new Point(12, 19);
            tb_regexTarget.Margin = new Padding(4);
            tb_regexTarget.Multiline = true;
            tb_regexTarget.Name = "tb_regexTarget";
            tb_regexTarget.Size = new Size(676, 453);
            tb_regexTarget.TabIndex = 4;
            // 
            // LoveStringHForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(706, 525);
            Controls.Add(tabcontrol_main);
            KeyPreview = true;
            Margin = new Padding(4);
            MinimumSize = new Size(728, 581);
            Name = "LoveStringHForm";
            Text = "LoveStringH";
            FormClosing += this.formClosing;
            KeyUp += form_KeyUp;
            ((System.ComponentModel.ISupportInitialize)nud_fontsizeDecode).EndInit();
            tabcontrol_main.ResumeLayout(false);
            tabpage_codepoint.ResumeLayout(false);
            tabpage_codepoint.PerformLayout();
            tabpage_translit.ResumeLayout(false);
            tabpage_translit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nud_fontsizeTranslit).EndInit();
            tabpage_regex.ResumeLayout(false);
            tabpage_regex.PerformLayout();
            ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox cb_fontTranslit;
        private System.Windows.Forms.NumericUpDown nud_fontsizeTranslit;
        private System.Windows.Forms.TabPage tabpage_regex;
        internal System.Windows.Forms.TextBox tb_regexTarget;
        private TextBox tb_doNotEncode;
        private Label lb_doNotEncode;
        private Label lb_doNotEncodeEnd;
    }
}

