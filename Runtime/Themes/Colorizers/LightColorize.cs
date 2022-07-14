using UnityEngine;

namespace LiteNinja.Colors.Themes.Colorizers
{
    public class LightColorize : AColorize<Light>
    {
        protected override void OnColorChanged()
        {
            if (!_component) return;
            _component.color = _colorLink.Color;
        }
    }
}