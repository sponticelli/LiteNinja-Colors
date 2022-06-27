using System;
using UnityEngine;

namespace LiteNinja.Colors.Spaces
{
    /// <summary>
    /// A struct that represents a color in the HSV color space.
    /// Also known as "HSB" color space.
    /// H = hue, S = saturation, V = value, A = alpha
    /// @ref https://en.wikipedia.org/wiki/HSL_and_HSV
    /// </summary>
    [Serializable]
    public struct ColorHSV
    {
        [SerializeField] private float _hue;
        [SerializeField] private float _saturation;
        [SerializeField] private float _value;
        [SerializeField] private float _alpha;

        #region Accessors

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
        
        public float HueDegrees
        {
            get => _hue * 360f;
            set
            {
                const float inverseDegrees = 1f / 360f;
                Hue = value * inverseDegrees;
            }
        }
        
        public float HueRadians
        {
            get => _hue * 2f * Mathf.PI;
            set
            {
                const float inverseRadians = 1f / (2f * Mathf.PI);
                Hue = value * inverseRadians;
            }
        }

        public float Saturation
        {
            get => _saturation;
            set => _saturation = Mathf.Clamp01(value);
        }

        public float Value
        {
            get => _value;
            set => _value = Mathf.Clamp01(value);
        }

        public float Alpha
        {
            get => _alpha;
            set => _alpha = Mathf.Clamp01(value);
        }

        public float this[int index]
        {
            get => index switch
            {
                0 => _hue,
                1 => _saturation,
                2 => _value,
                3 => _alpha,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch (index)
                {
                    case 0:
                        Hue = value;
                        break;
                    case 1:
                        Saturation = value;
                        break;
                    case 2:
                        Value = value;
                        break;
                    case 3:
                        Alpha = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion

        public ColorHSV(float hue = 0f, float saturation = 0f, float value = 0f, float alpha = 1f) : this()
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
            Alpha = alpha;
        }

        /// <summary>
        /// Returns the opposite color on the color wheel
        /// </summary>
        public ColorHSV complementary => new(Hue + 0.5f, Saturation, Value, Alpha);

        #region operators

        public static ColorHSV operator +(ColorHSV a, ColorHSV b) =>
            new(a.Hue + b.Hue, a.Saturation + b.Saturation, a.Value + b.Value, a.Alpha + b.Alpha);

        public static ColorHSV operator -(ColorHSV a, ColorHSV b) =>
            new(a.Hue - b.Hue, a.Saturation - b.Saturation, a.Value - b.Value, a.Alpha - b.Alpha);

        public static ColorHSV operator *(ColorHSV a, ColorHSV b) =>
            new(a.Hue * b.Hue, a.Saturation * b.Saturation, a.Value * b.Value, a.Alpha * b.Alpha);

        public static ColorHSV operator *(ColorHSV a, float b) =>
            new(a.Hue * b, a.Saturation * b, a.Value * b, a.Alpha * b);

        public static ColorHSV operator *(float a, ColorHSV b) =>
            new(a * b.Hue, a * b.Saturation, a * b.Value, a * b.Alpha);

        public static ColorHSV operator /(ColorHSV a, float b) =>
            new(a.Hue / b, a.Saturation / b, a.Value / b, a.Alpha / b);

        public static bool operator ==(ColorHSV a, ColorHSV b) =>
            a.Hue == b.Hue && a.Saturation == b.Saturation && a.Value == b.Value && a.Alpha == b.Alpha;

        public static bool operator !=(ColorHSV a, ColorHSV b) => !(a == b);

        public override bool Equals(object obj)
        {
            if (obj is ColorHSV hsv)
            {
                return this == hsv;
            }

            return false;
        }

        public override int GetHashCode() =>
            Hue.GetHashCode() ^ Saturation.GetHashCode() ^ Value.GetHashCode() ^ Alpha.GetHashCode();

        #endregion

        #region Conversions

        public static explicit operator Vector3(ColorHSV color) => new(color.Hue, color.Saturation, color.Value);

        public static explicit operator Vector4(ColorHSV color) =>
            new(color.Hue, color.Saturation, color.Value, color.Alpha);

        public static implicit operator Color(ColorHSV color)
        {
            var result = Color.HSVToRGB(color.Hue, color.Saturation, color.Value);
            result.a = color.Alpha;
            return result;
        }

        public static implicit operator ColorHSV(Color color)
        {
            Color.RGBToHSV(color, out var h, out var s, out var v);
            return new ColorHSV(h, s, v, color.a);
        }

        #endregion


        public override string ToString()
        {
            return $"HSV: {Hue:F3}, {Saturation:F3}, {Value:F3}, {Alpha:F3}";
        }
    }
}