using System;
using System.Collections.Generic;
using LiteNinja.Colors.Palettes;
using UnityEngine;

namespace LiteNinja.Colors.Themes
{
    [CreateAssetMenu(menuName = "LiteNinja/Colors/Themes/Palette", fileName = "PaletteSO", order = 0)]
    [Serializable]
    public class PaletteSO : ScriptableObject, IPalette, ISerializationCallbackReceiver
    {
        [SerializeField]
        private Palette _palette = new();
        private List<Action> _listeners = new();
        public int Count { get; }

        public Color this[int index]
        {
            get => _palette[index];
            set  {
                _palette[index] = value;
                Invoke();
            }
        }

        

        public void SetAll(IEnumerable<Color> colors)
        {
            _palette.SetAll(colors);
            Invoke();
        }

        public IEnumerable<Color> GetAll()
        {
            return _palette.GetAll();
        }

        public void Add(Color color)
        {
            _palette.Add(color);
            Invoke();
        }

        public void AddRange(IEnumerable<Color> colors)
        {
            _palette.AddRange(colors);
            Invoke();
        }

        public void Remove(Color color)
        {
            _palette.Remove(color);
            Invoke();
        }

        public void RemoveAt(int index)
        {
            _palette.RemoveAt(index);
            Invoke();
        }

        public void ReplaceFromPalette(IPalette palette)
        {
            _palette.ReplaceFromPalette(palette);
            Invoke();
        }

        public void AddFromPalette(IPalette palette)
        {
            _palette.AddFromPalette(palette);
            Invoke();
        }

        public void AddListener(Action listener)
        {
            throw new NotImplementedException();
        }

        public void RemoveListener(Action listener)
        {
            throw new NotImplementedException();
        }

        public Texture2D Texture => _palette.Texture;
        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            Invoke();
        }
        
        private void Invoke()
        {
            foreach (var listener in _listeners)
            {
                listener?.Invoke();
            }
        }
    }
}