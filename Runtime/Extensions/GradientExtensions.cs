using LiteNinja.Colors.Palettes;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class GradientExtensions
    {
        public static Color GetColor(this Gradient self, float value)
        {
            var color = self.Evaluate(value);
            return color;
        }

        /// <summary>
        /// Creates a new gradient and sets the given color keys and alpha keys.
        /// </summary>
        public static Gradient ToGradient(this GradientColorKey[] colors, GradientAlphaKey[] alpha)
        {
            var gradient = new Gradient();
            gradient.SetKeys(colors, alpha);
            return gradient;
        }

        /// <summary>
        /// Creates a new gradient and sets the given color keys with a constant alpha value for the entire gradient.
        /// </summary>
        public static Gradient ToGradient(this GradientColorKey[] colors, float alpha = 1f)
        {
            var gradient = new Gradient();
            gradient.SetKeys(colors,
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0f), new GradientAlphaKey(alpha, 1f) });
            return gradient;
        }

        /// <summary>
        /// Creates a new gradient with a given start color and end color.
        /// </summary>
        public static Gradient ToGradient(this Color start, Color end)
        {
            var gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(start, 0f), new GradientColorKey(end, 1f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
            return gradient;
        }

        /// <summary>
        /// Creates a new gradient that interpolates a linear amount of stops between a start color and end color.
        /// </summary>
        public static Gradient ToGradient(this Color start, Color end, int stops)
        {
            stops = Mathf.Clamp(stops + 2, 0,
                8); // Clamp stops to a maximum of 8 stops (Unity's maximum gradient key count)
            var gradient = new Gradient();
            var colors = new GradientColorKey[stops];
            var alpha = new GradientAlphaKey[stops];

            for (var i = 0; i < stops; i++)
            {
                var t = (float)i / (stops - 1);
                colors[i] = new GradientColorKey(Color.Lerp(start, end, t), t);
                alpha[i] = new GradientAlphaKey(Mathf.Lerp(start.a, end.a, t), t);
            }

            gradient.SetKeys(colors, alpha);
            return gradient;
        }

        /// <summary>
        /// Creates a new gradient that distributes a predefined array of stops between a start color and end color.
        /// </summary>
        public static Gradient ToGradient(this Color start, Color end, float[] stops)
        {
            var length =
                Mathf.Clamp(stops.Length + 2, 0,
                    8); // Clamp stops to a maximum of 8 stops (Unity's maximum gradient key count)
            var gradient = new Gradient();
            var colors = new GradientColorKey[length];
            var alpha = new GradientAlphaKey[length];

            colors[0] = new GradientColorKey(start, 0f);
            alpha[0] = new GradientAlphaKey(start.a, 0f);
            colors[length - 1] = new GradientColorKey(end, 1f);
            alpha[length - 1] = new GradientAlphaKey(end.a, 1f);

            for (var i = 1; i < length - 1; i++)
            {
                var t = stops[i];
                colors[i] = new GradientColorKey(Color.Lerp(start, end, t), t);
                alpha[i] = new GradientAlphaKey(Mathf.Lerp(start.a, end.a, t), t);
            }

            gradient.SetKeys(colors, alpha);
            return gradient;
        }

        /// <summary>
        /// Creates a new gradient with the given colors evenly distributed throughout.
        /// </summary>
        public static Gradient ToGradient(this Color[] colors)
        {
            var length =
                Mathf.Clamp(colors.Length, 0,
                    8); // Clamp stops to a maximum of 8 stops (Unity's maximum gradient key count)
            var gradient = new Gradient();
            var colorKeys = new GradientColorKey[length];
            var alphaKeys = new GradientAlphaKey[length];

            for (var i = 0; i < length; i++)
            {
                var t = (float)i / (length - 1);
                colorKeys[i] = new GradientColorKey(colors[i], t);
                alphaKeys[i] = new GradientAlphaKey(colors[i].a, t);
            }

            gradient.SetKeys(colorKeys, alphaKeys);
            return gradient;
        }

        /// <summary>
        /// Creates a new gradient with the given colors evenly distributed throughout starting at the given color.
        /// </summary>
        public static Gradient ToGradient(this Color start, Color[] colors)
        {
            var length =
                Mathf.Clamp(colors.Length + 1, 0,
                    8); // Clamp stops to a maximum of 8 stops (Unity's maximum gradient key count)
            var gradient = new Gradient();
            var colorKeys = new GradientColorKey[length];
            var alphaKeys = new GradientAlphaKey[length];
            colorKeys[0] = new GradientColorKey(start, 0f);
            alphaKeys[0] = new GradientAlphaKey(start.a, 0f);

            for (var i = 1; i < length; i++)
            {
                var t = (float)i / (length - 1);
                colorKeys[i] = new GradientColorKey(Color.Lerp(start, colors[i], t), t);
                alphaKeys[i] = new GradientAlphaKey(colors[i].a, t);
            }

            gradient.SetKeys(colorKeys, alphaKeys);
            return gradient;
        }

        /// <summary>
        /// Creates a new gradient with the given colors distributed with a predefined array of stops.
        /// </summary>
        public static Gradient ToGradient(this Color[] colors, float[] stops)
        {
            var length =
                Mathf.Clamp(stops.Length, 0,
                    8); // Clamp stops to a maximum of 8 stops (Unity's maximum gradient key count)
            var gradient = new Gradient();
            var colorKeys = new GradientColorKey[length];
            var alphaKeys = new GradientAlphaKey[length];

            for (var i = 0; i < length; i++)
            {
                var t = stops[i];
                colorKeys[i] = new GradientColorKey(Color.Lerp(colors[0], colors[length - 1], t), t);
                alphaKeys[i] = new GradientAlphaKey(Mathf.Lerp(colors[0].a, colors[length - 1].a, t), t);
            }

            gradient.SetKeys(colorKeys, alphaKeys);
            return gradient;
        }

        ///<summary>
        /// Creates a Palette from a given Gradient and a given number of colors.
        /// </summary>
        public static IPalette ToPalette(this Gradient gradient, int numColors)
        {
            var palette = new Palette();
            var step = 1f / (numColors - 1);
            for (var i = 0; i < numColors; i++)
            {
                var t = (float)i / (numColors - 1);
                palette.Add(gradient.Evaluate(t));
            }
            return palette;
        }
        
        ///<summary>
        /// Creates a Palette from a given Gradient and a given number of colors without allocation.
        /// </summary>
        public static IPalette ToPalette(this Gradient gradient, int numColors, IPalette palette)
        {
            var step = 1f / (numColors - 1);
            for (var i = 0; i < numColors; i++)
            {
                var t = (float)i / (numColors - 1);
                palette.Add(gradient.Evaluate(t));
            }
            return palette;
        }
        
        ///<summary>
        /// Creates a Palette from a given Gradient using the keys of the gradient.
        /// </summary>
        public static IPalette ToPalette(this Gradient gradient)
        {
            var palette = new Palette();
            var colorKeys = gradient.colorKeys;
            var alphaKeys = gradient.alphaKeys;
            for (var i = 0; i < colorKeys.Length; i++)
            {
                palette.Add(colorKeys[i].color.WithAlpha(alphaKeys[i].alpha));
            }
            return palette;
        }
        
        
        
    }
}