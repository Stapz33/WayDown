using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue_VDB", menuName = "Dialogue Validation Data")]
public class DialogueValidationScriptable : ScriptableObject
{
    public List<st_ValidationDialogue> ValidationDatas;
    [Serializable]
    public struct st_ValidationDialogue
    {
        public string BackValidation;
        public string GoodValidation;
        public string BadValidation;
        public string ValdationName;
    }
}
