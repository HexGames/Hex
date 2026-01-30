// Made by AI: Claude Opus 4.5
using System;

namespace Hex.Logic.DataBuffer
{
    /// <summary>
    /// Debug accessor for BonusStepBuffer internal data.
    /// Made by AI: Claude Opus 4.5
    /// </summary>
    public static class BonusStepBufferDebug
    {
        // Made by AI: Claude Opus 4.5 - Changed to classes for proper JSON serialization
        public class BonusStepItemDebug
        {
            public int TileId { get; set; }
            public int SourceTileId { get; set; }
            public int Depth { get; set; }
            public string BonusType { get; set; } = "";
            public int Value { get; set; }
        }
        
        public class BonusStepListDebug
        {
            public int Start { get; set; }
            public int Count { get; set; }
            public int Capacity { get; set; }
            public BonusStepItemDebug[] Items { get; set; } = Array.Empty<BonusStepItemDebug>();
        }
        
        public class BonusStepBufferDebugData
        {
            public int BufferLength { get; set; }
            public int ListsCount { get; set; }
            public int UsedCapacity { get; set; }
            public BonusStepListDebug[] Lists { get; set; } = Array.Empty<BonusStepListDebug>();
        }
        
        public static BonusStepBufferDebugData GetDebugData()
        {
            var data = new BonusStepBufferDebugData();
            
            // Access internal data through reflection
            var bufferField = typeof(BonusStepBuffer).GetField("_buffer", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var listsField = typeof(BonusStepBuffer).GetField("_lists", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var usedCapacityField = typeof(BonusStepBuffer).GetField("_usedCapacity", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var listsCountField = typeof(BonusStepBuffer).GetField("_listsCount", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            
            var buffer = bufferField?.GetValue(null) as BonusStep[];
            var lists = listsField?.GetValue(null) as BonusStepBuffer.BonusStepList[];
            var usedCapacity = usedCapacityField?.GetValue(null) as int? ?? 0;
            var listsCount = listsCountField?.GetValue(null) as int? ?? 0;
            
            data.BufferLength = buffer?.Length ?? 0;
            data.UsedCapacity = usedCapacity;
            data.ListsCount = listsCount;
            
            if (lists != null && listsCount > 0)
            {
                data.Lists = new BonusStepListDebug[listsCount];
                for (int i = 0; i < listsCount; i++)
                {
                    var list = lists[i];
                    data.Lists[i] = new BonusStepListDebug
                    {
                        Start = list.Start,
                        Count = list.Count,
                        Capacity = list.Capacity,
                        Items = new BonusStepItemDebug[list.Count]
                    };
                    
                    // Copy items
                    if (buffer != null)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            int idx = list.Start + j;
                            if (idx < buffer.Length)
                            {
                                data.Lists[i].Items[j] = new BonusStepItemDebug
                                {
                                    TileId = buffer[idx].Tile.ID,
                                    SourceTileId = buffer[idx].SourceTile.ID,
                                    Depth = buffer[idx].Depth,
                                    BonusType = buffer[idx].EffectType.ToString(),
                                    Value = buffer[idx].Total
                                };
                            }
                        }
                    }
                }
            }
            
            return data;
        }
    }
}
