using Godot;
using System.Diagnostics;

public static class Debug
{
    public static void Log(string message)
    {
        GD.Print("[Game] " + message);
        Debugger.Log(1, "info", "[Game] " + message);
    }

    public static void LogError(string message)
    {
        GD.PrintErr("[Game] " + message);
        Debugger.Log(3, "error", "[Game] " + message);
        throw new System.Exception("[Game] " + message);
    }
}
