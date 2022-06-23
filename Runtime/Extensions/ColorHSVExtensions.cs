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
            return self.WithH(Mathf.Repeat(self.H + angle / 360, 1));
        }

        /// <summary>
        /// Returns a new color with the modified hue component
        /// </summary>
        public static ColorHSV WithH(this ColorHSV self, float h)
        {
            return new ColorHSV(h, self.S, self.V, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified saturation component
        /// </summary>
        public static ColorHSV WithS(this ColorHSV self, float s)
        {
            return new ColorHSV(self.H, s, self.V, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified value component
        /// </summary>
        public static ColorHSV WithV(this ColorHSV self, float v)
        {
            return new ColorHSV(self.H, self.S, v, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified saturation and value components
        /// </summary>
        public static ColorHSV WithSV(this ColorHSV self, float s, float v)
        {
            return new ColorHSV(self.H, s, v, self.Alpha);
        }

        /// <summary>
        /// Returns a new color with the modified alpha component
        /// </summary>
        public static ColorHSV WithA(this ColorHSV self, float a)
        {
            return new ColorHSV(self.H, self.S, self.V, a);
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
            return new ColorHSV(Mathf.Lerp(self.H, other.H, t),
                Mathf.Lerp(self.S, other.S, t),
                Mathf.Lerp(self.V, other.V, t), Mathf.Lerp(self.Alpha, other.Alpha, t));
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
            return new ColorHSV(Mathf.LerpUnclamped(self.H, other.H, t),
                Mathf.LerpUnclamped(self.S, other.S, t),
                Mathf.LerpUnclamped(self.V, other.V, t), Mathf.LerpUnclamped(self.Alpha, other.Alpha, t));
        }

        #endregion
    }
}