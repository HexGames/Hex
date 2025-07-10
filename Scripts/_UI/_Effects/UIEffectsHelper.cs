using System;
using System.Collections.Generic;

namespace GodotUI
{
    public static class UIEffectsHelper
    {
        public static string Gain(Def.Res res, int value, Def.Timing timing)
        {
            return "Gain " + UIHelper.ResToString(res, value, alwaysShowSign: true, redNegativeValues: true);
        }

        public static string PerAdjacent(Data.Map map, string param, Def.Res res, int value, Def.Timing timing)
        {
            return "Gain " + UIHelper.ResToString(res, value, alwaysShowSign: true, redNegativeValues: true) + " for each adj.\n" + param;
        }

        public static string IfAdjacent(Data.Map map, string param, Def.Res res, int value, Def.Timing timing)
        {
            return "Gain " + UIHelper.ResToString(res, value, alwaysShowSign: true, redNegativeValues: true) + " if adj. to\n" + param;
        }
        public static string Cost(Def.Res res, int value, Def.Timing timing)
        {
            return "Lose " + UIHelper.ResToString(res, -value, alwaysShowSign: true, redNegativeValues: true);
        }
        public static string TransformX1(Data.Player player, string param, Def.Res res, int value, Def.Timing timing)
        {
            return "Transform 1" + UIHelper.GetIcon(param) + " to " + UIHelper.ResToString(res, value, alwaysShowSign: true, redNegativeValues: true);
        }

        public static string TransformAll(Data.Player player, string param, Def.Res res, int value, Def.Timing timing)
        {
            return "Transform all " + UIHelper.GetIcon(param) + " to " + UIHelper.ResToString(res, value, alwaysShowSign: true, redNegativeValues: true);
        }

        private static readonly Dictionary<string, System.Reflection.MethodInfo> _methodCache = new();
        public static string GetDescription(Data.Effect effect)
        {
            if (effect.EffectID == "Execute" || effect.EffectID == "GetDescription")
                return null;

            string[] paramArray = effect.EffectParam.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int paramIdx = 0;

            bool foundCachedMethod = _methodCache.TryGetValue(effect.EffectID, out var method);
            if (foundCachedMethod == false)
            {
                method = typeof(UIEffectsHelper).GetMethod(effect.EffectID, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
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
                    if (p.ParameterType == typeof(Def.Res))
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
                return (string)method.Invoke(null, args);
            }

            return null;
        }
    }
}