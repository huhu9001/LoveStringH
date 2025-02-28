package com.huhu9001.lovestringh.ui.translit

import android.view.View
import androidx.core.widget.doAfterTextChanged
import com.huhu9001.lovestringh.translit.Transliterator

class TranslitFragment : androidx.fragment.app.Fragment() {
    private class ScriptAdapter(context:android.content.Context)
        :android.widget.ArrayAdapter<Transliterator>(context, android.R.layout.simple_spinner_item) {
        override fun getCount() = Transliterator.trItems.size
        override fun getItem(position:Int) = Transliterator.trItems[position]
        init { setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item) }
    }

    private var disableOutput = false

    override fun onCreateView(
        inflater: android.view.LayoutInflater,
        container: android.view.ViewGroup?,
        savedInstanceState: android.os.Bundle?)
    : View {
        val context = requireContext()
        val body = com.huhu9001.lovestringh.databinding.FragmentTranslitBinding.inflate(inflater, container, false)

        @Suppress("UNUSED_PARAMETER") fun textChangedInput(p0:android.text.Editable?) {
            if (disableOutput) return
            disableOutput = true
            body.edittextOutput.setText((body.spinnerScript.selectedItem as Transliterator).translit(body.edittextInput.text.toString()))
            disableOutput = false
        }
        body.spinnerScript.adapter = ScriptAdapter(context)

        val clipboard = context.getSystemService(android.content.Context.CLIPBOARD_SERVICE) as android.content.ClipboardManager
        val toastCopied =
            android.widget.Toast.makeText(context, com.huhu9001.lovestringh.R.string.toast_output_copied, android.widget.Toast.LENGTH_SHORT)

        android.os.Handler(context.mainLooper).post {
            body.edittextInput.doAfterTextChanged(::textChangedInput)
            body.edittextOutput.doAfterTextChanged(::textChangedInput)
            body.spinnerScript.onItemSelectedListener = object:android.widget.AdapterView.OnItemSelectedListener {
                override fun onItemSelected(p0:android.widget.AdapterView<*>?, p1:View?, p2:Int, p3:Long) =
                    textChangedInput(body.edittextInput.text)
                override fun onNothingSelected(p0:android.widget.AdapterView<*>?) = p0?.setSelection(0) ?: Unit
            }
            body.buttonCopy.setOnClickListener {
                clipboard.setPrimaryClip(android.content.ClipData.newPlainText(context.getString(com.huhu9001.lovestringh.R.string.app_name), body.edittextOutput.text))
                toastCopied.show()
                body.edittextInput.selectAll()
                body.edittextInput.requestFocus()
            }
            body.buttonClear.setOnClickListener {
                body.edittextInput.text.clear()
                body.edittextInput.requestFocus()
            }
        }

        return body.root
    }
}