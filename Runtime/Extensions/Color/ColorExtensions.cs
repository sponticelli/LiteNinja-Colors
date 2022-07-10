using System;
using LiteNinja_Colors.Runtime;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorExtensions
    {
        public static readonly Color transparentBlack = new Color(0f, 0f, 0f, 0f);

        /// <summary>
        /// Find the closest color name in the CssColors dictionary.
        /// </summary>
        
        public static string ToCssColor(this Color color)
        {
           return ColorNameHelper.FindClosestName(color);
        }

        /// <summary>
        /// Returns the nearest websafe color to the current color.
        /// </summary>
        public static Color WebSafe(this Color color)
        {
            const float fiftyOneOver255 = 51f / 255f;
            var r = Mathf.Round(color.r * 5f);
            var g = Mathf.Round(color.g * 5f);
            var b = Mathf.Round(color.b * 5f);

            return new Color(r * fiftyOneOver255, g * fiftyOneOver255, b * fiftyOneOver255);
        }

        /// <summary>
        /// Returns the CCT temperature of the color in Kelvin.
        /// </summary>
        public static float Cct(this Color self)
        {
            //convert to XYZ 
            var r = Mathf.GammaToLinearSpace(self.r);
            var g = Mathf.GammaToLinearSpace(self.g);
            var b = Mathf.GammaToLinearSpace(self.b);

            var x = 100f * (0.4124564f * r + 0.3575761f * g + 0.1804375f * b);
            var y = 100f * (0.2126729f * r + 0.7151522f * g + 0.0721750f * b);
            var z = 100f * (0.0193339f * r + 0.1191920f * g + 0.9503041f * b);

            //convert to UCS
            var u = 4f * x / (x + 15f * y + 3f * z);
            var v = 6f * y / (x + 15f * y + 3f * z);

            //convert to CCT
            var uvt = new (float u, float v, float t)[]
            {
                (0.18006f, 0.26352f, -0.24341f),
                (0.18066f, 0.26589f, -0.25479f),
                (0.18133f, 0.26846f, -0.26876f),
                (0.18208f, 0.27119f, -0.28539f),
                (0.18293f, 0.27407f, -0.30470f),
                (0.18388f, 0.27709f, -0.32675f),
                (0.18494f, 0.28021f, -0.35156f),
                (0.18611f, 0.28342f, -0.37915f),
                (0.18740f, 0.28668f, -0.40955f),
                (0.18880f, 0.28997f, -0.44278f),
                (0.19032f, 0.29326f, -0.47888f),
                (0.19462f, 0.30141f, -0.58204f),
                (0.19962f, 0.30921f, -0.70471f),
                (0.20525f, 0.31647f, -0.84901f),
                (0.21142f, 0.32312f, -1.0182f),
                (0.21807f, 0.32909f, -1.2168f),
                (0.22511f, 0.33439f, -1.4512f),
                (0.23247f, 0.33904f, -1.7298f),
                (0.24010f, 0.34308f, -2.0637f),
                (0.24792f, 0.34655f, -2.4681f),
                (0.25591f, 0.34951f, -2.9641f),
                (0.26400f, 0.35200f, -3.5814f),
                (0.27218f, 0.35407f, -4.3633f),
                (0.28039f, 0.35577f, -5.3762f),
                (0.28863f, 0.35714f, -6.7262f),
                (0.29685f, 0.35823f, -8.5955f),
                (0.30505f, 0.35907f, -11.324f),
                (0.31320f, 0.35968f, -15.628f),
                (0.32129f, 0.36011f, -23.325f),
                (0.32931f, 0.36038f, -40.770f),
                (0.33724f, 0.36051f, -116.45f),
            };

            var rt = new[]
            {
                float.Epsilon,
                10.0e-6f, 20.0e-6f, 30.0e-6f, 40.0e-6f, 50.0e-6f, 60.0e-6f,
                70.0e-6f, 80.0e-6f, 90.0e-6f, 100.0e-6f, 125.0e-6f, 150.0e-6f,
                175.0e-6f, 200.0e-6f, 225.0e-6f, 250.0e-6f, 275.0e-6f, 300.0e-6f,
                325.0e-6f, 350.0e-6f, 375.0e-6f, 400.0e-6f, 425.0e-6f, 450.0e-6f,
                475.0e-6f, 500.0e-6f, 525.0e-6f, 550.0e-6f, 575.0e-6f, 600.0e-6f,
            };


            var di = 0f;
            var dm = 0f;

            var index = 0;
            for (var i = 0; i < uvt.Length; i++)
            {
                index = i;
                di = v - uvt[i].v - uvt[i].t * (u - uvt[i].u);
                if (i > 0 && ((di < 0f && dm >= 0f) || (di >= 0f && dm < 0f))) break;
                dm = di;
            }

            di /= Mathf.Sqrt(1f + uvt[index].t * uvt[index].t);
            dm /= Mathf.Sqrt(1f + uvt[index - 1].t * uvt[index - 1].t);

            var ratio = dm / (dm - di);
            return 1f / Mathf.Lerp(rt[index - 1], rt[index], ratio);
        }
    }
}