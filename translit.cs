using System;
using System.Windows.Forms;
using System.Text;

namespace LoveStringH {
    public abstract class Transliterator {
        public abstract string GetTranslit(string s);
        public abstract int GetStopper(string s);
    }

    public partial class Form1 : Form {
        struct TransliteratorItem {
            public string name { get; set; }
            public Transliterator t;
        }
        TransliteratorItem[] TransliteratorItems = new TransliteratorItem[] {
            new TransliteratorItem { name = "Greek", t = new TransliteratorGreek() },
            new TransliteratorItem { name = "Cyrillic", t = new TransliteratorCyrillic() },
            new TransliteratorItem { name = "Arabic", t = new TransliteratorArabic() },
            new TransliteratorItem { name = "Hangul", t = new TransliteratorHangul() },
        };

        private void tb_roman_TextChanged(object sender, EventArgs e) {
            Transliterator t = ((TransliteratorItem)cb_transliterator.SelectedItem).t;
            StringBuilder s_result = new StringBuilder();
            string s_this = "";
            int len_thisCut = 0;
            foreach (char c in string.Concat(tb_roman.Text, '\0')) {
                s_this = string.Concat(s_this, c);
                len_thisCut = t.GetStopper(s_this);
                while (len_thisCut > 0) {
                    s_result.Append(t.GetTranslit(s_this.Substring(0, len_thisCut)));
                    s_this = s_this.Substring(len_thisCut);
                    if (s_this == "") break;
                    len_thisCut = t.GetStopper(s_this);
                }
            }
            tb_nonroman.Text = s_result.ToString();
        }
    }
}
