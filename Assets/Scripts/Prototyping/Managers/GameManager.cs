using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }
    private List<Tuple<Vector3, float>> l_CameraInfos = new List<Tuple<Vector3, float>>() ;

    public void Awake()
    {
        if (Singleton != null)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Transform CameraGameObject = transform.GetChild(0).GetChild(i);
            l_CameraInfos.Add(new Tuple<Vector3, float>(CameraGameObject.position, CameraGameObject.GetComponent<CameraPosition>().CameraZoom));
        }
    }
    public Tuple<Vector3, float> GetCameraInfo(int Index)
    {
        if (Index >= 0 && Index < l_CameraInfos.Count)
        {
            return l_CameraInfos[Index];
        }
        Debug.LogError("Camera Index doesn't exist", gameObject);
        return null; 
    }

    public void SetupDialog()
    {

    }
}
