using UnityEngine;

namespace LiteNinja.Colors.Themes.Colorizers
{
    public class CameraBackgroundColorize : AColorize
    {
        [SerializeField] private Camera _camera;
        
        
        protected override void OnColorChanged()
        {
            if (_camera)
                _camera.backgroundColor = _colorLink.Color;
        }

        protected override void AttachComponent()
        {
            if (!_camera)
                _camera = GetComponent<Camera>();
        }
    }
}