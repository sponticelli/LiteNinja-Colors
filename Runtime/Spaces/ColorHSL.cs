using System;
using LiteNinja.Utils.Extensions;
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
        [SerializeField] private float _hue;
        [SerializeField] private float _saturation;
        [SerializeField] private float _lightness;
        [SerializeField] private float _alpha;

        /// <summary>
        /// Hue as a value between 0 and 1.
        /// </summary>
        public float Hue
        {
            get => _hue;
            set
            {
                value %= 1f;
                if (value < 0f)
                {
                    value += 1f;
                }

                _hue = value;
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
            var saturation = 0f;
            var hue = 0f;

            var delta = max - min;
            var add = max + min;

            // Calculate lightness
            var lightness = add / 2f;

            if (Mathf.Approximately(min, max)) return new ColorHSL(hue, saturation, lightness, color.a);

            // Calculate saturation
            saturation = lightness > 0.5f ? delta / (2f - max - min) : delta / add;
            // Calculate hue
            const float oneSixth = 1f / 6f;
            hue = max.Approximately(color.r) ? (color.g - color.b) / delta + (color.g < color.b ? 6f : 0f) :
                max.Approximately(color.g) ? (color.b - color.r) / delta + 2f :
                (color.r - color.g) / delta + 4f;
            hue *= oneSixth;

            return new ColorHSL(hue, saturation, lightness, color.a);
        }

        public static implicit operator Color(ColorHSL color)
        {
            const float oneThird = 1f / 3f;
            float r, g, b;
            if (color.Saturation == 0f)
            {
                r = g = b = color.Lightness;
            }
            else
            {
                var q = color.Lightness < 0.5f
                    ? color.Lightness * (1f + color.Saturation)
                    : color.Lightness + color.Saturation - (color.Lightness * color.Saturation);
                var p = (2f * color.Lightness) - q;
                r = CalcChannel(p, q, color.Hue + oneThird);
                g = CalcChannel(p, q, color.Hue);
                b = CalcChannel(p, q, color.Hue - oneThird);
            }

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

        private static float CalcChannel(float p, float q, float third)
        {
            const float oneSixth = 1f / 6f;
            const float twoThirds = 2f / 3f;

            if (third < 0f)
            {
                third += 1f;
            }

            if (third > 1f)
            {
                third -= 1f;
            }

            return third switch
            {
                < oneSixth => p + ((q - p) * 6f * third),
                < 0.5f => q,
                < twoThirds => p + ((q - p) * (twoThirds - third) * 6f),
                _ => p
            };
        }

        #endregion
    }
}