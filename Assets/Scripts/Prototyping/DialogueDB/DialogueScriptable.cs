using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogueDB_", menuName = "Dialogue Data Base")]
public class DialogueScriptable : ScriptableObject
{
    public List<Sprite> Backgrounds;
    public List<Sprite> Characters;
}
