using LiteNinja.Colors.Spaces;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorLightnessExtensions
    {

        public static float GetLightness(this Color self)
        {
            return ((ColorHSL) self).Lightness;
        }
        

        /// <summary>
        /// Returns a new instance of the color with increased lightness.
        /// </summary>
        public static Color Lighten(this Color color, float increase = 0.1f)
        {
            ColorHSL hsl = color;
            hsl.Lightness += increase;
            return hsl;
        }

        /// <summary>
        /// Generates a given amount of lighter colors from the base color.
        /// </summary>
        public static Color[] Lighten(this Color baseColor, int numColors)
        {
            var range = 1f - baseColor.GetLightness();
            var delta = range / Mathf.Max(numColors - 1, 1);
            var colors = new Color[numColors];
            for (var i = 0; i < numColors; i++)
            {
                colors[i] = baseColor.Lighten(i * delta);
            }
            return colors;
        }

        /// <summary>
        /// Fills an existing array with lighter colors of the base color to prevent heap allocations.
        /// </summary>
        public static void LightenNonAlloc(this Color baseColor, Color[] output)
        {
            var range = 1f - baseColor.GetLightness();
            var delta = range / Mathf.Max(output.Length - 1, 1);
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = baseColor.Lighten(i * delta);
            }
        }
        

        /// <summary>
        /// Returns a new instance of the color with decreased lightness.
        /// </summary>
        public static Color Darken(this Color color, float decrease = 0.1f)
        {
            ColorHSL hsl = color;
            hsl.Lightness -= decrease;
            return hsl;
        }

        /// <summary>
        /// Generates a given amount of darker colors from the base color.
        /// </summary>
        public static Color[] Darken(this Color baseColor, int numColors)
        {
            var range = baseColor.GetLightness();
            var delta = range / Mathf.Max(numColors - 1, 1);
            var colors = new Color[numColors];
            for (var i = 0; i < numColors; i++)
            {
                colors[i] = baseColor.Darken(i * delta);
            }
            return colors;
        }

        /// <summary>
        /// Fills an existing array with darker colors of the base color to prevent heap allocations. T
        /// </summary>
        public static void DarkerNonAlloc(this Color baseColor, Color[] output)
        {
            var range = baseColor.GetLightness();
            var delta = range / Mathf.Max(output.Length - 1, 1);
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = baseColor.Darken(i * delta);
            }
        }

    }
}