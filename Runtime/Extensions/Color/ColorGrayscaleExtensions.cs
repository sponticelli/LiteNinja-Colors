using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorGrayscaleExtensions
    {
        /// <summary>
        /// Sets the saturation of the color to zero.
        /// </summary>
        public static void Grayscale(this ref Color color)
        {
            color.Desaturate(1f);
        }

        /// <summary>
        /// Returns a new instance of the color with zero saturation.
        /// </summary>
        public static Color Grayscaled(this Color color)
        {
            return color.Desaturate(1f);
        }
    }
}