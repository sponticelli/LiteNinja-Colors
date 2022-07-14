using LiteNinja.Colors.Themes;
using UnityEditor;
using UnityEngine;

namespace LiteNinja.Colors.Editor.Themes
{
    [CustomEditor(typeof(ColorLinkSO))]
    public class ColorLinkSOEditor : UnityEditor.Editor
    {
        private  Color _fallbackColor;
        private  Texture2D _paletteTexture;
        private  PaletteSO _paletteSO;


        public override void OnInspectorGUI()
        {
            var colorLink = (ColorLinkSO)target;
            _paletteSO = colorLink.Palette;
            _fallbackColor = colorLink.FallbackColor;

            EditorGUILayout.LabelField("Color Link", EditorStyles.boldLabel);

            PaletteSOField(colorLink);

            var position = GUILayoutUtility.GetLastRect();

            //Draw the current color
            LinkedColor(position, colorLink);

            //Draw the fallback color
            FallbackColor(colorLink);
        }

        private void PaletteSOField(ColorLinkSO colorLink)
        {
            _paletteSO =
                (PaletteSO)EditorGUILayout.ObjectField("Palette", _paletteSO, typeof(PaletteSO), false);
            colorLink.Palette = _paletteSO;
            PaletteSOEditor.GameViewRepaint();
            EditorUtility.SetDirty(colorLink);
        }

        private void LinkedColor(Rect position, ColorLinkSO colorLink)
        {
            var colorIndex = colorLink.ColorIndex;
            EditorGUILayout.LabelField("Linked Color");
            //draw color name, if it is not null or empty
            if (!string.IsNullOrEmpty(colorLink.ColorName))
            {
                EditorGUILayout.LabelField(colorLink.ColorName);
            }
            var lastRect = GUILayoutUtility.GetLastRect();
            if (PaletteDrawingHelper.DrawColorPalette(colorLink.Palette, ref colorIndex, false,
                    _paletteTexture, 10,
                    EditorGUIUtility.labelWidth))
            {
                colorLink.ColorIndex = colorIndex;
                PaletteSOEditor.GameViewRepaint();
            }

            DrawPreviewColor(colorLink, lastRect);
            EditorUtility.SetDirty(colorLink);
        }

        private void DrawPreviewColor(ColorLinkSO colorLink, Rect lastRect)
        {
            //Draw a rect for the color
            var colorRect = new Rect(lastRect.x, lastRect.y + EditorGUIUtility.singleLineHeight,
                EditorGUIUtility.labelWidth - 20f, EditorGUIUtility.singleLineHeight);
            EditorGUI.DrawRect(colorRect, colorLink.Color);
        }


        private void FallbackColor(ColorLinkSO colorLink)
        {
            colorLink.FallbackColor = EditorGUILayout.ColorField("Fallback Color: ", colorLink.FallbackColor);
            PaletteSOEditor.GameViewRepaint();
            EditorUtility.SetDirty(colorLink);
        }
    }
}