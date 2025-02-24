package com.huhu9001.lovestringh.translit

object Latin {
    val ritems = arrayOf(
        Transliterator.RegexItem(Regex("..."), mapOf(
            "A:-" to "\u01DE", "a:-" to "\u01DF",
            "AE-" to "\u01E2", "Ae-" to "\u01E2", "ae-" to "\u01E3",
            "O:-" to "\u022A", "o:-" to "\u022B",
            "U:-" to "\u01D5", "u:-" to "\u01D6",
            "U-:" to "\u1E7A", "u-:" to "\u1E7B",

            "AE/" to "\u01FC", "Ae/" to "\u01FC", "ae/" to "\u01FD",
            "I:/" to "\u1E2E", "i:/" to "\u1E2F",
            "U:/" to "\u01D7", "u:/" to "\u01D8",

            "U:&" to "\u01D9", "u:&" to "\u01DA",

            "U:\\" to "\u01DB", "u:\\" to "\u01DC",
        )),
        Transliterator.RegexItem(Regex(".."), mapOf(
            "AE" to "\u00C6", "Ae" to "\u00C6", "ae" to "\u00E6",
            "CZ" to "\u00C7", "Cz" to "\u00C7", "cz" to "\u00E7",
            "OE" to "\u0152", "Oe" to "\u0152", "oe" to "\u0153",
            "SZ" to "\u1E9E", "Sz" to "\u1E9E", "sz" to "\u00DF",

            "A:" to "\u00C4", "a:" to "\u00E4",
            "E:" to "\u00CB", "e:" to "\u00EB",
            "H:" to "\u1E26", "h:" to "\u1E27",
            "I:" to "\u00CF", "i:" to "\u00EF",
            "O:" to "\u00D6", "o:" to "\u00F6",
            "t:" to "\u1E97",
            "U:" to "\u00DC", "u:" to "\u00FC",
            "Y:" to "\u0178", "y:" to "\u00FF",

            "A-" to "\u0100", "a-" to "\u0101",
            "E-" to "\u0112", "e-" to "\u0113",
            "G-" to "\u1E20", "g-" to "\u1E21",
            "I-" to "\u012A", "i-" to "\u012B",
            "O-" to "\u014C", "o-" to "\u014D",
            "U-" to "\u016A", "u-" to "\u016B",
            "Y-" to "\u0232", "y-" to "\u0233",

            "A/" to "\u00C1", "a/" to "\u00E1",
            "C/" to "\u0106", "c/" to "\u0107",
            "E/" to "\u00C9", "e/" to "\u00E9",
            "G/" to "\u01F4", "g/" to "\u01F5",
            "I/" to "\u00CD", "i/" to "\u00ED",
            "K/" to "\u1E30", "k/" to "\u1E31",
            "L/" to "\u0139", "l/" to "\u013A",
            "M/" to "\u1E3E", "m/" to "\u1E3F",
            "N/" to "\u0143", "n/" to "\u0144",
            "O/" to "\u00D3", "o/" to "\u00F3",
            "P/" to "\u1E54", "p/" to "\u1E55",
            "R/" to "\u0154", "r/" to "\u0155",
            "S/" to "\u015A", "s/" to "\u015B",
            "U/" to "\u00DA", "u/" to "\u00FA",
            "W/" to "\u1E82", "w/" to "\u1E83",
            "Y/" to "\u00DD", "y/" to "\u00FD",
            "Z/" to "\u0179", "z/" to "\u017A",

            "A&" to "\u01CD", "a&" to "\u01CE",
            "C&" to "\u010C", "c&" to "\u010D",
            "D&" to "\u010E", "d&" to "\u010F",
            "E&" to "\u011A", "e&" to "\u011B",
            "G&" to "\u01E6", "g&" to "\u01E7",
            "H&" to "\u021E", "h&" to "\u021F",
            "I&" to "\u01CF", "i&" to "\u01D0",
            "j&" to "\u01F0",
            "K&" to "\u01E8", "k&" to "\u01E9",
            "L&" to "\u013D", "l&" to "\u013E",
            "N&" to "\u0147", "n&" to "\u0148",
            "O&" to "\u01D1", "o&" to "\u01D2",
            "R&" to "\u0158", "r&" to "\u0159",
            "S&" to "\u0160", "s&" to "\u0161",
            "T&" to "\u0164", "t&" to "\u0165",
            "U&" to "\u01D3", "u&" to "\u01D4",
            "Z&" to "\u017D", "z&" to "\u017E",

            "A\\" to "\u00C0", "a\\" to "\u00E0",
            "E\\" to "\u00C8", "e\\" to "\u00E8",
            "I\\" to "\u00CC", "i\\" to "\u00EC",
            "O\\" to "\u00D2", "o\\" to "\u00F2",
            "U\\" to "\u00D9", "u\\" to "\u00F9",
            "W\\" to "\u1E80", "w\\" to "\u1E81",
            "Y\\" to "\u1EF2", "y\\" to "\u1EF3",

            "A^" to "\u00C2", "a^" to "\u00E2",
            "E^" to "\u00CA", "e^" to "\u00EA",
            "I^" to "\u00CE", "i^" to "\u00EE",
            "O^" to "\u00D4", "o^" to "\u00F4",
            "U^" to "\u00DB", "u^" to "\u00FB",
            "Y^" to "\u0176", "y^" to "\u0177",
        )),
        Transliterator.RegexItem(Regex("."), mapOf(
            ":" to "\u0308",//Diaeresis
            "-" to "\u0304",//Macron
            "/" to "\u0301",//Acute
            "&" to "\u030C",//Caron
            "\\" to "\u0300",//Grave
            "^" to "\u0302",//Circumflex
        )),
    )
}