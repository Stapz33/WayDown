using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DocumentDB_", menuName = "Document Data Base")]
public class DocumentScriptable : ScriptableObject
{
    public List<GameObject> Documents;
}
