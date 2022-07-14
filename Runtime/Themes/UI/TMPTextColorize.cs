using TMPro;
using UnityEngine;

namespace LiteNinja.Colors.Themes.UI
{
    public class TMPTextColorize : AColorize
    {
        [SerializeField] private TMP_Text _text;
        protected override void OnColorChanged()
        {
            if (_text) _text.color = _colorLink.Color;
        }

        protected override void AttachComponent()
        {
            if (!_text)
                _text = GetComponent<TMP_Text>();
        }
    }
}