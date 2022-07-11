using System;
using UnityEngine;

namespace LiteNinja.Colors.Themes
{
    public abstract class AColorize : MonoBehaviour
    {
        [SerializeField]
        private ColorLinkSO _colorLink;

        private void OnDisable()
        {
            _colorLink.RemoveListener(OnColorChanged); 
        }
        
        private void OnEnable()
        {
            _colorLink.AddListener(OnColorChanged); 
        }

        private void OnValidate()
        {
            _colorLink?.AddListener(OnColorChanged);
            OnColorChanged();
        }

        protected abstract void OnColorChanged();
    }
}