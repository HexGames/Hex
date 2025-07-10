using System.Collections.Generic;

namespace Save
{
    public class Block
    {
        public int Type = 0;
        public string Name = "";
        public int ValueI = 0;
        public string ValueS = "";
        public List<Block> Subs = new List<Block>();

        public string ValueToString()
        {
            Data.BaseType baseType = (Data.BaseType)(Type/10000);

            switch (baseType)
            {
                case Data.BaseType.INT: return ValueI.ToString();
                case Data.BaseType.STRING: return ValueS;
            }

            return "";
        }

        public List<Block> GetSubs()
        {
            return Subs;
        }

        public bool HasSub(string sub_0)
        {
            foreach (Block sub in Subs)
            {
                if (sub.Name == sub_0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasSub(string sub_0, string sub_1)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return false;

            return dataSub_0.HasSub(sub_1);
        }

        public bool HasSub(string sub_0, string sub_1, string sub_2)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return false;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return false;

            return dataSub_1.HasSub(sub_2);
        }

        public bool HasSub(string sub_0, string sub_1, string sub_2, string sub_3)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return false;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return false;
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return false;

            return dataSub_2.HasSub(sub_3);
        }

        public Block GetSub(int type)
        {
            foreach (Block sub in Subs)
            {
                if (sub.Type == type)
                {
                    return sub;
                }
            }
            return null;
        }

        public List<Block> GetSubsList(int type)
        {
            List<Block> ret = new List<Block>();

            foreach (Block sub in Subs)
            {
                if (sub.Type == type)
                {
                    ret.Add(sub);
                }
            }

            return ret;
        }

        public Block GetSub(string type, bool showWarning = true)
        {
            foreach (Block sub in Subs)
            {
                if (sub.Name == type)
                {
                    return sub;
                }
            }
            //if (showWarning) 
            //    GD.Print("sub not found Data : Type - " + Name + " : " + type);

            return null;
        }

        public string GetSubValueS(string sub_0, bool showWarning = false)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return "";

            return dataSub_0.ValueS;
        }

        public string GetSubValueS(string sub_0, string sub_1, bool showWarning = false)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return "";
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return "";

            return dataSub_1.ValueS;
        }

        public string GetSubValueS(string sub_0, string sub_1, string sub_2, bool showWarning = false)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return "";
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return "";
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return "";

            return dataSub_2.ValueS;
        }

        public string GetSubValueS(string sub_0, string sub_1, string sub_2, string sub_3, bool showWarning = false)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return "";
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return "";
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return "";
            Block dataSub_3 = dataSub_2.GetSub(sub_3);
            if (dataSub_3 == null) return "";

            return dataSub_3.ValueS;
        }

        public string GetSubValueS_Path(string path, bool showWarning = false)
        {
            int splitIdx = path.IndexOf("/");
            if (splitIdx > 0)
            {
                Block sub = GetSub(path.Substring(0, splitIdx));
                if (sub != null)
                {
                    return sub.GetSubValueS_Path(path.Substring(splitIdx + 1), showWarning);
                }
            }
            else
            {
                foreach (Block sub in Subs)
                {
                    if (sub.Name == path)
                    {
                        return sub.ValueS;
                    }
                }
            }

            //if (showWarning)
            //    GD.Print("sub not found Data : Type - " + Name + " : " + path);

            return "";
        }

        public int GetSubValueI(string sub_0, bool showWarning = false)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return 0;

            return dataSub_0.ValueI;
        }

        public int GetSubValueI(string sub_0, string sub_1, bool showWarning = false)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return 0;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return 0;

            return dataSub_1.ValueI;
        }

        public int GetSubValueI(string sub_0, string sub_1, string sub_2, bool showWarning = false)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return 0;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return 0;
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return 0;

            return dataSub_2.ValueI;
        }

        public int GetSubValueI(string sub_0, string sub_1, string sub_2, string sub_3, bool showWarning = false)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return 0;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return 0;
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return 0;
            Block dataSub_3 = dataSub_2.GetSub(sub_3);
            if (dataSub_3 == null) return 0;

            return dataSub_3.ValueI;
        }

        public int GetSubValueI_Path(string path, bool showWarning = false)
        {
            int splitIdx = path.IndexOf("/");
            if (splitIdx > 0)
            {
                Block sub = GetSub(path.Substring(0, splitIdx));
                if (sub != null)
                {
                    return sub.GetSubValueI_Path(path.Substring(splitIdx + 1), showWarning);
                }
            }
            else
            {
                foreach (Block sub in Subs)
                {
                    if (sub.Name == path)
                    {
                        return sub.ValueI;
                    }
                }
            }

            //if (showWarning)
            //    GD.Print("sub not found Data : Type - " + Name + " : " + path);

            return 0;
        }

        public Block GetSub(string type, string name)
        {
            foreach (Block sub in Subs)
            {
                if (sub.Name == type && sub.ValueS == name)
                {
                    return sub;
                }
            }
            return null;
        }

        public Block GetLink(string link)
        {
            foreach (Block sub in Subs)
            {
                if (sub.Name.StartsWith(link))
                {
                    return sub;
                }
            }
            return null;
        }

        public List<Block> GetSubs(string type, bool startsWith = false)
        {
            List<Block> ret = new List<Block>();

            if (startsWith)
            {
                foreach (Block sub in Subs)
                {
                    if (sub.Name.StartsWith(type))
                    {
                        ret.Add(sub);
                    }
                }
            }
            else
            {
                foreach (Block sub in Subs)
                {
                    if (sub.Name == type)
                    {
                        ret.Add(sub);
                    }
                }
            }

            return ret;
        }
        public List<Block> GetLinks(string link)
        {
            return GetSubs(link, true);
        }

        public void SetValueI(int value)
        {
            Type = Data._GetDBType("i_" + Name, Data.BaseType.INT);
            ValueI = value;
            ValueS = "";
        }

        public void SetSubValueI(string sub_0, int value)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return;

            dataSub_0.SetValueI(value);
        }

        public void SetSubValueI(string sub_0, string sub_1, int value)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return;

            dataSub_1.SetValueI(value);
        }

        public void SetSubValueI(string sub_0, string sub_1, string sub_2, int value)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return;
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return;

            dataSub_2.SetValueI(value);
        }

        public void SetSubValueI(string sub_0, string sub_1, string sub_2, string sub_3, int value)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return;
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return;
            Block dataSub_3 = dataSub_2.GetSub(sub_3);
            if (dataSub_3 == null) return;

            dataSub_3.SetValueI(value);
        }

        public void SetSubValueI_Path(string path, int value)
        {
            int splitIdx = path.IndexOf("/");
            if (splitIdx > 0)
            {
                Block sub = GetSub(path.Substring(0, splitIdx));
                if (sub != null)
                {
                    sub.SetSubValueI_Path(path.Substring(splitIdx + 1), value);
                }
            }
            else
            {
                if (HasSub(path))
                {
                    GetSub(path).SetValueI(value);
                }
            }
        }

        public void SetValueS(string value)
        {
            Type = Data._GetDBType("s_" + Name, Data.BaseType.STRING);
            ValueI = 0;
            ValueS = value;
        }

        public void SetSubValueS(string sub_0, string value)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return;

            dataSub_0.SetValueS(value);
        }

        public void SetSubValueS(string sub_0, string sub_1, string value)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return;

            dataSub_1.SetValueS(value);
        }

        public void SetSubValueS(string sub_0, string sub_1, string sub_2, string value)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return;
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return;

            dataSub_2.SetValueS(value);
        }

        public void SetSubValueS(string sub_0, string sub_1, string sub_2, string sub_3, string value)
        {
            Block dataSub_0 = GetSub(sub_0);
            if (dataSub_0 == null) return;
            Block dataSub_1 = dataSub_0.GetSub(sub_1);
            if (dataSub_1 == null) return;
            Block dataSub_2 = dataSub_1.GetSub(sub_2);
            if (dataSub_2 == null) return;
            Block dataSub_3 = dataSub_2.GetSub(sub_3);
            if (dataSub_3 == null) return;

            dataSub_3.SetValueS(value);
        }

        public void SetSubValueS_Path(string path, string value)
        {
            int splitIdx = path.IndexOf("/");
            if (splitIdx > 0)
            {
                Block sub = GetSub(path.Substring(0, splitIdx));
                if (sub != null)
                {
                    sub.SetSubValueS_Path(path.Substring(splitIdx + 1), value);
                }
            }
            else
            {
                if (HasSub(path))
                {
                    GetSub(path).SetValueS(value);
                }
            }
        }
    }
}
