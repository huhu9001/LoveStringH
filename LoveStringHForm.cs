using System.Text;
using static LoveStringH.Transliterator;

namespace LoveStringH {
    public partial class LoveStringHForm:Form {
        static private readonly System.Drawing.Text.InstalledFontCollection fonts = new();

        private bool stopTextChangedOnce;
        private bool stopEscapeStyleChangedOnce;
        private Encoder.EscapeStyle[] currentStyles;

        private readonly FormRegex formRegex;

        public LoveStringHForm() {
            InitializeComponent();

            formRegex = new FormRegex();
            AddOwnedForm(formRegex);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            cb_encoding.Items.AddRange(Encoder.all);
            cb_encoding.SelectedIndex = 0;

            cb_transliterator.Items.AddRange(Transliterator.all);
            cb_transliterator.SelectedIndex = 0;

            currentStyles = ((Encoder)cb_encoding.SelectedItem!).styles;
            cb_escapeStyle.Items.AddRange(currentStyles);
            cb_escapeStyle.SelectedIndex = 0;

            {
                FontFamily[] families = fonts.Families;
                cb_fontDecode.Items.AddRange(families);
                cb_fontTranslit.Items.AddRange(families);
                cb_fontDecode.SelectedIndex = cb_fontTranslit.SelectedIndex =
                    Array.IndexOf(families, this.Font.FontFamily);
            }

            cb_escapeStyle.SelectedIndexChanged += new(cb_escapeStyle_SelectedIndexChanged);
            cb_encoding.SelectedIndexChanged += new(cb_encoding_SelectedIndexChanged);
            cb_transliterator.SelectedIndexChanged += new(tb_roman_TextChanged);

            stopTextChangedOnce = false;
            stopEscapeStyleChangedOnce = false;
        }

        private void formClosing(object?sender, EventArgs e) {
            if (formRegex.regex_changed)
                try {
                    StreamWriter regfile = new StreamWriter(File.Open(
                        FormRegex.REGFILE_PATH,
                        FileMode.Create,
                        FileAccess.Write));
                    foreach (FormRegex.SavedRegex item in formRegex
                        .cb_savedRegex.Items.Cast<FormRegex.SavedRegex>().Skip(1)) {
                        regfile.WriteLine(item.regex);
                        regfile.WriteLine(item.repl);
                    }
                    regfile.Close();
                }
                catch (Exception) { }
        }

        private void encode_text() {
            stopTextChangedOnce = true;
            tb_byte.Text = ((Encoder)cb_encoding.SelectedItem!).encode(
                tb_main.Text,
                ((Encoder.EscapeStyle)cb_escapeStyle.SelectedItem!).escape,
                tb_doNotEncode.Text);
            stopTextChangedOnce = false;
        }
        private void decode_text() {
            stopTextChangedOnce = true;
            tb_main.Text = ((Encoder)cb_encoding.SelectedItem!).decode(tb_byte.Text);
            stopTextChangedOnce = false;
        }

        private void tb_main_TextChanged(object?sender, EventArgs e) {
            if (stopTextChangedOnce) return;
            encode_text();
            ch_keepText.Checked = true;
        }

        private void tb_byte_TextChanged(object?sender, EventArgs e) {
            if (stopTextChangedOnce) return;
            decode_text();
            ch_keepText.Checked = false;
        }

        private void cb_encoding_SelectedIndexChanged(object?sender, EventArgs e) {
            Encoder.EscapeStyle[] styles = ((Encoder)cb_encoding.SelectedItem!).styles;
            if (currentStyles != styles) {
                stopEscapeStyleChangedOnce = true;
                currentStyles = styles;
                cb_escapeStyle.Items.Clear();
                cb_escapeStyle.Items.AddRange(currentStyles);
                cb_escapeStyle.SelectedIndex = 0;
                stopEscapeStyleChangedOnce = false;
            }
            if (ch_keepText.Checked) encode_text();
            else decode_text();
        }

        private void cb_escapeStyle_SelectedIndexChanged(object?sender, EventArgs e) {
            if (stopEscapeStyleChangedOnce) return;
            encode_text();
        }

        private void encodingFontChanged(object?sender, EventArgs e) {
            tb_main.Font = new Font((FontFamily)cb_fontDecode.SelectedItem!, (float)nud_fontsizeDecode.Value);
        }
        private void translitFontChanged(object?sender, EventArgs e) {
            tb_nonroman.Font = new Font((FontFamily)cb_fontTranslit.SelectedItem!, (float)nud_fontsizeTranslit.Value);
        }

        private void tb_roman_TextChanged(object?sender, EventArgs e) {
            tb_nonroman.Text = ((Transliterator)cb_transliterator.SelectedItem!).GetTranslit(tb_roman.Text);
        }

        private void tabcontrol_main_Selected(object?sender, TabControlEventArgs e) {
            if (e.TabPage == tabpage_regex) {
                formRegex.Show();
                if (WindowState == FormWindowState.Normal) {
                    Point p = DesktopLocation;
                    p.Y += Size.Height;
                    formRegex.DesktopLocation = p;
                }
            }
        }
        private void tabcontrol_main_Deselected(object?sender, TabControlEventArgs e) {
            if (e.TabPage == tabpage_regex) formRegex.Hide();
        }

        private void form_KeyUp(object?sender, KeyEventArgs e) {
            if (e.Modifiers == Keys.Alt) {
                switch (e.KeyCode) {
                    case Keys.X:
                        tabcontrol_main.SelectedIndex = 0;
                        tb_byte.Focus();
                        e.Handled = true;
                        break;
                    case Keys.L:
                        tabcontrol_main.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 0;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                    case Keys.G:
                        tabcontrol_main.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 1;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                    case Keys.R:
                        tabcontrol_main.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 2;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                    case Keys.A:
                        tabcontrol_main.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 3;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                    case Keys.K:
                        tabcontrol_main.SelectedIndex = 1;
                        cb_transliterator.SelectedIndex = 4;
                        tb_roman.Focus();
                        e.Handled = true;
                        break;
                }
            }
        }

        private void input_KeyUp(object?sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F2) {
                Clipboard.SetText(tb_nonroman.Text);
                tb_roman.SelectAll();
                e.Handled = true;
            }
        }
        private void input_KeyUp_char(object?sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F2) {
                Clipboard.SetText(tb_byte.Text);
                tb_main.SelectAll();
                e.Handled = true;
            }
        }
        private void input_KeyUp_byte(object?sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F2) {
                Clipboard.SetText(tb_main.Text);
                tb_byte.SelectAll();
                e.Handled = true;
            }
        }
    }
}
