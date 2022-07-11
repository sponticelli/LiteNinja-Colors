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
        [SerializeField]
        protected List<Color> _colors = new();
        [NonSerialized]
        private Texture2D _texture;
        private List<Action> _listeners = new();
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
            _colors.Clear();
            _colors.AddRange(colors);
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
            _colors.AddRange(colors);
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

        public void Clear()
        {
            _colors.Clear();
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

        public void AddListener(Action listener)
        {
            if (_listeners.Contains(listener))
                return;
            _listeners.Add(listener);
        }

        public void RemoveListener(Action listener)
        {
            _listeners.Remove(listener);
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
                Debug.Log(_colors.Count);
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
            foreach (var listener in _listeners)
            {
                listener?.Invoke();
            }
        }

        public bool Contains(Color color)
        {
            return _colors.Contains(color);
        }
    }
}