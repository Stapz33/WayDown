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
        Vector3 newPos = new Vector3(Input.mousePosition.x +188, Input.mousePosition.y -70, Input.mousePosition.z);
        transform.position = newPos;
    }

    public void UpdateText(string text)
    {
        t_MyText.text = text;
    }
}
