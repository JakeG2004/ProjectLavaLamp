using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(memoOptions))]
public class MemoOptionsEditor : Editor
{
    public override void OnInspectorGUI()
	{
		serializedObject.Update();
		SerializedProperty structProp = serializedObject.FindProperty("Memo Options");
		EditorGUILayout.PropertyField(structProp, new GUIContent("Memo Options"), includeChildren: true);
		serializedObject.ApplyModifiedProperties();
	}
}
