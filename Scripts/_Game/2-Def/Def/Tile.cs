using System.Collections.Generic;

namespace Def
{
    public class Tile
    {
        public int IDX = -1;

        public string ID = "";

        public int Starting = 0;
        public int Level = 0;
        public int Weight = 0;
        public int Initiative = 0;
        public List<string> Terrain = new List<string>();
        public List<string> Tags = new List<string>();
        public List<Var> Conditions = new List<Var>();
        public List<Var> Effects = new List<Var>();


        public string Map_TilePrefab = "";

        public string UI_Title = "";
        public string UI_ToolTip_Title = "";
        public string UI_ToolTip_Description = "";


        public Tile(Save.Block targetData)
        {
            ID = targetData.ValueS;


            if (targetData.HasSub("Data") != false)
            {
                Starting = targetData.GetSubValueI("Data", "Starting");
                Level = targetData.GetSubValueI("Data", "Level");
                Weight = targetData.GetSubValueI("Data", "Weight");
                Initiative = targetData.GetSubValueI("Data", "Initiative");

                Tags.Clear();
                List<Save.Block> tagsData = targetData.GetSub("Data").GetSubs("Tags");
                for (int idx = 0; idx < tagsData.Count; idx++)
                {
                    Tags.Add(tagsData[idx].ValueS);
                }

                Conditions.Clear();
                List<Save.Block> conditionsData = targetData.GetSub("Data").GetSubs("PlaceCondition");
                for (int idx = 0; idx < conditionsData.Count; idx++)
                {
                    Var varCondition = new Var(conditionsData[idx].ValueS);
                    Conditions.Add(varCondition);
                }

                Effects.Clear();
                List<Save.Block> effectsData = targetData.GetSub("Data").GetSubs("Effect");
                for (int idx = 0; idx < effectsData.Count; idx++)
                {
                    Var varEffect = new Var(effectsData[idx].ValueS);
                    Effects.Add(varEffect);
                }
            }

            Map_TilePrefab = targetData.GetSubValueS("Map", "Prefab");

            UI_Title = targetData.GetSubValueS("UI", "Title");
            UI_ToolTip_Title = targetData.GetSubValueS("UI", "ToolTip", "Title");
            UI_ToolTip_Description = targetData.GetSubValueS("UI", "ToolTip", "Description");
        }
    }
}