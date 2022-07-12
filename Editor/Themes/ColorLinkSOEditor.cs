using LiteNinja.Colors.Themes;
using UnityEditor;
using UnityEngine;

namespace LiteNinja.Colors.Editor.Themes
{
    [CustomEditor(typeof(ColorLinkSO))]
    public class ColorLinkSOEditor : UnityEditor.Editor
    {
        private static Color _fallbackColor;
        private static Texture2D _paletteTexture;


        public override void OnInspectorGUI()
        {
            var colorLink = (ColorLinkSO)target;

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
            colorLink.Palette =
                (PaletteSO)EditorGUILayout.ObjectField("Palette", colorLink.Palette, typeof(PaletteSO), false);
            PaletteSOEditor.GameViewRepaint();
        }

        private void LinkedColor(Rect position, ColorLinkSO colorLink)
        {
            var colorIndex = colorLink.ColorIndex;
            EditorGUILayout.LabelField("Linked Color");
            var lastRect = GUILayoutUtility.GetLastRect();
            if (ThemeEditorHelper.DrawColorPalette(colorLink.Palette, ref colorIndex, false,
                    _paletteTexture, 10,
                    EditorGUIUtility.labelWidth))
            {
                colorLink.ColorIndex = colorIndex;
                PaletteSOEditor.GameViewRepaint();
            }

            //Draw a rect for the color
            var colorRect = new Rect(lastRect.x, lastRect.y + EditorGUIUtility.singleLineHeight,
                EditorGUIUtility.labelWidth -20f, EditorGUIUtility.singleLineHeight);
            EditorGUI.DrawRect(colorRect, colorLink.Color);
        }


        private static void FallbackColor(ColorLinkSO colorLink)
        {
            colorLink.FallbackColor = EditorGUILayout.ColorField("Fallback Color: ", colorLink.FallbackColor);
            PaletteSOEditor.GameViewRepaint();
        }
    }
}