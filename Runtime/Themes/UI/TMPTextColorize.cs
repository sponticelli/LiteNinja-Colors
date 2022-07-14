using TMPro;
using UnityEngine;

namespace LiteNinja.Colors.Themes.UI
{
    public class TMPTextColorize : AColorize<TMP_Text>
    {
        
        protected override void OnColorChanged()
        {
            if (!_component) return;
            _component.color = _colorLink.Color;
        }
        
    }
}