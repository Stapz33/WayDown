using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSectionManager : MonoBehaviour
{
    public LastSectionManager s_Singleton { get; private set; }

    public Transform DocumentParent;
    public GameObject BackButton;
    int i_ActualDocument = 0;

    public Transform Proof01;
    public Transform Proof02;
    public Transform Proof03;

    int i_actualproof = 0;

    int i_proofIdx01 = -1;
    int i_proofIdx02 = -1;
    int i_proofIdx03 = -1;

    int i_GoodIdx01 = 0;
    int i_GoodIdx02 = 1;
    int i_GoodIdx03 = 2;

    private void Awake()
    {
        if (s_Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            s_Singleton = this;
        }
    }


    public void ActivateDocPanel(int i)
    {
        DocumentParent.GetChild(i).gameObject.SetActive(true);
        i_ActualDocument = i;
        BackButton.SetActive(true);
    }

    public void DeactivateDocPanel()
    {
        DocumentParent.GetChild(i_ActualDocument).gameObject.SetActive(false);
        BackButton.SetActive(false);
    }

    
}
