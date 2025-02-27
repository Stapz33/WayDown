﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using RichTextSubstringHelper;
using UnityEngine.Rendering.PostProcessing;

public enum TabType {Map, AddressBook, Documents}
public enum DocumentFolder {CriminalRecord, ProstituteMotel, CapoAppartment, Bar, DriverAppartment, ClientAppartment, Wreckyard, KillerAppartment, Drugstore, Restaurant, Dockers,Pier35}

public class MainUIManager : MonoBehaviour {

    public static MainUIManager Singleton { get; private set; }

    //Blur
    public PostProcessProfile PPP;
    int i_BlurState = -1;
    float f_blurCooldown = 0;
    public int m_BlurSpeed;
    int blurValue = 5;
  
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
    public Image OtherCharacterSprite = null;
    [SerializeField] private Image DialogueBackground = null;
    [SerializeField] private GameObject m_CharacterVinyle = null;
    [SerializeField] private GameObject m_OtherCharacterVinyle = null;
    public List<Sprite> PlayerDialogueSprites;
    public List<Sprite> OCDialogueSprites;
    //TransitionDialogueBG
    [SerializeField] private Animator m_TransitionScreenAnimator = null;
    private int m_DialogueBackgroundIdx = 0;
    public GameObject Dialogue;
    public GameObject FreemodeIndicator;

    //Dialogue Validation
    public GameObject ValidationDialogue;
    public Text ValidationDialogueText;
    public struct st_ValidationDialogue
    {
        public string s_BackValidation;
        public string s_GoodValidation;
        public string s_BadValidation;
        public string s_ValdationName;
    }
    public DialogueValidationScriptable ValidationScriptable;
    int i_GoodIdxValidation = 0;
    public Text ValidationText;



    // Variables/ speed of typewriter
    private bool ChoiceNeeded = false;
	private bool b_StoryStarted = false;
    private string s_PlayerFullText = "";
    private string s_OtherCharacterFullText = "";
    private bool isFirstStart = true;
    private bool b_isCheckingView = false;
    
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
        public List<string> AddressDiscovered = new List<string>();
        public bool b_IsInspectorDiscorvered = false;
        public bool b_IsCSDiscorvered = false;

        public bool lanzaDiscovered = false;
        public bool giovanniDiscovered = false;
        public bool abatiDiscovered = false;

        //variables
        public int knowledgeSpaghetty = 0;
        public int prostituteknown = 0;
        public int madam2 = 0;
        public int clientseen = 0;
        public int driverappseen = 0;
        public int drugstoreseen = 0;
        public int dockerseen = 0;
        public int drugstorefirsttime = 0;

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
    int i_actualGoodAddress = 0;
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

    public CRDatas crDatas;
    
    public GameObject Lanza;
    public GameObject Giovanni;
    public GameObject abati;
    int actualCR = -1;

    #endregion


    private bool b_isGoodAddress = false;
    private bool b_DisablePlayer = false;
    private string stockedAdress = "";

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

    bool b_isInIntrospection = false;
    bool b_isInValidation = false;
    int i_TextFramingSound = 0;
    private bool b_iscontinuing = false;
    public Sprite BackGroundDeskTransitition;
    public GameObject InvestigationTab;
    public GameObject CsTab;
    public GameObject Cigar;
    public GameObject cender;

    [Header("EndSection")]
    public GameObject LastSection;
    int i_actualProof = 0;
    public List<GameObject> proofsList;
    public Sprite DefaultProof;
    private List<bool> isGoodList = new List<bool>();
    private List<GameObject> ProofRegisteredList = new List<GameObject>();
    public GameObject EndGameValidationButton;
    bool b_isLanzaChoosen = false;
    bool b_isSecondTime = false;
    bool b_isInDocDiscover = false;
    public GameObject MapFeedback;

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
        for (int i = 0; i <3;i++)
        {
            isGoodList.Add(false);
        }
        
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
        for (int h = 0; h < DocumentsDrawerParent.childCount -1 ; h++)
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
        AudioManager.Singleton.ChangeMusic(4);
    }

    void Update () {

        if (Input.GetKeyDown("escape") && !m_demoImage.activeSelf)
        {
            ExitMenu.SetActive(true);
        }


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
                    if (!b_isInIntrospection)
                    {
                        LoadScreen(true);
                    }
                    else
                    {
                        CloseTab();
                        b_isInIntrospection = false;
                    }
                    CloseDialogue();
                    SaveGame();
                    AudioManager.Singleton.DeskCheckRadio();
                    AudioManager.Singleton.ChangeMusic(4);
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
                    choiceText.text = _inkStory.currentChoices[ii].text.Replace("<color=red>", "<color=#9F2B2B>");

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

            if (Input.GetButtonDown("Fire2"))
            {
                if (!b_isCheckingView)
                {
                    Dialogue.SetActive(false);
                    OtherCharacterSection.SetActive(false);
                    b_isCheckingView = true;
                    b_StoryStarted = false;
                    FreemodeIndicator.SetActive(true);
                }
                return;
            }


        }

        if (Input.GetButtonDown("Fire2") &&  b_isCheckingView)
        {
            Dialogue.SetActive(true);
            OtherCharacterSection.SetActive(true);
            b_StoryStarted = true;
            b_isCheckingView = false;
            FreemodeIndicator.SetActive(false);
        }

        if (Input.GetKeyDown("return") && b_isInValidation)
        {
            DialogueValidationTest();
        }

        if (i_BlurState != -1)
        {
            float value = PPP.GetSetting<DepthOfField>().focalLength.value;
            if (i_BlurState == 0)
            {
                value -= Time.deltaTime * m_BlurSpeed;
                if (value <= 1)
                {
                    value = 1;
                    i_BlurState = -1;
                }
                PPP.GetSetting<DepthOfField>().focalLength.value = value;
            }
            else if (i_BlurState == 1)
            {
                value += Time.deltaTime * m_BlurSpeed;
                if (value >= 100)
                {
                    value = 100;
                    f_blurCooldown = blurValue;
                    blurValue = 10;
                    i_BlurState = -1;
                }
                PPP.GetSetting<DepthOfField>().focalLength.value = value;
            }
        }

        if (f_blurCooldown > 0)
        {
            f_blurCooldown -= Time.deltaTime;
            if (f_blurCooldown <= 0)
            {
                i_BlurState = 0;
                f_blurCooldown = 0;
            }
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
        Address_data data = AddressesList[ActualStoryDataBase.AddressIndexToDiscover[i_actualGoodAddress]];
        data.gameObject.SetActive(true);
        MapFeedback.SetActive(true);
        SetNewActualAddressDocumentFolder(data.GetDocumentFolder());
    }

    public void UpdateDiscoveredAdress()
    {
        for (int j = 0; j < AddressesList.Count; j++)
        {
            AddressesList[j].gameObject.SetActive(ActualDatas.AddressState[j]);
        }
        if (ActualDatas.b_IsInspectorDiscorvered)
        {
            InvestigationTab.SetActive(true);
        }
        if (ActualDatas.b_IsCSDiscorvered)
        {
            CsTab.SetActive(true);
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
                    s_PlayerFullText = ContinueText.Replace("\n","").Replace("<color=red>", "<color=#9F2B2B>");
                    characterText = 0;
                    TextCooldown = TextSpeed;
                    PlayerText.text = "";
                }
                else if (_inkStory.currentTags[f] == "otherCharacter")
                {
                    ReactivateOtherCharacterSection();
                    DeactivatePlayerDialogue();
                    ReactivateOtherCharacterDialogue();
                    s_OtherCharacterFullText = ContinueText.Replace("\n", "").Replace("<color=red>", "<color=#9F2B2B>");
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
                    m_DialogueBackgroundIdx = int.Parse(_inkStory.currentTags[f + 1]);
                    SetTransitionDialogueBackground();
                }
                else if (_inkStory.currentTags[f] == "NewNoBackground")
                {
                    DialogueBackground.sprite = DialogueDataBase.Backgrounds[int.Parse(_inkStory.currentTags[f + 1])];
                }
                else if (_inkStory.currentTags[f] == "NewBigBackground")
                {
                    m_DialogueBackgroundIdx = int.Parse(_inkStory.currentTags[f + 1]);
                    SetTransitionDialogueBigBackground();
                }
                else if (_inkStory.currentTags[f] == "DisableDiscussion")
                {
                    DeactivateOtherCharacterSection();
                }
                else if (_inkStory.currentTags[f] == "DisablePlayer")
                {
                    b_DisablePlayer = true;
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
                else if (_inkStory.currentTags[f] == "MusicStop")
                {
                    AudioManager.Singleton.StopMusic();
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
                else if (_inkStory.currentTags[f] == "Validation")
                {
                    b_isInValidation = true;
                    b_StoryStarted = false;
                    i_GoodIdxValidation = int.Parse(_inkStory.currentTags[f + 1]);
                    Dialogue.SetActive(false);
                    ValidationDialogueText.text = "";
                    ValidationDialogue.SetActive(true);
                }
                else if (_inkStory.currentTags[f] == "Introspection")
                {
                    b_isInIntrospection = true;
                }
                else if (_inkStory.currentTags[f] == "InspectorNameUnlock")
                {
                    InvestigationTab.SetActive(true);
                    ActualDatas.b_IsInspectorDiscorvered = true;
                }
                else if (_inkStory.currentTags[f] == "CSUnlock")
                {
                    CsTab.SetActive(true);
                    ActualDatas.b_IsCSDiscorvered = true;
                }
                else if (_inkStory.currentTags[f] == "endgame")
                {
                    ActivateEndSection();
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
        blurValue = 5;
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
        blurValue = 5;
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
                MapFeedback.SetActive(false);
                ActualTab = MapTab;
                break;
            case TabType.AddressBook:
                AudioManager.Singleton.ActivateAudio(AudioType.AddressBookOpen);
                AddressBookTab.SetActive(true);
                ActualTab = AddressBookTab;
                AddressBookTab.GetComponent<AddressBookManager>().DisableTaxiButton();
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
        if (DocumentDataBase.Documents[DocumentIdx].GetComponent<ButtonCall>().BWText != null)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.polaroid);
        }
        else
        {
            AudioManager.Singleton.ActivateAudio(AudioType.NewDocument);
        }
        DocumentDatas.DocumentStruct yes = new DocumentDatas.DocumentStruct();
        yes.doc = DocumentIdx;
        yes.doctype = Type;
        DocDatas.doclist.Add(yes);
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
            l_DocumentsDrawer[DrawerIdx].GetComponent<DocumentFolderManager>().UnlockTab(0);
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
            DeactivateDialogueTemporary();
            b_isInDocDiscover = true;
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
        if (ActualStoryDataBase.AddressInfos01 != null)
        {
            if (AddressToTest == ActualStoryDataBase.AddressInfos01)
            {
                b_isGoodAddress = true;
                i_actualGoodAddress = 0;
            }
            else if (AddressToTest == ActualStoryDataBase.AddressInfos02 && ActualStoryDataBase.AddressInfos02 != "")
            {
                b_isGoodAddress = true;
                i_actualGoodAddress = 1;
            }
            else
            {
                b_isGoodAddress = false;
                i_actualGoodAddress = -1;
            }
            stockedAdress = AddressToTest;
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
            SetupNewStory(AddressesList[ActualStoryDataBase.AddressIndexToDiscover[i_actualGoodAddress]].GetActualStory());
            b_isGoodAddress = false;
            DiscoverNewAddress();
            ActualDatas.AddressDiscovered.Add(stockedAdress);
        }
        else
        {
            // BUG
            for (int i = 0; i < ActualDatas.AddressDiscovered.Count; i++)
            {
                if (stockedAdress == ActualDatas.AddressDiscovered[i])
                {
                    SetupNewStory(AddressesList[i].GetActualStory());
                    SetupDialogueSystem();
                    AudioManager.Singleton.StopRadio();
                    AddressBookTab.GetComponent<AddressBookManager>().DisableTaxiButton();
                    cender.SetActive(false);
                    Cigar.SetActive(true);
                    return;
                }
                else if (i == ActualDatas.AddressDiscovered.Count -1)
                {
                    SetupNewStory(DefaultKnot);
                    DialogueBackground.sprite = DialogueDataBase.Backgrounds[4];
                }
            }
        }
        SetupDialogueSystem();
        AudioManager.Singleton.StopRadio();
        AudioManager.Singleton.StopMusic();
        AddressBookTab.GetComponent<AddressBookManager>().DisableTaxiButton();
        cender.SetActive(false);
        Cigar.SetActive(true);
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
        AudioManager.Singleton.StopMusic();
        AudioManager.Singleton.StopRadio();
        PoliceOfficeObject.SetActive(true);
        PoliceOffice.Singleton.UpdateNormalInspector();
    }

    public void ClosePoliceOffice()
    {
        AudioManager.Singleton.ChangeMusic(4);
        AudioManager.Singleton.DeskCheckRadio();
        PoliceOfficeObject.SetActive(false);
        CloseTab();
    }

    public void NameValidation(string name)
    {
        string GoodName = GetActualName();
        if (GoodName != "")
        {
            if (name.ToLower().Replace("'","").Replace(" ", string.Empty) == GoodName && !ActualDatas.m_IsCriminalKnown)
            {
                PoliceOffice.Singleton.GoodName();
                Invoke("AddNewCriminalRecord", 2f);
                PoliceOfficeObject.GetComponent<PoliceOffice>().GoodNameInput();
            }
            else if (name == GoodName && ActualDatas.m_IsCriminalKnown)
            {
                PoliceOffice.Singleton.AlreadyGood();
            }
            else 
            {
                foreach (CRDatas.st_CR st_CR in crDatas.CRData)
                {
                    if (st_CR.goodName == name.ToLower().Replace("'", "").Replace(" ", string.Empty))
                    {

                        actualCR = st_CR.CRIdx;

                        switch (actualCR)
                        {
                            case 13:
                                if (ActualDatas.lanzaDiscovered)
                                {
                                    PoliceOffice.Singleton.AlreadyGood();
                                    return;
                                }
                                ActualDatas.lanzaDiscovered = true;
                                break;
                            case 22:
                                if (ActualDatas.giovanniDiscovered)
                                {
                                    PoliceOffice.Singleton.AlreadyGood();
                                    return;
                                }
                                ActualDatas.giovanniDiscovered = true;
                                break;
                            case 23:
                                if (ActualDatas.abatiDiscovered)
                                {
                                    PoliceOffice.Singleton.AlreadyGood();
                                    return;
                                }
                                ActualDatas.abatiDiscovered = true;
                                break;
                            default:
                                break;
                        }
                        PoliceOffice.Singleton.GoodName();
                        Invoke("AddNewSCriminalRecord", 2f);
                        PoliceOfficeObject.GetComponent<PoliceOffice>().GoodNameInput();
                        return;
                    }
                }
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

    public void AddNewSCriminalRecord()
    {
        AddNewDocumentAndShowIt(actualCR, DocumentFolder.CriminalRecord, true);
        SaveGame();
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
                PoliceOfficeObject.GetComponent<PoliceOffice>().GoodCSInput();
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
        SaveManager.Singleton.SaveDocumentPath(DocDatas);
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
        ActualDatas.madam2 = (int)_inkStory.variablesState["madam2"];
        ActualDatas.clientseen = (int)_inkStory.variablesState["client_seen"];
        ActualDatas.driverappseen = (int)_inkStory.variablesState["driverapp_seen"];
        ActualDatas.drugstoreseen = (int)_inkStory.variablesState["drugstore_seen"];
        ActualDatas.dockerseen = (int)_inkStory.variablesState["docker_seen"];
        ActualDatas.drugstorefirsttime = (int)_inkStory.variablesState["drugstore_firsttime"]; 
    }

    public void LoadStoryVar()
    {
        _inkStory.variablesState["knowledge_Spaghetti"] = ActualDatas.knowledgeSpaghetty;
        _inkStory.variablesState["knowledge_prostitute_name"] = ActualDatas.prostituteknown;
        _inkStory.variablesState["madam2"] = ActualDatas.madam2;
        _inkStory.variablesState["client_seen"] = ActualDatas.clientseen;
        _inkStory.variablesState["driverapp_seen"] = ActualDatas.driverappseen;
        _inkStory.variablesState["drugstore_seen"] = ActualDatas.drugstoreseen;
        _inkStory.variablesState["docker_seen"] = ActualDatas.dockerseen;
        _inkStory.variablesState["drugstore_firsttime"] = ActualDatas.drugstorefirsttime;
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
        a_LoadingScreenAnimator.SetTrigger("LoadBlack");
        Invoke("async", 1.2f);
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
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

        if (!b_isInDocDiscover)
        {
            DeactivateDialogueTemporary();
        }
    }

    public void HideNarrativeLog()
    {
        m_LogManager.SetActive(false);
        m_LogManagerButton.SetActive(true);

        if (!b_isInDocDiscover)
        {
            ReactivateDialogue();
        }
        
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
        m_demoImage.SetActive(true);
        AudioManager.Singleton.StopMusic();
    }

    public bool GetStoryStarted()
    {
        return b_StoryStarted;
    }

    public void SetTransitionDialogueBackground()
    {
        Dialogue.SetActive(false);
        DeactivateOtherCharacterSection();
        DeactivatePlayerDialogue();
        m_TransitionScreenAnimator.SetTrigger("Loading");
        Invoke("SetBGDialogue", 0.3f);
        b_StoryStarted = false;
    }

    public void SetTransitionDialogueBigBackground()
    {
        Dialogue.SetActive(false);
        DeactivateOtherCharacterSection();
        DeactivatePlayerDialogue();
        a_LoadingScreenAnimator.SetTrigger("Loading");
        Invoke("SetBGBigDialogue", 0.3f);
        b_StoryStarted = false;
    }

    public void SetBGDialogue()
    {
        if (m_DialogueBackgroundIdx == 9)
        {
            m_TransitionScreenAnimator.transform.GetChild(0).GetComponent<Image>().sprite = BackGroundDeskTransitition;
            AudioManager.Singleton.ChangeMusic(4);
        }
        else
        {
            m_TransitionScreenAnimator.transform.GetChild(0).GetComponent<Image>().sprite = DialogueDataBase.Backgrounds[m_DialogueBackgroundIdx];
        }
        DialogueBackground.sprite = DialogueDataBase.Backgrounds[m_DialogueBackgroundIdx];
        Invoke("ResetBG", 3f);
    }
    public void SetBGBigDialogue()
    {
        if (m_DialogueBackgroundIdx == 9)
        {
            m_TransitionScreenAnimator.transform.GetChild(0).GetComponent<Image>().sprite = BackGroundDeskTransitition;
        }
        else
        {
            m_TransitionScreenAnimator.transform.GetChild(0).GetComponent<Image>().sprite = DialogueDataBase.Backgrounds[m_DialogueBackgroundIdx];
        }
        DialogueBackground.sprite = DialogueDataBase.Backgrounds[m_DialogueBackgroundIdx];
        Invoke("ResetBG", 3.5f);
    }
    public void ResetBG()
    {
        Dialogue.SetActive(true);
        ReactivateOtherCharacterSection();
        b_StoryStarted = true;
        if (b_DisablePlayer)
        {
            b_DisablePlayer = false;
            return;
        }
        ReactivatePlayerDialogue();
    }


    public void DialogueValidationTest()
    {
            string GoodName = ValidationScriptable.ValidationDatas[i_GoodIdxValidation].ValdationName;
            string TestName = ValidationText.text;
            if (GoodName != "")
            {
                if (TestName.ToLower().Replace("'", "").Replace(" ", string.Empty) == GoodName)
                {
                    DialogueValidationSetup(2);
                }
                else
                {
                    DialogueValidationSetup(1);
                }
            }
            else
            {
                DialogueValidationSetup(1);
            }
            b_isInValidation = false;
    }

    public void DialogueValidationSetup(int ValIdx)
    {
        ValidationDialogue.SetActive(false);
        Dialogue.SetActive(true);
        b_StoryStarted = true;
        switch (ValIdx)
        {
            case 0:
                _inkStory.ChoosePathString(ValidationScriptable.ValidationDatas[i_GoodIdxValidation].BackValidation);
                break;
            case 1:
                _inkStory.ChoosePathString(ValidationScriptable.ValidationDatas[i_GoodIdxValidation].BadValidation);
                break;
            case 2:
                _inkStory.ChoosePathString(ValidationScriptable.ValidationDatas[i_GoodIdxValidation].GoodValidation);
                break;
            default:
                break;
        }
        
    }

    public void AddBlurToScreen()
    {
        i_BlurState = 1;
    }

    public void SelectProof(GameObject proof,bool isGood)
    {
        if (i_actualProof <= 2)
        {
            ProofRegisteredList.Add(proof);
            isGoodList[i_actualProof] = isGood;
            proofsList[i_actualProof].GetComponent<Image>().sprite = proof.GetComponent<Image>().sprite;
            proofsList[i_actualProof].GetComponent<Button>().interactable = true;
            proof.SetActive(false);
            switch (i_actualProof)
            {
                case 0:
                    i_actualProof++;
                    break;
                case 1:
                    proofsList[0].GetComponent<Button>().interactable = false;
                    i_actualProof++;
                    break;
                case 2:
                    proofsList[1].GetComponent<Button>().interactable = false;
                    EndGameValidationButton.SetActive(true);
                    i_actualProof++;
                    break;
            }
        }
        
    }

    public void RemoveProof()
    {
        i_actualProof--;
        EndGameValidationButton.SetActive(false);
        ProofRegisteredList[i_actualProof].SetActive(true);
        ProofRegisteredList.Remove(ProofRegisteredList[i_actualProof]);
        isGoodList[i_actualProof] = false;
        proofsList[i_actualProof].GetComponent<Image>().sprite = DefaultProof;
        proofsList[i_actualProof].GetComponent<Button>().interactable = false;
        if (i_actualProof != 0)
        {
            proofsList[i_actualProof - 1].GetComponent<Button>().interactable = true;
        }
    }

    public void EndGameValidation()
    {
        LastSection.SetActive(false);
        if (b_isLanzaChoosen)
        {
            SetupNewStory("ending.lanza_ending");
            SetupDialogueSystem();
            return;
        }
        int goodChooses = 0;
        foreach (bool proof in isGoodList)
        {
            if (proof)
            {
                goodChooses++;
            }
        }
        if (goodChooses == 3)
        {
            SetupNewStory("ending.good_ending");
            SetupDialogueSystem();
        }
        else if (b_isSecondTime)
        {
            SetupNewStory("ending.bad_ending");
            SetupDialogueSystem();
        }
        else
        {
            SetupNewStory("ending.badchoice_proofs");
            SetupDialogueSystem();
            b_isSecondTime = true;
        }
    }

    public void ActivateEndSection()
    {
        b_StoryStarted = false;
        CloseTab();
        CloseDialogue();
        m_CharacterVinyle.SetActive(false);
        m_OtherCharacterVinyle.SetActive(false);
        LastSection.SetActive(true);
        if (ActualDatas.lanzaDiscovered)
        {
            Lanza.SetActive(true);
        }
        if (ActualDatas.giovanniDiscovered)
        {
            Giovanni.SetActive(true);
        }
        if (ActualDatas.abatiDiscovered)
        {
            abati.SetActive(true);
        }
    }

    public void SetLanzaChoosed(bool b)
    {
            b_isLanzaChoosen = b;
    }

    public void DeactivateDialogueTemporary()
    {
        Dialogue.SetActive(false);
        OtherCharacterSection.SetActive(false);
        b_StoryStarted = false;
    }

    public void ReactivateDialogue()
    {
        if (b_isInDocDiscover)
        {
            b_isInDocDiscover = false;
        }
        if (b_isCheckingView)
        {
            return;
        }
        
        Dialogue.SetActive(true);
        OtherCharacterSection.SetActive(true);
        b_StoryStarted = true;
    }

    public void ActivateCender()
    {
        Cigar.SetActive(false);
        cender.SetActive(true);
    }

    public void OpenSettings()
    {
        SettingsManager.Singleton.OpenSettings();
    }
}
