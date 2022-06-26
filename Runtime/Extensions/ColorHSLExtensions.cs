using LiteNinja.Colors.Spaces;
using LiteNinja.Utils.Extensions;
using UnityEngine;

namespace LiteNinja.Colors.extensions
{
    public static class ColorHSLExtensions
    {
        
        public static ColorHSL Complementary(this ColorHSL self)
        {
            return self.Analogous(0.5f);
        }
        
        public static ColorHSL Analogous(this ColorHSL self, float offset = 0.03f)
        {
            var newH = (self.Hue + offset) % 1f;
            return new ColorHSL(newH, self.Saturation, self.Lightness, self.Alpha);
        }
        
        public static ColorHSL Triadic(this ColorHSL self)
        {
            return self.Analogous(0.33f);
        }
        
        public static ColorHSL Tetradic(this ColorHSL self)
        {
            return self.Analogous(0.25f);
        }
        
        public static ColorHSL Invert(this ColorHSL self)
        {
            return ((Color)self).Invert();
        }
        
        #region Create a new HSL starting from an existing HSL, changing one or more of the HSL values

        /// <summary>
        /// Returns a new color with the hue offset by <paramref name="angle"/> degrees
        /// </summary>
        public static ColorHSL WithOffsetH(this ColorHSL self, float angle)
        {
            return self.WithH(Mathf.Repeat(self.Hue + angle / 360, 1));
        }

        /// <summary>
        /// Returns a new color with the modified hue component
        /// </summary>
        public static ColorHSL WithH(this ColorHSL self, float h)
        {
            return new ColorHSL(h, self.Saturation, self.Lightness, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified saturation component
        /// </summary>
        public static ColorHSL WithS(this ColorHSL self, float s)
        {
            return new ColorHSL(self.Hue, s, self.Lightness, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified value component
        /// </summary>
        public static ColorHSL WithL(this ColorHSL self, float l)
        {
            return new ColorHSL(self.Hue, self.Saturation, l, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified saturation and value components
        /// </summary>
        public static ColorHSL WitHSL(this ColorHSL self, float s, float l)
        {
            return new ColorHSL(self.Hue, s, l, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified alpha component
        /// </summary>
        public static ColorHSL WithA(this ColorHSL self, float a)
        {
            return new ColorHSL(self.Hue, self.Saturation, self.Lightness, a);
        }
        #endregion

        #region Lerp between two colors
        /// <summary>
        /// Lerp between two colors, using the hue, saturation,value  and alpha components
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        /// <param name="t"></param>
        /// <returns>a new HSL color</returns>
        public static ColorHSL Lerp(this ColorHSL self, ColorHSL other, float t)
        {
            return new ColorHSL(Mathf.Lerp(self.Hue, other.Hue, t),
                Mathf.Lerp(self.Saturation, other.Saturation, t),
                Mathf.Lerp(self.Lightness, other.Lightness, t), Mathf.Lerp(self.Alpha, other.Alpha, t));
        }
        
        /// <summary>
        /// Unclamped lerp between two colors, using the hue, saturation,value  and alpha components
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ColorHSL LerpUnclamped(this ColorHSL self, ColorHSL other, float t)
        {
            return new ColorHSL(Mathf.LerpUnclamped(self.Hue, other.Hue, t),
                Mathf.LerpUnclamped(self.Saturation, other.Saturation, t),
                Mathf.LerpUnclamped(self.Lightness, other.Lightness, t), Mathf.LerpUnclamped(self.Alpha, other.Alpha, t));
        }

        #endregion
        
        #region Comparing colors
        public static bool Approximately(this ColorHSL self, ColorHSL other)
        {
            return Mathf.Approximately(self.Hue, other.Hue) &&
                   Mathf.Approximately(self.Saturation, other.Saturation) &&
                   Mathf.Approximately(self.Lightness, other.Lightness) &&
                   Mathf.Approximately(self.Alpha, other.Alpha);
        }
        
        public static bool Approximately(this ColorHSL self, ColorHSL other, float epsilon)
        {
            return self.Hue.Approximately(other.Hue, epsilon) &&
                   Mathf.Approximately(self.Saturation, other.Saturation) &&
                   Mathf.Approximately(self.Lightness, other.Lightness) &&
                   Mathf.Approximately(self.Alpha, other.Alpha);
        }

        #endregion
    }
}