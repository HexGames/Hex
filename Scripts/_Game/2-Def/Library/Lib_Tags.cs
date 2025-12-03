using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Def
{
    public static partial class Lib
    {
        private static List<Tag> _tags = new List<Tag>();
        public static ReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        private static void InitTagsDefs(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                for (int idx = 0; idx < tile.TerrainTags.Count; idx++)
                {
                    string tagName = tile.TerrainTags[idx];

                    if (GetTag(tagName) != null)
                        continue;

                    if (GetTag(tagName) == null)
                    {
                        Tag tag = new Tag(tagName);
                        tag.ID = idx;
                        _tags.Add(tag);
                    }
                }

                for (int idx = 0; idx < tile.BuildingTags.Count; idx++)
                {
                    string tagName = tile.BuildingTags[idx];

                    if (GetTag(tagName) != null)
                        continue;

                    if (GetTag(tagName) == null)
                    {
                        Tag tag = new Tag(tagName);
                        tag.ID = idx;
                        _tags.Add(tag);
                    }
                }
            }
        }

        public static Tag GetTag(string name)
        {
            foreach (Tag tag in _tags)
            {
                if (tag.Name == name)
                {
                    return tag;
                }
            }
            return null;
        }

        public static Tag GetTag(int ID)
        {
            return _tags[ID];
        }

        public static Tag GetTag(ReadOnlySpan<char> id)
        {
            foreach (Tag tag in _tags)
            {
                if (id.SequenceEqual(tag.Name.AsSpan()) == true)
                {
                    return tag;
                }
            }
            return null;
        }
    }
}