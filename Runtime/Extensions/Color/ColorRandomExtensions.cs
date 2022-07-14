using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorRandomExtensions
    {
        /// <summary>
        /// Generates a random tint from the base color. The amount of change in the tint can be constrained between
        /// a minimum and maximum range if desired.
        /// </summary>
        public static Color RandomTint(this Color self, float min = 0f, float max = 1f)
        {
            return self.Tint(Random.Range(min, max));
        }

        /// <summary>
        /// Generates a given amount of random tints from the base color. The amount of change in the tints can be
        /// constrained between a minimum and maximum range if desired.
        /// </summary>
        public static Color[] RandomTints(this Color self, int count, float min = 0f, float max = 1f)
        {
            var tints = new Color[count];
            self.RandomTintsNonAlloc(tints, min, max);
            return tints;
        }

        /// <summary>
        // Fills an existing array with random tints of the base color to prevent heap allocations.
        /// </summary>
        public static void RandomTintsNonAlloc(this Color self, Color[] tints, float min = 0f, float max = 1f)
        {
            for (var i = 0; i < tints.Length; i++)
            {
                tints[i] = self.Tint(Random.Range(min, max));
            }
        }

        /// <summary>
        /// Fills an existing array with random shades of the base color to prevent heap allocations. 
        /// </summary>
        public static void RandomShadesNonAlloc(this Color self, Color[] shades, float min = 0f, float max = 1f)
        {
            for (var i = 0; i < shades.Length; i++)
            {
                shades[i] = self.Shade(Random.Range(min, max));
            }
        }

        /// <summary>
        /// Generates a random shade from the base color. The amount of change in the shade can be constrained between
        /// a minimum and maximum range if desired.
        /// </summary>
        public static Color RandomShade(this Color self, float min = 0f, float max = 1f)
        {
            return self.Shade(Random.Range(min, max));
        }

        /// <summary>
        /// Generates a given amount of random shades from the base color. The amount of change in the shades can be
        /// constrained between a minimum and maximum range if desired.
        /// </summary>
        public static Color[] RandomShades(this Color self, int count, float min = 0f, float max = 1f)
        {
            var shades = new Color[count];
            self.RandomShadesNonAlloc(shades, min, max);
            return shades;
        }

        /// <summary>
        /// Generates a random tone from the base color. The amount of change in the tone can be constrained between
        /// a minimum and maximum range if desired.
        /// </summary>
        public static Color RandomTone(this Color self, float min = 0f, float max = 1f)
        {
            return self.Tone(Random.Range(min, max));
        }

        /// <summary>
        /// Fills an existing array with random tones of the base color to prevent heap allocations. 
        /// </summary>
        public static void RandomTonesNonAlloc(this Color self, Color[] tones, float min = 0f, float max = 1f)
        {
            for (var i = 0; i < tones.Length; i++)
            {
                tones[i] = self.Tone(Random.Range(min, max));
            }
        }

        /// <summary>
        /// Generates a given amount of random tones from the base color. The amount of change in the tones can be
        /// constrained between a minimum and maximum range if desired.
        /// </summary>
        public static Color[] RandomTones(this Color self, int count, float min = 0f, float max = 1f)
        {
            var tones = new Color[count];
            self.RandomTonesNonAlloc(tones, min, max);
            return tones;
        }

        /// <summary>
        /// Fills an existing array with random lighter colors of the base color
        /// to prevent heap allocations.
        /// </summary>
        public static void RandomLightenColorsNonAlloc(this Color self, Color[] lighterColors, float min = 0f,
            float max = 1f)
        {
            for (var i = 0; i < lighterColors.Length; i++)
            {
                lighterColors[i] = self.Lighten(Random.Range(min, max));
            }
        }

        /// <summary>
        /// Generates a given amount of random lighter colors from the base color. The lighten amount  can be
        /// constrained between a minimum and maximum range if desired.
        /// </summary>
        public static Color[] RandomLightenColors(this Color self, int count, float min = 0f, float max = 1f)
        {
            var lighterColors = new Color[count];
            self.RandomLightenColorsNonAlloc(lighterColors, min, max);
            return lighterColors;
        }

        /// <summary>
        /// Generates a random lighter color from the base color. The lighten amount can be constrained between
        /// a minimum and maximum range if desired.
        /// </summary>
        public static Color RandomLightenColor(this Color self, float min = 0f, float max = 1f)
        {
            return self.Lighten(Random.Range(min, max));
        }

        /// <summary>
        /// Fills an existing array with random darker colors of the base color
        /// to prevent heap allocations.
        /// </summary>
        public static void RandomDarkenColorsNonAlloc(this Color self, Color[] darkerColors, float min = 0f,
            float max = 1f)
        {
            for (var i = 0; i < darkerColors.Length; i++)
            {
                darkerColors[i] = self.Darken(Random.Range(min, max));
            }
        }

        /// <summary>
        /// Generates a given amount of random darker colors from the base color. The darken amount  can be
        /// constrained between a minimum and maximum range if desired.
        /// </summary>
        public static Color[] RandomDarkenColors(this Color self, int count, float min = 0f, float max = 1f)
        {
            var darkerColors = new Color[count];
            self.RandomDarkenColorsNonAlloc(darkerColors, min, max);
            return darkerColors;
        }

        /// <summary>
        /// Generates a random darker color from the base color. The darken amount can be constrained between
        /// a minimum and maximum range if desired.
        /// </summary>
        public static Color RandomDarkenColor(this Color self, float min = 0f, float max = 1f)
        {
            return self.Darken(Random.Range(min, max));
        }

        /// <summary>
        /// Fills an existing array with random saturated colors of the base color to prevent heap allocations.
        /// </summary>
        public static void RandomSaturateColorsNonAlloc(this Color self, Color[] saturatedColors, float min = 0f,
            float max = 1f)
        {
            for (var i = 0; i < saturatedColors.Length; i++)
            {
                saturatedColors[i] = self.Saturate(Random.Range(min, max));
            }
        }

        /// <summary>
        /// Generates a given amount of random saturated colors from the base color.
        /// </summary>
        public static Color[] RandomSaturateColors(this Color self, int count, float min = 0f, float max = 1f)
        {
            var saturatedColors = new Color[count];
            self.RandomSaturateColorsNonAlloc(saturatedColors, min, max);
            return saturatedColors;
        }

        /// <summary>
        /// Generates a random saturated color from the base color.
        /// </summary>
        public static Color RandomSaturateColor(this Color self, float min = 0f, float max = 1f)
        {
            return self.Saturate(Random.Range(min, max));
        }

        /// <summary>
        /// Fills an existing array with random desaturated colors of the base color to prevent heap allocations.
        /// </summary>
        public static void RandomDesaturateColorsNonAlloc(this Color self, Color[] desaturatedColors, float min = 0f,
            float max = 1f)
        {
            for (var i = 0; i < desaturatedColors.Length; i++)
            {
                desaturatedColors[i] = self.Desaturate(Random.Range(min, max));
            }
        }

        /// <summary>
        /// Generates a given amount of random desaturated colors from the base color.
        /// </summary>
        public static Color[] RandomDesaturateColors(this Color self, int count, float min = 0f, float max = 1f)
        {
            var desaturatedColors = new Color[count];
            self.RandomDesaturateColorsNonAlloc(desaturatedColors, min, max);
            return desaturatedColors;
        }

        /// <summary>
        /// Generates a random desaturated color from the base color.
        /// </summary>
        public static Color RandomDesaturateColor(this Color self, float min = 0f, float max = 1f)
        {
            return self.Desaturate(Random.Range(min, max));
        }

        /// <summary>
        /// Fills an existing array with random hues of the base color to prevent heap allocations.
        /// </summary>
        public static void RandomHuesNonAlloc(this Color self, Color[] hues, float min = 0f, float max = 1f)
        {
            for (var i = 0; i < hues.Length; i++)
            {
                hues[i] = self.WithHue(Random.Range(min, max));
            }
        }

        /// <summary>
        /// Generates a given amount of random hues from the base color.
        /// </summary>
        public static Color[] RandomHues(this Color self, int count, float min = 0f, float max = 1f)
        {
            var hues = new Color[count];
            self.RandomHuesNonAlloc(hues, min, max);
            return hues;
        }

        /// <summary>
        /// Generates a random hue from the base color.
        /// </summary>
        public static Color RandomHue(this Color self, float min = 0f, float max = 1f)
        {
            return self.WithHue(Random.Range(min, max));
        }

        /// <summary>
        /// Creates a monochromatic harmony of the color, a set of tints and shades formed from the base color.
        /// The colors are stored in an existing array to prevent heap allocations. 
        /// </summary>
        /// <param name="spread">The amount of change from the start to end [0..1] (default=0.5).</param>
        /// <param name="min">The minimum amount of change in the tints and shades [-1..1] (default=-1).</param>
        /// <param name="max">The maximum amount of change in the tints and shades [-1..1] (default=1).</param>
        public static void RandomMonochromaticHarmonyNonAlloc(this Color self, Color[] colors, float spread = 0.5f,
            float min = -1f, float max = 1f)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var percent = Random.Range(min, max) * spread;
                colors[i] = percent switch
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
        public static Color[] RandomMonochromaticHarmony(this Color self, int count, float spread = 0.5f,
            float min = -1f,
            float max = 1f)
        {
            var output = new Color[count];
            self.RandomMonochromaticHarmonyNonAlloc(output, spread, min, max);
            return output;
        }

        /// <summary>
        /// Creates an analogous harmony of the color, a set of colors located next to each other on the color wheel.
        /// The colors are stored in an existing array to prevent heap allocations. 
        /// </summary>
        /// <param name="spread">The amount of hue change from start to end [0...360] (default=60).</param>
        /// <param name="min">The minimum amount of change in the hue spread [-1..1] (default=-1).</param>
        /// <param name="max">The maximum amount of change in the hue spread [-1..1] (default=1).</param>
        public static void RandomAnalogousHarmonyNonAlloc(this Color self, Color[] colors, float spread = 60f,
            float min = -1f,
            float max = 1f)
        {
            var hueMin = spread * 0.5f * min;
            var hueMax = spread * 0.5f * max;

            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = self.HueShiftDegree(Random.Range(hueMin, hueMax));
            }
        }

        /// <summary>
        /// Creates an analogous harmony of the color, a set of colors located next to each other on the color wheel.
        /// </summary>
        public static Color[] RandomAnalogousHarmony(this Color self, int count, float spread = 60f, float min = -1f,
            float max = 1f)
        {
            var output = new Color[count];
            self.RandomAnalogousHarmonyNonAlloc(output, spread, min, max);
            return output;
        }

        /// <summary>
        /// Creates a complementary harmony of the color, a set of colors located next to each other on the color wheel.
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        public static void RandomComplementaryHarmonyNonAlloc(this Color self, Color[] colors, float variance = 0.25f,
            float min = -1f, float max = 1f)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var percent = Random.Range(-1f, 1f) * variance;
                var rng = Random.Range(min, max);
                var color = rng < 0.5f ? self : self.HueShiftDegree(180f);
                colors[i] = percent switch
                {
                    < 0f => color.Shade(-percent),
                    > 0f => color.Tint(percent),
                    _ => color
                };
            }
        }

        /// <summary>
        /// Creates a complementary harmony of the color, a set of colors located next to each other on the color wheel.
        /// </summary>
        public static Color[] RandomComplementaryHarmony(this Color self, int count, float variance = 0.25f,
            float min = -1f, float max = 1f)
        {
            var output = new Color[count];
            self.RandomComplementaryHarmonyNonAlloc(output, variance, min, max);
            return output;
        }

        /// <summary>
        /// Creates a split complementary harmony of the color, the base color with two colors adjacent to the
        /// directly opposing color on the color wheel.
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        public static void RandomSplitComplementaryHarmonyNonAlloc(this Color self, Color[] colors,
            float variance = 0.25f, float min = -1f, float max = 1f)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var percent = Random.Range(-1f, 1f) * variance;
                var rng = Random.Range(min, max);

                var color = rng switch
                {
                    < 1f / 3f => self,
                    < 2f / 3f => self.HueShiftDegree(150f),
                    _ => self.HueShiftDegree(210f)
                };

                colors[i] = percent switch
                {
                    < 0f => color.Shade(-percent),
                    > 0f => color.Tint(percent),
                    _ => color
                };
            }
        }

        /// <summary>
        /// Creates a split complementary harmony of the color, the base color with two colors adjacent to the
        /// directly opposing color on the color wheel.
        /// </summary>
        public static Color[] RandomSplitComplementaryHarmony(this Color self, int count, float variance = 0.25f,
            float min = -1f, float max = 1f)
        {
            var output = new Color[count];
            self.RandomSplitComplementaryHarmonyNonAlloc(output, variance, min, max);
            return output;
        }

        /// <summary>
        /// Creates a double split complementary harmony of the color, two pairs of complementary colors on
        /// either side of the base color.
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        public static void RandomDoubleSplitComplementaryHarmonyNonAlloc(this Color self, Color[] colors,
            float variance = 0.25f, float min = 0f, float max = 1f)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var percent = Random.Range(-1f, 1f) * variance;
                var rng = Random.Range(min, max);

                var color = rng switch
                {
                    < 0.25f => self.HueShiftDegree(-30f),
                    < 0.5f => self.HueShiftDegree(150f),
                    < 0.75f => self.HueShiftDegree(30f),
                    _ => self.HueShiftDegree(210f)
                };

                colors[i] = percent switch
                {
                    < 0f => color.Shade(-percent),
                    > 0f => color.Tint(percent),
                    _ => color
                };
            }
        }

        /// <summary>
        /// Creates a double split complementary harmony of the color, two pairs of complementary colors on
        /// either side of the base color.
        /// </summary>
        public static Color[] RandomDoubleSplitComplementaryHarmony(this Color self, int count, float variance = 0.25f,
            float min = 0f, float max = 1f)
        {
            var output = new Color[count];
            self.RandomDoubleSplitComplementaryHarmonyNonAlloc(output, variance, min, max);
            return output;
        }

        /// <summary>
        /// Creates a triadic harmony of the color, three colors evenly spaced around the color wheel to form a
        /// triangle (120° hue shifts).
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        public static void RandomTriadicHarmonyNonAlloc(this Color self, Color[] colors, float variance = 0.25f,
            float min = 0f, float max = 1f)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var percent = Random.Range(-1f, 1f) * variance;
                var rng = Random.Range(min, max);

                var color = rng switch
                {
                    < 1f / 3f => self,
                    < 2f / 3f => self.HueShiftDegree(120f),
                    _ => self.HueShiftDegree(240f)
                };

                colors[i] = percent switch
                {
                    < 0f => color.Shade(-percent),
                    > 0f => color.Tint(percent),
                    _ => color
                };
            }
        }
        
        /// <summary>
        /// Creates a triadic harmony of the color, three colors evenly spaced around the color wheel to form a
        /// triangle (120° hue shifts).
        /// </summary>
        public static Color[] RandomTriadicHarmony(this Color self, int count, float variance = 0.25f, float min = 0f,
            float max = 1f)
        {
            var output = new Color[count];
            self.RandomTriadicHarmonyNonAlloc(output, variance, min, max);
            return output;
        }
        
        /// <summary>
        /// Creates a tetradic  harmony of the color, four colors evenly spaced around the color wheel to form a
        /// square (90° hue shifts).
        /// The colors are stored in an existing array to prevent heap allocations.
        /// </summary>
        public static void RandomTetradicHarmonyNonAlloc(this Color self, Color[] colors, float variance = 0.25f, float min = 0f, float max = 1f)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var percent = Random.Range(-1f, 1f) * variance;
                var rng = Random.Range(min, max);

                var color = rng switch
                {
                    < 0.25f => self,
                    < 0.5f => self.HueShiftDegree(90f),
                    < 0.75f => self.HueShiftDegree(180f),
                    _ => self.HueShiftDegree(270f)
                };

                colors[i] = percent switch
                {
                    < 0f => color.Shade(-percent),
                    > 0f => color.Tint(percent),
                    _ => color
                };
            }
        }
        
        /// <summary>
        /// Creates a tetradic  harmony of the color, four colors evenly spaced around the color wheel to form a
        /// square (90° hue shifts)
        /// </summary>
        public static Color[] RandomTetradicHarmony(this Color self, int count, float variance = 0.25f, float min = 0f, float max = 1f)
        {
            var output = new Color[count];
            self.RandomTetradicHarmonyNonAlloc(output, variance, min, max);
            return output;
        }
        
    }
}