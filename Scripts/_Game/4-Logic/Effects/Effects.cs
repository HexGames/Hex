using System;
using System.Collections.Generic;
namespace Logic
{
    public static class Effects
    {
        // ------------------------------------------------------------------------------------------
        public static List<Data.Benefit> Gain(Data.HexCoord coord, Def.Res res, int value, Def.Timing timing)
        {
            return new List<Data.Benefit> { new Data.Benefit(res, value, coord, timing) };
        }

        // ------------------------------------------------------------------------------------------
        public static List<Data.Benefit> PerAdjacent(Data.HexCoord coord, Data.Map map, string param, Def.Res res, int value, Data.Tile filterTile, Def.Timing timing)
        {
            // param is tag
            int count = 0;
            foreach (Data.HexCoord direction in Data.HexCoord.Directions)
            {
                Data.Tile adjTile = map.GetTile(coord + direction);
                if (adjTile != null)
                {
                    if (filterTile == null || filterTile == adjTile)
                    {
                        foreach (string tag in adjTile.Def.Data_Tags)
                        {
                            if (tag == param)
                            {
                                count++;
                                break;
                            }
                        }
                    }
                }
            }
            if (count == 0)
                return new List<Data.Benefit>();

            return new List<Data.Benefit> { new Data.Benefit(res, value * count, coord, timing) };
        }

        // ------------------------------------------------------------------------------------------
        public static List<Data.Benefit> IfAdjacent(Data.HexCoord coord, Data.Map map, string param, Def.Res res, int value, Data.Tile filterTile, Def.Timing timing)
        {
            // param is tag
            foreach (Data.HexCoord direction in Data.HexCoord.Directions)
            {
                Data.Tile adjTile = map.GetTile(coord + direction);
                if (adjTile != null)
                {
                    if (filterTile == null || filterTile == adjTile)
                    {
                        foreach (string tag in adjTile.Def.Data_Tags)
                        {
                            if (tag == param)
                            {
                                return new List<Data.Benefit> { new Data.Benefit(res, value, coord, timing) };
                            }
                        }
                    }
                }
            }
            return new List<Data.Benefit>();
        }

        // ------------------------------------------------------------------------------------------
        public static List<Data.Benefit> Cost(Data.Tile tile, Data.HexCoord coord, Def.Res res, int value, Def.Timing timing)
        {
            return new List<Data.Benefit> { new Data.Benefit(res, -value, coord, timing) };
        }

        // ------------------------------------------------------------------------------------------
        public static List<Data.Benefit> TransformX1(Data.Tile tile, Data.HexCoord coord, Data.Player player, string param, Def.Res res, int value, Def.Timing timing)
        {
            // param is res
            Def.Res transformedRes = Def.Lib.GetRes(param);
            int stockpileValue = Stockpile.GetValue(player.Stockpile, transformedRes);
            if (stockpileValue <= 0)
                return new List<Data.Benefit>();
            int transformedValue = 1;
            return new List<Data.Benefit> { new Data.Benefit(transformedRes, -transformedValue, coord, timing), new Data.Benefit(res, transformedValue * value, coord, timing) };
        }

        // ------------------------------------------------------------------------------------------
        public static List<Data.Benefit> TransformAll(Data.Tile tile, Data.HexCoord coord, Data.Player player, string param, Def.Res res, int value, Def.Timing timing)
        {
            // param is res
            Def.Res transformedRes = Def.Lib.GetRes(param);
            int stockpileValue = Stockpile.GetValue(player.Stockpile, transformedRes);
            if (stockpileValue <= 0)
                return new List<Data.Benefit>();
            return new List<Data.Benefit> { new Data.Benefit(transformedRes, -stockpileValue, coord, timing), new Data.Benefit(res, stockpileValue * value, coord, timing) };
        }

        // ------------------------------------------------------------------------------------------ Execute
        private static readonly Dictionary<string, System.Reflection.MethodInfo> _methodCache = new();
        public static List<Data.Benefit> Execute(Game game, Data.HexCoord coord, Data.Effect effect, Data.Tile filterTile = null) // tile is new or destroyed tile
        {
            if (effect.EffectID == "Execute" || effect.EffectID == "GetDescription")
                return new List<Data.Benefit>();

            string[] paramArray = effect.EffectParam.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int paramIdx = 0;

            bool foundCachedMethod = _methodCache.TryGetValue(effect.EffectID, out var method);
            if (foundCachedMethod == false)
            {
                method = typeof(Effects).GetMethod(effect.EffectID, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                if (method != null)
                    _methodCache[effect.EffectID] = method;
            }

            if (method != null)
            {
                var parameters = method.GetParameters();
                var args = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    var p = parameters[i];
                    if (p.ParameterType == typeof(Data.Map))
                        args[i] = game.Map;
                    else if (p.ParameterType == typeof(Data.Player))
                        args[i] = game.Player;
                    else if (p.ParameterType == typeof(Data.Tile))
                        args[i] = filterTile;
                    else if (p.ParameterType == typeof(Data.HexCoord))
                        args[i] = coord;
                    else if (p.ParameterType == typeof(Def.Res))
                        args[i] = effect.Res;
                    else if (p.ParameterType == typeof(Def.Timing))
                        args[i] = effect.EffectTiming;
                    else if (p.ParameterType == typeof(string))
                        args[i] = paramArray[paramIdx++];
                    else if (p.ParameterType == typeof(int))
                        args[i] = effect.Value;
                    else
                        args[i] = null;
                }
                return (List<Data.Benefit>)method.Invoke(null, args);
            }

            return new List<Data.Benefit>();
        }
    }
}