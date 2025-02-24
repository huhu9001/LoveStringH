package com.huhu9001.lovestringh.translit

object Hangul {
    private const val PAT_INIT_2 = "kk|tt|pp|ss|jj|ch"
    private const val PAT_INIT_1 = "[gkndtrlmbpsjchxf]"
    private const val PAT_INIT = "$PAT_INIT_2|$PAT_INIT_1|"
    private const val PAT_MED = "ae?|(?<![yw])(?:eu|ui|o[ei])|eo?|i|o|(?<![w])u"
    private const val PAT_CODA_2 = "k[ksh]|n[jhg]|th|l[kgmbstph]|p[sh]|ss|ch"
    private const val PAT_CODA_1 = PAT_INIT_1
    private const val PAT_CODA = "$PAT_CODA_2|$PAT_CODA_1|"

    private fun getSyllable(m:MatchResult):String? {
        return Char(0xAC00 + when(m.groupValues[1]) {
            "g" -> 0
            "kk" -> 1
            "n" -> 2
            "d" -> 3
            "tt" -> 4
            "r", "l" -> 5
            "m" -> 6
            "b" -> 7
            "pp" -> 8
            "s" -> 9
            "ss" -> 10
            "" -> 11
            "j" -> 12
            "jj" -> 13
            "c", "ch" -> 14
            "k", "x" -> 15
            "t" -> 16
            "p", "f" -> 17
            "h" -> 18
            else -> return null
        } * 0x24C + when (m.groupValues[2]) {
            "a" -> 0
            "ae" -> 1
            "ya" -> 2
            "yae" -> 3
            "eo" -> 4
            "e" -> 5
            "yeo" -> 6
            "ye" -> 7
            "o" -> 8
            "wa" -> 9
            "wae" -> 10
            "oe", "oi" -> 11
            "yo" -> 12
            "u" -> 13
            "wo", "weo" -> 14
            "we" -> 15
            "wi" -> 16
            "yu" -> 17
            "eu" -> 18
            "ui", "yi" -> 19
            "i" -> 20
            else -> return null
        } * 0x1C + when (m.groupValues[3]) {
            "" -> 0
            "k", "g" -> 1
            "kk" -> 2
            "ks" -> 3
            "n" -> 4
            "nj" -> 5
            "nh" -> 6
            "d" -> 7
            "l", "r" -> 8
            "lk", "lg" -> 9
            "lm" -> 10
            "lb" -> 11
            "ls" -> 12
            "lt" -> 13
            "lp" -> 14
            "lh" -> 15
            "m" -> 16
            "p", "b" -> 17
            "ps" -> 18
            "s" -> 19
            "ss" -> 20
            "ng" -> 21
            "j" -> 22
            "ch", "c" -> 23
            "x", "kh" -> 24
            "t", "th" -> 25
            "f", "ph" -> 26
            "h" -> 27
            else -> return null
        }).toString()
    }

    val ritems = arrayOf(
        Transliterator.RegexItem(Regex("($PAT_INIT)([yw]?(?:$PAT_MED))(l)(?=[yw]?(?:$PAT_MED))"), ::getSyllable),
        Transliterator.RegexItem(Regex("($PAT_INIT)([yw]?(?:$PAT_MED))()(?=(?:$PAT_INIT_2|$PAT_INIT_1)[yw]?(?:$PAT_MED))"), ::getSyllable),
        Transliterator.RegexItem(Regex("($PAT_INIT)([yw]?(?:$PAT_MED))($PAT_CODA_1)(?=(?:$PAT_INIT_2|$PAT_INIT_1)[yw]?(?:$PAT_MED))"), ::getSyllable),
        Transliterator.RegexItem(Regex("($PAT_INIT)([yw]?(?:$PAT_MED))($PAT_CODA)'?"), ::getSyllable),
        Transliterator.RegexItem(Regex("([B-DF-HJ-NP-TXZ]*)([AEIOUVWY]+)([B-DF-HJ-NP-TXZ]*)([012]?)'?")) {
            "${ when (it.groupValues[1]) {
                "K" -> "\u1100"
                "G" -> "\u1101"
                "N" -> "\u1102"
                "T" -> "\u1103"
                "D" -> "\u1104"
                "L" -> "\u1105"
                "M" -> "\u1106"
                "P" -> "\u1107"
                "B" -> "\u1108"
                "S" -> "\u1109"
                "SS" -> "\u110a"
                "" -> "\u110b"
                "J" -> "\u110c"
                "DG" -> "\u110d"
                "C" -> "\u110e"
                "KH" -> "\u110f"
                "TH" -> "\u1110"
                "PH" -> "\u1111"
                "H" -> "\u1112"
                "NK" -> "\u1113"
                "NN" -> "\u1114"
                "NT" -> "\u1115"
                "NP" -> "\u1116"
                "TK" -> "\u1117"
                "LN" -> "\u1118"
                "LL" -> "\u1119"
                "LH" -> "\u111a"
                "R" -> "\u111b"
                "MP" -> "\u111c"
                "MF" -> "\u111d"
                "PK" -> "\u111e"
                "PN" -> "\u111f"
                "PT" -> "\u1120"
                "PS" -> "\u1121"
                "PSK" -> "\u1122"
                "PST" -> "\u1123"
                "PSP" -> "\u1124"
                "PSS" -> "\u1125"
                "PSJ" -> "\u1126"
                "PJ" -> "\u1127"
                "PC" -> "\u1128"
                "PTH" -> "\u1129"
                "PPH" -> "\u112a"
                "F" -> "\u112b"
                "FF" -> "\u112c"
                "SK" -> "\u112d"
                "SN" -> "\u112e"
                "ST" -> "\u112f"
                "SL" -> "\u1130"
                "SM" -> "\u1131"
                "SP" -> "\u1132"
                "SPK" -> "\u1133"
                "SSS" -> "\u1134"
                "SNG" -> "\u1135"
                "SJ" -> "\u1136"
                "SC" -> "\u1137"
                "SKH" -> "\u1138"
                "STH" -> "\u1139"
                "SPH" -> "\u113a"
                "SH" -> "\u113b"
                "SX" -> "\u113c"
                "ZX" -> "\u113d"
                "SG" -> "\u113e"
                "ZG" -> "\u113f"
                "Z" -> "\u1140"
                "NGK" -> "\u1141"
                "NGT" -> "\u1142"
                "NGM" -> "\u1143"
                "NGP" -> "\u1144"
                "NGS" -> "\u1145"
                "NGZ" -> "\u1146"
                "NNG" -> "\u1147"
                "NGJ" -> "\u1148"
                "NGC" -> "\u1149"
                "NGTH" -> "\u114a"
                "NGPH" -> "\u114b"
                "NG" -> "\u114c"
                "JNG" -> "\u114d"
                "TSX" -> "\u114e"
                "DZX" -> "\u114f"
                "TSG" -> "\u1150"
                "DZG" -> "\u1151"
                "CKH" -> "\u1152"
                "CH" -> "\u1153"
                "TXH" -> "\u1154"
                "TGH" -> "\u1155"
                "PHP" -> "\u1156"
                "FH" -> "\u1157"
                "HH" -> "\u1158"
                "Q" -> "\u1159"
                "KT" -> "\u115a"
                "NS" -> "\u115b"
                "NJ" -> "\u115c"
                "NH" -> "\u115d"
                "TL" -> "\u115e"

                "TM" -> "\ua960"
                "TP" -> "\ua961"
                "TS" -> "\ua962"
                "TJ" -> "\ua963"
                "LK" -> "\ua964"
                "LG" -> "\ua965"
                "LT" -> "\ua966"
                "LD" -> "\ua967"
                "LM" -> "\ua968"
                "LP" -> "\ua969"
                "LB" -> "\ua96a"
                "LF" -> "\ua96b"
                "LS" -> "\ua96c"
                "LJ" -> "\ua96d"
                "LKH" -> "\ua96e"
                "MK" -> "\ua96f"
                "MT" -> "\ua970"
                "MS" -> "\ua971"
                "PSTH" -> "\ua972"
                "PKH" -> "\ua973"
                "BH" -> "\ua974"
                "SSP" -> "\ua975"
                "NGL" -> "\ua976"
                "NGH" -> "\ua977"
                "DGH" -> "\ua978"
                "TTH" -> "\ua979"
                "PHH" -> "\ua97a"
                "HS" -> "\ua97b"
                "QQ" -> "\ua97c"
                else -> return@RegexItem null
            } }${ when (it.groupValues[2]) {
                "A" -> "\u1161"
                "AI" -> "\u1162"
                "YA" -> "\u1163"
                "YAI" -> "\u1164"
                "E" -> "\u1165"
                "EI" -> "\u1166"
                "YE" -> "\u1167"
                "YEI" -> "\u1168"
                "O" -> "\u1169"
                "OA" -> "\u116a"
                "OAI" -> "\u116b"
                "OI" -> "\u116c"
                "YO" -> "\u116d"
                "U" -> "\u116e"
                "UE" -> "\u116f"
                "UEI" -> "\u1170"
                "UI" -> "\u1171"
                "YU" -> "\u1172"
                "W" -> "\u1173"
                "WI" -> "\u1174"
                "I" -> "\u1175"
                "AO" -> "\u1176"
                "AU" -> "\u1177"
                "YAO" -> "\u1178"
                "YAYO" -> "\u1179"
                "EO" -> "\u117a"
                "EU" -> "\u117b"
                "EW" -> "\u117c"
                "YEO" -> "\u117d"
                "YEU" -> "\u117e"
                "OE" -> "\u117f"
                "OEI" -> "\u1180"
                "OYEI" -> "\u1181"
                "OO" -> "\u1182"
                "OU" -> "\u1183"
                "YOYA" -> "\u1184"
                "YOYAI" -> "\u1185"
                "YOYE" -> "\u1186"
                "YOO" -> "\u1187"
                "YOI" -> "\u1188"
                "UA" -> "\u1189"
                "UAI" -> "\u118a"
                "UEW" -> "\u118b"
                "UYEI" -> "\u118c"
                "UU" -> "\u118d"
                "YUA" -> "\u118e"
                "YUE" -> "\u118f"
                "YUEI" -> "\u1190"
                "YUYE" -> "\u1191"
                "YUYEI" -> "\u1192"
                "YUU" -> "\u1193"
                "YUI" -> "\u1194"
                "WU" -> "\u1195"
                "WW" -> "\u1196"
                "WIU" -> "\u1197"
                "IA" -> "\u1198"
                "IYA" -> "\u1199"
                "IO" -> "\u119a"
                "IU" -> "\u119b"
                "IW" -> "\u119c"
                "IV" -> "\u119d"
                "V" -> "\u119e"
                "VE" -> "\u119f"
                "VU" -> "\u11a0"
                "VI" -> "\u11a1"
                "VV" -> "\u11a2"
                "AW" -> "\u11a3"
                "YAU" -> "\u11a4"
                "YEYA" -> "\u11a5"
                "OYA" -> "\u11a6"
                "OYAI" -> "\u11a7"

                "OYE" -> "\ud7b0"
                "OOI" -> "\ud7b1"
                "YOA" -> "\ud7b2"
                "YOAE" -> "\ud7b3"
                "YOE" -> "\ud7b4"
                "UYE" -> "\ud7b5"
                "UII" -> "\ud7b6"
                "YUAI" -> "\ud7b7"
                "YUO" -> "\ud7b8"
                "WA" -> "\ud7b9"
                "WE" -> "\ud7ba"
                "WEI" -> "\ud7bb"
                "WO" -> "\ud7bc"
                "IYAO" -> "\ud7bd"
                "IYAI" -> "\ud7be"
                "IYE" -> "\ud7bf"
                "IYEI" -> "\ud7c0"
                "IOI" -> "\ud7c1"
                "IYO" -> "\ud7c2"
                "IYU" -> "\ud7c3"
                "II" -> "\ud7c4"
                "VA" -> "\ud7c5"
                "VEI" -> "\ud7c6"
                else -> return@RegexItem null
            } }${ when (it.groupValues[3]) {
                "" -> ""
                "K" -> "\u11a8"
                "G" -> "\u11a9"
                "KS" -> "\u11aa"
                "N" -> "\u11ab"
                "NJ" -> "\u11ac"
                "NH" -> "\u11ad"
                "T" -> "\u11ae"
                "L" -> "\u11af"
                "LK" -> "\u11b0"
                "LM" -> "\u11b1"
                "LP" -> "\u11b2"
                "LS" -> "\u11b3"
                "LTH" -> "\u11b4"
                "LPH" -> "\u11b5"
                "LH" -> "\u11b6"
                "M" -> "\u11b7"
                "P" -> "\u11b8"
                "PS" -> "\u11b9"
                "S" -> "\u11ba"
                "SS" -> "\u11bb"
                "QNG" -> "\u11bc"
                "J" -> "\u11bd"
                "C" -> "\u11be"
                "KH" -> "\u11bf"
                "TH" -> "\u11c0"
                "PH" -> "\u11c1"
                "H" -> "\u11c2"
                "KL" -> "\u11c3"
                "KSK" -> "\u11c4"
                "NK" -> "\u11c5"
                "NT" -> "\u11c6"
                "NS" -> "\u11c7"
                "NZ" -> "\u11c8"
                "NTH" -> "\u11c9"
                "TK" -> "\u11ca"
                "TL" -> "\u11cb"
                "LKS" -> "\u11cc"
                "LN" -> "\u11cd"
                "LT" -> "\u11ce"
                "LDH" -> "\u11cf"
                "LL" -> "\u11d0"
                "LMK" -> "\u11d1"
                "LMS" -> "\u11d2"
                "LPS" -> "\u11d3"
                "LBH" -> "\u11d4"
                "LF" -> "\u11d5"
                "LSS" -> "\u11d6"
                "LZ" -> "\u11d7"
                "LKH" -> "\u11d8"
                "LQ" -> "\u11d9"
                "MK" -> "\u11da"
                "ML" -> "\u11db"
                "MP" -> "\u11dc"
                "MS" -> "\u11dd"
                "MSS" -> "\u11de"
                "MZ" -> "\u11df"
                "MC" -> "\u11e0"
                "MH" -> "\u11e1"
                "MF" -> "\u11e2"
                "PL" -> "\u11e3"
                "PPH" -> "\u11e4"
                "BH" -> "\u11e5"
                "F" -> "\u11e6"
                "SK" -> "\u11e7"
                "ST" -> "\u11e8"
                "SL" -> "\u11e9"
                "SP" -> "\u11ea"
                "Z" -> "\u11eb"
                "NGK" -> "\u11ec"
                "NGG" -> "\u11ed"
                "NNG" -> "\u11ee"
                "NGKH" -> "\u11ef"
                "NG" -> "\u11f0"
                "NGS" -> "\u11f1"
                "NGZ" -> "\u11f2"
                "PHP" -> "\u11f3"
                "FH" -> "\u11f4"
                "HN" -> "\u11f5"
                "HL" -> "\u11f6"
                "HM" -> "\u11f7"
                "HP" -> "\u11f8"
                "Q" -> "\u11f9"
                "KN" -> "\u11fa"
                "KP" -> "\u11fb"
                "KC" -> "\u11fc"
                "KKH" -> "\u11fd"
                "GH" -> "\u11fe"
                "NN" -> "\u11ff"

                "NL" -> "\ud7cb"
                "NC" -> "\ud7cc"
                "D" -> "\ud7cd"
                "DP" -> "\ud7ce"
                "TP" -> "\ud7cf"
                "TS" -> "\ud7d0"
                "TSK" -> "\ud7d1"
                "TJ" -> "\ud7d2"
                "TC" -> "\ud7d3"
                "TTH" -> "\ud7d4"
                "LG" -> "\ud7d5"
                "LGH" -> "\ud7d6"
                "LLKH" -> "\ud7d7"
                "LMH" -> "\ud7d8"
                "LPT" -> "\ud7d9"
                "LPPH" -> "\ud7da"
                "LNG" -> "\ud7db"
                "LQH" -> "\ud7dc"
                "R" -> "\ud7dd"
                "MN" -> "\ud7de"
                "MNN" -> "\ud7df"
                "MM" -> "\ud7e0"
                "MPS" -> "\ud7e1"
                "MJ" -> "\ud7e2"
                "PT" -> "\ud7e3"
                "PLPH" -> "\ud7e4"
                "PM" -> "\ud7e5"
                "B" -> "\ud7e6"
                "PST" -> "\ud7e7"
                "PJ" -> "\ud7e8"
                "PC" -> "\ud7e9"
                "SM" -> "\ud7ea"
                "SF" -> "\ud7eb"
                "SSK" -> "\ud7ec"
                "SST" -> "\ud7ed"
                "SZ" -> "\ud7ee"
                "SJ" -> "\ud7ef"
                "SC" -> "\ud7f0"
                "STH" -> "\ud7f1"
                "SH" -> "\ud7f2"
                "ZP" -> "\ud7f3"
                "ZF" -> "\ud7f4"
                "NGM" -> "\ud7f5"
                "NGH" -> "\ud7f6"
                "JP" -> "\ud7f7"
                "JB" -> "\ud7f8"
                "DG" -> "\ud7f9"
                "PHS" -> "\ud7fa"
                "PHTH" -> "\ud7fb"
                else -> return@RegexItem null
            } }${ when (it.groupValues[4]) {
                "1" -> "\u302e"
                "2" -> "\u302f"
                else -> ""
            } }"
        },
        Transliterator.RegexItem(Regex("..."), mapOf(
            "lps" to "\u316B",
            "psk" to "\u3174",
            "pst" to "\u3175",
            "pch" to "\u3176",
            "pth" to "\u3177",
            "sch" to "\u317E",
            "ngq" to "\u3181",
            "ngs" to "\u3182",
            "ngz" to "\u3183",
        )),
        Transliterator.RegexItem(Regex(".."), mapOf(
            "kk" to "\u3132",
            "tt" to "\u3138",
            "pp" to "\u3143",
            "ss" to "\u3146",
            "ng" to "\u3147",
            "jj" to "\u3149",
            "ch" to "\u314A",

            "ks" to "\u3133",
            "nj" to "\u3135",
            "nh" to "\u3136",
            "lk" to "\u313A", "lg" to "\u313A",
            "lm" to "\u313B",
            "lb" to "\u313C",
            "ls" to "\u313D",
            "lt" to "\u313E",
            "lp" to "\u313F",
            "lh" to "\u3140",
            "ps" to "\u3144",

            "nn" to "\u3165",
            "nt" to "\u3166",
            "ns" to "\u3167",
            "nz" to "\u3168",
            "lx" to "\u3169",
            "ld" to "\u316A",
            "lz" to "\u316C",
            "lq" to "\u316D",
            "mp" to "\u316E",
            "ms" to "\u316F",
            "mz" to "\u3170",
            "pk" to "\u3172",
            "pt" to "\u3173",
            "sk" to "\u317A",
            "sn" to "\u317B",
            "st" to "\u317C",
            "sp" to "\u317D",
            "fh" to "\u3184",
            "hh" to "\u3185",
        )),
        Transliterator.RegexItem(Regex("."), mapOf(
            "g" to "\u3131",
            "n" to "\u3134",
            "d" to "\u3137",
            "r" to "\u3139", "l" to "\u3139",
            "m" to "\u3141",
            "b" to "\u3142",
            "s" to "\u3145",
            "j" to "\u3148",
            "c" to "\u314A",
            "k" to "\u314B",
            "t" to "\u314C",
            "p" to "\u314D",
            "h" to "\u314E",

            "w" to "\u3171",
            "f" to "\u3178",
            "v" to "\u3179",
            "z" to "\u317F",
            "y" to "\u3180",
            "q" to "\u3186",
        )),
    )
}