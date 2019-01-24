using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardUIManager : MonoBehaviour
{
    public static BoardUIManager Singleton { get; private set; }
    private List<Button> l_AddressesList = new List<Button>();
    public Transform AddressesParent;

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

        for (int i = 0; i < AddressesParent.childCount;i++)
        {
            l_AddressesList.Add(AddressesParent.GetChild(i).GetComponent<Button>());
        }
    }

    public void SetupAdressesState(bool Active)
    {
        foreach(Button AdressButton in l_AddressesList)
        {
            if (Active)
                AdressButton.interactable = true;
            else
                AdressButton.interactable = false;
        }
    }

    public Transform GetAddressList()
    {
        return AddressesParent;
    }
}
