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
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

public enum ButtonType { OpenTab,CloseTab, Adress, AdressBookNext,AdressBookPrevious, AddressBookNote, AddressBookAddress, CallTaxi,OpenDocumentPanel,CloseDocumentPanel,CloseLargeDocument,OpenLargeDocument,ClosePoliceOffice,ClosePoliceName,ClosePoliceFace,OpenPoliceName,OpenPoliceFace,OpenPoliceOffice,PoliceOfficeNameValidation, PoliceOfficeCSValidation, SwitchDrawerTab, AlphabeticalButton,AddNewDocumentToComparison }
public class ButtonCall : MonoBehaviour
{

    public ButtonType TypeOfButton;
    public TabType TypeOfTab;
    public DocumentFolder TypeOfDocument;
    public int PagIdx;
    public string DocumentInfo;
    public Transform parent;

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
                MainUIManager.Singleton.GoToAddres(GetComponent<Address_data>().GetActualStory(),GetComponent<Address_data>().GetDocumentFolder());
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
            case ButtonType.ClosePoliceOffice:
                MainUIManager.Singleton.LoadScreen("ClosePoliceOffice",false);
                break;
            case ButtonType.ClosePoliceFace:
                PoliceOffice.Singleton.CloseCSTab();
                break;
            case ButtonType.ClosePoliceName:
                PoliceOffice.Singleton.CloseNameTab();
                break;
            case ButtonType.OpenPoliceOffice:
                MainUIManager.Singleton.LoadScreen("OpenPoliceOffice",false);
                break;
            case ButtonType.OpenPoliceFace:
                PoliceOffice.Singleton.OpenCSTab();
                break;
            case ButtonType.OpenPoliceName:
                PoliceOffice.Singleton.OpenNameTab();
                break;
            case ButtonType.PoliceOfficeNameValidation:
                PoliceOffice.Singleton.NameValidation();
                break;
            case ButtonType.PoliceOfficeCSValidation:
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
            default:
                break;
        }
    }


    public void Enter()
    {
            MainUIManager.Singleton.ActivateDocumentInfo(DocumentInfo);
    }

    public void Exit()
    {
            MainUIManager.Singleton.DeactivateDocumentInfo();
    }
}
