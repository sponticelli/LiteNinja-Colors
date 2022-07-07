using System.Collections.Generic;
using System.Linq;
using LiteNinja.Colors.Spaces;
using UnityEngine;

namespace LiteNinja.Colors.Palettes.Generators
{
    public class RandomGenerator : AGenerator
    {
        public RandomGenerator(int? seed) : base(seed)
        {
        }


        public override IPalette Generate(int count)
        {
            const float oneOver255 = 1f / 255f;
            var colors = new List<Color>();
            for (var i = 0; i < count; i++)
            {
                colors.Add(new Color(_random.Next(0, 256) * oneOver255, _random.Next(0, 256) * oneOver255,
                    _random.Next(0, 256) * oneOver255));
            }

            return new Palette(colors.OrderBy(c => ((ColorHSL)c).Hue));
        }

        public IPalette GenerateHSL(int count)
        {
            var colors = new List<Color>();
            for (var i = 0; i < count; i++)
            {
                colors.Add(new ColorHSL(
                    (float)_random.NextDouble(),
                    (float)_random.NextDouble(),
                    (float)_random.NextDouble()
                ));
            }

            return new Palette(colors);
        }
    }
}