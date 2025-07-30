using System.Collections.Generic;

namespace Def
{
    public class VarEffect
    {
        public Res Res = null;
        public int Value = 0;
        public string EffectTiming = "";
        public string EffectID = "";
        public string EffectParam = "";

        public VarEffect(string defString)
        {
            string[] mainSplit = defString.Split(":");
            if (mainSplit.Length == 4)
            {

                // 0
                EffectTiming = mainSplit[0];

                // 1
                string[] effectSplit = mainSplit[1].Split("-");
                EffectID = effectSplit[0];
                if (effectSplit.Length > 1)
                {
                    EffectParam = effectSplit[1];
                }

                // 2
                Res = Lib.GetRes(mainSplit[2]);

                // 3
                int.TryParse(mainSplit[3], out Value);
            }
            else if (mainSplit.Length == 2)
            {

                // 0
                EffectTiming = mainSplit[0];

                // 1
                string[] effectSplit = mainSplit[1].Split("-");
                EffectID = effectSplit[0];
                if (effectSplit.Length > 1)
                {
                    EffectParam = effectSplit[1];
                }
            }
            else
            {
                Debug.LogError("[VarEffect] mainSplit.Length unsupported");
                return;
            }
        }
    }
}