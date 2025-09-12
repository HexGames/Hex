using System;

namespace Def
{
    public class VarRes // TODO: Delete this class
    {
        public Res Res = null;
        public int Value = 0;
        public bool IsIncome = false;
        public int Turns = 0;

        public VarRes(string defString)
        {
            string[] mainSplit = defString.Split(":");

            if (mainSplit[0].StartsWith("Income-"))
            {
                IsIncome = true;
                Res = Lib.GetRes(mainSplit[0].Split("-")[1]);
            }
            else
            {
                Res = Lib.GetRes(mainSplit[0]);
            }

            if (Res == null)
            {
                Console.WriteLine("Res not found:" + mainSplit[0]);
            }

            if (mainSplit.Length <= 1) return;

            if (mainSplit[1].Contains('-'))
            {
                string[] resSplit = mainSplit[1].Split("-");
                int.TryParse(resSplit[0], out Value);
                if (resSplit[1].EndsWith("Turns")) int.TryParse(resSplit[1].Replace("Turns", ""), out Turns);
            }
            else
            {
                int.TryParse(mainSplit[1], out Value);
            }
        }
        
        /*public VarRes(string defString)
        {
            string[] split_1 = defString.Split(":");

            Res = Lib.x.GetResInfo(split_1[0]);

            if (Res == null)
            {
                Console.WriteLine("Res not found:" + split_1[0]);
            }

            if (split_1.Length <= 1) return;

            if (split_1[1].Contains('*'))
            {
                string[] split_2 = split_1[1].Split("*");
                if (split_2[0].StartsWith("+")) int.TryParse(split_2[0], out PerTurn);
                else int.TryParse(split_2[0], out Value);

                string[] split_3 = split_2[1].Split("<");
                if (split_3.Length > 1) int.TryParse(split_3[1], out ForEachMax);
                ForEach = split_3[0];
            }
            else if (split_1[1].Contains('-'))
            {
                string[] split_2 = split_1[1].Split("-");
                if (split_2[0].StartsWith("+")) int.TryParse(split_2[0], out PerTurn);
                else int.TryParse(split_2[0], out Value);
                if (split_2[1].EndsWith("Turns")) int.TryParse(split_2[1].Replace("Turns", ""), out Turns);
            }
            else
            {
                if (split_1[1].StartsWith("+")) int.TryParse(split_1[1], out PerTurn);
                else int.TryParse(split_1[1], out Value);
            }
        }*/
    }
}