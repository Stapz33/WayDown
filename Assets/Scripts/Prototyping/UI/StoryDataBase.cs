using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StoryDataBase))]
public class StoryDataBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("StoryData"),true);
        var InvType = serializedObject.FindProperty("investigationType");
        EditorGUILayout.PropertyField(InvType, true);
        switch(InvType.enumValueIndex)
        {
            case 0:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("AddressInfos"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("AddressIndexToDiscover"), true);
                break;
            case 1:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CriminalRecord"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CriminalBool"), true);
                var CriminalType = serializedObject.FindProperty("criminalRecordType");
                EditorGUILayout.PropertyField(CriminalType, true);
                if (CriminalType.enumValueIndex == 0)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("CriminalName"), true);
                }
                else
                {
                }
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}

public enum InvestigationType {Address, CriminalRecord}
public enum CriminalRecordType {Name, CompositeSketch}

[CreateAssetMenu(fileName = "StoryDB_", menuName = "Story Data Base")]
public class StoryDataBase : ScriptableObject
{
    public List<string> StoryData;

    public InvestigationType investigationType;
    public CriminalRecordType criminalRecordType;

    public string CriminalName;
    public string CriminalBool;
    // Criminal Sketch;
    public GameObject CriminalRecord;

    public string AddressInfos;
    public int AddressIndexToDiscover;
}
