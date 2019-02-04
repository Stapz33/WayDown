using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Address_data : MonoBehaviour
{
    private string ActualKnot;
    // Start is called before the first frame update
    public string GetActualStory()
    {
        return ActualKnot;
    }

    public void SetActualStory(string newStory)
    {
        ActualKnot = newStory;
    }
}
