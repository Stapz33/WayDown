using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }
    private List<Button> l_AdressesList = new List<Button>();
    public Transform AdressesParent;

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

        for (int i = 0; i < AdressesParent.childCount;i++)
        {
            l_AdressesList.Add(AdressesParent.GetChild(i).GetComponent<Button>());
        }
    }

    public void SetupDialogUI()
    {

    }

    public void SetupAdressesState(bool Active)
    {
        foreach(Button AdressButton in l_AdressesList)
        {
            if (Active)
                AdressButton.interactable = true;
            else
                AdressButton.interactable = false;
        }
    }
}
