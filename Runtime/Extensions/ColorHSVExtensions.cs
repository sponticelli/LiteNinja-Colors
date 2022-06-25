using com.liteninja.colors.spaces;
using UnityEngine;

namespace com.liteninja.colors.extensions
{
    public static class ColorHSVExtensions
    {
        #region Create a new HSV starting from an existing HSV, changing one or more of the HSV values

        /// <summary>
        /// Returns a new color with the hue offset by <paramref name="angle"/> degrees
        /// </summary>
        public static ColorHSV WithOffsetH(this ColorHSV self, float angle)
        {
            return self.WithH(Mathf.Repeat(self.Hue + angle / 360, 1));
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
        
    }
}