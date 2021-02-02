using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameSettings
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
    public static string LAYOUT_WINDOW = "Window";

    public static int DEFAULT_KEY_ROTATE_CW = (int)KeyCode.RightArrow;
    public static int DEFAULT_KEY_ROTATE_CCW = (int)KeyCode.LeftArrow;
    public static int DEFAULT_KEY_ACCELERATE = (int)KeyCode.Space;
    public static int DEFAULT_KEY_SHOOT = (int)KeyCode.LeftControl;
    public static int DEFAULT_SCREEN_LAYOUT = LayoutNumberByName[LAYOUT_BORDERLESS];
    public static int DEFAULT_SCREEN_WIDTH = ScreenResolutions.Last().Item1;
    public static int DEFAULT_SCREEN_HEIGHT = ScreenResolutions.Last().Item2;
    public static int DEFAULT_DIFFICULTY = 1;

    public static Dictionary<string, int> LayoutNumberByName = new Dictionary<string, int>
    { 
        [LAYOUT_FULLSCREEN] = 0, 
        [LAYOUT_BORDERLESS] = 1, 
        [LAYOUT_WINDOW] = 2
    };

    public static List<Tuple<int, int>> ScreenResolutions = new List<Tuple<int, int>>
    { 
        new Tuple<int, int>(1280, 720),
        new Tuple<int, int>(1366, 768),
        new Tuple<int, int>(1440, 900),
        new Tuple<int, int>(1536, 864),
        new Tuple<int, int>(1920, 1080),
    };
}