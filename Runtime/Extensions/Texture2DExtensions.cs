using System.Collections.Generic;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class Texture2DExtensions
    {
        public static Color AverageColor(this Texture2D self)
        {
            return self.GetPixels().Average();
        }


        public static Color[] GetColors(this Texture2D self)
        {
            var colors = new List<Color>();
            for (var x = 0; x < self.width; x++)
            {
                for (var y = 0; y < self.height; y++)
                {
                    var color = self.GetPixel(x, y);
                    if (colors.Contains(color)) continue;
                    colors.Add(self.GetPixel(x, y));
                }
            }

            return colors.ToArray();
        }

        /// <summary>
        /// Reduce the number of colors by removing the least used colors.
        /// </summary>
        public static Color[] ReduceColors(this Texture2D self, int maxColors)
        {
            return self.GetColors().Reduce(maxColors);
        }

        /// <summary>
        /// Reduce the number of colors by merging together the most similar colors.
        /// </summary>
        public static Color[] ReduceColors(this Texture2D self,  float threshold)
        {
            var colors = self.GetColors();
            return colors.Reduce(threshold);
        }
    }
}