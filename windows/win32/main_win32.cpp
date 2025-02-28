#include"../encoder_ex.hpp"
#include"../translit_ex.hpp"

#include<Windows.h>
#include<CommCtrl.h>

#include<algorithm>

static wchar_t const*const ERR_TOO_LONG = L"<Input string is too long.>";

static HMODULE hinstance;
static HWND hwnd_main;

static constexpr size_t ID_TAB_MAIN = 100;
static HWND htab_main;
static size_t sel_tab_main;
static HWND hwnd_tabs_main[2];
static constexpr size_t ID_B_FONT = 101;
static HWND hb_font;

static HFONT hfont;
static LOGFONTW logfont{
	.lfFaceName = L"Arial",
};

static constexpr size_t ID_EDIT_CHAR = 200;
static HWND hedit_char;
static constexpr size_t ID_EDIT_BYTE = 201;
static HWND hedit_byte;
static constexpr size_t ID_EDIT_ENCODEONLY = 202;
static HWND hedit_encodeonly;
static constexpr size_t ID_B_REENCODE = 203;
static HWND hb_reencode;
static HWND ht_encodeonly;
static constexpr size_t ID_CBOX_ENCODING = 204;
static HWND hcbox_encoding;
static size_t sel_encoding;
static constexpr size_t NUM_ENCODERS =
	sizeof(lovestringh::all_encoders) / sizeof(*lovestringh::all_encoders);
static constexpr size_t ID_CBOX_ESCAPE_FIRST = 205;
static HWND hcbox_escape[NUM_ENCODERS];
static_assert(ID_CBOX_ESCAPE_FIRST + NUM_ENCODERS <= 300);

static constexpr size_t ID_EDIT_ROMAN = 300;
static HWND hedit_roman;
static constexpr size_t ID_EDIT_NONROMAN = 301;
static HWND hedit_nonroman;
static constexpr size_t ID_CBOX_SCRIPT = 302;
static HWND hcbox_script;
static size_t sel_script;

static constexpr int HK_F2 = 500;
static constexpr int HK_F3 = 501;
static constexpr int HK_CTRL_A = 502;
static constexpr int HK_ALT_Z = 503;
static constexpr int HK_ALT_X = 504;
static constexpr int HK_ALT_L = 505;
static constexpr int HK_ALT_G = 506;
static constexpr int HK_ALT_R = 507;
static constexpr int HK_ALT_A = 508;
static constexpr int HK_ALT_K = 509;

static wchar_t buff_in[0x10000];
static wchar_t buff_in2[0x80];

static void encode() {
	if (GetWindowTextLengthW(hedit_char) < 0x10000 - 1) {
		GetWindowTextW(hedit_char, buff_in, 0x10000);
		std::u16string_view const in_v(reinterpret_cast<char16_t const*>(buff_in));

		if (GetWindowTextLengthW(hedit_encodeonly) < 0x80 - 1)
			GetWindowTextW(hedit_encodeonly, buff_in2, 0x80);
		else buff_in2[0] = 0;
		std::u16string_view const ne_v(reinterpret_cast<char16_t const*>(buff_in2));

		auto&e = lovestringh::all_encoders[sel_encoding];
		auto const es =
			e.styles[SendMessageW(hcbox_escape[sel_encoding], CB_GETCURSEL, 0, 0)].escape;
		auto const out = e.encode(in_v, ne_v, es);
		SetWindowTextW(hedit_byte, reinterpret_cast<LPCWSTR>(out.c_str()));
	}
	else SetWindowTextW(hedit_byte, reinterpret_cast<LPCWSTR>(ERR_TOO_LONG));
}

static void decode() {
	if (GetWindowTextLengthW(hedit_byte) < 0x10000 - 1) {
		GetWindowTextW(hedit_byte, buff_in, 0x10000);
		std::u16string_view const in_v(reinterpret_cast<char16_t const*>(buff_in));
		auto const out = lovestringh::all_encoders[sel_encoding].decode(in_v);
		SetWindowTextW(hedit_char, reinterpret_cast<LPCWSTR>(out.c_str()));
	}
	else SetWindowTextW(hedit_char, reinterpret_cast<LPCWSTR>(ERR_TOO_LONG));
}

static void change_tab(size_t sel) {
	if (sel != sel_tab_main) {
		ShowWindow(hwnd_tabs_main[sel_tab_main], SW_HIDE);
		ShowWindow(hwnd_tabs_main[sel], SW_SHOW);
		sel_tab_main = sel;
	}
}

static void change_script(size_t sel) {
	if (sel_script != sel) {
		SendMessageW(hcbox_script, CB_SETCURSEL, sel, 0);
		PostMessageW(
			hwnd_tabs_main[1],
			WM_COMMAND,
			MAKEWPARAM(ID_CBOX_SCRIPT, CBN_SELCHANGE),
			reinterpret_cast<LPARAM>(hcbox_script));
	}
}

static LRESULT CALLBACK wndproc_encoding(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
	switch (uMsg) {
	default: return DefWindowProcW(hWnd, uMsg, wParam, lParam);
	case WM_CREATE: {
		hedit_char = CreateWindowW(
			L"EDIT",
			nullptr,
			ES_LEFT | ES_MULTILINE | ES_AUTOVSCROLL | WS_BORDER | WS_CHILD | WS_VISIBLE | WS_VSCROLL,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_EDIT_CHAR),
			hinstance,
			nullptr);
		SendMessage(hedit_char, WM_SETFONT, reinterpret_cast<WPARAM>(hfont), true);
		hedit_byte = CreateWindowW(
			L"EDIT",
			nullptr,
			ES_LEFT | ES_MULTILINE | ES_AUTOVSCROLL | WS_BORDER | WS_CHILD | WS_VISIBLE | WS_VSCROLL,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_EDIT_BYTE),
			hinstance,
			nullptr);
		hedit_encodeonly = CreateWindowW(
			L"EDIT",
			L" \\r\\n",
			ES_LEFT | WS_BORDER | WS_CHILD | WS_VISIBLE,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_EDIT_ENCODEONLY),
			hinstance,
			nullptr);

		hcbox_encoding = CreateWindowW(
			L"COMBOBOX",
			nullptr,
			CBS_DROPDOWNLIST | CBS_HASSTRINGS | WS_CHILD | WS_OVERLAPPED | WS_VISIBLE,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_CBOX_ENCODING),
			hinstance,
			nullptr);

		for (size_t i = 0; i < NUM_ENCODERS; ++i) {
			auto const&e = lovestringh::all_encoders[i];
			if (e.has_charset()) {
				size_t const len = e.name.length();
				char const*start = e.name.data();
				char const*end = start + len;
				std::copy(start, end, buff_in);
				buff_in[len] = 0;
				SendMessageW(
					hcbox_encoding,
					CB_ADDSTRING,
					0,
					reinterpret_cast<LPARAM>(buff_in));

				hcbox_escape[i] = CreateWindowW(
					L"COMBOBOX",
					nullptr,
					CBS_DROPDOWNLIST | CBS_HASSTRINGS | WS_CHILD | WS_OVERLAPPED,
					0, 0,
					0, 0,
					hWnd,
					reinterpret_cast<HMENU>(ID_CBOX_ESCAPE_FIRST + i),
					hinstance,
					nullptr);
				for (auto const&ec : e.styles) {
					size_t const len = ec.name.length();
					char const*start = ec.name.data();
					char const*end = start + len;
					std::copy(start, end, buff_in);
					buff_in[len] = 0;
					SendMessageW(
						hcbox_escape[i],
						CB_ADDSTRING,
						0,
						reinterpret_cast<LPARAM>(buff_in));
				}
				SendMessageW(hcbox_escape[i], CB_SETCURSEL, 0, 0);
			}
			else hcbox_escape[i] = nullptr;
		}
		SendMessageW(hcbox_encoding, CB_SETCURSEL, 0, 0);
		sel_encoding = 0;
		ShowWindow(hcbox_escape[0], SW_SHOW);

		hb_reencode = CreateWindowW(
			L"BUTTON",
			L"Re-encode",
			BS_AUTOCHECKBOX | WS_CHILD | WS_VISIBLE,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_B_REENCODE),
			hinstance,
			nullptr);
		SendMessageW(hb_reencode, BM_SETCHECK, BST_CHECKED, 0);

		ht_encodeonly = CreateWindowW(
			L"STATIC",
			L"Do not encode:",
			SS_LEFT | WS_CHILD | WS_VISIBLE,
			0, 0,
			0, 0,
			hWnd,
			nullptr,
			hinstance,
			nullptr);
		SendMessageW(hb_reencode, BM_SETCHECK, BST_CHECKED, 0);
	} return 0;
	case WM_SIZE: {
		int y = 8;
		int h;

		h = (HIWORD(lParam) >> 1) - 40;
		MoveWindow(hedit_char, 7, y, LOWORD(lParam) - 14, h, true);
		y += h + 8;

		h = 20;
		MoveWindow(hcbox_encoding, 7, y, 100, h * 10, true);
		for (auto hcbox : hcbox_escape)
			MoveWindow(hcbox, 114, y, 50, h * 10, true);
		MoveWindow(hb_reencode, 171, y, 100, h, true);
		y += h + 8;

		h = 20;
		MoveWindow(ht_encodeonly, 7, y, 100, h, true);
		MoveWindow(hedit_encodeonly, 114, y, LOWORD(lParam) - 121, h, true);
		y += h + 8;

		h = (HIWORD(lParam) >> 1) - 40;
		MoveWindow(hedit_byte, 7, y, LOWORD(lParam) - 14, h, true);
	} return 0;
	case WM_COMMAND:
		switch (wParam) {
		default:
			for (size_t i = 0; i < NUM_ENCODERS; ++i) {
				if (wParam == MAKEWPARAM(ID_CBOX_ESCAPE_FIRST + i, CBN_SELCHANGE))
					encode();
				return 0;
			}
			return DefWindowProcW(hWnd, uMsg, wParam, lParam);
		case MAKEWPARAM(ID_EDIT_CHAR, EN_UPDATE):
			encode();
			SendMessageW(hb_reencode, BM_SETCHECK, BST_CHECKED, 0);
			return 0;
		case MAKEWPARAM(ID_EDIT_BYTE, EN_UPDATE):
			decode();
			SendMessageW(hb_reencode, BM_SETCHECK, BST_UNCHECKED, 0);
			return 0;
		case MAKEWPARAM(ID_EDIT_ENCODEONLY, EN_UPDATE):
			encode();
			return 0;
		case MAKEWPARAM(ID_CBOX_ENCODING, CBN_SELCHANGE): {
			size_t const sel = SendMessageW(hcbox_encoding, CB_GETCURSEL, 0, 0);
			size_t const sel_real =
				sel - std::count_if(hcbox_escape, hcbox_escape + sel, +[](HWND h) {
					return h == 0;
				});
			if (sel_real != sel_encoding) {
				ShowWindow(hcbox_escape[sel_encoding], SW_HIDE);
				ShowWindow(hcbox_escape[sel_real], SW_SHOW);
				sel_encoding = sel_real;
			}
			if (SendMessageW(hb_reencode, BM_GETCHECK, 0, 0)) encode();
			else decode();
		} return 0;
		}
	}
}

static LRESULT CALLBACK wndproc_translit(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
	switch (uMsg) {
	default: return DefWindowProcW(hWnd, uMsg, wParam, lParam);
	case WM_CREATE:
		hedit_roman = CreateWindowW(
			L"EDIT",
			nullptr,
			ES_LEFT | ES_MULTILINE | ES_AUTOVSCROLL | WS_BORDER | WS_CHILD | WS_VISIBLE | WS_VSCROLL,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_EDIT_ROMAN),
			hinstance,
			nullptr);
		hedit_nonroman = CreateWindowW(
			L"EDIT",
			nullptr,
			ES_LEFT | ES_READONLY | ES_MULTILINE | ES_AUTOVSCROLL | WS_BORDER | WS_CHILD | WS_VISIBLE | WS_VSCROLL,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_EDIT_NONROMAN),
			hinstance,
			nullptr);
		SendMessage(hedit_nonroman, WM_SETFONT, reinterpret_cast<WPARAM>(hfont), true);

		hcbox_script = CreateWindowW(
			L"COMBOBOX",
			nullptr,
			CBS_DROPDOWNLIST | CBS_HASSTRINGS | WS_CHILD | WS_OVERLAPPED | WS_VISIBLE,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_CBOX_SCRIPT),
			hinstance,
			nullptr);
		for (auto&t : lovestringh::all_translits) {
			size_t const len = t.name.length();
			char const*start = t.name.data();
			char const*end = start + len;
			std::copy(start, end, buff_in);
			buff_in[len] = 0;
			SendMessageW(
				hcbox_script,
				CB_ADDSTRING,
				0,
				reinterpret_cast<LPARAM>(buff_in));
		}
		SendMessageW(hcbox_script, CB_SETCURSEL, 0, 0);
		sel_script = 0;

		return 0;
	case WM_SIZE: {
		int y = 8;
		int h;

		h = (HIWORD(lParam) >> 1) - 30;
		MoveWindow(hedit_roman, 7, y, LOWORD(lParam) - 14, h, true);
		y += h + 8;

		h = (HIWORD(lParam) >> 1) - 30;
		MoveWindow(hedit_nonroman, 7, y, LOWORD(lParam) - 14, h, true);
		y += h + 8;

		h = 20;
		MoveWindow(hcbox_script, 7, y, 150, h * 10, true);
	} return 0;
	case WM_COMMAND:
		switch (wParam) {
		default: return DefWindowProcW(hWnd, uMsg, wParam, lParam);
		case MAKEWPARAM(ID_CBOX_SCRIPT, CBN_SELCHANGE):
			sel_script = SendMessageW(hcbox_script, CB_GETCURSEL, 0, 0);
			[[fallthrough]];
		case MAKEWPARAM(ID_EDIT_ROMAN, EN_UPDATE):
			if (GetWindowTextLengthW(hedit_roman) < 0x10000 - 1) {
				GetWindowTextW(hedit_roman, buff_in, 0x10000);
				int const len = WideCharToMultiByte(
					CP_UTF8, 0, buff_in, -1, nullptr, 0, nullptr, nullptr);
				std::string in;
				in.resize(len);
				WideCharToMultiByte(
					CP_UTF8, 0, buff_in, -1, in.data(), len, nullptr, nullptr);
				auto const out = lovestringh::all_translits[sel_script].translit(in);
				int const len2 = MultiByteToWideChar(CP_UTF8, 0, out.c_str(), -1, nullptr, 0);
				if (len2 <= 0x10000) {
					MultiByteToWideChar(CP_UTF8, 0, out.c_str(), -1, buff_in, len2);
					SetWindowTextW(hedit_nonroman, buff_in);
				}
				else SetWindowTextW(hedit_nonroman, ERR_TOO_LONG);
			}
			else SetWindowTextW(hedit_nonroman, ERR_TOO_LONG);
			return 0;
		}
	}
}

static LRESULT CALLBACK wndproc_main(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
	switch (uMsg) {
	default: return DefWindowProcW(hWnd, uMsg, wParam, lParam);
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	case WM_CREATE:
		{
			WNDCLASSW const wclass_encoding{
				.lpfnWndProc = wndproc_encoding,
				.hInstance = hinstance,
				.hCursor = LoadCursorW(nullptr, reinterpret_cast<LPCWSTR>(IDC_ARROW)),
				.hbrBackground = reinterpret_cast<HBRUSH>(COLOR_3DFACE + 1),
				.lpszClassName = L"TABP_ENCODING",
			};
			auto const wca_encoding = RegisterClassW(&wclass_encoding);
			hwnd_tabs_main[0] = CreateWindowW(
				reinterpret_cast<LPCWSTR>(wca_encoding),
				nullptr,
				WS_CHILD | WS_VISIBLE,
				0, 0,
				0, 0,
				hWnd,
				nullptr,
				hinstance,
				nullptr);
		}
		{
			WNDCLASSW const wclass_translit{
				.lpfnWndProc = wndproc_translit,
				.hInstance = hinstance,
				.hCursor = LoadCursorW(nullptr, reinterpret_cast<LPCWSTR>(IDC_ARROW)),
				.hbrBackground = reinterpret_cast<HBRUSH>(COLOR_3DFACE + 1),
				.lpszClassName = L"TABP_TRANSLIT",
			};
			auto const wca_translit = RegisterClassW(&wclass_translit);
			hwnd_tabs_main[1] = CreateWindowW(
				reinterpret_cast<LPCWSTR>(wca_translit),
				nullptr,
				WS_CHILD,
				0, 0,
				0, 0,
				hWnd,
				nullptr,
				hinstance,
				nullptr);
		}

		htab_main = CreateWindowW(
			WC_TABCONTROLW,
			nullptr,
			TCS_TABS | WS_CHILD | WS_VISIBLE,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_TAB_MAIN),
			hinstance,
			nullptr);
		{
			wchar_t tname1[] = L"Encoding";
			TCITEMW const titem1{
				.mask = TCIF_TEXT,
				.pszText = tname1,
			};
			SendMessageW(htab_main, TCM_INSERTITEMW, 0, reinterpret_cast<LPARAM>(&titem1));
			sel_tab_main = 0;
		}
		{
			wchar_t tname2[] = L"Translit";
			TCITEMW const titem2{
				.mask = TCIF_TEXT,
				.pszText = tname2,
			};
			SendMessageW(htab_main, TCM_INSERTITEMW, 1, reinterpret_cast<LPARAM>(&titem2));
		}

		hb_font = CreateWindowW(
			L"BUTTON",
			L"T",
			BS_PUSHBUTTON | WS_CHILD | WS_VISIBLE,
			0, 0,
			0, 0,
			hWnd,
			reinterpret_cast<HMENU>(ID_B_FONT),
			hinstance,
			nullptr);

		return 0;
	case WM_SIZE:
		MoveWindow(htab_main, 0, 0, LOWORD(lParam), HIWORD(lParam), true);
		{
			RECT r{
				.left = 0,
				.top = 0,
				.right = LOWORD(lParam),
				.bottom = HIWORD(lParam),
			};
			SendMessageW(htab_main, TCM_ADJUSTRECT, false, reinterpret_cast<LPARAM>(&r));
			MoveWindow(hwnd_tabs_main[0], r.left, r.top, r.right - r.left, r.bottom - r.top, true);
			MoveWindow(hwnd_tabs_main[1], r.left, r.top, r.right - r.left, r.bottom - r.top, true);
		}
		MoveWindow(hb_font, LOWORD(lParam) - 22, 2, 20, 20, true);
		return 0;
	case WM_COMMAND:
		switch (wParam) {
		default: return DefWindowProcW(hWnd, uMsg, wParam, lParam);
		case MAKEWPARAM(ID_B_FONT, BN_CLICKED): {
			CHOOSEFONTW cf{
				.lStructSize = sizeof(CHOOSEFONTW),
				.hwndOwner = hWnd,
				.lpLogFont = &logfont,
				.Flags = CF_INITTOLOGFONTSTRUCT,
			};
			if (ChooseFontW(&cf)) {
				DeleteObject(hfont);
				hfont = CreateFontIndirectW(&logfont);
				SendMessage(hedit_char, WM_SETFONT, reinterpret_cast<WPARAM>(hfont), true);
				SendMessage(hedit_nonroman, WM_SETFONT, reinterpret_cast<WPARAM>(hfont), true);
			}
		} return 0;
		case MAKEWPARAM(HK_F2, 1): {
			HWND const hedit = GetFocus();
			if (hedit == hedit_char) {
				SendMessageW(hedit_byte, EM_SETSEL, 0, -1);
				SendMessageW(hedit_byte, WM_COPY, 0, 0);
				SendMessageW(hedit_char, EM_SETSEL, 0, -1);
			}
			else if (hedit == hedit_byte) {
				SendMessageW(hedit_char, EM_SETSEL, 0, -1);
				SendMessageW(hedit_char, WM_COPY, 0, 0);
				SendMessageW(hedit_byte, EM_SETSEL, 0, -1);
			}
			else if (hedit == hedit_roman) {
				SendMessageW(hedit_nonroman, EM_SETSEL, 0, -1);
				SendMessageW(hedit_nonroman, WM_COPY, 0, 0);
				SendMessageW(hedit_roman, EM_SETSEL, 0, -1);
			}
		} return 0;
		case MAKEWPARAM(HK_F3, 1): {
			HWND const hedit = GetFocus();
			if (hedit == hedit_char) SetFocus(hedit_byte);
			else if (hedit == hedit_byte) SetFocus(hedit_char);
			else if (hedit == hedit_roman) SetFocus(hedit_nonroman);
			else if (hedit == hedit_nonroman) SetFocus(hedit_roman);
		} return 0;
		case MAKEWPARAM(HK_CTRL_A, 1): {
			HWND const hedit = GetFocus();
			if (hedit == hedit_char
				|| hedit == hedit_byte
				|| hedit == hedit_roman) SendMessageW(hedit, EM_SETSEL, 0, -1);
		} return 0;
		case MAKEWPARAM(HK_ALT_Z, 1):
			SendMessageW(htab_main, TCM_SETCURSEL, 0, 0);
			change_tab(0);
			SetFocus(hedit_char);
			return 0;
		case MAKEWPARAM(HK_ALT_X, 1):
			SendMessageW(htab_main, TCM_SETCURSEL, 0, 0);
			change_tab(0);
			SetFocus(hedit_byte);
			return 0;
		case MAKEWPARAM(HK_ALT_L, 1):
			change_script(0);
			SendMessageW(hcbox_script, CB_SETCURSEL, 0, 0);
			change_tab(1);
			SetFocus(hedit_roman);
			return 0;
		case MAKEWPARAM(HK_ALT_G, 1):
			change_script(1);
			SendMessageW(htab_main, TCM_SETCURSEL, 1, 0);
			change_tab(1);
			SetFocus(hedit_roman);
			return 0;
		case MAKEWPARAM(HK_ALT_R, 1):
			change_script(2);
			SendMessageW(htab_main, TCM_SETCURSEL, 1, 0);
			change_tab(1);
			SetFocus(hedit_roman);
			return 0;
		case MAKEWPARAM(HK_ALT_A, 1):
			change_script(3);
			SendMessageW(htab_main, TCM_SETCURSEL, 1, 0);
			change_tab(1);
			SetFocus(hedit_roman);
			return 0;
		case MAKEWPARAM(HK_ALT_K, 1):
			change_script(4);
			SendMessageW(htab_main, TCM_SETCURSEL, 1, 0);
			change_tab(1);
			SetFocus(hedit_roman);
			return 0;
		}
	case WM_NOTIFY: {
		NMHDR const&nm = *reinterpret_cast<NMHDR const*>(lParam);
		if (nm.hwndFrom == htab_main && nm.code == TCN_SELCHANGE) {
			change_tab(SendMessageW(htab_main, TCM_GETCURSEL, 0, 0));
			return 0;
		}
		else return DefWindowProcW(hWnd, uMsg, wParam, lParam);
	}
	}
}

int main(int, char**) {
	static constexpr INITCOMMONCONTROLSEX coninit{
		.dwSize = sizeof(INITCOMMONCONTROLSEX),
		.dwICC = ICC_STANDARD_CLASSES | ICC_TAB_CLASSES,
	};
	InitCommonControlsEx(&coninit);

	hinstance = GetModuleHandleW(nullptr);
	hfont = CreateFontIndirectW(&logfont);

	static ACCEL accels[] = {
		{
			.fVirt = FVIRTKEY,
			.key = VK_F2,
			.cmd = HK_F2,
		},
		{
			.fVirt = FVIRTKEY,
			.key = VK_F3,
			.cmd = HK_F3,
		},
		{
			.fVirt = FVIRTKEY | FCONTROL,
			.key = 'A',
			.cmd = HK_CTRL_A,
		},
		{
			.fVirt = FVIRTKEY | FALT,
			.key = 'Z',
			.cmd = HK_ALT_Z,
		},
		{
			.fVirt = FVIRTKEY | FALT,
			.key = 'X',
			.cmd = HK_ALT_X,
		},
		{
			.fVirt = FVIRTKEY | FALT,
			.key = 'L',
			.cmd = HK_ALT_L,
		},
		{
			.fVirt = FVIRTKEY | FALT,
			.key = 'G',
			.cmd = HK_ALT_G,
		},
		{
			.fVirt = FVIRTKEY | FALT,
			.key = 'R',
			.cmd = HK_ALT_R,
		},
		{
			.fVirt = FVIRTKEY | FALT,
			.key = 'A',
			.cmd = HK_ALT_A,
		},
		{
			.fVirt = FVIRTKEY | FALT,
			.key = 'K',
			.cmd = HK_ALT_K,
		},
	};
	static HACCEL haccel = CreateAcceleratorTableW(accels, sizeof(accels) / sizeof(*accels));

	static WNDCLASSW const wclass_main{
		.lpfnWndProc = wndproc_main,
		.hInstance = hinstance,
		.hCursor = LoadCursorW(nullptr, reinterpret_cast<LPCWSTR>(IDC_ARROW)),
		.hbrBackground = reinterpret_cast<HBRUSH>(COLOR_3DFACE + 1),
		.lpszClassName = L"FORM_MAIN",
	};
	static auto const wca_main = RegisterClassW(&wclass_main);
	hwnd_main = CreateWindowW(
		reinterpret_cast<LPCWSTR>(wca_main),
		L"Love String H",
		WS_OVERLAPPEDWINDOW | WS_VISIBLE,
		100, 100,
		400, 300,
		nullptr,
		nullptr,
		hinstance,
		nullptr);

	for (MSG msg; GetMessageW(&msg, nullptr, 0, 0);) {
		TranslateAcceleratorW(hwnd_main, haccel, &msg) || TranslateMessage(&msg);
		DispatchMessageW(&msg);
	}

	DestroyAcceleratorTable(haccel);
	DeleteObject(hfont);
}
