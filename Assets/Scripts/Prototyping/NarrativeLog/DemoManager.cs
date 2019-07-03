using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
    }
    // Update is called once per frame
    public void ReturnToMenu()
    {
        SaveManager.Singleton.DeleteSaves();
        MainUIManager.Singleton.ReturnToMenu();
    }
}
