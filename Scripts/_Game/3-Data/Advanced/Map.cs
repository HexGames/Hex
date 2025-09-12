using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data
{
    public class Map
    {
        private readonly Dictionary<HexCoord, List<Tile>> _hexTiles = new();
        private readonly List<Tile> _tilesInPlay = new List<Tile>();
        public ReadOnlyCollection<Tile> TilesInPlay => _tilesInPlay.AsReadOnly();

        public Map(List<Def.Tile> tileDefs)
        {
            int defIdx = 0;
            for (int x = -3; x <= 3; x++)
            {
                for (int y = Math.Max(-3, -x - 3); y <= Math.Min(3, -x + 3); y++)
                {
                    var coord = new HexCoord(x, y);
                    if (Data.HexCoord.Distance(coord, new HexCoord(0, 0)) <= 3)
                    {
                        var tile = new Tile(tileDefs[defIdx], Tile.State.IN_PLAY);
                        defIdx++;
                        // Initialize list for each coord
                        _hexTiles[coord] = new List<Data.Tile> { tile };
                        _tilesInPlay.Add(tile);
                    }
                }
            }
        }

        public void SetMapTiles(List<Tile> tiles)
        {
            _tilesInPlay.Clear();
        }

        // --------------------------------------------------------------------------------------------------------------
        // HexCoords and hex grid operations



        // Returns the first tile at the coord, or null if none

        //private Tile GetHexAtCoord(int x, int y)
        //{
        //    return GetHexAtCoord(new HexCoord(x, y));
        //}


        // Returns all tiles at the coord, or empty list if none

        //private List<Tile> GetTilesAtCoord(int x, int y)
        //{
        //    return GetTilesAtCoord(new HexCoord(x, y));
        //}

        //private List<Tile> GetTilesAtCoord(HexCoord coord)
        //{
        //    if (_hexTiles.TryGetValue(coord, out var tiles))
        //        return tiles;
        //    return new List<Tile>();
        //}

        //private List<Tile> GetAdjacentHexes(int x, int y)
        //{
        //    return GetAdjacentHexes(new HexCoord(x, y));
        //}

        //private List<Tile> GetAdjacentHexes(HexCoord coord)
        //{
        //    var adj = new List<Tile>();
        //    foreach (var dir in HexCoord.Directions)
        //    {
        //        var neighbor = new HexCoord(coord.X + dir.X, coord.Y + dir.Y);
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
        //    return IsAdjacent(new HexCoord(x1, y1), new HexCoord(x2, y2));
        //}

        //private bool IsAdjacent(HexCoord a, HexCoord b)
        //{
        //    return HexCoord.Distance(a, b) == 1;
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

        //private bool IsAdjacent(HexCoord coord, IEnumerable<HexCoord> coords)
        //{
        //    foreach (var c in coords)
        //    {
        //        if (IsAdjacent(coord, c))
        //            return true;
        //    }
        //    return false;
        //}
        
        public bool IsHexCoordOnMap(HexCoord coord)
        {
            return HexCoord.Distance(coord, new HexCoord(0, 0)) <= 3;
        }

        public HexCoord GetCoord(Tile tile)
        {
            HexCoord? coords = GetCoordOfTile(tile);
            if (coords != null)
            {
                return coords.Value;
            }
            else
            {
                Debug.LogError($"Tile not found in map dictionary");
                return HexCoord.Invalid;
            }
        }
        public Tile GetTile(HexCoord coord)
        {
            if (_hexTiles.TryGetValue(coord, out var tiles) && tiles.Count > 0)
                return tiles[0];
            return null;
        }

        // Helper: get the coordinate of a tile
        private HexCoord? GetCoordOfTile(Tile tile)
        {
            foreach (var kvp in _hexTiles)
            {
                if (kvp.Value.Contains(tile))
                    return kvp.Key;
            }
            return null;
        }


        // --------------------------------------------------------------------------------------------------------------
        public void AddTile(Tile tile, HexCoord coord)
        {
            if (_hexTiles.ContainsKey(coord) == false)
                _hexTiles[coord] = new List<Tile>();
            _hexTiles[coord].Add(tile);
            _tilesInPlay.Add(tile);
        }

        public void RemoveTile(Tile tile)
        {
            HexCoord? coords = GetCoordOfTile(tile);
            if (coords != null)
            {
                RemoveTile(tile, coords.Value);
            }
            else
            {
                Debug.LogError($"Tile not found in map dictionary");
            }
        }

        public void RemoveTiles(HexCoord coord)
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
        private void RemoveTile(Tile tile, HexCoord coord)
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