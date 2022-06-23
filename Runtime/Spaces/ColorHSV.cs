using System;
using UnityEngine;

namespace com.liteninja.colors.spaces
{
    /// <summary>
    /// A struct that represents a color in the HSV color space.
    /// Also known as "HSB" color space.
    /// H = hue, S = saturation, V = value, A = alpha
    /// @ref https://en.wikipedia.org/wiki/HSL_and_HSV
    /// </summary>
    public struct ColorHSV
    {
        private float _h;
        private float _s;
        private float _v;
        private float _alpha;

        #region Accessors

        public float H
        {
            get => _h;
            set
            {
                value %= 1f;
                if (value < 0f)
                {
                    value += 1f;
                }

                _h = value;
            }
        }

        public float S
        {
            get => _s;
            set => _s = Mathf.Clamp01(value);
        }

        public float V
        {
            get => _v;
            set => _v = Mathf.Clamp01(value);
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
                0 => _h,
                1 => _s,
                2 => _v,
                3 => _alpha,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch (index)
                {
                    case 0:
                        H = value;
                        break;
                    case 1:
                        S = value;
                        break;
                    case 2:
                        V = value;
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

        public ColorHSV(float h = 0f, float s = 0f, float v = 0f, float alpha = 1f) : this()
        {
            H = h;
            S = s;
            V = v;
            Alpha = alpha;
        }

        /// <summary>
        /// Returns the opposite color on the color wheel
        /// </summary>
        public ColorHSV complementary => new(H + 0.5f, S, V, Alpha);

        #region operators

        

        public static ColorHSV operator +(ColorHSV a, ColorHSV b) =>
            new(a.H + b.H, a.S + b.S, a.V + b.V, a.Alpha + b.Alpha);

        public static ColorHSV operator -(ColorHSV a, ColorHSV b) =>
            new(a.H - b.H, a.S - b.S, a.V - b.V, a.Alpha - b.Alpha);

        public static ColorHSV operator *(ColorHSV a, ColorHSV b) =>
            new(a.H * b.H, a.S * b.S, a.V * b.V, a.Alpha * b.Alpha);

        public static ColorHSV operator *(ColorHSV a, float b) => new(a.H * b, a.S * b, a.V * b, a.Alpha * b);
        public static ColorHSV operator *(float a, ColorHSV b) => new(a * b.H, a * b.S, a * b.V, a * b.Alpha);
        public static ColorHSV operator /(ColorHSV a, float b) => new(a.H / b, a.S / b, a.V / b, a.Alpha / b);

        public static bool operator ==(ColorHSV a, ColorHSV b) =>
            a.H == b.H && a.S == b.S && a.V == b.V && a.Alpha == b.Alpha;

        public static bool operator !=(ColorHSV a, ColorHSV b) => !(a == b);

        public override bool Equals(object obj)
        {
            if (obj is ColorHSV hsv)
            {
                return this == hsv;
            }

            return false;
        }

        public override int GetHashCode() => H.GetHashCode() ^ S.GetHashCode() ^ V.GetHashCode() ^ Alpha.GetHashCode();

        #endregion

        #region Conversions
        public static explicit operator Vector3(ColorHSV color) => new(color.H, color.S, color.V);
        public static explicit operator Vector4(ColorHSV color) => new(color.H, color.S, color.V, color.Alpha);

        public static implicit operator Color(ColorHSV color)
        {
            var result = Color.HSVToRGB(color.H, color.S, color.V);
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
            return $"HSV: {H:F3}, {S:F3}, {V:F3}, {Alpha:F3}";
        }

        
    }
}