using UnityEngine;

namespace LiteNinja.Colors.Themes.Colorizers
{
    public class ParticleSystemColorize : AColorize<ParticleSystem>
    {
        protected override void OnColorChanged()
        {
            if (!_component || !_colorLink) return;
            var componentMain = _component.main;
            componentMain.startColor = _colorLink.Color;
        }
    }
}