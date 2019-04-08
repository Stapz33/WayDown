using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOffice : MonoBehaviour
{
    public static PoliceOffice Singleton { get; private set; }

    [SerializeField] private TextWPW Inspector = null;
    [SerializeField] private GameObject NameInput = null;
    [SerializeField] private GameObject CsInput = null;
    [SerializeField] private GameObject BlackBackground = null;
    [SerializeField] private UnityEngine.UI.Text InputField = null;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(gameObject);
        else
            Singleton = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown("return") && NameInput.activeSelf)
        {
            NameValidation();
        }
    }

    public void OpenNameTab()
    {
        Inspector.NameTextLaunch();
        NameInput.SetActive(true);
        BlackBackground.SetActive(true);
    }

    public void OpenCSTab()
    {
        Inspector.NameTextLaunch();
        MainUIManager.Singleton.ResetDropdownValue();
        CsInput.SetActive(true);
        BlackBackground.SetActive(true);
    }

    public void UpdateNormalInspector()
    {
        Inspector.NormalTextLaunch();
    }

    public void CloseNameTab()
    {
        Inspector.NormalTextLaunch();
        NameInput.transform.GetChild(0).GetComponent<UnityEngine.UI.InputField>().text = "";
        NameInput.SetActive(false);
        BlackBackground.SetActive(false);
    }

    public void CloseCSTab()
    {
        Inspector.NormalTextLaunch();
        CsInput.SetActive(false);
        BlackBackground.SetActive(false);
    }

    public void FaceTextLaunch()
    {
        Inspector.FaceTextLaunch();
    }

    #region NAME_VERIFICATION
    public void GoodName()
    {
        Inspector.PeopleTextLaunch();
    }

    public void WrongName()
    {
        Inspector.NoPeopleTextLaunch();
    }

    public void AlreadyGood()
    {
        Inspector.AlreadyPeopleTextLaunch();
    }

    public void NameValidation()
    {
        MainUIManager.Singleton.NameValidation(InputField.text);
    }

    #endregion

    #region CS_VERIFICATION

    public void WrongCS()
    {
        Inspector.NoCSTextLaunch();
    }
    #endregion

}
