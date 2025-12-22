using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Def
{
    public static partial class Lib
    {
        private static Tag[] _tags = null;
        public static ReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        private static void InitTagsDefs(Tile[] tiles)
        {
            List<Tag> tags = new List<Tag>();
            foreach (Tile tile in tiles)
            {
                for (int idx = 0; idx < tile.TerrainTags.Count; idx++)
                {
                    string tagName = tile.TerrainTags[idx];

                    foreach (Tag existingTag in tags)
                    {
                        if (existingTag.Name == tagName)
                        {
                            continue;
                        }
                    }

                    int id = tags.Count;
                    tags.Add(new Tag(id, tagName));
                }

                for (int idx = 0; idx < tile.BuildingTags.Count; idx++)
                {
                    string tagName = tile.BuildingTags[idx];

                    foreach (Tag existingTag in tags)
                    {
                        if (existingTag.Name == tagName)
                        {
                            continue;
                        }
                    }

                    int id = tags.Count;
                    tags.Add(new Tag(id, tagName));
                }
            }

            _tags = tags.ToArray();
        }

        public static ref Tag GetTag(int ID)
        {
            return ref _tags[ID];
        }

        public static TagRef GetTagRef(string name)
        {
            return GetTagRef(name.AsSpan());
        }

        internal static TagRef GetTagRef(ReadOnlySpan<char> name)
        {
            for (int idx = 0; idx < _tags.Length; idx++)
            {
                if (name.SequenceEqual(_tags[idx].Name))
                {
                    return TagRef.FromID(idx);
                }
            }
            return TagRef.INVALID;
        }
    }
}