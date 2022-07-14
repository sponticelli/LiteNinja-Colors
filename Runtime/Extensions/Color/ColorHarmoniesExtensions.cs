using System.Collections.Generic;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorHarmoniesExtensions
    {
        /// <summary>
        /// Creates a monochromatic harmony of the color, a set of tints and
        /// shades formed from the base color. The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        /// <param name="spread">The amount of change from start to end [0..1] (default=0.5).</param>
        public static void MonochromaticHarmonyNonAlloc(this Color self, Color[] colors, float spread = 0.5f)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var percent = (float)i / (colors.Length - 1) * 2f - 1f;
                percent *= spread;
                colors[i] = percent switch
                {
                    < 0f => self.Shade(-percent),
                    > 0f => self.Tint(percent),
                    _ => self
                };
            }
        }

        private static void MonochromaticHarmonyNonAlloc(this Color self, IList<Color> colors, int amount,
            int startIndex, float spread)
        {
            for (var i = 0; i < amount; i++)
            {
                var percent = (float)i / (float)(amount - 1) * 2f - 1f;
                percent *= spread;

                colors[startIndex + i] = percent switch
                {
                    < 0f => self.Shade(-percent),
                    > 0f => self.Tint(percent),
                    _ => self
                };
            }
        }


        /// <summary>
        /// Creates a monochromatic harmony of the color, a set of tints and shades formed from the base color.
        /// </summary>
        public static IEnumerable<Color> MonochromaticHarmony(this Color color, int count)
        {
            var colors = new Color[count];
            color.MonochromaticHarmonyNonAlloc(colors);
            return colors;
        }


        /// <summary>
        /// Creates an analogous harmony of the color, a set of colors located next to each other on the color wheel.
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        /// <param name="spread">The amount of hue change from start to end [0...360] (default=60).</param>
        public static void AnalogousHarmonyNonAlloc(this Color self, Color[] colors, float spread = 60f)
        {
            var hue = -spread * 0.5f;
            var change = spread / (colors.Length - 1);

            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = self.HueShiftDegree(hue);
                hue += change;
            }
        }

        /// <summary>
        /// Creates an analogous harmony of the color, a set of colors located next to each other on the color wheel.
        /// </summary>
        public static IEnumerable<Color> AnalogousHarmony(this Color color, int count)
        {
            var colors = new Color[count];
            color.AnalogousHarmonyNonAlloc(colors);
            return colors;
        }

        /// <summary>
        /// Creates a complementary harmony of the color, two colors located opposite to each other on the color wheel.
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        /// <param name="variance">The amount of variance in tints and shades when generating more than 2 colors [0..1] (default=0.25).</param>
        public static void ComplementaryHarmonyNonAlloc(this Color self, Color[] colors, float variance = 0.25f)
        {
            var analogousCount = Mathf.CeilToInt((float)colors.Length / 2f);
            var complementaryCount = colors.Length - analogousCount;

            self.MonochromaticHarmonyNonAlloc(colors, analogousCount, 0, variance);
            self.HueShiftDegree(180f)
                .MonochromaticHarmonyNonAlloc(colors, complementaryCount, analogousCount, variance);
        }

        /// <summary>
        /// Creates a complementary harmony of the color, two colors located opposite to each other on the color wheel.
        /// </summary>
        public static IEnumerable<Color> ComplementaryHarmony(this Color color, int count)
        {
            var colors = new Color[count];
            color.ComplementaryHarmonyNonAlloc(colors);
            return colors;
        }

        /// <summary>
        /// Creates a split complementary harmony of the color, the base color with two colors adjacent to the directly
        /// opposing color on the color wheel.
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        /// <param name="variance">The amount of variance in tints and shades when generating more than 3 colors [0..1] (default=0.25).</param>
        public static void SplitComplementaryHarmonyNonAlloc(this Color self, Color[] colors, float variance = 0.25f)
        {
            var amount1 = Mathf.CeilToInt((float)colors.Length / 3f);
            var amount2 = Mathf.CeilToInt((float)(colors.Length - amount1) * 0.5f);
            var amount3 = colors.Length - amount1 - amount2;

            self.MonochromaticHarmonyNonAlloc(colors, amount1, 0, variance);
            self.HueShiftDegree(150f).MonochromaticHarmonyNonAlloc(colors, amount2, amount1, variance);
            self.HueShiftDegree(210f).MonochromaticHarmonyNonAlloc(colors, amount3, amount1 + amount2, variance);
        }

        /// <summary>
        /// Creates a split complementary harmony of the color, the base color with two colors adjacent to the directly
        /// opposing color on the color wheel.
        /// </summary>
        public static IEnumerable<Color> SplitComplementaryHarmony(this Color self, int count)
        {
            var colors = new Color[count];
            self.SplitComplementaryHarmonyNonAlloc(colors);
            return colors;
        }

        /// <summary>
        /// Creates a double split complementary harmony of the color, two pairs of complementary colors on either
        /// side of the base color.
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        /// <param name="variance">The amount of variance in tints and shades when generating more than 4 colors [0..1] (default=0.25).</param>
        public static void DoubleSplitComplementaryHarmonyNonAlloc(this Color self, Color[] colors,
            float variance = 0.25f)
        {
            var amount1 = Mathf.CeilToInt((float)colors.Length / 4f);
            var amount2 = Mathf.CeilToInt((float)(colors.Length - amount1) / 3f);
            var amount3 = Mathf.CeilToInt((float)(colors.Length - amount1 - amount2) / 2f);
            var amount4 = colors.Length - amount1 - amount2 - amount3;

            self.HueShiftDegree(-30f)
                .MonochromaticHarmonyNonAlloc(colors, amount1, 0, variance);
            self.HueShiftDegree(150f)
                .MonochromaticHarmonyNonAlloc(colors, amount2, amount1, variance);
            self.HueShiftDegree(30f)
                .MonochromaticHarmonyNonAlloc(colors, amount3, amount1 + amount2, variance);
            self.HueShiftDegree(210f)
                .MonochromaticHarmonyNonAlloc(colors, amount4, amount1 + amount2 + amount3, variance);
        }
        
        /// <summary>
        /// Creates a double split complementary harmony of the color, two pairs of complementary colors on either
        /// side of the base color.
        /// </summary>
        public static IEnumerable<Color> DoubleSplitComplementaryHarmony(this Color self, int count)
        {
            var colors = new Color[count];
            self.DoubleSplitComplementaryHarmonyNonAlloc(colors);
            return colors;
        }
        
        /// <summary>
        /// Creates a triadic harmony of the color, three colors evenly spaced around the color wheel to form a
        /// triangle (120° hue shifts).
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        /// <param name="variance">The amount of variance in tints and shades when generating more than 3 colors [0..1] (default=0.25).</param>
        
        public static void TriadicHarmonyNonAlloc(this Color self, Color[] colors, float variance = 0.25f)
        {
            var amount1 = Mathf.CeilToInt((float)colors.Length / 3f);
            var amount2 = Mathf.CeilToInt((float)(colors.Length - amount1) * 0.5f);
            var amount3 = colors.Length - amount1 - amount2;

            self.MonochromaticHarmonyNonAlloc(colors, amount1, 0, variance);
            self.HueShiftDegree(120f).MonochromaticHarmonyNonAlloc(colors, amount2, amount1, variance);
            self.HueShiftDegree(240f).MonochromaticHarmonyNonAlloc(colors, amount3, amount1 + amount2, variance);
        }
        
        /// <summary>
        /// Creates a triadic harmony of the color, three colors evenly spaced around the color wheel to form a
        /// triangle (120° hue shifts).
        /// </summary>
        public static IEnumerable<Color> TriadicHarmony(this Color self, int count)
        {
            var colors = new Color[count];
            self.TriadicHarmonyNonAlloc(colors);
            return colors;
        }
        
        /// <summary>
        /// Creates a tetradic harmony of the color, four colors evenly spaced around the color wheel
        /// to form a square (90° hue shifts).
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        /// <param name="variance">The amount of variance in tints and shades when generating more than 4 colors [0..1] (default=0.25).</param>
        public static void TetradicHarmonyNonAlloc(this Color self, Color[] colors, float variance = 0.25f)
        {
            var amount1 = Mathf.CeilToInt((float)colors.Length / 4f);
            var amount2 = Mathf.CeilToInt((float)(colors.Length - amount1) / 3f);
            var amount3 = Mathf.CeilToInt((float)(colors.Length - amount1 - amount2) / 2f);
            var amount4 = colors.Length - amount1 - amount2 - amount3;

            self.HueShiftDegree(90f).MonochromaticHarmonyNonAlloc(colors, amount1, 0, variance);
            self.HueShiftDegree(180f).MonochromaticHarmonyNonAlloc(colors, amount2, amount1, variance);
            self.HueShiftDegree(270f).MonochromaticHarmonyNonAlloc(colors, amount3, amount1 + amount2, variance);
            self.HueShiftDegree(360f).MonochromaticHarmonyNonAlloc(colors, amount4, amount1 + amount2 + amount3, variance);
        }
        
        /// <summary>
        /// Creates a tetradic harmony of the color, four colors evenly spaced around the color wheel
        /// to form a square (90° hue shifts).
        /// </summary>
        public static IEnumerable<Color> TetradicHarmony(this Color self, int count)
        {
            var colors = new Color[count];
            self.TetradicHarmonyNonAlloc(colors);
            return colors;
        }
        
    }
}