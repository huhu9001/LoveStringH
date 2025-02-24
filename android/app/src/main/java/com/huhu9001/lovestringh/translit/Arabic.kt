package com.huhu9001.lovestringh.translit

object Arabic {
    val ritems = arrayOf(
        Transliterator.RegexItem(Regex("(?<![A-Za-z])([aiu])(?=[A-Za-z])"), mapOf(
            "a" to "\u0627",
            "i" to "\u0625",
            "u" to "\u0624",
        )),
        Transliterator.RegexItem(Regex("(?<=[A-Za-z])-(?=[A-Za-z])"), ""),
        Transliterator.RegexItem(Regex(".."), mapOf(
            "aa" to "\u0627",
            "th" to "\u062B",
            "kh" to "\u062E",
            "dh" to "\u0630",
            "sh" to "\u0634",
            "so" to "\u0635",
            "do" to "\u0636",
            "to" to "\u0637",
            "zo" to "\u0638",
            "uu" to "\u0648",
            "ii" to "\u064A",

            "a~" to "\u0622",
            "'a" to "\u0623",
            "'u" to "\u0624",
            "'i" to "\u0625",

            "t~" to "\u0629",
            "i~" to "\u0649",
            "a'" to "\u0671",
        )),
        Transliterator.RegexItem(Regex("."), mapOf(
            "b" to "\u0628",
            "t" to "\u062A",
            "j" to "\u062C",
            "x" to "\u062D",
            "d" to "\u062F",
            "r" to "\u0631",
            "z" to "\u0632",
            "s" to "\u0633",
            "o" to "\u0639",
            "g" to "\u063A",
            "f" to "\u0641",
            "q" to "\u0642",
            "k" to "\u0643",
            "l" to "\u0644",
            "m" to "\u0645",
            "n" to "\u0646",
            "h" to "\u0647",
            "w" to "\u0648",
            "y" to "\u064A",

            "e" to "",
            "a" to "",
            "i" to "",
            "u" to "",

            "'" to "\u0621",
            "-" to "\u0640",
        )),
    )
}