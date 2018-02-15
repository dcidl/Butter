using UnityEngine;
using UnityEditor;

namespace Butter
{
    [CustomEditor(typeof(ButterManager))]
    public class ButterManagerEditor : Editor
    {
        SerializedProperty _firstSceneName;
        private void OnEnable()
        {
            _firstSceneName = serializedObject.FindProperty("_firstScenePath");
            _firstScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/" + _firstSceneName.stringValue + ".unity");
        }
        SceneAsset _firstScene;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _firstScene = EditorGUILayout.ObjectField("First Scene", _firstScene, typeof(SceneAsset), false) as SceneAsset;
            if (_firstScene == null)
                _firstSceneName.stringValue = null;
            else
                _firstSceneName.stringValue = AssetDatabase.GetAssetPath(_firstScene).Replace("Assets/", null).Replace(".unity", null);
            GUILayout.Label("FirstScenePath:" + _firstSceneName.stringValue);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
