using Godot;

public static class Debug
{
    public static void Log(string message)
    {
        GD.Print("[Game] " + message);
    }

    public static void LogError(string message)
    {
        GD.PrintErr("[Game] " + message);
    }
}
