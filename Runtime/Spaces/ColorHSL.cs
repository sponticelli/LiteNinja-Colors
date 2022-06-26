using System;
using UnityEngine;

namespace LiteNinja.Colors.Spaces
{
    /// <summary>
    /// A struct that represents a color in the HSL color space.
    /// H = hue, S = saturation, L = lightness, A = alpha
    /// @ref https://en.wikipedia.org/wiki/HSL_and_HSV
    /// </summary>
    [Serializable]
    public struct ColorHSL
    {
        [SerializeField] [Range(0f, 0.999999999f)] private float _hue;
        [SerializeField] [Range(0f, 1f)] private float _saturation;
        [SerializeField] [Range(0f, 1f)] private float _lightness;
        [SerializeField] [Range(0f, 1f)] private float _alpha;

        /// <summary>
        /// Hue as a value between 0 and 1.
        /// </summary>
        public float Hue
        {
            get => _hue;
            set
            {
                _hue = value - Mathf.FloorToInt(value);
                if (_hue < 0) _hue = 1 - _hue;
            }
        }

        /// <summary>
        /// Hue in degrees.
        /// </summary>
        public float HueDegrees
        {
            get => Hue * 360f;
            set
            {
                const float inverseDegrees = 1f / 360f;
                Hue = value * inverseDegrees;
            }
        }
        
        /// <summary>
        /// Hue in radians.
        /// </summary>
        public float HueRadians
        {
            get => Hue * Mathf.PI * 2f;
            set
            {
                const float inverseRadians = 1f / (Mathf.PI * 2f);
                Hue = value * inverseRadians;
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
        public static explicit operator ColorHSL(Vector3 vector) => new(vector.x, vector.y, vector.z, 1f);

        public static explicit operator Vector4(ColorHSL color) =>
            new(color.Hue, color.Saturation, color.Lightness, color.Alpha);

        public static explicit operator ColorHSL(Vector4 vector) => new(vector.x, vector.y, vector.z, vector.w);

        public static implicit operator ColorHSL(Color color)
        {
            var min = Mathf.Min(color.r, color.g, color.b);
            var max = Mathf.Max(color.r, color.g, color.b);

            // Calculate lightness
            var lightness = (min + max) / 2f;

            // Calculate saturation
            var saturation = 0f;
            if (!Mathf.Approximately(min, max))
            {
                if (lightness <= 0.5f)
                {
                    saturation = (max - min) / (max + min);
                }
                else
                {
                    saturation = (max - min) / (2f - max - min);
                }
            }

            if (saturation == 0) return new ColorHSL(0f, 0f, lightness, color.a);

            // Calculate hue
            var hue = 0f;
            if (color.r >= max)
            {
                hue = (color.g - color.b) / (max - min);
            }
            else if (color.g >= max)
            {
                hue = 2f + (color.b - color.r) / (max - min);
            }
            else
            {
                hue = 4f + (color.r - color.g) / (max - min);
            }

            return new ColorHSL(hue, saturation, lightness, color.a);
        }

        public static implicit operator Color(ColorHSL color)
        {
            if (color.Saturation == 0) return new Color(color.Lightness, color.Lightness, color.Lightness, color.Alpha);

            var temp1 = color.Lightness < 0.5f
                ? color.Lightness * (1f + color.Saturation)
                : color.Lightness + color.Saturation - color.Lightness * color.Saturation;
            var temp2 = 2f * color.Lightness - temp1;

            var r = color.Hue + 1f / 3f;
            var g = color.Hue;
            var b = color.Hue - 1f / 3f;

            if (r > 1f) r -= 1f;
            if (b < 0f) g += 1f;

            r = CalcChannel(r, temp1, temp2);
            g = CalcChannel(g, temp1, temp2);
            b = CalcChannel(b, temp1, temp2);

            return new Color(r, g, b, color.Alpha);
        }

        #endregion

        public override string ToString()
        {
            return $"HSL: {Hue:F3}, {Saturation:F3}, {Lightness:F3}, {Alpha:F3}";
        }

        #region Color shortcuts

        public static ColorHSL Black => new ColorHSL(0f, 0f, 0f, 1f);
        public static ColorHSL White => new ColorHSL(0f, 0f, 1f, 1f);
        public static ColorHSL Red => new ColorHSL(0f, 1f, 0.5f, 1f);
        public static ColorHSL Green => new ColorHSL(1f / 3f, 1f, 0.5f, 1f);
        public static ColorHSL Blue => new ColorHSL(2f / 3f, 1f, 0.5f, 1f);
        public static ColorHSL Cyan => new ColorHSL(0f, 1f, 0.5f, 1f);
        public static ColorHSL Magenta => new ColorHSL(1f / 3f, 1f, 0.5f, 1f);
        public static ColorHSL Yellow => new ColorHSL(2f / 3f, 1f, 0.5f, 1f);
        public static ColorHSL Transparent => new ColorHSL(0f, 0f, 0f, 0f);

        #endregion

        #region private methods

        private static float CalcChannel(float channel, float p, float q)
        {
            if (channel * 6f < 1)
            {
                return q + (p - q) * 6f * channel;
            }

            if (channel * 2f < 1)
            {
                return p;
            }

            if (channel * 3f < 2)
            {
                return q + (p - q) * 6f * (2f / 3f - channel);
            }

            return q;
        }

        #endregion
    }
}