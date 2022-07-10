using UnityEngine;

namespace LiteNinja.Colors.Helpers
{
    public static class ColorRandomHelper
    {
        // return a random color
        public static Color Random()
        {
            return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }
    }
}