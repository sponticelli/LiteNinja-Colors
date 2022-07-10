using UnityEngine;

namespace LiteNinja.Colors.Themes
{
    public abstract class AColorize : MonoBehaviour
    {
        [SerializeField]
        private ColorLink _colorLink;

        private void OnDisable()
        {
            _colorLink.OnColorChanged -= OnColorChanged;
        }
        
        private void OnEnable()
        {
            _colorLink ??= new ColorLink();
            _colorLink.OnColorChanged += OnColorChanged;
            _colorLink.OnEnable();
        }

        protected abstract void OnColorChanged();
    }
}