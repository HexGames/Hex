using Godot;
using System;
using System.Collections.Generic;

namespace Save
{
    public static class Data
    {
        public enum BaseType
        {
            NONE,
            INT,
            FLOAT,
            STRING
        }        
        // --- DB
        public static Dictionary<int, string> _DB_TypeToString = new Dictionary<int, string>();
        public static Dictionary<string, int> _DB_TypeToInt = new Dictionary<string, int>();

        public static int _GetDBType(string name, BaseType baseType)
        {

            int type;
            if (_DB_TypeToInt.TryGetValue(name, out type) == false)
            {
                int newType = 0 + ((int)baseType) * 10000;
                while (_DB_TypeToString.ContainsKey(newType)) newType++;

                _DB_TypeToString.Add(newType, name);
                _DB_TypeToInt.Add(name, newType);
                //GD.Print("Def new Data Block Type: " + name + " - " + newType);

                return newType;
            }

            return type;
        }

        public static string _GetDBValue(int type)
        {
            string name;
            if (_DB_TypeToString.TryGetValue(type, out name) == false)
            {
                //GD.PrintErr("Def Data Block Type  " + type + " not found!");
                return "";
            }
        
            return name;
        }

        //

        static public Block CreateData(string name)
        {
            Block data = new Block();

            data.Type = _GetDBType("_" + name, BaseType.NONE);
            data.Name = name;

            return data;
        }

        static public Block CreateData(string name, int value)
        {
            Block data = new Block();

            data.Type = _GetDBType("i_" + name, BaseType.INT);
            data.Name = name;
            data.ValueI = value;

            return data;
        }

        static public Block CreateData(string name, string value)
        {
            Block data = new Block();

            data.Type = _GetDBType("s_" + name, BaseType.STRING);
            data.Name = name;
            data.ValueS = value;

            return data;
        }

        static public Block AddData(Block parent, string name)
        {
            Block data = new Block();

            data.Type = _GetDBType("_" + name, BaseType.NONE);
            data.Name = name;

            parent.Subs.Add(data);
            //data.Parent = parent;

            return data;
        }

        static public Block AddData(Block parent, string name, int value)
        {
            Block data = new Block();

            data.Type = _GetDBType("i_" + name, BaseType.INT);
            data.Name = name;
            data.ValueI = value;

            parent.Subs.Add(data);
            //data.Parent = parent;

            return data;
        }

        static public Block AddData(Block parent, string name, string value)
        {
            Block data = new Block();

            data.Type = _GetDBType("s_" + name, BaseType.STRING);
            data.Name = name;
            data.ValueS = value;

            parent.Subs.Add(data);
            //data.Parent = parent;

            return data;
        }
        static public void RemoveDataByName(Block parent, string name, bool all = false)
        {
            for (int idx = 0; idx < parent.Subs.Count; idx++)
            {
                if (parent.Subs[idx].Name == name)
                {
                    parent.Subs.RemoveAt(idx);
                    if (all) idx--;
                    else break;
                }
            }
        }

        static public void RemoveDataByType(Block parent, string name, bool all = false)
        {
            int type = _GetDBType("_" + name, BaseType.NONE);

            for (int idx = 0; idx < parent.Subs.Count; idx++)
            {
                if (parent.Subs[idx].Type == type)
                {
                    parent.Subs.RemoveAt(idx);
                    if (all) idx--;
                    else break;
                }
            }
        }

        static public void RemoveDataByType(Block parent, string name, int value, bool all = false)
        {
            int type = _GetDBType("i_" + name, BaseType.INT);

            for (int idx = 0; idx < parent.Subs.Count; idx++)
            {
                if (parent.Subs[idx].Type == type && parent.Subs[idx].ValueI == value)
                {
                    parent.Subs.RemoveAt(idx);
                    if (all) idx--;
                    else break;
                }
            }
        }

        static public void RemoveDataByType(Block parent, string name, string value, bool all = false)
        {
            int type = _GetDBType("s_" + name, BaseType.STRING);

            for (int idx = 0; idx < parent.Subs.Count; idx++)
            {
                if (parent.Subs[idx].Type == type && parent.Subs[idx].ValueS == value)
                {
                    parent.Subs.RemoveAt(idx);
                    if (all) idx--;
                    else break;
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------
        /*static public void ChangeDataType(DataBlock data, string name)
        {
            Data.BaseType baseType = (Data.BaseType)(data.Type/10000);
            switch (baseType)
            {
                case BaseType.NONE: data.Type = _GetDBType("_" + name, BaseType.STRING); break;
                case BaseType.INT: data.Type = _GetDBType("i_" + name, BaseType.STRING); break;
                case BaseType.STRING: data.Type = _GetDBType("s_" + name, BaseType.STRING); break;
            }

            data.Name = name;
        }*/

        static public bool DeleteData(Block original, Block dataToRemove)
        {
            for (int oriIdx = 0; oriIdx < original.Subs.Count; oriIdx++)
            {
                for (int otherIdx = 0; otherIdx < dataToRemove.Subs.Count; otherIdx++)
                {
                    if (original.Subs[oriIdx].Name == dataToRemove.Subs[otherIdx].Name
                        && original.Subs[oriIdx].ValueI == dataToRemove.Subs[otherIdx].ValueI
                        && original.Subs[oriIdx].ValueS == dataToRemove.Subs[otherIdx].ValueS)
                    {
                        bool deletedAllSubs = DeleteData(original.Subs[oriIdx], dataToRemove.Subs[otherIdx]);
                        if (deletedAllSubs)
                        {
                            original.Subs.RemoveAt(oriIdx);
                            oriIdx--;
                            break;
                        }
                    }
                }
            }
            return original.Subs.Count == 0;
        }

        // ----------------------------------------------------------------------------------------------------
        static public Block LoadFile(string fileName)
        {
            var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Read);
            if (file == null)
            {
                file = FileAccess.Open(fileName, FileAccess.ModeFlags.Read);
            }
            string content = file.GetAsText();

            char[] delimiters = { '\n', '\r' ,'\t' };
            string[] rows = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            int rowIdx = -1;

            List<string> words = new List<string>();
            string[] wordsOnRow = LoadFile_GetWordsFromNextValidRow(rows, ref rowIdx);
            while (wordsOnRow != null)
            {
                if (words.Count > 0 && words[words.Count - 1] != "{" && words[words.Count - 1] != "}" && wordsOnRow[0] != "{")
                {
                    words.Add("{");
                    words.Add("}");
                }
                words.AddRange(wordsOnRow);
                wordsOnRow = LoadFile_GetWordsFromNextValidRow(rows, ref rowIdx);
            }

            return LoadBlock(words);
        }

        static string[] LoadFile_GetWordsFromNextValidRow(string[] rows, ref int rowIdx)
        {
            rowIdx++;
            if (rowIdx >= rows.Length)
            {
                // GD.PrintErr("Load map data error 01");
                return null;
            }
            rows[rowIdx] = rows[rowIdx].Replace("res://", "$res$");
            string[] words = rows[rowIdx].Split("//")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
            {
                return LoadFile_GetWordsFromNextValidRow(rows, ref rowIdx);
            }
            for (int i = 0; i < words.Length; i++) words[i] = words[i].Replace("$res$", "res://");
            return words;
        }

        static public Block LoadBlock(List<string> words)
        {
            Block data = new Block();

            BaseType baseType = BaseType.INT;
            if (words[1] == "{") baseType = BaseType.NONE;
            else if (System.Char.IsLetter(words[1][0])) baseType = BaseType.STRING;
            else if (words[1][0] == '#') baseType = BaseType.STRING;
            else if (words[1].Contains(".")) baseType = BaseType.FLOAT;

            string prefix = "_";
            if (baseType == BaseType.INT) prefix = "i_";
            if (baseType == BaseType.STRING) prefix = "s_";
            data.Type = _GetDBType(prefix + words[0], baseType);
            data.Name = words[0];
            switch (baseType)
            {
                case BaseType.INT: data.ValueI = words[1].ToInt(); break;
                case BaseType.STRING: data.ValueS = words[1]; break;
            }

            int subDataStart = 1;
            while (words[subDataStart] != "{") subDataStart++;
            subDataStart++; // {

            while (words[subDataStart] != "}")
            {
                int nextSubStart = subDataStart;
                int nextSubEnd = nextSubStart + 1;
                while (words[nextSubEnd] != "{") nextSubEnd++;
                int depth = 1;
                while (depth > 0)
                {
                    nextSubEnd++;
                    if (words[nextSubEnd] == "{") depth++;
                    if (words[nextSubEnd] == "}") depth--;
                }

                Block subData = LoadBlock(words.GetRange(nextSubStart, nextSubEnd + 1 - nextSubStart));
                data.Subs.Add(subData);
                //subData.Parent = data;

                subDataStart = nextSubEnd + 1;
            }

            return data;
        }

        // ----------------------------------------------------------------------------------------------------
        static public int SessionSave = 0;
        static public string LastSaveName = "";
        static public void SaveToFile_Progressive(Block data, string dirName)
        {
            if (SessionSave == 0)
            {
                string content = "";

                content += "// " + Time.GetDatetimeStringFromSystem() + "\n";

                content += SaveBlock(data, 0);

                string fileName = dirName + "Save.sav";
                var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Write);
                if (file == null)
                {
                    file = FileAccess.Open(fileName, FileAccess.ModeFlags.Write);
                }
                LastSaveName = fileName;
                file.StoreString(content);
                file.Close();

                SessionSave++;
            }
            else
            {
                string content = "";
                content += SaveBlock(data, 0);

                string fileName = dirName + "Save_" + SessionSave.ToString() + ".sav";
                var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Write);
                if (file == null)
                {
                    file = FileAccess.Open(fileName, FileAccess.ModeFlags.Write);
                }
                file.StoreString(content);
                file.Close();

                SessionSave++;

                Block lastSave = LoadFile(LastSaveName);
                Block newSave = LoadFile(fileName);
                DeleteData(newSave, lastSave);
                LastSaveName = fileName;

                content = "";
                content += SaveBlock(newSave, 0);

                string diffFileName = dirName + "Save_" + SessionSave.ToString() + "_diff.sav";
                var fileDiff = FileAccess.Open("res:///" + diffFileName, FileAccess.ModeFlags.Write);
                if (fileDiff == null)
                {
                    fileDiff = FileAccess.Open(diffFileName, FileAccess.ModeFlags.Write);
                }
                fileDiff.StoreString(content);
                fileDiff.Close();
            }
        }

        // ----------------------------------------------------------------------------------------------------
        static public void SaveToFile(Block data, string fileName)
        {
            string content = "";

            content += "// " + Time.GetDatetimeStringFromSystem() + "\n";

            // GameStats
            content += SaveBlock(data, 0);

            var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Write);
            if (file == null)
            {
                file = FileAccess.Open(fileName, FileAccess.ModeFlags.Write);
            }
            if (file == null)
            {
                GD.PrintErr("File error: " + fileName);
                return;
            }
            file.StoreString(content);
            file.Close();
        }

        static public string SaveBlock(Block dataBlock, int currentTabs)
        {
            string text = "";

            if (currentTabs > 25)
            {
                GD.PrintErr("Save over 25 deep - possible logic loop!");
                return "";
            }

            string name = _GetDBValue(dataBlock.Type);
            if (name == "")
            {
                name = dataBlock.Name;
            }
            if (name.StartsWith("_")) name = name.Substring(1);
            else name = name.Substring(2); // for i_ and s_
            text += Helper_Tabs(currentTabs) + name + " " + dataBlock.ValueToString() + "\n";

            //if (text.Contains("ActionColonyBuild"))
            //{
            //    GD.Print("HIT");
            //}

            if (dataBlock.Subs.Count > 0)
            {
                text += Helper_Tabs(currentTabs) + "{" + "\n";
                for (int subIdx = 0; subIdx < dataBlock.Subs.Count; subIdx++)
                {
                    text += SaveBlock(dataBlock.Subs[subIdx], currentTabs + 1);
                }
                text += Helper_Tabs(currentTabs) + "}" + "\n";
            }
            return text;
        }

        // ----------------------------------------------------------------------------------------------------
        static public Block LoadCSV(string fileName)
        {
            var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Read);
            if (file == null)
            {
                file = FileAccess.Open(fileName, FileAccess.ModeFlags.Read);
            }
            if (file == null)
            {
                GD.PrintErr("File not found: " + fileName);
                return null;
            }

            string content = file.GetAsText();

            content = content.Replace("\r", "");
            content = content.Replace("\t", "");
            content = content.Replace(" ", "_");

            char[] delimiters = { '\n' };
            string[] rows = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length < 4) return null;

            string[] tableHeadRow_1 = rows[0].Split(',');
            string[] tableHeadRow_2 = rows[1].Split(',');
            string[] tableHeadRow_3 = rows[2].Split(',');
            if (tableHeadRow_1[0] == "")
            {
                Debug.LogError("LoadCSV error: First column in the first row is empty. File: " + fileName);
                return null;
            }

            Block data = CreateData(tableHeadRow_1[0] + "s");

            List<string> words = new List<string>();
            List<string[]> extraRows = new List<string[]>();
            for (int idx = 3; idx < rows.Length; idx++)
            {
                string[] wordsOnRow = rows[idx].Split(',');
                if (wordsOnRow[0] == "") continue;

                if (wordsOnRow[0] == "-") continue;
                extraRows.Clear();
                for (int extraRowIdx = idx + 1; extraRowIdx < rows.Length; extraRowIdx++)
                {
                    string[] wordsOnExtraRow = rows[extraRowIdx].Split(',');
                    if (wordsOnExtraRow[0] == "-")
                    {
                        extraRows.Add(wordsOnExtraRow);
                    }
                    else
                    {
                        break;
                    }
                }

                Block item = AddData(data, tableHeadRow_1[0], wordsOnRow[0]);

                for (int colIdx = 1; colIdx < wordsOnRow.Length; colIdx++)
                {
                    if (wordsOnRow[colIdx] == "") continue;
                    if (colIdx < tableHeadRow_1.Length && colIdx < tableHeadRow_2.Length && colIdx < tableHeadRow_3.Length)
                    {
                        if (tableHeadRow_3[colIdx] != "" && tableHeadRow_2[colIdx] != "" && tableHeadRow_1[colIdx] != "")
                        {
                            Block level1 = item.GetSub(tableHeadRow_1[colIdx], false);
                            if (level1 == null) level1 = AddData(item, tableHeadRow_1[colIdx]);

                            Block level2 = level1.GetSub(tableHeadRow_2[colIdx], false);
                            if (level2 == null) level2 = AddData(level1, tableHeadRow_2[colIdx]);

                            Block level3 = null;
                            bool isInt = Int32.TryParse(wordsOnRow[colIdx], out int i);
                            if (isInt) level3 = AddData(level2, tableHeadRow_3[colIdx], i);
                            else level3 = AddData(level2, tableHeadRow_3[colIdx], wordsOnRow[colIdx]);
                            AddExtraData(extraRows, colIdx, level3);
                        }
                        else if (tableHeadRow_2[colIdx] != "" && tableHeadRow_1[colIdx] != "")
                        {
                            Block level1 = item.GetSub(tableHeadRow_1[colIdx], false);
                            if (level1 == null) level1 = AddData(item, tableHeadRow_1[colIdx]);

                            Block level2 = null;
                            bool isInt = Int32.TryParse(wordsOnRow[colIdx], out int i);
                            if (isInt) level2 = AddData(level1, tableHeadRow_2[colIdx], i);
                            else level2 = AddData(level1, tableHeadRow_2[colIdx], wordsOnRow[colIdx]);
                            AddExtraData(extraRows, colIdx, level2);
                        }
                        else
                        {
                            Block level1 = null;
                            bool isInt = Int32.TryParse(wordsOnRow[colIdx], out int i);
                            if (isInt) level1 = AddData(item, tableHeadRow_1[colIdx], i);
                            else level1 = AddData(item, tableHeadRow_1[colIdx], wordsOnRow[colIdx]);
                            AddExtraData(extraRows, colIdx, level1);
                        }
                    }
                }
            }

            return data;
        }

        private static void AddExtraData(List<string[]> extraRows, int colIdx, Block parent)
        {
            for (int extraRowIdx = 0; extraRowIdx < extraRows.Count; extraRowIdx++)
            {
                if (extraRows[extraRowIdx][colIdx] != "")
                {
                    string[] split = extraRows[extraRowIdx][colIdx].Split('&');
                    if (split.Length == 1)
                    {
                        AddData(parent, split[0]);
                    }
                    else if (split.Length == 2)
                    {
                        bool isInt = Int32.TryParse(split[1], out int i);
                        if (isInt) AddData(parent, split[0], i);
                        else AddData(parent, split[0], split[1]);
                    }
                    else
                    {
                        Debug.LogError("LoadCSV error: more than one & symbol not supported! Cell: " + extraRows[extraRowIdx][colIdx]);
                    }
                }
            }
        }

        // helper 
        public static string Helper_Tabs(int tabs)
        {
            string text = "";
            for (int n = 0; n < tabs; n++)
            {
                text += "\t";
            }
            return text;
        }
    }
}
