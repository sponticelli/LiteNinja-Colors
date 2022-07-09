using System.Collections.Generic;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorAveragingExtensions
    {
        public static Color Average(this IEnumerable<Color> self)
        {
            var r = 0f;
            var g = 0f;
            var b = 0f;
            var a = 0f;
            var length = 0;
            foreach (var color in self)
            {
                r += color.r;
                g += color.g;
                b += color.b;
                a += color.a;
                length++;
            }

            return new Color(r / length, g / length, b / length, a / length);
        }

        public static Color Average(this Color self, Color other)
        {
            return new Color((self.r + other.r) * 0.5f,
                (self.g + other.g) * 0.5f,
                (self.b + other.b) * 0.5f,
                (self.a + other.a) * 0.5f);
        }

        
    }
}