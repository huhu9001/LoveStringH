namespace LoveStringH
{
    partial class Form1
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
            ((System.ComponentModel.ISupportInitialize)(this.nud_fontsize)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_main
            // 
            this.tb_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_main.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_main.Location = new System.Drawing.Point(15, 12);
            this.tb_main.Multiline = true;
            this.tb_main.Name = "tb_main";
            this.tb_main.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_main.Size = new System.Drawing.Size(551, 160);
            this.tb_main.TabIndex = 0;
            this.tb_main.TextChanged += new System.EventHandler(this.tb_main_TextChanged);
            // 
            // tb_byte
            // 
            this.tb_byte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_byte.Location = new System.Drawing.Point(15, 210);
            this.tb_byte.Multiline = true;
            this.tb_byte.Name = "tb_byte";
            this.tb_byte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_byte.Size = new System.Drawing.Size(551, 172);
            this.tb_byte.TabIndex = 9;
            this.tb_byte.TextChanged += new System.EventHandler(this.tb_byte_TextChanged);
            // 
            // cb_escapeStyle
            // 
            this.cb_escapeStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_escapeStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_escapeStyle.FormattingEnabled = true;
            this.cb_escapeStyle.Location = new System.Drawing.Point(150, 178);
            this.cb_escapeStyle.Name = "cb_escapeStyle";
            this.cb_escapeStyle.Size = new System.Drawing.Size(70, 26);
            this.cb_escapeStyle.TabIndex = 16;
            this.cb_escapeStyle.SelectedIndexChanged += new System.EventHandler(this.cb_escapeStyle_SelectedIndexChanged);
            // 
            // cb_encoding
            // 
            this.cb_encoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_encoding.FormattingEnabled = true;
            this.cb_encoding.Location = new System.Drawing.Point(15, 178);
            this.cb_encoding.Name = "cb_encoding";
            this.cb_encoding.Size = new System.Drawing.Size(129, 26);
            this.cb_encoding.TabIndex = 17;
            this.cb_encoding.SelectedIndexChanged += new System.EventHandler(this.cb_encoding_SelectedIndexChanged);
            // 
            // ch_keepText
            // 
            this.ch_keepText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ch_keepText.AutoSize = true;
            this.ch_keepText.Checked = true;
            this.ch_keepText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_keepText.Location = new System.Drawing.Point(226, 182);
            this.ch_keepText.Name = "ch_keepText";
            this.ch_keepText.Size = new System.Drawing.Size(205, 22);
            this.ch_keepText.TabIndex = 18;
            this.ch_keepText.Text = "Keep Text Unchanged";
            this.ch_keepText.UseVisualStyleBackColor = true;
            // 
            // nud_fontsize
            // 
            this.nud_fontsize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud_fontsize.DecimalPlaces = 1;
            this.nud_fontsize.Location = new System.Drawing.Point(437, 176);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 394);
            this.Controls.Add(this.nud_fontsize);
            this.Controls.Add(this.ch_keepText);
            this.Controls.Add(this.cb_encoding);
            this.Controls.Add(this.cb_escapeStyle);
            this.Controls.Add(this.tb_byte);
            this.Controls.Add(this.tb_main);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "Form1";
            this.Text = "LoveStringH";
            ((System.ComponentModel.ISupportInitialize)(this.nud_fontsize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_main;
        private System.Windows.Forms.TextBox tb_byte;
        private System.Windows.Forms.ComboBox cb_escapeStyle;
        private System.Windows.Forms.ComboBox cb_encoding;
        private System.Windows.Forms.CheckBox ch_keepText;
        private System.Windows.Forms.NumericUpDown nud_fontsize;
    }
}

