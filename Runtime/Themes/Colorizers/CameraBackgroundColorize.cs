using UnityEngine;

namespace LiteNinja.Colors.Themes.Colorizers
{
    public class CameraBackgroundColorize : AColorize<Camera>
    {
        protected override void OnColorChanged()
        {
            if (!_component ) return;
            _component.backgroundColor = _colorLink ? _colorLink.Color : Color.magenta;;
        }
    }
}