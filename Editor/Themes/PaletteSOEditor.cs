using System.IO;
using System.Linq;
using LiteNinja.Colors.Extensions;
using LiteNinja.Colors.Palettes;
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
        private bool _merge;
        private bool _reduce;
        private int _numColorsToReduce ;
        
        private static Texture2D _paletteTexture;

        private PaletteSO _replacingPalette;
        private PaletteSO _mergingPalette;
        
        private const int itemsPerRow = 12;

        public override void OnInspectorGUI()
        {
            var palette = (PaletteSO)target;

            ShowPalette(palette);
            
            AddPalette(palette);
            ReplacePalette(palette);
            MergePalette(palette);
            SortPalette(palette);
            ReduceColors(palette);
            ExportPalette(palette);
            
            //TODO Generate Palette with random color
            //TODO Modify (Tint, Shade, etc)
            
            SavePalette(palette);
        }

        private void ExportPalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Export", EditorStyles.boldLabel);
            if (GUILayout.Button("Export To Color Preset")) {
                PaletteMenu.SavePaletteToColorPreset();
            }
            if (GUILayout.Button("Export To Texture")) {
                PaletteMenu.SavePaletteToTexture();
            }
            EditorGUILayout.Space();
        }

        private void ReduceColors(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Reduce", EditorStyles.boldLabel);
            if (_reduce)
            {
                if (_numColorsToReduce == 0) _numColorsToReduce = Mathf.Max(1, Mathf.RoundToInt( palette.Count *0.5f));
                _numColorsToReduce = EditorGUILayout.IntSlider("Num Colors",
                    _numColorsToReduce, 1, palette.Count);
                if (GUILayout.Button("Reduce"))
                {
                    var colors = palette.GetAll().ToArray();
                    colors = colors.Reduce( 0.02f).Reduce(_numColorsToReduce);
                    palette.Clear();
                    palette.SetAll(colors);
                    palette.Invoke();
                    GameViewRepaint();
                    _reduce = false;
                }
            }


            if (!GUILayout.Button(_reduce ? "Cancel" : "Reduce Colors")) return;
            _reduce = !_reduce;
        }

        private void MergePalette(IPalette palette)
        {
            EditorGUILayout.LabelField("Merge", EditorStyles.boldLabel);
            if (_merge)
            {
                _mergingPalette = (PaletteSO)EditorGUILayout.ObjectField(_mergingPalette, typeof(PaletteSO), false);
                EditorGUI.BeginDisabledGroup(_mergingPalette == null);
                if (GUILayout.Button("Merge"))
                {
                    palette.AddFromPalette(_mergingPalette);
                    GameViewRepaint();
                    _mergingPalette = null;
                    _merge = false;
                }

                EditorGUI.EndDisabledGroup();
            }

            if (!GUILayout.Button(_merge ? "Cancel" : "Merge")) return;

            _mergingPalette = null;
            _merge = !_merge;
        }

        private void SortPalette(PaletteSO palette)
        {
            void Apply(PaletteSO paletteSo, Color[] sorted)
            {
                paletteSo.Clear();
                paletteSo.AddRange(sorted);
                paletteSo.Invoke();
                GameViewRepaint();
            }

            EditorGUILayout.LabelField("Sorting", EditorStyles.boldLabel);

            if (!GUILayout.Button("Sort")) return;

            var colors = palette.GetAll();
            var sorted = colors.ToArray().SortByHSP();
            Apply(palette, sorted);
        }

        private void ReplacePalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Replace", EditorStyles.boldLabel);
            if (_replace)
            {
                // Object Field
                _replacingPalette = (PaletteSO)EditorGUILayout.ObjectField(_replacingPalette, typeof(PaletteSO), false);
                // Confirm
                EditorGUI.BeginDisabledGroup(_replacingPalette == null);
                if (GUILayout.Button("Replace"))
                {
                    palette.ReplaceFromPalette(_replacingPalette);
                    GameViewRepaint();
                    _replacingPalette = null;
                    //replace = false;
                }

                EditorGUI.EndDisabledGroup();
            }

            // Start & Cancel
            if (GUILayout.Button(_replace ? "Cancel" : "Replace"))
            {
                _replace = !_replace;
                _replacingPalette = null;
            }

            if (!GUILayout.Button("Replace with Texture")) return;

            var path = EditorUtility.OpenFilePanelWithFilters("Import Texture", "",
                new[] { "Image files", "png,jpg,jpeg", });
            if (string.IsNullOrEmpty(path)) return;

            var bytes = File.ReadAllBytes(path);
            var tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            var pixels = tex.GetPixels();
            if (pixels is not { Length: > 0 }) return;

            pixels = pixels.Reduce(256);

            palette.Clear();
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

        private static void SavePalette(PaletteSO palette)
        {
            EditorGUILayout.Space();
            // Save
            if (!GUILayout.Button("Save")) return;
            EditorUtility.SetDirty(palette);
            AssetDatabase.SaveAssets();
        }

        private static void AddPalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Add", EditorStyles.boldLabel);

            if (!GUILayout.Button("Add Texture")) return;

            var path = EditorUtility.OpenFilePanelWithFilters("Import Texture", "",
                new[] { "Image files", "png,jpg,jpeg", });
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
            if (ThemeEditorHelper.DrawColorPalette(palette, ref _selectedColorIndex, true, _paletteTexture))
            {
                Repaint();
            }

            if (palette.Count <= 0) return;

            var changeColorRect = DrawSelectedColor(palette, startingRect);

            var x = (int)(changeColorRect.x + changeColorRect.width + 2);
            var y = (int)(changeColorRect.y + changeColorRect.height - EditorGUIUtility.singleLineHeight);

            DrawDeleteButton(palette, x, y);
        }

        private Rect DrawSelectedColor(PaletteSO palette, Rect startingRect)
        {
            var selectedColor = palette[_selectedColorIndex];
            var selectedColorRow = _selectedColorIndex / itemsPerRow;
            var selectedColorColumn = selectedColorRow * EditorGUIUtility.singleLineHeight +
                                      EditorGUIUtility.singleLineHeight;
            var changeColorRect = new Rect(
                startingRect.x + itemsPerRow * EditorGUIUtility.singleLineHeight + 30 +
                EditorGUIUtility.singleLineHeight,
                startingRect.y + selectedColorColumn,
                64,
                EditorGUIUtility.singleLineHeight);
            EditorGUI.BeginChangeCheck();
            var newColor = EditorGUI.ColorField(changeColorRect, selectedColor);
            if (!EditorGUI.EndChangeCheck()) return changeColorRect;
            palette[_selectedColorIndex] = newColor;
            palette.Invoke();
            GameViewRepaint();

            return changeColorRect;
        }

        private void DrawDeleteButton(PaletteSO palette, int x, int y)
        {
            if (!ThemeEditorHelper.DrawDeleteButton(x, y)) return;
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
                var assembly = typeof(EditorWindow).Assembly;
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