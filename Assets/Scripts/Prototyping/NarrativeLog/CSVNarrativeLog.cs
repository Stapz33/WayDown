using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVNarrativeLog : MonoBehaviour
{

    public static CSVNarrativeLog Singleton { get; private set; } // Make the script accesible from everywhere

    public TextAsset csvFile; // public variable to stock the csvFile ( who contains loca / quest... )

    public List<TextImportationLog> keysImport; // Stock each key ( line ) in a custom class

    private void Awake()
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

    public void Seta()
    {
        string[] data = csvFile.text.Split(new char[] { '\n' }); // Separate each line of a file ( CSV better )

        TextImportationLog importRow = new TextImportationLog();

        
        string[] row = data[1].Split(new char[] { ',' }); // Separate each ',' from a CSV file
                                                            // Create a custom class to stock text / id...
        for (int y = 1; y < row.Length; y++)
        {
            importRow.Logs.Add(row[y]);
        }


        string[] rows = data[2].Split(new char[] { ',' }); // Separate each ',' from a CSV file
                                                          // Create a custom class to stock text / id...
        for (int y = 1; y < rows.Length; y++)
        {
            importRow.CLogs.Add(rows[y]);
        }

        keysImport.Add(importRow);

    }

    public List<string> GetStrings()
    {
        for (int i = 0; i < keysImport.Count;)
        {
            return keysImport[i].Logs;
        }
        return null;
    }

    public List<string> GetClogs()
    {
        for (int i = 0; i < keysImport.Count;)
        {
            return keysImport[i].CLogs;
        }
        return null;
    }
}

[Serializable]
public class TextImportationLog
{
    public List<string> Logs = new List<string>();
    public List<string> CLogs = new List<string>();
}