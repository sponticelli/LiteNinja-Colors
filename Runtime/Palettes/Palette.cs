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
        public List<Color> colors = new List<Color>();
        public int Count => colors.Count;

        public Color this[int index]
        {
            get => colors[index];
            set => colors[index] = value;
        }

        public void SetAll(IEnumerable<Color> colors)
        {
            this.colors.Clear();
            this.colors.AddRange(colors);
        }

        public IEnumerable<Color> GetAll()
        {
            return colors;
        }

        public void Add(Color color)
        {
            colors.Add(color);
        }

        public void Remove(Color color)
        {
            colors.Remove(color);
        }

        public void RemoveAt(int index)
        {
            colors.RemoveAt(index);
        }

        public Color Random()
        {
            return colors[UnityEngine.Random.Range(0, colors.Count)];
        }
    }
}