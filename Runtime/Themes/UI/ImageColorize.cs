using UnityEngine;
using UnityEngine.UI;

namespace LiteNinja.Colors.Themes.UI
{
    public class ImageColorize : AColorize<Image>
    {
        protected override void OnColorChanged()
        {
            if (!_component) return;
            _component.color = _colorLink.Color;
        }
    }
}