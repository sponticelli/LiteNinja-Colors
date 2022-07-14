using System;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

namespace LiteNinja.Colors.Palettes
{
    [Serializable]
    public class Palette : IPalette
    {
        [SerializeField] protected List<Color> _colors = new();

        [NonSerialized] private Texture2D _texture;
        private List<Action> _listeners = new();

#if UNITY_EDITOR
        [SerializeField] protected List<string> _colorNames = new();
#endif

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
#if UNITY_EDITOR
            _colorNames.Clear();
            foreach (var color in colors)
            {
                _colorNames.Add("");
            }
#endif
            Trigger();
        }

        public IEnumerable<Color> GetAll()
        {
            return _colors;
        }

        public void Add(Color color)
        {
            _colors.Add(color);
#if UNITY_EDITOR
            _colorNames.Add("");
#endif
            Trigger();
        }

        public void AddRange(IEnumerable<Color> colors)
        {
            _colors.AddRange(colors);
#if UNITY_EDITOR
            foreach (var color in colors)
            {
                _colorNames.Add("");
            }
#endif
            Trigger();
        }

        public void Remove(Color color)
        {
#if UNITY_EDITOR
            var index = _colors.IndexOf(color);
            if (index >= 0)
            {
                _colorNames.RemoveAt(index);
            }
#endif
            _colors.Remove(color);
            Trigger();
        }

        public void RemoveAt(int index)
        {
            _colors.RemoveAt(index);
#if UNITY_EDITOR
            _colorNames.RemoveAt(index);
#endif
            Trigger();
        }

        public void Clear()
        {
            _colors.Clear();
#if UNITY_EDITOR
            _colorNames.Clear();
#endif
            Trigger();
        }

        public void ReplaceFromPalette(IPalette palette)
        {
            _colors.Clear();
            _colors.AddRange(palette.GetAll());
#if UNITY_EDITOR
            _colorNames.Clear();
            for (var i = 0; i < palette.Count; i++)
            {
                _colorNames.Add(palette.GetColorName(i));
            }
#endif
            Trigger();
        }

        public void AddFromPalette(IPalette palette)
        {
            _colors.AddRange(palette.GetAll());
#if UNITY_EDITOR
            for (var i = 0; i < palette.Count; i++)
            {
                _colorNames.Add(palette.GetColorName(i));
            }
#endif
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

#if UNITY_EDITOR
        public string GetColorName(int index)
        {
            NormalizeColorNames();
            if (index < 0 || index >= _colorNames.Count)
            {
                return "";
            }

            return _colorNames[index];
        }

        public void SetColorName(int index, string name)
        {
            NormalizeColorNames();
            if (index < 0 || index >= _colors.Count)
            {
                return;
            }

            _colorNames[index] = name;
        }

        private void NormalizeColorNames()
        {
            if (_colorNames.Count > _colors.Count) return;
            //add an empty strings if the list is smaller than the colors until they are the same size
            for (var i = _colorNames.Count; i < _colors.Count; i++)
            {
                _colorNames.Add("");
            }
        }

#endif
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
            }
            else
            {
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