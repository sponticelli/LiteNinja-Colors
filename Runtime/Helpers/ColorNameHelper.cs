using System.Collections.Generic;
using UnityEngine;

namespace LiteNinja_Colors.Runtime
{
    public static class ColorNameHelper
    {
        
        // Dictionary with all the colors and their names.
        public static readonly Dictionary<string, Color> CssColors = new()
        {
            { "alice blue", new Color(0.9411765f, 0.972549f, 1f) },
            { "antique white", new Color(0.98039216f, 0.9215686f, 0.84313726f) },
            { "aqua", new Color(0f, 1f, 1f) },
            { "aquamarine", new Color(0.49803922f, 1f, 0.83137256f) },
            { "azure", new Color(0.9411765f, 1f, 1f) },
            { "beige", new Color(0.9607843f, 0.9607843f, 0.8627451f) },
            { "bisque", new Color(1f, 0.89411765f, 0.7686274f) },
            { "black", new Color(0f, 0f, 0f) },
            { "blanched almond", new Color(1f, 0.9215686f, 0.80392158f) },
            { "blue", new Color(0f, 0f, 1f) },
            { "blue violet", new Color(0.5411765f, 0.16862746f, 0.8862745f) },
            { "brown", new Color(0.64705884f, 0.16470589f, 0.16470589f) },
            { "burly wood", new Color(0.87058824f, 0.72156864f, 0.5294118f) },
            { "cadet blue", new Color(0.3764706f, 0.61960787f, 0.627451f) },
            { "chart reuse", new Color(0.49803922f, 1f, 0.0f) },
            { "chocolate", new Color(0.8235294f, 0.4117647f, 0.11764706f) },
            { "coral", new Color(1f, 0.49803922f, 0.3137255f) },
            { "corn flower blue", new Color(0.39215687f, 0.58431375f, 0.9294118f) },
            { "corn silk", new Color(1f, 0.972549f, 0.8627451f) },
            { "crimson", new Color(0.8627451f, 0.078431375f, 0.23529412f) },
            { "cyan", new Color(0f, 1f, 1f) },
            { "dark blue", new Color(0.0f, 0.0f, 0.5529412f) },
            { "dark cyan", new Color(0.0f, 0.54509807f, 0.54509807f) },
            { "dark goldenrod", new Color(0.72156864f, 0.5254902f, 0.043137256f) },
            { "dark gray", new Color(0.6627451f, 0.6627451f, 0.6627451f) },
            { "dark green", new Color(0.0f, 0.39215687f, 0.0f) },
            { "dark grey", new Color(0.6627451f, 0.6627451f, 0.6627451f) },
            { "dark khaki", new Color(0.7411765f, 0.7176471f, 0.41960785f) },
            { "dark magenta", new Color(0.54509807f, 0.0f, 0.54509807f) },
            { "dark olive green", new Color(0.33333334f, 0.41960785f, 0.18431373f) },
            { "dark orange", new Color(1f, 0.54901963f, 0f) },
            { "dark orchid", new Color(0.6f, 0.19607843f, 0.8f) },
            { "dark red", new Color(0.54509807f, 0.0f, 0.0f) },
            { "dark salmon", new Color(0.9137255f, 0.5882353f, 0.47843137f) },
            { "dark sea green", new Color(0.5607843f, 0.7372549f, 0.5607843f) },
            { "dark slate blue", new Color(0.28235295f, 0.23921569f, 0.54509807f) },
            { "dark slate gray", new Color(0.18431373f, 0.30980393f, 0.30980393f) },
            { "dark slate grey", new Color(0.18431373f, 0.30980393f, 0.30980393f) },
            { "dark turquoise", new Color(0.0f, 0.80784315f, 0.81960785f) },
            { "dark violet", new Color(0.5803922f, 0.0f, 0.827451f) },
            { "deep pink", new Color(1f, 0.078431375f, 0.5764706f) },
            { "deep sky blue", new Color(0.0f, 0.7490196f, 1f) },
            { "dim gray", new Color(0.4117647f, 0.4117647f, 0.4117647f) },
            { "dim grey", new Color(0.4117647f, 0.4117647f, 0.4117647f) },
            { "dodger blue", new Color(0.11764706f, 0.5647059f, 1f) },
            { "fire brick", new Color(0.69803923f, 0.13333334f, 0.13333334f) },
            { "floral white", new Color(1f, 0.98039216f, 0.9019608f) },
            { "forest green", new Color(0.13333334f, 0.54509807f, 0.13333334f) },
            { "fuchsia", new Color(1f, 0.0f, 1f) },
            { "gainsboro", new Color(0.8627451f, 0.8627451f, 0.8627451f) },
            { "ghost white", new Color(0.972549f, 0.972549f, 1f) },
            { "gold", new Color(1f, 0.84313726f, 0.0f) },
            { "golden rod", new Color(0.85490197f, 0.64705884f, 0.1254902f) },
            { "gray", new Color(0.5019608f, 0.5019608f, 0.5019608f) },
            { "green", new Color(0.0f, 0.5019608f, 0.0f) },
            { "green yellow", new Color(0.6784314f, 1f, 0.18431373f) },
            { "grey", new Color(0.5019608f, 0.5019608f, 0.5019608f) },
            { "honey dew", new Color(0.9411765f, 1f, 0.9411765f) },
            { "hot pink", new Color(1f, 0.4117647f, 0.7058824f) },
            { "indian red", new Color(0.8039216f, 0.3607843f, 0.3607843f) },
            { "indigo", new Color(0.29411766f, 0.0f, 0.50980395f) },
            { "ivory", new Color(1f, 1f, 0.9411765f) },
            { "khaki", new Color(0.9411765f, 0.9019608f, 0.54901963f) },
            { "lavender", new Color(0.9019608f, 0.9019608f, 0.98039216f) },
            { "lavender blush", new Color(1f, 0.9411765f, 0.9607843f) },
            { "lawn green", new Color(0.4862745f, 0.9882353f, 0.0f) },
            { "lemon chiffon", new Color(1f, 0.98039216f, 0.8039216f) },
            { "light blue", new Color(0.678f, 0.847f, 0.902f) },
            { "light coral", new Color(0.9411765f, 0.5019608f, 0.5019608f) },
            { "light cyan", new Color(0.8784314f, 1f, 1f) },
            { "light golden rod yellow", new Color(0.98039216f, 0.98039216f, 0.8235294f) },
            { "light gray", new Color(0.827451f, 0.827451f, 0.827451f) },
            { "light green", new Color(0.5647059f, 0.93333334f, 0.5647059f) },
            { "light grey", new Color(0.827451f, 0.827451f, 0.827451f) },
            { "light pink", new Color(1f, 0.7137255f, 0.75686276f) },
            { "light salmon", new Color(1f, 0.627451f, 0.47843137f) },
            { "light sea green", new Color(0.1254902f, 0.69803923f, 0.6666667f) },
            { "light sky blue", new Color(0.5294118f, 0.80784315f, 0.98039216f) },
            { "light slate gray", new Color(0.46666667f, 0.53333336f, 0.6f) },
            { "light slate grey", new Color(0.46666667f, 0.53333336f, 0.6f) },
            { "lightsteelblue", new Color(0.6901961f, 0.76862746f, 0.87058824f) },
            { "light yellow", new Color(1f, 1f, 0.87f) },
            { "lime", new Color(0.0f, 1f, 0.0f) },
            { "lime green", new Color(0.19607843f, 0.8039216f, 0.19607843f) },
            { "linen", new Color(0.98f, 0.941f, 0.902f) },
            { "magenta", new Color(1f, 0.0f, 1f) },
            { "maroon", new Color(0.5019608f, 0.0f, 0.0f) },
            { "medium aquamarine", new Color(0.4f, 0.8039216f, 0.6666667f) },
            { "medium blue", new Color(0.0f, 0.0f, 0.8039216f) },
            { "medium orchid", new Color(0.7294118f, 0.33333334f, 0.827451f) },
            { "medium purple", new Color(0.5764706f, 0.4392157f, 0.85882354f) },
            { "medium sea green", new Color(0.23529412f, 0.701961f, 0.44313726f) },
            { "medium slate blue", new Color(0.48235294f, 0.40784314f, 0.93333334f) },
            { "medium spring green", new Color(0.0f, 0.98039216f, 0.6039216f) },
            { "medium turquoise", new Color(0.28235295f, 0.81960785f, 0.8f) },
            { "medium violet red", new Color(0.78039217f, 0.08235294f, 0.52156866f) },
            { "midnight blue", new Color(0.09803922f, 0.09803922f, 0.42352942f) },
            { "mint cream", new Color(0.9607843f, 1f, 0.98039216f) },
            { "misty rose", new Color(1f, 0.89411765f, 0.88235295f) },
            { "moccasin", new Color(1f, 0.89411765f, 0.70980394f) },
            { "navajo white", new Color(1f, 0.87058824f, 0.6784314f) },
            { "navy", new Color(0.0f, 0.0f, 0.502f) },
            { "old lace", new Color(0.99215686f, 0.9607843f, 0.9019608f) },
            { "olive", new Color(0.5019608f, 0.5019608f, 0.0f) },
            { "olive drab", new Color(0.41960785f, 0.5568628f, 0.13725491f) },
            { "orange", new Color(1f, 0.64705884f, 0.0f) },
            { "orange red", new Color(1f, 0.27058825f, 0.0f) },
            { "orchid", new Color(0.85490197f, 0.43921569f, 0.83921569f) },
            { "pale golden rod", new Color(0.93333334f, 0.9098039f, 0.6666667f) },
            { "pale green", new Color(0.59607846f, 0.9843137f, 0.59607846f) },
            { "pale turquoise", new Color(0.6862745f, 0.96862745f, 0.96862745f) },
            { "pale violet red", new Color(0.85882354f, 0.43921569f, 0.5764706f) },
            { "papaya whip", new Color(1f, 0.9372549f, 0.83529415f) },
            { "peach puff", new Color(1f, 0.85490197f, 0.7254902f) },
            { "peru", new Color(0.8039216f, 0.52156866f, 0.24705882f) },
            { "pink", new Color(1f, 0.7529412f, 0.79607844f) },
            { "plum", new Color(0.8666667f, 0.627451f, 0.8666667f) },
            { "powder blue", new Color(0.6901961f, 0.8784314f, 0.9019608f) },
            { "purple", new Color(0.5019608f, 0.0f, 0.5019608f) },
            { "red", new Color(1f, 0.0f, 0.0f) },
            { "rosy brown", new Color(0.7372549f, 0.5607843f, 0.5607843f) },
            { "royal blue", new Color(0.25490198f, 0.4117647f, 0.88235295f) },
            { "saddle brown", new Color(0.54509807f, 0.27058825f, 0.07450981f) },
            { "salmon", new Color(0.98039216f, 0.5019608f, 0.44705883f) },
            { "sandy brown", new Color(0.95686275f, 0.64313726f, 0.3764706f) },
            { "sea green", new Color(0.18039216f, 0.54509807f, 0.34117648f) },
            { "seashell", new Color(1f, 0.9607843f, 0.93333334f) },
            { "sienna", new Color(0.627451f, 0.32156864f, 0.1764706f) },
            { "silver", new Color(0.7529412f, 0.7529412f, 0.7529412f) },
            { "sky blue", new Color(0.5294118f, 0.80784315f, 0.92156863f) },
            { "slate blue", new Color(0.41568628f, 0.3529412f, 0.8039216f) },
            { "slate gray", new Color(0.4392157f, 0.5019608f, 0.5647059f) },
            { "slate grey", new Color(0.4392157f, 0.5019608f, 0.5647059f) },
            { "snow", new Color(1f, 0.98039216f, 0.98039216f) },
            { "spring green", new Color(0.275f, 0.51f, 0.706f) },
            { "steel blue", new Color(0.27450982f, 0.50980395f, 0.7058824f) },
            { "stratos", new Color(0f, 0.02745098039f, 0.25490196078f)},
            { "tan", new Color(0.8235294f, 0.7058824f, 0.54901963f) },
            { "teal", new Color(0.0f, 0.5019608f, 0.5019608f) },
            { "thistle", new Color(0.84705883f, 0.7490196f, 0.84705883f) },
            { "tomato", new Color(0.98039216f, 0.38823528f, 0.2784314f) },
            { "turquoise", new Color(0.2509804f, 0.8784314f, 0.8156863f) },
            { "violet", new Color(0.93333334f, 0.50980395f, 0.93333334f) },
            { "wheat", new Color(0.9607843f, 0.87058824f, 0.7019608f) },
            { "white", new Color(1f, 1f, 1f) },
            { "white smoke", new Color(0.9607843f, 0.9607843f, 0.9607843f) },
            { "yellow", new Color(1f, 1f, 0.0f) },
            { "yellow green", new Color(0.6039216f, 0.8039216f, 0.19607843f) }
        };
        

        // Create a color from a color name.
        public static Color FromName(string name)
        {
            return CssColors.ContainsKey(name.ToLower()) ? CssColors[name] : Color.black;
        }

        // Find the closest color name in the CssColors dictionary.
        public static string FindClosestName(Color color)
        {
            var minDist = float.MaxValue;
            var closest = "black";
            foreach (var c in CssColors)
            {
                var tmp = c.Value - color;
                var dist = tmp.r * tmp.r + tmp.g * tmp.g + tmp.b * tmp.b;
                if (dist > minDist) continue;
                minDist = dist;
                closest = c.Key;
            }

            return closest;
        }

        // Find the closest color in the Colors dictionary.
        public static Color FindClosestNamedColor(Color color)
        {
            var minDist = float.MaxValue;
            var closest = Color.black;
            foreach (var c in CssColors)
            {
                var tmp = c.Value - color;
                var dist = tmp.r * tmp.r + tmp.g * tmp.g + tmp.b * tmp.b;
                if (dist > minDist) continue;
                minDist = dist;
                closest = c.Value;
            }

            return closest;
        }

    }
}