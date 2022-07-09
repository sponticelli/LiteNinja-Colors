using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorReducingExtensions
    {
        /// <summary>
        /// Reduce the number of colors by removing the least used colors.
        /// </summary>
        public static Color[] Reduce(this IEnumerable<Color> self, int maxColors)
        {
            var colorDictionary = new Dictionary<Color, int>();
            foreach (var color in self)
            {
                if (colorDictionary.ContainsKey(color))
                {
                    colorDictionary[color]++;
                }
                else
                {
                    colorDictionary.Add(color, 1);
                }
            }

            //sort colorDictionary by value
            var sortedColorDictionary = colorDictionary.ToDictionary(pair => pair.Key, pair => pair.Value);

            //get the top maxColors colors
            return sortedColorDictionary.OrderByDescending(pair => pair.Value).Take(maxColors)
                .ToDictionary(pair => pair.Key, pair => pair.Value)
                .Keys.ToArray();
        }
        
        /// <summary>
        /// Reduce the number of colors by merging together the most similar colors.
        /// </summary>
        public static Color[] Reduce(this IEnumerable<Color> self, float threshold)
        {
            var colorDictionary = new Dictionary<Color, int>();
            foreach (var color in self)
            {
                if (colorDictionary.ContainsKey(color))
                {
                    colorDictionary[color]++;
                }
                else
                {
                    colorDictionary.Add(color, 1);
                }
            }

            //sort colorDictionary by value
            var sortedColorDictionary = colorDictionary.ToDictionary(pair => pair.Key, pair => pair.Value);

            //starting from the top, merge together colors that are similar
            var mergedColors = new Dictionary<Color, int>();
            var alreadyMerged = new HashSet<Color>();
            foreach (var (color, count) in sortedColorDictionary.OrderByDescending(pair => pair.Value))
            {
                if (alreadyMerged.Contains(color)) continue;
                if (mergedColors.ContainsKey(color))
                {
                    mergedColors[color] += count;
                }
                else
                {
                    mergedColors.Add(color, count);
                }


                //find all the colors that are similar to this color
                var similarColors = self.Where(c => color.ApproximatelyRGB(c, threshold)).ToList();

                //merge them into this color
                foreach (var similarColor in similarColors.Where(similarColor => !alreadyMerged.Contains(similarColor)))
                {
                    mergedColors[color] += colorDictionary[similarColor];
                    alreadyMerged.Add(similarColor);
                }
            }

            return mergedColors.Keys.ToArray();
        }
    }
}