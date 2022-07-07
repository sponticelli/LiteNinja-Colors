using System.Linq;
using LiteNinja.Colors.Spaces;
using UnityEngine;

namespace LiteNinja.Colors.Palettes.Generators
{
    /// <summary>
    /// Generates a triadic color harmony.
    /// </summary>
    public class HarmonicGenerator : AGenerator
    {
        private Options _options;

        public HarmonicGenerator(int? seed, Options? options) : base(seed)
        {
            _options = options ?? new Options()
            {
                referenceAngle = ((float)_random.NextDouble() * 360.0f, (float)_random.NextDouble() * 360.0f),
                offsetAngle1 = ((float)_random.NextDouble() * 360.0f, (float)_random.NextDouble() * 360.0f),
                offsetAngle2 = ((float)_random.NextDouble() * 360.0f, (float)_random.NextDouble() * 360.0f),
                saturation = ((float)_random.NextDouble(), (float)_random.NextDouble()),
                lightness = ((float)_random.NextDouble(), (float)_random.NextDouble())
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

            for (var index = 0; index < count; index++)
            {
                var randomAngle = _random.NextDouble() * (_options.referenceAngle.Item2 + _options.offsetAngle1.Item2 +
                                                          _options.offsetAngle2.Item2);
                if (randomAngle > _options.referenceAngle.Item2)
                {
                    if (randomAngle < _options.referenceAngle.Item2 + _options.offsetAngle1.Item2)
                    {
                        randomAngle += _options.offsetAngle1.Item1;
                    }
                    else
                    {
                        randomAngle += _options.offsetAngle2.Item1;
                    }
                }

                var saturation = _options.saturation.Item1 +
                                 (_options.saturation.Item2 - _options.saturation.Item1) * _random.NextDouble();
                var lightness = _options.lightness.Item1 +
                                (_options.lightness.Item2 - _options.lightness.Item1) * _random.NextDouble();

                colors[index] = new ColorHSL((float)(_options.referenceAngle.Item1 + randomAngle) % 360.0f,
                    (float)saturation,
                    (float)lightness);
            }
            
            

            return new Palette(colors);
        }
        
        public struct Options
        {
            public (float, float) referenceAngle; // The reference angle of the harmony (in degrees)
            public (float, float) offsetAngle1; // The offset angle of the first color (in degrees)
            public (float, float) offsetAngle2; // The offset angle of the second color (in degrees)

            public (float, float) saturation; // (min, max)
            public (float, float) lightness; // (min, max)
        }
    }
}