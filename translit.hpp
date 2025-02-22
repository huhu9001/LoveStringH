#ifndef TRANSLIT_HPP
#define TRANSLIT_HPP

#include"ctrex.hpp"

#include<map>
#include<memory>
#include<span>
#include<string_view>

namespace lovestringh {
	struct Transliterator {
		std::string_view const name;

		Transliterator(std::string_view name, std::span<std::unique_ptr<Regexoid<char>const>const>items)
			:name(name), items(items) {}
		Transliterator(Transliterator const&) = delete;
		Transliterator(Transliterator&&) = delete;

		std::string translit(std::string_view s) const {
			std::string result;

			size_t const len_s = s.length();
			size_t i0 = 0;
			bool empty_match = false;

			while (i0 < len_s) {
				for (auto&item : items) {
					size_t const len = item->read_write(s, i0, empty_match, result);
					if (len != static_cast<size_t>(-1)) {
						empty_match = len == 0;
						i0 += len;
						goto LB_NEXT_POS;
					}
				}
				result.push_back(s[i0]);
				++i0;
			LB_NEXT_POS:;
			}

			return result;
		}
	private:
		std::span<std::unique_ptr<Regexoid<char>const>const>const items;
	};
}

#endif