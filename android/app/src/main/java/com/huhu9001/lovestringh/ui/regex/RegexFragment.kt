package com.huhu9001.lovestringh.ui.regex

import androidx.core.widget.doAfterTextChanged
import com.huhu9001.lovestringh.R
import com.huhu9001.lovestringh.regex.RegexHandler

class RegexFragment : androidx.fragment.app.Fragment() {
    override fun onCreateView(
        inflater: android.view.LayoutInflater,
        container: android.view.ViewGroup?,
        savedInstanceState: android.os.Bundle?)
    : android.view.View {
        val context = requireContext()
        val body = com.huhu9001.lovestringh.databinding.FragmentRegexBinding.inflate(inflater, container, false)

        val viewmodel = androidx.lifecycle.ViewModelProvider(this)[RegexViewModel::class.java]

        val toastInvalidRegex = android.widget.Toast.makeText(context, R.string.toast_invalid_regex, android.widget.Toast.LENGTH_SHORT)
        val toastOver = android.widget.Toast.makeText(context, R.string.toast_regex_over, android.widget.Toast.LENGTH_SHORT)
        val toastNotfound = android.widget.Toast.makeText(context, R.string.toast_regex_notfound, android.widget.Toast.LENGTH_SHORT)
        fun toastReplaceAll(count:Int) {
            android.widget.Toast.makeText(context, context.resources.getString(R.string.toast_regex_replaceall, count), android.widget.Toast.LENGTH_SHORT).show()
        }
        fun toast(e:Throwable) {
            when(e) {
                is java.util.regex.PatternSyntaxException -> toastInvalidRegex
                else -> android.widget.Toast.makeText(context, e.toString(), android.widget.Toast.LENGTH_SHORT)
            }.show()
        }

        fun loadInput() {
            viewmodel.re.regex = body.edittextRegex.text.toString()
            viewmodel.re.repl = body.edittextReplace.text.toString()
            viewmodel.re.setText(
                body.edittextMain.text.toString(),
                body.edittextMain.selectionStart,
                body.edittextMain.selectionEnd)
        }

        val handler = android.os.Handler(context.mainLooper)
        handler.post {
            body.edittextMain.setOnFocusChangeListener { _, hasFocus ->
                if (hasFocus) handler.post { body.edittextMain.setSelection(viewmodel.re.start, viewmodel.re.end) }
                else
                    viewmodel.re.setText(body.edittextMain.text.toString(), body.edittextMain.selectionStart, body.edittextMain.selectionEnd)
            }
            body.edittextRegex.doAfterTextChanged { viewmodel.re.regex = body.edittextReplace.text.toString() }
            body.edittextReplace.doAfterTextChanged { viewmodel.re.repl = body.edittextReplace.text.toString() }
            body.buttonFindnext.setOnClickListener {
                loadInput()
                viewmodel.re.findNext().fold({
                    when (it) {
                        RegexHandler.ResultCode.NOTFOUND -> toastNotfound.show()
                        RegexHandler.ResultCode.OVER -> toastOver.show()
                    }
                    body.edittextMain.setSelection(viewmodel.re.start, viewmodel.re.end)
                    body.edittextMain.requestFocus()
                }) { toast(it) }
            }
            body.buttonReplace.setOnClickListener {
                loadInput()
                viewmodel.re.replace().fold({
                    when (it) {
                        RegexHandler.ResultCode.NOTFOUND -> toastNotfound.show()
                        RegexHandler.ResultCode.OVER -> toastOver.show()
                    }
                    body.edittextMain.setText(viewmodel.re.text)
                    body.edittextMain.setSelection(viewmodel.re.start, viewmodel.re.end)
                    body.edittextMain.requestFocus()
                }) { toast(it) }
            }
            body.buttonReplaceall.setOnClickListener {
                loadInput()
                viewmodel.re.replaceAll().fold({
                    toastReplaceAll(it)
                    body.edittextMain.setText(viewmodel.re.text)
                    body.edittextMain.setSelection(viewmodel.re.start, viewmodel.re.end)
                    body.edittextMain.requestFocus()
                }) { toast(it) }
            }
        }

        return body.root
    }
}