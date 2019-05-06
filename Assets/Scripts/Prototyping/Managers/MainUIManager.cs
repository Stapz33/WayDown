using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using RichTextSubstringHelper;

public enum TabType {Map, AddressBook, Documents}
public enum DocumentFolder {CriminalRecord, ProstituteMotel, CapoAppartment, Bar, DriverAppartment, ClientAppartment, Drugstore, Restaurant }

public class MainUIManager : MonoBehaviour {

    public static MainUIManager Singleton { get; private set; }

    #region ADDRESS_SYSTEM_DATA

    private List<Button> l_AddressesList = new List<Button>();
    public Transform AddressesParent;

    #endregion

    #region TABS_DATA

    [SerializeField] private GameObject MapTab = null;
    [SerializeField] private GameObject AddressBookTab = null;
    [SerializeField] private GameObject DocumentsTab = null;
    private GameObject ActualTab;

    #endregion

    #region LOADING_SCREEN_DATA

    [SerializeField] private Animator a_LoadingScreenAnimator = null;

    #endregion

    #region DIALOGUE_SYSTEM_SETUP_DATA

    // Prefabs & Assets
    [Header("Dialogue System")]
    public GameObject DialogueSystem;
    public GameObject OtherCharacterSection;
    public GameObject OtherCharacterDialogue;
    public GameObject PlayerDialogue;
    public Transform ChoiceSection;
    public Text PlayerText;
    public Text OtherCharacterText;
    public Button ChoiceButton;
    [SerializeField] private Image OtherCharacterSprite = null;
    [SerializeField] private Image DialogueBackground = null;
    [SerializeField] private GameObject m_CharacterVinyle = null;
    [SerializeField] private GameObject m_OtherCharacterVinyle = null;
    public List<Sprite> PlayerDialogueSprites;
    public List<Sprite> OCDialogueSprites;



    // Variables/ speed of typewriter
    private bool ChoiceNeeded = false;
	private bool b_StoryStarted = false;
    private string s_PlayerFullText = "";
    private string s_OtherCharacterFullText = "";
    private bool isFirstStart = true;
    
    public float TextSpeed;
    private float TextCooldown = 0f;
    private string CurrentText = "";
    private int textlength = 0;
    private int characterText = 0;

    #endregion

    #region STORY_SETUP_DATA

    [Header("Story")]
    // Story
    [SerializeField] private TextAsset Story = null;
    private Story _inkStory;
    public string DefaultKnot;
    private List<Address_data> AddressesList = new List<Address_data>();
    [SerializeField] private List<StoryDataBase> StoryDataBase = new List<StoryDataBase>();
    private StoryDataBase ActualStoryDataBase;

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
        public int prostituteknown = 0;


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

    [SerializeField] private GameObject ExitMenu = null;

    #endregion

    #region DOCUMENTS_DATA

    
    private List<Transform> l_DocumentsDrawer = new List<Transform>();
    private List<int> l_DocumentsDrawerDocNB = new List<int>();
    [Header("Documents")]
    [SerializeField] private GameObject NewDocumentFeedback = null;
    [SerializeField] private GameObject DocumentTypeButtons = null;
    [SerializeField] private Transform DocumentsDrawerParent = null;
    [SerializeField] private Transform DocumentsDrawerButtonsParent = null;
    [SerializeField] private GameObject CloseDocumentPanelButton = null;

    private int ActualDocumentPanel;

    [SerializeField] private LargeDocumentManager m_LargeDocument = null;
    [SerializeField] private GameObject DocumentInfo = null;
    private bool b_isAddDocNavigation = false;

    #endregion

    
    private bool b_isGoodAddress = false;

    [Header("Police Office")]
    [SerializeField] private GameObject PoliceOfficeObject = null;
    [SerializeField] private Transform PoliceDropdownParent = null;
    private List<Dropdown> m_PoliceDropdown = new List<Dropdown>();

    [Header("Data Bases")]
    [SerializeField] private DocumentScriptable DocumentDataBase = null;
    [SerializeField] private DialogueScriptable DialogueDataBase = null;


    [Header("Log System")]
    [SerializeField] private GameObject m_LogManager = null;
    [SerializeField] private GameObject m_LogManagerButton = null;

    [Header("Demo")]
    public GameObject m_demoImage;


    int i_TextFramingSound = 0;
    private bool b_iscontinuing = false;

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
        AudioManager.Singleton.ToggleStartRadio();
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
        for (int h = 1; h < DocumentsDrawerParent.childCount ; h++)
        {
            l_DocumentsDrawer.Add(DocumentsDrawerParent.GetChild(h));
            l_DocumentsDrawerDocNB.Add(0);
        }
        _inkStory = new Story(Story.text);
        if (SaveManager.Singleton.LoadStoryPath() != null)
        {
            LoadGame();
            isFirstStart = false;
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
            SaveManager.Singleton.SaveDocumentPath(DocDatas);
            b_isGoodAddress = true;
            CallTaxi();
            m_LogManager.GetComponent<LogManager>().StartingScript();
        }
    }

    void Update () {
        if (b_StoryStarted)
        {
            if (b_iscontinuing)
            {
                if (_inkStory.canContinue)
                {
                    UpdateVisualText(_inkStory.Continue());
                    m_CharacterVinyle.SetActive(false);
                    m_OtherCharacterVinyle.SetActive(false);
                }
                else if (!_inkStory.canContinue && _inkStory.currentChoices.Count == 0)
                {
                    LoadScreen(true);
                    CloseDialogue();
                    SaveGame();
                    AudioManager.Singleton.DeskCheckRadio();
                    AudioManager.Singleton.StopMusic();
                    m_CharacterVinyle.SetActive(false);
                    m_OtherCharacterVinyle.SetActive(false);
                }
                b_iscontinuing = false;
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
                    m_CharacterVinyle.SetActive(false);
                    m_OtherCharacterVinyle.SetActive(false);
                }
            }
            if (!m_CharacterVinyle.activeSelf && PlayerText.text == s_PlayerFullText && OtherCharacterText.text == s_OtherCharacterFullText && _inkStory.currentChoices.Count == 0 && characterText == 0)
            {
                m_CharacterVinyle.SetActive(true);
            }
            else if (!m_CharacterVinyle.activeSelf && PlayerText.text == s_PlayerFullText && OtherCharacterText.text == s_OtherCharacterFullText && _inkStory.currentChoices.Count == 0 && characterText == 1)
            {
                m_OtherCharacterVinyle.SetActive(true);
            }

            if (TextCooldown > 0)
            {
                TextCooldown -= Time.deltaTime;
                if (TextCooldown <= 0)
                {
                    
                    if (s_PlayerFullText != "")
                    {
                        textlength++;
                        if (characterText == 0)
                        {
                            CurrentText = s_PlayerFullText.RichTextSubString(textlength);
                            //CurrentText = s_PlayerFullText.Substring(0, textlength);
                            PlayerText.text = CurrentText;
                            if (CurrentText == s_PlayerFullText)
                            {
                                textlength = 0;
                                TextCooldown = 0f;
                            }
                            else
                            {
                                if (i_TextFramingSound == 1)
                                {
                                    AudioManager.Singleton.ActivateAudio(AudioType.Text);
                                    i_TextFramingSound = 0;
                                }
                                else i_TextFramingSound++;
                                TextCooldown = TextSpeed;
                            }
                        }
                        else if (characterText == 1)
                        {
                            CurrentText = s_OtherCharacterFullText.RichTextSubString(textlength);
                            //CurrentText = s_OtherCharacterFullText.Substring(0, textlength);
                            OtherCharacterText.text = CurrentText;
                            if (CurrentText == s_OtherCharacterFullText)
                            {
                                textlength = 0;
                                TextCooldown = 0f;
                            }
                            else
                            {
                                if (i_TextFramingSound == 1)
                                {
                                    AudioManager.Singleton.ActivateAudio(AudioType.Text);
                                    i_TextFramingSound = 0;
                                }
                                else i_TextFramingSound++;
                                TextCooldown = TextSpeed;
                            }
                        }
                    }
                    else
                    {
                        if (characterText == 0)
                        {
                            CurrentText = s_PlayerFullText;
                            PlayerText.text = CurrentText;
                            textlength = 0;
                            TextCooldown = 0f;
                        }
                        else if (characterText == 1)
                        {
                            CurrentText = s_OtherCharacterFullText;
                            OtherCharacterText.text = CurrentText;
                            textlength = 0;
                            TextCooldown = 0f;
                        }
                    }
                }
                
            }
            
        }

        if (Input.GetKeyDown("escape"))
        {
            ExitMenu.SetActive(true);
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
                    DeactivateOtherCharacterDialogue();
                    ReactivatePlayerDialogue();
                    s_PlayerFullText = ContinueText.Replace("\n","").Replace("<color=yellow>", "<color=#D6A80C>");
                    characterText = 0;
                    TextCooldown = TextSpeed;
                    PlayerText.text = "";
                }
                else if (_inkStory.currentTags[f] == "otherCharacter")
                {
                    ReactivateOtherCharacterSection();
                    DeactivatePlayerDialogue();
                    ReactivateOtherCharacterDialogue();
                    s_OtherCharacterFullText = ContinueText.Replace("\n", "").Replace("<color=yellow>", "<color=#D6A80C>");
                    characterText = 1;
                    TextCooldown = TextSpeed;
                    OtherCharacterText.text = "";
                }
                else if (_inkStory.currentTags[f] == "NewInvestigation")
                {
                    LaunchNewInvestigation();
                }
                else if (_inkStory.currentTags[f] == "NewDocument")
                {
                    AddNewDocumentAndShowIt(int.Parse(_inkStory.currentTags[f + 1]),AddressActualFolder,false);
                }
                else if (_inkStory.currentTags[f] == "NewCharacterSprite")
                {
                    OtherCharacterSprite.sprite = DialogueDataBase.Characters[int.Parse(_inkStory.currentTags[f + 1])];
                }
                else if (_inkStory.currentTags[f] == "NewBackground")
                {
                    DialogueBackground.sprite = DialogueDataBase.Backgrounds[int.Parse(_inkStory.currentTags[f + 1])];
                }
                else if (_inkStory.currentTags[f] == "DisableDiscussion")
                {
                    DeactivateOtherCharacterSection();
                }
                else if (_inkStory.currentTags[f] == "ActivateDiscussion")
                {
                    ReactivateOtherCharacterSection();
                }
                else if (_inkStory.currentTags[f] == "NewNarrativeLog")
                {
                    m_LogManager.GetComponent<LogManager>().AddNLog(int.Parse(_inkStory.currentTags[f + 1]),true);
                }
                else if (_inkStory.currentTags[f] == "NewCharacterLog")
                {
                    m_LogManager.GetComponent<LogManager>().AddCLog(int.Parse(_inkStory.currentTags[f + 1]), true);
                }
                else if (_inkStory.currentTags[f] == "SFXPlay")
                {
                    AudioManager.Singleton.ActivateAudio((AudioType)int.Parse(_inkStory.currentTags[f + 1]));
                }
                else if (_inkStory.currentTags[f] == "SFXStop")
                {
                    AudioManager.Singleton.StopAudio((AudioType)int.Parse(_inkStory.currentTags[f + 1]));
                }
                else if (_inkStory.currentTags[f] == "MusicPlay")
                {
                    AudioManager.Singleton.ChangeMusic(int.Parse(_inkStory.currentTags[f + 1]));
                }
                else if (_inkStory.currentTags[f] == "OtherCharacterDbox")
                {
                    OtherCharacterDialogue.GetComponent<Image>().sprite = OCDialogueSprites[int.Parse(_inkStory.currentTags[f + 1])];
                }
                else if (_inkStory.currentTags[f] == "PlayerDBox")
                {
                    PlayerDialogue.GetComponent<Image>().sprite = PlayerDialogueSprites[int.Parse(_inkStory.currentTags[f + 1])];
                }
                else if (_inkStory.currentTags[f] == "Demo")
                {
                    LoadScreen("GoToDemo",false);
                }
            }
        }
    }

    public void SetupNewStory(string newStory)
    {
        _inkStory.ChoosePathString(newStory);
        if (newStory == DefaultKnot)
        {
            DeactivateOtherCharacterSection();
        }
        else
        {
            ReactivateOtherCharacterSection();
        }
    }

    public void UpdateAddressesStory(List<string> newStories)
    {
        for (int x = 0; x < AddressesList.Count;x++)
        {
            if (newStories[x] != "")
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

    public void DeactivateOtherCharacterDialogue()
    {
        OtherCharacterDialogue.SetActive(false);
    }

    public void DeactivateOtherCharacterSection()
    {
        OtherCharacterSection.SetActive(false);
    }

    public void ReactivateOtherCharacterSection()
    {
        OtherCharacterSection.SetActive(true);
    }

    public void ReactivateOtherCharacterDialogue()
    {
        OtherCharacterDialogue.SetActive(true);
    }

    public void DeactivatePlayerDialogue()
    {
        PlayerDialogue.SetActive(false);
    }

    public void ReactivatePlayerDialogue()
    {
        PlayerDialogue.SetActive(true);
    }

    #endregion

    #region LOADING_SCREEN

    public void LoadScreen(string method, bool NeedToCloseTab)
    {
        AudioManager.Singleton.ActivateAudio(AudioType.LoadingTransition);
        a_LoadingScreenAnimator.SetTrigger("Loading");
        if (NeedToCloseTab)
        {
            Invoke("CloseTab", 1f);
        }
        Invoke(method, 1f);
    }

    public void LoadScreen(bool NeedToCloseTab)
    {
        AudioManager.Singleton.ActivateAudio(AudioType.LoadingTransition);
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
                AudioManager.Singleton.ActivateAudio(AudioType.MapOpen);
                MapTab.SetActive(true);
                ActualTab = MapTab;
                break;
            case TabType.AddressBook:
                AudioManager.Singleton.ActivateAudio(AudioType.AddressBookOpen);
                AddressBookTab.SetActive(true);
                ActualTab = AddressBookTab;
                break;
            case TabType.Documents:
                AudioManager.Singleton.ActivateAudio(AudioType.DrawerOpen);
                DocumentsTab.SetActive(true);
                NewDocumentFeedback.SetActive(false);
                ActualTab = DocumentsTab;
                break;
            default:
                break;
        }
    }

    public void CloseTab()
    {
        if (ActualTab == DocumentsTab && b_isAddDocNavigation)
        {
            m_LargeDocument.CloseComparison();
            b_isAddDocNavigation = false;
        }
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
        AudioManager.Singleton.ActivateAudio(AudioType.NewDocument);
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
        NewDocumentFeedback.SetActive(true);
    }

    public void InstantiateDocument(int DocumentIdx, int DrawerIdx,bool needToShow)
    {
        GameObject Document = null;
        if (l_DocumentsDrawerDocNB[DrawerIdx] >= 24)
        {
            l_DocumentsDrawer[DrawerIdx].GetComponent<DocumentFolderManager>().UnlockTab(3);
            Document = Instantiate(DocumentDataBase.Documents[DocumentIdx], l_DocumentsDrawer[DrawerIdx].transform.GetChild(0).GetChild(3));
        }
        else if (l_DocumentsDrawerDocNB[DrawerIdx] >= 16)
        {
            l_DocumentsDrawer[DrawerIdx].GetComponent<DocumentFolderManager>().UnlockTab(2);
            Document = Instantiate(DocumentDataBase.Documents[DocumentIdx], l_DocumentsDrawer[DrawerIdx].transform.GetChild(0).GetChild(2));
        }
        else if (l_DocumentsDrawerDocNB[DrawerIdx] >= 8)
        {
            l_DocumentsDrawer[DrawerIdx].GetComponent<DocumentFolderManager>().UnlockTab(1);
            Document = Instantiate(DocumentDataBase.Documents[DocumentIdx], l_DocumentsDrawer[DrawerIdx].transform.GetChild(0).GetChild(1));
        }
        else
        {
            Document = Instantiate(DocumentDataBase.Documents[DocumentIdx], l_DocumentsDrawer[DrawerIdx].transform.GetChild(0).GetChild(0));
        }
        l_DocumentsDrawerDocNB[DrawerIdx]++;
        if (needToShow)
        {
            if (Document.GetComponent<ButtonCall>().BWText != null)
                ShowLargeDocumentSingle(Document.GetComponent<Image>().sprite, Document.GetComponent<ButtonCall>().BWText);
            else
                ShowLargeDocumentSingle(Document.GetComponent<Image>().sprite);
        }
        
    }



    public void ShowLargeDocumentSingle(Sprite sprite,Texture2D BW)
    {
        m_LargeDocument.gameObject.SetActive(true);
        m_LargeDocument.UpdateSingleDoc(sprite, BW);
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
        m_LargeDocument.CloseComparison();
    }

    public void CloseLargeDocumentSolo()
    { 
        m_LargeDocument.gameObject.SetActive(false);
        m_LargeDocument.HideMultiDoc();
        m_LargeDocument.HideSingleDocSolo();
        m_LargeDocument.CloseComparison();
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
        if (isFirstStart)
        {
            isFirstStart = false;
            return;
        }
        else
        {
            SaveGame();
        }
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
            DialogueBackground.sprite = DialogueDataBase.Backgrounds[4];
        }
        SetupDialogueSystem();
        AudioManager.Singleton.StopRadio();
    }

    public void GoToAddres(string s,DocumentFolder documentFolder,Sprite BG)
    {
        AudioManager.Singleton.StopRadio();
        LoadScreen("SetupDialogueSystem",true);
        SetupNewStory(s);
        SetNewActualAddressDocumentFolder(documentFolder);
        DialogueBackground.sprite = BG;
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
        AudioManager.Singleton.StopRadio();
        PoliceOfficeObject.SetActive(true);
        PoliceOffice.Singleton.UpdateNormalInspector();
    }

    public void ClosePoliceOffice()
    {
        AudioManager.Singleton.DeskCheckRadio();
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
        if (ActualStoryDataBase.CriminalBool != "")
            _inkStory.variablesState[ActualStoryDataBase.CriminalBool] = true;
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
        if (ActualStoryDataBase.investigationType == InvestigationType.CriminalRecord && ActualStoryDataBase.criminalRecordType == CriminalRecordType.CompositeSketch)
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
        m_LogManager.GetComponent<LogManager>().SaveLog();
    }

    public void LoadGame()
    {
        ActualDatas = SaveManager.Singleton.LoadStoryPath();
        DocDatas = SaveManager.Singleton.LoadDocumentPath();
        m_LogManager.GetComponent<LogManager>().LoadLog(SaveManager.Singleton.LoadLogPath());
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
        ActualDatas.prostituteknown = (int)_inkStory.variablesState["knowledge_prostitute_name"];
    }

    public void LoadStoryVar()
    {
        _inkStory.variablesState["knowledge_Spaghetti"] = ActualDatas.knowledgeSpaghetty;
        _inkStory.variablesState["knowledge_prostitute_name"] = ActualDatas.prostituteknown;
    }

    public void SetNewActualAddressDocumentFolder(DocumentFolder documentFolder)
    {
        AddressActualFolder = documentFolder;
    }

    public void AddNewDocToComparision()
    {
        m_LargeDocument.gameObject.SetActive(false);
        m_LargeDocument.AddDoc();
        b_isAddDocNavigation = true;
    }

    public void ReturnToMenu()
    {
        ExitMenu.SetActive(false);
        m_LogManager.GetComponent<LogManager>().SaveLog();
        a_LoadingScreenAnimator.SetTrigger("LoadBlack");
        Invoke("async", 1f);
    }

    public void ContinueGame()
    {
        ExitMenu.SetActive(false);
    }

    public void async()
    {
        StartCoroutine("LoadSceneAsync");
    }

    IEnumerator LoadSceneAsync()
    {
        AudioManager.Singleton.StopDefinitiveRadio();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    #region NARRATIVE_LOG
    public void ShowNarrativeLog()
    {
        m_LogManager.SetActive(true);
        m_LogManager.GetComponent<LogManager>().ResetLogImage();
        m_LogManagerButton.SetActive(false);
    }

    public void HideNarrativeLog()
    {
        m_LogManager.SetActive(false);
        m_LogManagerButton.SetActive(true);
    }
    #endregion

    public void DialogueContinue()
    {
        switch (characterText)
        {
            case 0:
                if (PlayerText.text == s_PlayerFullText)
                {
                    b_iscontinuing = true;
                }
                else
                {
                    textlength = 0;
                    TextCooldown = 0f;
                    PlayerText.text = s_PlayerFullText;
                }
                break;
            case 1:
                if (OtherCharacterText.text == s_OtherCharacterFullText)
                {
                    b_iscontinuing = true;
                }
                else
                {
                    textlength = 0;
                    TextCooldown = 0f;
                    OtherCharacterText.text = s_OtherCharacterFullText;
                }
                break;

        }
    }

    public void GoToDemo()
    {
        Debug.Log("yes");
        m_demoImage.SetActive(true);
        SaveManager.Singleton.DeleteSaves();
    }

    public bool GetStoryStarted()
    {
        return b_StoryStarted;
    }
    
}
