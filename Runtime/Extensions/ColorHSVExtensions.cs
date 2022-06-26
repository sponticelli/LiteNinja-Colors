using LiteNinja.Colors.Spaces;
using LiteNinja.Utils.Extensions;
using UnityEngine;

namespace LiteNinja.Colors.extensions
{
    public static class ColorHSVExtensions
    {
        
        public static ColorHSV Complementary(this ColorHSV self)
        {
            return self.Analogous(0.5f);
        }
        
        public static ColorHSV Analogous(this ColorHSV self, float offset = 0.03f)
        {
            var newH = (self.Hue + offset) % 1f;
            return new ColorHSV(newH, self.Saturation, self.Value, self.Alpha);
        }
        
        public static ColorHSV Triadic(this ColorHSV self)
        {
            return self.Analogous(0.33f);
        }
        
        public static ColorHSV Tetradic(this ColorHSV self)
        {
            return self.Analogous(0.25f);
        }
        
        public static ColorHSV Invert(this ColorHSV self)
        {
            return ((Color)self).Invert();
        }
        
        public static ColorHSV Balance(this ColorHSV self, float cyanRed, float magentaGreen, float yellowBlue)
        {
            return ((Color)self).Balance(cyanRed, magentaGreen, yellowBlue);
        }
        
        #region Create a new HSV color changing some parameters
        public static ColorHSV AddBrightness(this ColorHSV self, float amount)
        {
            return ((Color)self).AddBrightness(amount);
        }
        
        public static ColorHSV AddContrast(this ColorHSV self, float amount)
        {
            return ((Color)self).AddContrast(amount);
        }
        
        public static ColorHSV AddHue(this ColorHSV self, float amount)
        {
            return self.WithH(self.Hue + amount);
        }
        
        public static ColorHSV AddSaturation(this ColorHSV self, float amount)
        {
            return self.WithS(self.Saturation + amount);
        }
        
        public static ColorHSV AddValue(this ColorHSV self, float amount)
        {
            return self.WithV(self.Value + amount);
        }
        
        public static ColorHSV AddAlpha(this ColorHSV self, float amount)
        {
            return self.WithA(self.Alpha + amount);
        }
        
        public static ColorHSV AddLightness(this ColorHSV self, float amount)
        {
            return ((Color)self).AddLightness(amount);
        }
        
        
        #endregion
        
        #region Create a new HSV starting from an existing HSV, changing one or more of the HSV values

        /// <summary>
        /// Returns a new color with the hue offset by <paramref name="angle"/> degrees
        /// </summary>
        public static ColorHSV WithOffsetH(this ColorHSV self, float angle)
        {
            return self.WithH(Mathf.Repeat(self.HueDegrees + angle, 1));
        }

        /// <summary>
        /// Returns a new color with the modified hue component
        /// </summary>
        public static ColorHSV WithH(this ColorHSV self, float h)
        {
            return new ColorHSV(h, self.Saturation, self.Value, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified saturation component
        /// </summary>
        public static ColorHSV WithS(this ColorHSV self, float s)
        {
            return new ColorHSV(self.Hue, s, self.Value, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified value component
        /// </summary>
        public static ColorHSV WithV(this ColorHSV self, float v)
        {
            return new ColorHSV(self.Hue, self.Saturation, v, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified saturation and value components
        /// </summary>
        public static ColorHSV WithSV(this ColorHSV self, float s, float v)
        {
            return new ColorHSV(self.Hue, s, v, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified alpha component
        /// </summary>
        public static ColorHSV WithA(this ColorHSV self, float a)
        {
            return new ColorHSV(self.Hue, self.Saturation, self.Value, a);
        }
        #endregion

        #region Lerp between two colors
        /// <summary>
        /// Lerp between two colors, using the hue, saturation,value  and alpha components
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        /// <param name="t"></param>
        /// <returns>a new HSV color</returns>
        public static ColorHSV Lerp(this ColorHSV self, ColorHSV other, float t)
        {
            return new ColorHSV(Mathf.Lerp(self.Hue, other.Hue, t),
                Mathf.Lerp(self.Saturation, other.Saturation, t),
                Mathf.Lerp(self.Value, other.Value, t), Mathf.Lerp(self.Alpha, other.Alpha, t));
        }
        
        /// <summary>
        /// Unclamped lerp between two colors, using the hue, saturation,value  and alpha components
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ColorHSV LerpUnclamped(this ColorHSV self, ColorHSV other, float t)
        {
            return new ColorHSV(Mathf.LerpUnclamped(self.Hue, other.Hue, t),
                Mathf.LerpUnclamped(self.Saturation, other.Saturation, t),
                Mathf.LerpUnclamped(self.Value, other.Value, t), Mathf.LerpUnclamped(self.Alpha, other.Alpha, t));
        }

        #endregion

        #region Comparing colors
        public static bool Approximately(this ColorHSV self, ColorHSV other)
        {
            return Mathf.Approximately(self.Hue, other.Hue) &&
                   Mathf.Approximately(self.Saturation, other.Saturation) &&
                   Mathf.Approximately(self.Value, other.Value) &&
                   Mathf.Approximately(self.Alpha, other.Alpha);
        }
        
        public static bool Approximately(this ColorHSV self, ColorHSV other, float epsilon)
        {
            return self.Hue.Approximately(other.Hue, epsilon) &&
                   Mathf.Approximately(self.Saturation, other.Saturation) &&
                   Mathf.Approximately(self.Value, other.Value) &&
                   Mathf.Approximately(self.Alpha, other.Alpha);
        }

        #endregion
    }
}