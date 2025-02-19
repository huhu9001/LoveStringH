namespace LoveStringH {
    public partial class FormRegex:Form {
        internal const string REGFILE_PATH = "reg.txt";
        internal class SavedRegex {
            public string regex { get; }
            public string repl { get; }
            public SavedRegex(string regex, string repl) {
                this.regex = regex; this.repl = repl;
            }
        }

        internal bool regex_changed = false;

        private readonly RegexHandler re = new RegexHandler();
        //private List<RegexItem> regexes;

        private bool noevent_cb_savedRegex;
        private bool noevent_tb_regex;

        public FormRegex() {
            InitializeComponent();

            cb_savedRegex.Items.Add(new SavedRegex("(new regex)", ""));
            try {
                StreamReader regfile =
                    new StreamReader(File.Open(REGFILE_PATH, FileMode.OpenOrCreate, FileAccess.Read));
                for (string?regex; (regex = regfile.ReadLine()) != null;) {
                    string?repl = regfile.ReadLine();
                    if (repl == null) break;
                    cb_savedRegex.Items.Add(new SavedRegex(regex, repl));
                }
                regfile.Close();
            }
            catch (Exception err) { lb_msgRegex.Text = err.Message; }
            cb_savedRegex.SelectedIndex = 0;
            cb_savedRegex.SelectedIndexChanged += cb_savedRegex_SelectedIndexChanged;

            noevent_cb_savedRegex = false;
            noevent_tb_regex = false;
        }

        private new LoveStringHForm Owner { get => (LoveStringHForm)base.Owner!; }
        
        private void loadRegexInput() {
            re.regex = tb_regex.Text;
            re.repl = tb_repl.Text;
            re.setText(
                Owner.tb_regexTarget.Text,
                Owner.tb_regexTarget.SelectionStart,
                Owner.tb_regexTarget.SelectionStart + ((LoveStringHForm)Owner).tb_regexTarget.SelectionLength);
        }

        private void b_findNext_Click(object?sender, EventArgs e) {
            loadRegexInput();
            RegexHandler.ResultCode r;
            try { r = re.findNext(); } catch(Exception err) {
                lb_msgRegex.Text = err.Message; return;
            }
            switch (r) {
                case RegexHandler.ResultCode.OK: lb_msgRegex.Text = ""; break;
                case RegexHandler.ResultCode.NOTFOUND:
                    lb_msgRegex.Text = "No match is found.";
                    break;
                case RegexHandler.ResultCode.OVER:
                    lb_msgRegex.Text = "Reaching the end of text. Searching from the beginning.";
                    break;
            }
            Owner.tb_regexTarget.Select(re.start, re.end - re.start);
        }
        private void b_replace_Click(object?sender, EventArgs e) {
            loadRegexInput();
            RegexHandler.ResultCode r;
            try { r = re.replace(); } catch(Exception err) {
                lb_msgRegex.Text = err.Message; return;
            }
            switch (r) {
                case RegexHandler.ResultCode.OK: lb_msgRegex.Text = ""; break;
                case RegexHandler.ResultCode.NOTFOUND:
                    lb_msgRegex.Text = "No match is found.";
                    break;
                case RegexHandler.ResultCode.OVER:
                    lb_msgRegex.Text = "End of text reached.";
                    break;
            }
            Owner.tb_regexTarget.Text = re.text;
            Owner.tb_regexTarget.Select(re.start, re.end - re.start);
        }
        private void b_replaceAll_Click(object?sender, EventArgs e) {
            loadRegexInput();
            int count;
            try { count = re.replaceAll(); } catch(Exception err) {
                lb_msgRegex.Text = err.Message; return;
            }
            lb_msgRegex.Text = $"{count} matches replaced.";
            Owner.tb_regexTarget.Text = re.text;
            Owner.tb_regexTarget.Select(re.start, re.end - re.start);
        }

        private void tb_regex_TextChanged(object?sender, EventArgs e) {
            if (noevent_tb_regex) return;
            cb_savedRegex.SelectedIndex = 0;
        }

        private void cb_savedRegex_SelectedIndexChanged(object?sender, EventArgs e) {
            if (noevent_cb_savedRegex) return;
            if (cb_savedRegex.SelectedIndex == 0) b_saveRegex.Text = "Save";
            else {
                b_saveRegex.Text = "Delete";
                SavedRegex item = (SavedRegex)cb_savedRegex.SelectedItem!;
                noevent_tb_regex = true;
                tb_regex.Text = item.regex;
                tb_repl.Text = item.repl;
                noevent_tb_regex = false;
            }
        }

        private void b_saveRegex_Click(object?sender, EventArgs e) {
            if (cb_savedRegex.SelectedIndex == 0) {
                string regex = tb_regex.Text;
                string repl = tb_repl.Text;
                int i = 0;
                foreach (var item in cb_savedRegex.Items) {
                    if (regex.CompareTo(((SavedRegex)item).regex) < 0) break;
                    i += 1;
                }
                noevent_cb_savedRegex = true;
                cb_savedRegex.Items.Insert(i, new SavedRegex(regex, repl));
                cb_savedRegex.SelectedIndex = i;
                noevent_cb_savedRegex = false;
                b_saveRegex.Text = "Delete";
            }
            else {
                noevent_cb_savedRegex = true;
                cb_savedRegex.Items.RemoveAt(cb_savedRegex.SelectedIndex);
                cb_savedRegex.SelectedIndex = 0;
                noevent_cb_savedRegex = false;
                b_saveRegex.Text = "Save";
            }

            regex_changed = true;
        }
    }
}
