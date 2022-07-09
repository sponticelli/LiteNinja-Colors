using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorExtensions
    {
        public static readonly Color transparentBlack = new Color(0f, 0f, 0f, 0f);


        /// <summary>
        /// Returns the nearest websafe color to the current color.
        /// </summary>
        public static Color WebSafe(this Color color)
        {
            const float fiftyOneOver255 = 51f / 255f;
            var r = Mathf.Round(color.r * 5f);
            var g = Mathf.Round(color.g * 5f);
            var b = Mathf.Round(color.b * 5f);

            return new Color(r * fiftyOneOver255, g * fiftyOneOver255, b * fiftyOneOver255);
        }
    }
}