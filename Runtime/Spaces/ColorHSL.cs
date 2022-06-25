using System;
using UnityEngine;

namespace com.liteninja.colors.spaces
{
    /// <summary>
    /// A struct that represents a color in the HSL color space.
    /// H = hue, S = saturation, L = lightness, A = alpha
    /// @ref https://en.wikipedia.org/wiki/HSL_and_HSV
    /// </summary>
    [Serializable]
    public struct ColorHSL
    {
        [SerializeField] [Range(0f, 1f)] private float _hue;
        [SerializeField] [Range(0f, 1f)] private float _saturation;
        [SerializeField] [Range(0f, 1f)] private float _lightness;
        [SerializeField] [Range(0f, 1f)] private float _alpha;

        public float Hue
        {
            get => _hue;
            set
            {
                _hue = value - Mathf.FloorToInt(value);
                if (_hue < 0) _hue = 1 - _hue;
            }
        }

        public float Saturation
        {
            get => _saturation;
            set => _saturation = Mathf.Clamp01(value);
        }

        public float Lightness
        {
            get => _lightness;
            set => _lightness = Mathf.Clamp01(value);
        }

        public float Alpha
        {
            get => _alpha;
            set => _alpha = value;
        }

        public ColorHSL(float hue, float saturation, float lightness, float alpha = 1f) : this()
        {
            _hue = 0;
            _saturation = 0;
            _lightness = 0;
            _alpha = 0;
            Hue = hue;
            Saturation = saturation;
            Lightness = lightness;
            Alpha = alpha;
        }

        #region Color Conversion

        public static explicit operator Vector3(ColorHSL color) => new(color.Hue, color.Saturation, color.Lightness);
        public static explicit operator Vector4(ColorHSL color) => new(color.Hue, color.Saturation, color.Lightness, color.Alpha);

        public static implicit operator ColorHSL(Color color)
        {
            var max = Mathf.Max(color.r, Mathf.Max(color.g, color.b));
            if (max <= 0)
            {
                return new ColorHSL(0, 0, 0, color.a);
            }

            var min = Mathf.Min(color.r, Mathf.Min(color.g, color.b));
            var delta = max - min;
            var hue = 0f;
            if (max > min)
            {
                if (color.g >= color.r && color.g >= color.b)
                {
                    hue = (color.b - color.r) / delta * 60f + 120f;
                }
                else if (color.b >= color.g && color.b >= color.r)
                {
                    hue = (color.r - color.g) / delta * 60f + 240f;
                }
                else if (color.b > color.g)
                {
                    hue = (color.g - color.b) / delta * 60f + 360f;
                }
                else
                {
                    hue = (color.g - color.b) / delta * 60f;
                }

                if (hue < 0)
                {
                    hue += 360f;
                }
            }
            else
            {
                delta = 0;
            }

            hue *= 1f / 360f;

            return new ColorHSL(hue, delta / max, max, color.a);
        }

        public static implicit operator Color(ColorHSL color)
        {
            var r = color.Lightness;
            var g = color.Lightness;
            var b = color.Lightness;

            if (color.Saturation <= 0) return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), color.Alpha);

            //TODO: continue here
            throw new NotImplementedException();
        }

        #endregion

        public override string ToString()
        {
            return $"HSL: {Hue:F3}, {Saturation:F3}, {Lightness:F3}, {Alpha:F3}";
        }
    }
}