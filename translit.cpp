#include"translit.hpp"

#include"helper_func.hpp"
#include<ctre.hpp>

std::unique_ptr<std::string> lovestringh::Transliterator::RegexItem::mreplace_s(
	RegexItem const*r,
	boost::cmatch const*m)
{
	auto result = std::make_unique<std::string>();
	char const*i_last = r->s_replace.data();
	char const*const i_end = i_last + r->s_replace.length();
	for (auto const&mg : ctre::split<"\\$(\\d+)">(i_last, i_end)) {
		auto i = mg.data();
		if (i_last < i) result->append(i_last, i);
		result->append(to_view((*m)[mg.get<1>().to_number()]));
		i_last = mg.end();
	}
	if (i_last < i_end) result->append(i_last, i_end);
	return result;
}

std::unique_ptr<std::string> lovestringh::Transliterator::RegexItem::mreplace_d(
	RegexItem const*r,
	boost::cmatch const*m)
{
	auto s = to_view((*m)[0]);
	if (auto i = r->d_replace.find(s); i != r->d_replace.end())
		return std::make_unique<std::string>(i->second);
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