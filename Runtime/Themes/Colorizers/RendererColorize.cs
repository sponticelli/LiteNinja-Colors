using UnityEngine;

namespace LiteNinja.Colors.Themes.Colorizers
{
    public class RendererColorize : AColorize<Renderer>
    {
        private MaterialPropertyBlock _materialPropertyBlock;
        private int _colorShaderId;
        protected override void OnColorChanged()
        {
            if (!_component || !_colorLink) return;
            _materialPropertyBlock?.SetColor(_colorShaderId, _colorLink.Color);
        }

        protected override void AttachComponent()
        {
            base.AttachComponent();
            if (!_component) return;
            if (_materialPropertyBlock == null)
            {
                _materialPropertyBlock = new MaterialPropertyBlock();
                _colorShaderId = Shader.PropertyToID("_Color");
            }

            _materialPropertyBlock.SetColor(_colorShaderId, _colorLink.Color);
            _component.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}