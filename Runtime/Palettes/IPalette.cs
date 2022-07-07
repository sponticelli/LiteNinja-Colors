using System.Collections;
using UnityEngine;

namespace LiteNinja.Colors.Palettes
{
    /// <summary>
    /// A class that contains a list of colors
    /// </summary>
    public interface IPalette
    {
        IEnumerator Colors();
        
        int Count { get; }
        
        Color this[int index] { get; }
        
        void Sort();
        
    }
}