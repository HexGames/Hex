using System.Collections.Generic;

namespace Hex.Def
{
    public struct Res 
    {
        public readonly int ID = -1;

        public readonly string Name = "";

        public readonly int Default = 0;
        public readonly string Title = "";
        public readonly string Description = "";
        public readonly string Image = "";
        public readonly int Value = 0;

        public Res(int id, Save.Block targetData)
        {
            ID = id;
            Name = targetData.ValueS;
            Default = targetData.GetSubValueI("Default");
            Title = targetData.GetSubValueS("UI", "Title");
            Description = targetData.GetSubValueS("UI", "Description");
            Image = targetData.GetSubValueS("UI", "Image");
            Value = targetData.GetSubValueI("Data", "Value");
        }
    }
}