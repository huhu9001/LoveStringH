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
    public partial class LoveStringHForm : Form {
        static readonly Regex rgxEscape =
            new Regex("\\\\([0-7]{1,3})|\\\\x([A-Fa-f0-9]+)|%([A-Fa-f0-9]{2})|\\\\u([A-Fa-f0-9]{4})|\\\\U([A-Fa-f0-9]{8})|&#(\\d+);|&#x([A-Fa-f0-9]+);");

        class EscapeStyle {
            public string name { get; set; }
            public Func<uint, string> escape;
        }
        static readonly EscapeStyle[] estylesDefault = new EscapeStyle[] {
            new EscapeStyle{ name = "\\x", escape = (uint u) => { return String.Format("\\x{0:X}", u); } },
            new EscapeStyle{ name = "%", escape = (uint u) => { return String.Format("%{0:X2}", u); } },
        };

        class EncoderItem {
            public string name { get; set; }
            public Encoding e;
            public EscapeStyle[] styles;
        };
        static readonly EncoderItem[] EncoderItems = new EncoderItem[] {
            new EncoderItem{ name = "Unicode", e = null, styles = new EscapeStyle[] {
                new EscapeStyle{ name = "\\u", escape = (uint u) => {
                    if (u < 0x80) return String.Format("\\x{0:X}", u);
                    if (u < 0x10000) return String.Format("\\u{0:X4}", u);
                    return String.Format("\\U{0:X8}", u);
                } },
                new EscapeStyle{ name = "&#x", escape = (uint u) => {
                    return String.Format("&#x{0:X};", u);
                } },
            } },
            new EncoderItem{ name = "UTF-8", e = Encoding.UTF8, styles = estylesDefault },
            new EncoderItem{ name = "UTF-16", e = null, styles = new EscapeStyle[] { estylesDefault[0] } },
            new EncoderItem{ name = "GB 18030", e = Encoding.GetEncoding(54936), styles = estylesDefault },
            new EncoderItem{ name = "GB 2312", e = Encoding.GetEncoding(936), styles = estylesDefault },
            new EncoderItem{ name = "Shift-JIS", e = Encoding.GetEncoding(932), styles = estylesDefault },
            new EncoderItem{ name = "Big5", e = Encoding.GetEncoding(950), styles = estylesDefault },
            new EncoderItem{ name = "EUC-JP", e = Encoding.GetEncoding(51932), styles = estylesDefault },
        };
        
        static readonly Transliterator[] TransliteratorItems = new Transliterator[] {
            new Transliterator("Latin", TrLatin.RegexItems),
            new Transliterator("Greek", TrGreek.RegexItems),
            new Transliterator("Cyrillic", TrCyrillic.RegexItems),
            new Transliterator("Arabic", TrArabic.RegexItems),
            new Transliterator("Hangul", TrHangul.RegexItems),
        };

        bool stopTextChangedOnce;
        bool stopEscapeStyleChangedOnce;

        public LoveStringHForm() {
            InitializeComponent();

            cb_encoding.DataSource = EncoderItems;
            cb_encoding.DisplayMember = "name";
            cb_encoding.SelectedIndex = 0;

            cb_escapeStyle.DisplayMember = "name";
            cb_encoding_SelectedIndexChanged(null, null);

            cb_transliterator.DataSource = TransliteratorItems;
            cb_transliterator.DisplayMember = "name";

            KeyPreview = true;

            stopTextChangedOnce = false;
            stopEscapeStyleChangedOnce = false;
        }

        private void tb_main_TextChanged(object sender, EventArgs e) {
            if (stopTextChangedOnce) return;
            EncoderItem encoder = (EncoderItem)cb_encoding.SelectedItem;
            Encoding ec = encoder.e;
            StringBuilder s_result = new StringBuilder();
            Func<uint, string> escape = ((EscapeStyle)cb_escapeStyle.SelectedItem).escape;
            if (ec == null) {
                char surrogate = '\0';
                foreach (char chr in tb_main.Text) {
                    if (surrogate != '\0') {
                        if (chr >= '\uDC00' && chr < '\uE000') {
                            s_result.Append(escape(((uint)surrogate - 0xD7C0 << 10) + ((uint)chr - 0xDC00)));
                            surrogate = '\0';
                            continue;
                        }
                        else {
                            s_result.Append(escape(surrogate));
                            surrogate = '\0';
                        }
                    }
                    if (encoder.name == "Unicode" && chr >= '\uD800' && chr < '\uDC00')
                        surrogate = chr;
                    else s_result.Append(escape(chr));
                }
                if (surrogate != '\0') s_result.Append(escape(surrogate));
            }
            else {
                byte[] bs = ec.GetBytes(tb_main.Text);
                foreach(byte b in bs) {
                    s_result.Append(escape(b));
                }
            }

            stopTextChangedOnce = true;
            tb_byte.Text = s_result.ToString();
            stopTextChangedOnce = false;
            ch_keepText.Checked = true;
        }

        private void tb_byte_TextChanged(object sender, EventArgs e) {
            if (stopTextChangedOnce) return;
            EncoderItem encoder = (EncoderItem)cb_encoding.SelectedItem;
            Encoding ec = encoder.e;
            StringBuilder s_result = new StringBuilder();
            char[] decode(List<byte> bytes) {
                if (ec != null) {
                    try { return ec.GetChars(bytes.ToArray()); }
                    catch (DecoderFallbackException) { return new char[] { '?' }; }
                }
                else {
                    char[] result = new char[(bytes.Count + 1) / 2];
                    for (int i0 = 0; ; ++i0) {
                        if (i0 * 2 >= bytes.Count - 1) {
                            if (i0 * 2 < bytes.Count) result[i0] = (char)bytes[i0 * 2];
                            break;
                        }
                        result[i0] = (char)(bytes[i0 * 2] | (bytes[i0 * 2 + 1] << 8));
                    }
                    return result;
                }
            }

            int endLastMatch = 0;
            List<byte> bs = new List<byte>();
            foreach (Match m in rgxEscape.Matches(tb_byte.Text)) {
                if (endLastMatch < m.Index) {
                    s_result.Append(decode(bs));
                    bs.Clear();
                    s_result.Append(tb_byte.Text.Substring(endLastMatch, m.Index - endLastMatch));
                }

                uint u_char;
                uint ToUInt32(string str, int radix) {
                    try { return Convert.ToUInt32(str, radix); }
                    catch (OverflowException) { return 0x3F; }
                } 
                if (m.Groups[1].Success) u_char = ToUInt32(m.Groups[1].Value, 8);
                else if (m.Groups[2].Success) u_char = ToUInt32(m.Groups[2].Value, 16);
                else if (m.Groups[3].Success) u_char = ToUInt32(m.Groups[3].Value, 16);
                else {
                    s_result.Append(decode(bs));
                    bs.Clear();

                    if (m.Groups[4].Success) u_char = ToUInt32(m.Groups[4].Value, 16);
                    else if (m.Groups[5].Success) u_char = ToUInt32(m.Groups[5].Value, 16);
                    else if (m.Groups[6].Success) u_char = ToUInt32(m.Groups[6].Value, 10);
                    else u_char = ToUInt32(m.Groups[7].Value, 16);

                    if (u_char < 0x10000) s_result.Append((char)u_char);
                    else if (u_char < 0x110000) {
                        s_result.Append((char)((u_char >> 10) + 0xD7C0));
                        s_result.Append((char)((u_char & 0x3FF) + 0xDC00));
                    }
                    else s_result.Append('?');
                    endLastMatch = m.Index + m.Length;
                    continue;
                }

                if (ec == null) {
                    if (u_char >= 0x10000) {
                        if (encoder.name == "Unicode" && u_char < 0x110000) {
                            char s1 = (char)((u_char >> 10) + 0xD7C0);
                            char s2 = (char)((u_char & 0x3FF) + 0xDC00);
                            bs.Add((byte)(s1 & 0xFF));
                            bs.Add((byte)(s1 >> 8 & 0xFF));
                            bs.Add((byte)(s2 & 0xFF));
                            bs.Add((byte)(s2 >> 8 & 0xFF));
                        }
                        else {
                            bs.Add((byte)(u_char & 0xFF));
                            bs.Add((byte)(u_char >> 8 & 0xFF));
                            bs.Add((byte)(u_char >> 16 & 0xFF));
                            bs.Add((byte)(u_char >> 24 & 0xFF));
                        }
                    }
                    else {
                        bs.Add((byte)(u_char & 0xFF));
                        bs.Add((byte)(u_char >> 8 & 0xFF));
                    }
                }
                else {
                    while (u_char > 0xFF) {
                        bs.Add((byte)(u_char & 0xFF));
                        u_char >>= 8;
                    }
                    bs.Add((byte)(u_char & 0xFF));
                }
                endLastMatch = m.Index + m.Length;
            }
            s_result.Append(decode(bs));
            if (endLastMatch < tb_byte.Text.Length) s_result.Append(tb_byte.Text.Substring(endLastMatch));
            
            stopTextChangedOnce = true;
            tb_main.Text = s_result.ToString();
            stopTextChangedOnce = false;
            ch_keepText.Checked = false;
        }

        private void cb_encoding_SelectedIndexChanged(object sender, EventArgs e) {
            EscapeStyle[] styles = ((EncoderItem)cb_encoding.SelectedItem).styles;
            if (cb_escapeStyle.DataSource != styles) {
                stopEscapeStyleChangedOnce = true;
                cb_escapeStyle.DataSource = styles;
                stopEscapeStyleChangedOnce = false;
            }
            if (ch_keepText.Checked) tb_main_TextChanged(sender, e);
            else tb_byte_TextChanged(sender, e);
        }

        private void cb_escapeStyle_SelectedIndexChanged(object sender, EventArgs e) {
            if (stopEscapeStyleChangedOnce) return;
            tb_main_TextChanged(sender, e);
        }

        private void nud_fontsize_ValueChanged(object sender, EventArgs e) {
            tb_main.Font = new Font("宋体", (float)nud_fontsize.Value);
        }

        private void tb_roman_TextChanged(object sender, EventArgs e) {
            tb_nonroman.Text = ((Transliterator)cb_transliterator.SelectedItem).GetTranslit(tb_roman.Text);
        }

        private void form_KeyUp(object sender, KeyEventArgs e) {
            if (e.Modifiers == Keys.Alt) {
                switch (e.KeyCode) {
                    case Keys.L:
                        tabControl1.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 0;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                    case Keys.G:
                        tabControl1.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 1;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                    case Keys.R:
                        tabControl1.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 2;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                    case Keys.A:
                        tabControl1.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 3;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                    case Keys.K:
                        tabControl1.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 4;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                }
            }
        }

        private void input_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F2) {
                Clipboard.SetText(tb_nonroman.Text);
                tb_roman.SelectAll();
            }
        }
    }
}
