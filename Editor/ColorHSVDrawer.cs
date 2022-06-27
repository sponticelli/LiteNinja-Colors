using LiteNinja.Colors.Spaces;
using UnityEditor;
using UnityEngine;

namespace LiteNinja_Colors.Editor
{
    [CustomPropertyDrawer(typeof(ColorHSV))]
    public class ColorHSVDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            position.width = property.isExpanded ? position.width : EditorGUIUtility.labelWidth;
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, property.isExpanded, label);
            if (property.isExpanded)
            {
                var yStep = EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;

                position.y += yStep;
                var fieldRect = new Rect(position.x , position.y,
                    position.width, EditorGUIUtility.singleLineHeight);
                DrawSliderField(fieldRect, property, "_hue", "Hue");
                
                position.y += yStep;
                fieldRect.y = position.y;
                DrawSliderField(fieldRect, property, "_saturation", "Saturation");
                
                position.y += yStep;
                fieldRect.y = position.y;
                DrawSliderField(fieldRect, property, "_value", "Value");
                
                position.y += yStep;
                fieldRect.y = position.y;
                DrawSliderField(fieldRect, property, "_alpha", "Alpha");
                
                position.y += yStep;
                var colorRect = new Rect(position.x + EditorGUIUtility.labelWidth,
                    position.y, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight);
                DrawColorField(colorRect, property);
            }
            else
            {
                var colorRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y,
                    EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight);
                DrawColorField(colorRect, property);
            }
            EditorGUI.EndFoldoutHeaderGroup();
        }

        private static void DrawColorField(Rect position, SerializedProperty property)
        {
            var h = property.FindPropertyRelative("_hue");
            var s = property.FindPropertyRelative("_saturation");
            var v = property.FindPropertyRelative("_value");
            var a = property.FindPropertyRelative("_alpha");
            var hsv = new ColorHSV(h.floatValue, s.floatValue, v.floatValue, a.floatValue);
            hsv = EditorGUI.ColorField(position, hsv);
            h.floatValue = hsv.Hue;
            s.floatValue = hsv.Saturation;
            v.floatValue = hsv.Value;
            a.floatValue = hsv.Alpha;
        }

        private static void DrawSliderField(Rect position, SerializedProperty property, string name, string label)
        {
            var field = property.FindPropertyRelative(name);
            field.floatValue = EditorGUI.Slider(position, label, field.floatValue, 0f, 1f);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
            {
                return (EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight) * 6;
            }

            return EditorGUIUtility.singleLineHeight;
        }
    }


}