#include"translit.hpp"

#include"helper_func.hpp"
#include<ctre.hpp>

#include<sstream>

std::unique_ptr<std::string> lovestringh::Transliterator::RegexItem::mreplace_s(
	RegexItem const*r,
	boost::cmatch const*m)
{
	std::ostringstream result;
	auto i_last = r->s_replace.data();
	auto const i_end = i_last + r->s_replace.length();
	for (auto const&mg : ctre::split<"\\$(\\d+)">(i_last, i_end)) {
		auto i = mg.data();
		if (i_last < i) result << std::string_view(i_last, i);
		result << to_view((*m)[mg.get<1>().to_number()]);
		i_last = mg.end();
	}
	if (i_last < i_end) result << std::string_view(i_last, i_end);
	return std::make_unique<std::string>(std::move(result).str());
}

std::unique_ptr<std::string> lovestringh::Transliterator::RegexItem::mreplace_d(
	RegexItem const*r,
	boost::cmatch const*m)
{
	auto s = to_view((*m)[0]);
	if (r->d_replace.contains(s))
		return std::make_unique<std::string>(std::string(r->d_replace.at(s)));
	else return nullptr;
}

std::string lovestringh::Transliterator::translit(std::string_view s) const {
	 std::string result;

	 char const*i0 = s.data();
	 char const*const istart = i0;
	 char const*const iend = i0 + s.length();
	 bool empty_match = false;

	 while (i0 < iend) {
		 std::unique_ptr<std::string> ss;
		 for (auto const&item : items) {
			 boost::cmatch m;
			 if (item.len_behind > 0) {
				 if (istart + item.len_behind > i0) {
					 if (item.polar_behind) continue;
				 }
				 else if (boost::regex_match(
					 i0 - item.len_behind,
					 i0,
					 item.re_behind) != item.polar_behind) continue;
			 }
			 if (!boost::regex_search(i0, iend, m, item.re, opt_continuous))
				 continue;

			 if (m.length() == 0) {
				 if (!empty_match) {
					 ss = item.me(&item, &m);
					 if (ss) {
						 empty_match = true;
						 result.append(*ss);
						 break;
					 }
				 }
			 }
			 else {
				 ss = item.me(&item, &m);
				 if (ss) {
					 empty_match = false;
					 i0 += m.length();
					 result.append(*ss);
					 break;
				 }
			 }
		 }
		 if (!ss) {
			 result.push_back(*i0);
			 ++i0;
		 }
	 }

	 return result;
}