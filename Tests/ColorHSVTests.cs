using System;
using LiteNinja.Colors.extensions;
using LiteNinja.Colors.Spaces;
using NUnit.Framework;
using UnityEngine;

namespace LiteNinja.Colors.Tests
{
    public class ColorHSVTests
    {
        [Test]
        public void RGBToHSV()
        {
            var mappings = new Tuple<Color, ColorHSV>[]
            {
                new(new Color(1f, 0f, 0f, 1f), new ColorHSV(0f, 1f, 1f, 1f)),
                new(new Color(1f, 1f, 0f, 1f), new ColorHSV(1f / 6f, 1f, 1f, 1f)),
                new(new Color(0f, 1f, 0f, 1f), new ColorHSV(1f / 3f, 1f, 1f, 1f)),
                new(new Color(0f, 1f, 1f, 1f), new ColorHSV(0.5f, 1f, 1f, 1f)),
                new(new Color(0f, 0f, 1f, 1f), new ColorHSV(2f / 3f, 1f, 1f, 1f)),
                new(new Color(1f, 0f, 1f, 1f), new ColorHSV(5f / 6f, 1f, 1f, 1f)),

                new(new Color(1f, 0.5f, 0.5f, 1f), new ColorHSV(0f, 0.5f, 1f, 1f)),
                new(new Color(1f, 1f, 0.5f, 1f), new ColorHSV(1f / 6f, 0.5f, 1f, 1f)),
                new(new Color(0.5f, 1f, 0.5f, 1f), new ColorHSV(1f / 3f, 0.5f, 1f, 1f)),
                new(new Color(0.5f, 1f, 1f, 1f), new ColorHSV(0.5f, 0.5f, 1f, 1f)),
                new(new Color(0.5f, 0.5f, 1f, 1f), new ColorHSV(2f / 3f, 0.5f, 1f, 1f)),
                new(new Color(1f, 0.5f, 1f, 1f), new ColorHSV(5f / 6f, 0.5f, 1f, 1f)),

                new(new Color(0.5f, 0f, 0f, 1f), new ColorHSV(0f, 1f, 0.5f, 1f)),
                new(new Color(0.5f, 0.5f, 0f, 1f), new ColorHSV(1f / 6f, 1f, 0.5f, 1f)),
                new(new Color(0f, 0.5f, 0f, 1f), new ColorHSV(1f / 3f, 1f, 0.5f, 1f)),
                new(new Color(0f, 0.5f, 0.5f, 1f), new ColorHSV(0.5f, 1f, 0.5f, 1f)),
                new(new Color(0f, 0f, 0.5f, 1f), new ColorHSV(2f / 3f, 1f, 0.5f, 1f)),
                new(new Color(0.5f, 0f, 0.5f, 1f), new ColorHSV(5f / 6f, 1f, 0.5f, 1f)),

                new(new Color(0f, 0f, 0f, 1f), new ColorHSV(0f, 0f, 0f, 1f)),
                new(new Color(1f, 1f, 1f, 1f), new ColorHSV(0f, 0f, 1f, 1f)),
                new(new Color(0.5f, 0.5f, 0.5f, 1f), new ColorHSV(0f, 0f, 0.5f, 1f)),
            };
            const float epsilon = 0.000001f;
            
            foreach (var (color, expected) in mappings)
            {
                var actual = (ColorHSV)color;
                Assert.IsTrue(expected.Approximately(actual, epsilon));
            }
        }

        [Test]
        public void HSVToRGB()
        {
            var mappings = new Tuple<Color, ColorHSV>[]
            {
                new(new Color(1f, 0f, 0f, 1f), new ColorHSV(0f, 1f, 1f, 1f)),
                new(new Color(1f, 1f, 0f, 1f), new ColorHSV(1f / 6f, 1f, 1f, 1f)),
                new(new Color(0f, 1f, 0f, 1f), new ColorHSV(1f / 3f, 1f, 1f, 1f)),
                new(new Color(0f, 1f, 1f, 1f), new ColorHSV(0.5f, 1f, 1f, 1f)),
                new(new Color(0f, 0f, 1f, 1f), new ColorHSV(2f / 3f, 1f, 1f, 1f)),
                new(new Color(1f, 0f, 1f, 1f), new ColorHSV(5f / 6f, 1f, 1f, 1f)),

                new(new Color(1f, 0.5f, 0.5f, 1f), new ColorHSV(0f, 0.5f, 1f, 1f)),
                new(new Color(1f, 1f, 0.5f, 1f), new ColorHSV(1f / 6f, 0.5f, 1f, 1f)),
                new(new Color(0.5f, 1f, 0.5f, 1f), new ColorHSV(1f / 3f, 0.5f, 1f, 1f)),
                new(new Color(0.5f, 1f, 1f, 1f), new ColorHSV(0.5f, 0.5f, 1f, 1f)),
                new(new Color(0.5f, 0.5f, 1f, 1f), new ColorHSV(2f / 3f, 0.5f, 1f, 1f)),
                new(new Color(1f, 0.5f, 1f, 1f), new ColorHSV(5f / 6f, 0.5f, 1f, 1f)),

                new(new Color(0.5f, 0f, 0f, 1f), new ColorHSV(0f, 1f, 0.5f, 1f)),
                new(new Color(0.5f, 0.5f, 0f, 1f), new ColorHSV(1f / 6f, 1f, 0.5f, 1f)),
                new(new Color(0f, 0.5f, 0f, 1f), new ColorHSV(1f / 3f, 1f, 0.5f, 1f)),
                new(new Color(0f, 0.5f, 0.5f, 1f), new ColorHSV(0.5f, 1f, 0.5f, 1f)),
                new(new Color(0f, 0f, 0.5f, 1f), new ColorHSV(2f / 3f, 1f, 0.5f, 1f)),
                new(new Color(0.5f, 0f, 0.5f, 1f), new ColorHSV(5f / 6f, 1f, 0.5f, 1f)),

                new(new Color(0f, 0f, 0f, 1f), new ColorHSV(0f, 0f, 0f, 1f)),
                new(new Color(1f, 1f, 1f, 1f), new ColorHSV(0f, 0f, 1f, 1f)),
                new(new Color(0.5f, 0.5f, 0.5f, 1f), new ColorHSV(0f, 0f, 0.5f, 1f)),
            };
            const float epsilon = 0.000001f;
            
            foreach (var (expected, hsv) in mappings)
            {
                var actual = (Color)hsv;
                Assert.IsTrue(expected.Approximately(actual, epsilon));
            }
        }
        
        [Test]
        public void Constructor_Should_Set_Alpha_To_One()
        {
            var color = new ColorHSV(0f, 0f, 0f);
            Assert.AreEqual(1f, color.Alpha);
        }

        [Test]
        public void Get_Hue_In_Degree()
        {
            var mappings = new[]
            {
                new Tuple<ColorHSV, float>(new ColorHSV(0f, 1f, 1f), 0f),
                new Tuple<ColorHSV, float>(new ColorHSV(0.5f, 1f, 1f), 180f),
                new Tuple<ColorHSV, float>(new ColorHSV(1f / 3f, 1f, 1f), 120f),
            };

            foreach (var (hsv, expected) in mappings)
            {
                Assert.AreEqual(expected, hsv.HueDegrees);
            }
        }

        [Test]
        public void Get_Hue_In_Radians()
        {
            var mappings = new[]
            {
                new Tuple<ColorHSV, float>(new ColorHSV(0f, 1f, 1f), 0f),
                new Tuple<ColorHSV, float>(new ColorHSV(0.5f, 1f, 1f), Mathf.PI),
                new Tuple<ColorHSV, float>(new ColorHSV(1f / 3f, 1f, 1f), Mathf.PI * 2f / 3f),
            };

            foreach (var (hsv, expected) in mappings)
            {
                Assert.AreEqual(expected, hsv.HueRadians);
            }
        }
        
        [Test]
        ///<summary>
        /// Test set hue in radians.
        /// </summary>
        public void Set_Hue_In_Radians()
        {
            var mappings = new[]
            {
                new Tuple<ColorHSV, float>(new ColorHSV(0f, 1f, 1f), 0f),
                new Tuple<ColorHSV, float>(new ColorHSV(0.5f, 1f, 1f), Mathf.PI),
                new Tuple<ColorHSV, float>(new ColorHSV(1f / 3f, 1f, 1f), Mathf.PI * 2f / 3f),
            };

            foreach (var (hsv, expected) in mappings)
            {
                var colorHSV = hsv;
                colorHSV.HueRadians = expected;
                Assert.AreEqual(expected, colorHSV.HueRadians);
            }
        }
        
        [Test]
        ///<summary>
        /// Test set hue in degrees.
        /// </summary>
        public void Set_Hue_In_Degree()
        {
            var mappings = new[]
            {
                new Tuple<ColorHSV, float>(new ColorHSV(0f, 1f, 1f), 0f),
                new Tuple<ColorHSV, float>(new ColorHSV(0.5f, 1f, 1f), 180f),
                new Tuple<ColorHSV, float>(new ColorHSV(1f / 3f, 1f, 1f), 120f),
            };

            foreach (var (hsv, expected) in mappings)
            {
                var colorHSV = hsv;
                colorHSV.HueDegrees = expected;
                Assert.AreEqual(expected, colorHSV.HueDegrees);
            }
        }
        
        [Test]
        ///<summary>
        /// Test complementary color.
        /// </summary>
        public void ComplementaryColor()
        {
            var mappings = new Tuple<ColorHSV, ColorHSV>[]
            {
                new(new ColorHSV(0f, 1f, 1f), new ColorHSV(0.5f, 1f, 1f)),
                new(new ColorHSV(0.25f, 1f, 1f), new ColorHSV(0.75f, 1f, 1f)),
                new(new ColorHSV(0.5f, 1f, 1f), new ColorHSV(0f, 1f, 1f)),
                new(new ColorHSV(0.75f, 1f, 1f), new ColorHSV(0.25f, 1f, 1f)),
            };

            foreach (var (hsv, expected) in mappings)
            {
                Assert.AreEqual(expected, hsv.Complementary());
            }
        }
    }
}