
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GodotUI
{
    public class UIHelper
    {
        // https://www.desmos.com/calculator/j69axyulw7
        // https://www.desmos.com/calculator/mkuwt9fpaf
        public static float SoftLimit(float x)
        {
            x = MathF.Max(x, 0);
            return x / (x + 1);
        }

        public static void Split(string text, char separator, out string firstPart, out string secondPart)
        {
            char[] delimiter = { separator };
            string[] split = text.Split(delimiter);

            firstPart = split[0];
            secondPart = split[1];
        }

        public static string Split_0(string text, char separator = ':')
        {
            return text.Split(separator)[0];
        }

        public static string Split_1(string text, char separator = ':')
        {
            string[] split = text.Split(separator);
            return split.Length > 1 ? split[1] : "";
        }
        public static string GetColorPrefix_Good()
        {
            return "[color=#88ff88]";
        }
        public static string GetColorPrefix_Bad()
        {
            return "[color=#ff8888]";
        }
        public static string GetColorPrefix_Neutral()
        {
            return "[color=#ffffff]";
        }

        public static string GetColorSufix()
        {
            return "[/color]";
        }

        public static string ResToString(Data.Res res, int iconSize = 24, int precision = 1, bool alwaysShowSign = false, bool redNegativeValues = false)
        {
            return ResToString(res.Def, res.Value, iconSize, precision, alwaysShowSign, redNegativeValues);
        }

        public static string ResToString(Def.Res resDef, int value, int iconSize = 24, int precision = 1, bool alwaysShowSign = false, bool redNegativeValues = false)
        {
            string prefix = "";
            string sufix = GetIcon(resDef.Image, iconSize);
            if (redNegativeValues && value < 0)
            {
                prefix += GetColorPrefix_Bad();
                sufix += GetColorSufix();
            }
            if (alwaysShowSign && value > 0)
            {
                prefix += "+";
            }

            if (Mathf.Abs(value) >= 10 * precision)
            {
                return prefix + (value / precision).ToString() + sufix;
            }
            else
            {
                return prefix + (value / precision).ToString() + ((value * 10 / precision) % 10 != 0 ? "." + (Mathf.Abs((value * 10 / precision) % 10)).ToString() : "") + sufix;
            }
        }

        public static string ResValueToString(int value, int precision = 10, bool alwaysShowSign = false, bool redNegativeValues = false)
        {
            string prefix = "";
            string sufix = "";
            if (redNegativeValues && value < 0)
            {
                prefix += GetColorPrefix_Bad();
                sufix += GetColorSufix();
            }
            if (alwaysShowSign && value > 0)
            {
                prefix += "+";
            }

            if (Mathf.Abs(value) >= 10 * precision)
            {
                return prefix + (value / precision).ToString() + sufix;
            }
            else
            {
                return prefix + (value / precision).ToString() + ((value * 10 / precision) % 10 != 0 ? "." + (Mathf.Abs((value * 10 / precision) % 10)).ToString() : "") + sufix;
            }
        }

        public static string GetIcon(string name, int size = 24)
        {
            return "[img=" + size.ToString() + "x" + size.ToString() + "]Assets/Icons/" + name + ".png[/img]";
        }

        public static string GetOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

        static Dictionary<string, int> RomanNumbersMap = new Dictionary<string, int>
    {
        {"M", 1000 },
        {"CM", 900},
        {"D", 500},
        {"CD", 400},
        {"C", 100},
        {"XC", 90},
        {"L", 50},
        {"XL", 40},
        {"X", 10},
        {"IX", 9},
        {"V", 5},
        {"IV", 4},
        {"I", 1}
    };

        public static string IntToRoman(int num)
        {
            var result = string.Empty;

            foreach (var pair in RomanNumbersMap)
            {
                result += string.Join(string.Empty, Enumerable.Repeat(pair.Key, num / pair.Value));
                num %= pair.Value;
            }
            return result;
        }

        public static int TurnsToComplete(int progressToGo, int production)
        {
            if (production <= 0) return 999;
            return (progressToGo + production - 1) / production;
        }

        public static T[] GDGetAllNodes<T>(Node n, bool recursive = false) where T : Node
        {
            List<T> nodes = new();
            for (int i = 0; i < n.GetChildCount(); i++)
            {
                if (n.GetChild(i) is T)
                {
                    nodes.Add((T)n.GetChild(i));
                }
                if (recursive)
                {
                    T[] childResult = GDGetAllNodes<T>(n.GetChild(i), recursive);
                    nodes.AddRange(childResult);
                }
            }

            return nodes.ToArray();
        }
    }
}