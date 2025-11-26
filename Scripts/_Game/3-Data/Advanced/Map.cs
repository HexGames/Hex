using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data
{
    public static class Map
    {
        private static readonly Dictionary<HexPos, List<Tile>> _hexTiles = new();
        private static readonly List<Tile> _tilesInPlay = new List<Tile>();
        public static ReadOnlyCollection<Tile> TilesInPlay => _tilesInPlay.AsReadOnly();

        public static void Init(List<Def.Tile> tileDefs)
        {
            int defIdx = 0;
            for (int x = -3; x <= 3; x++)
            {
                for (int y = Math.Max(-3, -x - 3); y <= Math.Min(3, -x + 3); y++)
                {
                    var coords = new HexPos(x, y);
                    if (Data.HexPos.Distance(coords, new HexPos(0, 0)) <= 3)
                    {
                        var tile = new Tile(tileDefs[defIdx], Tile.State.IN_PLAY);
                        defIdx++;
                        // Initialize list for each coord
                        _hexTiles[coords] = new List<Data.Tile> { tile };
                        _tilesInPlay.Add(tile);
                    }
                }
            }
        }

        //public static void ClearMapTiles(List<Tile> tiles)
        //{
        //    _tilesInPlay.Clear();
        //}

        // -------------------------------------------------------------------------------------------------------------- methods for future needs
        // HexPos and hex grid operations

        // Returns the first tile at the coord, or null if none

        //private Tile GetHexAtCoord(int x, int y)
        //{
        //    return GetHexAtCoord(new HexPos(x, y));
        //}


        // Returns all tiles at the coord, or empty list if none

        //private List<Tile> GetTilesAtCoord(int x, int y)
        //{
        //    return GetTilesAtCoord(new HexPos(x, y));
        //}

        //private List<Tile> GetTilesAtCoord(HexPos coord)
        //{
        //    if (_hexTiles.TryGetValue(coord, out var tiles))
        //        return tiles;
        //    return new List<Tile>();
        //}

        //private List<Tile> GetAdjacentHexes(int x, int y)
        //{
        //    return GetAdjacentHexes(new HexPos(x, y));
        //}

        //private List<Tile> GetAdjacentHexes(HexPos coord)
        //{
        //    var adj = new List<Tile>();
        //    foreach (var dir in HexPos.Directions)
        //    {
        //        var neighbor = new HexPos(coord.X + dir.X, coord.Y + dir.Y);
        //        if (_hexTiles.TryGetValue(neighbor, out var tiles) && tiles.Count > 0)
        //            adj.AddRange(tiles);
        //    }
        //    return adj;
        //}

        //private List<Tile> GetAdjacentHexes(Tile tile)
        //{
        //    var coord = GetCoordOfTile(tile);
        //    return coord is null ? new List<Tile>() : GetAdjacentHexes(coord.Value);
        //}

        //private bool IsAdjacent(int x1, int y1, int x2, int y2)
        //{
        //    return IsAdjacent(new HexPos(x1, y1), new HexPos(x2, y2));
        //}

        //private bool IsAdjacent(HexPos a, HexPos b)
        //{
        //    return HexPos.Distance(a, b) == 1;
        //}

        //private bool IsAdjacent(Tile a, Tile b)
        //{
        //    var coordA = GetCoordOfTile(a);
        //    var coordB = GetCoordOfTile(b);
        //    if (coordA is null || coordB is null) return false;
        //    return IsAdjacent(coordA.Value, coordB.Value);
        //}

        //private bool IsAdjacent(Tile tile, IEnumerable<Tile> tiles)
        //{
        //    var coord = GetCoordOfTile(tile);
        //    if (coord is null) return false;
        //    foreach (var t in tiles)
        //    {
        //        var c = GetCoordOfTile(t);
        //        if (c != null && IsAdjacent(coord.Value, c.Value))
        //            return true;
        //    }
        //    return false;
        //}

        //private bool IsAdjacent(HexPos coord, IEnumerable<HexPos> coords)
        //{
        //    foreach (var c in coords)
        //    {
        //        if (IsAdjacent(coord, c))
        //            return true;
        //    }
        //    return false;
        //}
        
        public static bool IsHexPosOnMap(HexPos coord)
        {
            return HexPos.Distance(coord, new HexPos(0, 0)) <= 3;
        }

        public static HexPos GetCoords(Tile tile)
        {
            HexPos? coords = GetCoordOfTile(tile);
            if (coords != null)
            {
                return coords.Value;
            }
            else
            {
                Debug.LogError($"Tile not found in map dictionary");
                return HexPos.Invalid;
            }
        }
        public static Tile GetTile(HexPos coord)
        {
            if (_hexTiles.TryGetValue(coord, out var tiles) && tiles.Count > 0)
                return tiles[0];
            return null;
        }

        // Helper: get the coordinate of a tile
        private static HexPos? GetCoordOfTile(Tile tile)
        {
            foreach (var kvp in _hexTiles)
            {
                if (kvp.Value.Contains(tile))
                    return kvp.Key;
            }
            return null;
        }


        // --------------------------------------------------------------------------------------------------------------
        public static void AddTile(Tile tile, HexPos coord)
        {
            if (_hexTiles.ContainsKey(coord) == false)
                _hexTiles[coord] = new List<Tile>();
            _hexTiles[coord].Add(tile);
            _tilesInPlay.Add(tile);
        }

        public static void RemoveTile(Tile tile)
        {
            HexPos? coords = GetCoordOfTile(tile);
            if (coords != null)
            {
                RemoveTile(tile, coords.Value);
            }
            else
            {
                Debug.LogError($"Tile not found in map dictionary");
            }
        }

        public static void RemoveTiles(HexPos coord)
        {
            if (_hexTiles.TryGetValue(coord, out var tiles))
            {
                foreach (var tile in tiles)
                {
                    _tilesInPlay.Remove(tile);
                }
                tiles.Clear();
            }
            else
            {
                Debug.LogError($"No tiles found at coord {coord}.");
            }
        }
        private static void RemoveTile(Tile tile, HexPos coord)
        {
            if (_hexTiles.TryGetValue(coord, out var tiles))
            {
                if (tiles.Contains(tile))
                {
                    tiles.Remove(tile);
                    _tilesInPlay.Remove(tile);
                }
                else
                {
                    Debug.LogError($"Tile not found at coord {coord}.");
                }
            }
            else
            {
                Debug.LogError($"No tiles found at coord {coord}.");
            }
        }
    }
}