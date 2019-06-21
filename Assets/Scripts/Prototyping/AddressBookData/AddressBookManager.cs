using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddressBookManager : MonoBehaviour
{
    public static AddressBookManager Singleton { get; private set; }
    
    private List<Text> AddressesList = new List<Text>();

    [SerializeField] private Transform AddressesButtonParent = null;
    [SerializeField] private GameObject AddressesAlphabeticalButtonParent = null;
    [SerializeField] private GameObject AlphabeticalButton = null;

    [SerializeField] private GameObject PreviousButton = null;
    [SerializeField] private GameObject NextButton = null;
    [SerializeField] private GameObject CallTaxiButton = null;

    [SerializeField] private Text ValidationText = null;


    [Header("Good Addresses")]

    public int LastPageIdx;
    private int PageIdx = 1;
    private float f_DoubleClickCD = 0;

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
        //NextPage();
    }

    private void Update()
    {
        if (f_DoubleClickCD > 0)
        {
            f_DoubleClickCD -= Time.deltaTime;
            if (f_DoubleClickCD <= 0)
            {
                f_DoubleClickCD = 0;
            }
        }
    }

    public void NextPage()
    {
        if (PageIdx < LastPageIdx)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.ChangePageAddressBook);
            PageIdx++;
            RefreshPage();
        }
    }

    public void PreviousPage()
    {
        if (PageIdx > 1)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.ChangePageAddressBook);
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
        if (PageIdx <= 1)
        {
            PreviousButton.SetActive(false);
        }
        if (PageIdx >= 2 && !PreviousButton.activeSelf)
        {
            PreviousButton.SetActive(true);
        }
        if (PageIdx <= LastPageIdx && !NextButton.activeSelf)
        {
            NextButton.SetActive(true);
        }
        if (PageIdx >= LastPageIdx)
        {
            NextButton.SetActive(false);
        }
    }

    public void JumpToPage(int idx)
    {
        PageIdx = idx;
        RefreshPage();
        AddressesAlphabeticalButtonParent.SetActive(false);
        AddressesButtonParent.gameObject.SetActive(true);
        AlphabeticalButton.SetActive(true);
    }

    public void SetValidationText(string TextToSet) 
    {
        if (f_DoubleClickCD == 0)
        {
            ValidationText.text = TextToSet;
            MainUIManager.Singleton.TestGoodAddress(TextToSet);
            f_DoubleClickCD = 0.3f;
            CallTaxiButton.SetActive(true);
        }
        else if (f_DoubleClickCD > 0)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.CallTaxi);
            MainUIManager.Singleton.LoadScreen("CallTaxi", true);
        }
    }

    public void GoToAlphabetical()
    {
        AlphabeticalButton.SetActive(false);
        NextButton.SetActive(false);
        PreviousButton.SetActive(false);
        AddressesButtonParent.gameObject.SetActive(false);
        AddressesAlphabeticalButtonParent.SetActive(true);
        ValidationText.text = "";
        CallTaxiButton.SetActive(false);
    }

    private void OnDisable()
    {
        f_DoubleClickCD = 0;
    }

    public void DisableTaxiButton()
    {
        CallTaxiButton.SetActive(false);
        ValidationText.text = "";
    }
}
