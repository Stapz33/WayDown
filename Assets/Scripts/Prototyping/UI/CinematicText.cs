using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RichTextSubstringHelper;

public class CinematicText : MonoBehaviour
{
    public float TextSpeed;

    public TextMeshProUGUI PlayerText;

    private string s_PlayerFullText = "";

    private string CurrentText;

    private int textlength = 0;


    private float TextCooldown = 0f;

    int i_TextFramingSound = 0;
    // Start is called before the first frame update
    void Awake()
    {
    }

    private void Start()
    {
        TextCooldown = TextSpeed;
        PlayerText.text = "";
        s_PlayerFullText = "";
        s_PlayerFullText = "<b>Somewhere in San Francisco</b> \n <size=35><i>April 13th 1953 </i></size>";
        textlength = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TextCooldown != 0)
        {
            TextCooldown -= Time.deltaTime;
            if (TextCooldown <= 0)
            {
                
                textlength++;
                if (s_PlayerFullText != "")
                {
                    CurrentText = s_PlayerFullText.RichTextSubString(textlength);
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
            }
        }
    }


}
