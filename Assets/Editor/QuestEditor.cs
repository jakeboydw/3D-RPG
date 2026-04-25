using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    private SerializedProperty stepsProp;

    private Type[] stepTypes;

    private void OnEnable()
    {
        stepsProp = serializedObject.FindProperty("steps");

        // 自动获取所有 QuestStep 子类
        stepTypes = TypeCache.GetTypesDerivedFrom<QuestStep>()
            .Where(t => !t.IsAbstract)
            .ToArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("任务步骤", EditorStyles.boldLabel);

        for (int i = 0; i < stepsProp.arraySize; i++)
        {
            var element = stepsProp.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Step {i}", EditorStyles.boldLabel);

            if (GUILayout.Button("删除"))
            {
                stepsProp.DeleteArrayElementAtIndex(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(element, true);

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("添加步骤"))
        {
            ShowAddStepMenu();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowAddStepMenu()
    {
        GenericMenu menu = new GenericMenu();

        foreach (var type in stepTypes)
        {
            menu.AddItem(new GUIContent(type.Name), false, () =>
            {
                AddStep(type);
            });
        }

        menu.ShowAsContext();
    }

    private void AddStep(Type type)
    {
        serializedObject.Update();

        stepsProp.arraySize++;
        var element = stepsProp.GetArrayElementAtIndex(stepsProp.arraySize - 1);

        element.managedReferenceValue = Activator.CreateInstance(type);

        serializedObject.ApplyModifiedProperties();
    }
}