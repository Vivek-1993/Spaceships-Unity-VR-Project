using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using System;
#endif

[CanEditMultipleObjects]
[CustomEditor(typeof(Lightning))]
public class LightningEditor : Editor {

	public override void OnInspectorGUI()
	{
		string[] LayerNames = GetSortingLayerNames();
		SerializedProperty SortingLayer = serializedObject.FindProperty("SortingLayer");


		serializedObject.Update();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("MolniyaLych"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("TypeModeLightning"), true);

		Rect firstHoriz = EditorGUILayout.BeginHorizontal();
		EditorGUI.BeginChangeCheck();
		EditorGUI.BeginProperty(firstHoriz, GUIContent.none, SortingLayer);
		int IdLayer = 0;

		for (int i = 0; i < LayerNames.Length; i++)
			if (SortingLayer.stringValue == LayerNames[i])
				IdLayer = i;

		IdLayer = EditorGUILayout.Popup("Sorting Layer", IdLayer, LayerNames, EditorStyles.popup);
		SortingLayer.stringValue = LayerNames[IdLayer];
		EditorGUI.EndProperty();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("OrderLayer"), true);
		
		EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxTimeLifeLightning"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("DeltaTimeNextSubLightning"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxTimeLifeSubLightning"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("HasLoop"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("ColorLightning"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("QuantityIterations"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("OffsetLine"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("OffsetPlusDistanseLine"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("AngleAdditionalLightning"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("LengthScaleAdditionalLightning"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("ProbabilityAdditionalLightning"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("WidthLightning"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("WidthLightningGlow"), true);
		
		serializedObject.ApplyModifiedProperties();
	}
	
	public string[] GetSortingLayerNames()
	{
		Type internalEditorUtilityType = typeof(InternalEditorUtility);
		PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
		return (string[])sortingLayersProperty.GetValue(null, new object[0]);
	}
}
