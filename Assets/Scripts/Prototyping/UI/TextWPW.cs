using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWPW : MonoBehaviour
{
    public float wordsPerSecond = 10f;

    public Text PlayerText;

    private string s_PlayerFullText = "What are you lookin' for ?";

    private float PlayerTimeElapsed = 0f;

    int i_TextFramingSound = 0;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTimeElapsed += Time.deltaTime;
        PlayerText.text = GetWords(s_PlayerFullText, (int)(PlayerTimeElapsed * wordsPerSecond));
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
                if (i_TextFramingSound == 8)
                {
                    AudioManager.Singleton.ActivateAudio(AudioType.Text);
                    int rnd = UnityEngine.Random.Range(-3, 0);
                    i_TextFramingSound = rnd;
                }
                else i_TextFramingSound++;
                return text.Substring(0, i);
            }
        }
        return text;
    }

    public void NameTextLaunch()
    {
        PlayerTimeElapsed = 0f;
        PlayerText.text = "";
        s_PlayerFullText = "And who is the winner ?";
    }

    public void NormalTextLaunch()
    {
        PlayerTimeElapsed = 0f;
        PlayerText.text = "";
        s_PlayerFullText = "What are you lookin' for ?";
    }

    public void FaceTextLaunch()
    {
        PlayerTimeElapsed = 0f;
        PlayerText.text = "";
        s_PlayerFullText = "Give me his face then";
    }

    public void NoPeopleTextLaunch()
    {
        PlayerTimeElapsed = 0f;
        PlayerText.text = "";
        s_PlayerFullText = "Wait a second, no i have no one named like this in the records";
    }

    public void NoCSTextLaunch()
    {
        PlayerTimeElapsed = 0f;
        PlayerText.text = "";
        s_PlayerFullText = "Wait a second, there are too many possible profiles";
    }

    public void PeopleTextLaunch()
    {
        PlayerTimeElapsed = 0f;
        PlayerText.text = "";
        s_PlayerFullText = "Wait a second, so this is the guy your lookin' for huh ?";
    }

    public void AlreadyPeopleTextLaunch()
    {
        PlayerTimeElapsed = 0f;
        PlayerText.text = "";
        s_PlayerFullText = "You already asked me for this record huh ?";
    }
}
