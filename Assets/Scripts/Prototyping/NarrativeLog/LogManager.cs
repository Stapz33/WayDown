using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[Serializable]
public class SaveLog
{
    public List<int> SavedIdx = new List<int>();
}

public class LogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextPanel = null;
    [SerializeField] private GameObject PreviousButton = null;
    [SerializeField] private GameObject NextButton = null;
    public GameObject LogButtonFeedback;
    public Animator NewInfoFeedback;
    private List<string> m_LogsDataBase = new List<string>();
    private int m_nbOfPages = 1;
    private int m_ActualPage = 1;
    private SaveLog idxtosave;
    

    private void Update()
    {
        
    }

    public void StartingScript()
    {
        GetComponent<CSVNarrativeLog>().Seta();
        m_LogsDataBase = GetComponent<CSVNarrativeLog>().GetStrings();
        idxtosave = new SaveLog();
        idxtosave.SavedIdx = new List<int>();
        NewLogPanel();
        SaveLog();
    }

    public void NewLogPanel()
    {
        m_ActualPage = m_nbOfPages;
    }

    public void AddLogFromCSV(int idx,bool feedback)
    {
        m_TextPanel.text = m_TextPanel.text + m_LogsDataBase[idx].Replace("\\n", "\n");
        idxtosave.SavedIdx.Add(idx);
        m_nbOfPages = m_TextPanel.textInfo.pageCount;
        m_TextPanel.pageToDisplay = m_nbOfPages;
        m_ActualPage = m_nbOfPages;
        Debug.Log("m_nbOfPages " + m_nbOfPages);
        Debug.Log("m_TextPanel " + m_TextPanel.textInfo.pageCount);
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
        if (feedback)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.NewLog);
            LogButtonFeedback.SetActive(true);
            NewInfoFeedback.SetTrigger("Info");
        }
    }


    public void NextLogPage()
    {
        if (m_ActualPage < m_nbOfPages)
        {
            m_ActualPage++;
            m_TextPanel.pageToDisplay = m_ActualPage;
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

    public void PreviousLogPage()
    {
        if (m_ActualPage > 1)
        {
            m_ActualPage--;
            m_TextPanel.pageToDisplay = m_ActualPage;
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

    public void LoadLog(SaveLog x)
    {
        StartingScript();
        List<int> yes = x.SavedIdx;
        for (int i = 0; i < yes.Count;i++)
        {
            AddLogFromCSV(yes[i], false);
        }
    }

    public void SaveLog()
    {
        SaveManager.Singleton.SaveLogPath(idxtosave);
    }

    private void OnApplicationQuit()
    {
        if (!MainUIManager.Singleton.GetStoryStarted())
        SaveLog();
    }

    public void ResetLogImage()
    {
        LogButtonFeedback.SetActive(false);
    }
}
