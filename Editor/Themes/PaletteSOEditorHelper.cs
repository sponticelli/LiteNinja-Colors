using System.Collections.Generic;
using System.Linq;
using LiteNinja.Colors.Themes;
using UnityEditor;
using UnityEngine;

namespace LiteNinja.Colors.Editor.Themes
{
    public class PaletteSOEditorHelper
    {
        public const int itemsPerRow = 12;
        
        private static GUIStyle _tempDrawTextureStyle;
        private static Texture2D _blackTexture;
        private static Texture2D _whiteTexture;
        private static Texture2D _paletteTexture;
        private static int _paletteTextureCachedHashCode;

        public static bool DrawColorPalette(PaletteSO palette, ref int colorKey, bool drawNewColorButton)
        {
            if (palette == null)
            {
                return false;
            }

            var lastRect = GUILayoutUtility.GetLastRect();

            if (palette.Count > 0)
            {
                var paletteHash = palette.Texture.GetHashCode();
                if (_paletteTexture == null || _paletteTextureCachedHashCode != paletteHash)
                {
                    _paletteTexture = TextureWithColors(palette.GetAll().ToArray());
                    _paletteTextureCachedHashCode = paletteHash;
                }
            }
            else
            {
                _paletteTexture = null;
            }

            if (_blackTexture == null)
            {
                _blackTexture = TextureWithColor(Color.black);
            }

            if (_whiteTexture == null)
            {
                _whiteTexture = TextureWithColor(Color.white);
            }

            var numColors = palette.Count;
            var numPerRow = itemsPerRow;
            var numInBottomRow = numColors % numPerRow;

            float heightOfPalette = 0;
            var textureRect = new Rect(lastRect.x, lastRect.y + lastRect.height, 0.0f, 0.0f);
            if (_paletteTexture != null)
            {
                textureRect = new Rect(lastRect.x, lastRect.y + lastRect.height,
                    _paletteTexture.width * EditorGUIUtility.singleLineHeight,
                    _paletteTexture.height * EditorGUIUtility.singleLineHeight);
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

            GUILayoutUtility.GetRect(clickRect.width, clickRect.height);
            if (_paletteTexture != null)
            {
                DrawRect(_paletteTexture, textureRect);
                DrawBlock(textureRect.x, textureRect.y, palette.Count,
                    _paletteTexture.width, _paletteTexture.height,
                    (int)EditorGUIUtility.singleLineHeight, _blackTexture);
            }

            if (drawNewColorButton)
            {
                DrawNewColorButton(numColors, textureRect);
            }

            var somethingHasChanged = false;
            if (IsClick())
            {
                if (IsClickInRect(clickRect))
                {
                    var e = Event.current;
                    var rectClickPosition = e.mousePosition - textureRect.position;
                    var cellXIndex = (int)(rectClickPosition.x / EditorGUIUtility.singleLineHeight);
                    var cellYIndex = (int)(rectClickPosition.y / EditorGUIUtility.singleLineHeight);
                    var textureWidth = _paletteTexture != null ? _paletteTexture.width : 0;
                    var clickedOnKey = cellYIndex * textureWidth + cellXIndex;
                    if (numColors > 0 && clickedOnKey < numColors)
                    {
                        colorKey = clickedOnKey;
                        somethingHasChanged = true;
                    }
                    else if (clickedOnKey == numColors)
                    {
                        colorKey = clickedOnKey;
                        palette.Add(Color.white);
                        palette.Invoke();
                        somethingHasChanged = true;
                    }
                }
            }

            if (palette.Count <= 0) return somethingHasChanged;
            
            DrawOnSelectedCell(colorKey, textureRect);
            var selectedColorRow = colorKey / itemsPerRow;
            var selectedColorY = selectedColorRow * EditorGUIUtility.singleLineHeight +
                                 EditorGUIUtility.singleLineHeight;
            var colorKeyRect =
                new Rect(lastRect.x + (1+itemsPerRow) * EditorGUIUtility.singleLineHeight,
                    lastRect.y + selectedColorY, 64, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(colorKeyRect, colorKey.ToString());

            return somethingHasChanged;
        }

        public static bool DrawDeleteButton(int x, int y)
        {
            var slh = (int)(EditorGUIUtility.singleLineHeight);
            var slhHalf = (int)(slh * 0.5f);
            if (_blackTexture == null)
            {
                _blackTexture = TextureWithColor(Color.black);
            }

            DrawBlock(x, y, 1, 1, 1, slh, _blackTexture);
            DrawBlock(x + 1, y + 1, 1, 1, 1, slh - 2, _whiteTexture);

            const int minusLength = 7;
            const int halfMinusLength = 3;

            var minusRect = new Rect(x + slhHalf - halfMinusLength, y + slhHalf, minusLength, 1);
            DrawRect(_whiteTexture, minusRect);
            var clickRect = new Rect(x, y, slh, slh);
            return IsClick() && IsClickInRect(clickRect);
        }

        public static Rect GetNewColorButtonRect(PaletteSO palette)
        {
            var numColors = palette.Count;
            var totalRows = Mathf.CeilToInt(numColors / (float)itemsPerRow);
            var numInBottomRow = numColors % itemsPerRow;
            var r = new Rect
            {
                x = (int)(numInBottomRow * EditorGUIUtility.singleLineHeight),
                y = (int)(totalRows * EditorGUIUtility.singleLineHeight),
                width = EditorGUIUtility.singleLineHeight,
                height = EditorGUIUtility.singleLineHeight
            };
            return r;
        }

        private static void DrawBlock(float startingPointX, float startingPointY, int numColors, int cellsX, int cellsY,
            int cellSize, Texture2D colorTexture)
        {
            if (cellsX == 0 && cellsY == 0)
            {
                return;
            }

            // draw vertical lines
            var currentRect = new Rect(startingPointX, startingPointY, 1, cellSize * cellsY);
            var fullHeight = cellSize * cellsY + 1; // +1 to get the corners
            var oneLessHeight = cellSize * (cellsY - 1) + 1;
            // oneLessHeight will be 1 if theres only one row
            if (cellsY == 1)
            {
                oneLessHeight = 0;
            }

            var numInBottomRow = numColors % cellsX;
            for (var i = 0; i <= cellsX; i++)
            {
                // height will be 1 unit shorter if bottom cell does not exist
                currentRect.x = startingPointX + cellSize * i;
                var bottomCellExists = numInBottomRow == 0 || i <= numInBottomRow;
                currentRect.height = bottomCellExists ? fullHeight : oneLessHeight;
                DrawRect(colorTexture, currentRect);
            }

            // draw horizontal lines
            currentRect.x = startingPointX;
            currentRect.height = 1;
            currentRect.width = cellSize * cellsX;
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

        private static void DrawOnSelectedCell(int selectedCell, Rect textureRect)
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

        private static void DrawNewColorButton(int selectedCell, Rect textureRect)
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
            DrawRect(_whiteTexture, plusVerticalRect);
            DrawRect(_whiteTexture, plusHorizontalRect);
        }


        private static void DrawRect(Texture2D texture, Rect rect)
        {
            _tempDrawTextureStyle ??= new GUIStyle();

            _tempDrawTextureStyle.normal.background = texture;
            EditorGUI.LabelField(rect, "", _tempDrawTextureStyle);
        }

        private static bool IsClick()
        {
            var e = Event.current;
            return e != null && e.type == EventType.MouseDown && e.button == 0;
        }

        private static bool IsClickInRect(Rect rect)
        {
            var e = Event.current;
            return e is { type: EventType.MouseDown, button: 0 } && rect.Contains(e.mousePosition);
        }

        private static Texture2D TextureWithColor(Color color)
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

        private static Texture2D TextureWithColors(IReadOnlyList<Color> colors)
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