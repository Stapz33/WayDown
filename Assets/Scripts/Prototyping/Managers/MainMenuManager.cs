using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private Animator m_LoadingScreen = null;
    [SerializeField] private GameObject m_ContinueButton = null;

    [SerializeField] private GameObject m_NewGameMenu = null;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveManager.Singleton.CheckSaveExist())
        {
            m_ContinueButton.SetActive(true);
        }
        AudioManager.Singleton.ChangeMusic(0);
    }

    public void NewGame()
    {
        if (SaveManager.Singleton.CheckSaveExist())
        {
            m_NewGameMenu.SetActive(true);
        }
        else
        {
            LaunchGame();
        }
    }

    public void LaunchGame()
    {
        AudioManager.Singleton.ActivateAudio(AudioType.LoadingTransition);
        m_LoadingScreen.SetTrigger("LoadBlack");
        Invoke("async", 1.2f);
    }

    public void ConfirmNewGame()
    {
        SaveManager.Singleton.DeleteSaves();
        m_NewGameMenu.SetActive(false);
        LaunchGame();
    }

    public void RefuseNewGame()
    {
        m_NewGameMenu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void async()
    {
        StartCoroutine("LoadSceneAsync");
    }

    IEnumerator LoadSceneAsync()
    {
        AudioManager.Singleton.StopMusic();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
