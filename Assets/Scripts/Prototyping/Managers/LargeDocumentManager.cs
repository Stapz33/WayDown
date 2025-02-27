﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeDocumentManager : MonoBehaviour
{
    [SerializeField] private GameObject m_monoDoc = null;
    [SerializeField] private GameObject m_multiDoc = null;
    [SerializeField] private GameObject m_ComparisonImage = null;
    [SerializeField] private Material m_Shader = null;
    public GameObject BackButton;
    public Animator m_NewDocAnimator = null;

    private Image MultiDoc_01;
    private Image MultiDoc_02;

    private Sprite feedbackSprite = null;
    private float m_MaskAmount = 0f;
    private bool needtoMask = false;

    public void Awake()
    {
        MultiDoc_01 = m_multiDoc.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        MultiDoc_02 = m_multiDoc.transform.GetChild(1).GetChild(1).GetComponent<Image>();
    }

    private void Update()
    {
        if (needtoMask)
        {
            m_MaskAmount += Time.deltaTime * 3;
            if (m_MaskAmount >= 10f)
            {
                m_MaskAmount = 10f;
                needtoMask = false;
            }
            m_Shader.SetFloat("_MaskAmount", m_MaskAmount);
        }
    }

    public void UpdateSingleDoc(Sprite sprite,Texture2D BW)
    {
        m_monoDoc.SetActive(true);
        Transform Shad = m_monoDoc.transform.GetChild(1);
        Shad.gameObject.SetActive(true);
        m_monoDoc.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        m_Shader.SetTexture("_Text_01", BW);
        feedbackSprite = sprite;
        needtoMask = true;
    }

    public void UpdateSingleDoc(Sprite sprite)
    {
        m_monoDoc.SetActive(true);
        m_monoDoc.transform.GetChild(1).gameObject.SetActive(false);
        m_monoDoc.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        feedbackSprite = sprite;
    }

    public void HideSingleDoc()
    {
        m_monoDoc.SetActive(false);
    }

    public void HideSingleDocSolo()
    {
        m_monoDoc.SetActive(false);
        m_NewDocAnimator.transform.GetChild(1).GetComponent<Image>().sprite = feedbackSprite;
        m_NewDocAnimator.SetTrigger("Info");
        AudioManager.Singleton.ActivateAudio(AudioType.NewLog);
        m_Shader.SetFloat("_MaskAmount", 0f);
        m_MaskAmount = 0f;
        needtoMask = false;
        MainUIManager.Singleton.ReactivateDialogue();
    }

    public void UpdateMultiDoc01(Sprite sprite)
    {
        MultiDoc_02.gameObject.SetActive(false);
        m_multiDoc.SetActive(true);
        MultiDoc_01.sprite = sprite;
        BackButton.SetActive(false);
    }

    public void UpdateMultiDoc02(Sprite sprite)
    {
        m_ComparisonImage.SetActive(false);
        MultiDoc_02.gameObject.SetActive(true);
        m_multiDoc.SetActive(true);
        MultiDoc_02.sprite = sprite;
        BackButton.SetActive(false);
    }

    public void HideMultiDoc()
    {
        m_multiDoc.SetActive(false);
        BackButton.SetActive(true);
    }

    public void HideMultiDocadd()
    {
        m_multiDoc.SetActive(false);
        MainUIManager.Singleton.CloseDocumentPanel();
    }

    public void AddDoc()
    {
        HideMultiDocadd();
        m_ComparisonImage.transform.GetChild(0).GetComponent<Image>().sprite = MultiDoc_01.sprite;
        m_ComparisonImage.SetActive(true);
    }

    public void CloseComparison()
    {
        m_ComparisonImage.SetActive(false);
        
    }
}
