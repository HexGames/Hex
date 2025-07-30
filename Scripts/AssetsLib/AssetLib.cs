using Godot;
using Godot.Collections;
using System.Linq;

[Tool]
public partial class AssetLib : Node
{
    [Export]
    private Dictionary<string, PackedScene> PrefabTiles = new Dictionary<string, PackedScene>();
    [Export]
    private Dictionary<string, Texture2D> TexturesIcons = new Dictionary<string, Texture2D>();
    //[Export]
    //private Dictionary<string, Material> Materials = new Dictionary<string, Material>();

    [ExportCategory("Loader")]
    [Export]
    public bool LoadAllAssets
    { 
        get => false;
        set
        {
            if (value)
            {
                LoadAll();
            }
        }
    }

    public override void _Ready()
    {
        //LoadAll();
    }

    private void LoadAll()
    {
        LoadPackedScenes(PrefabTiles, "res://Assets/Map/Prefabs/");
        Load2DTextures(TexturesIcons, "res://Assets/Icons/");
        //LoadMaterials(Materials, "res://Assets//3D/Materials/");
    }

    private void Load2DTextures(Dictionary<string, Texture2D> textures, string dirPath)
    {
        textures.Clear();
        string[] filePaths = DirAccess.GetFilesAt(dirPath);
        for (int idx = 0; idx < filePaths.Length; idx++)
        {
            string path = filePaths[idx].Replace(".remap", "");
            if (path.EndsWith(".png"))
            {
                textures.Add(path, GD.Load<Texture2D>(dirPath + path));
            }
        }
    }

    public Texture2D GetTexture2D_Icons(string path)
    {
        string file = path + ".png";
        if (TexturesIcons.ContainsKey(file) == false)
        {
            GD.PrintErr("Unable to find icon texture2D " + file);
            return null;
        }
        return TexturesIcons[file];
    }

    private void LoadPackedScenes(Dictionary<string, PackedScene> prefabs, string dirPath)
    {
        prefabs.Clear();
        string[] filePaths = DirAccess.GetFilesAt(dirPath);
        for (int idx = 0; idx < filePaths.Length; idx++)
        {
            string path = filePaths[idx].Replace(".remap", "");
            if (path.EndsWith(".tscn"))
            {
                prefabs.Add(path, GD.Load<PackedScene>(dirPath + path));
            }
        }
    }
    public PackedScene GetPrefab_Tiles(string path)
    {
        string file = path + ".tscn";
        if (PrefabTiles.ContainsKey(file) == false)
        {
            GD.PrintErr("Unable to find tile texture2D " + file);
            return null;
        }
        return PrefabTiles[file];
    }


    //private void LoadMaterials(Dictionary<string, Material> materials, string dirPath)
    //{
    //    materials.Clear();
    //    string[] filePaths = DirAccess.GetFilesAt(dirPath);
    //    for (int idx = 0; idx < filePaths.Length; idx++)
    //    {
    //        if (filePaths[idx].EndsWith("tres"))
    //        {
    //            materials.Add(filePaths[idx], GD.Load<Material>(dirPath + filePaths[idx]));
    //        }
    //    }
    //}

    //public Material GetMaterial(string path)
    //{
    //    if (Materials.ContainsKey(path) == false)
    //    {
    //        GD.Print("Unable to find material " + path);
    //        return null;
    //    }
    //    return Materials[path];
    //}

    //public void LODPlanetsShow()
    //{
    //    foreach (var item in MaterialsPlanets)
    //    {
    //        Material mat = item.Value;
    //        if (mat is StandardMaterial3D stdMat)
    //        {
    //            stdMat.AlbedoColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    //        }
    //    }
    //}
    //
    //public void LODPlanetsHide()
    //{
    //    foreach (var item in MaterialsPlanets)
    //    {
    //        Material mat = item.Value;
    //        if (mat is StandardMaterial3D stdMat)
    //        {
    //            stdMat.AlbedoColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    //        }
    //    }
    //}
}
