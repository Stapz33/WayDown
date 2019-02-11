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
    ButtonCall cs_Script;
    private void OnEnable()
    {
        cs_Script = (ButtonCall)target;
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "ButtonCall");
        cs_Script.TypeOfButton = (ButtonType)EditorGUILayout.EnumPopup("Button Type", cs_Script.TypeOfButton);

        switch (cs_Script.TypeOfButton)
        {
            case ButtonType.OpenTab:
                cs_Script.TypeOfTab = (TabType)EditorGUILayout.EnumPopup("Tab To Open", cs_Script.TypeOfTab);
                break;
            case ButtonType.AddressBookNote:
                cs_Script.PagIdx = EditorGUILayout.IntField("Page Index", cs_Script.PagIdx);
                break;
            case ButtonType.OpenDocumentPanel:
                cs_Script.TypeOfDocument = (DocumentType)EditorGUILayout.EnumPopup("Document Panel To Open", cs_Script.TypeOfDocument);
                break;
            case ButtonType.OpenLargeDocument:
                cs_Script.DocumentInfo = EditorGUILayout.TextField("DocumentText", cs_Script.DocumentInfo);
                break;
            default:
                break;
        }
    }
}
#endif

public enum ButtonType { OpenTab,CloseTab, Adress, AdressBookNext,AdressBookPrevious, AddressBookNote, AddressBookAddress, CallTaxi,OpenDocumentPanel,CloseDocumentPanel,CloseLargeDocument,OpenLargeDocument,ClosePoliceOffice,ClosePoliceName,ClosePoliceFace,OpenPoliceName,OpenPoliceFace,OpenPoliceOffice,PoliceOfficeNameValidation}
public class ButtonCall : MonoBehaviour
{

    public ButtonType TypeOfButton;
    public TabType TypeOfTab;
    public DocumentType TypeOfDocument;
    public int PagIdx;
    public string DocumentInfo;

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
                MainUIManager.Singleton.GoToAddres(GetComponent<Address_data>().GetActualStory());
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
                MainUIManager.Singleton.OpenLargeDocument(GetComponent<Image>().sprite);
                break;
            case ButtonType.CloseLargeDocument:
                MainUIManager.Singleton.CloseLargeDocument();
                break;
            case ButtonType.ClosePoliceOffice:
                MainUIManager.Singleton.LoadScreen("ClosePoliceOffice",false);
                break;
            case ButtonType.ClosePoliceFace:
                break;
            case ButtonType.ClosePoliceName:
                PoliceOffice.Singleton.CloseNameTab();
                break;
            case ButtonType.OpenPoliceOffice:
                MainUIManager.Singleton.LoadScreen("OpenPoliceOffice",false);
                break;
            case ButtonType.OpenPoliceFace:
                break;
            case ButtonType.OpenPoliceName:
                PoliceOffice.Singleton.OpenNameTab();
                break;
            case ButtonType.PoliceOfficeNameValidation:
                PoliceOffice.Singleton.NameValidation();
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
