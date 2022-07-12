using System;
using LiteNinja.Colors.Spaces;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorSortingExtensions
    {
        public static Color[] SortByHue(this Color[] colors)
        {
            var sorted = new Color[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                sorted[i] = colors[i];
            }

            Array.Sort(sorted, (a, b) => ((ColorHSL)a).Hue.CompareTo(((ColorHSL)b).Hue));
            return sorted;
        }

        public static Color[] SortBySaturation(this Color[] colors)
        {
            var sorted = new Color[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                sorted[i] = colors[i];
            }

            Array.Sort(sorted, (a, b) => ((ColorHSL)a).Saturation.CompareTo(((ColorHSL)b).Saturation));
            return sorted;
        }

        public static Color[] SortByLightness(this Color[] colors)
        {
            var sorted = new Color[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                sorted[i] = colors[i];
            }

            Array.Sort(sorted, (a, b) => ((ColorHSL)a).Lightness.CompareTo(((ColorHSL)b).Lightness));
            return sorted;
        }

        public static Color[] SortByRelativeLuminance(this Color[] colors)
        {
            var sorted = new Color[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                sorted[i] = colors[i];
            }

            Array.Sort(sorted, (a, b) => a.RelativeLuminance().CompareTo(b.RelativeLuminance()));
            return sorted;
        }

        public static Color[] SortByContrast(this Color[] colors)
        {
            var sorted = new Color[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                sorted[i] = colors[i];
            }

            Array.Sort(sorted, (a, b) =>
            {
                var c1 = 0.299f * a.r + 0.587f * a.g + 0.114f * a.b;
                var c2 = 0.299f * b.r + 0.587f * b.g + 0.114f * b.b;
                return c1.CompareTo(c2);
            });
            return sorted;
        }
        
        public static Color[] SortByHSP(this Color[] colors)
        {
            var sorted = new Color[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                sorted[i] = colors[i];
            }
            
            Array.Sort(sorted, (a, b) =>
            {
                var hsp1 = 0.299 * a.r*a.r + 0.587 * a.g*a.g + 0.114 * a.b*a.b;
                var hsp2 = 0.299 * b.r*b.r + 0.587 * b.g*b.g + 0.114 * b.b*b.b;
                return hsp1.CompareTo(hsp2);
            });

            
            return sorted;
        }
        
        public static Color[] SortByHSL(this Color[] colors)
        {
            var sorted = new Color[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                sorted[i] = colors[i];
            }
            
            Array.Sort(sorted, (a, b) =>
            {
                ColorHSL hsl1 = a;
                ColorHSL hsl2 = b;
                var value1 = hsl1.Hue + 2*hsl1.Saturation + 5*hsl1.Lightness;
                var value2 = hsl2.Hue + 2*hsl2.Saturation + 5*hsl2.Lightness;
                return value1.CompareTo(value2);
            });
            
            return sorted;
        }
    }
}