using System.Collections.Generic;

namespace Hex.Def
{
    public class Tile
    {
        public int ID = -1;

        public string Name = "";

        public int Starting = 0;
        public int Level = 0;
        public int Weight = 0;
        public int Initiative = 0;
        public List<string> TerrainTags = new List<string>();
        public List<string> BuildingTags = new List<string>();
        public List<string> Conditions = new List<string>();
        public List<string> Effects = new List<string>();

        public string Map_TilePrefab = "";

        public string UI_Title = "";
        public string UI_ToolTip_Title = "";
        public string UI_ToolTip_Description = "";


        public Tile(Save.Block targetData)
        {
            Name = targetData.ValueS;


            if (targetData.HasSub("Data") != false)
            {
                Starting = targetData.GetSubValueI("Data", "Starting");
                Level = targetData.GetSubValueI("Data", "Level");
                Weight = targetData.GetSubValueI("Data", "Weight");
                Initiative = targetData.GetSubValueI("Data", "Initiative");

                BuildingTags.Clear();
                List<Save.Block> tagsData = targetData.GetSub("Data").GetSubs("Tags");
                for (int idx = 0; idx < tagsData.Count; idx++)
                {
                    BuildingTags.Add(tagsData[idx].ValueS);
                }

                Conditions.Clear();
                List<Save.Block> conditionsData = targetData.GetSub("Data").GetSubs("PlaceCondition");
                for (int idx = 0; idx < conditionsData.Count; idx++)
                {
                    Conditions.Add(conditionsData[idx].ValueS);
                }

                Effects.Clear();
                List<Save.Block> effectsData = targetData.GetSub("Data").GetSubs("Effect");
                for (int idx = 0; idx < effectsData.Count; idx++)
                {
                    Effects.Add(effectsData[idx].ValueS);
                }
            }

            Map_TilePrefab = targetData.GetSubValueS("Map", "Prefab");

            UI_Title = targetData.GetSubValueS("UI", "Title");
            UI_ToolTip_Title = targetData.GetSubValueS("UI", "ToolTip", "Title");
            UI_ToolTip_Description = targetData.GetSubValueS("UI", "ToolTip", "Description");
        }
    }
}