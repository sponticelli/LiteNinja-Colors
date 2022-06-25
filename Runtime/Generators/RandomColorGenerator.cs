using UnityEngine;

namespace LiteNinja_Colors.Runtime.Generators
{
    public class RandomColorGenerator : IColorGenerator<RandomColorGeneratorOptions>
    {
        public Color GetColor(RandomColorGeneratorOptions colorGeneratorOptions = default)
        {
            return new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
        }

        public Color[] GetColors(int numColors, RandomColorGeneratorOptions colorGeneratorOptions = default)
        {
            var colors = new Color[numColors];
            for (var i = 0; i < numColors; i++)
            {
                colors[i] = GetColor(colorGeneratorOptions);
            }
            return colors;
        }
    }
}