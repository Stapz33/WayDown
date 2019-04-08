using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWPW : MonoBehaviour
{
    public float TextSpeed;

    public Text PlayerText;

    private string s_PlayerFullText = "What are you lookin' for ?";

    private string CurrentText;

    private int textlength = 0;


    private float TextCooldown = 0f;

    int i_TextFramingSound = 0;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (TextCooldown != 0)
        {
            TextCooldown -= Time.deltaTime;
            if (TextCooldown <= 0)
            {
                if (i_TextFramingSound == 1)
                {
                    AudioManager.Singleton.ActivateAudio(AudioType.Text);
                    i_TextFramingSound = 0;
                }
                else i_TextFramingSound++;
                textlength++;
                CurrentText = s_PlayerFullText.Substring(0, textlength);
                PlayerText.text = CurrentText;
                if (CurrentText == s_PlayerFullText)
                {
                    textlength = 0;
                    TextCooldown = 0f;
                }
                else
                {
                    TextCooldown = TextSpeed;
                }
            }
        }
    }

    public void NameTextLaunch()
    {
        PlayerText.text = "";
        s_PlayerFullText = "And who is the winner ?";
        TextCooldown = TextSpeed;
    }

    public void NormalTextLaunch()
    {
        PlayerText.text = "";
        s_PlayerFullText = "What are you lookin' for ?";
        TextCooldown = TextSpeed;
    }

    public void FaceTextLaunch()
    {
        PlayerText.text = "";
        s_PlayerFullText = "Give me his face then";
        TextCooldown = TextSpeed;
    }

    public void NoPeopleTextLaunch()
    {
        PlayerText.text = "";
        s_PlayerFullText = "Wait a second, no i have no one named like this in the records";
        TextCooldown = TextSpeed;
    }

    public void NoCSTextLaunch()
    {
        PlayerText.text = "";
        s_PlayerFullText = "Wait a second, there are too many possible profiles";
        TextCooldown = TextSpeed;
    }

    public void PeopleTextLaunch()
    {
        PlayerText.text = "";
        s_PlayerFullText = "Wait a second, so this is the guy your lookin' for huh ?";
        TextCooldown = TextSpeed;
    }

    public void AlreadyPeopleTextLaunch()
    {
        PlayerText.text = "";
        s_PlayerFullText = "You already asked me for this record huh ?";
        TextCooldown = TextSpeed;
    }
}
