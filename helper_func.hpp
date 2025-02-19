#ifndef HELPER_FUNC_HPP
#define HELPER_FUNC_HPP

#include<stdint.h>
#include<string_view>

template<typename SubMatch>
[[nodiscard]]std::string_view to_view(SubMatch const&m) {
	return std::string_view(m.begin(), m.end());
}

inline uint32_t u16sur1(uint32_t codepoint) {
	return (codepoint >> 10) + 0xD7C0;
}

inline uint32_t u16sur2(uint32_t codepoint) {
	return codepoint & 0x3FF | 0xDC00;
}

inline uint32_t u16sur(uint32_t sur1, uint32_t sur2) {
	return sur1 - 0xD7C0U << 10 | sur2 & ~0xDC00U;
}

template<typename TC> concept TChar8 = sizeof(TC) == sizeof(uint8_t);

template<TChar8 Char8> int u32u8(uint32_t code, Char8*c) {
	if (code < 0x80) {
		c[0] = code;
		return 1;
	}
	else if (code < 0x800) {
		c[1] = code & 0x3F | 0x80;
		c[0] = code >> 6 | 0xC0;
		return 2;
	}
	else if (code < 0x10000) {
		c[2] = code & 0x3F | 0x80;
		c[1] = code >> 6 & 0x3F | 0x80;
		c[0] = code >> 12 | 0xE0;
		return 3;
	}
	else if (code < 0x110000) {
		c[3] = code & 0x3F | 0x80;
		c[2] = code >> 6 & 0x3F | 0x80;
		c[1] = code >> 12 & 0x3F | 0x80;
		c[0] = code >> 18 | 0xF0;
		return 4;
	}
	else return 0;
}

template<TChar8 Char8> uint32_t u8u32(Char8 const**c, Char8 const*end) {
	static constexpr int tb_len[] = {
		1, 1, 1, 1, 1, 1, 1, 1,
		0, 0, 0, 0,
		2, 2,
		3, 4,
	};
	static constexpr uint32_t tb_msk[] = {
		~0U, ~0U, ~0U, ~0U, ~0U, ~0U, ~0U, ~0U,
		0, 0, 0, 0,
		~0xC0U, ~0xC0U,
		~0xE0U, ~0xF0U,
	};
	uint32_t codepoint = static_cast<unsigned char>(**c);
	uint32_t const sig = codepoint >> 4;
	int len = tb_len[sig];
	++*c;
	if (len == 0) return 0xFFFDU;
	codepoint &= tb_msk[sig];
	while (--len > 0) {
		unsigned char uc;
		if (*c >= end || (uc = **c, (uc & 0xC0U) != 0x80U)) return 0xFFFDU;
		++*c;
		codepoint = codepoint << 6 | uc & 0x3FU;
	}
	return codepoint;
}

#endif