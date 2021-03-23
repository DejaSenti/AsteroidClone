using System;
using UnityEngine;

public static class UIHelpers
{
    public static string KeycodeToChar(KeyCode keyCode)
    {
        string result;

        int keyInt = (int)keyCode;
        if (keyInt >= 97 && keyInt <= 122)
        {
            result = (keyInt - 32).ToString();
        }
        else if (keyInt >= 48 && keyInt <= 57)
        {
            result = keyInt.ToString();
        }
        else if (keyInt >= 256 && keyInt <= 265)
        {
            result = "Num" + (keyInt - 208);
        }
        else if (keyInt >= 282 && keyInt <= 293)
        {
            result = "F" + (keyInt - 281);
        }
        else
        {
            switch (keyCode)
            {
                case KeyCode.AltGr:
                    result = "Alt Gr";
                    break;
                case KeyCode.BackQuote:
                    result = "`";
                    break;
                case KeyCode.Backslash:
                    result = @"\";
                    break;
                case KeyCode.Backspace:
                    result = "Backspace";
                    break;
                case KeyCode.Break:
                    result = "Break";
                    break;
                case KeyCode.CapsLock:
                    result = "Caps Lock";
                    break;
                case KeyCode.Comma:
                    result = ",";
                    break;
                case KeyCode.Delete:
                    result = "Del";
                    break;
                case KeyCode.DownArrow:
                    result = "Down";
                    break;
                case KeyCode.End:
                    result = "End";
                    break;
                case KeyCode.Equals:
                    result = "=";
                    break;
                case KeyCode.Escape:
                    result = "Esc";
                    break;
                case KeyCode.Home:
                    result = "Home";
                    break;
                case KeyCode.Insert:
                    result = "Insert";
                    break;
                case KeyCode.KeypadDivide:
                    result = "Num /";
                    break;
                case KeyCode.KeypadEnter:
                    result = "Num Enter";
                    break;
                case KeyCode.KeypadMinus:
                    result = "Num -";
                    break;
                case KeyCode.KeypadMultiply:
                    result = "Num *";
                    break;
                case KeyCode.KeypadPeriod:
                    result = "Num .";
                    break;
                case KeyCode.KeypadPlus:
                    result = "Num +";
                    break;
                case KeyCode.LeftAlt:
                    result = "L Alt";
                    break;
                case KeyCode.LeftArrow:
                    result = "Left";
                    break;
                case KeyCode.LeftBracket:
                    result = "[";
                    break;
                case KeyCode.LeftControl:
                    result = "L Ctrl";
                    break;
                case KeyCode.LeftShift:
                    result = "L Shift";
                    break;
                case KeyCode.LeftWindows:
                    result = "L Win";
                    break;
                case KeyCode.Minus:
                    result = "-";
                    break;
                case KeyCode.Mouse0:
                    result = "L Mouse";
                    break;
                case KeyCode.Mouse1:
                    result = "R Mouse";
                    break;
                case KeyCode.Mouse2:
                    result = "M Mouse";
                    break;
                case KeyCode.Numlock:
                    result = "Num Lock";
                    break;
                case KeyCode.PageDown:
                    result = "Page Down";
                    break;
                case KeyCode.PageUp:
                    result = "Page Up";
                    break;
                case KeyCode.Pause:
                    result = "Pause";
                    break;
                case KeyCode.Period:
                    result = ".";
                    break;
                case KeyCode.Print:
                    result = "Prnt Scrn";
                    break;
                case KeyCode.Quote:
                    result = @"'";
                    break;
                case KeyCode.Return:
                    result = "Enter";
                    break;
                case KeyCode.RightAlt:
                    result = "R Alt";
                    break;
                case KeyCode.RightArrow:
                    result = "Right";
                    break;
                case KeyCode.RightBracket:
                    result = "]";
                    break;
                case KeyCode.RightControl:
                    result = "R Ctrl";
                    break;
                case KeyCode.RightShift:
                    result = "R Shift";
                    break;
                case KeyCode.RightWindows:
                    result = "L Win";
                    break;
                case KeyCode.ScrollLock:
                    result = "Scrl Lock";
                    break;
                case KeyCode.Semicolon:
                    result = ";";
                    break;
                case KeyCode.Slash:
                    result = "/";
                    break;
                case KeyCode.Space:
                    result = "Space";
                    break;
                case KeyCode.Tab:
                    result = "Tab";
                    break;
                case KeyCode.Tilde:
                    result = "~";
                    break;
                case KeyCode.UpArrow:
                    result = "Up";
                    break;
                default:
                    result = null;
                    break;
            }
        }

        return result;
    }

    public static string ResolutionTupleToText(Tuple<int, int> tuple)
    {
        var result = tuple.Item1.ToString() + "X" + tuple.Item2.ToString();
        return result;
    }
}