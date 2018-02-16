using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ScenePathFieldAttribute), true)]
public class ScenePathDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.String)
        {
            string path = "Assets/" + property.stringValue + ".unity";
            SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            scene = EditorGUI.ObjectField(position, label, scene, typeof(SceneAsset), false) as SceneAsset;
            path = AssetDatabase.GetAssetPath(scene).Replace("Assets/", null).Replace(".unity", null);
            property.stringValue = path;
        }
        else
            EditorGUI.LabelField(position, "ScenePath得是string啊哥");

        EditorGUI.EndProperty();
    }
}
