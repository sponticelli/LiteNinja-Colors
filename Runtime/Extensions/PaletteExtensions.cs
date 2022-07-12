using LiteNinja.Colors.Palettes;
using UnityEngine;
using UnityEngine.Windows;

namespace LiteNinja.Colors.Extensions
{
    public static class PaletteExtensions
    {
        public static void SaveToTexture(this IPalette self, string fullSaveLocation)
        {
            var texture = self.Texture;
            var pngData = texture.EncodeToPNG();
            File.WriteAllBytes(fullSaveLocation, pngData);
        }
    }
}