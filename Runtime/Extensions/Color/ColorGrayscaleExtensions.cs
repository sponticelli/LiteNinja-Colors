using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorGrayscaleExtensions
    {
        /// <summary>
        /// Returns a new instance of the color with zero saturation.
        /// </summary>
        public static Color Grayscale(this Color color)
        {
            return color.Desaturate(1f);
        }
    }
}