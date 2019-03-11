using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeDocumentManager : MonoBehaviour
{
    [SerializeField] private GameObject m_monoDoc;
    [SerializeField] private GameObject m_multiDoc;
    [SerializeField] private GameObject m_CloseDocFolderButton;

    private Image MultiDoc_01;
    private Image MultiDoc_02;

    public void Start()
    {
        MultiDoc_01 = m_multiDoc.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        MultiDoc_02 = m_multiDoc.transform.GetChild(1).GetChild(1).GetComponent<Image>();
    }

    public void UpdateSingleDoc(Sprite sprite)
    {
        m_monoDoc.SetActive(true);
        m_monoDoc.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }

    public void HideSingleDoc()
    {
        m_monoDoc.SetActive(false);
    }

    public void UpdateMultiDoc01(Sprite sprite)
    {
        MultiDoc_02.gameObject.SetActive(false);
        m_multiDoc.SetActive(true);
        MultiDoc_01.sprite = sprite;
    }

    public void UpdateMultiDoc02(Sprite sprite)
    {
        MultiDoc_02.gameObject.SetActive(true);
        m_multiDoc.SetActive(true);
        MultiDoc_02.sprite = sprite;
    }

    public void HideMultiDoc()
    {
        m_multiDoc.SetActive(false);
    }

    public void AddDoc()
    {
        HideMultiDoc();
        m_CloseDocFolderButton.SetActive(false);
    }
}
