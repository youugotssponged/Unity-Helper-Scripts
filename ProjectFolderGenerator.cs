
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

using static System.IO.Directory;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;

public static class ProjectFolderGenerator 
{
    [MenuItem("Tools/Setup/Generate-Default-Project")]
    public static void CreateDefaultProjectFolders()
    {
        string projectRoot = "_Project";
        string[] dirsToGenerate = new string[]
        {
            "Scripts", 
            "Scenes", 
            "Prefabs",
            "Textures",
            "UI", 
            "Materials", 
            "Audio", 
            "Animation", 
            "SaveFiles", 
            "Testing"
        };

        GenerateDirectories(projectRoot, dirsToGenerate);
        Refresh();
    }

    private static void GenerateDirectories(string root, string[] dirsToGenerate)
    {
        string fullPath = Combine(dataPath, root);
        foreach (var dir in dirsToGenerate)
        {
            CreateDirectory(Combine(fullPath, dir));
        }
    }
}

#endif
