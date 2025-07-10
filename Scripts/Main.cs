using Godot;
using System;

public partial class Main : Node
{
    // --- GODOT ---
    [Export]
    public AssetLib Assets;
    [Export]
    public Godot3D.MapMain MapInstance;
    [Export]
    public GodotUI.UIMain UIInstance;

    // --- public ---
    public Logic.Game GameInstance = null;

    // --- private ---
    private SceneTree Tree = null;
    private bool FirstFrame = true;

    static Main _self;
    public static Main x
    {
        get
        {
            if (_self != null)
            {
                return _self;
            }
            else
            {
                _self = ((SceneTree)Engine.GetMainLoop()).Root.GetNode<Main>("/root/Main");
                return _self;
            }
        }
        set
        {
            _self = value;
        }
    }

    public override void _Ready()
    {
        Tree = GetTree();

        RNG.RNG.xInit();

        Def.Lib.xTakeDefsFromDownloads();
        Def.Lib.xLoadDefs();
        Def.Lib.xInitDefs();
    }

    public override void _Process(double delta)
    {
        if (FirstFrame)
        {
            // do things on the first frame
            FirstFrame = false;
        }
    }

    public static void DelayedAction(Action action, float delay)
    {
        SceneTreeTimer timer = x.Tree.CreateTimer(delay);
        timer.Timeout += action;
    }
}
