using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOffice : MonoBehaviour
{
    public static PoliceOffice Singleton { get; private set; }

    [SerializeField] private TextWPW Inspector = null;
    [SerializeField] private GameObject NameInput = null;
    [SerializeField] private GameObject CsInput = null;
    [SerializeField] private GameObject BlackBackgroundN = null;
    [SerializeField] private GameObject BlackBackgroundCS = null;
    [SerializeField] private UnityEngine.UI.Text InputField = null;
    public ButtonCall Validation;

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
            if (Validation.GetComponent<UnityEngine.UI.Button>().interactable == true)
            {
                NameValidation();
                StartCoroutine(Validation.Resetcall());
            }
        }
    }

    public void OpenNameTab()
    {
        Inspector.NameTextLaunch();
        NameInput.SetActive(true);
        BlackBackgroundN.SetActive(true);
    }

    public void OpenCSTab()
    {
        Inspector.FaceTextLaunch();
        MainUIManager.Singleton.ResetDropdownValue();
        CsInput.SetActive(true);
        BlackBackgroundCS.SetActive(true);
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
        BlackBackgroundN.SetActive(false);
    }

    public void CloseCSTab()
    {
        Inspector.NormalTextLaunch();
        CsInput.SetActive(false);
        BlackBackgroundCS.SetActive(false);
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

    public void GoodNameInput()
    {
        NameInput.SetActive(false);
        BlackBackgroundN.SetActive(false);
    }

    public void GoodCSInput()
    {
        CsInput.SetActive(false);
        BlackBackgroundCS.SetActive(false);
    }

    #endregion

    #region CS_VERIFICATION

    public void WrongCS()
    {
        Inspector.NoCSTextLaunch();
    }
    #endregion

}
