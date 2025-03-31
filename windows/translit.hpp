#ifndef TRANSLIT_HPP
#define TRANSLIT_HPP

#include"ctrex.hpp"

#include<span>
#include<string_view>

namespace lovestringh {
	struct Transliterator {
		std::string_view const name;

		constexpr Transliterator(
			std::string_view name,
			std::span<Regexoid<char>const*const> items):name(name), items(items) {}
		Transliterator(Transliterator const&) = delete;
		Transliterator(Transliterator&&) = delete;

		void translit(std::string_view s, std::string&out) const {
			size_t const len_s = s.length();
			size_t i0 = 0;
			bool empty_match = false;

			while (i0 < len_s) {
				for (auto item : items) {
					size_t const len = item->read_write(s, i0, empty_match, out);
					if (len != static_cast<size_t>(-1)) {
						empty_match = len == 0;
						i0 += len;
						goto LB_NEXT_POS;
					}
				}
				out.push_back(s[i0]);
				++i0;
			LB_NEXT_POS: continue;
			}
		}
	private:
		std::span<Regexoid<char>const*const>const items;
	};
}

#endif