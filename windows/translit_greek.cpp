#include"translit.hpp"

#include"helper_func.hpp"

static const std::map<std::string_view, int> dict_tilde{
    {"a", 0x1F06},
	{"A", 0x1F0E},
	{"h", 0x1F26},
	{"H", 0x1F2E},
	{"i", 0x1F36},
	{"I", 0x1F3E},
	{"y", 0x1F56},
	{"v", 0x1F66},
	{"V", 0x1F6E},

	{"aj", 0x1F86},
	{"AJ", 0x1F8E}, {"Aj", 0x1F8E},
	{"hj", 0x1F96},
	{"HJ", 0x1F9E}, {"Hj", 0x1F9E},
	{"vj", 0x1FA6},
	{"VJ", 0x1FAE}, {"Vj", 0x1FAE},
};

template<typename Match> bool tilde(Match const&m, std::string&out) {
	uint32_t c;
	if (auto const i = dict_tilde.find(m.template get<1>().to_view()); i != dict_tilde.cend())
		c = i->second;
	else return false;

	c += m.template get<2>().to_view() == ")" ? 0 : 1;

	auto const len = out.length();
	out.resize(len + 4);
	out.resize(len + u32u8(c, out.data() + len));
	return true;
}

static const std::map<std::string_view, int> dict_acute{
    {"a", 0x1F00},
	{"A", 0x1F08},
	{"e", 0x1F10},
	{"E", 0x1F18},
	{"h", 0x1F20},
	{"H", 0x1F28},
	{"i", 0x1F30},
	{"I", 0x1F38},
	{"o", 0x1F40},
	{"O", 0x1F48},
	{"y", 0x1F50},
	{"v", 0x1F60},
	{"V", 0x1F68},

	{"aj", 0x1F80},
	{"AJ", 0x1F88}, {"Aj", 0x1F88},
	{"hj", 0x1F90},
	{"HJ", 0x1F98}, {"Hj", 0x1F98},
	{"vj", 0x1FA0},
	{"VJ", 0x1FA8}, {"Vj", 0x1FA8},
};

template<typename Match> bool acute(Match const&m, std::string&out) {
	uint32_t c;
	if (auto const i = dict_acute.find(m.template get<1>().to_view()); i != dict_acute.cend())
		c = i->second;
	else return false;

	c += m.template get<2>().to_view() == ")" ? 0 : 1;

	c += m.template get<3>().to_view() == "\\" ? 2 :
		m.template get<3>().to_view() == "/" ? 4 : 0;

	auto const len = out.length();
	out.resize(len + 4);
	out.resize(len + u32u8(c, out.data() + len));
	return true;
}

namespace lovestringh {
	Transliterator make_greek() {
		static std::map<std::string_view, std::string_view> const dict_y{
			{"Y(", "\u1F59"},
			{"Y(\\", "\u1F5B"},
			{"Y(/", "\u1F5D"},
			{"Y(~", "\u1F5F"},
		}, dict_3{
			{ "I:/", "\u0390" }, { "i:/", "\u0390" },
			{ "Y:/", "\u03B0" }, { "y:/", "\u03B0" },
			{ "aj/", "\u1FB4" },
			{ "hj/", "\u1FC4" },
			{ "vj/", "\u1FF4" },

			{ "I:\\", "\u1FD2" }, { "i:\\", "\u1FD2" },
			{ "Y:\\", "\u1FE2" }, { "y:\\", "\u1FE2" },
			{ "aj\\", "\u1FB2" },
			{ "hj\\", "\u1FC2" },
			{ "vj\\", "\u1FF2" },

			{ "I:~", "\u1FD7" }, { "i:~", "\u1FD7" },
			{ "Y:~", "\u1FE7" }, { "y:~", "\u1FE7" },
			{ "aj~", "\u1FB7" },
			{ "hj~", "\u1FC7" },
			{ "vj~", "\u1FF7" },
		}, dict_2{
			{ "TH", "\u0398" }, { "Th", "\u0398" }, { "th", "\u03B8" },
			{ "T'", "\u03A4" }, { "t'", "\u03C4" },
			{ "PH", "\u03A6" }, { "Ph", "\u03A6" }, { "ph", "\u03C6" },
			{ "P'", "\u03A0" }, { "p'", "\u03C0" },
			{ "KH", "\u03A7" }, { "Kh", "\u03A7" }, { "kh", "\u03C7" },
			{ "K'", "\u039A" }, { "k'", "\u03BA" },
			{ "PS", "\u03A8" }, { "Ps", "\u03A8" }, { "ps", "\u03C8" },
			{ "SH", "\u03FA" }, { "Sh", "\u03FA" }, { "sh", "\u03FB" },
			{ "S'", "\u03A3" }, { "s'", "\u03C3" },

			{ "I:", "\u03AA" }, { "i:", "\u03CA" },
			{ "Y:", "\u03AB" }, { "y:", "\u03CB" },
			{ "AJ", "\u1FBC" }, { "Aj", "\u1FBC" }, { "aj", "\u1FB3" },
			{ "HJ", "\u1FCC" }, { "Hj", "\u1FCC" }, { "hj", "\u1FC3" },
			{ "VJ", "\u1FFC" }, { "Vj", "\u1FFC" }, { "vj", "\u1FF3" },

			{ "A/", "\u0386" }, { "a/", "\u03AC" },
			{ "E/", "\u0388" }, { "e/", "\u03AD" },
			{ "H/", "\u0389" }, { "h/", "\u03AE" },
			{ "I/", "\u038A" }, { "i/", "\u03AF" },
			{ "O/", "\u038C" }, { "o/", "\u03CC" },
			{ "Y/", "\u038E" }, { "y/", "\u03CD" },
			{ "V/", "\u038F" }, { "v/", "\u03CE" },
			{ "A\\", "\u1FBA" }, { "a\\", "\u1F70" },
			{ "E\\", "\u1FC8" }, { "e\\", "\u1F72" },
			{ "H\\", "\u1FCA" }, { "h\\", "\u1F74" },
			{ "I\\", "\u1FDA" }, { "i\\", "\u1F76" },
			{ "O\\", "\u1FF8" }, { "o\\", "\u1F78" },
			{ "Y\\", "\u1FEA" }, { "y\\", "\u1F7A" },
			{ "V\\", "\u1FFA" }, { "v\\", "\u1F7C" },
			{ "a~", "\u1FB6" },
			{ "h~", "\u1FC6" },
			{ "i~", "\u1FD6" },
			{ "y~", "\u1FE6" },
			{ "v~", "\u1FF6" },

			{ "R)", "\u1FE4" }, { "r)", "\u1FE4" },
			{ "R(", "\u1FEC" }, { "r(", "\u1FE5" },
		}, dict_1{
			{ "A", "\u0391" }, { "a", "\u03B1" },
			{ "B", "\u0392" }, { "b", "\u03B2" },
			{ "G", "\u0393" }, { "g", "\u03B3" },
			{ "D", "\u0394" }, { "d", "\u03B4" },
			{ "E", "\u0395" }, { "e", "\u03B5" },
			{ "Z", "\u0396" }, { "z", "\u03B6" },
			{ "H", "\u0397" }, { "h", "\u03B7" },
			{ "I", "\u0399" }, { "i", "\u03B9" },
			{ "K", "\u039A" }, { "k", "\u03BA" },
			{ "L", "\u039B" }, { "l", "\u03BB" },
			{ "M", "\u039C" }, { "m", "\u03BC" },
			{ "N", "\u039D" }, { "n", "\u03BD" },
			{ "X", "\u039E" }, { "x", "\u03BE" },
			{ "O", "\u039F" }, { "o", "\u03BF" },
			{ "P", "\u03A0" }, { "p", "\u03C0" },
			{ "R", "\u03A1" }, { "r", "\u03C1" },
			{ "S", "\u03A3" }, { "s", "\u03C3" },
			{ "T", "\u03A4" }, { "t", "\u03C4" },
			{ "Y", "\u03A5" }, { "y", "\u03C5" },
			{ "V", "\u03A9" }, { "v", "\u03C9" },
			{ "Q", "\u03D8" }, { "q", "\u03D9" },
			{ "W", "\u03DC" }, { "w", "\u03DD" },
		};
		static std::unique_ptr<Regexoid<char>const>const items[] = {
			RgxdMaker<char, "([AHIVahiyv]|[AHV][Jj]|[ahv]j)([()])~">::make(tilde),
			RgxdMaker<char, "([AEHIOVaehioyv]|[AHV][Jj]|[ahv]j)([()])([/\\\\]?)">::make(acute),
			RgxdMaker<char, "Y\\(([/\\\\~]?)">::make(dict_y),
			RgxdMaker<char, "(?<=[A-Za-z](?:|[()~/\\\\]|[()~/\\\\][()~/\\\\]))s(?=[^A-Za-z]|$)">::make("\u03C2"),
			RgxdMaker<char, "...">::make(dict_3),
			RgxdMaker<char, "..">::make(dict_2),
			RgxdMaker<char, ".">::make(dict_1),
		};
		return Transliterator("Greek (Alt+G)", items);
	}
}