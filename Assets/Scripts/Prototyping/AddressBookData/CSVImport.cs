using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVImport : MonoBehaviour
{

    public static CSVImport Singleton { get; private set; } // Make the script accesible from everywhere

    public TextAsset csvFile; // public variable to stock the csvFile ( who contains loca / quest... )

    public List<TextImportation> keysImport; // Stock each key ( line ) in a custom class

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

        string[] data = csvFile.text.Split(new char[] { '\n' }); // Separate each line of a file ( CSV better )


        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' }); // Separate each ',' from a CSV file
            TextImportation importRow = new TextImportation(); // Create a custom class to stock text / id...
            importRow.PageName = row[0];
            for (int y = 1; y < row.Length; y++)
            {
                importRow.Addresses.Add(row[y]);
            }

            keysImport.Add(importRow);
        }


    }

    public List<string> GetPage(int pageToGet)
    {
        for(int i = 0; i < keysImport.Count; i++)
        {
            if(pageToGet.ToString("") == keysImport[i].PageName)
            {
                return keysImport[i].Addresses;
            }
        }

        return null;
    }
}

[Serializable]
public class TextImportation
{
    public string PageName;
    public List<string> Addresses = new List<string>();
}