using System.Collections.Generic;

namespace Def
{
    public class Tile
    {
        public int IDX = -1;

        public string ID = "";

        public int Data_Level = 0;
        public int Data_Weight = 0;
        public int Data_Starting = 0;
        public List<string> Data_Terrain = new List<string>();
        public List<string> Data_Tags = new List<string>();
        // PlaceConditions
        // DraftConditions
        public List<VarEffect> Data_Effects = new List<VarEffect>();


        public string Map_TilePrefab = "";

        public string UI_Title = "";
        public string UI_ToolTip_Title = "";
        public string UI_ToolTip_Description = "";


        public Tile(Save.Block targetData)
        {
            ID = targetData.ValueS;


            if (targetData.HasSub("Data") != false)
            {
                Data_Level = targetData.GetSubValueI("Data", "Level");
                Data_Weight = targetData.GetSubValueI("Data", "Weight");
                Data_Starting = targetData.GetSubValueI("Data", "Starting");

                Data_Tags.Clear();
                List<Save.Block> tagsData = targetData.GetSub("Data").GetSubs("Tags");
                for (int idx = 0; idx < tagsData.Count; idx++)
                {
                    Data_Tags.Add(tagsData[idx].ValueS);
                }

                Data_Effects.Clear();
                List<Save.Block> benefitsData = targetData.GetSub("Data").GetSubs("Effect");
                for (int idx = 0; idx < benefitsData.Count; idx++)
                {
                    VarEffect varRes = new VarEffect(benefitsData[idx].ValueS);
                    if (varRes.Res != null) Data_Effects.Add(varRes);
                }
            }

            Map_TilePrefab = targetData.GetSubValueS("Map", "Prefab");

            UI_Title = targetData.GetSubValueS("UI", "Title");
            UI_ToolTip_Title = targetData.GetSubValueS("UI", "ToolTip", "Title");
            UI_ToolTip_Description = targetData.GetSubValueS("UI", "ToolTip", "Description");
        }
    }
}