using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Address_data : MonoBehaviour
{
    private TextAsset ActualStory;
    // Start is called before the first frame update
    public TextAsset GetActualStory()
    {
        return ActualStory;
    }

    public void SetActualStory(TextAsset newStory)
    {
        ActualStory = newStory;
    }
}
