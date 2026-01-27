// Made by AI: Claude Opus 4.5
using System;

namespace Hex.Logic.DataBuffer
{
    /// <summary>
    /// Debug accessor for BonusGiverBuffer internal data.
    /// Made by AI: Claude Opus 4.5
    /// </summary>
    public static class BonusGiverBufferDebug
    {
        // Made by AI: Claude Opus 4.5 - Changed to classes for proper JSON serialization
        public class BonusGiverItemDebug
        {
            public int MapTileId { get; set; }
            public int EffectIdx { get; set; }
        }
        
        public class BonusGiverListDebug
        {
            public int Start { get; set; }
            public int Count { get; set; }
            public int Capacity { get; set; }
            public BonusGiverItemDebug[] Items { get; set; } = Array.Empty<BonusGiverItemDebug>();
        }
        
        public class BonusGiverBufferDebugData
        {
            public int BufferLength { get; set; }
            public int ListsCount { get; set; }
            public int UsedCapacity { get; set; }
            public BonusGiverListDebug[] Lists { get; set; } = Array.Empty<BonusGiverListDebug>();
        }
        
        public static BonusGiverBufferDebugData GetDebugData()
        {
            var data = new BonusGiverBufferDebugData();
            
            // Access internal data through reflection or exposed methods
            var bufferField = typeof(BonusGiverBuffer).GetField("_buffer", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var listsField = typeof(BonusGiverBuffer).GetField("_lists", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var usedCapacityField = typeof(BonusGiverBuffer).GetField("_usedCapacity", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var listsCountField = typeof(BonusGiverBuffer).GetField("_listsCount", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            
            var buffer = bufferField?.GetValue(null) as BonusGiver[];
            var lists = listsField?.GetValue(null) as BonusGiverBuffer.BonusGiverList[];
            var usedCapacity = usedCapacityField?.GetValue(null) as int? ?? 0;
            var listsCount = listsCountField?.GetValue(null) as int? ?? 0;
            
            data.BufferLength = buffer?.Length ?? 0;
            data.UsedCapacity = usedCapacity;
            data.ListsCount = listsCount;
            
            if (lists != null && listsCount > 0)
            {
                data.Lists = new BonusGiverListDebug[listsCount];
                for (int i = 0; i < listsCount; i++)
                {
                    var list = lists[i];
                    data.Lists[i] = new BonusGiverListDebug
                    {
                        Start = list.Start,
                        Count = list.Count,
                        Capacity = list.Capacity,
                        Items = new BonusGiverItemDebug[list.Count]
                    };
                    
                    // Copy items
                    if (buffer != null)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            int idx = list.Start + j;
                            if (idx < buffer.Length)
                            {
                                data.Lists[i].Items[j] = new BonusGiverItemDebug
                                {
                                    MapTileId = buffer[idx].MapTile.ID,
                                    EffectIdx = buffer[idx].EffectIdx
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
