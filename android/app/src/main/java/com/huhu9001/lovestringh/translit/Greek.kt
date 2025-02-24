package com.huhu9001.lovestringh.translit

object Greek {
    val ritems = arrayOf(
        Transliterator.RegexItem(Regex("([AHIVahiyv]|[AHV][Jj]|[ahv]j)([()])~")) {
            Char(when (it.groupValues[1]) {
                "a" -> 0x1F06
                "A" -> 0x1F0E
                "h" -> 0x1F26
                "H" -> 0x1F2E
                "i" -> 0x1F36
                "I" -> 0x1F3E
                "y" -> 0x1F56
                "v" -> 0x1F66
                "V" -> 0x1F6E

                "aj" -> 0x1F86
                "AJ", "Aj" -> 0x1F8E
                "hj" -> 0x1F96
                "HJ", "Hj" -> 0x1F9E
                "vj" -> 0x1FA6
                "VJ", "Vj" -> 0x1FAE 
                else -> return@RegexItem null
            } + if (it.groupValues[2] == ")") 0 else 1).toString()
        },
        Transliterator.RegexItem(Regex("([AEHIOVaehioyv]|[AHV][Jj]|[ahv]j)([()])([/\\\\]?)")) {
            Char(when (it.groupValues[1]) {
                "a" -> 0x1F00
                "A" -> 0x1F08
                "e" -> 0x1F10
                "E" -> 0x1F18
                "h" -> 0x1F20
                "H" -> 0x1F28
                "i" -> 0x1F30
                "I" -> 0x1F38
                "o" -> 0x1F40
                "O" -> 0x1F48
                "y" -> 0x1F50
                "v" -> 0x1F60
                "V" -> 0x1F68

                "aj" -> 0x1F80
                "AJ", "Aj" -> 0x1F88
                "hj" -> 0x1F90
                "HJ", "Hj" -> 0x1F98
                "vj" -> 0x1FA0
                "VJ", "Vj" -> 0x1FA8
                else -> return@RegexItem null
            } + when (it.groupValues[3]) {
                "\\" -> 2
                "/" -> 4
                else -> 0
            } + if (it.groupValues[2] == ")") 0 else 1).toString()
        },
        Transliterator.RegexItem(Regex("Y\\(([/\\\\~]?)")) {
            when (it.groupValues[1]) {
                 "" -> "\u1F59"
                 "\\" -> "\u1F5B"
                 "/" -> "\u1F5D"
                 "~" -> "\u1F5F"
                else -> null
            }
        },
        Transliterator.RegexItem(Regex("(?<=[A-Za-z](?:[()~/\\\\]|[()~/\\\\][()~/\\\\]))s(?=[^A-Za-z]|$)"), "\u03C2"),
        Transliterator.RegexItem(Regex("..."), mapOf(
            "I:/" to "\u0390", "i:/" to "\u0390",
            "Y:/" to "\u03B0", "y:/" to "\u03B0",
            "aj/" to "\u1FB4",
            "hj/" to "\u1FC4",
            "vj/" to "\u1FF4",

            "I:\\" to "\u1FD2", "i:\\" to "\u1FD2",
            "Y:\\" to "\u1FE2", "y:\\" to "\u1FE2",
            "aj\\" to "\u1FB2",
            "hj\\" to "\u1FC2",
            "vj\\" to "\u1FF2",

            "I:~" to "\u1FD7", "i:~" to "\u1FD7",
            "Y:~" to "\u1FE7", "y:~" to "\u1FE7",
            "aj~" to "\u1FB7",
            "hj~" to "\u1FC7",
            "vj~" to "\u1FF7",
        )),
        Transliterator.RegexItem(Regex(".."), mapOf(
            "TH" to "\u0398", "Th" to "\u0398", "th" to "\u03B8",
            "T'" to "\u03A4", "t'" to "\u03C4",
            "PH" to "\u03A6", "Ph" to "\u03A6", "ph" to "\u03C6",
            "P'" to "\u03A0", "p'" to "\u03C0",
            "KH" to "\u03A7", "Kh" to "\u03A7", "kh" to "\u03C7",
            "K'" to "\u039A", "k'" to "\u03BA",
            "PS" to "\u03A8", "Ps" to "\u03A8", "ps" to "\u03C8",
            "SH" to "\u03FA", "Sh" to "\u03FA", "sh" to "\u03FB",
            "S'" to "\u03A3", "s'" to "\u03C3",

            "I:" to "\u03AA", "i:" to "\u03CA",
            "Y:" to "\u03AB", "y:" to "\u03CB",
            "AJ" to "\u1FBC", "Aj" to "\u1FBC", "aj" to "\u1FB3",
            "HJ" to "\u1FCC", "Hj" to "\u1FCC", "hj" to "\u1FC3",
            "VJ" to "\u1FFC", "Vj" to "\u1FFC", "vj" to "\u1FF3",

            "A/" to "\u0386", "a/" to "\u03AC",
            "E/" to "\u0388", "e/" to "\u03AD",
            "H/" to "\u0389", "h/" to "\u03AE",
            "I/" to "\u038A", "i/" to "\u03AF",
            "O/" to "\u038C", "o/" to "\u03CC",
            "Y/" to "\u038E", "y/" to "\u03CD",
            "V/" to "\u038F", "v/" to "\u03CE",
            "A\\" to "\u1FBA", "a\\" to "\u1F70",
            "E\\" to "\u1FC8", "e\\" to "\u1F72",
            "H\\" to "\u1FCA", "h\\" to "\u1F74",
            "I\\" to "\u1FDA", "i\\" to "\u1F76",
            "O\\" to "\u1FF8", "o\\" to "\u1F78",
            "Y\\" to "\u1FEA", "y\\" to "\u1F7A",
            "V\\" to "\u1FFA", "v\\" to "\u1F7C",
            "a~" to "\u1FB6",
            "h~" to "\u1FC6",
            "i~" to "\u1FD6",
            "y~" to "\u1FE6",
            "v~" to "\u1FF6",

            "R)" to "\u1FE4", "r)" to "\u1FE4",
            "R(" to "\u1FEC", "r(" to "\u1FE5",
        )),
        Transliterator.RegexItem(Regex("."), mapOf(
            "A" to "\u0391", "a" to "\u03B1",
            "B" to "\u0392", "b" to "\u03B2",
            "G" to "\u0393", "g" to "\u03B3",
            "D" to "\u0394", "d" to "\u03B4",
            "E" to "\u0395", "e" to "\u03B5",
            "Z" to "\u0396", "z" to "\u03B6",
            "H" to "\u0397", "h" to "\u03B7",
            "I" to "\u0399", "i" to "\u03B9",
            "K" to "\u039A", "k" to "\u03BA",
            "L" to "\u039B", "l" to "\u03BB",
            "M" to "\u039C", "m" to "\u03BC",
            "N" to "\u039D", "n" to "\u03BD",
            "X" to "\u039E", "x" to "\u03BE",
            "O" to "\u039F", "o" to "\u03BF",
            "P" to "\u03A0", "p" to "\u03C0",
            "R" to "\u03A1", "r" to "\u03C1",
            "S" to "\u03A3", "s" to "\u03C3",
            "T" to "\u03A4", "t" to "\u03C4",
            "Y" to "\u03A5", "y" to "\u03C5",
            "V" to "\u03A9", "v" to "\u03C9",
            "Q" to "\u03D8", "q" to "\u03D9",
            "W" to "\u03DC", "w" to "\u03DD",
        )),
    )
}