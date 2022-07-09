using System;
using System.ComponentModel;
using LiteNinja.Colors.Helpers;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorVisionExtensions
    {
        #region Matrices
        
        // TODO Check color blindess matrixes

        /// <summary>
        /// normal vision.
        /// </summary>
        private static readonly float[,] Normal =
        {
            { 1f, 0, 0 },
            { 0, 1f, 0 },
            { 0, 0, 1f }
        };

        /// <summary>
        /// achromatopsia color blindness
        /// </summary>
        private static readonly float[,] Achromatopsia =
        {
            { 0.299f, 0.587f, 0.114f },
            { 0.299f, 0.587f, 0.114f },
            { 0.299f, 0.587f, 0.114f }
        };

        /// <summary>
        /// achromatomaly color blindness.
        /// </summary>
        private static readonly float[,] Achromatomaly =
        {
            { 0.618f, 0.320f, 0.062f },
            { 0.163f, 0.775f, 0.062f },
            { 0.163f, 0.320f, 0.516f }
        };


        /// <summary>
        ///  deuteranopia color blindness
        /// </summary>
        private static readonly float[,] Deuteranopia =
        {
            { 0.625f, 0.375f, 0.000f },
            { 0.700f, 0.300f, 0.000f },
            { 0.000f, 0.300f, 0.700f }
        };

        /// <summary>
        /// protanopia color blindness.
        /// </summary>
        private static readonly float[,] Protanopia =
        {
            { 0.56667f, 0.43333f, 0.00000f },
            { 0.55833f, 0.44167f, 0.00000f },
            { 0.00000f, 0.24167f, 0.75833f }
        };

        /// <summary>
        /// protanomaly color blindness
        /// </summary>
        private static readonly float[,] Protanomaly =
        {
            { 0.81667f, 0.18333f, 0.00000f },
            { 0.33333f, 0.66667f, 0.00000f },
            { 0.00000f, 0.12500f, 0.87500f }
        };

        /// <summary>
        /// deuteranomaly color blindness
        /// </summary>
        private static readonly float[,] Deuteranomaly =
        {
            { 0.80000f, 0.20000f, 0.00000f },
            { 0.25833f, 0.74167f, 0.00000f },
            { 0.00000f, 0.14167f, 0.85833f }
        };

        /// <summary>
        /// tritanopia color blindness
        /// </summary>
        private static readonly float[,] Tritanopia =
        {
            { 0.95000f, 0.05000f, 0.00000f },
            { 0.00000f, 0.43333f, 0.56667f },
            { 0.00000f, 0.47500f, 0.52500f }
        };

        /// <summary>
        /// Tritanomaly color blindness
        /// </summary>
        private static readonly float[,] Tritanomaly =
        {
            { 0.96667f, 0.03333f, 0.00000f },
            { 0.00000f, 0.73333f, 0.26667f },
            { 0.00000f, 0.18333f, 0.81667f }
        };

        #endregion


        /// <summary>
        /// Applies a 3x3 matrix to the RGB channels of the color.
        /// </summary>
        public static Color ColorVision(this Color self, float[,] matrix)
        {
            var r = self.r;
            var g = self.g;
            var b = self.b;

            r = Mathf.Clamp01(r * matrix[0, 0] + g * matrix[0, 1] + b * matrix[0, 2]);
            g = Mathf.Clamp01(r * matrix[1, 0] + g * matrix[1, 1] + b * matrix[1, 2]);
            b = Mathf.Clamp01(r * matrix[2, 0] + g * matrix[2, 1] + b * matrix[2, 2]);

            return new Color(r, g, b, self.a);
        }

        /// <summary>
        /// Applies a 3x3 matrix to the RGB channels of the provided colors.
        /// </summary>
        public static Color[] ColorVision(this Color[] self, float[,] matrix)
        {
            var result = new Color[self.Length];
            for (var i = 0; i < self.Length; i++)
            {
                result[i] = ColorVision(self[i], matrix);
            }

            return result;
        }

        public static float[,] ColorVisionMatrix(this ColorVisionType self)
        {
            if (!Enum.IsDefined(typeof(ColorVisionType), self))
                throw new InvalidEnumArgumentException(nameof(self), (int)self, typeof(ColorVisionType));
            switch (self)
            {
                case ColorVisionType.Normal:
                    return Normal;
                case ColorVisionType.Achromatomaly:
                    return Achromatomaly;
                case ColorVisionType.Achromatopsia:
                    return Achromatopsia;
                case ColorVisionType.Deuteranomaly:
                    return Deuteranomaly;
                case ColorVisionType.Deuteranopia:
                    return Deuteranopia;
                case ColorVisionType.Protanomaly:
                    return Protanomaly;
                case ColorVisionType.Protanopia:
                    return Protanopia;
                case ColorVisionType.Tritanomaly:
                    return Tritanomaly;
                case ColorVisionType.Tritanopia:
                    return Tritanopia;
                default:
                    return Normal;
            }
        }

        /// <summary>
        /// Applies the Vision matrix to the color.
        /// </summary>
        public static Color ColorVision(this Color self, ColorVisionType colorVision)
        {
            var matrix = colorVision.ColorVisionMatrix();
            return self.ColorVision(matrix);
        }

        /// <summary>
        /// Applys the Vision matrix to the colors.
        /// </summary>
        public static Color[] ColorVision(this Color[] self, ColorVisionType colorVision)
        {
            var matrix = colorVision.ColorVisionMatrix();
            return self.ColorVision(matrix);
        }
    }
}