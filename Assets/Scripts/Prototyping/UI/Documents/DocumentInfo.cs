using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentInfo : MonoBehaviour
{
    public bool isImage;
    private Text t_MyText;
    

    private void Awake()
    {
        if (!isImage)
        t_MyText = transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()   
    {
        Vector3 newPos = Input.mousePosition;
        if (!isImage)
        {
            newPos.z = 0.36f;
        }
        else
        {
            newPos.z = 0.36f;
            newPos.x += 140f;
            newPos.y += 167f;
        }
        transform.position = Camera.main.ScreenToWorldPoint(newPos);
    }

    public void UpdateText(string text)
    {
        t_MyText.text = text;
    }
}
