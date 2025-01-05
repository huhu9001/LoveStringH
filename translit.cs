using System;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LoveStringH {
    public partial class Transliterator {
        public class RegexItem {
            public readonly Regex re;
            public readonly MatchEvaluator me;

            readonly string s_replace;
            string m_replaceS(Match m) {
                string m_replace_dollar(Match mm) {
                    return m.Groups[int.Parse(mm.Groups[1].Value)].Value;
                }
                return Regex.Replace(s_replace, "\\$(\\d+)", m_replace_dollar);
            }

            readonly Dictionary<string, string> dict_replace;
            string m_replaceD(Match m) {
                if (dict_replace.ContainsKey(m.Value)) return dict_replace[m.Value];
                else return null;
            }

            public RegexItem(string regexpr, MatchEvaluator replace) {
                re = new Regex(regexpr);
                me = replace;
            }
            public RegexItem(string regexpr, string replace) {
                re = new Regex(regexpr);
                s_replace = replace;
                me = m_replaceS;
            }
            public RegexItem(string regexpr, Dictionary<string, string> replace) {
                re = new Regex(regexpr);
                dict_replace = replace;
                me = m_replaceD;
            }
        }

        public string name { get; }
		
        public Transliterator(string name, RegexItem[] items) {
            this.name = name;
            tData = items;
        }

        public string GetTranslit(string s) {
            StringBuilder result = new StringBuilder();

            int i0 = 0;
            bool emptyMatch = false;
            Match m;

            while (i0 < s.Length) {
                string ss = null;
                foreach (RegexItem td in tData) {
                    m = td.re.Match(s, i0);
                    if (m.Success && m.Index == i0) {
                        if (m.Length == 0) {
                            if (!emptyMatch) {
                                ss = td.me(m);
                                if (ss != null) {
                                    emptyMatch = true;
                                    result.Append(ss);
                                    break;
                                }
                            }
                        }
                        else {
                            ss = td.me(m);
                            if (ss != null) {
                                emptyMatch = false;
                                i0 += m.Length;
                                result.Append(ss);
                                break;
                            }
                        }
                    }
                }
                if (ss == null) {
                    result.Append(s[i0]);
                    ++i0;
                }
            }
            return result.ToString();
        }

        private readonly RegexItem[] tData;
    }
}
