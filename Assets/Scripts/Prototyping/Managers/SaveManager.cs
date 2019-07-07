using System;
using System.IO;
using UnityEngine;
using static MainUIManager;
using static SettingsManager;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Singleton { get; private set; }

    string storypath;
    string docpath;
    string logpath;
    string settingspath;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(gameObject);
        else
            Singleton = this;
        DontDestroyOnLoad(gameObject);
        SetupPaths();
    }

    public void SetupPaths()
    {
        storypath = Path.Combine(Application.persistentDataPath, "Story.txt");
        docpath = Path.Combine(Application.persistentDataPath, "Document.txt");
        logpath = Path.Combine(Application.persistentDataPath, "NarrativeLog.txt");
        settingspath = Path.Combine(Application.persistentDataPath, "Settings.txt");
    }

    public void SaveStoryPath(Data idx)
    {
        string jsonString = JsonUtility.ToJson(idx);

        using (StreamWriter streamWriter = File.CreateText(storypath))
        {
            streamWriter.Write(jsonString);
        }
    }

    public void SaveDocumentPath(DocumentDatas idx)
    {
        string jsonString = JsonUtility.ToJson(idx);

        using (StreamWriter streamWriter = File.CreateText(docpath))
        {
            streamWriter.Write(jsonString);
        }
    }

    public void SaveLogPath(SaveLog idx)
    {
        string jsonString = JsonUtility.ToJson(idx);

        using (StreamWriter streamWriter = File.CreateText(logpath))
        {
            streamWriter.Write(jsonString);
        }
    }

    public void SaveSettingPath(SSettings idx)
    {
        string jsonString = JsonUtility.ToJson(idx);

        using (StreamWriter streamWriter = File.CreateText(settingspath))
        {
            streamWriter.Write(jsonString);
        }
    }

    public Data LoadStoryPath()
    {
        if (File.Exists(storypath))
        {
            using (StreamReader streamReader = File.OpenText(storypath))
            {
                string jsonString = streamReader.ReadToEnd();
                return JsonUtility.FromJson<Data>(jsonString);
            }
        }
        return null;
        
    }

    public DocumentDatas LoadDocumentPath()
    {
        if (File.Exists(docpath))
        {
            using (StreamReader streamReader = File.OpenText(docpath))
            {
                string jsonString = streamReader.ReadToEnd();
                return JsonUtility.FromJson<DocumentDatas>(jsonString);
            }
        }
        return null;

    }

    public SaveLog LoadLogPath()
    {
        if (File.Exists(logpath))
        {
            using (StreamReader streamReader = File.OpenText(logpath))
            {
                string jsonString = streamReader.ReadToEnd();
                return JsonUtility.FromJson<SaveLog>(jsonString);
            }
        }
        return null;

    }

    public SSettings LoadSettingsPath()
    {
        if (File.Exists(settingspath))
        {
            using (StreamReader streamReader = File.OpenText(settingspath))
            {
                string jsonString = streamReader.ReadToEnd();
                return JsonUtility.FromJson<SSettings>(jsonString);
            }
        }
        return null;

    }

    public bool CheckSaveExist()
    {
        if (File.Exists(storypath))
        {
            return true;
        }
        return false;
    }

    public void DeleteSaves()
    {
        File.Delete(storypath);
        File.Delete(docpath);
        File.Delete(logpath);
    }
    




}
