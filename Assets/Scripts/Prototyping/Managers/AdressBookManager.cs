using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdressBookManager : MonoBehaviour
{
    public List<Sprite> AddressPages;
    public Image AddressBookPage;
    private int PageIndex = 0;

    public void NextPage()
    {
        PageIndex++;
        AddressBookPage.sprite = AddressPages[PageIndex];
    }

    public void PreviousPage()
    {
        PageIndex--;
        AddressBookPage.sprite = AddressPages[PageIndex];
    }
}
