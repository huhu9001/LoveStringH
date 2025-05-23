#ifndef ENCODER_HPP
#define ENCODER_HPP

#include<stddef.h>
#include<format>
#include<span>
#include<string>
#include<string_view>
#include<type_traits>
#include<vector>

namespace lovestringh {
	template<typename TChar> struct Encoder {
		struct EscapeStyle {
			std::basic_string_view<TChar> const name;
			std::basic_string<TChar>(*const escape)(uint32_t);

			static constexpr TChar const*estyle_x_name() {
				if constexpr (std::is_same_v<TChar, char>) return "\\x";
				else if constexpr (std::is_same_v<TChar, wchar_t>) return L"\\x";
			}

			static constexpr TChar const*estyle_per_name() {
				if constexpr (std::is_same_v<TChar, char>) return "%";
				else if constexpr (std::is_same_v<TChar, wchar_t>) return L"%";
			}

			static constexpr TChar const*estyle_u_name() {
				if constexpr (std::is_same_v<TChar, char>) return "\\u";
				else if constexpr (std::is_same_v<TChar, wchar_t>) return L"\\u";
			}

			static constexpr TChar const*estyle_html_name() {
				if constexpr (std::is_same_v<TChar, char>) return "&#x";
				else if constexpr (std::is_same_v<TChar, wchar_t>) return L"&#x";
			}
			
			static std::basic_string<TChar> estyle_x(uint32_t u) {
				if constexpr (std::is_same_v<TChar, char>)
					return std::format("\\x{:X}", u);
				else if constexpr (std::is_same_v<TChar, wchar_t>)
					return std::format(L"\\x{:X}", u);
			}

			static std::basic_string<TChar> estyle_per(uint32_t u) {
				if constexpr (std::is_same_v<TChar, char>)
					return std::format("%{:02X}", u);
				else if constexpr (std::is_same_v<TChar, wchar_t>)
					return std::format(L"%{:02X}", u);
			}

			static std::basic_string<TChar> estyle_u(uint32_t u) {
				if constexpr (std::is_same_v<TChar, char>)
					return u < 0x80 ? std::format("\\x{:X}", u) :
						u < 0x10000 ? std::format("\\u{:04X}", u) :
						std::format("\\U{:08X}", u);
				else if constexpr (std::is_same_v<TChar, wchar_t>)
					return u < 0x80 ? std::format(L"\\x{:X}", u) :
						u < 0x10000 ? std::format(L"\\u{:04X}", u) :
						std::format(L"\\U{:08X}", u);
			}

			static std::basic_string<TChar> estyle_html(uint32_t u) {
				if constexpr (std::is_same_v<TChar, char>)
					return std::format("&#x{:X};", u);
				else if constexpr (std::is_same_v<TChar, wchar_t>)
					return std::format(L"&#x{:X};", u);
			}
		};

		inline static constexpr EscapeStyle styles_u32[] = {
			{ .name = EscapeStyle::estyle_u_name(), .escape = EscapeStyle::estyle_u },
			{ .name = EscapeStyle::estyle_html_name(), .escape = EscapeStyle::estyle_html },
		};
		inline static constexpr EscapeStyle styles_default[] = {
			{ .name = EscapeStyle::estyle_x_name(), .escape = EscapeStyle::estyle_x },
			{ .name = EscapeStyle::estyle_per_name(), .escape = EscapeStyle::estyle_per },
		};

		static constexpr TChar const*NAME_UTF32() {
			if constexpr (std::is_same_v<TChar, char>) return "UTF-32";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"UTF-32";
		}
		static constexpr TChar const*NAME_UTF8() {
			if constexpr (std::is_same_v<TChar, char>) return "UTF-8";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"UTF-8";
		}
		static constexpr TChar const*NAME_UTF16() {
			if constexpr (std::is_same_v<TChar, char>) return "UTF-16";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"UTF-16";
		}
		static constexpr TChar const*NAME_GB18030() {
			if constexpr (std::is_same_v<TChar, char>) return "GB18030";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"GB18030";
		}
		static constexpr TChar const*NAME_GB2312() {
			if constexpr (std::is_same_v<TChar, char>) return "GB2312";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"GB2312";
		}
		static constexpr TChar const*NAME_Shift_JIS() {
			if constexpr (std::is_same_v<TChar, char>) return "Shift-JIS";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"Shift-JIS";
		}
		static constexpr TChar const*NAME_Big5() {
			if constexpr (std::is_same_v<TChar, char>) return "Big5";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"Big5";
		}
		static constexpr TChar const*NAME_EUC_JP() {
			if constexpr (std::is_same_v<TChar, char>) return "EUC-JP";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"EUC-JP";
		}
		static constexpr TChar const*NAME_EUC_KR() {
			if constexpr (std::is_same_v<TChar, char>) return "EUC-KR";
			else if constexpr (std::is_same_v<TChar, wchar_t>) return L"EUC-KR";
		}

		std::basic_string_view<TChar> const name;
		std::span<EscapeStyle const>const styles;

		constexpr Encoder(std::basic_string_view<TChar> name, std::span<EscapeStyle const> styles)
			:name(name), styles(styles) {}
		Encoder(Encoder const&) = delete;
		Encoder(Encoder&&) = delete;

		bool has_charset() const;

		void encode(
			std::u8string_view s,
			std::u8string_view exclude,
			std::basic_string<TChar>(*escape)(uint32_t),
			std::u8string&out) const;
		void encode(
			std::u16string_view s,
			std::u16string_view exclude,
			std::basic_string<TChar>(*escape)(uint32_t),
			std::u16string&out) const;

		void decode(std::u8string_view s, std::u8string&out) const;
		void decode(std::u16string_view s, std::u16string&out) const;

	private:
		void decode_piece(std::vector<char>&bs, std::u8string&s_out) const;
		void decode_piece(std::vector<char>&bs, std::u16string&s_out) const;

		static std::vector<char>to_charset(std::basic_string_view<TChar> name, std::u8string_view s);
		static std::vector<char>to_charset(std::basic_string_view<TChar> name, std::u16string_view s);

		static void from_charset(std::basic_string_view<TChar> name, std::span<char>bs, std::u8string&s_out);
		static void from_charset(std::basic_string_view<TChar> name, std::span<char>bs, std::u16string&s_out);
	};
}

#endif