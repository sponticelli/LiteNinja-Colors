using LiteNinja.Utils.Extensions;
using UnityEngine;

namespace LiteNinja.Colors.Extensions
{
    public static class ColorComparisonExtensions
    {

        public static bool Approximately(this Color value, Color other)
        {
            return Mathf.Approximately(value.r, other.r) &&
                   Mathf.Approximately(value.g, other.g) &&
                   Mathf.Approximately(value.b, other.b) &&
                   Mathf.Approximately(value.a, other.a);
        }

        public static bool Approximately(this Color value, Color other, float epsilon)
        {
            return value.r.Approximately(other.r, epsilon) &&
                   value.g.Approximately(other.g, epsilon) &&
                   value.b.Approximately(other.b, epsilon) &&
                   value.a.Approximately(other.a, epsilon);
        }

        public static bool IsApproximatelyBlack(this Color self)
        {
            return self.r + self.g + self.b <= Mathf.Epsilon;
        }

        public static bool IsApproximatelyWhite(this Color self)
        {
            return self.r + self.g + self.b >= 1 - Mathf.Epsilon;
        }
    }
}