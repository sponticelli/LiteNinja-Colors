using System.IO;
using System.Linq;
using LiteNinja.Colors.Extensions;
using LiteNinja.Colors.Themes;
using UnityEditor;
using UnityEngine;

namespace LiteNinja.Colors.Editor.Themes
{
    public static class PaletteMenu
    {
        [MenuItem("LiteNinja/Colors/Themes/Save Palette To Texture")]
        public static void SavePaletteToTexture()
        {
            if (Selection.activeObject == null)
            {
                Debug.LogError("No palette selected");
                return;
            }

            //Check if the selected object is a palette
            if (Selection.activeObject is not PaletteSO)
            {
                Debug.LogError("Selected object is not a palette");
                return;
            }

            var palette = (PaletteSO)Selection.activeObject;
            var assetLocation = GetSelectedPath() + "/" + GetSelectedFileName() + ".png";
            var saveLocation = ConvertAssetPathToFullPath(assetLocation);
            palette.SaveToTexture(saveLocation);
            AssetDatabase.ImportAsset(assetLocation);
        }

        [MenuItem("LiteNinja/Colors/Themes/Save Palette To Color Preset Library", true)]
        public static void SavePaletteToColorPreset()
        {
            if (Selection.activeObject == null)
            {
                Debug.LogError("No palette selected");
                return;
            }

            //Check if the selected object is a palette
            if (Selection.activeObject is not PaletteSO)
            {
                Debug.LogError("Selected object is not a palette");
                return;
            }

            var palette = (PaletteSO)Selection.activeObject;
            var projectPath = AssetDatabase.GetAssetPath(palette.GetInstanceID());
            var paletteDirectory = Path.GetDirectoryName(projectPath);
            var libraryDirectory = paletteDirectory + "/Editor";
            if (!AssetDatabase.IsValidFolder(libraryDirectory))
            {
                AssetDatabase.CreateFolder(paletteDirectory, "Editor");
            }

            var filePath = libraryDirectory + "/" + palette.name + ".colors";
            var fullFilePath = filePath.Replace("Assets", Application.dataPath);
            var colors = palette.GetAll();
            var fileText = colors.Aggregate(
                $"%YAML 1.1\n%TAG !u! tag:unity3d.com,2011:\n--- !u!114 &1\nMonoBehaviour:\n  m_ObjectHideFlags: 52\n" +
                "  m_PrefabParentObject: {fileID: 0}\n  m_PrefabInternal: {fileID: 0}\n  m_GameObject: {fileID: 0}\n  " +
                "m_Enabled: 1\n  m_EditorHideFlags: 1\n  m_Script: {fileID: 12323, guid: 0000000000000000e000000000000000, type: 0}\n  " +
                $"m_Name: {palette.name}\n  m_EditorClassIdentifier: \n  m_Presets:",
                (current, color) =>
                    current +
                    $"\n  - m_Name: \n    m_Color: {{r: {color.r}, g: {color.g}, b: {color.b}, a: {color.a}}}");
            
            File.WriteAllText(fullFilePath, fileText);
            AssetDatabase.ImportAsset(filePath);
        }

        public static string ConvertAssetPathToFullPath(string assetPath)
        {
            var fullPath = Application.dataPath + assetPath.Substring("Assets".Length);
            return fullPath;
        }

        private static string GetSelectedFileName()
        {
            return Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        private static string GetSelectedPath()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(path), "");
            }

            return path;
        }
    }
}