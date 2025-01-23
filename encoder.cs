using System.Text;
using System.Text.RegularExpressions;

namespace LoveStringH {
    public class Encoder {
        public class EscapeStyle {
            public string name { get; }
            public readonly Func<uint, string> escape;
            public EscapeStyle(string name, Func<uint, string> escape) {
                this.name = name; this.escape = escape;
            }
        }
        static readonly EscapeStyle[] estylesDefault = new EscapeStyle[] {
            new EscapeStyle("\\x", u => $"\\x{u:X}"),
            new EscapeStyle("%", u => $"%{u:X2}"),
        };

        public static readonly Encoder[] all = new Encoder[] {
            new Encoder("Unicode", null, new EscapeStyle[] {
                new EscapeStyle("\\u", u => u < 0x80 ? $"\\x{u:X}" : u < 0x10000 ? $"\\u{u:X4}" : $"\\U{u:X8}"),
                new EscapeStyle("&#x", u => $"&#x{u:X};"),
            }),
            new Encoder("UTF-8", Encoding.UTF8, estylesDefault ),
            new Encoder("UTF-16", null, new EscapeStyle[] { estylesDefault[0] } ),
            new Encoder("GB 18030", Encoding.GetEncoding(54936), estylesDefault ),
            new Encoder("GB 2312", Encoding.GetEncoding(936), estylesDefault ),
            new Encoder("Shift-JIS", Encoding.GetEncoding(932), estylesDefault ),
            new Encoder("Big5", Encoding.GetEncoding(950), estylesDefault ),
            new Encoder("EUC-JP", Encoding.GetEncoding(51932), estylesDefault ),
        };

        static readonly Regex rgxEscape =
            new Regex("\\\\([0-7]{1,3})|\\\\x([A-Fa-f0-9]{1,8})|%([A-Fa-f0-9]{2})|\\\\u([A-Fa-f0-9]{4})|\\\\U([A-Fa-f0-9]{8})|&#(\\d{1,10});|&#x([A-Fa-f0-9]{1,8});");

        public string name { get; }
        readonly Encoding?e;
        public readonly EscapeStyle[] styles;

        Encoder(string name, Encoding?e, EscapeStyle[] styles) {
            this.name = name; this.e = e; this.styles = styles;
        }

        public string encode(string str, Func<uint, string> escape) {
            StringBuilder s_result = new StringBuilder();
            if (e == null) {
                char surrogate = '\0';
                foreach (char chr in str) {
                    if (surrogate != '\0') {
                        if (chr >= '\uDC00' && chr < '\uE000') {
                            s_result.Append(escape(((uint)surrogate - 0xD7C0 << 10) + ((uint)chr - 0xDC00)));
                            surrogate = '\0';
                            continue;
                        }
                        else {
                            s_result.Append(escape(surrogate));
                            surrogate = '\0';
                        }
                    }
                    if (name == "Unicode" && chr >= '\uD800' && chr < '\uDC00')
                        surrogate = chr;
                    else s_result.Append(escape(chr));
                }
                if (surrogate != '\0') s_result.Append(escape(surrogate));
            }
            else {
                byte[] bs = e.GetBytes(str);
                foreach(byte b in bs) {
                    s_result.Append(escape(b));
                }
            }
            return s_result.ToString();
        }

        public string decode(string str) {
            StringBuilder s_result = new StringBuilder();
            char[] decodePiece(List<byte> bytes) {
                if (e != null) {
                    try { return e.GetChars(bytes.ToArray()); }
                    catch (DecoderFallbackException) { return new char[] { '?' }; }
                }
                else {
                    char[] result = new char[(bytes.Count + 1) / 2];
                    for (int i0 = 0; ; ++i0) {
                        if (i0 * 2 >= bytes.Count - 1) {
                            if (i0 * 2 < bytes.Count) result[i0] = (char)bytes[i0 * 2];
                            break;
                        }
                        result[i0] = (char)(bytes[i0 * 2] | (bytes[i0 * 2 + 1] << 8));
                    }
                    return result;
                }
            }

            int endLastMatch = 0;
            List<byte> bs = new List<byte>();
            foreach (Match m in rgxEscape.Matches(str)) {
                if (endLastMatch < m.Index) {
                    s_result.Append(decodePiece(bs));
                    bs.Clear();
                    s_result.Append(str.Substring(endLastMatch, m.Index - endLastMatch));
                }

                uint u_char;
                uint ToUInt32(string numstr, int radix) {
                    try { return Convert.ToUInt32(numstr, radix); }
                    catch (OverflowException) { return 0x3F; }
                }
                if (m.Groups[1].Success) u_char = ToUInt32(m.Groups[1].Value, 8);
                else if (m.Groups[2].Success) u_char = ToUInt32(m.Groups[2].Value, 16);
                else if (m.Groups[3].Success) u_char = ToUInt32(m.Groups[3].Value, 16);
                else {
                    s_result.Append(decodePiece(bs));
                    bs.Clear();

                    if (m.Groups[4].Success) u_char = ToUInt32(m.Groups[4].Value, 16);
                    else if (m.Groups[5].Success) u_char = ToUInt32(m.Groups[5].Value, 16);
                    else if (m.Groups[6].Success) u_char = ToUInt32(m.Groups[6].Value, 10);
                    else u_char = ToUInt32(m.Groups[7].Value, 16);

                    if (u_char < 0x10000) s_result.Append((char)u_char);
                    else if (u_char < 0x110000) {
                        s_result.Append((char)((u_char >> 10) + 0xD7C0));
                        s_result.Append((char)((u_char & 0x3FF) + 0xDC00));
                    }
                    else s_result.Append('?');
                    endLastMatch = m.Index + m.Length;
                    continue;
                }

                if (e == null) {
                    if (u_char >= 0x10000) {
                        if (name == "Unicode" && u_char < 0x110000) {
                            char s1 = (char)((u_char >> 10) + 0xD7C0);
                            char s2 = (char)((u_char & 0x3FF) + 0xDC00);
                            bs.Add((byte)(s1 & 0xFF));
                            bs.Add((byte)(s1 >> 8 & 0xFF));
                            bs.Add((byte)(s2 & 0xFF));
                            bs.Add((byte)(s2 >> 8 & 0xFF));
                        }
                        else {
                            bs.Add((byte)(u_char & 0xFF));
                            bs.Add((byte)(u_char >> 8 & 0xFF));
                            bs.Add((byte)(u_char >> 16 & 0xFF));
                            bs.Add((byte)(u_char >> 24 & 0xFF));
                        }
                    }
                    else {
                        bs.Add((byte)(u_char & 0xFF));
                        bs.Add((byte)(u_char >> 8 & 0xFF));
                    }
                }
                else {
                    while (u_char > 0xFF) {
                        bs.Add((byte)(u_char & 0xFF));
                        u_char >>= 8;
                    }
                    bs.Add((byte)(u_char & 0xFF));
                }
                endLastMatch = m.Index + m.Length;
            }
            s_result.Append(decodePiece(bs));
            if (endLastMatch < str.Length) s_result.Append(str.Substring(endLastMatch));
            return s_result.ToString();
        }
    }
}
