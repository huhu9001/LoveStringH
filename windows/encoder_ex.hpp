#ifndef ENCODER_EX_HPP
#define ENCODER_EX_HPP

#include"encoder.hpp"

namespace lovestringh {
	inline constexpr Encoder<wchar_t> all_encoders[] = {
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_UTF32(), Encoder<wchar_t>::styles_u32),
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_UTF8(), Encoder<wchar_t>::styles_default),
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_UTF16(), std::span(Encoder<wchar_t>::styles_default, 1)),
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_GB18030(), Encoder<wchar_t>::styles_default),
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_GB2312(), Encoder<wchar_t>::styles_default),
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_Shift_JIS(), Encoder<wchar_t>::styles_default),
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_Big5(), Encoder<wchar_t>::styles_default),
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_EUC_JP(), Encoder<wchar_t>::styles_default),
		Encoder<wchar_t>(Encoder<wchar_t>::NAME_EUC_KR(), Encoder<wchar_t>::styles_default),
	};
}

#endif