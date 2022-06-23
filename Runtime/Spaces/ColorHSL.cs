using UnityEngine;

namespace com.liteninja.colors.spaces
{
    /// <summary>
    /// A struct that represents a color in the HSL color space.
    /// H = hue, S = saturation, L = lightness, A = alpha
    /// @ref https://en.wikipedia.org/wiki/HSL_and_HSV
    /// </summary>
    public struct ColorHSL
    {
        private float _h;
        private float _s;
        private float _l;
        private float _alpha;

        public float H
        {
            get => _h;
            set
            {
                _h = value - Mathf.FloorToInt(value);
                if (_h < 0) _h = 1 - _h;
            }
        }

        public float S
        {
            get => _s;
            set => _s = Mathf.Clamp01(value);
        }

        public float L
        {
            get => _l;
            set => _l = Mathf.Clamp01(value);
        }

        public float Alpha
        {
            get => _alpha;
            set => _alpha = value;
        }

        public ColorHSL(float h, float s, float l, float alpha = 1f) : this()
        {
            _h = 0;
            _s = 0;
            _l = 0;
            _alpha = 0;
            H = h;
            S = s;
            L = l;
            Alpha = alpha;
        }

        #region Color Conversion

        public static explicit operator Vector3(ColorHSL color) => new(color.H, color.S, color.L);
        public static explicit operator Vector4(ColorHSL color) => new(color.H, color.S, color.L, color.Alpha);

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

        

        #endregion
    }
}