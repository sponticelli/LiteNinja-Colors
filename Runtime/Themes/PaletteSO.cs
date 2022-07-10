using System;
using System.Collections.Generic;
using LiteNinja.Colors.Palettes;
using UnityEngine;

namespace LiteNinja.Colors.Themes
{
    [CreateAssetMenu(menuName = "LiteNinja/Colors/PaletteSO", fileName = "PaletteSO", order = 0)]
    [Serializable]
    public class PaletteSO : ScriptableObject, IPalette
    {
        [SerializeField]
        private Palette _palette = new();
        public event IPalette.PaletteChange OnPaletteChange;
        public int Count { get; }

        public Color this[int index]
        {
            get => _palette[index];
            set  {
                _palette[index] = value;
                OnPaletteChange?.Invoke();
            }
        }

        public void SetAll(IEnumerable<Color> colors)
        {
            _palette.SetAll(colors);
            OnPaletteChange?.Invoke();
        }

        public IEnumerable<Color> GetAll()
        {
            return _palette.GetAll();
        }

        public void Add(Color color)
        {
            _palette.Add(color);
            OnPaletteChange?.Invoke();
        }

        public void AddRange(IEnumerable<Color> colors)
        {
            _palette.AddRange(colors);
            OnPaletteChange?.Invoke();
        }

        public void Remove(Color color)
        {
            _palette.Remove(color);
            OnPaletteChange?.Invoke();
        }

        public void RemoveAt(int index)
        {
            _palette.RemoveAt(index);
            OnPaletteChange?.Invoke();
        }

        public void ReplaceFromPalette(IPalette palette)
        {
            _palette.ReplaceFromPalette(palette);
            OnPaletteChange?.Invoke();
        }

        public void AddFromPalette(IPalette palette)
        {
            _palette.AddFromPalette(palette);
            OnPaletteChange?.Invoke();
        }

        public Texture2D Texture => _palette.Texture;
    }
}