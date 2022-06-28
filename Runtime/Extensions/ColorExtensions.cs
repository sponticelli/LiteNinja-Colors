using System.Collections.Generic;
using LiteNinja.Colors.Spaces;
using LiteNinja.Utils.Extensions;
using UnityEngine;

namespace LiteNinja.Colors.extensions
{
    public static class ColorExtensions
    {
        public static readonly Color transparentBlack = new Color(0f, 0f, 0f, 0f);


        #region Create a color changing one parameter at a time

        public static Color WithAlpha(this Color self, float alpha)
        {
            return new Color(self.r, self.g, self.b, alpha);
        }

        public static Color WithAlpha(this Color self, float alpha, float alphaMultiplier)
        {
            return new Color(self.r, self.g, self.b, alpha * alphaMultiplier);
        }

        public static Color WithRed(this Color self, float red)
        {
            return new Color(red, self.g, self.b, self.a);
        }

        public static Color WithGreen(this Color self, float green)
        {
            return new Color(self.r, green, self.b, self.a);
        }

        public static Color WithBlue(this Color self, float blue)
        {
            return new Color(self.r, self.g, blue, self.a);
        }

        public static Color WithHue(this Color self, float hue)
        {
            var hsl = (ColorHSL)self;
            hsl.Hue = hue;
            return hsl;
        }

        public static Color WithSaturation(this Color self, float saturation)
        {
            var hsl = (ColorHSL)self;
            hsl.Saturation = saturation;
            return hsl;
        }

        public static Color WithLightness(this Color self, float lightness)
        {
            var hsl = (ColorHSL)self;
            hsl.Lightness = lightness;
            return hsl;
        }

        public static Color WithValue(this Color self, float value)
        {
            var hsv = (ColorHSV)self;
            hsv.Value = value;
            return hsv;
        }

        public static Color Opaque(this Color color)
        {
            return new Color(color.r, color.g, color.b);
        }

        #endregion

        public static Color Complementary(this Color self)
        {
            ColorHSV colorHsv = self;
            return colorHsv.Complementary();
        }


        public static Color Invert(this Color self)
        {
            return new Color(1f - self.r, 1f - self.g, 1f - self.b, self.a);
        }

        #region Create colors by changing parameters

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

        public static Color AddSaturation(this Color self, float amount)
        {
            ColorHSV colorHsv = self;
            colorHsv.Saturation += amount;
            return colorHsv;
        }

        public static Color AddHue(this Color self, float amount)
        {
            ColorHSV colorHsv = self;
            colorHsv.Hue += amount;
            return colorHsv;
        }

        public static Color AddLightness(this Color self, float amount)
        {
            ColorHSL colorHsl = self;
            colorHsl.Lightness += amount;
            return colorHsl;
        }

        public static Color AddValue(this Color self, float amount)
        {
            ColorHSV colorHsv = self;
            colorHsv.Value += amount;
            return colorHsv;
        }

        public static Color AddAlpha(this Color self, float amount)
        {
            return new Color(self.r, self.g, self.b, Mathf.Clamp01(self.a + amount));
        }

        public static Color AddRed(this Color self, float amount)
        {
            return new Color(Mathf.Clamp01(self.r + amount), self.g, self.b, self.a);
        }

        public static Color AddGreen(this Color self, float amount)
        {
            return new Color(self.r, Mathf.Clamp01(self.g + amount), self.b, self.a);
        }

        public static Color AddBlue(this Color self, float amount)
        {
            return new Color(self.r, self.g, Mathf.Clamp01(self.b + amount), self.a);
        }


        /// <summary>
        /// Balance the color by adding the difference between the max and min color values to each color channel.
        /// </summary>
        /// <param name="self">Color to adjust</param>
        /// <param name="cyanRed">The amount of cyan/red adjustment to apply, -1 (cyan) to 1 (red)</param>
        /// <param name="magentaGreen">The amount of magenta/green adjustment to apply, -1 (magenta) to 1 (green)</param>
        /// <param name="yellowBlue">The amount of yellow/blue adjustment to apply, -1 (yellow) to 1 (blue)</param>
        /// <returns>The modified color</returns>
        public static Color Balance(this Color self, float cyanRed, float magentaGreen, float yellowBlue)
        {
            return new Color(
                Mathf.Clamp01(self.r + cyanRed - (magentaGreen + yellowBlue) * 0.5f),
                Mathf.Clamp01(self.g + magentaGreen - (cyanRed + yellowBlue) * 0.5f),
                Mathf.Clamp01(self.b + yellowBlue - (cyanRed + magentaGreen) * 0.5f),
                self.a);
        }

        #endregion


        #region Color comparison

        public static bool Approximately(this Color value, Color other)
        {
            return Mathf.Approximately(value.r, other.r) &&
                   Mathf.Approximately(value.g, other.g) &&
                   Mathf.Approximately(value.b, other.b) &&
                   Mathf.Approximately(value.a, other.a);
        }

        public static bool Approximately(this Color value, Color other, float epsilon)
        {
            return value.r.Approximately(other.r, epsilon) &&
                   value.g.Approximately(other.g, epsilon) &&
                   value.b.Approximately(other.b, epsilon) &&
                   value.a.Approximately(other.a, epsilon);
        }

        public static bool IsApproximatelyBlack(this Color self)
        {
            return self.r + self.g + self.b <= Mathf.Epsilon;
        }

        public static bool IsApproximatelyWhite(this Color self)
        {
            return self.r + self.g + self.b >= 1 - Mathf.Epsilon;
        }

        #endregion
    }
}