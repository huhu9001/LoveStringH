using System.Text.RegularExpressions;

namespace LoveStringH {
    public class RegexHandler {
        public enum ResultCode { OK, NOTFOUND, OVER }
        public string text { get; private set; }
        public int start { get; private set; }
        public int end { get; private set; }
        public string regex;
        public string repl;

        private bool emptyMatch;

        public void setText(string text, int start, int end) {
            if (this.text == text && this.end == end && this.start == start) return;
            this.text = text;
            this.end = end <= text.Length ? end : 0;
            this.start = start <= this.end ? start : 0;
            emptyMatch = false;
        }
        public void setSelection(int start, int end) {
        }
        public ResultCode findNext() /*throws*/ {
            Regex reg = new Regex(regex);
            Match m;
            if (emptyMatch) {
                if (end < text.Length) m = reg.Match(text, end + 1);
                else m = Match.Empty;
            }
            else m = reg.Match(text, end);
            if (m.Success) {
                start = m.Index;
                end = m.Index + m.Length;
                emptyMatch = start == end;
                return ResultCode.OK;
            }
            else {
                Match mOver = reg.Match(text);
                if (!mOver.Success) return ResultCode.NOTFOUND;
                start = mOver.Index;
                end = mOver.Index + mOver.Length;
                emptyMatch = start == end;
                return ResultCode.OVER;
            }
        }
        public ResultCode replace() /*throws*/ {
            Regex reg = new Regex(regex);
            if (!reg.IsMatch(text, start)) return findNext();
            Match m = reg.Match(text, start);
            if (m.Index + m.Length != end) {
                end = m.Index + m.Length;
                emptyMatch = start == end;
                return ResultCode.OK;
            }

            string replacement = getReplacement(reg, text, repl, start, end);
            text = $"{text.Substring(0, start)}{replacement}{text.Substring(end)}";
            end += replacement.Length - m.Length;
            start = end;

            return findNext();
        }
        public int replaceAll() /*throws*/ {
            Regex reg = new Regex(regex);
            int count = 0;
            text = reg.Replace(text, m => {
                count += 1;
                return getReplacement(reg, text, repl, m.Index, m.Index + m.Length);
            });
            start = 0;
            end = 0;
            emptyMatch = false;

            return count;
        }

        private static string getReplacement(Regex reg, string text, string repl, int start, int end) {
            string replm = reg.Replace(text, repl, 1, start);
            return replm.Substring(start, (end - start) + (replm.Length - text.Length));
        }
    }
}
