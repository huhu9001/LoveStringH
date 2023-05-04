using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace LoveStringH
{
    public partial class Form1 : Form {
        struct EscapeStyle {
            public delegate string f(uint u);

            public string name { get; set; }
            public f f_this;
        }
        EscapeStyle[] EscapeStyles_default = new EscapeStyle[] {
            new EscapeStyle{ name = "\\x", f_this = (uint u) => { return String.Format("\\x{0:X}", u); } },
            new EscapeStyle{ name = "%", f_this = (uint u) => { return String.Format("%{0:X2}", u); } },
        };

        struct EncoderItem {
            public string name { get; set; }
            public Encoding e;
            public EscapeStyle[] styles;
        };
        EncoderItem[] EncoderItems = new EncoderItem[] {
            new EncoderItem{ name = "Unicode", e = null, styles = new EscapeStyle[] {
                new EscapeStyle{ name = "\\u", f_this = (uint u) => {
                    if (u < 0x80) return String.Format("\\x{0:X}", u);
                    if (u < 0x10000) return String.Format("\\u{0:X4}", u);
                    return String.Format("\\U{0:X8}", u);
                } },
                new EscapeStyle{ name = "&#x", f_this = (uint u) => {
                    return String.Format("&#x{0:X};", u);
                } },
            } },
            new EncoderItem{ name = "UTF-8", e = Encoding.UTF8 },
            new EncoderItem{ name = "UTF-16", e = null, styles = new EscapeStyle[] {
                new EscapeStyle{ name = "\\x", f_this = (uint u) => { return String.Format("\\x{0:X}", u); } },
            } },
            new EncoderItem{ name = "GB 18030", e = Encoding.GetEncoding(54936) },
            new EncoderItem{ name = "Shift-JIS", e = Encoding.GetEncoding(932) },
            new EncoderItem{ name = "Big5", e = Encoding.GetEncoding(950) },
            new EncoderItem{ name = "EUC-JP", e = Encoding.GetEncoding(51932) },
        };

        public Form1() {
            InitializeComponent();

            cb_encoding.DataSource = EncoderItems;
            cb_encoding.DisplayMember = "name";
            cb_encoding.SelectedIndex = 0;

            cb_escapeStyle.DisplayMember = "name";
            cb_encoding_SelectedIndexChanged(null, null);

            cb_transliterator.DataSource = TransliteratorItems;
            cb_transliterator.DisplayMember = "name";
        }

        private void tb_main_TextChanged(object sender, EventArgs e) {
            Encoding ec = ((EncoderItem)cb_encoding.SelectedItem).e;
            StringBuilder s_result = new StringBuilder();
            if (ec == null) {
                byte[] bs = Encoding.Unicode.GetBytes(tb_main.Text);
                for (uint u0 = 0; u0 < bs.Length; u0 += 2) {
                    uint u_char = bs[u0] + ((uint)bs[u0 + 1] << 8); 
                    if (cb_encoding.SelectedIndex == 0 && u_char >= 0xD800 && u_char < 0xDC00) {
                        u0 += 2;
                        uint u_char_2 = bs[u0] + ((uint)bs[u0 + 1] << 8);
                        u_char = (u_char - 0xD7C0 << 10) + (u_char_2 - 0xDC00);
                    }
                    s_result.Append(((EscapeStyle)cb_escapeStyle.SelectedItem).f_this(u_char));
                }
            }
            else {
                byte[] bs = ec.GetBytes(tb_main.Text);
                foreach(byte b in bs) {
                    s_result.Append(((EscapeStyle)cb_escapeStyle.SelectedItem).f_this(b));
                }
            }

            tb_byte.TextChanged -= tb_byte_TextChanged;
            tb_byte.Text = s_result.ToString();
            tb_byte.TextChanged += tb_byte_TextChanged;
            ch_keepText.Checked = true;
        }

        private void tb_byte_TextChanged(object sender, EventArgs e) {
            Encoding ec = ((EncoderItem)cb_encoding.SelectedItem).e;
            StringBuilder s_result = new StringBuilder();
            if (ec == null) ec = Encoding.Unicode;

            int i0 = 0;
            List<byte> bs = new List<byte>();
            foreach (Match m in Regex.Matches(tb_byte.Text, "\\\\([0-7]{1,3})|\\\\x([A-Fa-f0-9]+)|%([A-Fa-f0-9]{2})|\\\\u([A-Fa-f0-9]{4})|\\\\U([A-Fa-f0-9]{8})|&#(\\d+);|&#x([A-Fa-f0-9]+);")) {
                if (i0 < m.Index) {
                    try { s_result.Append(ec.GetChars(bs.ToArray())); }
                    catch (DecoderFallbackException) { s_result.Append('?'); }
                    bs.Clear();
                    s_result.Append(tb_byte.Text.Substring(i0, m.Index - i0));
                }

                uint u_char;
                if (m.Groups[1].Success) u_char = Convert.ToUInt32(m.Groups[1].Value, 8);
                else if (m.Groups[2].Success) u_char = Convert.ToUInt32(m.Groups[2].Value, 16);
                else if (m.Groups[3].Success) u_char = Convert.ToUInt32(m.Groups[3].Value, 16);
                else {
                    try { s_result.Append(ec.GetChars(bs.ToArray())); }
                    catch (DecoderFallbackException) { s_result.Append('?'); }
                    bs.Clear();

                    if (m.Groups[4].Success) u_char = Convert.ToUInt32(m.Groups[4].Value, 16);
                    else if (m.Groups[5].Success) u_char = Convert.ToUInt32(m.Groups[5].Value, 16);
                    else if (m.Groups[6].Success) u_char = Convert.ToUInt32(m.Groups[6].Value, 10);
                    else u_char = Convert.ToUInt32(m.Groups[7].Value, 16);

                    if (u_char < 0x10000) s_result.Append((char)u_char);
                    else {
                        s_result.Append((char)((u_char >> 10) + 0xD7C0));
                        s_result.Append((char)((u_char & 0x3FF) + 0xDC00));
                    }
                    i0 = m.Index + m.Length;
                    continue;
                }

                if (ec == Encoding.Unicode) {
                    while (u_char > 0xFFFF) {
                        bs.Add((byte)(u_char & 0xFF));
                        bs.Add((byte)(u_char >> 8 & 0xFF));
                        u_char >>= 16;
                    }
                    bs.Add((byte)(u_char & 0xFF));
                    bs.Add((byte)(u_char >> 8 & 0xFF));
                }
                else {
                    while (u_char > 0xFF) {
                        bs.Add((byte)(u_char & 0xFF));
                        u_char >>= 8;
                    }
                    bs.Add((byte)(u_char & 0xFF));
                }
                i0 = m.Index + m.Length;
            }
            try { s_result.Append(ec.GetChars(bs.ToArray())); }
            catch (DecoderFallbackException) { s_result.Append('?'); }
            if (i0 < tb_byte.Text.Length) s_result.Append(tb_byte.Text.Substring(i0));

            tb_main.TextChanged -= tb_main_TextChanged;
            tb_main.Text = s_result.ToString();
            tb_main.TextChanged += tb_main_TextChanged;
            ch_keepText.Checked = false;
        }

        private void cb_encoding_SelectedIndexChanged(object sender, EventArgs e) {
            EscapeStyle[] styles = ((EncoderItem)cb_encoding.SelectedItem).styles;
            cb_escapeStyle.SelectedIndexChanged -= cb_escapeStyle_SelectedIndexChanged;
            if (styles == null) cb_escapeStyle.DataSource = EscapeStyles_default;
            else cb_escapeStyle.DataSource = styles;
            cb_escapeStyle.SelectedIndexChanged += cb_escapeStyle_SelectedIndexChanged;

            if (ch_keepText.Checked) tb_main_TextChanged(sender, e);
            else tb_byte_TextChanged(sender, e);
        }

        private void cb_escapeStyle_SelectedIndexChanged(object sender, EventArgs e) {
            tb_main_TextChanged(sender, e);
        }

        private void nud_fontsize_ValueChanged(object sender, EventArgs e) {
            tb_main.Font = new Font("宋体", (float)nud_fontsize.Value);
        }
    }
}
