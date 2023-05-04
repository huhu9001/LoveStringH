using System.Text.RegularExpressions;

namespace LoveStringH {
    public class TransliteratorHangul : Transliterator {
        public override string GetTranslit(string s) {
            s = s.ToLower();

            Match m = Regex.Match(s, "^([hgkndtmbpsjcrl]{0,2})([aeiouyw]{1,3})([hgkndtmbpsjcrl]{0,2})'?$");
            if (!m.Success) switch (s) {
                    default: return s;
                    case "h": return "ㅎ";
                    case "g": return "ㄱ";
                    case "k": return "ㅋ";
                    case "n": return "ㄴ";
                    case "d": return "ㄷ";
                    case "t": return "ㅌ";
                    case "m": return "ㅁ";
                    case "b": return "ㅂ";
                    case "p": return "ㅍ";
                    case "s": return "ㅅ";
                    case "j": return "ㅈ";
                    case "c": case "ch": return "ㅊ";
                    case "r": case "l": return "ㄹ";
                }

            int i_initial;
            switch (m.Groups[1].Value) {
                default: return s;
                case "g": i_initial = 0; break;
                case "kk": i_initial = 1; break;
                case "n": i_initial = 2; break;
                case "d": i_initial = 3; break;
                case "tt": i_initial = 4; break;
                case "r": case "l": i_initial = 5; break;
                case "m": i_initial = 6; break;
                case "b": i_initial = 7; break;
                case "pp": i_initial = 8; break;
                case "s": i_initial = 9; break;
                case "ss": i_initial = 10; break;
                case "": i_initial = 11; break;
                case "j": i_initial = 12; break;
                case "jj": i_initial = 13; break;
                case "c": case "ch": i_initial = 14; break;
                case "k": i_initial = 15; break;
                case "t": i_initial = 16; break;
                case "p": i_initial = 17; break;
                case "h": i_initial = 18; break;
            }

            int i_median;
            switch (m.Groups[2].Value) {
                default: return s;
                case "a": i_median = 0; break;
                case "ae": i_median = 1; break;
                case "ya": i_median = 2; break;
                case "yae": i_median = 3; break;
                case "eo": i_median = 4; break;
                case "e": i_median = 5; break;
                case "yeo": i_median = 6; break;
                case "ye": i_median = 7; break;
                case "o": i_median = 8; break;
                case "wa": i_median = 9; break;
                case "wae": i_median = 10; break;
                case "oe": i_median = 11; break;
                case "yo": i_median = 12; break;
                case "u": i_median = 13; break;
                case "weo": i_median = 14; break;
                case "we": i_median = 15; break;
                case "wi": i_median = 16; break;
                case "yu": i_median = 17; break;
                case "eu": i_median = 18; break;
                case "ui": case "yi": i_median = 19; break;
                case "i": i_median = 20; break;
            }

            int i_coda;
            switch (m.Groups[3].Value) {
                default: return s;
                case "": i_coda = 0; break;
                case "k": case "g": i_coda = 1; break;
                case "kk": i_coda = 2; break;
                case "ks": i_coda = 3; break;
                case "n": i_coda = 4; break;
                case "nj": i_coda = 5; break;
                case "nh": i_coda = 6; break;
                case "t": case "d": i_coda = 7; break;
                case "l": case "r": i_coda = 8; break;
                case "lk": i_coda = 9; break;
                case "lm": i_coda = 10; break;
                case "lp": i_coda = 11; break;
                case "ls": i_coda = 12; break;
                case "lth": i_coda = 13; break;
                case "lph": i_coda = 14; break;
                case "lh": i_coda = 15; break;
                case "m": i_coda = 16; break;
                case "p": case "b": i_coda = 17; break;
                case "ps": i_coda = 18; break;
                case "s": i_coda = 19; break;
                case "ss": i_coda = 20; break;
                case "ng": i_coda = 21; break;
                case "j": i_coda = 22; break;
                case "ch": case "c": i_coda = 23; break;
                case "kh": i_coda = 24; break;
                case "th": i_coda = 25; break;
                case "ph": i_coda = 26; break;
                case "h": i_coda = 27; break;
            }

            return ((char)(0xAC00 + i_initial * 0x24C + i_median * 0x1C + i_coda)).ToString();
        }

        public override int GetStopper(string s) {
            s = s.ToLower();
            if (s.EndsWith("\'")) return s.Length;
            if (!Regex.Match(s[0].ToString(), "[aeiouywhgkndtmbpsjcrl]").Success) return 1;
            if (!Regex.Match(s, "[aeiouyw]").Success) {
                if (s.Length > 1) if (s[0] != s[1]) if (s != "ch") return 1;
                if (s.Length > 2) return 1;
            }
            Match m = Regex.Match(s, "^([hgkndtmbpsjcrl]{0,2}[yw]{0,1})([aeiou]{1,2})([hgkndtmbpsjcrl]{0,2})(.)");
            if (m.Success) {
                if (m.Groups[3].Length < 2 && Regex.Match(m.Groups[4].Value, "[hgkndtmbpsjcrl]").Success) return 0;
                if (m.Groups[3].Length >= 2 && m.Groups[3].Value[1] == 'c') return m.Groups[0].Length - 2;
                if (Regex.Match(m.Groups[4].Value, "[aeiouyw]").Success) {
                    if (m.Groups[3].Value == "ch") return m.Groups[0].Length - 3;
                    if (m.Groups[3].Length > 0) return m.Groups[0].Length - 2;
                    switch (string.Concat(m.Groups[2].Value, m.Groups[4].Value)) {
                        default: return m.Groups[0].Length - 1;
                        case "ae":
                        case "eo":
                        case "eu":
                        case "oe":
                        case "ui": return 0;
                    }
                }
                return m.Groups[0].Length - 1;
            }
            return 0;
        }
    }
}