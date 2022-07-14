using UnityEngine;

namespace LiteNinja.Colors.Themes
{
    public abstract class AColorize : MonoBehaviour
    {
        [SerializeField] protected ColorLinkSO _colorLink;

        protected void OnDisable()
        {
            _colorLink.RemoveListener(OnColorChanged);
        }

        protected void OnEnable()
        {
            AttachComponent();
            _colorLink.AddListener(OnColorChanged);
        }

        protected void OnValidate()
        {
            AttachComponent();
            _colorLink?.AddListener(OnColorChanged);
            OnColorChanged();
        }

        protected abstract void OnColorChanged();
        protected abstract void AttachComponent();
    }

    public abstract class AColorize<T> : AColorize where T : Component
    {
        [SerializeField] protected T _component;

        protected override void AttachComponent()
        {
            if (!_component)
                _component = GetComponent<T>();
        }
    }
}