using System;
using LiteNinja.Colors.extensions;
using NUnit.Framework;
using UnityEngine;

namespace LiteNinja.Colors.Tests
{
    public class ColorTests
    {
        [Test]
        /// <summary>
        /// Test invert color using a map of known values.
        /// </summary>
        public void InvertColor()
        {
            var mappings = new[]
            {
                new Tuple<Color, Color>(new Color(0f, 0.5f, 1f), new Color(1f, 0.5f, 0f)),
                new Tuple<Color, Color>(new Color(0.5f, 1f, 0f), new Color(0.5f, 0f, 1f)),
                new Tuple<Color, Color>(new Color(1f, 0f, 0.5f), new Color(0f, 1f, 0.5f)),
                new Tuple<Color, Color>(new Color(0.25f, 0.67f, 0.75f), new Color(0.75f, 0.33f, 0.25f)),
                new Tuple<Color, Color>(new Color(0.67f, 0.75f, 0.25f), new Color(0.33f, 0.25f, 0.75f)),
                new Tuple<Color, Color>(new Color(0.75f, 0.25f, 0.67f), new Color(0.25f, 0.75f, 0.33f)),
            };
            const float epsilon = 0.000001f;
            
            foreach (var (hsl, expected) in mappings)
            {
                var inverted = hsl.Invert();
                Assert.IsTrue(expected.Approximately(inverted, epsilon));
            }
        }
    }
}