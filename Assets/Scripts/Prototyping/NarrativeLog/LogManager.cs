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
    [SerializeField] private GameObject m_NPanel = null;
    [SerializeField] private GameObject m_CPanel = null;
    [SerializeField] private GameObject PreviousButton = null;
    [SerializeField] private GameObject NextButton = null;
    public GameObject NlogButton;
    public GameObject ClogButton;
    public GameObject ClogFeedback;
    public GameObject LogButtonFeedback;
    public Animator NewInfoFeedback;
    private List<string> m_NLogsBase = new List<string>();
    private List<string> m_CLogsBase = new List<string>();
    private int m_nbOfPages = 1;
    private int m_ActualPage = 1;
    private int m_CPages = 1;
    private int m_ActualCPage = 1;
    private SaveLog idxtosave = new SaveLog();
    private TextMeshProUGUI NText01;
    private TextMeshProUGUI NText02;

    private TextMeshProUGUI CText01;
    private TextMeshProUGUI CText02;

    private bool needReacFB = false;

    bool needtoUpdate = false;

    private void Awake()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        //gameObject.SetActive(false);
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
        NText01 = m_NPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        NText02 = m_NPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        CText01 = m_CPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        CText02 = m_CPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        NewLogPanel();
        SaveLog();
    }

    public void NewLogPanel()
    {
        m_ActualPage = m_nbOfPages;
    }

    public void AddNLog(int idx, bool feedback)
    {
        NText01.text = NText01.text + m_NLogsBase[idx].Replace("\\n", "\n").Replace("<color=red>", "<color=#9F2B2B>");
        NText02.text = NText01.text;
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
        CText01.text = CText01.text + m_CLogsBase[idx].Replace("\\n", "\n").Replace("<color=red>", "<color=#9F2B2B>");
        CText02.text = CText01.text;
        idxtosave.Cidx.Add(idx);
        if (feedback)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.NewLog);
            LogButtonFeedback.SetActive(true);
            ClogFeedback.SetActive(true);
            NewInfoFeedback.SetTrigger("Info");
        }
        needtoUpdate = true;
    }

    public void LogUpdate()
    {
        if (NText01.textInfo.pageCount == m_nbOfPages + 2)
        {
            Debug.Log("1");
            NText02.pageToDisplay = NText01.textInfo.pageCount + 1;
            NText01.pageToDisplay = NText01.textInfo.pageCount;
            m_nbOfPages = NText01.textInfo.pageCount;
        }
        else if (NText01.textInfo.pageCount == m_nbOfPages + 1)
        {
            Debug.Log("2");
            NText02.pageToDisplay = NText01.textInfo.pageCount;
            NText01.pageToDisplay = NText01.textInfo.pageCount - 1;
        }
        else if (NText01.textInfo.pageCount == 0)
        {
            Debug.Log("3");
            NText02.pageToDisplay = 2;
            NText01.pageToDisplay = 1;
            m_nbOfPages = 1;
        }
        else
        {
            Debug.Log("4");
            NText02.pageToDisplay = NText01.textInfo.pageCount + 1;
            NText01.pageToDisplay = NText01.textInfo.pageCount;
            m_nbOfPages = NText01.textInfo.pageCount;
        }
        m_ActualPage = m_nbOfPages;
        if (m_ActualPage > 2)
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
        if (m_NPanel.gameObject.activeSelf)
        {
            if (m_ActualPage < m_nbOfPages)
            {
                m_ActualPage += 2;
                NText01.pageToDisplay = m_ActualPage;
                NText02.pageToDisplay = m_ActualPage + 1;
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
                m_ActualCPage += 2;
                CText01.pageToDisplay = m_ActualPage;
                CText02.pageToDisplay = m_ActualPage + 1;
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
        if (m_NPanel.gameObject.activeSelf)
        {
            if (m_ActualPage > 1)
            {
                m_ActualPage -= 2;
                NText01.pageToDisplay = m_ActualPage;
                NText02.pageToDisplay = m_ActualPage + 1;
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
                m_ActualCPage -=2;
                CText01.pageToDisplay = m_ActualCPage;
                CText02.pageToDisplay = m_ActualCPage + 1;
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
        m_CPanel.gameObject.SetActive(false);
        m_NPanel.gameObject.SetActive(true);
        needtoUpdate = true;
        //LogUpdate();
    }

    public void SetCLog()
    {
        ClogFeedback.SetActive(false);
        m_NPanel.gameObject.SetActive(false);
        m_CPanel.gameObject.SetActive(true);
        if (CText01.textInfo.pageCount == m_CPages + 2)
        {
            CText01.pageToDisplay = CText01.textInfo.pageCount;
            CText01.pageToDisplay = CText01.textInfo.pageCount + 1;
            m_CPages = CText01.textInfo.pageCount;
        }
        NextButton.SetActive(false);
        m_ActualCPage = m_CPages;
        if (CText01.textInfo.pageCount > 2)
        {
            PreviousButton.SetActive(true);
        }
        else
        {
            PreviousButton.SetActive(false);
        }
    }


}
