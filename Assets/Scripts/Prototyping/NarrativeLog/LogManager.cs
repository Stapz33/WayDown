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
    [SerializeField] private Transform m_PanelParent = null;
    [SerializeField] private GameObject m_PanelToSpawn = null;
    [SerializeField] private GameObject PreviousButton = null;
    [SerializeField] private GameObject NextButton = null;
    public GameObject LogButtonFeedback;
    public Animator NewInfoFeedback;
    private List<GameObject> m_PanelList = new List<GameObject>();
    private List<string> m_LogsDataBase = new List<string>();
    private int m_NBofPanel = -1;
    private int m_ActualPanel = -1;
    private int m_ActualIdx = -1;
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
        m_PanelList.Add(Instantiate(m_PanelToSpawn, m_PanelParent));
        m_NBofPanel++;
        m_ActualPanel = m_NBofPanel;
        m_ActualIdx = -1;
    }

    public void AddLogFromCSV(int idx,bool feedback)
    {
        if (m_ActualIdx >= 10)
        {
            m_PanelList[m_ActualPanel].gameObject.SetActive(false);
            NewLogPanel();
            if (m_NBofPanel == 1)
            {
                PreviousButton.SetActive(true);
            }
        }
        m_ActualIdx++;
        m_PanelList[m_NBofPanel].transform.GetChild(m_ActualIdx).GetComponent<TextMeshProUGUI>().text = m_LogsDataBase[idx].Replace("\\n", "\n");
        idxtosave.SavedIdx.Add(idx);
        if (feedback)
        {
            AudioManager.Singleton.ActivateAudio(AudioType.NewLog);
            LogButtonFeedback.SetActive(true);
            NewInfoFeedback.SetTrigger("Info");
        }
    }


    public void NextLogPage()
    {
        if (m_ActualPanel < m_NBofPanel)
        {
            m_PanelList[m_ActualPanel].gameObject.SetActive(false);
            m_ActualPanel++;
            m_PanelList[m_ActualPanel].gameObject.SetActive(true);
            if (m_ActualPanel == m_NBofPanel)
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
        if (m_ActualPanel > 0)
        {
            m_PanelList[m_ActualPanel].gameObject.SetActive(false);
            m_ActualPanel--;
            m_PanelList[m_ActualPanel].gameObject.SetActive(true);
            if (m_ActualPanel == 0)
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
        foreach (int a in yes)
        {
            AddLogFromCSV(a,false);
        }
    }

    public void SaveLog()
    {
        SaveManager.Singleton.SaveLogPath(idxtosave);
    }

    public void OnApplicationQuit()
    {
        SaveLog();
    }

    public void ResetLogImage()
    {
        LogButtonFeedback.SetActive(false);
    }
}
