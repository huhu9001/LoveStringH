#ifndef TRANSLIT_EX_HPP
#define TRANSLIT_EX_HPP

#include"translit.hpp"

namespace lovestringh {
	Transliterator make_arabic();
	Transliterator make_cyrillic();
	Transliterator make_greek();
	Transliterator make_hangul();
	Transliterator make_latin();

	static inline Transliterator const all_translits[] = {
		make_latin(),
		make_greek(),
		make_cyrillic(),
		make_arabic(),
		make_hangul(),
	};
}

#endif