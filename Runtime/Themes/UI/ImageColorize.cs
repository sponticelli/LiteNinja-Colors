using UnityEngine;
using UnityEngine.UI;

namespace LiteNinja.Colors.Themes.UI
{
    public class ImageColorize : AColorize
    {
        [SerializeField] private Image _image;

        protected override void OnColorChanged()
        {
            if (_image) _image.color = _colorLink.Color;
        }

        protected override void AttachComponent()
        {
            if (!_image)
                _image = GetComponent<Image>();
        }
    }
}