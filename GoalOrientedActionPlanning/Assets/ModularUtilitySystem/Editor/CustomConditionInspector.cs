using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UtilitySystem
{
    [CustomEditor(typeof(Condition))]
	public class CustomConditionInspector : Editor {

        public override void OnInspectorGUI()
        {
            Condition condition = (Condition)target;
            //SerializedObject serializedObject = new SerializedObject(condition);

            SerializedProperty floatRef = serializedObject.FindProperty("floatParameter");
            SerializedProperty boolRef = serializedObject.FindProperty("boolParameter");
            SerializedProperty stringRef = serializedObject.FindProperty("stringParameter");
            SerializedProperty intRef = serializedObject.FindProperty("intParameter");

            SerializedProperty floatCompareRef = serializedObject.FindProperty("floatCompareParameter");
            SerializedProperty boolCompareRef = serializedObject.FindProperty("boolCompareParameter");
            SerializedProperty stringCompareRef = serializedObject.FindProperty("stringCompareParameter");
            SerializedProperty intCompareRef = serializedObject.FindProperty("intCompareParameter");

            
            GUILayout.BeginVertical();
            condition.setting = (CompareSettings)EditorGUILayout.EnumPopup("Setting",condition.setting);
            switch (condition.setting)
            {
                case CompareSettings.BoolCompare:
                    EditorGUILayout.PropertyField(boolRef);
                    break;
                case CompareSettings.FloatCompare:
                    EditorGUILayout.PropertyField(floatRef);
                    break;
                case CompareSettings.IntCompare:
                    EditorGUILayout.PropertyField(intRef);
                    break;
                case CompareSettings.StringCompare:
                    EditorGUILayout.PropertyField(stringRef);
                    break;
            }

            condition.sign = (CompareSign)EditorGUILayout.EnumPopup("CompareSign", condition.sign);
            switch (condition.setting)
            {
                case CompareSettings.BoolCompare:
                    EditorGUILayout.PropertyField(boolCompareRef);
                    break;
                case CompareSettings.FloatCompare:
                    EditorGUILayout.PropertyField(floatCompareRef);
                    break;
                case CompareSettings.IntCompare:
                    EditorGUILayout.PropertyField(intCompareRef);
                    break;
                case CompareSettings.StringCompare:
                    EditorGUILayout.PropertyField(stringCompareRef);
                    break;
            }
            GUILayout.EndVertical();
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
