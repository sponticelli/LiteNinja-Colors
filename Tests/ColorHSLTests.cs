using System;
using System.Collections;
using LiteNinja.Colors.extensions;
using LiteNinja.Colors.Spaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LiteNinja.Colors.Tests
{
    public class ColorHSLTests
    {
        [Test]
        public void Constructor_Should_Set_Correct_Values()
        {
            var hsl = new ColorHSL(0.5f, 0.5f, 0.5f);
            Assert.AreEqual(0.5f, hsl.Hue);
            Assert.AreEqual(0.5f, hsl.Saturation);
            Assert.AreEqual(0.5f, hsl.Lightness);
            Assert.AreEqual(1f, hsl.Alpha);
        }


        [Test]
        public void HSLValueRange()
        {
            var color = new ColorHSL(1f, 0.5f, 0.75f);
            Debug.Log(color);
            Assert.AreEqual(0f, color.Hue); // hue value is circular, so it wraps around
            Assert.AreEqual(0.5f, color.Saturation);
            Assert.AreEqual(0.75f, color.Lightness);

            color = new ColorHSL(1.5f, -0.5f, 1.75f);
            Assert.AreEqual(0.5f,
                Mathf.Round(color.Hue * 100f) / 100f); // hue value is circular, so it wraps around));
            Assert.AreEqual(0.0f, color.Saturation);
            Assert.AreEqual(1.0f, color.Lightness);
        }

        [Test]
        // <summary>
        /// Test that the conversion from HSL to RGB is correct using a map of known values.
        /// </summary>
        public void HSLToRGB()
        {
            Tuple<Color, ColorHSL>[] mappings =
            {
                new(new Color(1f, 0f, 0f, 1f), new ColorHSL(0f, 1f, 0.5f, 1f)),
                new(new Color(1f, 1f, 0f, 1f), new ColorHSL(1f / 6f, 1f, 0.5f, 1f)),
                new(new Color(0f, 1f, 0f, 1f), new ColorHSL(1f / 3f, 1f, 0.5f, 1f)),
                new(new Color(0f, 1f, 1f, 1f), new ColorHSL(0.5f, 1f, 0.5f, 1f)),
                new(new Color(0f, 0f, 1f, 1f), new ColorHSL(2f / 3f, 1f, 0.5f, 1f)),
                new(new Color(1f, 0f, 1f, 1f), new ColorHSL(5f / 6f, 1f, 0.5f, 1f)),

                new(new Color(1f, 0.5f, 0.5f, 1f), new ColorHSL(0f, 1f, 0.75f, 1f)),
                new(new Color(1f, 1f, 0.5f, 1f), new ColorHSL(1f / 6f, 1f, 0.75f, 1f)),
                new(new Color(0.5f, 1f, 0.5f, 1f), new ColorHSL(1f / 3f, 1f, 0.75f, 1f)),
                new(new Color(0.5f, 1f, 1f, 1f), new ColorHSL(0.5f, 1f, 0.75f, 1f)),
                new(new Color(0.5f, 0.5f, 1f, 1f), new ColorHSL(2f / 3f, 1f, 0.75f, 1f)),
                new(new Color(1f, 0.5f, 1f, 1f), new ColorHSL(5f / 6f, 1f, 0.75f, 1f)),

                new(new Color(0.5f, 0f, 0f, 1f), new ColorHSL(0f, 1f, 0.25f, 1f)),
                new(new Color(0.5f, 0.5f, 0f, 1f), new ColorHSL(1f / 6f, 1f, 0.25f, 1f)),
                new(new Color(0f, 0.5f, 0f, 1f), new ColorHSL(1f / 3f, 1f, 0.25f, 1f)),
                new(new Color(0f, 0.5f, 0.5f, 1f), new ColorHSL(0.5f, 1f, 0.25f, 1f)),
                new(new Color(0f, 0f, 0.5f, 1f), new ColorHSL(2f / 3f, 1f, 0.25f, 1f)),
                new(new Color(0.5f, 0f, 0.5f, 1f), new ColorHSL(5f / 6f, 1f, 0.25f, 1f)),

                new(new Color(0f, 0f, 0f, 1f), new ColorHSL(0f, 0f, 0f, 1f)),
                new(new Color(1f, 1f, 1f, 1f), new ColorHSL(0f, 0f, 1f, 1f)),
                new(new Color(0.5f, 0.5f, 0.5f, 1f), new ColorHSL(0f, 0f, 0.5f, 1f)),
            };
            const float epsilon = 0.000001f;
            foreach (var (rgb, hsl) in mappings)
            {
                var rgb2 = (Color)hsl;
                Assert.IsTrue(rgb.Approximately(rgb2, epsilon));
            }
        }


        [Test]
        /// <summary>
        /// Test that the conversion from RGB to HSL is correct using a map of known values.
        /// </summary>
        public void RGBToHSL()
        {
            Tuple<Color, ColorHSL>[] mappings =
            {
                new(new Color(1f, 0f, 0f, 1f), new ColorHSL(0f, 1f, 0.5f, 1f)),
                new(new Color(1f, 1f, 0f, 1f), new ColorHSL(1f / 6f, 1f, 0.5f, 1f)),
                new(new Color(0f, 1f, 0f, 1f), new ColorHSL(1f / 3f, 1f, 0.5f, 1f)),
                new(new Color(0f, 1f, 1f, 1f), new ColorHSL(0.5f, 1f, 0.5f, 1f)),
                new(new Color(0f, 0f, 1f, 1f), new ColorHSL(2f / 3f, 1f, 0.5f, 1f)),
                new(new Color(1f, 0f, 1f, 1f), new ColorHSL(5f / 6f, 1f, 0.5f, 1f)),

                new(new Color(1f, 0.5f, 0.5f, 1f), new ColorHSL(0f, 1f, 0.75f, 1f)),
                new(new Color(1f, 1f, 0.5f, 1f), new ColorHSL(1f / 6f, 1f, 0.75f, 1f)),
                new(new Color(0.5f, 1f, 0.5f, 1f), new ColorHSL(1f / 3f, 1f, 0.75f, 1f)),
                new(new Color(0.5f, 1f, 1f, 1f), new ColorHSL(0.5f, 1f, 0.75f, 1f)),
                new(new Color(0.5f, 0.5f, 1f, 1f), new ColorHSL(2f / 3f, 1f, 0.75f, 1f)),
                new(new Color(1f, 0.5f, 1f, 1f), new ColorHSL(5f / 6f, 1f, 0.75f, 1f)),

                new(new Color(0.5f, 0f, 0f, 1f), new ColorHSL(0f, 1f, 0.25f, 1f)),
                new(new Color(0.5f, 0.5f, 0f, 1f), new ColorHSL(1f / 6f, 1f, 0.25f, 1f)),
                new(new Color(0f, 0.5f, 0f, 1f), new ColorHSL(1f / 3f, 1f, 0.25f, 1f)),
                new(new Color(0f, 0.5f, 0.5f, 1f), new ColorHSL(0.5f, 1f, 0.25f, 1f)),
                new(new Color(0f, 0f, 0.5f, 1f), new ColorHSL(2f / 3f, 1f, 0.25f, 1f)),
                new(new Color(0.5f, 0f, 0.5f, 1f), new ColorHSL(5f / 6f, 1f, 0.25f, 1f)),

                new(new Color(0f, 0f, 0f, 1f), new ColorHSL(0f, 0f, 0f, 1f)),
                new(new Color(1f, 1f, 1f, 1f), new ColorHSL(0f, 0f, 1f, 1f)),
                new(new Color(0.5f, 0.5f, 0.5f, 1f), new ColorHSL(0f, 0f, 0.5f, 1f)),
            };
            const float epsilon = 0.000001f;
            foreach (var (rgb, hsl) in mappings)
            {
                var hsl2 = (ColorHSL)rgb;
                Assert.IsTrue(hsl.Approximately(hsl2, epsilon));
            }
        }

        [Test]
        /// <summary>
        /// Test that get HueDegrees is correct.
        /// </summary>
        public void Get_Hue_In_Degree()
        {
            var mappings = new[]
            {
                new Tuple<ColorHSL, float>(new ColorHSL(0f, 1f, 1f), 0f),
                new Tuple<ColorHSL, float>(new ColorHSL(0.5f, 1f, 1f), 180f),
                new Tuple<ColorHSL, float>(new ColorHSL(1f / 3f, 1f, 1f), 120f),
            };

            foreach (var (hsl, expected) in mappings)
            {
                Assert.AreEqual(expected, hsl.HueDegrees);
            }
        }

        [Test]
        /// <summary>
        /// Test that get HueRadians is correct.
        /// </summary>
        public void Get_Hue_In_Radians()
        {
            var mappings = new[]
            {
                new Tuple<ColorHSL, float>(new ColorHSL(0f, 1f, 1f), 0f),
                new Tuple<ColorHSL, float>(new ColorHSL(0.5f, 1f, 1f), Mathf.PI),
                new Tuple<ColorHSL, float>(new ColorHSL(1f / 3f, 1f, 1f), Mathf.PI * 2f / 3f),
            };

            foreach (var (hsl, expected) in mappings)
            {
                Assert.AreEqual(expected, hsl.HueRadians);
            }
        }

        [Test]
        /// <summary>
        /// Test that set HueDegrees is correct.
        /// </summary>
        public void Set_Hue_In_Degrees()
        {
            var mappings = new[]
            {
                new Tuple<ColorHSL, float, float>(new ColorHSL(0f, 1f, 1f), 0f, 0f),
                new Tuple<ColorHSL, float, float>(new ColorHSL(0f, 1f, 1f), 180f, 0.5f),
                new Tuple<ColorHSL, float, float>(new ColorHSL(0f, 1f, 1f), 120f, 1f / 3f),
            };

            foreach (var (hsl, degrees, expected) in mappings)
            {
                var colorHsl = hsl;
                colorHsl.HueDegrees = degrees;
                Assert.AreEqual(expected, colorHsl.Hue);
            }
        }

        [Test]
        /// <summary>
        /// Test that set HueRadians is correct.
        /// </summary>
        public void Set_Hue_In_Radians()
        {
            var mappings = new Tuple<ColorHSL, float, float>[]
            {
                new(new ColorHSL(0f, 1f, 1f), 0f, 0f),
                new(new ColorHSL(0f, 1f, 1f), Mathf.PI, 0.5f),
                new(new ColorHSL(0f, 1f, 1f), Mathf.PI * 2f / 3f, 1f / 3f),
            };

            foreach (var (hsl, radians, expected) in mappings)
            {
                var colorHsl = hsl;
                colorHsl.HueRadians = radians;
                Assert.AreEqual(expected, colorHsl.Hue);
            }
        }

        [Test]
        /// <summary>
        /// Test complementary color using a map of known values.
        /// </summary>
        public void ComplementaryColor()
        {
            var mappings = new Tuple<ColorHSL, ColorHSL>[]
            {
                new(new ColorHSL(0f, 1f, 1f), new ColorHSL(0.5f, 1f, 1f)),
                new(new ColorHSL(0.25f, 1f, 1f), new ColorHSL(0.75f, 1f, 1f)),
                new(new ColorHSL(0.5f, 1f, 1f), new ColorHSL(0f, 1f, 1f)),
                new(new ColorHSL(0.75f, 1f, 1f), new ColorHSL(0.25f, 1f, 1f)),
            };
            const float epsilon = 0.000001f;

            foreach (var (hsl, expected) in mappings)
            {
                var complementary = hsl.Complementary();
                Assert.IsTrue(expected.Approximately(complementary, epsilon));
            }
        }
        
    }
}