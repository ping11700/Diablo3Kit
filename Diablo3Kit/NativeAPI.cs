namespace Diablo3Kit;

internal class NativeAPI
{


    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetCursorPos(out MousePoint lpMousePoint);



    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    internal static extern void mouse_event(uint dwFlags, int dx, int dy, int cButtons, IntPtr dwExtraInfo);



    [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, IntPtr lParam);
}




[StructLayout(LayoutKind.Sequential)]
internal struct MousePoint(int x, int y)
{
    internal int X = x;
    internal int Y = y;
}



internal enum VirtualKeyCode
{
    //
    // 摘要:
    //     Left mouse button
    LBUTTON = 1,
    //
    // 摘要:
    //     Right mouse button
    RBUTTON = 2,
    //
    // 摘要:
    //     Control-break processing
    CANCEL = 3,
    //
    // 摘要:
    //     Middle mouse button (three-button mouse) - NOT contiguous with LBUTTON and RBUTTON
    MBUTTON = 4,
    //
    // 摘要:
    //     Windows 2000/XP: X1 mouse button - NOT contiguous with LBUTTON and RBUTTON
    XBUTTON1 = 5,
    //
    // 摘要:
    //     Windows 2000/XP: X2 mouse button - NOT contiguous with LBUTTON and RBUTTON
    XBUTTON2 = 6,
    //
    // 摘要:
    //     BACKSPACE key
    BACK = 8,
    //
    // 摘要:
    //     TAB key
    TAB = 9,
    //
    // 摘要:
    //     CLEAR key
    CLEAR = 12,
    //
    // 摘要:
    //     ENTER key
    RETURN = 13,
    //
    // 摘要:
    //     SHIFT key
    SHIFT = 16,
    //
    // 摘要:
    //     CTRL key
    CONTROL = 17,
    //
    // 摘要:
    //     ALT key
    MENU = 18,
    //
    // 摘要:
    //     PAUSE key
    PAUSE = 19,
    //
    // 摘要:
    //     CAPS LOCK key
    CAPITAL = 20,
    //
    // 摘要:
    //     Input Method Editor (IME) Kana mode
    KANA = 21,
    //
    // 摘要:
    //     IME Hanguel mode (maintained for compatibility; use HANGUL)
    HANGEUL = 21,
    //
    // 摘要:
    //     IME Hangul mode
    HANGUL = 21,
    //
    // 摘要:
    //     IME Junja mode
    JUNJA = 23,
    //
    // 摘要:
    //     IME final mode
    FINAL = 24,
    //
    // 摘要:
    //     IME Hanja mode
    HANJA = 25,
    //
    // 摘要:
    //     IME Kanji mode
    KANJI = 25,
    //
    // 摘要:
    //     ESC key
    ESCAPE = 27,
    //
    // 摘要:
    //     IME convert
    CONVERT = 28,
    //
    // 摘要:
    //     IME nonconvert
    NONCONVERT = 29,
    //
    // 摘要:
    //     IME accept
    ACCEPT = 30,
    //
    // 摘要:
    //     IME mode change request
    MODECHANGE = 31,
    //
    // 摘要:
    //     SPACEBAR
    SPACE = 32,
    //
    // 摘要:
    //     PAGE UP key
    PRIOR = 33,
    //
    // 摘要:
    //     PAGE DOWN key
    NEXT = 34,
    //
    // 摘要:
    //     END key
    END = 35,
    //
    // 摘要:
    //     HOME key
    HOME = 36,
    //
    // 摘要:
    //     LEFT ARROW key
    LEFT = 37,
    //
    // 摘要:
    //     UP ARROW key
    UP = 38,
    //
    // 摘要:
    //     RIGHT ARROW key
    RIGHT = 39,
    //
    // 摘要:
    //     DOWN ARROW key
    DOWN = 40,
    //
    // 摘要:
    //     SELECT key
    SELECT = 41,
    //
    // 摘要:
    //     PRINT key
    PRINT = 42,
    //
    // 摘要:
    //     EXECUTE key
    EXECUTE = 43,
    //
    // 摘要:
    //     PRINT SCREEN key
    SNAPSHOT = 44,
    //
    // 摘要:
    //     INS key
    INSERT = 45,
    //
    // 摘要:
    //     DEL key
    DELETE = 46,
    //
    // 摘要:
    //     HELP key
    HELP = 47,
    //
    // 摘要:
    //     0 key
    VK_0 = 48,
    //
    // 摘要:
    //     1 key
    VK_1 = 49,
    //
    // 摘要:
    //     2 key
    VK_2 = 50,
    //
    // 摘要:
    //     3 key
    VK_3 = 51,
    //
    // 摘要:
    //     4 key
    VK_4 = 52,
    //
    // 摘要:
    //     5 key
    VK_5 = 53,
    //
    // 摘要:
    //     6 key
    VK_6 = 54,
    //
    // 摘要:
    //     7 key
    VK_7 = 55,
    //
    // 摘要:
    //     8 key
    VK_8 = 56,
    //
    // 摘要:
    //     9 key
    VK_9 = 57,
    //
    // 摘要:
    //     A key
    VK_A = 65,
    //
    // 摘要:
    //     B key
    VK_B = 66,
    //
    // 摘要:
    //     C key
    VK_C = 67,
    //
    // 摘要:
    //     D key
    VK_D = 68,
    //
    // 摘要:
    //     E key
    VK_E = 69,
    //
    // 摘要:
    //     F key
    VK_F = 70,
    //
    // 摘要:
    //     G key
    VK_G = 71,
    //
    // 摘要:
    //     H key
    VK_H = 72,
    //
    // 摘要:
    //     I key
    VK_I = 73,
    //
    // 摘要:
    //     J key
    VK_J = 74,
    //
    // 摘要:
    //     K key
    VK_K = 75,
    //
    // 摘要:
    //     L key
    VK_L = 76,
    //
    // 摘要:
    //     M key
    VK_M = 77,
    //
    // 摘要:
    //     N key
    VK_N = 78,
    //
    // 摘要:
    //     O key
    VK_O = 79,
    //
    // 摘要:
    //     P key
    VK_P = 80,
    //
    // 摘要:
    //     Q key
    VK_Q = 81,
    //
    // 摘要:
    //     R key
    VK_R = 82,
    //
    // 摘要:
    //     S key
    VK_S = 83,
    //
    // 摘要:
    //     T key
    VK_T = 84,
    //
    // 摘要:
    //     U key
    VK_U = 85,
    //
    // 摘要:
    //     V key
    VK_V = 86,
    //
    // 摘要:
    //     W key
    VK_W = 87,
    //
    // 摘要:
    //     X key
    VK_X = 88,
    //
    // 摘要:
    //     Y key
    VK_Y = 89,
    //
    // 摘要:
    //     Z key
    VK_Z = 90,
    //
    // 摘要:
    //     Left Windows key (Microsoft Natural keyboard)
    LWIN = 91,
    //
    // 摘要:
    //     Right Windows key (Natural keyboard)
    RWIN = 92,
    //
    // 摘要:
    //     Applications key (Natural keyboard)
    APPS = 93,
    //
    // 摘要:
    //     Computer Sleep key
    SLEEP = 95,
    //
    // 摘要:
    //     Numeric keypad 0 key
    NUMPAD0 = 96,
    //
    // 摘要:
    //     Numeric keypad 1 key
    NUMPAD1 = 97,
    //
    // 摘要:
    //     Numeric keypad 2 key
    NUMPAD2 = 98,
    //
    // 摘要:
    //     Numeric keypad 3 key
    NUMPAD3 = 99,
    //
    // 摘要:
    //     Numeric keypad 4 key
    NUMPAD4 = 100,
    //
    // 摘要:
    //     Numeric keypad 5 key
    NUMPAD5 = 101,
    //
    // 摘要:
    //     Numeric keypad 6 key
    NUMPAD6 = 102,
    //
    // 摘要:
    //     Numeric keypad 7 key
    NUMPAD7 = 103,
    //
    // 摘要:
    //     Numeric keypad 8 key
    NUMPAD8 = 104,
    //
    // 摘要:
    //     Numeric keypad 9 key
    NUMPAD9 = 105,
    //
    // 摘要:
    //     Multiply key
    MULTIPLY = 106,
    //
    // 摘要:
    //     Add key
    ADD = 107,
    //
    // 摘要:
    //     Separator key
    SEPARATOR = 108,
    //
    // 摘要:
    //     Subtract key
    SUBTRACT = 109,
    //
    // 摘要:
    //     Decimal key
    DECIMAL = 110,
    //
    // 摘要:
    //     Divide key
    DIVIDE = 111,
    //
    // 摘要:
    //     F1 key
    F1 = 112,
    //
    // 摘要:
    //     F2 key
    F2 = 113,
    //
    // 摘要:
    //     F3 key
    F3 = 114,
    //
    // 摘要:
    //     F4 key
    F4 = 115,
    //
    // 摘要:
    //     F5 key
    F5 = 116,
    //
    // 摘要:
    //     F6 key
    F6 = 117,
    //
    // 摘要:
    //     F7 key
    F7 = 118,
    //
    // 摘要:
    //     F8 key
    F8 = 119,
    //
    // 摘要:
    //     F9 key
    F9 = 120,
    //
    // 摘要:
    //     F10 key
    F10 = 121,
    //
    // 摘要:
    //     F11 key
    F11 = 122,
    //
    // 摘要:
    //     F12 key
    F12 = 123,
    //
    // 摘要:
    //     F13 key
    F13 = 124,
    //
    // 摘要:
    //     F14 key
    F14 = 125,
    //
    // 摘要:
    //     F15 key
    F15 = 126,
    //
    // 摘要:
    //     F16 key
    F16 = 127,
    //
    // 摘要:
    //     F17 key
    F17 = 128,
    //
    // 摘要:
    //     F18 key
    F18 = 129,
    //
    // 摘要:
    //     F19 key
    F19 = 130,
    //
    // 摘要:
    //     F20 key
    F20 = 131,
    //
    // 摘要:
    //     F21 key
    F21 = 132,
    //
    // 摘要:
    //     F22 key
    F22 = 133,
    //
    // 摘要:
    //     F23 key
    F23 = 134,
    //
    // 摘要:
    //     F24 key
    F24 = 135,
    //
    // 摘要:
    //     NUM LOCK key
    NUMLOCK = 144,
    //
    // 摘要:
    //     SCROLL LOCK key
    SCROLL = 145,
    //
    // 摘要:
    //     Left SHIFT key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
    LSHIFT = 160,
    //
    // 摘要:
    //     Right SHIFT key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
    RSHIFT = 161,
    //
    // 摘要:
    //     Left CONTROL key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
    LCONTROL = 162,
    //
    // 摘要:
    //     Right CONTROL key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
    RCONTROL = 163,
    //
    // 摘要:
    //     Left MENU key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
    LMENU = 164,
    //
    // 摘要:
    //     Right MENU key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
    RMENU = 165,
    //
    // 摘要:
    //     Windows 2000/XP: Browser Back key
    BROWSER_BACK = 166,
    //
    // 摘要:
    //     Windows 2000/XP: Browser Forward key
    BROWSER_FORWARD = 167,
    //
    // 摘要:
    //     Windows 2000/XP: Browser Refresh key
    BROWSER_REFRESH = 168,
    //
    // 摘要:
    //     Windows 2000/XP: Browser Stop key
    BROWSER_STOP = 169,
    //
    // 摘要:
    //     Windows 2000/XP: Browser Search key
    BROWSER_SEARCH = 170,
    //
    // 摘要:
    //     Windows 2000/XP: Browser Favorites key
    BROWSER_FAVORITES = 171,
    //
    // 摘要:
    //     Windows 2000/XP: Browser Start and Home key
    BROWSER_HOME = 172,
    //
    // 摘要:
    //     Windows 2000/XP: Volume Mute key
    VOLUME_MUTE = 173,
    //
    // 摘要:
    //     Windows 2000/XP: Volume Down key
    VOLUME_DOWN = 174,
    //
    // 摘要:
    //     Windows 2000/XP: Volume Up key
    VOLUME_UP = 175,
    //
    // 摘要:
    //     Windows 2000/XP: Next Track key
    MEDIA_NEXT_TRACK = 176,
    //
    // 摘要:
    //     Windows 2000/XP: Previous Track key
    MEDIA_PREV_TRACK = 177,
    //
    // 摘要:
    //     Windows 2000/XP: Stop Media key
    MEDIA_STOP = 178,
    //
    // 摘要:
    //     Windows 2000/XP: Play/Pause Media key
    MEDIA_PLAY_PAUSE = 179,
    //
    // 摘要:
    //     Windows 2000/XP: Start Mail key
    LAUNCH_MAIL = 180,
    //
    // 摘要:
    //     Windows 2000/XP: Select Media key
    LAUNCH_MEDIA_SELECT = 181,
    //
    // 摘要:
    //     Windows 2000/XP: Start Application 1 key
    LAUNCH_APP1 = 182,
    //
    // 摘要:
    //     Windows 2000/XP: Start Application 2 key
    LAUNCH_APP2 = 183,
    //
    // 摘要:
    //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
    //     For the US standard keyboard, the ';:' key
    OEM_1 = 186,
    //
    // 摘要:
    //     Windows 2000/XP: For any country/region, the '+' key
    OEM_PLUS = 187,
    //
    // 摘要:
    //     Windows 2000/XP: For any country/region, the ',' key
    OEM_COMMA = 188,
    //
    // 摘要:
    //     Windows 2000/XP: For any country/region, the '-' key
    OEM_MINUS = 189,
    //
    // 摘要:
    //     Windows 2000/XP: For any country/region, the '.' key
    OEM_PERIOD = 190,
    //
    // 摘要:
    //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
    //     For the US standard keyboard, the '/?' key
    OEM_2 = 191,
    //
    // 摘要:
    //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
    //     For the US standard keyboard, the '`~' key
    OEM_3 = 192,
    //
    // 摘要:
    //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
    //     For the US standard keyboard, the '[{' key
    OEM_4 = 219,
    //
    // 摘要:
    //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
    //     For the US standard keyboard, the '\|' key
    OEM_5 = 220,
    //
    // 摘要:
    //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
    //     For the US standard keyboard, the ']}' key
    OEM_6 = 221,
    //
    // 摘要:
    //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
    //     For the US standard keyboard, the 'single-quote/double-quote' key
    OEM_7 = 222,
    //
    // 摘要:
    //     Used for miscellaneous characters; it can vary by keyboard.
    OEM_8 = 223,
    //
    // 摘要:
    //     Windows 2000/XP: Either the angle bracket key or the backslash key on the RT
    //     102-key keyboard
    OEM_102 = 226,
    //
    // 摘要:
    //     Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key
    PROCESSKEY = 229,
    //
    // 摘要:
    //     Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes.
    //     The PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard
    //     input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN,
    //     and WM_KEYUP
    PACKET = 231,
    //
    // 摘要:
    //     Attn key
    ATTN = 246,
    //
    // 摘要:
    //     CrSel key
    CRSEL = 247,
    //
    // 摘要:
    //     ExSel key
    EXSEL = 248,
    //
    // 摘要:
    //     Erase EOF key
    EREOF = 249,
    //
    // 摘要:
    //     Play key
    PLAY = 250,
    //
    // 摘要:
    //     Zoom key
    ZOOM = 251,
    //
    // 摘要:
    //     Reserved
    NONAME = 252,
    //
    // 摘要:
    //     PA1 key
    PA1 = 253,
    //
    // 摘要:
    //     Clear key
    OEM_CLEAR = 254
}