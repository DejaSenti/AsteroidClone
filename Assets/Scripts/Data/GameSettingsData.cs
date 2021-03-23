using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameSettingsData
{
    public static string KEY_ROTATE_CW = "Rotate CW";
    public static string KEY_ROTATE_CCW = "Rotate CCW";
    public static string KEY_ACCELERATE = "Accelerate";
    public static string KEY_SHOOT = "Shoot";
    public static string SCREEN_LAYOUT = "Screen Layout";
    public static string SCREEN_WIDTH = "Screen Width";
    public static string SCREEN_HEIGHT = "Screen Height";
    public static string DIFFICULTY = "Difficulty";

    public static string LAYOUT_FULLSCREEN = "Fullscreen";
    public static string LAYOUT_BORDERLESS = "Borderless";
    public static string LAYOUT_MAX_WINDOW = "Maximized Window";
    public static string LAYOUT_WINDOW = "Window";

    public static Dictionary<FullScreenMode, string> LayoutNameByType = new Dictionary<FullScreenMode, string>
    {
        [FullScreenMode.ExclusiveFullScreen] = LAYOUT_FULLSCREEN,
        [FullScreenMode.FullScreenWindow] = LAYOUT_BORDERLESS,
        [FullScreenMode.MaximizedWindow] = LAYOUT_MAX_WINDOW,
        [FullScreenMode.Windowed] = LAYOUT_WINDOW
    };

    public static Dictionary<string, FullScreenMode> LayoutTypeByName = LayoutNameByType.ToDictionary(i => i.Value, i => i.Key);

    public static List<Tuple<int, int>> ScreenResolutions = new List<Tuple<int, int>>
    {
        new Tuple<int, int>(1280, 720),
        new Tuple<int, int>(1366, 768),
        new Tuple<int, int>(1440, 900),
        new Tuple<int, int>(1536, 864),
        new Tuple<int, int>(1920, 1080)
    };

    public static int DEFAULT_KEY_ROTATE_CW = (int)KeyCode.RightArrow;
    public static int DEFAULT_KEY_ROTATE_CCW = (int)KeyCode.LeftArrow;
    public static int DEFAULT_KEY_ACCELERATE = (int)KeyCode.Space;
    public static int DEFAULT_KEY_SHOOT = (int)KeyCode.LeftControl;
    public static FullScreenMode DEFAULT_SCREEN_LAYOUT = FullScreenMode.FullScreenWindow;
    public static Tuple<int, int> DEFAULT_SCREEN_RESOLUTION = ScreenResolutions[ScreenResolutions.Count - 1];
    public static int DEFAULT_DIFFICULTY = 1;
}