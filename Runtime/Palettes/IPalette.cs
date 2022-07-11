using System;
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
        /// A Texture2D containing the colors of the palette.
        /// </summary>
        Texture2D Texture { get; }

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
        /// Adds colors to the palette.
        /// </summary>
        void AddRange(IEnumerable<Color> colors);

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
        /// Replace the colors with the colors of another palette.
        /// </summary>
        void ReplaceFromPalette(IPalette palette);
        
        /// <summary>
        /// Add colors from another palette to the end of the current palette.
        /// </summary>
        void AddFromPalette(IPalette palette);

        
        void AddListener(Action listener);
        void RemoveListener(Action listener);
        
    }
}