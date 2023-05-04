namespace LoveStringH {
    public class TransliteratorCyrillic : Transliterator {
        public override string GetTranslit(string s) {
            switch (s) {
                default: return s;
                case "A": return "\u0410"; case "a": return "\u0430";
                case "B": return "\u0411"; case "b": return "\u0431";
                case "V": return "\u0412"; case "v": return "\u0432";
                case "G": return "\u0413"; case "g": return "\u0433";
                case "D": return "\u0414"; case "d": return "\u0434";
                case "YE": case "Ye": return "\u0415"; case "ye": return "\u0435";
                case "Zh": return "\u0416"; case "zh": return "\u0436";
                case "Z": return "\u0417"; case "z": return "\u0437";
                case "I": return "\u0418"; case "i": return "\u0438";
                case "J": return "\u0419"; case "j": return "\u0439";
                case "K": return "\u041A"; case "k": return "\u043A";
                case "L": return "\u041B"; case "l": return "\u043B";
                case "M": return "\u041C"; case "m": return "\u043C";
                case "N": return "\u041D"; case "n": return "\u043D";
                case "O": return "\u041E"; case "o": return "\u043E";
                case "P": return "\u041F"; case "p": return "\u043F";
                case "R": return "\u0420"; case "r": return "\u0440";
                case "S": return "\u0421"; case "s": return "\u0441";
                case "T": return "\u0422"; case "t": return "\u0442";
                case "U": return "\u0423"; case "u": return "\u0443";
                case "F": return "\u0424"; case "f": return "\u0444";
                case "KH": case "Kh": case "H": return "\u0425"; case "kh": case "h": return "\u0445";
                case "C": case "TS": case "Ts": return "\u0426"; case "c": case "ts": return "\u0446";
                case "CH": case "Ch": return "\u0427";  case "ch": return "\u0447";
                case "SH": case "Sh": return "\u0428"; case "sh": return "\u0448";
                case "SHCH": case "Shch": return "\u0429"; case "shch": return "\u0449";
                case "\"": return "\u044A";
                case "Y": return "\u042B"; case "y": return "\u044B";
                case "\'": return "\u044C";
                case "E": return "\u042D"; case "e": return "\u044D";
                case "YU": case "Yu": return "\u042E"; case "yu": return "\u044E";
                case "YA": case "Ya": return "\u042F"; case "ya": return "\u044F";
                case "YO": case "Yo": return "\u0401"; case "yo": return "\u0451";
            }
        }

        public override int GetStopper(string s) {
            switch (s) {
                default: break;
                case "Y": case "y":
                case "Z": case "z":
                case "K": case "k":
                case "T": case "t":
                case "C": case "c":
                case "S": case "s":
                case "SH": case "Sh": case "sh":
                case "SHC": case "Shc": case "shc": return 0;
            }
            if (s.Length >= 4 && GetTranslit(s.Substring(0, 4)).Length == 1) return 4;
            if (s.Length >= 2 && GetTranslit(s.Substring(0, 2)).Length == 1) return 2;
            return 1;
        }
    }
}