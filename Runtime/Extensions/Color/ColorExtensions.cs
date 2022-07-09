using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorExtensions
    {
        public static readonly Color transparentBlack = new Color(0f, 0f, 0f, 0f);


        public static float RelativeLuminance(this Color self)
        {
            //find linear value for each color channel
            var rLinear = (float)(self.r <= 0.03928 ? self.r / 12.92 : Mathf.Pow((self.r + 0.055f) / 1.055f, 2.4f));
            var gLinear = (float)(self.g <= 0.03928 ? self.g / 12.92 : Mathf.Pow((self.g + 0.055f) / 1.055f, 2.4f));
            var bLinear = (float)(self.b <= 0.03928 ? self.b / 12.92 : Mathf.Pow((self.b + 0.055f) / 1.055f, 2.4f));

            //find relative luminance
            return 0.2126f * rLinear + 0.7152f * gLinear + 0.0722f * bLinear;
        }


        /// <summary>
        /// Returns the nearest websafe color to the current color.
        /// </summary>
        public static Color Websafe(this Color color)
        {
            const float fiftyOneOver255 = 51f / 255f;
            var r = Mathf.Round(color.r * 5f);
            var g = Mathf.Round(color.g * 5f);
            var b = Mathf.Round(color.b * 5f);

            return new Color(r * fiftyOneOver255, g * fiftyOneOver255, b * fiftyOneOver255);
        }
    }

    public static class ColorAveragingExtensions
    {
        public static Color Average(this Color[] self)
        {
            var r = 0f;
            var g = 0f;
            var b = 0f;
            var a = 0f;
            foreach (var color in self)
            {
                r += color.r;
                g += color.g;
                b += color.b;
                a += color.a;
            }

            return new Color(r / self.Length, g / self.Length, b / self.Length, a / self.Length);
        }

        public static Color Average(this Color self, Color other)
        {
            return new Color((self.r + other.r) * 0.5f,
                (self.g + other.g) * 0.5f,
                (self.b + other.b) * 0.5f,
                (self.a + other.a) * 0.5f);
        }
        
        /// <summary>
        /// Returns the result of averaging the RGBA component values of every pixel in the texture.
        /// </summary>
        public static Color AverageColor(this Texture2D texture)
        {
            return Average(texture.GetPixels());
        }
    }
}