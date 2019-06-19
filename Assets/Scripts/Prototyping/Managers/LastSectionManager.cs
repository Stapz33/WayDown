using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSectionManager : MonoBehaviour
{
    public LastSectionManager s_Singleton { get; private set; }

    public Transform DocumentParent;
    public GameObject BackButton;
    int i_ActualDocument = 0;

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
