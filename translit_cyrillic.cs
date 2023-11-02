using System.Collections.Generic;

namespace LoveStringH {
    public class TransliteratorCyrillic : Transliterator {
        public TransliteratorCyrillic() {
            tData = new RegexItem[] {
                new RegexItem("....", new Dictionary<string, string> {
                    { "SHCH", "\u0429" }, { "Shch", "\u0429" }, { "shch", "\u0449" },
                }),
                new RegexItem("..", new Dictionary<string, string> {
                    { "YE", "\u0415" }, { "Ye", "\u0415" }, { "ye", "\u0435" },
                    { "ZH", "\u0416" }, { "Zh", "\u0416" }, { "zh", "\u0436" },
                    { "KH", "\u0425" }, { "Kh", "\u0425" }, { "kh", "\u0445" },
                    { "TS", "\u0426" }, { "Ts", "\u0426" }, { "ts", "\u0446" },
                    { "CH", "\u0427" }, { "Ch", "\u0427" }, { "ch", "\u0447" },
                    { "SH", "\u0428" }, { "Sh", "\u0428" }, { "sh", "\u0448" },
                
                    { "YU", "\u042E" }, { "Yu", "\u042E" }, { "yu", "\u044E" },
                    { "YA", "\u042F" }, { "Ya", "\u042F" }, { "ya", "\u044F" },
                    { "YO", "\u0401" }, { "Yo", "\u0401" }, { "yo", "\u0451" },
                }),
                new RegexItem(".", new Dictionary<string, string> {
                    { "A", "\u0410" }, { "a", "\u0430" },
                    { "B", "\u0411" }, { "b", "\u0431" },
                    { "V", "\u0412" }, { "v", "\u0432" },
                    { "G", "\u0413" }, { "g", "\u0433" },
                    { "D", "\u0414" }, { "d", "\u0434" },
                    { "Z", "\u0417" }, { "z", "\u0437" },
                    { "I", "\u0418" }, { "i", "\u0438" },
                    { "J", "\u0419" }, { "j", "\u0439" },
                    { "K", "\u041A" }, { "k", "\u043A" },
                    { "L", "\u041B" }, { "l", "\u043B" },
                    { "M", "\u041C" }, { "m", "\u043C" },
                    { "N", "\u041D" }, { "n", "\u043D" },
                    { "O", "\u041E" }, { "o", "\u043E" },
                    { "P", "\u041F" }, { "p", "\u043F" },
                    { "R", "\u0420" }, { "r", "\u0440" },
                    { "S", "\u0421" }, { "s", "\u0441" },
                    { "T", "\u0422" }, { "t", "\u0442" },
                    { "U", "\u0423" }, { "u", "\u0443" },
                    { "F", "\u0424" }, { "f", "\u0444" },
                    { "H", "\u0425" }, { "h", "\u0445" },
                    { "C", "\u0426" }, { "c", "\u0446" },
                
                    { "\"", "\u044A" },
                    { "Y", "\u042B" }, { "y", "\u044B" },
                    { "\'", "\u044C" },
                    { "E", "\u042D" }, { "e", "\u044D" },

                    { "/", "\u0301" },
                }),
            };
        }
    }
}