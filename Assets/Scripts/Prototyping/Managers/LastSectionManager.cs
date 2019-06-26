using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSectionManager : MonoBehaviour
{
    public static LastSectionManager s_Singleton { get; private set; }

    public Transform DocumentParent;
    int i_ActualDocument = 0;

    public Animator LanzaAnimator;
    public Animator MorelloAnimator;
    public GameObject ValidationButton;
    public GameObject ProofSection;

    public GameObject DocumentInfo;

    bool lanzachoosen = false;
    bool morellochoosen = false;

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
    }

    public void DeactivateDocPanel()
    {
        DocumentParent.GetChild(i_ActualDocument).gameObject.SetActive(false);
    }

    public void ActivateLanza()
    {
        if (lanzachoosen)
        {
            return;
        }
        LanzaAnimator.SetTrigger("Activation");
        if (morellochoosen)
        {
            morellochoosen = false;
            MorelloAnimator.SetTrigger("Sleep");
        }
        else
        {
            MorelloAnimator.SetTrigger("Black");
        }
        ValidationButton.SetActive(true);
        MainUIManager.Singleton.SetLanzaChoosed(true);
        lanzachoosen = true;
    }

    public void ActivateMorello()
    {
        if (morellochoosen)
        {
            return;
        }
        MorelloAnimator.SetTrigger("Activation");
        if (lanzachoosen)
        {
            lanzachoosen = false;
            LanzaAnimator.SetTrigger("Sleep");
        }
        else
        {
            LanzaAnimator.SetTrigger("Black");
        }
        ValidationButton.SetActive(true);
        MainUIManager.Singleton.SetLanzaChoosed(false);
        morellochoosen = true;
    }

    public void ActivateProofSection()
    {
        ValidationButton.SetActive(false);
        ProofSection.SetActive(true);
    }

    public void ActivateDocumentInfo(Sprite info)
    {
        DocumentInfo.GetComponent<UnityEngine.UI.Image>().sprite = info;
        DocumentInfo.SetActive(true);
    }

    public void DeactivateDocumentInfo()
    {
        DocumentInfo.SetActive(false);
    }

}
