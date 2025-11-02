using Godot;
using Godot.Collections;
using System.Linq;

[Tool]
public partial class ColorLib : Node
{
    [Export]
    public Dictionary<Def.Timing, Color> TimingTextColor = new Dictionary<Def.Timing, Color>();
    [Export]
    public Dictionary<Def.Timing, Color> TimingBgColor = new Dictionary<Def.Timing, Color>();

    public static Color GetColor_Text(Def.Timing timing)
    {
        if (Main.x.Colors.TimingTextColor.TryGetValue(timing, out Color color) == true)
        {
            return color;
        }
        return new Color(0xffffffff);
    }
    public static Color GetColor_BG(Def.Timing timing)
    {
        if (Main.x.Colors.TimingBgColor.TryGetValue(timing, out Color color) == true)
        {
            return color;
        }
        return new Color(0x000000dd);
    }
}
