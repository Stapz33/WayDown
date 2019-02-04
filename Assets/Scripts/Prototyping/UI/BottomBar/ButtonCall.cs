using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            default:
                break;
        }
    }
}
#endif

public enum ButtonType { OpenTab,CloseTab, Adress, AdressBookNext,AdressBookPrevious, AddressBookNote, AddressBookAddress, CallTaxi }
public class ButtonCall : MonoBehaviour
{

    public ButtonType TypeOfButton;
    public TabType TypeOfTab;
    public int PagIdx;

    public void OnClick()
    {
        switch (TypeOfButton)
        {
            case ButtonType.OpenTab:
                MainUIManager.Singleton.ActivateTab(TypeOfTab);
                break;
            case ButtonType.CloseTab:
                MainUIManager.Singleton.DeactivateTab();
                break;
            case ButtonType.Adress:
                PlayerPinManager.Singleton.GoToAdress(transform.position);
                MainUIManager.Singleton.SetupNewStory(GetComponent<Address_data>().GetActualStory());
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
                MainUIManager.Singleton.LoadScreen("SetupDialogueSystem");
                break;
            default:
                break;
        }
    }
}
