using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiteNinja.Colors.Palettes
{
    /// <summary>
    /// A class that contains a list of colors
    /// </summary>
    public interface IPalette
    {
        /// <summary>
        /// Returns the number of colors in the palette
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets or sets the color at the specified index.
        /// </summary>
        /// <param name="index">The index of the color to get or set.</param>
        Color this[int index] { get; set; }


        /// <summary>
        /// Set all colors in the palette to the specified array of colors.
        /// </summary>
        void SetAll(IEnumerable<Color> colors);

        /// <summary>
        /// Get all colors in the palette.
        /// </summary>
        IEnumerable<Color> GetAll();


        /// <summary>
        /// Adds a new color to the palette.
        /// </summary>
        /// <param name="color">The color to add.</param>
        void Add(Color color);

        /// <summary>
        /// Removes a specified color from the palette.
        /// </summary>
        /// <param name="color"></param>
        public void Remove(Color color);

        /// <summary>
        /// Removes the color at the specified index.
        /// </summary>
        public void RemoveAt(int index);

        /// <summary>
        /// Returns a random color from the palette.
        /// </summary>
        /// <returns>A random color from the palette.</returns>
        Color Random();
    }
}