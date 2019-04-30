using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentInfo : MonoBehaviour
{
    private Text t_MyText;
    

    private void Awake()
    {
        t_MyText = transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()   
    {
        Vector3 newPos = Input.mousePosition;
        newPos.z = 0.36f;
        transform.position = Camera.main.ScreenToWorldPoint(newPos);
    }

    public void UpdateText(string text)
    {
        t_MyText.text = text;
    }
}
