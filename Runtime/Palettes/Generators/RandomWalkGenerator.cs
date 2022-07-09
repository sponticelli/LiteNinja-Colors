using System;
using LiteNinja.Colors.Extensions;
using LiteNinja.Colors.Spaces;
using UnityEngine;

namespace LiteNinja.Colors.Palettes.Generators
{
    public class RandomWalkGenerator : AGenerator
    {
        [SerializeField] private Options _options;

        public RandomWalkGenerator(int? seed, Options? options) : base(seed)
        {
            _options = options ?? new Options
            {
                color = new Color(_random.Next(), _random.Next(), _random.Next()),
                offsetRange = (_random.Next(), _random.Next()),
                fixLightness = _random.Next(0, 2) == 0
            };
        }

        public void Reset(Options options, int? seed)
        {
            base.Reset(seed);
            _options = options;
        }

        public override IPalette Generate(int count)
        {
            var colors = new Color[count];
            var newColor = _options.color;
            for (var index = 0; index < count; index++)
            {
                var amplitude = _options.offsetRange.Item1 +
                                _random.NextDouble() * (_options.offsetRange.Item2 - _options.offsetRange.Item1);
                var offset = new Vector3((float)(amplitude * _random.NextDouble()),
                    (float)(amplitude * _random.NextDouble()),
                    (float)(amplitude * _random.NextDouble()));
                newColor = new Color(
                    Mathf.Clamp01(newColor.r + offset.x),
                    Mathf.Clamp01(newColor.g + offset.y),
                    Mathf.Clamp01(newColor.b + offset.z));
                colors[index] = newColor;
            }

            if (!_options.fixLightness) return new Palette(colors);
            {
                var lightness = ((ColorHSL)_options.color).Lightness;
                for (var index = 0; index < count; index++)
                {
                    colors[index] = colors[index].WithLightness(lightness);
                }
            }

            return new Palette(colors);
        }

        [Serializable]
        public class Options
        {
            public Color color;
            public (float, float) offsetRange;
            public bool fixLightness;
        }
    }
}