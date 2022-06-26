using LiteNinja.Colors.Spaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LiteNinja.Colors.Tests
{


    public class ColorHSLTests
    {

        [Test]
        public void HSLValueRange()
        {
            var color = new ColorHSL(1f, 0.5f, 0.75f);
            Debug.Log(color);
            Assert.AreEqual(0f, color.Hue); // hue value is circular, so it wraps around
            Assert.AreEqual(0.5f, color.Saturation);
            Assert.AreEqual(0.75f, color.Lightness);
            
            color = new ColorHSL(1.5f, -0.5f, 1.75f);
            Assert.AreEqual(0.5f, Mathf.Round(color.Hue * 100f) / 100f); // hue value is circular, so it wraps aroundf));
            Assert.AreEqual(0.0f, color.Saturation);
            Assert.AreEqual(1.0f, color.Lightness);
        }
        
        [Test]
        public void HSLToRGB()
        {
            var color = ColorHSL.Black;
            Color rgb = color;
            Assert.AreEqual(0f, rgb.r);
            Assert.AreEqual(0f, rgb.g);
            Assert.AreEqual(0f, rgb.b);
            
            color = ColorHSL.White;
            rgb = color;
            Assert.AreEqual(1f, rgb.r);
            Assert.AreEqual(1f, rgb.g);
            Assert.AreEqual(1f, rgb.b);
        }

    }
}