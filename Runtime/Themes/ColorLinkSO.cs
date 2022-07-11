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
    public class ColorLinkSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        private PaletteSO _palette;
        [SerializeField]
        private int _colorIndex;
        [SerializeField]
        private Color _fallbackColor;
        
        private List<Action> _listeners = new();

        public PaletteSO Palette => _palette;

        public int ColorIndex => _colorIndex;

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
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _listeners = new List<Action>();
        }

        public void OnValidate()
        {
            Invoke();
        }
    }
}