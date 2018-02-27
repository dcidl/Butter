using System.Text.RegularExpressions;

using UnityEngine;
using UnityEditor;

namespace EditorEx
{
    [CustomPropertyDrawer(typeof(ResourcesPathAttribute))]
    public class ResourcesPathDrawer : PropertyDrawer
    {
        Object _obj;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            string targetFieldName = (attribute as ResourcesPathAttribute).fieldName;
            if (!string.IsNullOrEmpty(targetFieldName))
            {
                SerializedProperty targetProperty = property.serializedObject.FindProperty(targetFieldName);
                if (targetProperty != null)
                {
                    if (_obj != targetProperty.objectReferenceValue)
                    {
                        //重新赋值
                        _obj = targetProperty.objectReferenceValue;
                        if (_obj == null)
                            property.stringValue = null;
                        else
                        {
                            property.stringValue = AssetDatabase.GetAssetPath(_obj);
                            Match match = Regex.Match(property.stringValue, @".+Resources[/\\](?<rsc>.+?)\.\w+");
                            if (match.Success)
                            {
                                property.stringValue = match.Groups["rsc"].Value;
                            }
                            else
                                property.stringValue = null;
                        }
                    }
                    //绘制
                    if (!string.IsNullOrEmpty(property.stringValue))
                    {
                        EditorGUI.LabelField(position, label, new GUIContent(property.stringValue));
                    }
                    else
                        EditorGUI.LabelField(position, label, new GUIContent("非Resources路径"));
                }
                else
                    EditorGUI.LabelField(position, label, new GUIContent("未找到目标字段"));
            }
            else
                EditorGUI.LabelField(position, label, new GUIContent("目标字段为空"));

            EditorGUI.EndProperty();
        }
    }
}