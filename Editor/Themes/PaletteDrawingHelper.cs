using System.Collections.Generic;
using System.Linq;
using LiteNinja.Colors.Themes;
using UnityEditor;
using UnityEngine;

namespace LiteNinja.Colors.Editor.Themes
{
    public static class PaletteDrawingHelper
    {
        private static readonly Texture2D _blackTexture = CreateTexture(Color.black);
        private static readonly Texture2D _whiteTexture = CreateTexture(Color.white);

        private static int _paletteTextureCachedHashCode;

        public static bool DrawColorPalette(PaletteSO palette, ref int colorKey, bool drawNewColorButton,
            Texture2D paletteTexture, int itemsPerRow = 12, float indent = 0f)
        {
            if (palette == null)
            {
                return false;
            }

            var lastRect = GUILayoutUtility.GetLastRect();

            paletteTexture = CreateTextureFromPalette(palette, paletteTexture, itemsPerRow);
            
            var numInBottomRow = palette.Count % itemsPerRow;

            Rect textureRect;
            Rect clickRect;
            (textureRect, clickRect) = CalcTextureRect(palette, paletteTexture, indent, lastRect, numInBottomRow);

            GUILayoutUtility.GetRect(clickRect.width, clickRect.height);
            DrawPaletteInterface(palette, drawNewColorButton, paletteTexture, itemsPerRow, textureRect);

            var somethingHasChanged = false;
            (somethingHasChanged, colorKey) = ClickOnPalette(palette, colorKey, paletteTexture, clickRect,
                textureRect);

            if (palette.Count <= 0) return somethingHasChanged;

            var isInRange = colorKey >= 0 && colorKey < palette.Count;
            HighlightSelectedCell(colorKey, itemsPerRow, isInRange, textureRect);
            DrawCurrentIndex(colorKey, itemsPerRow, indent, isInRange, lastRect);
            return somethingHasChanged;
        }

        private static (bool, int clickedOnKey) ClickOnPalette(PaletteSO palette, int previousKey, 
            Texture2D paletteTexture, Rect clickRect,
            Rect textureRect)
        {
            if (!IsClick()) return (false, previousKey);
            if (!IsClickInRect(clickRect)) return (false, previousKey);
            var numColors = palette.Count;
            var e = Event.current;
            var rectClickPosition = e.mousePosition - textureRect.position;
            var cellXIndex = (int)(rectClickPosition.x / EditorGUIUtility.singleLineHeight);
            var cellYIndex = (int)(rectClickPosition.y / EditorGUIUtility.singleLineHeight);
            var textureWidth = paletteTexture != null ? paletteTexture.width : 0;
            var clickedOnKey = cellYIndex * textureWidth + cellXIndex;
            if (numColors > 0 && clickedOnKey < numColors)
            {
                return (true, clickedOnKey);
            }

            if (clickedOnKey != numColors) return (false, previousKey);
            palette.Add(Color.white);
            palette.Invoke();
            return (true, clickedOnKey);
        }

        private static void DrawPaletteInterface(PaletteSO palette, bool drawNewColorButton, Texture2D paletteTexture,
            int itemsPerRow, Rect textureRect)
        {
            if (paletteTexture != null)
            {
                DrawRect(paletteTexture, textureRect);
                DrawBlock(textureRect.x, textureRect.y, palette.Count,
                    paletteTexture.width, paletteTexture.height,
                    (int)EditorGUIUtility.singleLineHeight, _blackTexture);
            }

            if (drawNewColorButton)
            {
                DrawNewColorButton(palette.Count, textureRect, itemsPerRow);
            }
        }

        private static (Rect, Rect) CalcTextureRect(PaletteSO palette, Texture2D paletteTexture, float indent, Rect lastRect,
            int numInBottomRow)
        {
            float heightOfPalette = 0;
            var textureRect = new Rect(lastRect.x + indent, lastRect.y + lastRect.height, 0.0f, 0.0f);
            if (paletteTexture != null)
            {
                textureRect = new Rect(lastRect.x + indent, lastRect.y + lastRect.height,
                    paletteTexture.width * EditorGUIUtility.singleLineHeight,
                    paletteTexture.height * EditorGUIUtility.singleLineHeight);
                heightOfPalette = textureRect.height;
            }

            if (numInBottomRow == 0)
            {
                heightOfPalette += EditorGUIUtility.singleLineHeight;
            }

            var clickRect = textureRect;
            if (palette.Count == 0)
            {
                clickRect.width = EditorGUIUtility.singleLineHeight;
            }

            clickRect.height = heightOfPalette;
            return (textureRect, clickRect);
        }

        private static Texture2D CreateTextureFromPalette(PaletteSO palette, Texture2D paletteTexture, int itemsPerRow)
        {
            if (palette.Count > 0)
            {
                var paletteHash = palette.Texture.GetHashCode();
                if (paletteTexture != null && _paletteTextureCachedHashCode == paletteHash) return paletteTexture;
                paletteTexture = CreateTexture(palette.GetAll().ToArray(), itemsPerRow);
                _paletteTextureCachedHashCode = paletteHash;
            }
            else
            {
                paletteTexture = null;
            }

            return paletteTexture;
        }

        private static void DrawCurrentIndex(int colorKey, int itemsPerRow, float indent, bool isInRange, Rect lastRect)
        {
            var selectedColorRow = (isInRange) ? colorKey / itemsPerRow : 0;
            var selectedColorY = selectedColorRow * EditorGUIUtility.singleLineHeight +
                                 EditorGUIUtility.singleLineHeight;
            var colorKeyRect =
                new Rect(lastRect.x + indent + (1 + itemsPerRow) * EditorGUIUtility.singleLineHeight,
                    lastRect.y + selectedColorY, 64, EditorGUIUtility.singleLineHeight);
            var guiStyle = new GUIStyle(EditorStyles.label);
            if (!isInRange)
            {
                guiStyle.normal.textColor = Color.red;
                guiStyle.fontStyle = FontStyle.Bold;
            }

            EditorGUI.LabelField(colorKeyRect, colorKey.ToString(), guiStyle);
        }

        private static void HighlightSelectedCell(int colorKey, int itemsPerRow, bool isInRange, Rect textureRect)
        {
            if (isInRange)
            {
                DrawOnSelectedCell(colorKey, textureRect, itemsPerRow);
            }
        }

        public static bool DrawDeleteButton(int x, int y)
        {
            var slh = (int)(EditorGUIUtility.singleLineHeight);
            var slhHalf = (int)(slh * 0.5f);

            DrawBlock(x, y, 1, 1, 1, slh, _blackTexture);
            DrawBlock(x + 1, y + 1, 1, 1, 1, slh - 2, _whiteTexture);

            const int minusLength = 7;
            const int halfMinusLength = 3;

            var minusRect = new Rect(x + slhHalf - halfMinusLength, y + slhHalf, minusLength, 1);
            DrawRect(EditorGUIUtility.isProSkin ? _whiteTexture : _blackTexture, minusRect);

            var clickRect = new Rect(x, y, slh, slh);
            return IsClick() && IsClickInRect(clickRect);
        }

        private static void DrawBlock(float startingPointX, float startingPointY, int numColors, int cellsX, int cellsY,
            int cellSize, Texture2D colorTexture)
        {
            if (cellsX == 0 && cellsY == 0)
            {
                return;
            }

            
            var currentRect = new Rect(startingPointX, startingPointY, 1, cellSize * cellsY);
            var fullHeight = cellSize * cellsY + 1; // +1 to get the corners
            var oneLessHeight = cellSize * (cellsY - 1) + 1;
            
            if (cellsY == 1)
            {
                oneLessHeight = 0;
            }

            var numInBottomRow = numColors % cellsX;
            DrawVerticalLines(startingPointX, cellsX, cellSize, colorTexture, currentRect, numInBottomRow, fullHeight, oneLessHeight);

            // draw horizontal lines
            currentRect.x = startingPointX;
            currentRect.height = 1;
            currentRect.width = cellSize * cellsX;
            DrawHorizontalLines(startingPointY, cellsY, cellSize, colorTexture, currentRect, numInBottomRow);
        }

        private static void DrawHorizontalLines(float startingPointY, int cellsY, int cellSize, Texture2D colorTexture,
            Rect currentRect, int numInBottomRow)
        {
            for (var i = 0; i <= cellsY; i++)
            {
                currentRect.y = startingPointY + cellSize * i;
                if ((i == cellsY || cellsY == 1) && numInBottomRow > 0)
                {
                    currentRect.width = numInBottomRow * cellSize;
                }

                DrawRect(colorTexture, currentRect);
            }
        }

        private static void DrawVerticalLines(float startingPointX, int cellsX, int cellSize, Texture2D colorTexture,
            Rect currentRect, int numInBottomRow, int fullHeight, int oneLessHeight)
        {
            for (var i = 0; i <= cellsX; i++)
            {
                currentRect.x = startingPointX + cellSize * i;
                var bottomCellExists = numInBottomRow == 0 || i <= numInBottomRow;
                currentRect.height = bottomCellExists ? fullHeight : oneLessHeight;
                DrawRect(colorTexture, currentRect);
            }
        }

        private static void DrawOnSelectedCell(int selectedCell, Rect textureRect, int itemsPerRow = 12)
        {
            var selectedCellY = selectedCell / itemsPerRow;
            var selectedCellX = selectedCell - (itemsPerRow * selectedCellY);
            var smallBlackRect = new Rect(textureRect.x + selectedCellX * EditorGUIUtility.singleLineHeight,
                textureRect.y + selectedCellY * EditorGUIUtility.singleLineHeight, 10f, 10f);
            DrawBlock(smallBlackRect.x - 1, smallBlackRect.y - 1, 1, 1, 1,
                (int)(EditorGUIUtility.singleLineHeight) + 2, _blackTexture);
            DrawBlock(smallBlackRect.x, smallBlackRect.y, 1, 1, 1, (int)(EditorGUIUtility.singleLineHeight),
                _whiteTexture);
        }

        private static void DrawNewColorButton(int selectedCell, Rect textureRect, int itemsPerRow = 12)
        {
            var selectedCellY = selectedCell / itemsPerRow;
            var selectedCellX = selectedCell - (itemsPerRow * selectedCellY);
            var smallBlackRect = new Rect(textureRect.x + selectedCellX * EditorGUIUtility.singleLineHeight,
                textureRect.y + selectedCellY * EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight,
                EditorGUIUtility.singleLineHeight);
            DrawBlock(smallBlackRect.x, smallBlackRect.y, 1, 1, 1, (int)(EditorGUIUtility.singleLineHeight),
                _blackTexture);
            DrawBlock(smallBlackRect.x + 1, smallBlackRect.y + 1, 1, 1, 1,
                (int)(EditorGUIUtility.singleLineHeight - 2), _whiteTexture);

            const int plusLength = 7;
            const float halfPlusLength = 3.0f;

            var centerX = smallBlackRect.x + smallBlackRect.width * 0.5f;
            var centerY = smallBlackRect.y + smallBlackRect.height * 0.5f;
            var plusVerticalRect = new Rect(centerX, centerY - halfPlusLength, 1, plusLength);
            var plusHorizontalRect = new Rect(centerX - halfPlusLength, centerY, plusLength, 1);
            DrawRect(EditorGUIUtility.isProSkin ? _whiteTexture : _blackTexture, plusVerticalRect);
            DrawRect(EditorGUIUtility.isProSkin ? _whiteTexture : _blackTexture, plusHorizontalRect);
        }


        private static void DrawRect(Texture2D texture, Rect rect)
        {
            var guiStyle = new GUIStyle
            {
                normal =
                {
                    background = texture
                }
            };
            EditorGUI.LabelField(rect, "", guiStyle);
        }

        private static bool IsClick()
        {
            var e = Event.current;
            return e is { type: EventType.MouseDown, button: 0 };
        }

        private static bool IsClickInRect(Rect rect)
        {
            var e = Event.current;
            return e is { type: EventType.MouseDown, button: 0 } && rect.Contains(e.mousePosition);
        }

        private static Texture2D CreateTexture(Color color)
        {
            var tex = new Texture2D(1, 1, TextureFormat.RGB24, false, true)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp,
                hideFlags = HideFlags.HideAndDontSave
            };
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }

        private static Texture2D CreateTexture(IReadOnlyList<Color> colors, int itemsPerRow)
        {
            if (colors == null || colors.Count == 0)
            {
                return new Texture2D(0, 0, TextureFormat.RGBA32, false, true);
            }

            // figure out our texture size based on the itemsPerRow and color count
            var totalRows = Mathf.CeilToInt((float)colors.Count / (float)itemsPerRow);
            var tex = new Texture2D(itemsPerRow, totalRows, TextureFormat.RGBA32, false, true)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp,
                hideFlags = HideFlags.HideAndDontSave
            };
            var x = 0;
            var y = 0;
            for (var i = 0; i < colors.Count; i++)
            {
                x = i % itemsPerRow;
                y = totalRows - 1 - Mathf.CeilToInt(i / itemsPerRow);
                tex.SetPixel(x, y, colors[i]);
            }

            for (x++; x < tex.width; x++)
            {
                tex.SetPixel(x, y, Color.clear);
            }

            tex.Apply();

            return tex;
        }
    }
}