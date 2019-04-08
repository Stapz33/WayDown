using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Address_data : MonoBehaviour
{
    private string ActualKnot = null;
    [SerializeField] private string m_addressDescription = null;
    [SerializeField] private Sprite m_addressImage = null;
    [SerializeField] private DocumentFolder m_MyAddresDoc = new DocumentFolder();

    [SerializeField] private GameObject m_infosPanel = null;

    private void Awake()
    {
        m_infosPanel.transform.GetChild(0).GetComponent<Text>().text = m_addressDescription;
        m_infosPanel.transform.GetChild(1).GetComponent<Image>().sprite = m_addressImage;
    }

    // Start is called before the first frame update
    public string GetActualStory()
    {
        return ActualKnot;
    }

    public void SetActualStory(string newStory)
    {
        ActualKnot = newStory;
    }

    public void PointerEnter()
    {
        m_infosPanel.SetActive(true);
    }

    public void PointerExit()
    {
        m_infosPanel.SetActive(false);
    }

    public DocumentFolder GetDocumentFolder ()
    {
        return m_MyAddresDoc;
    }

    public Sprite GetBG()
    {
        return m_addressImage;
    }
}
