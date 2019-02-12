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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_LaunchNewInvestigation"), true);
        var InvType = serializedObject.FindProperty("investigationType");
        EditorGUILayout.PropertyField(InvType, true);
        switch(InvType.enumValueIndex)
        {
            case 0:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("AddressInfos"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("AddressIndexToDiscover"), true);
                break;
            case 1:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CriminalRecordIdx"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CriminalBool"), true);
                var CriminalType = serializedObject.FindProperty("criminalRecordType");
                EditorGUILayout.PropertyField(CriminalType, true);
                if (CriminalType.enumValueIndex == 0)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("CriminalName"), true);
                }
                else
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("HintNeeded"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Corpulence"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Height"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("SexType"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Ethnicity"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("HairType"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("HairColor"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("EyeColor"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("TattooPiercing"), true);
                }
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}

public enum InvestigationType {Address, CriminalRecord}
public enum CriminalRecordType {Name, CompositeSketch}

public enum Corpulence { Null, Slim, Fat, Normal, Stocky, Muscular }
public enum Height { Null, Small, Medium, Large }
public enum SexType { Null, Male, Female }
public enum Ethnicity { Null, Italian, Russian, Irish, African, Asian, Arabian, Indian, Caucasian }
public enum HairType { Null, Long, Medium, Short, Bald, Curly, Straight, Coily }
public enum HairColor { Null, Blond, Red, Brown, White, Black, Grey }
public enum EyeColor { Null, Blue, Green, Brown, Black }
public enum TattooPiercing { Null, None, Tattoo, EarPiercing, NosePiercing, LipsPiercing }

[CreateAssetMenu(fileName = "StoryDB_", menuName = "Story Data Base")]
public class StoryDataBase : ScriptableObject
{
    public List<string> StoryData;

    public InvestigationType investigationType;
    public CriminalRecordType criminalRecordType;

    public string CriminalName;
    public string CriminalBool;

    public bool m_LaunchNewInvestigation = false;


    // Criminal Sketch;
    public int CriminalRecordIdx;

    public string AddressInfos;
    public int AddressIndexToDiscover;

    public Corpulence Corpulence;
    public Height Height;
    public SexType SexType;
    public Ethnicity Ethnicity;
    public HairType HairType;
    public HairColor HairColor;
    public EyeColor EyeColor;
    public TattooPiercing TattooPiercing;

    public int HintNeeded;
}
