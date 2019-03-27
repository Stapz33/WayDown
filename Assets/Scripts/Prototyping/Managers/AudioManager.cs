using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {AddressBookOpen,DrawerOpen,MapOpen,NewDocument,ChangePageAddressBook,CallTaxi,Text,LoadingTransition }

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_ClickAudio = null;
    [SerializeField] private AudioSource m_ButtonHoverAudio = null;
    [SerializeField] private AudioSource m_TextAudio = null;
    [SerializeField] private AudioSource m_LoadingTransitionAudio = null;

    [Header("Address Book")]
    [SerializeField] private AudioSource m_AddressBookOpenAudio = null;
    [SerializeField] private AudioSource m_ChangePageAddressBookAudio = null;
    [SerializeField] private AudioSource m_CallTaxiAudio = null;

    [Header("Documents")]
    [SerializeField] private AudioSource m_DrawerOpenAudio = null;
    [SerializeField] private AudioSource m_NewDocumentAudio = null;

    [Header("Map")]
    [SerializeField] private AudioSource m_MapOpenAudio = null;

    public static AudioManager Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton != null)
            Destroy(gameObject);
        else Singleton = this;

        DontDestroyOnLoad(gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_ClickAudio.Play();
        }
    }

    public void HoverButton()
    {
        m_ButtonHoverAudio.Play();
    }

    public void ActivateAudio(AudioType type)
    {
        switch (type)
        {
            case AudioType.AddressBookOpen:
                m_AddressBookOpenAudio.Play();
                break;
            case AudioType.DrawerOpen:
                m_DrawerOpenAudio.Play();
                break;
            case AudioType.MapOpen:
                m_MapOpenAudio.Play();
                break;
            case AudioType.NewDocument:
                m_NewDocumentAudio.Play();
                break;
            case AudioType.ChangePageAddressBook:
                m_ChangePageAddressBookAudio.Play();
                break;
            case AudioType.CallTaxi:
                m_CallTaxiAudio.Play();
                break;
            case AudioType.Text:
                m_TextAudio.Play();
                break;
            case AudioType.LoadingTransition:
                m_LoadingTransitionAudio.Play();
                break;
            default:
                break;
        }
    }
}
