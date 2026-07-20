using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(newsPaperOptions))]
public class NewspaperOptionsEditor : Editor
{
    public override void OnInspectorGUI()
	{
		serializedObject.Update();
		SerializedProperty structProp = serializedObject.FindProperty("Newspaper Options");
		EditorGUILayout.PropertyField(structProp, new GUIContent("Newspaper Options"), includeChildren: true);
		serializedObject.ApplyModifiedProperties();
	}
}