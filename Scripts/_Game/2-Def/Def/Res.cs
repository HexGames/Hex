using System.Collections.Generic;

namespace Def
{
    public class Res 
    {
        public int IDX = -1;

        public string ID = "";

        public int Default = 0;
        public string Title = "";
        public string Description = "";
        public string Image = "";
        public int Value = 0;

        public Res(Save.Block targetData)
        {
            ID = targetData.ValueS;
            Default = targetData.GetSubValueI("Default");
            Title = targetData.GetSubValueS("UI", "Title");
            Description = targetData.GetSubValueS("UI", "Description");
            Image = targetData.GetSubValueS("UI", "Image");
            Value = targetData.GetSubValueI("Data", "Value");
        }
    }
}