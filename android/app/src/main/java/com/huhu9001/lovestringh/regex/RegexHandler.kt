package com.huhu9001.lovestringh.regex

class RegexHandler {
    object ResultCode {
        const val OK = 0
        const val NOTFOUND = -1
        const val OVER = -2
    }
    var text = ""; private set
    var start = 0; private set
    var end = 0; private set
    var regex = ""
    var repl = ""

    private var emptyMatch = false

    fun setText(text:String, start:Int, end:Int) {
        if (this.text == text && this.end == end && this.start == start) return
        this.text = text
        this.end = if (end in 0 .. text.length) end else 0
        this.start = if (start in 0 .. this.end) start else 0
        emptyMatch = false
    }
    fun findNext():Result<Int> {
		val reg = try { Regex(regex) }
        catch (e:java.util.regex.PatternSyntaxException) { return Result.failure(e) }
        val m = if (emptyMatch) {
            if (end < text.length) reg.find(text, end + 1) else null
        } else reg.find(text, end)
        if (m != null) {
            start = m.range.first
            end = m.range.last + 1
            emptyMatch = start == end
            return Result.success(ResultCode.OK)
        }
        else {
            val mOver = reg.find(text) ?: return Result.success(ResultCode.NOTFOUND)
            start = mOver.range.first
            end = mOver.range.last + 1
            emptyMatch = start == end
            return Result.success(ResultCode.OVER)
        }
    }
    fun replace():Result<Int> {
		val reg = try { Regex(regex) }
        catch (e:java.util.regex.PatternSyntaxException) { return Result.failure(e) }
        val m = reg.matchAt(text, start) ?: return findNext()
        if (m.range.last + 1 != end) {
            end = m.range.last + 1
            emptyMatch = start == end
            return Result.success(ResultCode.OK)
        }

        val replacement = try {
            substituteGroupRefs(m, repl)
        } catch(e:Exception) { return Result.failure(e) }
        text = "${text.substring(0, start)}$replacement${text.substring(end)}"
        end += replacement.length - m.value.length
        start = end

        return findNext()
    }
    fun replaceAll():Result<Int> {
		val reg = try { Regex(regex) }
        catch (e:java.util.regex.PatternSyntaxException) { return Result.failure(e) }
        var count = 0
        text = try {
            reg.replace(text) {
                count += 1
                substituteGroupRefs(it, repl)
            }
        } catch(e:Exception) { return Result.failure(e) }
        start = 0
        end = 0
        emptyMatch = false
        return Result.success(count)
    }

    /* https://github.com/JetBrains/kotlin/blob/rrr/2.1.0/core-docs/libraries/stdlib/native-wasm/src/kotlin/text/Regex.kt#L386 */
    private fun substituteGroupRefs(match:MatchResult, replacement:String):String {
        var index = 0
        val result = StringBuilder(replacement.length)

        while (index < replacement.length) {
            val char = replacement[index++]
            if (char == '\\') {
                if (index == replacement.length)
                    throw IllegalArgumentException("The Char to be escaped is missing")

                result.append(replacement[index++])
            } else if (char == '$') {
                if (index == replacement.length)
                    throw IllegalArgumentException("Capturing group index is missing")

                if (replacement[index] == '{') {
                    val endIndex = replacement.readGroupName(++index)
                    val groupName = replacement.substring(index, endIndex)

                    if (groupName.isEmpty())
                        throw IllegalArgumentException("Named capturing group reference should have a non-empty name")
                    if (groupName[0] in '0'..'9')
                        throw IllegalArgumentException("Named capturing group reference {$groupName} should start with a letter")

                    if (endIndex == replacement.length || replacement[endIndex] != '}')
                        throw IllegalArgumentException("Named capturing group reference is missing trailing '}'")

                    result.append(match.groups[groupName]?.value ?: "")
                    index = endIndex + 1    // skip past '}'
                } else {
                    if (replacement[index] !in '0'..'9')
                        throw IllegalArgumentException("Invalid capturing group reference")

                    val groups = match.groups
                    val endIndex = replacement.readGroupIndex(index, groups.size)
                    val groupIndex = replacement.substring(index, endIndex).toInt()

                    if (groupIndex >= groups.size)
                        throw IndexOutOfBoundsException("Group with index $groupIndex does not exist")

                    result.append(groups[groupIndex]?.value ?: "")
                    index = endIndex
                }
            } else {
                result.append(char)
            }
        }
        return result.toString()
    }
    private fun String.readGroupName(startIndex:Int):Int {
        var index = startIndex
        while (index < length) {
            val char = this[index]
            if (char in 'a'..'z' || char in 'A'..'Z' || char in '0'..'9') {
                index++
            } else {
                break
            }
        }
        return index
    }

    private fun String.readGroupIndex(startIndex:Int, groupCount:Int):Int {
        // at least one digit after '$' is always captured
        var index = startIndex + 1
        var groupIndex = this[startIndex] - '0'

        // capture the largest valid group index
        while (index < length && this[index] in '0'..'9') {
            val newGroupIndex = (groupIndex * 10) + (this[index] - '0')
            if (newGroupIndex in 0 until groupCount) {
                groupIndex = newGroupIndex
                index++
            } else {
                break
            }
        }
        return index
    }
}