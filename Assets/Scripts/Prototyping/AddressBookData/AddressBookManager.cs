using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddressBookManager : MonoBehaviour
{
    public static AddressBookManager Singleton { get; private set; }
    
    private List<Text> AddressesList = new List<Text>();

    public Transform AddressesButtonParent;

    [SerializeField] private Text ValidationText;

    [Header("Good Addresses")]

    public int LastPageIdx;
    private int PageIdx = 0;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(this);
        else
            Singleton = this;
    }

    private void Start()
    {
        for (int i = 0; i < AddressesButtonParent.childCount; i++)
        {
            AddressesList.Add(AddressesButtonParent.GetChild(i).GetChild(0).GetComponent<Text>());
        }
        NextPage();
    }

    public void NextPage()
    {
        if (PageIdx < LastPageIdx)
        {
            PageIdx++;
            RefreshPage();
        }
    }

    public void PreviousPage()
    {
        if (PageIdx > 1)
        {
            PageIdx--;
            RefreshPage();
        }
    }

    public void RefreshPage()
    {
        List<string> Addresses = CSVImport.Singleton.GetPage(PageIdx);
        for(int y = 0; y < AddressesList.Count; y++)
        {
            AddressesList[y].text = Addresses[y];
        }
    }

    public void JumpToPage(int idx)
    {
        PageIdx = idx;
        RefreshPage();
    }

    public void SetValidationText(string TextToSet)
    {
        ValidationText.text = TextToSet;
        MainUIManager.Singleton.TestGoodAddress(TextToSet);
    }
}
