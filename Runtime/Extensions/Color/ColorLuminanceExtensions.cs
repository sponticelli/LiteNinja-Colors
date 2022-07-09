using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorLuminanceExtensions
    {
        public static float RelativeLuminance(this Color self)
        {
            var linear = self.linear;

            //find relative luminance
            return 0.2126f * linear.r + 0.7152f * linear.g + 0.0722f * linear.b;
        }

        /// <summary>
        /// Calculates the perceived brightness of the color.
        /// 
        /// </summary>
        public static float PerceivedBrightness(this Color self)
        {
            var linear = self.linear;
            var r = 0.299f * Mathf.Pow(linear.r, 2f);
            var g = 0.587f * Mathf.Pow(linear.g, 2f);
            var b = 0.114f * Mathf.Pow(linear.b, 2f);
            return Mathf.Sqrt(r + g + b);
        }
    }
}