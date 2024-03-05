using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class FUIProcessor : AssetPostprocessor
{
    private static string path = "Assets/GameArt/FUI";
    private static string atlasDst = "Assets/GameArt/FUI_atlas";
    private static string bytesDst = "Assets/GameArt/FUI_bytes";

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (var asset in importedAssets)
        {
            if(!asset.StartsWith(path + "/") || AssetDatabase.IsValidFolder(asset))
            {
                continue;
            }

            if(asset.EndsWith(".png"))
            {
                string dstPath = atlasDst + "/" + Path.GetFileName(asset);
                AssetDatabase.DeleteAsset(dstPath);
                AssetDatabase.MoveAsset(asset, dstPath);
            }
            else if(asset.EndsWith(".bytes"))
            {
                string dstPath = bytesDst + "/" + Path.GetFileName(asset);
                AssetDatabase.DeleteAsset(dstPath);
                AssetDatabase.MoveAsset(asset, dstPath);
            }
        }
    }

    [MenuItem("MKTools/ForceMoveFUIAsset", priority = 10)]
    private static void ForceMoveFUIAsset()
    {
        List<string> paths = GetResPaths();
        foreach (var asset in paths)
        {
            if (AssetDatabase.IsValidFolder(asset))
            {
                continue;
            }


            if (asset.EndsWith(".png"))
            {
                string dstPath = atlasDst + "/" + Path.GetFileName(asset);
                AssetDatabase.DeleteAsset(dstPath);
                AssetDatabase.MoveAsset(asset, dstPath);
            }
            else if (asset.EndsWith(".bytes"))
            {
                string dstPath = bytesDst + "/" + Path.GetFileName(asset);
                AssetDatabase.DeleteAsset(dstPath);
                AssetDatabase.MoveAsset(asset, dstPath);
            }
        }

        foreach (var asset in paths)
        {
            if (AssetDatabase.IsValidFolder(asset))
            {
                AssetDatabase.DeleteAsset(asset);
            }
        }

        AssetDatabase.Refresh();
    }

    private static List<string> GetResPaths()
    {
        string[] guids = AssetDatabase.FindAssets("", new[] { path });
        List<string> paths = new List<string>();
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            paths.Add(assetPath);
        }

        return paths;
    }
}
