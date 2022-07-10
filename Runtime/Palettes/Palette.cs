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
        protected List<Color> _colors = new();
        [NonSerialized]
        private Texture2D _texture;
        public event IPalette.PaletteChange OnPaletteChange;
        public int Count => _colors.Count;

        public Color this[int index]
        {
            get
            {
                if (index < 0 || index >= _colors.Count)
                {
                    return Color.white;
                }

                return _colors[index];
            }
            set
            {
                if (index < 0 || index >= _colors.Count)
                {
                    return;
                }

                _colors[index] = value;
                Trigger();
            }
        }

        public void SetAll(IEnumerable<Color> colors)
        {
            this._colors.Clear();
            this._colors.AddRange(colors);
            Trigger();
        }

        public IEnumerable<Color> GetAll()
        {
            return _colors;
        }

        public void Add(Color color)
        {
            _colors.Add(color);
            Trigger();
        }

        public void AddRange(IEnumerable<Color> colors)
        {
            this._colors.AddRange(colors);
            Trigger();
        }

        public void Remove(Color color)
        {
            _colors.Remove(color);
            Trigger();
        }

        public void RemoveAt(int index)
        {
            _colors.RemoveAt(index);
            Trigger();
        }

        public void ReplaceFromPalette(IPalette palette)
        {
            _colors.Clear();
            _colors.AddRange(palette.GetAll());
            Trigger();
        }

        public void AddFromPalette(IPalette palette)
        {
            _colors.AddRange(palette.GetAll());
            Trigger();
        }

        public Texture2D Texture
        {
            get
            {
                if (_texture != null) return _texture;
                GenerateTexture();
                return _texture;
            }
        }

        private void GenerateTexture()
        {
            if (_colors.Count > 0)
            {
                _texture = new Texture2D(_colors.Count, 1)
                {
                    filterMode = FilterMode.Point
                };
                _texture.SetPixels(_colors.ToArray());
                _texture.Apply();
            } else {
                _texture = null;
            }
        }

        private void Trigger()
        {
            GenerateTexture();
            OnPaletteChange?.Invoke();
        }
    }
}