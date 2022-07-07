using System;
using System.Collections;
using System.Collections.Generic;
using LiteNinja.Colors.Spaces;
using UnityEngine;

namespace LiteNinja.Colors.Palettes
{
    [Serializable]
    public class Palette : IPalette
    {
        [SerializeField] protected List<Color> _colors;

        public Palette(IEnumerable<Color> colors)
        {
            _colors = new List<Color>(colors);
        }

        public IEnumerator Colors()
        {
            return _colors.GetEnumerator();
        }

        public int Count { get; }
        public Color this[int index] => _colors[index];

        public void Sort()
        {
            _colors.Sort((color, color1) => ((ColorHSL)color).Hue.CompareTo(((ColorHSL)color1).Hue));
        }
    }
}