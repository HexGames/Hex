// Made by AI: Claude Opus 4.5
using System;

namespace Hex.Logic.DataBuffer
{
    /// <summary>
    /// Debug accessor for LocalStepBuffer internal data.
    /// Made by AI: Claude Opus 4.5
    /// </summary>
    public static class LocalStepBufferDebug
    {
        // Made by AI: Claude Opus 4.5 - Classes for JSON serialization
        public class LocalStepItemDebug
        {
            public int SourceTileId { get; set; }
            public int Value { get; set; }
        }
        
        public class LocalStepListDebug
        {
            public int Start { get; set; }
            public int Count { get; set; }
            public int Capacity { get; set; }
            public LocalStepItemDebug[] Items { get; set; } = Array.Empty<LocalStepItemDebug>();
        }
        
        public class LocalStepBufferDebugData
        {
            public int BufferLength { get; set; }
            public int ListsCount { get; set; }
            public int UsedCapacity { get; set; }
            public LocalStepListDebug[] Lists { get; set; } = Array.Empty<LocalStepListDebug>();
        }
        
        public static LocalStepBufferDebugData GetDebugData()
        {
            var data = new LocalStepBufferDebugData();
            
            // Access internal data through reflection
            var bufferField = typeof(LocalStepBuffer).GetField("_buffer", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var listsField = typeof(LocalStepBuffer).GetField("_lists", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var usedCapacityField = typeof(LocalStepBuffer).GetField("_usedCapacity", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var listsCountField = typeof(LocalStepBuffer).GetField("_listsCount", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            
            var buffer = bufferField?.GetValue(null) as LocalStep[];
            var lists = listsField?.GetValue(null) as LocalStepBuffer.LocalStepList[];
            var usedCapacity = usedCapacityField?.GetValue(null) as int? ?? 0;
            var listsCount = listsCountField?.GetValue(null) as int? ?? 0;
            
            data.BufferLength = buffer?.Length ?? 0;
            data.UsedCapacity = usedCapacity;
            data.ListsCount = listsCount;
            
            if (lists != null && listsCount > 0)
            {
                data.Lists = new LocalStepListDebug[listsCount];
                for (int i = 0; i < listsCount; i++)
                {
                    var list = lists[i];
                    data.Lists[i] = new LocalStepListDebug
                    {
                        Start = list.Start,
                        Count = list.Count,
                        Capacity = list.Capacity,
                        Items = new LocalStepItemDebug[list.Count]
                    };
                    
                    // Copy items
                    if (buffer != null)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            int idx = list.Start + j;
                            if (idx < buffer.Length)
                            {
                                data.Lists[i].Items[j] = new LocalStepItemDebug
                                {
                                    SourceTileId = buffer[idx].SourceTile.ID,
                                    Value = buffer[idx].Value
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
