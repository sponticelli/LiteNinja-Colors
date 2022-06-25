using UnityEngine;

namespace LiteNinja_Colors.Runtime.Generators
{
    public interface IColorGenerator<in T> where T : IGeneratorOptions
    {
        Color GetColor(T options = default);
        Color[] GetColors(int numColors, T options = default);
    }
}