using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public Sprite spriteToSet;
    public UnityEngine.UI.Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void SetBackground()
    {
        image.sprite = spriteToSet;
        image.GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
    }

    public void SetTransition()
    {
        image.sprite = null;
        image.GetComponent<UnityEngine.Video.VideoPlayer>().enabled = true;
    }
}
