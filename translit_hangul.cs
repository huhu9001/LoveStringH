using System.Text.RegularExpressions;
using System.Collections.Generic;
using static LoveStringH.Transliterator;

namespace LoveStringH {
    public class TrHangul {
        const string pat_initial_2 = "kk|tt|pp|ss|jj|ch";
        const string pat_initial_1 = "[gkndtrlmbpsjchxf]";
        const string pat_initial = pat_initial_2 + "|" + pat_initial_1 + "|";
        const string pat_median = "ae?|(?<![yw])(?:eu|ui|o[ei])|eo?|i|o|(?<![w])u";
        const string pat_coda_2 = "k[ksh]|n[jhg]|th|l[kgmbstph]|p[sh]|ss|ch";
        const string pat_coda_1 = pat_initial_1;
        const string pat_coda = pat_coda_2 + "|" + pat_coda_1 + "|";
        const string pat_jamo = "n[ntsz]|l[xdzq]|lps|m[psz]|ps?[kt]|p[ct]h|s[kntp]|sch|ng[qsz]|fh|hh|[wfvzyq]|";

        public static readonly RegexItem[] RegexItems = new RegexItem[] {
            new RegexItem(string.Concat("(", pat_initial, ")([yw]?(?:", pat_median, "))(l)(?=[yw]?(?:", pat_median,"))"), GetSyllable),
            new RegexItem(string.Concat("(", pat_initial, ")([yw]?(?:", pat_median, "))()(?=(?:", pat_initial_2, "|", pat_initial_1, ")[yw]?(?:", pat_median,"))"), GetSyllable),
            new RegexItem(string.Concat("(", pat_initial, ")([yw]?(?:", pat_median, "))(", pat_coda_1, ")(?=(?:", pat_initial_2, "|", pat_initial_1, ")[yw]?(?:", pat_median,"))"), GetSyllable),
            new RegexItem(string.Concat("(", pat_initial, ")([yw]?(?:", pat_median, "))(", pat_coda, ")'?"), GetSyllable),
            new RegexItem("([B-DF-HJ-NP-TXZ]*)([AEIOUVWY]+)([B-DF-HJ-NP-TXZ]*)([012]?)'?", GetSyllableArchaic),
            new RegexItem("...", new Dictionary<string, string> {
                { "lps", "\u316B" },
                { "psk", "\u3174" },
                { "pst", "\u3175" },
                { "pch", "\u3176" },
                { "pth", "\u3177" },
                { "sch", "\u317E" },
                { "ngq", "\u3181" },
                { "ngs", "\u3182" },
                { "ngz", "\u3183" },
            }),
            new RegexItem("..", new Dictionary<string, string> {
                { "kk", "\u3132" },
                { "tt", "\u3138" },
                { "pp", "\u3143" },
                { "ss", "\u3146" },
                { "ng", "\u3147" },
                { "jj", "\u3149" },
                { "ch", "\u314A" },

                { "ks", "\u3133" },
                { "nj", "\u3135" },
                { "nh", "\u3136" },
                { "lk", "\u313A" }, { "lg", "\u313A" },
                { "lm", "\u313B" },
                { "lb", "\u313C" },
                { "ls", "\u313D" },
                { "lt", "\u313E" },
                { "lp", "\u313F" },
                { "lh", "\u3140" },
                { "ps", "\u3144" },

                { "nn", "\u3165" },
                { "nt", "\u3166" },
                { "ns", "\u3167" },
                { "nz", "\u3168" },
                { "lx", "\u3169" },
                { "ld", "\u316A" },
                { "lz", "\u316C" },
                { "lq", "\u316D" },
                { "mp", "\u316E" },
                { "ms", "\u316F" },
                { "mz", "\u3170" },
                { "pk", "\u3172" },
                { "pt", "\u3173" },
                { "sk", "\u317A" },
                { "sn", "\u317B" },
                { "st", "\u317C" },
                { "sp", "\u317D" },
                { "fh", "\u3184" },
                { "hh", "\u3185" },
            }),
            new RegexItem(".", new Dictionary<string, string> {
                { "g", "\u3131" },
                { "n", "\u3134" },
                { "d", "\u3137" },
                { "r", "\u3139" }, { "l", "\u3139" },
                { "m", "\u3141" },
                { "b", "\u3142" },
                { "s", "\u3145" },
                { "j", "\u3148" },
                { "c", "\u314A" },
                { "k", "\u314B" },
                { "t", "\u314C" },
                { "p", "\u314D" },
                { "h", "\u314E" },

                { "w", "\u3171" },
                { "f", "\u3178" },
                { "v", "\u3179" },
                { "z", "\u317F" },
                { "y", "\u3180" },
                { "q", "\u3186" },
            }),
        };

        static string?GetSyllable(Match m) {
            int initial;
            switch (m.Groups[1].Value) {
                default: return null;
                case "g": initial = 0; break;
                case "kk": initial = 1; break;
                case "n": initial = 2; break;
                case "d": initial = 3; break;
                case "tt": initial = 4; break;
                case "r": case "l": initial = 5; break;
                case "m": initial = 6; break;
                case "b": initial = 7; break;
                case "pp": initial = 8; break;
                case "s": initial = 9; break;
                case "ss": initial = 10; break;
                case "": initial = 11; break;
                case "j": initial = 12; break;
                case "jj": initial = 13; break;
                case "c": case "ch": initial = 14; break;
                case "k": case "x": initial = 15; break;
                case "t": initial = 16; break;
                case "p": case "f": initial = 17; break;
                case "h": initial = 18; break;
            }

            int median;
            switch (m.Groups[2].Value) {
                default: return null;
                case "a": median = 0; break;
                case "ae": median = 1; break;
                case "ya": median = 2; break;
                case "yae": median = 3; break;
                case "eo": median = 4; break;
                case "e": median = 5; break;
                case "yeo": median = 6; break;
                case "ye": median = 7; break;
                case "o": median = 8; break;
                case "wa": median = 9; break;
                case "wae": median = 10; break;
                case "oe": case "oi": median = 11; break;
                case "yo": median = 12; break;
                case "u": median = 13; break;
                case "wo": case "weo": median = 14; break;
                case "we": median = 15; break;
                case "wi": median = 16; break;
                case "yu": median = 17; break;
                case "eu": median = 18; break;
                case "ui": case "yi": median = 19; break;
                case "i": median = 20; break;
            }

            int coda;
            switch (m.Groups[3].Value) {
                default: return null;
                case "": coda = 0; break;
                case "k": case "g": coda = 1; break;
                case "kk": coda = 2; break;
                case "ks": coda = 3; break;
                case "n": coda = 4; break;
                case "nj": coda = 5; break;
                case "nh": coda = 6; break;
                case "d": coda = 7; break;
                case "l": case "r": coda = 8; break;
                case "lk": case "lg": coda = 9; break;
                case "lm": coda = 10; break;
                case "lb": coda = 11; break;
                case "ls": coda = 12; break;
                case "lt": coda = 13; break;
                case "lp": coda = 14; break;
                case "lh": coda = 15; break;
                case "m": coda = 16; break;
                case "p": case "b": coda = 17; break;
                case "ps": coda = 18; break;
                case "s": coda = 19; break;
                case "ss": coda = 20; break;
                case "ng": coda = 21; break;
                case "j": coda = 22; break;
                case "ch": case "c": coda = 23; break;
                case "x": case "kh": coda = 24; break;
                case "t": case "th": coda = 25; break;
                case "f": case "ph": coda = 26; break;
                case "h": coda = 27; break;
            }

            return ((char)(0xAC00 + initial * 0x24C + median * 0x1C + coda)).ToString();
        }

        static string?GetSyllableArchaic(Match m) {
            string initial;
            switch (m.Groups[1].Value) {
                default: return null;
                case "K": initial = "\u1100"; break;
                case "G": initial = "\u1101"; break;
                case "N": initial = "\u1102"; break;
                case "T": initial = "\u1103"; break;
                case "D": initial = "\u1104"; break;
                case "L": initial = "\u1105"; break;
                case "M": initial = "\u1106"; break;
                case "P": initial = "\u1107"; break;
                case "B": initial = "\u1108"; break;
                case "S": initial = "\u1109"; break;
                case "SS": initial = "\u110a"; break;
                case "": initial = "\u110b"; break;
                case "J": initial = "\u110c"; break;
                case "DG": initial = "\u110d"; break;
                case "C": initial = "\u110e"; break;
                case "KH": initial = "\u110f"; break;
                case "TH": initial = "\u1110"; break;
                case "PH": initial = "\u1111"; break;
                case "H": initial = "\u1112"; break;
                case "NK": initial = "\u1113"; break;
                case "NN": initial = "\u1114"; break;
                case "NT": initial = "\u1115"; break;
                case "NP": initial = "\u1116"; break;
                case "TK": initial = "\u1117"; break;
                case "LN": initial = "\u1118"; break;
                case "LL": initial = "\u1119"; break;
                case "LH": initial = "\u111a"; break;
                case "R": initial = "\u111b"; break;
                case "MP": initial = "\u111c"; break;
                case "MF": initial = "\u111d"; break;
                case "PK": initial = "\u111e"; break;
                case "PN": initial = "\u111f"; break;
                case "PT": initial = "\u1120"; break;
                case "PS": initial = "\u1121"; break;
                case "PSK": initial = "\u1122"; break;
                case "PST": initial = "\u1123"; break;
                case "PSP": initial = "\u1124"; break;
                case "PSS": initial = "\u1125"; break;
                case "PSJ": initial = "\u1126"; break;
                case "PJ": initial = "\u1127"; break;
                case "PC": initial = "\u1128"; break;
                case "PTH": initial = "\u1129"; break;
                case "PPH": initial = "\u112a"; break;
                case "F": initial = "\u112b"; break;
                case "FF": initial = "\u112c"; break;
                case "SK": initial = "\u112d"; break;
                case "SN": initial = "\u112e"; break;
                case "ST": initial = "\u112f"; break;
                case "SL": initial = "\u1130"; break;
                case "SM": initial = "\u1131"; break;
                case "SP": initial = "\u1132"; break;
                case "SPK": initial = "\u1133"; break;
                case "SSS": initial = "\u1134"; break;
                case "SNG": initial = "\u1135"; break;
                case "SJ": initial = "\u1136"; break;
                case "SC": initial = "\u1137"; break;
                case "SKH": initial = "\u1138"; break;
                case "STH": initial = "\u1139"; break;
                case "SPH": initial = "\u113a"; break;
                case "SH": initial = "\u113b"; break;
                case "SX": initial = "\u113c"; break;
                case "ZX": initial = "\u113d"; break;
                case "SG": initial = "\u113e"; break;
                case "ZG": initial = "\u113f"; break;
                case "Z": initial = "\u1140"; break;
                case "NGK": initial = "\u1141"; break;
                case "NGT": initial = "\u1142"; break;
                case "NGM": initial = "\u1143"; break;
                case "NGP": initial = "\u1144"; break;
                case "NGS": initial = "\u1145"; break;
                case "NGZ": initial = "\u1146"; break;
                case "NNG": initial = "\u1147"; break;
                case "NGJ": initial = "\u1148"; break;
                case "NGC": initial = "\u1149"; break;
                case "NGTH": initial = "\u114a"; break;
                case "NGPH": initial = "\u114b"; break;
                case "NG": initial = "\u114c"; break;
                case "JNG": initial = "\u114d"; break;
                case "TSX": initial = "\u114e"; break;
                case "DZX": initial = "\u114f"; break;
                case "TSG": initial = "\u1150"; break;
                case "DZG": initial = "\u1151"; break;
                case "CKH": initial = "\u1152"; break;
                case "CH": initial = "\u1153"; break;
                case "TXH": initial = "\u1154"; break;
                case "TGH": initial = "\u1155"; break;
                case "PHP": initial = "\u1156"; break;
                case "FH": initial = "\u1157"; break;
                case "HH": initial = "\u1158"; break;
                case "Q": initial = "\u1159"; break;
                case "KT": initial = "\u115a"; break;
                case "NS": initial = "\u115b"; break;
                case "NJ": initial = "\u115c"; break;
                case "NH": initial = "\u115d"; break;
                case "TL": initial = "\u115e"; break;
					
                case "TM": initial = "\ua960"; break;
                case "TP": initial = "\ua961"; break;
                case "TS": initial = "\ua962"; break;
                case "TJ": initial = "\ua963"; break;
                case "LK": initial = "\ua964"; break;
                case "LG": initial = "\ua965"; break;
                case "LT": initial = "\ua966"; break;
                case "LD": initial = "\ua967"; break;
                case "LM": initial = "\ua968"; break;
                case "LP": initial = "\ua969"; break;
                case "LB": initial = "\ua96a"; break;
                case "LF": initial = "\ua96b"; break;
                case "LS": initial = "\ua96c"; break;
                case "LJ": initial = "\ua96d"; break;
                case "LKH": initial = "\ua96e"; break;
                case "MK": initial = "\ua96f"; break;
                case "MT": initial = "\ua970"; break;
                case "MS": initial = "\ua971"; break;
                case "PSTH": initial = "\ua972"; break;
                case "PKH": initial = "\ua973"; break;
                case "BH": initial = "\ua974"; break;
                case "SSP": initial = "\ua975"; break;
                case "NGL": initial = "\ua976"; break;
                case "NGH": initial = "\ua977"; break;
                case "DGH": initial = "\ua978"; break;
                case "TTH": initial = "\ua979"; break;
                case "PHH": initial = "\ua97a"; break;
                case "HS": initial = "\ua97b"; break;
                case "QQ": initial = "\ua97c"; break;
            }

            string median;
            switch (m.Groups[2].Value) {
                default: return null;
                case "A": median = "\u1161"; break;
                case "AI": median = "\u1162"; break;
                case "YA": median = "\u1163"; break;
                case "YAI": median = "\u1164"; break;
                case "E": median = "\u1165"; break;
                case "EI": median = "\u1166"; break;
                case "YE": median = "\u1167"; break;
                case "YEI": median = "\u1168"; break;
                case "O": median = "\u1169"; break;
                case "OA": median = "\u116a"; break;
                case "OAI": median = "\u116b"; break;
                case "OI": median = "\u116c"; break;
                case "YO": median = "\u116d"; break;
                case "U": median = "\u116e"; break;
                case "UE": median = "\u116f"; break;
                case "UEI": median = "\u1170"; break;
                case "UI": median = "\u1171"; break;
                case "YU": median = "\u1172"; break;
                case "W": median = "\u1173"; break;
                case "WI": median = "\u1174"; break;
                case "I": median = "\u1175"; break;
                case "AO": median = "\u1176"; break;
                case "AU": median = "\u1177"; break;
                case "YAO": median = "\u1178"; break;
                case "YAYO": median = "\u1179"; break;
                case "EO": median = "\u117a"; break;
                case "EU": median = "\u117b"; break;
                case "EW": median = "\u117c"; break;
                case "YEO": median = "\u117d"; break;
                case "YEU": median = "\u117e"; break;
                case "OE": median = "\u117f"; break;
                case "OEI": median = "\u1180"; break;
                case "OYEI": median = "\u1181"; break;
                case "OO": median = "\u1182"; break;
                case "OU": median = "\u1183"; break;
                case "YOYA": median = "\u1184"; break;
                case "YOYAI": median = "\u1185"; break;
                case "YOYE": median = "\u1186"; break;
                case "YOO": median = "\u1187"; break;
                case "YOI": median = "\u1188"; break;
                case "UA": median = "\u1189"; break;
                case "UAI": median = "\u118a"; break;
                case "UEW": median = "\u118b"; break;
                case "UYEI": median = "\u118c"; break;
                case "UU": median = "\u118d"; break;
                case "YUA": median = "\u118e"; break;
                case "YUE": median = "\u118f"; break;
                case "YUEI": median = "\u1190"; break;
                case "YUYE": median = "\u1191"; break;
                case "YUYEI": median = "\u1192"; break;
                case "YUU": median = "\u1193"; break;
                case "YUI": median = "\u1194"; break;
                case "WU": median = "\u1195"; break;
                case "WW": median = "\u1196"; break;
                case "WIU": median = "\u1197"; break;
                case "IA": median = "\u1198"; break;
                case "IYA": median = "\u1199"; break;
                case "IO": median = "\u119a"; break;
                case "IU": median = "\u119b"; break;
                case "IW": median = "\u119c"; break;
                case "IV": median = "\u119d"; break;
                case "V": median = "\u119e"; break;
                case "VE": median = "\u119f"; break;
                case "VU": median = "\u11a0"; break;
                case "VI": median = "\u11a1"; break;
                case "VV": median = "\u11a2"; break;
                case "AW": median = "\u11a3"; break;
                case "YAU": median = "\u11a4"; break;
                case "YEYA": median = "\u11a5"; break;
                case "OYA": median = "\u11a6"; break;
                case "OYAI": median = "\u11a7"; break;
					
                case "OYE": median = "\ud7b0"; break;
                case "OOI": median = "\ud7b1"; break;
                case "YOA": median = "\ud7b2"; break;
                case "YOAE": median = "\ud7b3"; break;
                case "YOE": median = "\ud7b4"; break;
                case "UYE": median = "\ud7b5"; break;
                case "UII": median = "\ud7b6"; break;
                case "YUAI": median = "\ud7b7"; break;
                case "YUO": median = "\ud7b8"; break;
                case "WA": median = "\ud7b9"; break;
                case "WE": median = "\ud7ba"; break;
                case "WEI": median = "\ud7bb"; break;
                case "WO": median = "\ud7bc"; break;
                case "IYAO": median = "\ud7bd"; break;
                case "IYAI": median = "\ud7be"; break;
                case "IYE": median = "\ud7bf"; break;
                case "IYEI": median = "\ud7c0"; break;
                case "IOI": median = "\ud7c1"; break;
                case "IYO": median = "\ud7c2"; break;
                case "IYU": median = "\ud7c3"; break;
                case "II": median = "\ud7c4"; break;
                case "VA": median = "\ud7c5"; break;
                case "VEI": median = "\ud7c6"; break;
            }

            string coda;
            switch (m.Groups[3].Value) {
                default: return null;
                case "": coda = ""; break;
                case "K": coda = "\u11a8"; break;
                case "G": coda = "\u11a9"; break;
                case "KS": coda = "\u11aa"; break;
                case "N": coda = "\u11ab"; break;
                case "NJ": coda = "\u11ac"; break;
                case "NH": coda = "\u11ad"; break;
                case "T": coda = "\u11ae"; break;
                case "L": coda = "\u11af"; break;
                case "LK": coda = "\u11b0"; break;
                case "LM": coda = "\u11b1"; break;
                case "LP": coda = "\u11b2"; break;
                case "LS": coda = "\u11b3"; break;
                case "LTH": coda = "\u11b4"; break;
                case "LPH": coda = "\u11b5"; break;
                case "LH": coda = "\u11b6"; break;
                case "M": coda = "\u11b7"; break;
                case "P": coda = "\u11b8"; break;
                case "PS": coda = "\u11b9"; break;
                case "S": coda = "\u11ba"; break;
                case "SS": coda = "\u11bb"; break;
                case "QNG": coda = "\u11bc"; break;
                case "J": coda = "\u11bd"; break;
                case "C": coda = "\u11be"; break;
                case "KH": coda = "\u11bf"; break;
                case "TH": coda = "\u11c0"; break;
                case "PH": coda = "\u11c1"; break;
                case "H": coda = "\u11c2"; break;
                case "KL": coda = "\u11c3"; break;
                case "KSK": coda = "\u11c4"; break;
                case "NK": coda = "\u11c5"; break;
                case "NT": coda = "\u11c6"; break;
                case "NS": coda = "\u11c7"; break;
                case "NZ": coda = "\u11c8"; break;
                case "NTH": coda = "\u11c9"; break;
                case "TK": coda = "\u11ca"; break;
                case "TL": coda = "\u11cb"; break;
                case "LKS": coda = "\u11cc"; break;
                case "LN": coda = "\u11cd"; break;
                case "LT": coda = "\u11ce"; break;
                case "LDH": coda = "\u11cf"; break;
                case "LL": coda = "\u11d0"; break;
                case "LMK": coda = "\u11d1"; break;
                case "LMS": coda = "\u11d2"; break;
                case "LPS": coda = "\u11d3"; break;
                case "LBH": coda = "\u11d4"; break;
                case "LF": coda = "\u11d5"; break;
                case "LSS": coda = "\u11d6"; break;
                case "LZ": coda = "\u11d7"; break;
                case "LKH": coda = "\u11d8"; break;
                case "LQ": coda = "\u11d9"; break;
                case "MK": coda = "\u11da"; break;
                case "ML": coda = "\u11db"; break;
                case "MP": coda = "\u11dc"; break;
                case "MS": coda = "\u11dd"; break;
                case "MSS": coda = "\u11de"; break;
                case "MZ": coda = "\u11df"; break;
                case "MC": coda = "\u11e0"; break;
                case "MH": coda = "\u11e1"; break;
                case "MF": coda = "\u11e2"; break;
                case "PL": coda = "\u11e3"; break;
                case "PPH": coda = "\u11e4"; break;
                case "BH": coda = "\u11e5"; break;
                case "F": coda = "\u11e6"; break;
                case "SK": coda = "\u11e7"; break;
                case "ST": coda = "\u11e8"; break;
                case "SL": coda = "\u11e9"; break;
                case "SP": coda = "\u11ea"; break;
                case "Z": coda = "\u11eb"; break;
                case "NGK": coda = "\u11ec"; break;
                case "NGG": coda = "\u11ed"; break;
                case "NNG": coda = "\u11ee"; break;
                case "NGKH": coda = "\u11ef"; break;
                case "NG": coda = "\u11f0"; break;
                case "NGS": coda = "\u11f1"; break;
                case "NGZ": coda = "\u11f2"; break;
                case "PHP": coda = "\u11f3"; break;
                case "FH": coda = "\u11f4"; break;
                case "HN": coda = "\u11f5"; break;
                case "HL": coda = "\u11f6"; break;
                case "HM": coda = "\u11f7"; break;
                case "HP": coda = "\u11f8"; break;
                case "Q": coda = "\u11f9"; break;
                case "KN": coda = "\u11fa"; break;
                case "KP": coda = "\u11fb"; break;
                case "KC": coda = "\u11fc"; break;
                case "KKH": coda = "\u11fd"; break;
                case "GH": coda = "\u11fe"; break;
                case "NN": coda = "\u11ff"; break;
					
                case "NL": coda = "\ud7cb"; break;
                case "NC": coda = "\ud7cc"; break;
                case "D": coda = "\ud7cd"; break;
                case "DP": coda = "\ud7ce"; break;
                case "TP": coda = "\ud7cf"; break;
                case "TS": coda = "\ud7d0"; break;
                case "TSK": coda = "\ud7d1"; break;
                case "TJ": coda = "\ud7d2"; break;
                case "TC": coda = "\ud7d3"; break;
                case "TTH": coda = "\ud7d4"; break;
                case "LG": coda = "\ud7d5"; break;
                case "LGH": coda = "\ud7d6"; break;
                case "LLKH": coda = "\ud7d7"; break;
                case "LMH": coda = "\ud7d8"; break;
                case "LPT": coda = "\ud7d9"; break;
                case "LPPH": coda = "\ud7da"; break;
                case "LNG": coda = "\ud7db"; break;
                case "LQH": coda = "\ud7dc"; break;
                case "R": coda = "\ud7dd"; break;
                case "MN": coda = "\ud7de"; break;
                case "MNN": coda = "\ud7df"; break;
                case "MM": coda = "\ud7e0"; break;
                case "MPS": coda = "\ud7e1"; break;
                case "MJ": coda = "\ud7e2"; break;
                case "PT": coda = "\ud7e3"; break;
                case "PLPH": coda = "\ud7e4"; break;
                case "PM": coda = "\ud7e5"; break;
                case "B": coda = "\ud7e6"; break;
                case "PST": coda = "\ud7e7"; break;
                case "PJ": coda = "\ud7e8"; break;
                case "PC": coda = "\ud7e9"; break;
                case "SM": coda = "\ud7ea"; break;
                case "SF": coda = "\ud7eb"; break;
                case "SSK": coda = "\ud7ec"; break;
                case "SST": coda = "\ud7ed"; break;
                case "SZ": coda = "\ud7ee"; break;
                case "SJ": coda = "\ud7ef"; break;
                case "SC": coda = "\ud7f0"; break;
                case "STH": coda = "\ud7f1"; break;
                case "SH": coda = "\ud7f2"; break;
                case "ZP": coda = "\ud7f3"; break;
                case "ZF": coda = "\ud7f4"; break;
                case "NGM": coda = "\ud7f5"; break;
                case "NGH": coda = "\ud7f6"; break;
                case "JP": coda = "\ud7f7"; break;
                case "JB": coda = "\ud7f8"; break;
                case "DG": coda = "\ud7f9"; break;
                case "PHS": coda = "\ud7fa"; break;
                case "PHTH": coda = "\ud7fb"; break;
            }

            string tone;
            switch (m.Groups[4].Value) {
                default: tone = ""; break;
                case "1": tone = "\u302e"; break;
                case "2": tone = "\u302f"; break;
            }

            return string.Concat(initial, median, coda, tone);
        }
    }
}