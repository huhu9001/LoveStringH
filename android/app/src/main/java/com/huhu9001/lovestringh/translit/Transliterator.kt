package com.huhu9001.lovestringh.translit

class Transliterator private constructor(val name:String, private val ritems:Array<RegexItem>) {
    class RegexItem {
        constructor(regex:Regex, replace:(MatchResult)->String?) {
            re = regex; me = replace
            strReplace = ""; dictReplace = emptyMap()
        }
        constructor(regex:Regex, replace:String) {
            re = regex; me = ::replaceS
            strReplace = replace; dictReplace = emptyMap()
        }
        constructor(regex:Regex, replace:Map<String, String>) {
            re = regex; me = ::replaceD
            strReplace = ""; dictReplace = replace
        }

        val re:Regex
        val me:(MatchResult)->String?

        private val strReplace:String
        private val dictReplace:Map<out String, String>

        private fun replaceS(m:MatchResult) = Regex("\\$(\\d+)").replace(strReplace) {
            m.groupValues[it.groupValues[1].toInt()]
        }
        private fun replaceD(m:MatchResult) = dictReplace.getOrDefault(m.value, null)
    }

    companion object {
        val trItems = arrayOf(
            Transliterator("Latin", Latin.ritems),
            Transliterator("Greek", Greek.ritems),
            Transliterator("Cyrillic", Cyrillic.ritems),
            Transliterator("Arabic", Arabic.ritems),
            Transliterator("Hangul", Hangul.ritems),
        )
    }

    override fun toString() = name
    fun translit(str:String):String {
        val result = StringBuilder()
        var i0 = 0
        var emptyMatch = false
        while (i0 < str.length) {
            var substr:String? = null
            for (ritem in ritems) {
                val m = ritem.re.matchAt(str, i0) ?: continue
                if (m.value.isEmpty()) {
                    if (!emptyMatch) {
                        substr = ritem.me(m) ?: continue
                        emptyMatch = true
                        result.append(substr)
                        break
                    }
                }
                else {
                    substr = ritem.me(m) ?: continue
                    emptyMatch = false
                    i0 += m.value.length
                    result.append(substr)
                    break
                }
            }
            if (substr == null) {
                result.append(str[i0])
                i0 += 1
            }
        }
        return result.toString()
    }
}