using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoveStringH {
    public partial class FormRegex:Form {
        private const string REGFILE_PATH = "reg.txt";

        private readonly RegexHandler re = new RegexHandler();
        private class RegexItem {
            public string regex { get; }
            public string repl { get; }
            public RegexItem(string regex, string repl) {
                this.regex = regex; this.repl = repl;
            }
        }
        private List<RegexItem> regexes;

        private bool noevent_cb_savedRegex;
        private bool noevent_tb_regex;

        public FormRegex() {
            InitializeComponent();
            
            regexes = new List<RegexItem> { new RegexItem("(new regex)", "") };
            try {
                StreamReader regfile =
                    new StreamReader(File.Open(REGFILE_PATH, FileMode.OpenOrCreate, FileAccess.Read));
                for (string?regex; (regex = regfile.ReadLine()) != null;) {
                    string?repl = regfile.ReadLine();
                    if (repl == null) break;
                    regexes.Add(new RegexItem(regex, repl));
                }
                regfile.Close();
            }
            catch (Exception err) { lb_msgRegex.Text = err.Message; }
            cb_savedRegex.DataSource = regexes;
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
                RegexItem item = (RegexItem)cb_savedRegex.SelectedItem!;
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
                int i = regexes.FindIndex(item => regex.CompareTo(item.regex) < 0);
                if (i == -1) {
                    regexes.Add(new RegexItem(regex, repl));
                    noevent_cb_savedRegex = true;
                    cb_savedRegex.DataSource = null;
                    cb_savedRegex.DataSource = regexes;
                    cb_savedRegex.DisplayMember = "regex";
                    cb_savedRegex.SelectedIndex = regexes.Count - 1;
                    noevent_cb_savedRegex = false;
                }
                else {
                    regexes.Insert(i, new RegexItem(regex, repl));
                    noevent_cb_savedRegex = true;
                    cb_savedRegex.DataSource = null;
                    cb_savedRegex.DataSource = regexes;
                    cb_savedRegex.DisplayMember = "regex";
                    cb_savedRegex.SelectedIndex = i;
                    noevent_cb_savedRegex = false;
                }
                b_saveRegex.Text = "Delete";
            }
            else {
                regexes.RemoveAt(cb_savedRegex.SelectedIndex);
                noevent_cb_savedRegex = true;
                cb_savedRegex.DataSource = null;
                cb_savedRegex.DataSource = regexes;
                cb_savedRegex.DisplayMember = "regex";
                cb_savedRegex.SelectedIndex = 0;
                noevent_cb_savedRegex = false;
                b_saveRegex.Text = "Save";
            }

            try {
                StreamWriter regfile = new StreamWriter(File.Open(REGFILE_PATH, FileMode.Create, FileAccess.Write));
                foreach (RegexItem item in regexes.Skip(1)) {
                    regfile.WriteLine(item.regex);
                    regfile.WriteLine(item.repl);
                }
                regfile.Close();
            }
            catch (Exception err) { lb_msgRegex.Text = err.Message; }
        }
    }
}
