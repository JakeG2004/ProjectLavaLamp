using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(MultiButton), editorForChildClasses: true)]
[CanEditMultipleObjects]

public class MultiButtonEditor : ButtonEditor
{
	private SerializedProperty additionalTargetImagesProperty;
	
	protected override void OnEnable()
	{
		base.OnEnable();
		additionalTargetImagesProperty = serializedObject.FindProperty("additionalTargetImages");
	}
	
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		base.OnInspectorGUI();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("MultiButton Properties", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(additionalTargetImagesProperty);
		serializedObject.ApplyModifiedProperties();
	}
}
