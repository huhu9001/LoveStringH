using System.Text;
using System.Text.RegularExpressions;

namespace LoveStringH {
    public class Transliterator {
        public static readonly Transliterator[] all = new Transliterator[] {
            new Transliterator("Latin", TrLatin.RegexItems),
            new Transliterator("Greek", TrGreek.RegexItems),
            new Transliterator("Cyrillic", TrCyrillic.RegexItems),
            new Transliterator("Arabic", TrArabic.RegexItems),
            new Transliterator("Hangul", TrHangul.RegexItems),
        };

        public class RegexItem {
            public readonly Regex re;
            public readonly Func<Match, string?> me;

            readonly string s_replace;
            string m_replaceS(Match m) {
                return Regex.Replace(s_replace, "\\$(\\d+)", mm =>
                    m.Groups[int.Parse(mm.Groups[1].ValueSpan)].Value);
            }

            readonly Dictionary<string, string> dict_replace;
            string?m_replaceD(Match m) {
                return dict_replace.GetValueOrDefault(m.Value);
            }

            public RegexItem(string regexpr, Func<Match, string?> replace) {
                re = new Regex($"\\G{regexpr}"); me = replace;
                s_replace = ""; dict_replace = new();
            }
            public RegexItem(string regexpr, string replace) {
                re = new Regex($"\\G{regexpr}"); me = m_replaceS;
                s_replace = replace; dict_replace = new();
            }
            public RegexItem(string regexpr, Dictionary<string, string> replace) {
                re = new Regex($"\\G{regexpr}"); me = m_replaceD;
                s_replace = ""; dict_replace = replace;
            }
        }

        public string name { get; }
		
        Transliterator(string name, RegexItem[] items) {
            this.name = name;
            tData = items;
        }

        public string GetTranslit(string s) {
            StringBuilder result = new StringBuilder();

            int i0 = 0;
            bool emptyMatch = false;
            Match m;

            while (i0 < s.Length) {
                string?ss = null;
                foreach (RegexItem td in tData) {
                    m = td.re.Match(s, i0);
                    if (m.Success) {
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
