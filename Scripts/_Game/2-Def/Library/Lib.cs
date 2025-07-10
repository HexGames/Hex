using System.Collections.Generic;
using System.IO;

namespace Def
{
    public static partial class Lib
    {
        public static void xTakeDefsFromDownloads()
        {
            if (Directory.Exists("C:\\Users\\Vlad\\Downloads"))
            {
                string[] allFiles = Directory.GetFiles("C:\\Users\\Vlad\\Downloads");

                List<string> csvFiles = new List<string>();
                for (int idx = 0; idx < allFiles.Length; idx++)
                {
                    if (allFiles[idx].EndsWith(".csv"))
                    {
                        csvFiles.Add(allFiles[idx]);
                    }
                }

                string path = "D:\\___Hex\\Godot\\Defs\\";

                MoveAndRenameFile(SearchForMostRecentFile(csvFiles, "Tiles"), path + "Tiles.table");
                MoveAndRenameFile(SearchForMostRecentFile(csvFiles, "Res"), path + "Res.table");
            }
        }

        public static void xLoadDefs()
        {
            LoadResDef();
            LoadTilesDef();

            SaveResDef();
            SaveTilesDef();
        }

        public static void xInitDefs()
        {
            InitResDefs();
            InitTileDefs();
        }

        // --------------------------------------------------------------------------------------------------- private
        private static string SearchForMostRecentFile(List<string> files, string subName)
        {
            string file = null;

            for (int idx = 0; idx < files.Count; idx++)
            {
                if (files[idx].Contains(subName))
                {
                    if (file == null || File.GetCreationTime(files[idx]) > File.GetCreationTime(file))
                    {
                        file = files[idx];
                    }
                }
            }
            return file;
        }

        private static void MoveAndRenameFile(string originalFile, string destination)
        {
            if (!File.Exists(originalFile))
            {
                Debug.LogError("File " + originalFile + " not found!");
                return;
            }

            if (File.Exists(destination))
            {
                File.Delete(destination);
            }

            File.Copy(originalFile, destination);

            Debug.Log("Moved " + originalFile + " to " + destination);
        }
    }
}