#ifndef TRANSLIT_HPP
#define TRANSLIT_HPP

#include<boost/regex.hpp>

#include<map>
#include<memory>
#include<string_view>
#include<vector>
#include<utility>

namespace lovestringh {
	struct Transliterator {
		std::string const name;
		std::string translit(std::string_view s) const;
	private:
		struct RegexItem {
			boost::regex const re;
			boost::regex const re_behind;
			int const len_behind;
			bool const polar_behind;
			std::unique_ptr<std::string>(*const me)(
				RegexItem const*,
				boost::cmatch const*);

			RegexItem(
				char const*pat,
				std::unique_ptr<std::string>(*replace)(
					RegexItem const*,
					boost::cmatch const*),
				char const*pat_behind = "",
				int len_behind = 0,
				bool polar_behind = true)
				:re(pat, opt_re),
				re_behind(pat_behind, opt_nosubs),
				len_behind(len_behind),
				polar_behind(polar_behind),
				me(replace) {}

			RegexItem(
				char const*pat,
				char const*replace,
				char const*pat_behind = "",
				int len_behind = 0,
				bool polar_behind = true)
				:re(pat, opt_re),
				re_behind(pat_behind, opt_nosubs),
				len_behind(len_behind),
				polar_behind(polar_behind),
				me(mreplace_s),
				s_replace(replace) {}

			RegexItem(
				char const*pat,
				std::map<std::string_view, std::string_view>&&replace,
				char const*pat_behind = "",
				int len_behind = 0,
				bool polar_behind = true)
				:re(pat, opt_nosubs),
				re_behind(pat_behind, opt_nosubs),
				len_behind(len_behind),
				polar_behind(polar_behind),
				me(mreplace_d),
				d_replace(replace) {}
		private:
			static constexpr auto opt_re = (
				boost::regex_constants::optimize |
				boost::regex_constants::no_except);
			static constexpr auto opt_nosubs =
				opt_re | boost::regex_constants::nosubs;

			std::string const s_replace;
			std::map<std::string_view, std::string_view>const d_replace;

			static std::unique_ptr<std::string> mreplace_s(
				RegexItem const*r,
				boost::cmatch const*m);

			static std::unique_ptr<std::string> mreplace_d(
				RegexItem const*r,
				boost::cmatch const*m);
		};

		static constexpr auto opt_continuous =
			boost::regex_constants::match_continuous;

		std::vector<RegexItem>const items;

		Transliterator(char const*name, std::vector<RegexItem>&&items)
			:name(name), items(std::move(items)) {}
		Transliterator(Transliterator const&) = delete;
		Transliterator(Transliterator&&) = delete;

		static Transliterator const*make_arabic();
		static Transliterator const*make_cyrillic();
		static Transliterator const*make_greek();
		static Transliterator const*make_hangul();
		static Transliterator const*make_latin();

	public:
		static inline Transliterator const*all[] = {
			make_latin(),
			make_greek(),
			make_cyrillic(),
			make_arabic(),
			make_hangul(),
		};
	};
}

#endif