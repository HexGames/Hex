using System.Collections.Generic;

namespace Hex.Def
{
    public struct Tag 
    {
        public readonly int ID = -1;

        public readonly string Name = "";

        public Tag(int id, string tag)
        {
            ID = id;
            Name = tag;
        }
    }
}