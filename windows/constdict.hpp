#ifndef CONSTDICT_HPP
#define CONSTDICT_HPP

#include<algorithm>

template<typename TKey, typename TValue> struct ConstDict {
	template<size_t N> class Dict {
	public:
		constexpr Dict(std::pair<TKey const, TValue const>const(&input)[N]) {
			std::copy(input, input + N, data);
			std::sort(data, data + N);
		}

		constexpr TValue const*operator[](TKey key) const {
			auto item = std::lower_bound(data, data + N, key, cmp);
			return item != data + N && item->first == key ? &item->second : nullptr;
		}

	private:
		std::pair<TKey, TValue> data[N];

		constexpr static bool cmp(std::pair<TKey, TValue>const&item, TKey const&key) {
			return item.first < key;
		}
	};
};

#endif