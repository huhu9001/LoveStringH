package com.huhu9001.lovestringh.translit

object Cyrillic {
    val ritems = arrayOf(
        Transliterator.RegexItem(Regex("...."), mapOf(
            "SHCH" to "\u0429", "Shch" to "\u0429", "shch" to "\u0449",
        )),
        Transliterator.RegexItem(Regex(".."), mapOf(
            "YE" to "\u0415", "Ye" to "\u0415", "ye" to "\u0435",
            "ZH" to "\u0416", "Zh" to "\u0416", "zh" to "\u0436",
            "KH" to "\u0425", "Kh" to "\u0425", "kh" to "\u0445",
            "TS" to "\u0426", "Ts" to "\u0426", "ts" to "\u0446",
            "CH" to "\u0427", "Ch" to "\u0427", "ch" to "\u0447",
            "SH" to "\u0428", "Sh" to "\u0428", "sh" to "\u0448",

            "YU" to "\u042E", "Yu" to "\u042E", "yu" to "\u044E",
            "YA" to "\u042F", "Ya" to "\u042F", "ya" to "\u044F",
            "YO" to "\u0401", "Yo" to "\u0401", "yo" to "\u0451",
        )),
        Transliterator.RegexItem(Regex("."), mapOf(
            "A" to "\u0410", "a" to "\u0430",
            "B" to "\u0411", "b" to "\u0431",
            "V" to "\u0412", "v" to "\u0432",
            "G" to "\u0413", "g" to "\u0433",
            "D" to "\u0414", "d" to "\u0434",
            "Z" to "\u0417", "z" to "\u0437",
            "I" to "\u0418", "i" to "\u0438",
            "J" to "\u0419", "j" to "\u0439",
            "K" to "\u041A", "k" to "\u043A",
            "L" to "\u041B", "l" to "\u043B",
            "M" to "\u041C", "m" to "\u043C",
            "N" to "\u041D", "n" to "\u043D",
            "O" to "\u041E", "o" to "\u043E",
            "P" to "\u041F", "p" to "\u043F",
            "R" to "\u0420", "r" to "\u0440",
            "S" to "\u0421", "s" to "\u0441",
            "T" to "\u0422", "t" to "\u0442",
            "U" to "\u0423", "u" to "\u0443",
            "F" to "\u0424", "f" to "\u0444",
            "H" to "\u0425", "h" to "\u0445",
            "C" to "\u0426", "c" to "\u0446",

            "\"" to "\u044A",
            "Y" to "\u042B", "y" to "\u044B",
            "\'" to "\u044C",
            "E" to "\u042D", "e" to "\u044D",

            "/" to "\u0301",
        )),
    )
}