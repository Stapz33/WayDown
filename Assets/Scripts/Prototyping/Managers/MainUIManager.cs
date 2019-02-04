using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour {

    public static MainUIManager Singleton { get; private set; }

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
    [Header("Variables")]
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
    public StoryDataBase StartingStories;
    public string DefaultKnot;
    private List<Address_data> AddressesList = new List<Address_data>();

    #endregion
    


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

        Transform AddressParent = BoardUIManager.Singleton.GetAddressList();
        for (int y = 0; y < AddressParent.childCount; y++)
        {
            AddressesList.Add(AddressParent.GetChild(y).GetComponent<Address_data>());
        }
        UpdateAddressesStory(StartingStories.StoryData);
        _inkStory = new Story(Story.text);
    }


    // Update is called once per frame
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
                    CloseDialogueSystem();
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
}
