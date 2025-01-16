using System.Text.RegularExpressions;
using System.Collections.Generic;
using static LoveStringH.Transliterator;

namespace LoveStringH {
    public class TrGreek {
        public static readonly RegexItem[] RegexItems = new RegexItem[] {
            new RegexItem("([AHIVahiyv]|[AHV][Jj]|[ahv]j)([()])~", GetVowel2),
            new RegexItem("([AEHIOVaehioyv]|[AHV][Jj]|[ahv]j)([()])([/\\\\]?)", GetVowel1),
            new RegexItem("Y\\(([/\\\\~]?)", GetVowel3),
            new RegexItem("(?<=[A-Za-z][()~/\\\\]*)s(?=[^A-Za-z]|$)", "\u03C2"),
            new RegexItem("...", new Dictionary<string, string> {
                { "I:/", "\u0390" }, { "i:/", "\u0390" },
                { "Y:/", "\u03B0" }, { "y:/", "\u03B0" },
                { "aj/", "\u1FB4" },
                { "hj/", "\u1FC4" },
                { "vj/", "\u1FF4" },

                { "I:\\", "\u1FD2" }, { "i:\\", "\u1FD2" },
                { "Y:\\", "\u1FE2" }, { "y:\\", "\u1FE2" },
                { "aj\\", "\u1FB2" },
                { "hj\\", "\u1FC2" },
                { "vj\\", "\u1FF2" },

                { "I:~", "\u1FD7" }, { "i:~", "\u1FD7" },
                { "Y:~", "\u1FE7" }, { "y:~", "\u1FE7" },
                { "aj~", "\u1FB7" },
                { "hj~", "\u1FC7" },
                { "vj~", "\u1FF7" },
            }),
            new RegexItem("..'?", new Dictionary<string, string> {
                { "TH", "\u0398" }, { "Th", "\u0398" }, { "th", "\u03B8" },
                { "T'", "\u03A4" }, { "t'", "\u03C4" },
                { "PH", "\u03A6" }, { "Ph", "\u03A6" }, { "ph", "\u03C6" },
                { "P'", "\u03A0" }, { "p'", "\u03C0" },
                { "KH", "\u03A7" }, { "Kh", "\u03A7" }, { "kh", "\u03C7" },
                { "K'", "\u039A" }, { "k'", "\u03BA" },
                { "PS", "\u03A8" }, { "Ps", "\u03A8" }, { "ps", "\u03C8" },
                { "SH", "\u03FA" }, { "Sh", "\u03FA" }, { "sh", "\u03FB" },
                { "S'", "\u03A3" }, { "s'", "\u03C3" },

                { "I:", "\u03AA" }, { "i:", "\u03CA" },
                { "Y:", "\u03AB" }, { "y:", "\u03CB" },
                { "AJ", "\u1FBC" }, { "Aj", "\u1FBC" }, { "aj", "\u1FB3" },
                { "HJ", "\u1FCC" }, { "Hj", "\u1FCC" }, { "hj", "\u1FC3" },
                { "VJ", "\u1FFC" }, { "Vj", "\u1FFC" }, { "vj", "\u1FF3" },

                { "A/", "\u0386" }, { "a/", "\u03AC" },
                { "E/", "\u0388" }, { "e/", "\u03AD" },
                { "H/", "\u0389" }, { "h/", "\u03AE" },
                { "I/", "\u038A" }, { "i/", "\u03AF" },
                { "O/", "\u038C" }, { "o/", "\u03CC" },
                { "Y/", "\u038E" }, { "y/", "\u03CD" },
                { "V/", "\u038F" }, { "v/", "\u03CE" },
                { "A\\", "\u1FBA" }, { "a\\", "\u1F70" },
                { "E\\", "\u1FC8" }, { "e\\", "\u1F72" },
                { "H\\", "\u1FCA" }, { "h\\", "\u1F74" },
                { "I\\", "\u1FDA" }, { "i\\", "\u1F76" },
                { "O\\", "\u1FF8" }, { "o\\", "\u1F78" },
                { "Y\\", "\u1FEA" }, { "y\\", "\u1F7A" },
                { "V\\", "\u1FFA" }, { "v\\", "\u1F7C" },
                { "a~", "\u1FB6" },
                { "h~", "\u1FC6" },
                { "i~", "\u1FD6" },
                { "y~", "\u1FE6" },
                { "v~", "\u1FF6" },

                { "R)", "\u1FE4" }, { "r)", "\u1FE4" },
                { "R(", "\u1FEC" }, { "r(", "\u1FE5" },
            }),
            new RegexItem(".'?", new Dictionary<string, string> {
                { "A", "\u0391" }, { "a", "\u03B1" },
                { "B", "\u0392" }, { "b", "\u03B2" },
                { "G", "\u0393" }, { "g", "\u03B3" },
                { "D", "\u0394" }, { "d", "\u03B4" },
                { "E", "\u0395" }, { "e", "\u03B5" },
                { "Z", "\u0396" }, { "z", "\u03B6" },
                { "H", "\u0397" }, { "h", "\u03B7" },
                { "I", "\u0399" }, { "i", "\u03B9" },
                { "K", "\u039A" }, { "k", "\u03BA" },
                { "L", "\u039B" }, { "l", "\u03BB" },
                { "M", "\u039C" }, { "m", "\u03BC" },
                { "N", "\u039D" }, { "n", "\u03BD" },
                { "X", "\u039E" }, { "x", "\u03BE" },
                { "O", "\u039F" }, { "o", "\u03BF" },
                { "P", "\u03A0" }, { "p", "\u03C0" },
                { "R", "\u03A1" }, { "r", "\u03C1" },
                { "S", "\u03A3" }, { "s", "\u03C3" },
                { "T", "\u03A4" }, { "t", "\u03C4" },
                { "Y", "\u03A5" }, { "y", "\u03C5" },
                { "V", "\u03A9" }, { "v", "\u03C9" },
                { "Q", "\u03D8" }, { "q", "\u03D9" },
                { "W", "\u03DC" }, { "w", "\u03DD" },
            }),
        };

            static string?GetVowel1(Match m) {
                int i1, i2;

                switch (m.Groups[1].Value) {
                    default: return null;
                    case "a": i1 = 0x1F00; break;
                    case "A": i1 = 0x1F08; break;
                    case "e": i1 = 0x1F10; break;
                    case "E": i1 = 0x1F18; break;
                    case "h": i1 = 0x1F20; break;
                    case "H": i1 = 0x1F28; break;
                    case "i": i1 = 0x1F30; break;
                    case "I": i1 = 0x1F38; break;
                    case "o": i1 = 0x1F40; break;
                    case "O": i1 = 0x1F48; break;
                    case "y": i1 = 0x1F50; break;
                    case "v": i1 = 0x1F60; break;
                    case "V": i1 = 0x1F68; break;

                    case "aj": i1 = 0x1F80; break;
                    case "AJ": case "Aj": i1 = 0x1F88; break;
                    case "hj": i1 = 0x1F90; break;
                    case "HJ": case "Hj": i1 = 0x1F98; break;
                    case "vj": i1 = 0x1FA0; break;
                    case "VJ": case "Vj": i1 = 0x1FA8; break;
                }

                switch (m.Groups[3].Value) {
                    default: case "": i2 = 0; break;
                    case "\\": i2 = 2; break;
                    case "/": i2 = 4; break;
                }
                return ((char)(i1 + i2 + (m.Groups[2].Value == ")" ? 0 : 1))).ToString();
            }

            static string?GetVowel2(Match m) {
                int i1;
                switch (m.Groups[1].Value) {
                    default: return null;
                    case "a": i1 = 0x1F06; break;
                    case "A": i1 = 0x1F0E; break;
                    case "h": i1 = 0x1F26; break;
                    case "H": i1 = 0x1F2E; break;
                    case "i": i1 = 0x1F36; break;
                    case "I": i1 = 0x1F3E; break;
                    case "y": i1 = 0x1F56; break;
                    case "v": i1 = 0x1F66; break;
                    case "V": i1 = 0x1F6E; break;

                    case "aj": i1 = 0x1F86; break;
                    case "AJ": case "Aj": i1 = 0x1F8E; break;
                    case "hj": i1 = 0x1F96; break;
                    case "HJ": case "Hj": i1 = 0x1F9E; break;
                    case "vj": i1 = 0x1FA6; break;
                    case "VJ": case "Vj": i1 = 0x1FAE; break;
                }
                return ((char)(i1 + (m.Groups[2].Value == ")" ? 0 : 1))).ToString();
            }

            static string?GetVowel3(Match m) {
                switch (m.Groups[1].Value) {
                    default: return null;
                    case "": return "\u1F59";
                    case "\\": return "\u1F5B";
                    case "/": return "\u1F5D";
                    case "~": return "\u1F5F";
                }
            }
        }
}