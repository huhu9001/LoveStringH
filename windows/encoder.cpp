#include"encoder.hpp"

#include"helper_func.hpp"
#include<boost/regex.hpp>
#include<ctre.hpp>

template<typename TChar> struct Pseudoregex {
	std::basic_string_view<TChar>const s;
	boost::regex re;
	std::string sc;

	Pseudoregex(std::basic_string_view<TChar>s, std::basic_string_view<TChar>ex) :s(s) {
		sc.reserve(s.size());
		for (auto c : s) {
			char cc = static_cast<char>(c);
			if (cc == c) sc.push_back(cc);
			else sc.push_back(static_cast<char>(0x80));
		}

		std::string exc;
		exc.reserve(ex.size());
		for (auto c : ex) {
			char cc = static_cast<char>(c);
			if (cc == c) exc.push_back(cc);
			else {
				exc.clear();
				break;
			}
		}
		if (!exc.empty()) {
			std::string str_exclude;
			str_exclude.append("[^");
			str_exclude.append(exc);
			str_exclude.append("]+");
			re.assign(str_exclude, boost::regex_constants::nosubs
				| boost::regex_constants::no_except);
		}
		if (re.empty()) {
			re.assign(".*", boost::regex_constants::nosubs
				| boost::regex_constants::no_except);
		}
	}

	struct Iter {
		std::basic_string_view<TChar>const s;
		boost::cregex_iterator citer;

		bool operator==(Iter other) { return citer == other.citer; }
		void operator++() { ++citer; }
		std::basic_string_view<TChar> operator*() {
			TChar const*const start = s.data() + citer->position();
			TChar const*const end = start + citer->length();
			return std::basic_string_view(start, end);
		}
	};

	Iter begin() const {
		char const*const s_start = sc.data();
		char const*const s_end = s_start + sc.size();
		return {
			.s = s,
			.citer = boost::cregex_iterator(s_start, s_end, re),
		};
	}

	Iter end() const {
		return {
			.s = s,
			.citer = boost::cregex_iterator()
		};
	}
};

template<typename TChar> void append_escape(
	std::basic_string<TChar>&result,
	std::string(*escape)(uint32_t),
	uint32_t cp)
{
	for (unsigned char const c : escape(cp))
		result.push_back(static_cast<TChar>(c));
}

void lovestringh::Encoder::encode(
	std::u8string_view s,
	std::u8string_view exclude,
	std::string(*escape)(uint32_t),
	std::u8string&out) const
{
	char8_t const*clast = s.data(), *const cend = clast + s.length();
	for (auto s_sub : Pseudoregex(std::u8string_view(clast, cend), exclude)) {
		char8_t const*const csubbegin = s_sub.data();
		char8_t const*const csubend = csubbegin + s_sub.length();
		if (clast < csubbegin) out.append(clast, csubbegin);

		if (name == NAME_UTF8) {
			for (char8_t const*c = csubbegin; c < csubend; ++c)
				append_escape(out, escape, static_cast<unsigned char>(*c));
		}
		else if (name == NAME_UTF32 || name == NAME_UTF16) {
			for (char8_t const*c = csubbegin; c < csubend;) {
				uint32_t const codepoint = u8u32(&c, csubend);
				if (codepoint > 0x10000 && codepoint < 0x110000 && name == NAME_UTF16) {
					append_escape(out, escape, u16sur1(codepoint));
					append_escape(out, escape, u16sur2(codepoint));
				}
				else append_escape(out, escape, codepoint);
			}
		}
		else for (unsigned char uc : to_charset(name, std::u8string_view(csubbegin, csubend)))
			append_escape(out, escape, uc);

		clast = csubend;
	}
	if (clast < cend) out.append(clast, cend);
}

void lovestringh::Encoder::encode(
	std::u16string_view s,
	std::u16string_view exclude,
	std::string(*escape)(uint32_t),
	std::u16string&out) const
{
	char16_t const*clast = s.data(), *const cend = clast + s.length();
	for (auto s_sub : Pseudoregex(std::u16string_view(clast, cend), exclude)) {
		char16_t const*const csubbegin = s_sub.data();
		char16_t const*const csubend = csubbegin + s_sub.length();
		if (clast < csubbegin)
			out.append(clast, csubbegin);

		if (name == NAME_UTF16) {
			for (char16_t const*c = csubbegin; c < csubend; ++c)
				append_escape(out, escape, *c);
		}
		else if (name == NAME_UTF32) {
            char16_t surrogate = 0;
			for (char16_t const*c = csubbegin; c < csubend; ++c) {
				if (surrogate != 0) {
					if (*c >= 0xDC00U && *c < 0xE000U) {
						append_escape(out, escape, u16sur(surrogate, *c));
						surrogate = 0;
						continue;
					}
					else {
						append_escape(out, escape, surrogate);
						surrogate = 0;
					}
				}
				if (*c >= 0xD800U && *c < 0xDC00U) surrogate = *c;
				else append_escape(out, escape, *c);
			}
		}
		else if (name == NAME_UTF8) {
			auto const result_append = +[](
				std::u16string&result,
				std::string(*escape)(uint32_t),
				uint32_t codepoint)
			{
				char8_t buf[4]{ 0, 0, 0, 0 };
				int len = u32u8(codepoint, buf);
				for (int i = 0; i < len; ++i)
					append_escape(result, escape, buf[i]);
			};
			char16_t surrogate = 0;
			for (char16_t const*c = csubbegin; c < csubend; ++c) {
				if (surrogate != 0) {
					if (*c >= 0xDC00U && *c < 0xE000U) {
						result_append(out, escape, u16sur(surrogate, *c));
						surrogate = 0;
						continue;
					}
					else {
						result_append(out, escape, surrogate);
						surrogate = 0;
					}
				}
				if (*c >= 0xD800U && *c < 0xDC00U) surrogate = *c;
				else result_append(out, escape, *c);
			}
		}
		else for (unsigned char const uc : to_charset(name, std::u16string_view(csubbegin, csubend)))
			append_escape(out, escape, uc);

		clast = csubend;
	}
	if (clast < cend) out.append(clast, cend);
}

void lovestringh::Encoder::decode(std::u8string_view s, std::u8string&out) const {
	auto const toi = +[](std::u8string_view v, int radix)->uint32_t {
		uint32_t c = 0xFFFD;
		auto const start = reinterpret_cast<char const*>(v.data());
		std::from_chars(start, start + v.size(), c, radix);
		return c;
	};

	char8_t const*last_end = s.data();
	char8_t const*const end = last_end + s.length();
	std::vector<char>bs;
	for (auto&m : ctre::search_all<
		"\\\\([0-7]{1,3})" "|"
		"\\\\x([A-Fa-f0-9]{1,8})" "|"
		"%([A-Fa-f0-9]{2})" "|"
		"\\\\u([A-Fa-f0-9]{4})" "|"
		"\\\\U([A-Fa-f0-9]{8})" "|"
		"&#(\\d{1,10});" "|"
		"&#x([A-Fa-f0-9]{1,8});">(last_end, end))
	{
		if (char8_t const*const begin = m.begin(); last_end < begin) {
			decode_piece(bs, out);
			out.append(last_end, begin);
		}

		static_assert(sizeof(long) == sizeof(uint32_t));
		static_assert(sizeof(long long) == sizeof(uint64_t));
		uint32_t c;
		if (m.get<1>()) c = toi(m.get<1>().to_view(), 8);
		else if (m.get<2>()) c = toi(m.get<2>().to_view(), 16);
		else if (m.get<3>()) c = toi(m.get<3>().to_view(), 16);
		else {
			decode_piece(bs, out);

			uint32_t const c_whole =
				m.get<4>() ? toi(m.get<4>().to_view(), 16) :
				m.get<5>() ? toi(m.get<5>().to_view(), 16) :
				m.get<6>() ? toi(m.get<6>().to_view(), 10) :
				toi(m.get<7>().to_view(), 16);

			if (c_whole < 0x110000) {
				size_t const len = out.length();
				out.resize(len + 4, '\0');
				out.resize(len + u32u8(c_whole, out.data() + len));
			}
			else out.append(u8"\uFFFD");
			last_end = m.end();
			continue;
		}

		if (name == NAME_UTF32) {
			bs.push_back(c);
			bs.push_back(c >> 8);
			bs.push_back(c >> 16);
			bs.push_back(c >> 24);
		}
		else if (name == NAME_UTF16) {
			bs.push_back(c);
			bs.push_back(c >> 8);
			if (c >= 0x10000) {
				bs.push_back(c >> 16);
				bs.push_back(c >> 24);
			}
		}
		else {
			bs.push_back(c);
			for (uint32_t c_mut = c >> 8; c_mut; c_mut >>= 8)
				bs.push_back(c_mut);
		}
		last_end = m.end();
	}
	decode_piece(bs, out);
	if (last_end < end) out.append(last_end, end);
}

void lovestringh::Encoder::decode(std::u16string_view s, std::u16string&out) const {
	auto const toi = +[](std::u16string_view v, int radix)->uint32_t {
		uint32_t c = 0xFFFD;
		std::vector<char> vc;
		vc.reserve(v.size());
		for (auto const chr : v) vc.push_back(static_cast<char>(chr));
		auto const start = vc.data();
		std::from_chars(start, start + vc.size(), c, radix);
		return c;
	};
	
	char16_t const*last_end = s.data();
	char16_t const*const end = last_end + s.length();
	std::vector<char>bs;
	for (auto&m : ctre::search_all<
		"\\\\([0-7]{1,3})" "|"
		"\\\\x([A-Fa-f0-9]{1,8})" "|"
		"%([A-Fa-f0-9]{2})" "|"
		"\\\\u([A-Fa-f0-9]{4})" "|"
		"\\\\U([A-Fa-f0-9]{8})" "|"
		"&#(\\d{1,10});" "|"
		"&#x([A-Fa-f0-9]{1,8});">(last_end, end))
	{
		if (char16_t const*const begin = m.begin(); last_end < begin) {
			decode_piece(bs, out);
			out.append(last_end, begin);
		}

		static_assert(sizeof(long) == sizeof(uint32_t));
		static_assert(sizeof(long long) == sizeof(uint64_t));
		uint32_t c;
		if (m.get<1>()) c = toi(m.get<1>().to_view(), 8);
		else if (m.get<2>()) c = toi(m.get<2>().to_view(), 16);
		else if (m.get<3>()) c = toi(m.get<3>().to_view(), 16);
		else {
			decode_piece(bs, out);

			uint32_t const c_whole =
				m.get<4>() ? toi(m.get<4>().to_view(), 16) :
				m.get<5>() ? toi(m.get<5>().to_view(), 16) :
				m.get<6>() ? toi(m.get<6>().to_view(), 10) :
				toi(m.get<7>().to_view(), 16);

			if (c_whole < 0x110000) {
				if (c_whole >= 0x10000 && c_whole < 0x110000) {
					out.append({
						static_cast<char16_t>(u16sur1(c_whole)),
						static_cast<char16_t>(u16sur2(c_whole)),
					});
				}
				else out.push_back(static_cast<char16_t>(c_whole));
			}
			else out.append(u"\uFFFD");
			last_end = m.end();
			continue;
		}

		if (name == NAME_UTF32) {
			bs.push_back(c);
			bs.push_back(c >> 8);
			bs.push_back(c >> 16);
			bs.push_back(c >> 24);
		}
		else if (name == NAME_UTF16) {
			bs.push_back(c);
			bs.push_back(c >> 8);
			if (c >= 0x10000) {
				bs.push_back(c >> 16);
				bs.push_back(c >> 24);
			}
		}
		else {
			bs.push_back(c);
			for (uint32_t c_mut = c >> 8; c_mut; c_mut >>= 8)
				bs.push_back(c_mut);
		}
		last_end = m.end();
	}
	decode_piece(bs, out);
	if (last_end < end) out.append(last_end, end);
}

void lovestringh::Encoder::decode_piece(std::vector<char>&bs, std::u8string&s_out) const {
	if (bs.empty()) return;
	if (name == NAME_UTF8) s_out.append(reinterpret_cast<char8_t*>(bs.data()), bs.size());
	else if (name == NAME_UTF32) {
		for (auto i = bs.begin(), end = bs.end() - 3; i < end; i += 4) {
			uint32_t const codepoint =
				static_cast<unsigned char>(i[3]) << 24 |
				static_cast<unsigned char>(i[2]) << 16 |
				static_cast<unsigned char>(i[1]) << 8 |
				static_cast<unsigned char>(i[0]);

			size_t const len = s_out.length();
			s_out.resize(len + 4, '\0');
			s_out.resize(len + u32u8(codepoint, s_out.data() + len));
		}
	}
	else if (name == NAME_UTF16) {
		uint32_t surrogate = 0U;
		for (auto i = bs.begin(), end = bs.end() - 1; i < end; i += 2) {
			uint32_t const codepoint =
				static_cast<unsigned char>(i[1]) << 8 |
				static_cast<unsigned char>(i[0]);

			if (surrogate != 0U) {
				if (codepoint >= 0xDC00U && codepoint < 0xE000U) {
					size_t const len = s_out.length();
					s_out.resize(len + 4, '\0');
					s_out.resize(len + u32u8(u16sur(surrogate, codepoint), s_out.data() + len));
					surrogate = 0U;
					continue;
				}
				else {
					size_t const len = s_out.length();
					s_out.resize(len + 2, '\0');
					s_out.resize(len + u32u8(surrogate, s_out.data() + len));
					surrogate = 0U;
				}
			}
			if (codepoint >= 0xD800U && codepoint < 0xDC00U) surrogate = codepoint;
			else {
				size_t const len = s_out.length();
				s_out.resize(len + 2, '\0');
				s_out.resize(len + u32u8(codepoint, s_out.data() + len));
			}
		}
	}
	else from_charset(name, bs, s_out);
	bs.clear();
}

void lovestringh::Encoder::decode_piece(std::vector<char>&bs, std::u16string&s_out) const {
	if (bs.empty()) return;
	if (name == NAME_UTF8) {
		for (auto i = static_cast<char const*>(bs.data()), end = i + bs.size(); i < end;) {
			uint32_t const codepoint = u8u32(&i, end);
			if (codepoint >= 0x10000 && codepoint < 0x110000) {
				s_out.append({
					static_cast<char16_t>(u16sur1(codepoint)),
					static_cast<char16_t>(u16sur2(codepoint)),
				});
			}
			else s_out.push_back(static_cast<char16_t>(codepoint));
		}
	}
	else if (name == NAME_UTF32) {
		for (auto i = bs.begin(), end = bs.end() - 3; i < end; i += 4) {
			uint32_t const codepoint = static_cast<unsigned char>(i[3]) << 24
				| static_cast<unsigned char>(i[2]) << 16
				| static_cast<unsigned char>(i[1]) << 8
				| static_cast<unsigned char>(i[0]);
			if (codepoint >= 0x10000 && codepoint < 0x110000) {
				s_out.append({
					static_cast<char16_t>(u16sur1(codepoint)),
					static_cast<char16_t>(u16sur2(codepoint)),
					});
			}
			else s_out.push_back(static_cast<char16_t>(codepoint));
		}
	}
	else if (name == NAME_UTF16) {
		for (auto i = bs.begin(), end = bs.end() - 1; i < end; i += 2) {
			s_out.push_back(static_cast<unsigned char>(i[0])
				| static_cast<unsigned char>(i[1]) << 8);
		}
	}
	else from_charset(name, bs, s_out);
	bs.clear();
}