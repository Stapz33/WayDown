using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine.UI;
using System;

public enum TabType {Map, AddressBook, Documents}
public enum DocumentFolder {CriminalRecord, AddressD01, AddressD02, AddressD03, AddressD04, AddressD05 }

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

    #region SAVE_SYSTEM_DATA
    [Serializable]
    public class Data
    {
        public int i_investigationIndex = 0;
        public bool m_IsCriminalKnown = false;
        public string s_Story = "";
        public List<bool> AddressState = new List<bool>();


        //variables
        public int knowledgeSpaghetty = 0;

    }

    [Serializable]
    public class DocumentDatas
    {
        [Serializable]
        public struct DocumentStruct
        {
            public int doc;
            public DocumentFolder doctype;
        }

        public List<DocumentStruct> doclist = new List<DocumentStruct>();
    }
    private Data ActualDatas;
    private DocumentDatas DocDatas;
    private DocumentFolder AddressActualFolder;
    
    [Header("Save/Load")]
    public GameObject SaveState;


    #endregion

    #region DOCUMENTS_DATA

    [Header("Documents")]
    private List<Transform> l_DocumentsDrawer = new List<Transform>();
    private List<int> l_DocumentsDrawerDocNB = new List<int>();
    [SerializeField] private GameObject DocumentTypeButtons;
    [SerializeField] private Transform DocumentsDrawerParent;
    [SerializeField] private Transform DocumentsDrawerButtonsParent;
    [SerializeField] private GameObject CloseDocumentPanelButton;

    private int ActualDocumentPanel;

    [SerializeField] private LargeDocumentManager m_LargeDocument;
    [SerializeField] private GameObject DocumentInfo;
    private bool b_isAddDocNavigation = false;

    #endregion

    [SerializeField] private List<StoryDataBase> StoryDataBase;
    private StoryDataBase ActualStoryDataBase;
    private bool b_isGoodAddress = false;

    [SerializeField] private GameObject PoliceOfficeObject;

    [SerializeField] private DocumentScriptable DocumentDataBase;

    [SerializeField] private Transform PoliceDropdownParent;
    private List<Dropdown> m_PoliceDropdown = new List<Dropdown>();

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
        
    }

    private void Start()
    {
        for (int i = 0; i < AddressesParent.childCount; i++)
        {
            l_AddressesList.Add(AddressesParent.GetChild(i).GetComponent<Button>());
        }
        for (int y = 0; y < l_AddressesList.Count; y++)
        {
            AddressesList.Add(l_AddressesList[y].GetComponent<Address_data>());
        }
        for (int x = 0; x < PoliceDropdownParent.childCount; x++)
        {
            m_PoliceDropdown.Add(PoliceDropdownParent.GetChild(x).GetComponent<Dropdown>());
        }
        for (int h = 0; h < DocumentsDrawerParent.childCount - 1; h++)
        {
            l_DocumentsDrawer.Add(DocumentsDrawerParent.GetChild(h));
            l_DocumentsDrawerDocNB.Add(0);
        }
        _inkStory = new Story(Story.text);
        if (SaveManager.Singleton.LoadStoryPath() != null)
        {
            LoadGame();
            Debug.Log("Loading");
        }
        else
        {
            ActualDatas = new Data();
            DocDatas = new DocumentDatas();
            DocDatas.doclist = new List<DocumentDatas.DocumentStruct>();
            for (int y = 0; y < AddressesList.Count; y++)
            {
                ActualDatas.AddressState.Add(AddressesList[y].gameObject.activeSelf);
            }
            LaunchNewInvestigation();
            Debug.Log("New Game");
        }
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
                    LoadScreen(true);
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
        Address_data data = AddressesList[ActualStoryDataBase.AddressIndexToDiscover];
        data.gameObject.SetActive(true);
        SetNewActualAddressDocumentFolder(data.GetDocumentFolder());
    }

    public void UpdateDiscoveredAdress()
    {
        for (int j = 0; j < AddressesList.Count; j++)
        {
            AddressesList[j].gameObject.SetActive(ActualDatas.AddressState[j]);
        }
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
        if (_inkStory.currentTags.Count >= 1)
        {
            for (int f = 0; f <_inkStory.currentTags.Count; f++)
            {
                if (_inkStory.currentTags[f] == "player")
                {
                    s_PlayerFullText = ContinueText;
                    PlayerText.text = "";
                    PlayerTimeElapsed = 0f;
                }
                else if (_inkStory.currentTags[f] == "otherCharacter")
                {
                    s_OtherCharacterFullText = ContinueText;
                    OtherCharacterText.text = "";
                    OtherPlayerTimeElapsed = 0f;
                }
                else if (_inkStory.currentTags[f] == "NewInvestigation")
                {
                    LaunchNewInvestigation();
                }
                else if (_inkStory.currentTags[f] == "NewDocument")
                {
                    AddNewDocumentAndShowIt(int.Parse(_inkStory.currentTags[f + 1]),AddressActualFolder,false);
                }
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

    public void LoadScreen(string method, bool NeedToCloseTab)
    {
        a_LoadingScreenAnimator.SetTrigger("Loading");
        if (NeedToCloseTab)
        {
            Invoke("CloseTab", 1f);
        }
        Invoke(method, 1f);
    }

    public void LoadScreen(bool NeedToCloseTab)
    {
        a_LoadingScreenAnimator.SetTrigger("Loading");
        if (NeedToCloseTab)
        {
            Invoke("CloseTab", 1f);
        }
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
    public void OpenDocumentPanel(DocumentFolder Type)
    {
        DocumentTypeButtons.SetActive(false);
        CloseDocumentPanelButton.SetActive(true);
        DocumentsDrawerParent.gameObject.SetActive(true);
        l_DocumentsDrawer[(int)Type].gameObject.SetActive(true);
        ActualDocumentPanel = (int)Type;
    }

    public void CloseDocumentPanel()
    {
        DocumentsDrawerParent.gameObject.SetActive(false);
        l_DocumentsDrawer[ActualDocumentPanel].gameObject.SetActive(false);
        DocumentTypeButtons.SetActive(true);
        CloseDocumentPanelButton.SetActive(false);
    }

    // Large Document
    

    public void AddNewDocumentAndShowIt(int DocumentIdx, DocumentFolder Type, bool isCriminalRecord)
    {
        DocumentDatas.DocumentStruct yes = new DocumentDatas.DocumentStruct();
        yes.doc = DocumentIdx;
        yes.doctype = Type;
        DocDatas.doclist.Add(yes);
        SaveManager.Singleton.SaveDocumentPath(DocDatas);
        if (!DocumentsDrawerButtonsParent.GetChild((int)Type).gameObject.activeSelf)
        {
            DocumentsDrawerButtonsParent.GetChild((int)Type).gameObject.SetActive(true);
        }
        if (isCriminalRecord)
        {
            ActualDatas.m_IsCriminalKnown = true;
        }
        InstantiateDocument(DocumentIdx, (int)Type,true);
    }

    public void InstantiateDocument(int DocumentIdx, int DrawerIdx,bool needToShow)
    {
        GameObject Document = null;
        if (l_DocumentsDrawerDocNB[DrawerIdx] >= 24)
        {
            l_DocumentsDrawer[DrawerIdx].GetComponent<DocumentFolderManager>().UnlockTab(3);
            Document = Instantiate(DocumentDataBase.Documents[DocumentIdx], l_DocumentsDrawer[DrawerIdx].transform.GetChild(3));
        }
        else if (l_DocumentsDrawerDocNB[DrawerIdx] >= 16)
        {
            l_DocumentsDrawer[DrawerIdx].GetComponent<DocumentFolderManager>().UnlockTab(2);
            Document = Instantiate(DocumentDataBase.Documents[DocumentIdx], l_DocumentsDrawer[DrawerIdx].transform.GetChild(2));
        }
        else if (l_DocumentsDrawerDocNB[DrawerIdx] >= 8)
        {
            l_DocumentsDrawer[DrawerIdx].GetComponent<DocumentFolderManager>().UnlockTab(1);
            Document = Instantiate(DocumentDataBase.Documents[DocumentIdx], l_DocumentsDrawer[DrawerIdx].transform.GetChild(1));
        }
        else
        {
            Document = Instantiate(DocumentDataBase.Documents[DocumentIdx], l_DocumentsDrawer[DrawerIdx].transform.GetChild(0));
        }
        l_DocumentsDrawerDocNB[DrawerIdx]++;
        if (needToShow)
        ShowLargeDocumentSingle(Document.GetComponent<Image>().sprite);
    }



    public void ShowLargeDocumentSingle(Sprite sprite)
    {
        m_LargeDocument.gameObject.SetActive(true);
        m_LargeDocument.UpdateSingleDoc(sprite);
    }

    public void ShowLargeDocumentMulti(Sprite sprite)
    {
        if (!b_isAddDocNavigation)
        {
            m_LargeDocument.gameObject.SetActive(true);
            m_LargeDocument.UpdateMultiDoc01(sprite);
        }
        if (b_isAddDocNavigation)
        {
            m_LargeDocument.gameObject.SetActive(true);
            m_LargeDocument.UpdateMultiDoc02(sprite);
            b_isAddDocNavigation = false;
        }

    }

    public void CloseLargeDocument()
    {
        m_LargeDocument.gameObject.SetActive(false);
        m_LargeDocument.HideMultiDoc();
        m_LargeDocument.HideSingleDoc();
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
        ActualStoryDataBase = StoryDataBase[ActualDatas.i_investigationIndex];
        UpdateAddressesStory(ActualStoryDataBase.StoryData);
        ActualDatas.m_IsCriminalKnown = false;
        ActualDatas.i_investigationIndex++;
        SaveGame();
    }

    public void LoadInvestigation()
    {
        ActualStoryDataBase = StoryDataBase[ActualDatas.i_investigationIndex - 1];
        UpdateAddressesStory(ActualStoryDataBase.StoryData);
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
            if (ActualStoryDataBase.m_LaunchNewInvestigation)
            {
                LaunchNewInvestigation();
            }
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

    public void GoToAddres(string s,DocumentFolder documentFolder)
    {
        LoadScreen("SetupDialogueSystem",true);
        SetupNewStory(s);
        SetNewActualAddressDocumentFolder(documentFolder);
    }

    public string GetActualName()
    {
        if (ActualStoryDataBase.CriminalName != null)
        {
            return ActualStoryDataBase.CriminalName;
        }
        return null;
    }

    public void OpenPoliceOffice()
    {
        PoliceOfficeObject.SetActive(true);
        PoliceOffice.Singleton.UpdateNormalInspector();
    }

    public void ClosePoliceOffice()
    {
        PoliceOfficeObject.SetActive(false);
    }

    public void NameValidation(string name)
    {
        string GoodName = GetActualName();
        if (GoodName != "")
        {
            if (name == GoodName && !ActualDatas.m_IsCriminalKnown)
            {
                PoliceOffice.Singleton.GoodName();
                Invoke("AddNewCriminalRecord", 1.5f);
            }
            else if (name == GoodName && ActualDatas.m_IsCriminalKnown)
            {
                PoliceOffice.Singleton.AlreadyGood();
            }
            else 
            {
                PoliceOffice.Singleton.WrongName();
            }
        }
        else
        {
            PoliceOffice.Singleton.WrongName();
        }
    }

    public void AddNewCriminalRecord()
    {
        AddNewDocumentAndShowIt(ActualStoryDataBase.CriminalRecordIdx, DocumentFolder.CriminalRecord, true);
        if (ActualStoryDataBase.m_LaunchNewInvestigation)
        {
            LaunchNewInvestigation();
        }
        else
        {
            SaveGame();
        }
    }

    /// Police Record
    /// 
    /// Composite Sketch
    /// 

    public void ResetDropdownValue()
    {
        foreach(Dropdown x in m_PoliceDropdown)
        {
            x.value = 0;
        }
    }

    public void TestCS()
    {
        int nb = 0;
        if (ActualStoryDataBase.Corpulence != Corpulence.Null)
        {
            if ((int)ActualStoryDataBase.Corpulence == m_PoliceDropdown[0].value)
            {
                nb++;
            }
        }
        else if (ActualStoryDataBase.Corpulence == Corpulence.Null && m_PoliceDropdown[0].value != 0)
        {
            nb = -100;
        }
        if (ActualStoryDataBase.Height != Height.Null)
        {
            if ((int)ActualStoryDataBase.Height == m_PoliceDropdown[1].value)
            {
                nb++;
            }
        }
        else if (ActualStoryDataBase.Height == Height.Null && m_PoliceDropdown[1].value != 0)
        {
            nb = -100;
        }
        if (ActualStoryDataBase.SexType != SexType.Null)
        {
            if ((int)ActualStoryDataBase.SexType == m_PoliceDropdown[2].value)
            {
                nb++;
            }
        }
        else if (ActualStoryDataBase.SexType == SexType.Null && m_PoliceDropdown[2].value != 0)
        {
            nb = -100;
        }
        if (ActualStoryDataBase.Ethnicity != Ethnicity.Null)
        {
            if ((int)ActualStoryDataBase.Ethnicity == m_PoliceDropdown[3].value)
            {
                nb++;
            }
        }
        else if (ActualStoryDataBase.Ethnicity == Ethnicity.Null && m_PoliceDropdown[3].value != 0)
        {
            nb = -100;
        }
        if (ActualStoryDataBase.HairType != HairType.Null)
        {
            if ((int)ActualStoryDataBase.HairType == m_PoliceDropdown[4].value)
            {
                nb++;
            }
        }
        else if (ActualStoryDataBase.HairType == HairType.Null && m_PoliceDropdown[4].value != 0)
        {
            nb = -100;
        }
        if (ActualStoryDataBase.HairColor != HairColor.Null)
        {
            if ((int)ActualStoryDataBase.HairColor == m_PoliceDropdown[5].value)
            {
                nb++;
            }
        }
        else if (ActualStoryDataBase.HairColor == HairColor.Null && m_PoliceDropdown[5].value != 0)
        {
            nb = -100;
        }
        if (ActualStoryDataBase.EyeColor != EyeColor.Null)
        {
            if ((int)ActualStoryDataBase.EyeColor == m_PoliceDropdown[6].value)
            {
                nb++;
            }
        }
        else if (ActualStoryDataBase.EyeColor == EyeColor.Null && m_PoliceDropdown[6].value != 0)
        {
            nb = -100;
        }
        if (ActualStoryDataBase.TattooPiercing != TattooPiercing.Null)
        {
            if ((int)ActualStoryDataBase.TattooPiercing == m_PoliceDropdown[7].value)
            {
                nb++;
            }
        }
        else if (ActualStoryDataBase.TattooPiercing == TattooPiercing.Null && m_PoliceDropdown[7].value != 0)
        {
            nb = -100;
        }
        if (nb == ActualStoryDataBase.HintNeeded && !ActualDatas.m_IsCriminalKnown)
        {
            PoliceOffice.Singleton.GoodName();
            Invoke("AddNewCriminalRecord", 1.5f);
        }
        else if (nb == ActualStoryDataBase.HintNeeded && ActualDatas.m_IsCriminalKnown)
        {
            PoliceOffice.Singleton.AlreadyGood();
        }
        else
        {
            PoliceOffice.Singleton.WrongCS();
        }
    }

    public void SaveGame()
    {
        SaveState.SetActive(true);
        Invoke("DeactivateSaveState", 4f);
        SaveStoryVar();
        ActualDatas.s_Story = _inkStory.state.ToJson();
        for (int a = 0; a < AddressesList.Count; a++)
        {
            ActualDatas.AddressState[a] = AddressesList[a].gameObject.activeSelf;
        }
        SaveManager.Singleton.SaveStoryPath(ActualDatas);
    }

    public void LoadGame()
    {
        ActualDatas = SaveManager.Singleton.LoadStoryPath();
        DocDatas = SaveManager.Singleton.LoadDocumentPath();
        UpdateDocumentState();
        UpdateDiscoveredAdress();
        _inkStory.state.LoadJson(ActualDatas.s_Story);
        LoadStoryVar();
        LoadInvestigation();
    }

    public void UpdateDocumentState()
    {
        if (DocDatas != null)
        {
            foreach (DocumentDatas.DocumentStruct doc in DocDatas.doclist)
            {
                InstantiateDocument(doc.doc, (int)doc.doctype,false);
                if (!DocumentsDrawerButtonsParent.GetChild((int)doc.doctype).gameObject.activeSelf)
                {
                    DocumentsDrawerButtonsParent.GetChild((int)doc.doctype).gameObject.SetActive(true);
                }
            }
        }
    }

    public void DeactivateSaveState()
    {
        SaveState.SetActive(false);
    }

    public void SaveStoryVar()
    {
        ActualDatas.knowledgeSpaghetty = (int)_inkStory.variablesState["knowledge_Spaghetti"];
    }

    public void LoadStoryVar()
    {
        _inkStory.variablesState["knowledge_Spaghetti"] = ActualDatas.knowledgeSpaghetty;
    }

    public void SetNewActualAddressDocumentFolder(DocumentFolder documentFolder)
    {
        Debug.Log("New Doc Folder");
        AddressActualFolder = documentFolder;
    }

    public void AddNewDocToComparision()
    {
        m_LargeDocument.gameObject.SetActive(false);
        m_LargeDocument.AddDoc();
        b_isAddDocNavigation = true;
    }

    
}
