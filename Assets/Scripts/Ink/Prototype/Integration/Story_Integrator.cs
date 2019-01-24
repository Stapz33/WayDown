using UnityEngine;
using System.Collections;
using Ink.Runtime;
using UnityEngine.UI;

public class Story_Integrator : MonoBehaviour {

    #region DIALOGUE_SYSTEM_SETUP_DATA

    // Prefabs & Assets
    [SerializeField] private Transform ChoiceSection;
    [SerializeField] private Text PlayerText;
    [SerializeField] private Text OtherCharacterText;
    [SerializeField] private Button ChoiceButton;

    // Story
    [SerializeField] private TextAsset inkAsset;
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
                    MainUIManager.Singleton.CloseDialogueSystem();
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
        _inkStory = new Story(inkAsset.text);
        PlayerText.text = "";
        OtherCharacterText.text = "";
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
                Debug.Log("player");
                s_PlayerFullText = ContinueText;
                PlayerText.text = "";
                PlayerTimeElapsed = 0f;
            }
            else if (_inkStory.currentTags[0] == "otherCharacter")
            {
                Debug.Log("OtherCharacter");
                s_OtherCharacterFullText = ContinueText;
                OtherCharacterText.text = "";
                OtherPlayerTimeElapsed = 0f;
            }
        }
    }
}
