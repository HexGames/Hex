using System.Collections.Generic;

namespace Hex.Def
{
    public class Res 
    {
        public int ID = -1;

        public string Name = "";

        public int Default = 0;
        public string Title = "";
        public string Description = "";
        public string Image = "";
        public int Value = 0;

        public Res(Save.Block targetData)
        {
            Name = targetData.ValueS;
            Default = targetData.GetSubValueI("Default");
            Title = targetData.GetSubValueS("UI", "Title");
            Description = targetData.GetSubValueS("UI", "Description");
            Image = targetData.GetSubValueS("UI", "Image");
            Value = targetData.GetSubValueI("Data", "Value");
        }
    }
}