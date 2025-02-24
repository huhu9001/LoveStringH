package com.huhu9001.lovestringh.encoding

import java.nio.charset.Charset

class Encoder private constructor(val name:String, private val e: Charset?, val styles:Array<EscapeStyle>) {
    class EscapeStyle(val name:String, val escape:(Int)->String) { override fun toString() = name }
    companion object {
        private val rgxEscape =
            Regex("\\\\([0-7]{1,3})|\\\\x([A-Fa-f0-9]{1,8})|%([A-Fa-f0-9]{2})|\\\\u([A-Fa-f0-9]{4})|\\\\U([A-Fa-f0-9]{8})|&#(\\d{1,10});|&#x([A-Fa-f0-9]{1,8});")
        private val defaultEscapeStyle = arrayOf(
            EscapeStyle("\\x") { "\\x%X".format(it) },
            EscapeStyle("%") { "%%%02X".format(it) },
        )

        val encoders = arrayOf(
            Encoder("Unicode", null, arrayOf(
                EscapeStyle("\\u") {
                    if (it < 0 || it >= 0x10000) "\\U%08X".format(it)
                    else if (it >= 0x80) "\\u%04X".format(it)
                    else "\\x%X".format(it)
                },
                EscapeStyle("&#x") { "&#x%X;".format(it) }
            )),
            Encoder("UTF-8", Charset.forName("UTF-8"), defaultEscapeStyle),
            Encoder("UTF-16", null, arrayOf(defaultEscapeStyle[0])),
            Encoder("GB 18030", Charset.forName("GB18030"), defaultEscapeStyle),
            Encoder("GB 2312", Charset.forName("GB2312"), defaultEscapeStyle),
            Encoder("Shift-JIS", Charset.forName("Shift-JIS"), defaultEscapeStyle),
            Encoder("Big5", Charset.forName("Big5"), defaultEscapeStyle),
            Encoder("EUC-JP", Charset.forName("EUC-JP"), defaultEscapeStyle),
            Encoder("EUC-KR", Charset.forName("EUC-KR"), defaultEscapeStyle),
        )
    }

    override fun toString() = name

    fun encode(str:CharSequence, escape:(Int)->String, strNE:CharSequence) = try {
        Regex("[^${strNE}]+")
    } catch (_:Exception) { Regex("^\b$") }.replace(str) {
        val strResult = StringBuilder()
        if (e != null) {
            val bs = e.encode(java.nio.CharBuffer.wrap(it.value))
            while (true) {
                val b = try { bs.get() } catch (_:java.nio.BufferUnderflowException) { break }
                strResult.append(escape(b.toUByte().toInt()))
            }
        }
        else {
            var surrogate = '\u0000'
            for (char in it.value) {
                if (surrogate != '\u0000') {
                    if (char in '\uDC00'..< '\uE000') {
                        strResult.append(escape((surrogate.code - 0xD7C0 shl 10) or (char.code - 0xDC00)))
                        surrogate = '\u0000'
                        continue
                    }
                    else {
                        strResult.append(escape(surrogate.code))
                        surrogate = '\u0000'
                    }
                }
                if (name == "Unicode" && char in '\uD800' ..< '\uDC00') surrogate = char
                else strResult.append(escape(char.code))
            }
            if (surrogate != '\u0000') strResult.append(escape(surrogate.code))
        }
        strResult.toString()
    }

    fun decode(str:CharSequence):String {
        val strResult = StringBuilder()
        fun decodePiece(bs:List<Byte>) =
            e?.decode(java.nio.ByteBuffer.wrap(bs.toByteArray())) ?: java.nio.CharBuffer.wrap(CharArray(bs.size / 2) {
                Char(bs[it * 2].toUByte().toInt() or bs[it * 2 + 1].toUByte().toInt().shl(8))
            })

        var endLastMatch = 0
        val bs = mutableListOf<Byte>()
        for (m in rgxEscape.findAll(str)) {
            if (endLastMatch < m.range.first) {
                strResult.append(decodePiece(bs))
                bs.clear()
                strResult.append(str.slice(endLastMatch ..< m.range.first))
            }

            val c = (if (m.groups[1] != null) m.groupValues[1].toIntOrNull(8)
            else if (m.groups[2] != null) m.groupValues[2].toIntOrNull(16)
            else if (m.groups[3] != null) m.groupValues[3].toIntOrNull(16)
            else {
                strResult.append(decodePiece(bs))
                bs.clear()

                val cWhole = (if (m.groups[4] != null) m.groupValues[4].toIntOrNull(16)
                else if (m.groups[5] != null) m.groupValues[5].toIntOrNull(16)
                else if (m.groups[6] != null) m.groupValues[6].toIntOrNull(10)
                else m.groupValues[7].toIntOrNull(16)) ?: 0x3F

                if (cWhole in 0 ..< 0x110000) {
                    if (cWhole < 0x10000) strResult.append(Char(cWhole))
                    else {
                        strResult.append(Char(cWhole.ushr(10) + 0xD7C0))
                        strResult.append(Char(cWhole.and(0x3FF) + 0xDC00))
                    }
                }
                else strResult.append("?")
                endLastMatch = m.range.last + 1
                continue
            }) ?: 0x3F

            if (e == null) {
                if (c in 0 ..< 0x10000) {
                    bs.add(c.toByte())
                    bs.add(c.ushr(8).toByte())
                }
                else {
                    if (name == "Unicode" && c in 0x10000 ..< 0x110000) {
                        val s1 = c.ushr(10) + 0xD7C0
                        val s2 = c.and(0x3FF) + 0xDC00
                        bs.add(s1.toByte())
                        bs.add(s1.ushr(8).toByte())
                        bs.add(s2.toByte())
                        bs.add(s2.ushr(8).toByte())
                    }
                    else {
                        bs.add(c.toByte())
                        bs.add(c.ushr(8).toByte())
                        bs.add(c.ushr(16).toByte())
                        bs.add(c.ushr(24).toByte())
                    }
                }
            }
            else {
                bs.add(c.toByte())
                var cmut = c ushr 8
                while (cmut != 0) {
                    bs.add(cmut.toByte())
                    cmut = cmut ushr 8
                }
            }
            endLastMatch = m.range.last + 1
        }
        strResult.append(decodePiece(bs))
        strResult.append(str.slice(endLastMatch ..< str.length))
        return strResult.toString()
    }
}