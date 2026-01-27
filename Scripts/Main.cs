using Godot;
using System;

public partial class Main : Node
{
    // --- GODOT ---
    [Export]
    public AssetLib Assets;
    [Export]
    public ColorLib Colors;
    
    // Made by AI: Claude Opus 4.5 - Debug tool window reference
    [Export]
    public Hex.Tools.DebugToolWindow DebugToolWindow;

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

    // Made by AI: Claude Opus 4.5 - Handle F1 key to toggle debug tool window
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            if (keyEvent.Keycode == Key.F1)
            {
                DebugToolWindow?.Toggle();
                GetViewport().SetInputAsHandled();
            }
        }
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
