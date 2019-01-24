using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Singleton { get; private set; }

    #region DIALOGUE_SYSTEM_DATA

    [Header("Dialogue System")]
    public GameObject DialogueSystem;

    #endregion

    public void Awake()
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

    #region DIALOGUE_SYSTEM

    public void SetupDialogueSystem()
    {
        DialogueSystem.SetActive(true);
        Story_Integrator.Singleton.OpenDialogue();
    }

    public void CloseDialogueSystem()
    {
        DialogueSystem.SetActive(false);
        Story_Integrator.Singleton.CloseDialogue();
    }

    #endregion
    
}
