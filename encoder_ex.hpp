#ifndef ENCODER_EX_HPP
#define ENCODER_EX_HPP

#include"encoder.hpp"

namespace lovestringh {
	inline constexpr Encoder all_encoders[] = {
		Encoder(Encoder::NAME_UTF32, Encoder::styles_u32),
		Encoder(Encoder::NAME_UTF8, Encoder::styles_default),
		Encoder(Encoder::NAME_UTF16, std::span(Encoder::styles_default, 1)),
		Encoder(Encoder::NAME_GB18030, Encoder::styles_default),
		Encoder(Encoder::NAME_GB2312, Encoder::styles_default),
		Encoder(Encoder::NAME_Shift_JIS, Encoder::styles_default),
		Encoder(Encoder::NAME_Big5, Encoder::styles_default),
		Encoder(Encoder::NAME_EUC_JP, Encoder::styles_default),
		Encoder(Encoder::NAME_EUC_KR, Encoder::styles_default),
	};
}

#endif