using System.Collections.Generic;

namespace Hex.Def
{
    public struct Tile
    {
        public readonly int ID = -1;

        public readonly string Name = "";

        public readonly int Starting = 0;
        public readonly int Level = 0;
        public readonly int Weight = 0;
        public readonly int Initiative = 0;
        private readonly List<string> _terrainTags = new List<string>();
        public IReadOnlyList<string> TerrainTags => _terrainTags.AsReadOnly();
        private readonly List<string> _buildingTags = new List<string>();
        public IReadOnlyList<string> BuildingTags => _buildingTags.AsReadOnly();
        private readonly List<string> _conditions = new List<string>();
        public IReadOnlyList<string> Conditions => _conditions.AsReadOnly();
        private readonly List<string> _effects = new List<string>();
        public IReadOnlyList<string> Effects => _effects.AsReadOnly();

        public readonly string Map_TilePrefab = "";

        public readonly string UI_Title = "";
        public readonly string UI_ToolTip_Title = "";
        public readonly string UI_ToolTip_Description = "";


        public Tile(int id, Save.Block targetData)
        {
            ID = id;
            Name = targetData.ValueS;


            if (targetData.HasSub("Data") != false)
            {
                Starting = targetData.GetSubValueI("Data", "Starting");
                Level = targetData.GetSubValueI("Data", "Level");
                Weight = targetData.GetSubValueI("Data", "Weight");
                Initiative = targetData.GetSubValueI("Data", "Initiative");

                _terrainTags.Clear();
                List<Save.Block> terrainTagsData = targetData.GetSub("Data").GetSubs("Terrain");
                for (int idx = 0; idx < terrainTagsData.Count; idx++)
                {
                    _terrainTags.Add(terrainTagsData[idx].ValueS);
                }

                _buildingTags.Clear();
                List<Save.Block> tagsData = targetData.GetSub("Data").GetSubs("Tags");
                for (int idx = 0; idx < tagsData.Count; idx++)
                {
                    _buildingTags.Add(tagsData[idx].ValueS);
                }

                _conditions.Clear();
                List<Save.Block> conditionsData = targetData.GetSub("Data").GetSubs("PlaceCondition");
                for (int idx = 0; idx < conditionsData.Count; idx++)
                {
                    _conditions.Add(conditionsData[idx].ValueS);
                }

                _effects.Clear();
                List<Save.Block> effectsData = targetData.GetSub("Data").GetSubs("Effect");
                for (int idx = 0; idx < effectsData.Count; idx++)
                {
                    _effects.Add(effectsData[idx].ValueS);
                }
            }

            Map_TilePrefab = targetData.GetSubValueS("Map", "Prefab");

            UI_Title = targetData.GetSubValueS("UI", "Title");
            UI_ToolTip_Title = targetData.GetSubValueS("UI", "ToolTip", "Title");
            UI_ToolTip_Description = targetData.GetSubValueS("UI", "ToolTip", "Description");
        }
    }
}