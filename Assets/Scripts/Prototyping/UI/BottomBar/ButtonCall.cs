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
            case ButtonType.Tab:
                cs_Script.ButtonIndex = EditorGUILayout.IntField("Button Index", cs_Script.ButtonIndex);
                break;
            case ButtonType.Adress:
                break;
            default:
                break;
        }
    }
}
#endif

public enum ButtonType { Tab, Adress }
public class ButtonCall : MonoBehaviour
{

    public ButtonType TypeOfButton;
    public int ButtonIndex;

    public void OnClick()
    {
        switch (TypeOfButton)
        {
            case ButtonType.Tab:
                PlayerCameraManager.Singleton.SetPlayerTab(ButtonIndex);
                break;
            case ButtonType.Adress:
                PlayerPinManager.Singleton.GoToAdress(transform.position);
                break;
            default:
                break;
        }
    }
}
