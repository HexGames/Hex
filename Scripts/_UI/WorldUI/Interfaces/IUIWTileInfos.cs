using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GodotUI
{
    public interface IUIWTileInfos
    {
        void AddTileInfo(Data.HexCoord forTile, string text);
        void RemoveTileInfo(Data.HexCoord forTile);
        void ClearTileInfos();
    }
}
