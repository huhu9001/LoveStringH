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
			std::string_view const name;
			std::string(*const escape)(uint32_t);
			
			static std::string estyle_x(uint32_t u) {
				return std::format("\\x{:X}", u);
			}

			static std::string estyle_per(uint32_t u) {
				return std::format("%{:02X}", u);
			}

			static std::string estyle_u(uint32_t u) {
				return u < 0x80 ? std::format("\\x{:X}", u) :
					u < 0x10000 ? std::format("\\u{:04X}", u) :
					std::format("\\U{:08X}", u);
			}

			static std::string estyle_html(uint32_t u) {
				return std::format("&#x{:X};", u);
			}
		};

		inline static constexpr EscapeStyle styles_u32[] = {
			{ .name = "\\u", .escape = EscapeStyle::estyle_u },
			{ .name = "&#x", .escape = EscapeStyle::estyle_html },
		};
		inline static constexpr EscapeStyle styles_default[] = {
			{ .name = "\\x", .escape = EscapeStyle::estyle_x },
			{ .name = "%", .escape = EscapeStyle::estyle_per },
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

		std::string_view const name;
		std::span<EscapeStyle const>const styles;

		constexpr Encoder(std::string_view name, std::span<EscapeStyle const> styles)
			:name(name), styles(styles) {}
		Encoder(Encoder const&) = delete;
		Encoder(Encoder&&) = delete;

		bool has_charset() const;

		void encode(
			std::u8string_view s,
			std::u8string_view exclude,
			std::string(*escape)(uint32_t),
			std::u8string&out) const;
		void encode(
			std::u16string_view s,
			std::u16string_view exclude,
			std::string(*escape)(uint32_t),
			std::u16string&out) const;

		void decode(std::u8string_view s, std::u8string&out) const;
		void decode(std::u16string_view s, std::u16string&out) const;

	private:
		void decode_piece(std::vector<char>&bs, std::u8string&s_out) const;
		void decode_piece(std::vector<char>&bs, std::u16string&s_out) const;

		static std::vector<char>to_charset(std::string_view name, std::u8string_view s);
		static std::vector<char>to_charset(std::string_view name, std::u16string_view s);

		static void from_charset(std::string_view name, std::span<char>bs, std::u8string&s_out);
		static void from_charset(std::string_view name, std::span<char>bs, std::u16string&s_out);
	};
}

#endif