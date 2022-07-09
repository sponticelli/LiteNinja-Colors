using LiteNinja.Colors.Spaces;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorSaturationExtensions
    {
        public static float GetSaturation(this Color self)
        {
            return ((ColorHSL) self).Saturation;
        }
        
        public static Color Saturate(this Color self, float amount =0.1f)
        {
            ColorHSV colorHsv = self;
            colorHsv.Saturation += amount;
            return colorHsv;
        }
        

        /// <summary>
        /// Generates a given amount of saturated colors from the base color.
        /// </summary>
        public static Color[] Saturate(this Color baseColor, int numColors)
        {
            var range = 1f - baseColor.GetSaturation();
            var delta = range / Mathf.Max(numColors - 1, 1);
            var colors = new Color[numColors];
            for (var i = 0; i < numColors; i++)
            {
                colors[i] = baseColor.Saturate(delta * i);
            }
            return colors;
        }

        /// <summary>
        /// Fills an existing array with saturated colors of the base color to prevent heap allocations. 
        /// </summary>
        public static void SaturateNonAlloc(this Color baseColor, Color[] output)
        {
            var range = 1f - baseColor.GetSaturation();
            var delta = range / Mathf.Max(output.Length - 1, 1);
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = baseColor.Saturate(delta * i);
            }
        }
        

        /// <summary>
        /// Returns a new instance of the color with decreased saturation.
        /// </summary>
        public static Color Desaturate(this Color color, float decrease = 0.1f)
        {
            ColorHSL  hsl = color;
            hsl.Saturation -=  decrease;
            return hsl;
        }

        /// <summary>
        /// Generates a given amount of desaturated colors from the base color.
        /// </summary>
        public static Color[] Desaturate(this Color baseColor, int numColors)
        {
            var range = baseColor.GetSaturation();
            var delta = range / Mathf.Max(numColors - 1, 1);
            
            var colors = new Color[numColors];
            for (var i = 0; i < numColors; i++)
            {
                colors[i] = baseColor.Desaturate(delta * i);
            }
            return colors;
        }

        /// <summary>
        /// Fills an existing array with desaturated colors of the base color to prevent heap allocations.
        /// </summary>
        public static void DesaturateNonAlloc(this Color baseColor, Color[] output)
        {
            var range = baseColor.GetSaturation();
            var delta = range / Mathf.Max(output.Length - 1, 1);
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = baseColor.Desaturate(delta * i);
            }
        }


    }
}