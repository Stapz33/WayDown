using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine.UI;

public enum TabType {Map, AddressBook, Documents}
public enum DocumentType { NewsPaper, WritingNote, Object, PoliceRecord }

public class MainUIManager : MonoBehaviour {

    public static MainUIManager Singleton { get; private set; }

    #region ADDRESS_SYSTEM_DATA

    private List<Button> l_AddressesList = new List<Button>();
    public Transform AddressesParent;

    #endregion

    #region TABS_DATA

    [SerializeField] private GameObject MapTab;
    [SerializeField] private GameObject AddressBookTab;
    [SerializeField] private GameObject DocumentsTab;
    private GameObject ActualTab;

    #endregion

    #region LOADING_SCREEN_DATA

    [SerializeField] private Animator a_LoadingScreenAnimator;

    #endregion

    #region DIALOGUE_SYSTEM_SETUP_DATA

    // Prefabs & Assets
    [Header("Dialogue System")]
    public GameObject DialogueSystem;
    public GameObject OtherCharacterSection;
    public Transform ChoiceSection;
    public Text PlayerText;
    public Text OtherCharacterText;
    public Button ChoiceButton;

    // Story
    [SerializeField]private TextAsset Story;
    private Story _inkStory;

    // Variables
    public float wordsPerSecond = 10f; // speed of typewriter
    private bool ChoiceNeeded = false;
	private bool b_StoryStarted = false;
    private string s_PlayerFullText = "";
    private string s_OtherCharacterFullText = "";
    private float PlayerTimeElapsed = 0f;
    private float OtherPlayerTimeElapsed = 0f;
    #endregion

    #region STORY_SETUP_DATA

    [Header("Story")]
    public string DefaultKnot;
    private List<Address_data> AddressesList = new List<Address_data>();

    #endregion

    #region DOCUMENTS_DATA

    [Header("Documents")]
    [SerializeField] private GameObject NewsPapers;
    [SerializeField] private GameObject WritingNotes;
    [SerializeField] private GameObject Objects;
    [SerializeField] private GameObject PoliceRecord;
    [SerializeField] private GameObject DocumentTypeButtons;
    [SerializeField] private GameObject CloseDocumentPanelButton;

    private GameObject ActualDocumentPanel;

    [SerializeField] private GameObject LargeDocument;
    [SerializeField] private GameObject DocumentInfo;

    #endregion

    [SerializeField] private List<StoryDataBase> StoryDataBase;
    private StoryDataBase ActualStoryDataBase;
    private int i_investigationIndex = 0;
    private bool b_isGoodAddress = false;

    private void Awake()
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
        for (int y = 0; y < l_AddressesList.Count; y++)
        {
            AddressesList.Add(l_AddressesList[y].GetComponent<Address_data>());
        }
        LaunchNewInvestigation();
        _inkStory = new Story(Story.text);
    }

    void Update () {
        if (b_StoryStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_inkStory.canContinue)
                {
                    UpdateVisualText(_inkStory.Continue());
                }
                else if (!_inkStory.canContinue && _inkStory.currentChoices.Count == 0)
                {
                    LoadScreen();
                    CloseDialogue();
                }
            }
            if (_inkStory.currentChoices.Count > 0 && ChoiceNeeded == false)
            {
                for (int ii = 0; ii < _inkStory.currentChoices.Count; ++ii)
                {
                    Button choice = Instantiate(ChoiceButton, ChoiceSection);
                    Text choiceText = choice.GetComponentInChildren<Text>();
                    choiceText.text = _inkStory.currentChoices[ii].text;

                    int choiceId = ii;
                    choice.onClick.AddListener(delegate { ChoiceSelected(choiceId); });
                    ChoiceNeeded = true;
                }
            }
            PlayerTimeElapsed += Time.deltaTime;
            OtherPlayerTimeElapsed += Time.deltaTime;
            PlayerText.text = GetWords(s_PlayerFullText, (int)(PlayerTimeElapsed * wordsPerSecond));
            OtherCharacterText.text = GetWords(s_OtherCharacterFullText, (int)(OtherPlayerTimeElapsed * wordsPerSecond));
        }
    }

    #region ADDRESS_SYSTEM

    public void SetupAdressesState(bool Active)
    {
        foreach (Button AdressButton in l_AddressesList)
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

    public void DiscoverNewAddress()
    {
        AddressesList[ActualStoryDataBase.AddressIndexToDiscover].gameObject.SetActive(true);
    }

    #endregion

    #region DIALOGUE_SYSTEM

    void RemoveChildren () {
		int childCount = ChoiceSection.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
            Destroy (ChoiceSection.transform.GetChild (i).gameObject);
		}
	}

	public void ChoiceSelected (int id) {
		_inkStory.ChooseChoiceIndex (id);
		ChoiceNeeded = false;
        UpdateVisualText(_inkStory.Continue());
        RemoveChildren();
	}    

    private string GetWords(string text, int wordCount)
    {
        int words = wordCount;
        // loop through each character in text
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == ' ')
            {
                words--;
            }
            if (words <= 0)
            {
                return text.Substring(0, i);
            }
        }
        return text;
    }

    public void OpenDialogue ()
    {
        PlayerText.text = "";
        s_PlayerFullText = "";
        OtherCharacterText.text = "";
        s_OtherCharacterFullText = "";
        UpdateVisualText(_inkStory.Continue());
        b_StoryStarted = true;
    }

    public void CloseDialogue()
    {
        b_StoryStarted = false;

    }

    public void UpdateVisualText(string ContinueText)
    {
        if (_inkStory.currentTags.Count == 1)
        {
            if (_inkStory.currentTags[0] == "player")
            {
                s_PlayerFullText = ContinueText;
                PlayerText.text = "";
                PlayerTimeElapsed = 0f;
            }
            else if (_inkStory.currentTags[0] == "otherCharacter")
            {
                s_OtherCharacterFullText = ContinueText;
                OtherCharacterText.text = "";
                OtherPlayerTimeElapsed = 0f;
            }
        }
    }

    public void SetupNewStory(string newStory)
    {
        _inkStory.ChoosePathString(newStory);
        if (newStory == DefaultKnot)
        {
            DeactivateOtherCharacterDialogue();
        }
        else
        {
            ReactivateOtherCharacterDialogue();
        }
    }

    public void UpdateAddressesStory(List<string> newStories)
    {
        for (int x = 0; x < AddressesList.Count;x++)
        {
            if (newStories[x] != null)
            AddressesList[x].SetActualStory(newStories[x]);
            else
            AddressesList[x].SetActualStory(DefaultKnot);
        }
    }

    public void SetupDialogueSystem()
    {
        DialogueSystem.SetActive(true);
        OpenDialogue();
        ActualTab = DialogueSystem;
    }

    public void CloseDialogueSystem()
    {
        DialogueSystem.SetActive(false);
        CloseDialogue();
    }

    public void DeactivateOtherCharacterDialogue()
    {
        OtherCharacterSection.SetActive(false);
    }

    public void ReactivateOtherCharacterDialogue()
    {
        OtherCharacterSection.SetActive(true);
    }

    #endregion

    #region LOADING_SCREEN

    public void LoadScreen(string method)
    {
        a_LoadingScreenAnimator.SetTrigger("Loading");
        Invoke("CloseTab", 1f);
        Invoke(method, 1f);
    }

    public void LoadScreen()
    {
        a_LoadingScreenAnimator.SetTrigger("Loading");
        Invoke("CloseTab", 1f);
    }

    #endregion

    #region TABS

    public void OpenTab(TabType tab)
    { 
        switch (tab)
        {
            case TabType.Map:
                MapTab.SetActive(true);
                ActualTab = MapTab;
                break;
            case TabType.AddressBook:
                AddressBookTab.SetActive(true);
                ActualTab = AddressBookTab;
                break;
            case TabType.Documents:
                DocumentsTab.SetActive(true);
                ActualTab = DocumentsTab;
                break;
            default:
                break;
        }
    }

    public void CloseTab()
    {
        ActualTab.SetActive(false);
    }

    #endregion

    #region DOCUMENTS

    // Documents Panels
    public void OpenDocumentPanel(DocumentType Type)
    {
        DocumentTypeButtons.SetActive(false);
        CloseDocumentPanelButton.SetActive(true);
        switch (Type)
        {
            case DocumentType.NewsPaper:
                NewsPapers.SetActive(true);
                ActualDocumentPanel = NewsPapers;
                break;
            case DocumentType.WritingNote:
                WritingNotes.SetActive(true);
                ActualDocumentPanel = WritingNotes;
                break;
            case DocumentType.Object:
                Objects.SetActive(true);
                ActualDocumentPanel = Objects;
                break;
            case DocumentType.PoliceRecord:
                PoliceRecord.SetActive(true);
                ActualDocumentPanel = PoliceRecord;
                break;
            default:
                break;
        }
    }

    public void CloseDocumentPanel()
    {
        ActualDocumentPanel.SetActive(false);
        DocumentTypeButtons.SetActive(true);
        CloseDocumentPanelButton.SetActive(false);
    }

    // Large Document
    public void OpenLargeDocument(Sprite sprite)
    {
        LargeDocument.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        LargeDocument.SetActive(true);
    }

    public void CloseLargeDocument()
    {
        LargeDocument.SetActive(false);
    }

    // Document Info

    public void ActivateDocumentInfo(string Text)
    {
        DocumentInfo.SetActive(true);
        DocumentInfo.GetComponent<DocumentInfo>().UpdateText(Text);
    }

    public void DeactivateDocumentInfo()
    {
        DocumentInfo.SetActive(false);
    }

    #endregion

    public void LaunchNewInvestigation()
    {
        ActualStoryDataBase = StoryDataBase[i_investigationIndex];
        UpdateAddressesStory(ActualStoryDataBase.StoryData);
        i_investigationIndex++;
    }

    public void TestGoodAddress(string AddressToTest)
    {
        if (ActualStoryDataBase.AddressInfos != null)
        {
            if (AddressToTest == ActualStoryDataBase.AddressInfos)
            {
                b_isGoodAddress = true;
            }
            else
            {
                b_isGoodAddress = false;
            }
        }
    }

    public void CallTaxi()
    {
        if (b_isGoodAddress)
        {
            SetupNewStory(AddressesList[ActualStoryDataBase.AddressIndexToDiscover].GetActualStory());
            b_isGoodAddress = false;
            DiscoverNewAddress();
        }
        else
        {
            SetupNewStory(DefaultKnot);
        }
        SetupDialogueSystem();
    }

    public void GoToAddres(string s)
    {
        LoadScreen("SetupDialogueSystem");
        SetupNewStory(s);
    }
}
