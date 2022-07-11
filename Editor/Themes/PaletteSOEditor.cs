using System.IO;
using LiteNinja.Colors.Extensions;
using LiteNinja.Colors.Themes;
using UnityEditor;
using UnityEngine;


namespace LiteNinja.Colors.Editor.Themes
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PaletteSO))]
    public class PaletteSOEditor : UnityEditor.Editor
    {
        private int _selectedColorIndex = 0;
        private bool _replace;
        private PaletteSO _replacingPalette;

        public override void OnInspectorGUI()
        {
            var palette = (PaletteSO)target;

            ShowPalette(palette);
            AddPalette(palette);
            ReplacePalette(palette);
            SavePalette(palette);
        }

        private void ReplacePalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Replace", EditorStyles.boldLabel);
            if (_replace) {
                // Object Field
                _replacingPalette = (PaletteSO)EditorGUILayout.ObjectField(_replacingPalette, typeof(PaletteSO), false);
                // Confirm
                EditorGUI.BeginDisabledGroup(_replacingPalette == null);
                if (GUILayout.Button("Replace")) {
                    palette.ReplaceFromPalette(_replacingPalette);
                    GameViewRepaint();
                    _replacingPalette = null;
                    //replace = false;
                }
                EditorGUI.EndDisabledGroup();
            }
            // Start & Cancel
            if (GUILayout.Button(_replace ? "Cancel" : "Replace")) {
                _replace = !_replace;
                _replacingPalette = null;
            }
        }

        private void SavePalette(PaletteSO palette)
        {
            // Save
            if (!GUILayout.Button("Save")) return;
            EditorUtility.SetDirty(palette);
            AssetDatabase.SaveAssets();
        }

        private void AddPalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Add", EditorStyles.boldLabel);

            if (!GUILayout.Button("Add Texture")) return;
            
            var path = EditorUtility.OpenFilePanel("Import Texture", "", "png");
            if (string.IsNullOrEmpty(path)) return;
                
            var bytes = File.ReadAllBytes(path);
            var tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            var pixels = tex.GetPixels();
            if (pixels is not { Length: > 0 }) return;

            pixels = pixels.Reduce(256);

            foreach (var t in pixels)
            {
                if (!palette.Contains(t))
                {
                    palette.Add(t);
                }
            }
            

            palette.Invoke();
            GameViewRepaint();
        }

        private void ShowPalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Palette", EditorStyles.boldLabel);

            var startingRect = GUILayoutUtility.GetLastRect();
            if (PaletteColorsDrawer.DrawColorPalette(palette, ref _selectedColorIndex, true))
            {
                Repaint();
            }

            if (palette.Count <= 0) return;

            var selectedColor = palette[_selectedColorIndex];
            var selectedColorRow = _selectedColorIndex / PaletteColorsDrawer.itemsPerRow;
            var selectedColorColumn = selectedColorRow * EditorGUIUtility.singleLineHeight +
                                      EditorGUIUtility.singleLineHeight;
            var changeColorRect = new Rect(
                startingRect.x + PaletteColorsDrawer.itemsPerRow * EditorGUIUtility.singleLineHeight + 30,
                startingRect.y + selectedColorColumn,
                64,
                EditorGUIUtility.singleLineHeight);
            EditorGUI.BeginChangeCheck();
            var newColor = EditorGUI.ColorField(changeColorRect, selectedColor);
            if (EditorGUI.EndChangeCheck())
            {
                palette[_selectedColorIndex] = newColor;
                palette.Invoke();
                GameViewRepaint();
            }

            var x = (int)(changeColorRect.x + changeColorRect.width + 2);
            var y = (int)(changeColorRect.y + changeColorRect.height - EditorGUIUtility.singleLineHeight);

            if (!PaletteColorsDrawer.DrawDeleteButton(x, y)) return;

            palette.RemoveAt(_selectedColorIndex);

            if (_selectedColorIndex >= palette.Count)
            {
                _selectedColorIndex = palette.Count - 1;
                if (_selectedColorIndex < 0)
                {
                    _selectedColorIndex = 0;
                }
            }

            palette.Invoke();
            GameViewRepaint();
        }

        private static EditorWindow _gameView;

        public static void GameViewRepaint()
        {
            if (_gameView == null)
            {
                var assembly = typeof(UnityEditor.EditorWindow).Assembly;
                var type = assembly.GetType("UnityEditor.GameView");
                _gameView = EditorWindow.GetWindow(type);
            }

            if (_gameView != null)
            {
                _gameView.Repaint();
            }
        }
    }
}