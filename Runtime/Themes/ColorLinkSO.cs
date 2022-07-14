using System;
using System.Collections.Generic;
using UnityEngine;

namespace LiteNinja.Colors.Themes
{
    /// <summary>
    /// A class that references a scriptable object palette and an indexed color of the palette
    /// It allows to observe when the palette changes and when the color changes.
    /// </summary>
    [CreateAssetMenu(menuName = "LiteNinja/Colors/Themes/ColorLink", fileName = "ColorLinkSO", order = 0)]
    [Serializable]
    public class ColorLinkSO : ScriptableObject
    {
        [SerializeField] private PaletteSO _palette;
        [SerializeField] private int _colorIndex;
        [SerializeField] private Color _fallbackColor = Color.magenta;
        private List<Action> _listeners = new();

        public PaletteSO Palette
        {
            get => _palette;
            set
            {
                _palette = value;
                Invoke();
            }
        }

        public int ColorIndex
        {
            get => _colorIndex;
            set
            {
                _colorIndex = value;
                Invoke();
            }
        }

        public Color Color
        {
            get
            {
                if (_palette == null)
                    return _fallbackColor;
                if (_colorIndex < 0 || _colorIndex >= _palette.Count)
                    return _fallbackColor;
                return _palette[_colorIndex];
            }
        }

#if UNITY_EDITOR
        public string ColorName => _palette == null ? "" : _palette.GetColorName(_colorIndex);
#endif

        public Color FallbackColor
        {
            get => _fallbackColor;
            set => _fallbackColor = value;
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

        public void Invoke()
        {
            foreach (var listener in _listeners)
            {
                listener();
            }
        }

        public void OnEnable()
        {
            if (_palette == null) return;
            _palette.AddListener(Invoke);
        }

        public void OnDisable()
        {
            if (_palette != null)
                _palette.RemoveListener(Invoke);
        }


        public void OnValidate()
        {
            Invoke();
        }
    }
}