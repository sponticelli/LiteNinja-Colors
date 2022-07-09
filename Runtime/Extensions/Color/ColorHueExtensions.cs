using LiteNinja.Colors.Spaces;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorHueExtensions {

        public static Color HueShift(this Color self, float amount)
        {
            ColorHSV colorHsv = self;
            colorHsv.Hue += amount;
            return colorHsv;
        }

        public static Color HueShiftDegree(this Color self, float degrees)
        {
            var colorHsv = (ColorHSV)self;
            colorHsv.HueDegrees += degrees;
            return colorHsv;
        }
        /// <summary>
        /// Generates hues of the base color with a given increment of degrees.
        /// </summary>
        public static Color[] Hues(this Color self, float degrees = 30f, float degreeOffset = 0f)
        {
            var amount = Mathf.CeilToInt(360f / degrees);
            return Hues(self, amount, degreeOffset);
        }

        /// <summary>
        /// Generates a given amount of hues from the base color.
        /// </summary>
        public static Color[] Hues(this Color self, int amount, float degreeOffset = 0f)
        {
            var colors = new Color[amount];
            HuesNonAlloc(self, colors, degreeOffset);
            return colors;
        }

        /// <summary>
        /// Fills an existing array with the hues of the base color to prevent heap allocations.
        /// </summary>
        public static void HuesNonAlloc(this Color self, Color[] output, float degreeOffset = 0f)
        {
            var degrees = 360f / output.Length;
            for (var i = 0; i < output.Length; i++) {
                output[i] = self.HueShiftDegree(degrees * i + degreeOffset);
            }
        }
    }
}