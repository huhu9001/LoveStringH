using System.Text.RegularExpressions;

namespace LoveStringH {
    public class TransliteratorGreek : Transliterator {
        public override string GetTranslit(string s) {
            Match m;

            m = Regex.Match(s, "^([AEHIOVaehioyv]|[AHVahv][Jj])([()])([/\\\\]?)$");
            if (m.Success) {
                int i1;
                switch (m.Groups[1].Value) {
                    default: i1 = 0; break;
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
                if (i1 != 0) {
                    int i2;
                    switch (m.Groups[3].Value) {
                        default: case "": i2 = 0; break;
                        case "\\": i2 = 2; break;
                        case "/": i2 = 4; break;
                    }
                    return ((char)(i1 + i2 + (m.Groups[2].Value == ")" ? 0 : 1))).ToString();
                }
            }
            m = Regex.Match(s, "^([AHIVahiyv]|[AHVahv][Jj])([()])~$");
            if (m.Success) {
                int i1;
                switch (m.Groups[1].Value) {
                    default: i1 = 0; break;
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
                if (i1 != 0) {
                    return ((char)(i1 + (m.Groups[2].Value == ")" ? 0 : 1))).ToString();
                }
            }
            m = Regex.Match(s, "^Y\\(([/\\\\~]?)$");
            if (m.Success) {
                switch (m.Groups[1].Value) {
                    default: break;
                    case "": return "\u1F59";
                    case "\\": return "\u1F5B";
                    case "/": return "\u1F5D";
                    case "~": return "\u1F5F";
                }
            }

            switch (s) {
                default: return s;
                case "A": return "\u0391"; case "a": return "\u03B1";
                case "B": return "\u0392"; case "b": return "\u03B2";
                case "G": return "\u0393"; case "g": return "\u03B3";
                case "D": return "\u0394"; case "d": return "\u03B4";
                case "E": return "\u0395"; case "e": return "\u03B5";
                case "Z": return "\u0396"; case "z": return "\u03B6";
                case "H": return "\u0397"; case "h": return "\u03B7";
                case "TH": case "Th": return "\u0398"; case "th": return "\u03B8";
                case "I": return "\u0399"; case "i": return "\u03B9";
                case "K": return "\u039A"; case "k": return "\u03BA";
                case "L": return "\u039B"; case "l": return "\u03BB";
                case "M": return "\u039C"; case "m": return "\u03BC";
                case "N": return "\u039D"; case "n": return "\u03BD";
                case "X": return "\u039E"; case "x": return "\u03BE";
                case "O": return "\u039F"; case "o": return "\u03BF";
                case "P": return "\u03A0"; case "p": return "\u03C0";
                case "R": return "\u03A1"; case "r": return "\u03C1";
                case "S": return "\u03A3"; case "s": return "\u03C3";
                case "T": return "\u03A4"; case "t": return "\u03C4";
                case "Y": return "\u03A5"; case "y": return "\u03C5";
                case "PH": case "Ph": return "\u03A6"; case "ph": return "\u03C6";
                case "KH": case "Kh": return "\u03A7"; case "kh": return "\u03C7";
                case "PS": case "Ps": return "\u03A8"; case "ps": return "\u03C8";
                case "V": return "\u03A9"; case "v": return "\u03C9";
                case "Q": return "\u03D8"; case "q": return "\u03D9";
                case "W": return "\u03DC"; case "w": return "\u03DD";
                case "SH": case "Sh": return "\u03FA"; case "sh": return "\u03FB";
                case "s ": return "\u03C2";

                case "I:": return "\u03AA"; case "i:": return "\u03CA";
                case "Y:": return "\u03AB"; case "y:": return "\u03CB";
                case "AJ": case "Aj": return "\u1FBC"; case "aj": return "\u1FB3";
                case "HJ": case "Hj": return "\u1FCC"; case "hj": return "\u1FC3";
                case "VJ": case "Vj": return "\u1FFC"; case "vj": return "\u1FF3";

                case "A/": return "\u0386"; case "a/": return "\u03AC";
                case "E/": return "\u0388"; case "e/": return "\u03AD";
                case "H/": return "\u0389"; case "h/": return "\u03AE";
                case "I/": return "\u038A"; case "i/": return "\u03AF";
                case "O/": return "\u038C"; case "o/": return "\u03CC";
                case "Y/": return "\u038E"; case "y/": return "\u03CD";
                case "V/": return "\u038F"; case "v/": return "\u03CE";
                case "I:/": case "i:/": return "\u0390";
                case "Y:/": case "y:/": return "\u03B0";
                case "aj/": return "\u1FB4";
                case "hj/": return "\u1FC4";
                case "vj/": return "\u1FF4";

                case "A\\": return "\u1FBA"; case "a\\": return "\u1F70";
                case "E\\": return "\u1FC8"; case "e\\": return "\u1F72";
                case "H\\": return "\u1FCA"; case "h\\": return "\u1F74";
                case "I\\": return "\u1FDA"; case "i\\": return "\u1F76";
                case "O\\": return "\u1FF8"; case "o\\": return "\u1F78";
                case "Y\\": return "\u1FEA"; case "y\\": return "\u1F7A";
                case "V\\": return "\u1FFA"; case "v\\": return "\u1F7C";
                case "I:\\": case "i:\\": return "\u1FD2";
                case "Y:\\": case "y:\\": return "\u1FE2";
                case "aj\\": return "\u1FB2";
                case "hj\\": return "\u1FC2";
                case "vj\\": return "\u1FF2";

                case "a~": return "\u1FB6";
                case "h~": return "\u1FC6";
                case "i~": return "\u1FD6";
                case "y~": return "\u1FE6";
                case "v~": return "\u1FF6";
                case "I:~": case "i:~": return "\u1FD7";
                case "Y:~": case "y:~": return "\u1FE7";
                case "aj~": return "\u1FB7";
                case "hj~": return "\u1FC7";
                case "vj~": return "\u1FF7";

                case "R)": case "r)": return "\u1FE4";
                case "R(": return "\u1FEC"; case "r(": return "\u1FE5";
            }
        }

        public override int GetStopper(string s) {
            if (Regex.Match(s, "^[AEHIOVaehioyv]?[Jj]?[():]?$").Success) return 0;
            switch (s) {
                default: break;
                case "T": case "t":
                case "P": case "p":
                case "K": case "k":
                case "R": case "r":
                case "S": case "s": return 0;
            }
            if (s.Length >= 4 && GetTranslit(s.Substring(0, 4)).Length == 1) return 4;
            if (s.Length >= 3 && GetTranslit(s.Substring(0, 3)).Length == 1) return 3;
            if (s.Length >= 2 && GetTranslit(s.Substring(0, 2)).Length == 1) return 2;
            return 1;
        }
    }
}