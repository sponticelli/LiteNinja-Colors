using System;
using UnityEngine;

namespace LiteNinja.Colors.Themes
{
    /// <summary>
    /// A class that references a scriptable object palette and an indexed color of the palette
    /// It allows to observe when the palette changes and when the color changes.
    /// </summary>
    [Serializable]
    public class ColorLink
    {
        [SerializeField]
        private PaletteSO _palette;
        [SerializeField]
        private int _colorIndex;
        [SerializeField]
        private Color _fallbackColor;
        
        public event Action OnColorChanged;

        public PaletteSO Palette
        {
            get => _palette;
            set
            {
                if (_palette != null)
                    _palette.OnPaletteChange -= OnPaletteChanged;
                _palette = value;
                if (_palette != null)
                    _palette.OnPaletteChange += OnPaletteChanged;
                OnColorChanged?.Invoke();
            }
        }
        
        public int ColorIndex
        {
            get => _colorIndex;
            set
            {
                _colorIndex = value;
                OnColorChanged?.Invoke();
            }
        }

        public Color Color
        {
            get
            {
                if (_palette == null)
                    return _fallbackColor;
                if (_colorIndex< 0 || _colorIndex >= _palette.Count)
                    return _fallbackColor;
                return _palette[_colorIndex];
            }
        }

        private void OnPaletteChanged()
        {
            OnColorChanged?.Invoke();
        }
        
        public void OnEnable()
        {
            if (_palette == null) return;
            _palette.OnPaletteChange -= OnPaletteChanged;
            _palette.OnPaletteChange += OnPaletteChanged;
        }
        
        public void OnDisable()
        {
            if (_palette != null)
                _palette.OnPaletteChange -= OnPaletteChanged;
        }
    }
}