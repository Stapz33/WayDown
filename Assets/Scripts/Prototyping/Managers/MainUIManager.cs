using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Singleton { get; private set; }

    #region DIALOGUE_SYSTEM_DATA

    [Header("Dialogue System")]
    public GameObject DialogueSystem;
    private Story_Integrator cs_ScriptIntegrator = new Story_Integrator();

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

        cs_ScriptIntegrator = GetComponent<Story_Integrator>();
    }

    #region DIALOGUE_SYSTEM

    public void SetupDialogueSystem()
    {
        DialogueSystem.SetActive(true);
        cs_ScriptIntegrator.OpenDialogue();
    }

    public void CloseDialogueSystem()
    {
        DialogueSystem.SetActive(false);
        cs_ScriptIntegrator.CloseDialogue();
    }

    #endregion

}
