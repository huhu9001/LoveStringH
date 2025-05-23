#include"../encoder.hpp"

#include"../constdict.hpp"
#include<Windows.h>

template<typename TChar>
constexpr static ConstDict<std::basic_string_view<TChar>, UINT>::Dict dict_codepage({
	{ lovestringh::Encoder<TChar>::NAME_Big5(), 950 },
	{ lovestringh::Encoder<TChar>::NAME_EUC_JP(), 20932 },
	{ lovestringh::Encoder<TChar>::NAME_EUC_KR(), 51949 },
	{ lovestringh::Encoder<TChar>::NAME_GB18030(), 54936 },
	{ lovestringh::Encoder<TChar>::NAME_GB2312(), 936 },
	{ lovestringh::Encoder<TChar>::NAME_Shift_JIS(), 932 },
});

bool lovestringh::Encoder<wchar_t>::has_charset() const {
	if (auto const cpn = dict_codepage<wchar_t>[name]; cpn != nullptr) {
		return IsValidCodePage(*cpn);
	}
	else return true;
}

template<typename TChar> std::vector<char> lovestringh::Encoder<TChar>::to_charset(
	std::basic_string_view<TChar> name,
	std::u8string_view s)
{
	std::u16string ws;
	int const len1 = MultiByteToWideChar(
		CP_UTF8,
		0,
		reinterpret_cast<LPCCH>(s.data()),
		static_cast<int>(s.length()),
		nullptr,
		0);
	ws.resize(len1);
	MultiByteToWideChar(
		CP_UTF8,
		0,
		reinterpret_cast<LPCCH>(s.data()),
		static_cast<int>(s.length()),
		reinterpret_cast<LPWSTR>(ws.data()),
		len1);

	return to_charset(name, ws);
}

template std::vector<char> lovestringh::Encoder<char>::to_charset(
	std::string_view name,
	std::u8string_view s);
template std::vector<char> lovestringh::Encoder<wchar_t>::to_charset(
	std::wstring_view name,
	std::u8string_view s);

template<typename TChar> std::vector<char> lovestringh::Encoder<TChar>::to_charset(
	std::basic_string_view<TChar> name,
	std::u16string_view s)
{
	UINT const cp = *dict_codepage<TChar>[name];
	int const len2 = WideCharToMultiByte(
		cp,
		0,
		reinterpret_cast<LPCWCH>(s.data()),
		static_cast<int>(s.length()),
		nullptr, 
		0,
		nullptr,
		nullptr);
	std::vector<char> result;
	result.resize(len2);
	WideCharToMultiByte(
		cp,
		0,
		reinterpret_cast<LPCWCH>(s.data()),
		static_cast<int>(s.length()),
		result.data(),
		len2,
		nullptr,
		nullptr);

	return result;
}

template std::vector<char> lovestringh::Encoder<char>::to_charset(
	std::string_view name,
	std::u16string_view s);
template std::vector<char> lovestringh::Encoder<wchar_t>::to_charset(
	std::wstring_view name,
	std::u16string_view s);

template<typename TChar> void lovestringh::Encoder<TChar>::from_charset(
	std::basic_string_view<TChar> name,
	std::span<char> bs,
	std::u8string&s_out)
{
	std::u16string ws;
	from_charset(name, bs, ws);

	size_t const len1 = s_out.size();
	int const len2 = WideCharToMultiByte(
		CP_UTF8,
		0,
		reinterpret_cast<LPCWCH>(ws.data()),
		static_cast<int>(ws.size()),
		nullptr,
		0,
		nullptr,
		nullptr);
	s_out.resize(len1 + len2);
	WideCharToMultiByte(
		CP_UTF8,
		0,
		reinterpret_cast<LPCWCH>(ws.data()),
		static_cast<int>(ws.size()),
		reinterpret_cast<LPSTR>(s_out.data() + len1),
		len2,
		nullptr,
		nullptr);
}

template void lovestringh::Encoder<char>::from_charset(
	std::string_view name,
	std::span<char> bs,
	std::u8string&s_out);
template void lovestringh::Encoder<wchar_t>::from_charset(
	std::wstring_view name,
	std::span<char> bs,
	std::u8string&s_out);

template<typename TChar> void lovestringh::Encoder<TChar>::from_charset(
	std::basic_string_view<TChar> name,
	std::span<char> bs,
	std::u16string&s_out)
{
	UINT const cp = *dict_codepage<TChar>[name];
	size_t const len1 = s_out.size();
	int const len2 = MultiByteToWideChar(
		cp,
		0,
		bs.data(),
		static_cast<int>(bs.size()),
		nullptr,
		0);
	s_out.resize(len1 + len2);
	MultiByteToWideChar(
		cp,
		0,
		bs.data(),
		static_cast<int>(bs.size()),
		reinterpret_cast<LPWSTR>(s_out.data() + len1),
		len2);
}

template void lovestringh::Encoder<char>::from_charset(
	std::string_view name,
	std::span<char> bs,
	std::u16string&s_out);
template void lovestringh::Encoder<wchar_t>::from_charset(
	std::wstring_view name,
	std::span<char> bs,
	std::u16string&s_out);