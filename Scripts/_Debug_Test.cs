using Godot;

[Tool]
public partial class _Debug_Test : Node
{
    [Export]
    public bool Test
    {
        get => false;
        set
        {
            if (value)
            {
                TestFunc();
            }
        }
    }

    public void TestFunc()
    {
        //var dg = GetNode<DefGenerator>("DefGenerator");
        //GD.Print("Test DefGenerator." + dg.DefLibrary.); 
        //var LocationNode = GetNode<Node>("/root/Game");
        //var tree  = GetTree();
        //var Location = tree.EditedSceneRoot.GetNode<LocationNode>("Game/Locations/Avalon");
        //var Location = (LocationNode)LocationNode;
        //LocationNode Location = GetTree().Root.GetNode("Game").GetNode<LocationNode>("Game/Locations/Avalon");
        //GD.Print("Test Location Avalon.Def - " + Location.Def.FilePath);
        //GD.Print("Test Location Avalon.Data - " + Location.Data.Population.ToString());
    }

    //public static void Search_General(DataBlock data)
    //{
    //    for (int a = 0; a < data.Subs.Count; a++)
    //    {
    //        DataBlock dataA = data.Subs[a];
    //        GD.Print(dataA.Name + dataA.ValueToString());
    //        for (int b = 0; b < dataA.Subs.Count; b++)
    //        {
    //            DataBlock dataB = dataA.Subs[b];
    //            GD.Print(dataB.Name + dataB.ValueToString());
    //        }
    //    }
    //}

    //public static void Search_1(DataBlock data)
    //{
    //    for (int a = 0; a < data.Subs.Count; a++)
    //    {
    //        DataBlock dataA = data.Subs[a];
    //        if (dataA.Name == "Fleets")
    //        {
    //            for (int b = 0; b < dataA.Subs.Count; b++)
    //            {
    //                DataBlock dataB = dataA.Subs[b];
    //                GD.Print(dataB.Name + dataB.ValueToString());
    //            }
    //        }
    //    }
    //}
}
