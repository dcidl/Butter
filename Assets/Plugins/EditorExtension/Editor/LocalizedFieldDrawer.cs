
using UnityEngine;
using UnityEditor;

namespace EditorEx
{
    [CustomPropertyDrawer(typeof(LocalizedFieldAttribute))]
    public class LocalizedFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = (attribute as LocalizedFieldAttribute).name;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}