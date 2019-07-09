using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_CR", menuName = "CR Datas")]
public class CRDatas : ScriptableObject
{
    public List<st_CR> CRData;
    [Serializable]
    public struct st_CR
    {
        public string goodName;
        public int CRIdx;
    }
}
