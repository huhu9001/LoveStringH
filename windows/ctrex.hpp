#include<ctre.hpp>

#include<map>
#include<memory>
#include<string_view>

template<typename TChar> struct Regexoid {
	virtual size_t read_write(std::basic_string_view<TChar>in, size_t pos, bool empty_before, std::basic_string<TChar>&out) const = 0;
	virtual ~Regexoid() = default;
};

template<typename TChar, CTRE_REGEX_INPUT_TYPE pattern, typename... Modifiers> struct RgxdMaker {
	typedef ctre::return_type<typename std::basic_string_view<TChar>::iterator, typename ctre::regex_builder<pattern>::type> Match;
	typedef bool(*FuncRegexoid)(Match const&, std::basic_string<TChar>&out);

private:
	template<typename RgxdType> static size_t rw_from_output(RgxdType const*thiz, std::basic_string_view<TChar>in, size_t pos, bool empty_before, std::basic_string<TChar>&out) {
		auto const m = ctre::starts_with<pattern, Modifiers...>.template exec_with_result_iterator<decltype(in.begin())>(in.begin(), in.begin() + pos, in.end());
		if (!m) return -1;
		if (empty_before && m.size() == 0) return -1;
		return thiz->output(m, out) ? m.size() : -1;
	}

	struct RegexoidFunc : Regexoid<TChar> {
		FuncRegexoid const repl;
		constexpr RegexoidFunc(FuncRegexoid repl) :repl(repl) {}

		bool output(Match const&m, std::basic_string<TChar>&out) const {
			return (*repl)(m, out);
		}

		size_t read_write(std::basic_string_view<TChar>in, size_t pos, bool empty_before, std::basic_string<TChar>&out) const override {
			return RgxdMaker::rw_from_output(this, in, pos, empty_before, out);
		}
	};

	struct RegexoidMap : Regexoid<TChar> {
		std::map<std::basic_string_view<TChar>, std::basic_string_view<TChar>>const&dict;
		RegexoidMap(std::map<std::basic_string_view<TChar>, std::basic_string_view<TChar>>const&dict) :dict(dict) {}

		bool output(Match const&m, std::basic_string<TChar>&out) const {
			std::basic_string_view<TChar>const key = m.template get<(m.count() > 1 ? 1 : 0)>().to_view();
			if (auto const entry = dict.find(key); entry != dict.end()) {
				out.append(entry->second);
				return true;
			}
			else return false;
		}

		size_t read_write(std::basic_string_view<TChar>in, size_t pos, bool empty_before, std::basic_string<TChar>&out) const override {
			return RgxdMaker::rw_from_output(this, in, pos, empty_before, out);
		}
	};

	struct RegexoidStr : Regexoid<TChar> {
		std::basic_string_view<TChar>const str;
		constexpr RegexoidStr(std::basic_string_view<TChar>str) :str(str) {}

		bool output(Match const&m, std::basic_string<TChar>&out) const {
			/*Match const&m = *std::launder(reinterpret_cast<Match const*>(mdata));

			TChar const*i_last = str.data();
			TChar const*const i_end = i_last + str.length();
			for (auto const&mg : ctre::split<"\\$(\\d+)">(i_last, i_end)) {
			auto i = mg.data();
			if (i_last < i) out.append(i_last, i);
			//out.append(to_view((*m)[mg.get<1>().to_number()]));
			i_last = mg.end();
			}
			if (i_last < i_end) out.append(i_last, i_end);*/

			out.append(str);
			return true;
		}

		size_t read_write(std::basic_string_view<TChar>in, size_t pos, bool empty_before, std::basic_string<TChar>&out) const override {
			return RgxdMaker::rw_from_output(this, in, pos, empty_before, out);
		}
	};

public:
	static std::unique_ptr<Regexoid<TChar>> make(FuncRegexoid repl) {
		return std::make_unique<RegexoidFunc>(repl);
	}

	static std::unique_ptr<Regexoid<TChar>> make(std::map<std::basic_string_view<TChar>, std::basic_string_view<TChar>>const&dict) {
		return std::make_unique<RegexoidMap>(dict);
	}

	static std::unique_ptr<Regexoid<TChar>> make(std::basic_string_view<TChar>str) {
		return std::make_unique<RegexoidStr>(str);
	}
};