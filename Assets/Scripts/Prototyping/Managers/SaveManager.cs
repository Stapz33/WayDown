using System;
using System.IO;
using UnityEngine;
using static MainUIManager;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Singleton { get; private set; }

    string storypath;
    string docpath;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(this);
        else
            Singleton = this;
        SetupPaths();
    }

    public void SetupPaths()
    {
        storypath = Path.Combine(Application.persistentDataPath, "Story.txt");
        docpath = Path.Combine(Application.persistentDataPath, "Document.txt");
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
}
