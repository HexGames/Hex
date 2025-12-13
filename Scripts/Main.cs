using Godot;
using System;

public partial class Main : Node
{
    // --- GODOT ---
    [Export]
    public AssetLib Assets;
    [Export]
    public ColorLib Colors;

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

        Hex.Def.LibInit.Init();
    }

    public override void _Process(double delta)
    {
        if (FirstFrame)
        {
            // do things on the first frame
            Hex.GameLoop.Init();

            FirstFrame = false;
        }

        Hex.GameLoop.Update(delta);

        ProcessDelayedCalls(delta);
    }

    //public static void DelayedAction(Action<string> action, float delay, string context)
    //{
    //    SceneTreeTimer timer = x.Tree.CreateTimer(delay);
    //    timer.Timeout += () =>
    //    {
    //        action.Invoke(context);
    //    };
    //}
}
