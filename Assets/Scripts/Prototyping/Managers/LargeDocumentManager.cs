using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeDocumentManager : MonoBehaviour
{
    [SerializeField] private GameObject m_monoDoc = null;
    [SerializeField] private GameObject m_multiDoc = null;
    [SerializeField] private GameObject m_ComparisonImage = null;
    public Animator m_NewDocAnimator = null;

    private Image MultiDoc_01;
    private Image MultiDoc_02;


    public void Awake()
    {
        MultiDoc_01 = m_multiDoc.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        MultiDoc_02 = m_multiDoc.transform.GetChild(1).GetChild(1).GetComponent<Image>();
    }

    public void UpdateSingleDoc(Sprite sprite,Texture2D BW)
    {
        m_monoDoc.SetActive(true);
        Transform Shad = m_monoDoc.transform.GetChild(1);
        Shad.gameObject.SetActive(true);
        m_monoDoc.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        Shad.GetComponent<Image>().sprite = sprite;
        //Shad.GetComponent<Image>().material.SetTexture("_Text_01", BW);
    }

    public void UpdateSingleDoc(Sprite sprite)
    {
        m_monoDoc.SetActive(true);
        m_monoDoc.transform.GetChild(1).gameObject.SetActive(false);
        m_monoDoc.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        m_monoDoc.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
    }

    public void HideSingleDoc()
    {
        m_monoDoc.SetActive(false);
    }

    public void HideSingleDocSolo()
    {
        m_monoDoc.SetActive(false);
        m_NewDocAnimator.SetTrigger("Info");
        AudioManager.Singleton.ActivateAudio(AudioType.NewLog);
    }

    public void UpdateMultiDoc01(Sprite sprite)
    {
        MultiDoc_02.gameObject.SetActive(false);
        m_multiDoc.SetActive(true);
        MultiDoc_01.sprite = sprite;
    }

    public void UpdateMultiDoc02(Sprite sprite)
    {
        m_ComparisonImage.SetActive(false);
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
        m_ComparisonImage.SetActive(true);
    }

    public void CloseComparison()
    {
        m_ComparisonImage.SetActive(false);
        
    }
}
