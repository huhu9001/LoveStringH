#ifndef ENCODER_HPP
#define ENCODER_HPP

#include<stddef.h>
#include<format>
#include<span>
#include<string>
#include<string_view>
#include<vector>

namespace lovestringh {
	struct Encoder {
		struct EscapeStyle {
			std::string const name;
			std::string(*const escape)(uint32_t);
		};

		inline static EscapeStyle const estyle_x{
			.name = "\\x",
				.escape = [](uint32_t u) {
				return std::format("\\x{:X}", u);
			},
		};
		inline static EscapeStyle const estyle_per{
			.name = "%",
			.escape = [](uint32_t u) {
				return std::format("%{:02X}", u);
			},
		};
		inline static EscapeStyle const estyle_u{
			.name = "\\u",
			.escape = [](uint32_t u) {
				return u < 0x80 ? std::format("\\x{:X}", u) :
					u < 0x10000 ? std::format("\\u{:04X}", u) :
					std::format("\\U{:08X}", u);
			},
		};
		inline static EscapeStyle const estyle_html{
			.name = "&#x",
			.escape = [](uint32_t u) {
				return std::format("&#x{:X};", u);
			},
		};

		static constexpr char const*NAME_UTF32 = "UTF-32";
		static constexpr char const*NAME_UTF8 = "UTF-8";
		static constexpr char const*NAME_UTF16 = "UTF-16";
		static constexpr char const*NAME_GB18030 = "GB18030";
		static constexpr char const*NAME_GB2312 = "GB2312";
		static constexpr char const*NAME_Shift_JIS = "Shift-JIS";
		static constexpr char const*NAME_Big5 = "Big5";
		static constexpr char const*NAME_EUC_JP = "EUC-JP";
		static constexpr char const*NAME_EUC_KR = "EUC-KR";

		std::string const name;
		bool const supported;
		std::vector<EscapeStyle const*>const styles;

		Encoder(
			char const*name,
			std::vector<EscapeStyle const*>&&styles,
			bool force_support = false)
			:name(name),
			supported(force_support || has_charset(name)),
			styles(std::move(styles)) {}

		std::u8string encode(
			std::u8string_view s,
			std::u8string_view exclude,
			std::string(*escape)(uint32_t)) const;
		std::u16string encode(
			std::u16string_view s,
			std::u16string_view exclude,
			std::string(*escape)(uint32_t)) const;

		std::u8string decode(std::u8string_view s) const;
		std::u16string decode(std::u16string_view s) const;

	private:
		void decode_piece(std::vector<char>&bs, std::u8string&s_out) const;
		void decode_piece(std::vector<char>&bs, std::u16string&s_out) const;

		static bool has_charset(std::string_view name);

		static std::vector<char>to_charset(std::string_view name, std::u8string_view s);
		static std::vector<char>to_charset(std::string_view name, std::u16string_view s);

		static void from_charset(
			std::string_view name,
			std::span<char> bs,
			std::u8string&s_out);
		static void from_charset(
			std::string_view name,
			std::span<char> bs,
			std::u16string&s_out);
	};

	inline Encoder const all_encoders[] = {
		Encoder(Encoder::NAME_UTF32, { &Encoder::estyle_u, &Encoder::estyle_html }, true),
		Encoder(Encoder::NAME_UTF8, { &Encoder::estyle_x, &Encoder::estyle_per }, true),
		Encoder(Encoder::NAME_UTF16, { &Encoder::estyle_x }, true),
		Encoder(Encoder::NAME_GB18030, { &Encoder::estyle_x, &Encoder::estyle_per }),
		Encoder(Encoder::NAME_GB2312, { &Encoder::estyle_x, &Encoder::estyle_per }),
		Encoder(Encoder::NAME_Shift_JIS, { &Encoder::estyle_x, &Encoder::estyle_per }),
		Encoder(Encoder::NAME_Big5, { &Encoder::estyle_x, &Encoder::estyle_per }),
		Encoder(Encoder::NAME_EUC_JP, { &Encoder::estyle_x, &Encoder::estyle_per }),
		Encoder(Encoder::NAME_EUC_KR, { &Encoder::estyle_x, &Encoder::estyle_per }),
	};
}

#endif