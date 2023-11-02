using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LoveStringH {
    public class TransliteratorHangul : Transliterator {
        const string pat_initial_2 = "kk|tt|pp|ss|jj|ch";
        const string pat_initial_1 = "[gkndtrlmbpsjch]";
        const string pat_initial = pat_initial_2 + "|" + pat_initial_1 + "|";
        const string pat_median = "ae?|(?<![yw])(?:eu|ui|o[ei])|eo?|i|o|(?<![w])u";
        const string pat_coda_2 = "k[ksh]|n[jhg]|th|l[kgmbstph]|p[sh]|ss|ch";
        const string pat_coda_1 = pat_initial_1;
        const string pat_coda = pat_coda_2 + "|" + pat_coda_1 + "|";
        const string pat_jamo = "n[ntsz]|l[xdzq]|lps|m[psz]|ps?[kt]|p[ct]h|s[kntp]|sch|ng[qsz]|fh|hh|[wfvzyq]|";

        public TransliteratorHangul() {
            tData = new RegexItem[] {
                new RegexItem(string.Concat("(", pat_initial, ")([yw]?(?:", pat_median, "))(l)(?=[yw]?(?:", pat_median,"))"), GetSyllable),
                new RegexItem(string.Concat("(", pat_initial, ")([yw]?(?:", pat_median, "))(?=(?:", pat_initial_2, "|", pat_initial_1, ")[yw]?(?:", pat_median,"))"), GetSyllable),
                new RegexItem(string.Concat("(", pat_initial, ")([yw]?(?:", pat_median, "))(", pat_coda_1, ")(?=(?:", pat_initial_2, "|", pat_initial_1, ")[yw]?(?:", pat_median,"))"), GetSyllable),
                new RegexItem(string.Concat("(", pat_initial, ")([yw]?(?:", pat_median, "))(", pat_coda, ")'?"), GetSyllable),
                new RegexItem("...", new Dictionary<string, string> {
                    { "lps", "ㅫ" },
                    { "psk", "ㅴ" },
                    { "pst", "ㅵ" },
                    { "pch", "ㅶ" },
                    { "pth", "ㅷ" },
                    { "sch", "ㅾ" },
                    { "ngq", "ㆁ" },
                    { "ngs", "ㆂ" },
                    { "ngz", "ㆃ" },
                }),
                new RegexItem("..", new Dictionary<string, string> {
                    { "kk", "ㄲ" },
                    { "tt", "ㄸ" },
                    { "pp", "ㅃ" },
                    { "ss", "ㅆ" },
                    { "ng", "ㅇ" },
                    { "jj", "ㅉ" },
                    { "ch", "ㅊ" },

                    { "ks", "ㄳ" },
                    { "nj", "ㄵ" },
                    { "nh", "ㄶ" },
                    { "lk", "ㄺ" }, { "lg", "ㄺ" },
                    { "lm", "ㄻ" },
                    { "lb", "ㄼ" },
                    { "ls", "ㄽ" },
                    { "lt", "ㄾ" },
                    { "lp", "ㄿ" },
                    { "lh", "ㅀ" },
                    { "ps", "ㅄ" },
                
                    { "nn", "ㅥ" },
                    { "nt", "ㅦ" },
                    { "ns", "ㅧ" },
                    { "nz", "ㅨ" },
                    { "lx", "ㅩ" },
                    { "ld", "ㅪ" },
                    { "lz", "ㅬ" },
                    { "lq", "ㅭ" },
                    { "mp", "ㅮ" },
                    { "ms", "ㅯ" },
                    { "mz", "ㅰ" },
                    { "pk", "ㅲ" },
                    { "pt", "ㅳ" },
                    { "sk", "ㅺ" },
                    { "sn", "ㅻ" },
                    { "st", "ㅼ" },
                    { "sp", "ㅽ" },
                    { "fh", "ㆄ" },
                    { "hh", "ㆅ" },
                }),
                new RegexItem(".", new Dictionary<string, string> {
                    { "g", "ㄱ" },
                    { "n", "ㄴ" },
                    { "d", "ㄷ" },
                    { "r", "ㄹ" }, { "l", "ㄹ" },
                    { "m", "ㅁ" },
                    { "b", "ㅂ" },
                    { "s", "ㅅ" },
                    { "j", "ㅈ" },
                    { "c", "ㅊ" }, 
                    { "k", "ㅋ" },
                    { "t", "ㅌ" },
                    { "p", "ㅍ" },
                    { "h", "ㅎ" },
                
                    { "w", "ㅱ" },
                    { "f", "ㅸ" },
                    { "v", "ㅹ" },
                    { "z", "ㅿ" },
                    { "y", "ㆀ" },
                    { "q", "ㆆ" },
                }),
            };
        }

        string GetSyllable(Match m) {int i_initial;
            switch (m.Groups[1].Value) {
                default: return null;
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
                default: return null;
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
                case "oe": case "oi": i_median = 11; break;
                case "yo": i_median = 12; break;
                case "u": i_median = 13; break;
                case "wo": case "weo": i_median = 14; break;
                case "we": i_median = 15; break;
                case "wi": i_median = 16; break;
                case "yu": i_median = 17; break;
                case "eu": i_median = 18; break;
                case "ui": case "yi": i_median = 19; break;
                case "i": i_median = 20; break;
            }

            int i_coda;
            switch (m.Groups[3].Value) {
                default: return null;
                case "": i_coda = 0; break;
                case "k": case "g": i_coda = 1; break;
                case "kk": i_coda = 2; break;
                case "ks": i_coda = 3; break;
                case "n": i_coda = 4; break;
                case "nj": i_coda = 5; break;
                case "nh": i_coda = 6; break;
                case "t": case "d": i_coda = 7; break;
                case "l": case "r": i_coda = 8; break;
                case "lk": case "lg": i_coda = 9; break;
                case "lm": i_coda = 10; break;
                case "lb": i_coda = 11; break;
                case "ls": i_coda = 12; break;
                case "lt": i_coda = 13; break;
                case "lp": i_coda = 14; break;
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
    }
}