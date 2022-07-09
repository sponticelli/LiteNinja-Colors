using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorContrastExtensions
    {
        public static Color AddBrightness(this Color self, float amount)
        {
            return new Color(Mathf.Clamp01(self.r + amount), Mathf.Clamp01(self.g + amount),
                Mathf.Clamp01(self.b + amount), self.a);
        }

        public static Color AddContrast(this Color self, float amount)
        {
            return new Color(Mathf.Clamp01((1f + amount) * (self.r - 0.5f) + 0.5f),
                Mathf.Clamp01((1f + amount) * (self.g - 0.5f) + 0.5f),
                Mathf.Clamp01((1f + amount) * (self.b - 0.5f) + 0.5f),
                self.a);
        }

 
        /// <summary>
        /// Returns the contrast ratio of the color to another color.
        /// </summary>
        public static float ContrastRatio(this Color self, Color color)
        {
            var selfLuminance = self.RelativeLuminance();
            var colorLuminance = color.RelativeLuminance();

            var darker = Mathf.Min(selfLuminance, colorLuminance);
            var lighter = Mathf.Max(selfLuminance, colorLuminance);

            return (lighter + 0.05f) / (darker + 0.05f);
        }
        
        /// <summary>
        /// Returns the color that has the greatest contrast ratio to the current color.
        /// </summary>
        public static Color HigherContrastingColor(this Color self, Color a, Color b)
        {
            var ratio1 = self.ContrastRatio(a);
            var ratio2 = self.ContrastRatio(b);
            return ratio1 > ratio2 ? a : b;
        }

        /// <summary>
        ///   Returns the color from a list that has the greatest contrast ratio to the current color.
        /// </summary>
        public static Color HigherContrastingColor(this Color self,IEnumerable<Color> colors)
        {
            var contrastingColor = colors.First();
            var maxContrastRatio = self.ContrastRatio(contrastingColor);
            
            foreach (var c in colors)
            {
                var contrastRatio = self.ContrastRatio(c);
                if (!(self.ContrastRatio(c) > maxContrastRatio)) continue;
                contrastingColor = c;
                maxContrastRatio = contrastRatio;
            }

            return contrastingColor;
        }
        
        /// <summary>
        /// Returns the color that has the lower contrast ratio to the current color.
        /// </summary>
        public static Color LowerContrastingColor(this Color self, Color a, Color b)
        {
            var contrastRatio1 = self.ContrastRatio(a);
            var contrastRatio2 = self.ContrastRatio(b);
            return contrastRatio1 < contrastRatio2 ? a : b;
        }
        
        /// <summary>
        /// Returns the color from a list that has the lowest constrast ratio to the current color
        /// </summary>
        public static Color LowerContrastingColor(this Color self, IEnumerable<Color> colors)
        {
            var contrastingColor = colors.First();
            var minContrastRatio = self.ContrastRatio(contrastingColor);
            
            foreach (var c in colors)
            {
                var contrastRatio = self.ContrastRatio(c);
                if (!(self.ContrastRatio(c) < minContrastRatio)) continue;
                contrastingColor = c;
                minContrastRatio = contrastRatio;
            }
            
            return contrastingColor;
        }

 
    }
}