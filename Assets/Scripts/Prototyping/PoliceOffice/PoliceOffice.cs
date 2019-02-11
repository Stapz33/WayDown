using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOffice : MonoBehaviour
{
    public static PoliceOffice Singleton { get; private set; }

    [SerializeField] private TextWPW Inspector;
    [SerializeField] private GameObject NameInput;
    [SerializeField] private GameObject BlackBackground;
    [SerializeField] private UnityEngine.UI.Text InputField;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(gameObject);
        else
            Singleton = this;
    }

    public void OpenNameTab()
    {
        Inspector.NameTextLaunch();
        NameInput.SetActive(true);
        BlackBackground.SetActive(true);
    }

    public void UpdateNormalInspector()
    {
        Inspector.NormalTextLaunch();
    }

    public void CloseNameTab()
    {
        Inspector.NormalTextLaunch();
        NameInput.SetActive(false);
        BlackBackground.SetActive(false);
    }

    public void NameValidation()
    {
        string GoodName = MainUIManager.Singleton.GetActualName();
        if (GoodName != null)
        {
            if (InputField.text == GoodName)
            {
                Inspector.PeopleTextLaunch();
            }
            else
            {
                Inspector.NoPeopleTextLaunch();
            }
        }
    }

    public void FaceTextLaunch()
    {
        Inspector.FaceTextLaunch();
    }
}
