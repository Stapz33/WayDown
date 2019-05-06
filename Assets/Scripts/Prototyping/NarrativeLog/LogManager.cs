using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveLog
{
    public List<int> Nidx = new List<int>();
    public List<int> Cidx = new List<int>();
}

public class LogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_NTextPanel = null;
    [SerializeField] private TextMeshProUGUI m_CTextPanel = null;
    [SerializeField] private GameObject PreviousButton = null;
    [SerializeField] private GameObject NextButton = null;
    public GameObject NlogButton;
    public GameObject ClogButton;
    public GameObject LogButtonFeedback;
    public Animator NewInfoFeedback;
    private List<string> m_NLogsBase = new List<string>();
    private List<string> m_CLogsBase = new List<string>();
    private int m_nbOfPages = 1;
    private int m_ActualPage = 1;
    private int m_CPages = 1;
    private int m_ActualCPage = 1;
    private SaveLog idxtosave;

    bool needtoUpdate = false;

    private void Awake()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void Update()
    {
        if (needtoUpdate)
        {
            LogUpdate();
            needtoUpdate = false;
        }
    }

    public void StartingScript()
    {
        GetComponent<CSVNarrativeLog>().Seta();
        m_NLogsBase = GetComponent<CSVNarrativeLog>().GetStrings();
        m_CLogsBase = GetComponent<CSVNarrativeLog>().GetClogs();
        idxtosave = new SaveLog();
        idxtosave.Nidx = new List<int>();
        idxtosave.Cidx = new List<int>();
        NewLogPanel();
        SaveLog();
    }

    public void NewLogPanel()
    {
        m_ActualPage = m_nbOfPages;
    }

    public void AddNLog(int idx, bool feedback)
    {
        m_NTextPanel.text = m_NTextPanel.text + m_NLogsBase[idx].Replace("\\n", "\n");
        idxtosave.Nidx.Add(idx);
        if (feedback)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.NewLog);
            LogButtonFeedback.SetActive(true);
            NewInfoFeedback.SetTrigger("Info");
        }
        needtoUpdate = true;
    }

    public void AddCLog(int idx, bool feedback)
    {
        m_CTextPanel.text = m_CTextPanel.text + m_CLogsBase[idx].Replace("\\n", "\n");
        idxtosave.Cidx.Add(idx);
        if (feedback)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.NewLog);
            LogButtonFeedback.SetActive(true);
            NewInfoFeedback.SetTrigger("Info");
        }
        needtoUpdate = true;
    }

    public void LogUpdate()
    {
        m_nbOfPages = m_NTextPanel.textInfo.pageCount;
        m_NTextPanel.pageToDisplay = m_NTextPanel.textInfo.pageCount;
        m_ActualPage = m_nbOfPages;
        if (m_ActualPage > 1)
        {
            PreviousButton.SetActive(true);
        }
        if (m_ActualPage < m_nbOfPages)
        {
            NextButton.SetActive(true);
        }
        else if (m_ActualPage == m_nbOfPages)
        {
            NextButton.SetActive(false);
        }
    }


    public void NextLogPage()
    {
        if (m_NTextPanel.gameObject.activeSelf)
        {
            if (m_ActualPage < m_nbOfPages)
            {
                m_ActualPage++;
                m_NTextPanel.pageToDisplay = m_ActualPage;
                if (m_ActualPage == m_nbOfPages)
                {
                    NextButton.SetActive(false);
                }
                if (!PreviousButton.activeSelf)
                {
                    PreviousButton.SetActive(true);
                }
            }
        }
        else
        {
            if (m_ActualCPage < m_CPages)
            {
                m_ActualCPage++;
                m_CTextPanel.pageToDisplay = m_ActualCPage;
                if (m_ActualCPage == m_CPages)
                {
                    NextButton.SetActive(false);
                }
                if (!PreviousButton.activeSelf)
                {
                    PreviousButton.SetActive(true);
                }
            }
        }
    }

    public void PreviousLogPage()
    {
        if (m_NTextPanel.gameObject.activeSelf)
        {
            if (m_ActualPage > 1)
            {
                m_ActualPage--;
                m_NTextPanel.pageToDisplay = m_ActualPage;
                if (m_ActualPage == 1)
                {
                    PreviousButton.SetActive(false);
                }
                if (!NextButton.activeSelf)
                {
                    NextButton.SetActive(true);
                }
            }
        }
        else
        {
            if (m_ActualCPage > 1)
            {
                m_ActualCPage--;
                m_CTextPanel.pageToDisplay = m_ActualCPage;
                if (m_ActualCPage == 1)
                {
                    PreviousButton.SetActive(false);
                }
                if (!NextButton.activeSelf)
                {
                    NextButton.SetActive(true);
                }
            }
        }
        
    }

    public void LoadLog(SaveLog x)
    {
        StartingScript();
        List<int> a = x.Nidx;
        for (int i = 0; i < a.Count; i++)
        {
            AddNLog(a[i], false);
        }
        List<int> b = x.Cidx;
        for (int i = 0; i < b.Count; i++)
        {
            AddCLog(b[i], false);
        }
        SaveLog();
    }

    public void SaveLog()
    {
        SaveManager.Singleton.SaveLogPath(idxtosave);
    }

    public void ResetLogImage()
    {
        LogButtonFeedback.SetActive(false);
        SetNLog();
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (!MainUIManager.Singleton.GetStoryStarted())
            SaveLog();
    }

    public void SetNLog()
    {
        NlogButton.SetActive(false);
        ClogButton.SetActive(true);
        m_CTextPanel.gameObject.SetActive(false);
        m_NTextPanel.gameObject.SetActive(true);
        LogUpdate();
    }

    public void SetCLog()
    {
        ClogButton.SetActive(false);
        NlogButton.SetActive(true);
        m_NTextPanel.gameObject.SetActive(false);
        m_CTextPanel.gameObject.SetActive(true);
        m_CTextPanel.pageToDisplay = m_CTextPanel.textInfo.pageCount;
        NextButton.SetActive(false);
        m_CPages = m_CTextPanel.textInfo.pageCount;
        m_ActualCPage = m_CPages;
        if (m_CTextPanel.textInfo.pageCount > 1)
        {
            PreviousButton.SetActive(true);
        }
    }
}
