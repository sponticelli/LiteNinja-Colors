using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorLerpExtensions
    {
        /// <summary>
        /// Lerp a color with an other color, using the specified amount of interpolation.
        /// set mixAlpha to false to ignore the alpha channel.
        /// </summary>
        public static Color Lerp(this Color color, Color to, float amount = 0.5f, bool mixAlpha = true)
        {
            if (mixAlpha)
                return Color.Lerp(color, to, amount);
            return new Color(
                Mathf.Lerp(color.r, to.r, amount),
                Mathf.Lerp(color.g, to.g, amount),
                Mathf.Lerp(color.b, to.b, amount),
                color.a);
        }

        /// <summary>
        /// Returns a tint of the color by mixing it with a percentage of white.
        /// </summary>
        public static Color Tint(this Color color, float amount = 0.1f)
        {
            return color.Lerp(Color.white, amount, false);
        }

        /// <summary>
        /// Generates a given amount of tints from the base color.
        /// </summary>
        public static Color[] Tints(this Color color, int amount = 10)
        {
            var tints = new Color[amount];
            color.TintsNonAlloc(tints);
            return tints;
        }

        /// <summary>
        /// Fills an existing array with tints of the base color to prevent heap allocations.
        /// </summary>
        public static void TintsNonAlloc(this Color color, Color[] tints)
        {
            var step = 1f / tints.Length;
            for (var i = 0; i < tints.Length; i++)
                tints[i] = color.Tint((i + 1) * step);
        }

        /// <summary>
        /// Returns a shade of the color by mixing it with a percentage of black.
        /// </summary>
        public static Color Shade(this Color color, float amount = 0.1f)
        {
            return color.Lerp(Color.black, amount, false);
        }

        /// <summary>
        /// Generates a given amount of shades from the base color.
        /// </summary>
        public static Color[] Shades(this Color color, int amount = 10)
        {
            var shades = new Color[amount];
            color.ShadesNonAlloc(shades);
            return shades;
        }

        /// <summary>
        /// Fills an existing array with shades of the base color to prevent heap allocations.
        /// </summary>
        public static void ShadesNonAlloc(this Color color, Color[] shades)
        {
            var step = 1f / shades.Length;
            for (var i = 0; i < shades.Length; i++)
                shades[i] = color.Shade((i + 1) * step);
        }
        
        /// <summary>
        /// Returns a tone of the color by mixing it with a percentage of gray.
        /// </summary>
        public static Color Tone(this Color color, float amount = 0.1f)
        {
            return color.Lerp(Color.gray, amount, false);
        }
        
        /// <summary>
        /// Generates a given amount of tones from the base color.
        /// </summary>
        public static Color[] Tones(this Color color, int amount = 10)
        {
            var tones = new Color[amount];
            color.TonesNonAlloc(tones);
            return tones;
        }
        
        /// <summary>
        /// Fill an existing array with tones of the base color to prevent heap allocations.
        /// </summary>
        public static void TonesNonAlloc(this Color color, Color[] tones)
        {
            var step = 1f / tones.Length;
            for (var i = 0; i < tones.Length; i++)
                tones[i] = color.Tone((i + 1) * step);
        }
    }
}