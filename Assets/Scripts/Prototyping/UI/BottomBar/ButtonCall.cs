using System.Collections;
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
                break;
            case (int)ButtonType.SwitchDrawerTab:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PagIdx"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("parent"), true);
                break;
            case (int)ButtonType.Cigar:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CigarSprites"), true);
                break;
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

public enum ButtonType { OpenTab,CloseTab, Adress, AdressBookNext,AdressBookPrevious, AddressBookNote, AddressBookAddress, CallTaxi,OpenDocumentPanel,CloseDocumentPanel,CloseLargeDocument,OpenLargeDocument,ClosePoliceOffice,ClosePoliceName,ClosePoliceFace,OpenPoliceName,OpenPoliceFace,OpenPoliceOffice,PoliceOfficeNameValidation, PoliceOfficeCSValidation, SwitchDrawerTab, AlphabeticalButton,AddNewDocumentToComparison,none,RadioChannel,ToggleRadio, Whisky, CloseLargeDocumentSolo, Cigar }
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

    private void Start()
    {
        if (TypeOfButton == ButtonType.OpenDocumentPanel)
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
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
                break;
            case ButtonType.RadioChannel:
                AudioManager.Singleton.ChangeRadioChannel();
                break;
            case ButtonType.ToggleRadio:
                AudioManager.Singleton.ToggleRadio();
                break;
            case ButtonType.Whisky:
                AudioManager.Singleton.ActivateAudio(AudioType.Whisky);
                break;
            case ButtonType.Cigar:
                if (cigarIdx < CigarSprites.Count - 1)
                {
                    cigarIdx++;
                    AudioManager.Singleton.ActivateAudio(AudioType.Cigar);
                }
                else
                    cigarIdx = 0;
                GetComponent<Image>().sprite = CigarSprites[cigarIdx];
                break;
            default:
                break;
        }
    }

    public IEnumerator Resetcall()
    {
        GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.3f);
        GetComponent<Button>().interactable = true;
    }


    public void Enter()
    {
            MainUIManager.Singleton.ActivateDocumentInfo(DocumentInfo);
    }

    public void Exit()
    {
            MainUIManager.Singleton.DeactivateDocumentInfo();
    }

    public void PlaySound()
    {
        AudioManager.Singleton.HoverButton();
    }
}
