namespace LoveStringH {
    public class TransliteratorArabic : Transliterator {
        public override string GetTranslit(string s) {
            switch (s) {
                default: return s;
                case "aa": return "\u0627";
                case "b": return "\u0628";
                case "t": return "\u062A";
                case "th": return "\u062B";
                case "j": return "\u062C";
                case "x": return "\u062D";
                case "kh": return "\u062E";
                case "d": return "\u062F";
                case "dh": return "\u0630";
                case "r": return "\u0631";
                case "z": return "\u0632";
                case "s": return "\u0633";
                case "sh": return "\u0634";
                case "so": return "\u0635";
                case "do": return "\u0636";
                case "to": return "\u0637";
                case "zo": return "\u0638";
                case "o": return "\u0639";
                case "gh": return "\u063A";
                case "f": return "\u0641";
                case "q": return "\u0642";
                case "k": return "\u0643";
                case "l": return "\u0644";
                case "m": return "\u0645";
                case "n": return "\u0646";
                case "h": return "\u0647";
                case "w": case "uu": return "\u0648";
                case "y": case "ii": return "\u064A";

                case "e": return "";
                case "a": return "";
                case "i": return "";
                case "u": return "";
                
                case "'": return "\u0621";
                case "a~": return "\u0622";
                case "'a": return "\u0623";
                case "'u": return "\u0624";
                case "'i": return "\u0625";
                    
                case "t~": return "\u0629";
                case "i~": return "\u0649";
                case "a'": return "\u0671";
                    
                case "-": return "\u0640";
            }
        }
        public override int GetStopper(string s) {
            switch (s) {
                default: break;
                case "s":
                case "z":
                case "t":
                case "d":
                case "k":
                case "g":
                case "'":
                case "a":
                case "u":
                case "i": return 0;
            }
            if (s.Length >= 2 && GetTranslit(s.Substring(0, 2)).Length == 1) return 2;
            return 1;
        }
    }
}