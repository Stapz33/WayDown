﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ButtonCall))]
public class ButtonCallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var InvType = serializedObject.FindProperty("TypeOfButton");
        EditorGUILayout.PropertyField(InvType, true);
        switch (InvType.enumValueIndex)
        {
            case (int)ButtonType.OpenTab:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("TypeOfTab"), true);
                break;
            case (int)ButtonType.AddressBookNote:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PagIdx"), true);
                break;
            case (int)ButtonType.OpenDocumentPanel:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("TypeOfDocument"), true);
                break;
            case (int)ButtonType.OpenLargeDocument:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("DocumentInfo"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("BWText"), true);
                break;
            case (int)ButtonType.SwitchDrawerTab:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PagIdx"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("parent"), true);
                break;
            case (int)ButtonType.Cigar:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CigarSprites"), true);
                break;
            case (int)ButtonType.Whisky:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CigarSprites"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Bottle"), true);
                break;
            case (int)ButtonType.WhiskyBottle:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Bottle"), true);
                break;
            case (int)ButtonType.ToggleRadio:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("radioled"), true);
                break;
            case (int)ButtonType.SelectProof:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("DocumentInfo"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("isGood"), true);
                break;
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

public enum ButtonType { OpenTab,CloseTab, Adress, AdressBookNext,AdressBookPrevious, AddressBookNote, AddressBookAddress, CallTaxi,OpenDocumentPanel,CloseDocumentPanel,CloseLargeDocument,OpenLargeDocument,ClosePoliceOffice,ClosePoliceName,ClosePoliceFace,OpenPoliceName,OpenPoliceFace,OpenPoliceOffice,PoliceOfficeNameValidation, PoliceOfficeCSValidation, SwitchDrawerTab, AlphabeticalButton,AddNewDocumentToComparison,none,RadioChannel,ToggleRadio, Whisky, CloseLargeDocumentSolo, Cigar, WhiskyBottle,SelectProof }
public class ButtonCall : MonoBehaviour
{

    public ButtonType TypeOfButton;
    public TabType TypeOfTab;
    public DocumentFolder TypeOfDocument;
    public int PagIdx;
    public string DocumentInfo;
    public Transform parent;
    public List<Sprite> CigarSprites;
    private int cigarIdx = 0;
    public GameObject Bottle;
    public Texture2D BWText;
    int whiskeyDrank = 0;
    public bool isGood;
    public GameObject radioled;

    private void Start()
    {
        if (TypeOfButton == ButtonType.OpenDocumentPanel)
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        if (TypeOfButton == ButtonType.Whisky)
        {
            cigarIdx = CigarSprites.Count - 1;
            Bottle.SetActive(true);
            GetComponent<Image>().sprite = CigarSprites[cigarIdx];
            whiskeyDrank = 0;
        }
            
    }
    public void OnClick()
    {
            
        switch (TypeOfButton)
        {
            case ButtonType.OpenTab:
                MainUIManager.Singleton.OpenTab(TypeOfTab);
                break;
            case ButtonType.CloseTab:
                MainUIManager.Singleton.CloseTab();
                break;
            case ButtonType.Adress:
                StartCoroutine("Resetcall");
                MainUIManager.Singleton.GoToAddres(GetComponent<Address_data>().GetActualStory(),GetComponent<Address_data>().GetDocumentFolder(), GetComponent<Address_data>().GetBG());
                break;
            case ButtonType.AdressBookNext:
                AddressBookManager.Singleton.NextPage();
                break;
            case ButtonType.AdressBookPrevious:
                AddressBookManager.Singleton.PreviousPage();
                break;
            case ButtonType.AddressBookNote:
                AddressBookManager.Singleton.JumpToPage(PagIdx);
                break;
            case ButtonType.AddressBookAddress:
                AddressBookManager.Singleton.SetValidationText(transform.GetComponentInChildren<UnityEngine.UI.Text>().text);
                break;
            case ButtonType.CallTaxi:
                StartCoroutine("Resetcall");
                AudioManager.Singleton.ActivateAudio(AudioType.CallTaxi);
                MainUIManager.Singleton.LoadScreen("CallTaxi",true);
                break;
            case ButtonType.OpenDocumentPanel:
                MainUIManager.Singleton.OpenDocumentPanel(TypeOfDocument);
                break;
            case ButtonType.CloseDocumentPanel:
                MainUIManager.Singleton.CloseDocumentPanel();
                break;
            case ButtonType.OpenLargeDocument:
                MainUIManager.Singleton.ShowLargeDocumentMulti(GetComponent<Image>().sprite);
                break;
            case ButtonType.CloseLargeDocument:
                MainUIManager.Singleton.CloseLargeDocument();
                break;
            case ButtonType.CloseLargeDocumentSolo:
                MainUIManager.Singleton.CloseLargeDocumentSolo();
                break;
            case ButtonType.ClosePoliceOffice:
                StartCoroutine("Resetcall");
                MainUIManager.Singleton.LoadScreen("ClosePoliceOffice",false);
                break;
            case ButtonType.ClosePoliceFace:
                PoliceOffice.Singleton.CloseCSTab();
                break;
            case ButtonType.ClosePoliceName:
                PoliceOffice.Singleton.CloseNameTab();
                break;
            case ButtonType.OpenPoliceOffice:
                StartCoroutine("Resetcall");
                MainUIManager.Singleton.LoadScreen("OpenPoliceOffice",false);
                break;
            case ButtonType.OpenPoliceFace:
                PoliceOffice.Singleton.OpenCSTab();
                break;
            case ButtonType.OpenPoliceName:
                PoliceOffice.Singleton.OpenNameTab();
                break;
            case ButtonType.PoliceOfficeNameValidation:
                StartCoroutine("Resetcall");
                PoliceOffice.Singleton.NameValidation();
                break;
            case ButtonType.PoliceOfficeCSValidation:
                StartCoroutine("Resetcall");
                MainUIManager.Singleton.TestCS();
                break;
            case ButtonType.SwitchDrawerTab:
                parent.GetComponent<DocumentFolderManager>().SetupTab(PagIdx);
                break;
            case ButtonType.AlphabeticalButton:
                AddressBookManager.Singleton.GoToAlphabetical();
                break;
            case ButtonType.AddNewDocumentToComparison:
                MainUIManager.Singleton.AddNewDocToComparision();
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case ButtonType.RadioChannel:
                AudioManager.Singleton.ChangeRadioChannel();
                break;
            case ButtonType.ToggleRadio:
                AudioManager.Singleton.ToggleRadio();
                radioled.SetActive(!radioled.activeSelf);
                break;
            case ButtonType.Whisky:
                if (cigarIdx < CigarSprites.Count - 2)
                {
                    cigarIdx++;
                    GetComponent<Image>().sprite = CigarSprites[cigarIdx];
                    AudioManager.Singleton.ActivateAudio(AudioType.Whisky);

                    return;
                }
                else if (cigarIdx == CigarSprites.Count - 2)
                {
                    cigarIdx++;
                    Bottle.SetActive(true);
                    GetComponent<Image>().sprite = CigarSprites[cigarIdx];
                    AudioManager.Singleton.ActivateAudio(AudioType.Whisky);
                    whiskeyDrank++;
                    if(whiskeyDrank >= 3)
                    {
                        whiskeyDrank = 0;
                        MainUIManager.Singleton.AddBlurToScreen();
                    }
                }
                
                break;
            case ButtonType.Cigar:
                if (cigarIdx < CigarSprites.Count - 1)
                {
                    cigarIdx++;
                    AudioManager.Singleton.ActivateAudio(AudioType.Cigar);
                }
                else
                {
                    cigarIdx = 0;
                    AudioManager.Singleton.ActivateAudio(AudioType.Cigar);
                    MainUIManager.Singleton.ActivateCender();
                }
                    
                GetComponent<Image>().sprite = CigarSprites[cigarIdx];
                break;
            case ButtonType.WhiskyBottle:
                Bottle.GetComponent<ButtonCall>().Whiksy();
                transform.GetChild(0).gameObject.SetActive(false);
                AudioManager.Singleton.ActivateAudio(AudioType.Bottle);
                gameObject.SetActive(false);
                break;
            case ButtonType.SelectProof:
                MainUIManager.Singleton.SelectProof(gameObject,isGood);
                Exit();
                break;
            default:
                break;
        }
    }

    public IEnumerator Resetcall()
    {
        GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Button>().interactable = true;
    }


    public void Enter()
    {
        if (TypeOfButton == ButtonType.SelectProof)
        {
            LastSectionManager.s_Singleton.ActivateDocumentInfo(GetComponent<Image>().sprite);
        }
        else
        {
            MainUIManager.Singleton.ActivateDocumentInfo(DocumentInfo);
        }
    }

    public void Exit()
    {
        if (TypeOfButton == ButtonType.SelectProof)
        {
            LastSectionManager.s_Singleton.DeactivateDocumentInfo();
        }
        else
        {
            MainUIManager.Singleton.DeactivateDocumentInfo();
        }
    }

    public void PlaySound()
    {
        AudioManager.Singleton.HoverButton();
    }

    public void Whiksy()
    {
        GetComponent<Button>().interactable = true;
        cigarIdx = 0;
        GetComponent<Image>().sprite = CigarSprites[cigarIdx];
    }
}
