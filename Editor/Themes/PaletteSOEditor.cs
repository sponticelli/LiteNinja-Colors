using System;
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
        private bool _generate;
        private bool _modify;

        private static Texture2D _paletteTexture;

        private const int _itemsPerRow = 12;
        private static EditorWindow _gameView;

        private int _numColorsToReduce;
        private PaletteSO _replacingPalette;
        private PaletteSO _mergingPalette;
        private Color _seedColor = Color.white;
        private int _numColors = 16;
        private bool _replaceColors;


        public override void OnInspectorGUI()
        {
            var palette = (PaletteSO)target;

            ShowPalette(palette);

            AddPalette(palette);
            ReplacePalette(palette);
            MergePalette(palette);
            GenerateHarmonies(palette);
            ModifyPalette(palette);
            SortPalette(palette);
            ReduceColors(palette);
            ExportPalette();

            //TODO Modify (Tint, Shade, etc)

            SavePalette(palette);
        }

        private void ModifyPalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Modify", EditorStyles.boldLabel);
            if (_modify)
            {
                Saturate(palette);
                Desaturate(palette);
                EditorGUILayout.Space();
                Lighten(palette);
                Darken(palette);
                EditorGUILayout.Space();
                Invert(palette);
                EditorGUILayout.Space();
                Tint(palette);
                Shade(palette);
                Tone(palette);
                EditorGUILayout.Space();
            }

            if (!GUILayout.Button(_modify ? "Cancel" : "Modify")) return;
            _modify = !_modify;
        }

        private static void Tone(PaletteSO palette)
        {
            Modify(palette, "Tone", color => color.Tone());
        }

        private static void Shade(PaletteSO palette)
        {
            Modify(palette, "Shade", color => color.Shade());
        }

        private static void Tint(PaletteSO palette)
        {
            Modify(palette, "Tint", color => color.Tint());
        }

        private static void Invert(PaletteSO palette)
        {
            Modify(palette, "Invert", color => color.Invert());
        }

        private static void Darken(PaletteSO palette)
        {
            Modify(palette, "Darken", color => color.Darken());
        }

        private static void Lighten(PaletteSO palette)
        {
            Modify(palette, "Lighten", color => color.Lighten());
        }

        private static void Desaturate(PaletteSO palette)
        {
            Modify(palette, "Desaturate", color => color.Desaturate());
        }

        private static void Saturate(PaletteSO palette)
        {
            Modify(palette, "Saturate", color => color.Saturate());
        }

        private static void Modify(PaletteSO palette, string buttonLabel, Func<Color, Color> operation)
        {
            if (!GUILayout.Button(buttonLabel)) return;
            for (var i = 0; i < palette.Count; i++)
            {
                palette[i] = operation(palette[i]);
            };
            palette.Invoke();
            GameViewRepaint();
        }

        private void GenerateHarmonies(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Harmonies", EditorStyles.boldLabel);
            if (_generate)
            {
                //Draw a color picker for the color to generate
                _seedColor = EditorGUILayout.ColorField("Color", _seedColor);
                //Draw a slider for the number of colors to generate
                _numColors = EditorGUILayout.IntSlider("Num Colors", _numColors, 1, 256);
                //Draw a checkbox for whether to replace colors or not
                _replaceColors = EditorGUILayout.Toggle("Replace Colors", _replaceColors);

                //Add a space between the buttons
                EditorGUILayout.Space();

                MonochromaticHarmony(palette);
                AnalogousHarmony(palette);
                ComplementaryHarmony(palette);
                SplitComplementaryHarmony(palette);
                DoubleSplitComplementaryHarmony(palette);
                TriadicHarmony(palette);
                TetradicHarmony(palette);
            }

            if (!GUILayout.Button(_generate ? "Cancel" : "Harmonies")) return;
            _generate = !_generate;
        }

        private void TetradicHarmony(PaletteSO palette)
        {
            if (!GUILayout.Button("Tetradic Harmony")) return;
            var colors = _seedColor.RandomTetradicHarmony(_numColors);
            UpdatePaletteWithHarmony(palette, colors);
        }

        private void TriadicHarmony(PaletteSO palette)
        {
            if (!GUILayout.Button("Triadic Harmony")) return;
            var colors = _seedColor.RandomTriadicHarmony(_numColors);
            UpdatePaletteWithHarmony(palette, colors);
        }

        private void DoubleSplitComplementaryHarmony(PaletteSO palette)
        {
            if (!GUILayout.Button("Double Split Complementary Harmony")) return;
            var colors = _seedColor.RandomDoubleSplitComplementaryHarmony(_numColors);
            UpdatePaletteWithHarmony(palette, colors);
        }

        private void SplitComplementaryHarmony(PaletteSO palette)
        {
            if (!GUILayout.Button("Split Complementary Harmony")) return;
            var colors = _seedColor.RandomSplitComplementaryHarmony(_numColors);
            UpdatePaletteWithHarmony(palette, colors);
        }

        private void ComplementaryHarmony(PaletteSO palette)
        {
            if (!GUILayout.Button("Complementary Harmony")) return;
            var colors = _seedColor.RandomComplementaryHarmony(_numColors);
            UpdatePaletteWithHarmony(palette, colors);
        }

        private void AnalogousHarmony(PaletteSO palette)
        {
            if (!GUILayout.Button("Analogous Harmony")) return;
            var colors = _seedColor.RandomAnalogousHarmony(_numColors);
            UpdatePaletteWithHarmony(palette, colors);
        }

        private void MonochromaticHarmony(PaletteSO palette)
        {
            //Add a button to generate the monochromatic harmony
            if (!GUILayout.Button("Monochromatic Harmony")) return;
            var colors = _seedColor.RandomMonochromaticHarmony(_numColors);
            UpdatePaletteWithHarmony(palette, colors);
        }

        private void UpdatePaletteWithHarmony(PaletteSO palette, Color[] colors)
        {
            if (_replaceColors)
            {
                palette.Clear();
            }

            palette.AddRange(colors.SortByHSP());
            palette.Invoke();
            GameViewRepaint();
        }

        private static void ExportPalette()
        {
            EditorGUILayout.LabelField("Export", EditorStyles.boldLabel);
            if (GUILayout.Button("Export To Color Preset"))
            {
                PaletteMenu.SavePaletteToColorPreset();
            }

            if (GUILayout.Button("Export To Texture"))
            {
                PaletteMenu.SavePaletteToTexture();
            }

            EditorGUILayout.Space();
        }

        private void ReduceColors(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Reduce", EditorStyles.boldLabel);
            if (_reduce)
            {
                if (_numColorsToReduce == 0) _numColorsToReduce = Mathf.Max(1, Mathf.RoundToInt(palette.Count * 0.5f));
                _numColorsToReduce = EditorGUILayout.IntSlider("Num Colors",
                    _numColorsToReduce, 1, palette.Count);
                if (GUILayout.Button("Reduce"))
                {
                    var colors = palette.GetAll().ToArray();
                    colors = colors.Reduce(0.02f).Reduce(_numColorsToReduce);
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

        private static void SortPalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Sorting", EditorStyles.boldLabel);

            if (!GUILayout.Button("Sort")) return;

            var colors = palette.GetAll();
            var sorted = colors.ToArray().SortByHSP();
            palette.Clear();
            palette.AddRange(sorted);
            palette.Invoke();
            GameViewRepaint();
        }

        private void ReplacePalette(PaletteSO palette)
        {
            EditorGUILayout.LabelField("Replace", EditorStyles.boldLabel);
            if (_replace)
            {
                _replacingPalette = (PaletteSO)EditorGUILayout.ObjectField(_replacingPalette, typeof(PaletteSO), false);
                EditorGUI.BeginDisabledGroup(_replacingPalette == null);
                if (GUILayout.Button("Replace"))
                {
                    palette.ReplaceFromPalette(_replacingPalette);
                    GameViewRepaint();
                    _replacingPalette = null;
                }

                EditorGUI.EndDisabledGroup();
            }

            if (GUILayout.Button(_replace ? "Cancel" : "Replace"))
            {
                _replace = !_replace;
                _replacingPalette = null;
            }

            if (!GUILayout.Button("Replace with Texture")) return;

            var path = EditorUtility.OpenFilePanelWithFilters("Import Texture", "",
                new[] { "Image files", "png,jpg,jpeg", });
            if (string.IsNullOrEmpty(path)) return;

            ReplacePaletteWithTextureFile(palette, path);
        }

        private static void ReplacePaletteWithTextureFile(PaletteSO palette, string textureFullPath)
        {
            var bytes = File.ReadAllBytes(textureFullPath);
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
            if (PaletteDrawingHelper.DrawColorPalette(palette, ref _selectedColorIndex, true, _paletteTexture))
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
            var selectedColorRow = _selectedColorIndex / _itemsPerRow;
            var selectedColorColumn = selectedColorRow * EditorGUIUtility.singleLineHeight +
                                      EditorGUIUtility.singleLineHeight;
            var changeColorRect = new Rect(
                startingRect.x + _itemsPerRow * EditorGUIUtility.singleLineHeight + 30 +
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
            if (!PaletteDrawingHelper.DrawDeleteButton(x, y)) return;
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