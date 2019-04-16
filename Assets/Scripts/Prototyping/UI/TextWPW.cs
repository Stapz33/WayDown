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
                if (s_PlayerFullText != "")
                {
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
    }

    public void NameTextLaunch()
    {
        TextCooldown = TextSpeed;
        PlayerText.text = "";
        s_PlayerFullText = "";
        s_PlayerFullText = "And who is the winner ?";
        textlength = 0;
    }

    public void NormalTextLaunch()
    {
        TextCooldown = TextSpeed;
        PlayerText.text = "";
        s_PlayerFullText = "";
        s_PlayerFullText = "What are you lookin' for ?";
        textlength = 0;
    }

    public void FaceTextLaunch()
    {
        TextCooldown = TextSpeed;
        PlayerText.text = "";
        s_PlayerFullText = "";
        s_PlayerFullText = "Give me his face then";
        textlength = 0;
    }

    public void NoPeopleTextLaunch()
    {
        TextCooldown = TextSpeed;
        PlayerText.text = "";
        s_PlayerFullText = "";
        s_PlayerFullText = "Wait a second, no i have no one named like this in the records";
        textlength = 0;
    }

    public void NoCSTextLaunch()
    {
        TextCooldown = TextSpeed;
        PlayerText.text = "";
        s_PlayerFullText = "";
        s_PlayerFullText = "Wait a second, there are too many possible profiles";
        textlength = 0;
    }

    public void PeopleTextLaunch()
    {
        TextCooldown = TextSpeed;
        PlayerText.text = "";
        s_PlayerFullText = "";
        s_PlayerFullText = "Wait a second, so this is the guy your lookin' for huh ?";
        textlength = 0;
    }

    public void AlreadyPeopleTextLaunch()
    {
        TextCooldown = TextSpeed;
        PlayerText.text = "";
        s_PlayerFullText = "";
        s_PlayerFullText = "You already asked me for this record huh ?";
        textlength = 0;
    }
}
