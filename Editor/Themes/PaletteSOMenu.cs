using System.IO;
using System.Linq;
using LiteNinja.Colors.Extensions;
using LiteNinja.Colors.Themes;
using UnityEditor;
using UnityEngine;

namespace LiteNinja.Colors.Editor.Themes
{
    public static class PaletteSOMenu
    {
        #region Menu strings
        private const string _menuPath = "LiteNinja/Colors/Themes/";
        private const string _savePaletteToTexture = _menuPath + "Save Palette To Texture";
        private const string _createPaletteFromTexture = _menuPath + "Create Palette from Texture";
        private const string _savePaletteToSwatch = _menuPath + "Save Palette To Swatch";
        private const string _duplicatePalette = _menuPath + "Duplicate Palette";
        #endregion        
        
        #region Texture
        [MenuItem(_createPaletteFromTexture)]
        private static void CreatePaletteFromTexture()
        {
            var path = EditorUtility.OpenFilePanelWithFilters("Import Texture", "",
                new[] { "Image files", "png,jpg,jpeg", });
            if (string.IsNullOrEmpty(path)) return;
            
            var bytes = File.ReadAllBytes(path);
            var tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            var colors = tex.GetPixels();
            if (colors is not { Length: > 0 }) return;
            colors = colors.Reduce(256);
            
            var palette = ScriptableObject.CreateInstance<PaletteSO>();
            palette.AddRange(colors);
            ProjectWindowUtil.CreateAsset(palette,  "PaletteSO.asset");
            AssetDatabase.SaveAssets();
        }
        
        
        [MenuItem(_savePaletteToTexture, true)]
        private static bool SavePaletteToTextureValidate()
        {
            return SelectionIsPaletteSO();
        }
        
        [MenuItem(_savePaletteToTexture)]
        public static void SavePaletteToTexture()
        {
            var palette = (PaletteSO)Selection.activeObject;
            var assetLocation = GetSelectedPath() + "/" + GetSelectedFileName() + ".png";
            var saveLocation = ConvertAssetPathToFullPath(assetLocation);
            palette.SaveToTexture(saveLocation);
            AssetDatabase.ImportAsset(assetLocation);
        }
        #endregion

        #region Swatch
        [MenuItem(_savePaletteToSwatch, true)]
        public static bool SavePaletteToSwatchValidate()
        {
            return SelectionIsPaletteSO();
        }

        [MenuItem(_savePaletteToSwatch)]
        public static void SavePaletteToSwatch()
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
        
        #endregion
        
        #region Duplicate

        [MenuItem(_duplicatePalette, true)]
        private static bool DuplicatePaletteValidate()
        {
            return SelectionIsPaletteSO();
        }

        [MenuItem(_duplicatePalette)]
        private static void DuplicatePalette()
        {
            var palette = (PaletteSO)Selection.activeObject;
            var newPalette = ScriptableObject.CreateInstance<PaletteSO>();
            newPalette.AddFromPalette(palette);
            ProjectWindowUtil.CreateAsset(newPalette, palette.name + ".asset");
            AssetDatabase.SaveAssets();
        }
        #endregion

        #region Private utilities
        private static bool SelectionIsPaletteSO()
        {
            return Selection.activeObject != null && Selection.activeObject is PaletteSO;
        }
        private static string ConvertAssetPathToFullPath(string assetPath)
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
        #endregion
    }
}