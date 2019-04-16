using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentFolderManager : MonoBehaviour
{
    [SerializeField] private Transform tabParent = null;


    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0f;
    }
    public void UnlockTab(int idx)
    {
        GameObject tab = tabParent.GetChild(idx).gameObject;
        if (!tab.activeSelf)
        {
            tab.SetActive(true);
            SetupTab(idx);
        }
    }

    public void SetupTab(int idx)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(idx).gameObject.SetActive(true);
    }

    

}
