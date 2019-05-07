using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {AddressBookOpen,DrawerOpen,MapOpen,NewDocument,ChangePageAddressBook,CallTaxi,Text,LoadingTransition,PhoneRing, Whisky,NewLog,Cigar, Bottle,BarEntrance,CloseCarDoor,hailCab }

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_ClickAudio = null;
    [SerializeField] private AudioSource m_ButtonHoverAudio = null;
    [SerializeField] private AudioSource m_TextAudio = null;
    [SerializeField] private AudioSource m_LoadingTransitionAudio = null;

    [Header("Music")]
    [SerializeField] private AudioSource m_MusicSource = null;
    [SerializeField] private AudioClip m_MainMenuMusic = null;
    [SerializeField] private AudioClip m_StreetAmbiance = null;
    [SerializeField] private AudioClip m_BarAmbiance = null;
    [SerializeField] private AudioClip m_CarAmbiance = null;

    [Header("Address Book")]
    [SerializeField] private AudioSource m_AddressBookOpenAudio = null;
    [SerializeField] private AudioSource m_ChangePageAddressBookAudio = null;
    [SerializeField] private AudioSource m_CallTaxiAudio = null;

    [Header("Documents")]
    [SerializeField] private AudioSource m_DrawerOpenAudio = null;
    [SerializeField] private AudioSource m_NewDocumentAudio = null;

    [Header("Map")]
    [SerializeField] private AudioSource m_MapOpenAudio = null;

    [Header("Others")]
    [SerializeField] private AudioSource m_PhoneRingAudio = null;
    [SerializeField] private AudioSource m_WhiskyAudio = null;
    [SerializeField] private AudioSource m_BottleAudio = null;
    [SerializeField] private AudioSource m_NewLogAudio = null;
    [SerializeField] private AudioSource m_CigarAudio = null;
    [SerializeField] private AudioSource m_BarEntranceAudio = null;
    [SerializeField] private AudioSource m_CarDoorAudio = null;
    [SerializeField] private AudioSource m_HailCabAudio = null;

    [Header("Radio")]
    [SerializeField] private AudioSource m_RadioAudio = null;
    [SerializeField] private AudioSource m_RadioTransitionAudio = null;
    [SerializeField] private List<AudioClip> m_RadioClips = new List<AudioClip>();

    private int i_ChannelIdx = 0;
    private bool b_IsRadioActive = false;

    public static AudioManager Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton != null)
            Destroy(gameObject);
        else Singleton = this;

        DontDestroyOnLoad(gameObject);
        m_RadioAudio.clip = m_RadioClips[0];
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
                float rnd = Random.Range(0.7f,3f);
                m_TextAudio.pitch = rnd;
                m_TextAudio.Play();
                break;
            case AudioType.LoadingTransition:
                m_LoadingTransitionAudio.Play();
                break;
            case AudioType.PhoneRing:
                m_PhoneRingAudio.Play();
                break;
            case AudioType.BarEntrance:
                m_BarEntranceAudio.Play();
                break;

            case AudioType.Whisky:
                m_WhiskyAudio.Play();
                break;
            case AudioType.Bottle:
                m_BottleAudio.Play();
                break;
            case AudioType.NewLog:
                m_NewLogAudio.Play();
                break;
            case AudioType.Cigar:
                m_CigarAudio.Play();
                break;
            case AudioType.CloseCarDoor:
                m_CarDoorAudio.Play();
                break;
            case AudioType.hailCab:
                m_HailCabAudio.Play();
                break;
            default:
                break;
        }
    }

    public void StopAudio(AudioType type)
    {
        switch (type)
        {
            case AudioType.AddressBookOpen:
                m_AddressBookOpenAudio.Stop();
                break;
            case AudioType.DrawerOpen:
                m_DrawerOpenAudio.Stop();
                break;
            case AudioType.MapOpen:
                m_MapOpenAudio.Stop();
                break;
            case AudioType.NewDocument:
                m_NewDocumentAudio.Stop();
                break;
            case AudioType.ChangePageAddressBook:
                m_ChangePageAddressBookAudio.Stop();
                break;
            case AudioType.CallTaxi:
                m_CallTaxiAudio.Stop();
                break;
            case AudioType.Text:
                m_TextAudio.Stop();
                break;
            case AudioType.LoadingTransition:
                m_LoadingTransitionAudio.Stop();
                break;
            case AudioType.PhoneRing:
                m_PhoneRingAudio.Stop();
                break;
            case AudioType.Whisky:
                m_WhiskyAudio.Stop();
                break;
            case AudioType.NewLog:
                m_NewLogAudio.Stop();
                break;
            default:
                break;
        }
    }

    public void ChangeMusic(int idx)
    {
        m_MusicSource.Stop();
        switch (idx)
        {
            case 0:
                m_MusicSource.clip = m_MainMenuMusic;
                m_MusicSource.Play();
                break;
            case 1:
                m_MusicSource.clip = m_StreetAmbiance;
                m_MusicSource.Play();
                break;
            case 2:
                m_MusicSource.clip = m_BarAmbiance;
                m_MusicSource.Play();
                break;
            case 3:
                m_MusicSource.clip = m_CarAmbiance;
                m_MusicSource.Play();
                break;
            default:
                break;
        }
    }

    public void StopMusic()
    {
        m_MusicSource.Stop();
    }

    public void ToggleRadio()
    {
        b_IsRadioActive = !b_IsRadioActive;
        if (b_IsRadioActive)
        {
            m_RadioAudio.volume = 0.4f;
        }
        else
        {
            m_RadioAudio.volume = 0f;
        }
    }

    public void ChangeRadioChannel()
    {
        if (b_IsRadioActive)
        {
            if (i_ChannelIdx < m_RadioClips.Count - 1)
            {
                i_ChannelIdx++;
            }
            else
            {
                i_ChannelIdx = 0;
            }
            m_RadioAudio.Stop();
            m_RadioTransitionAudio.Play();
            m_RadioAudio.clip = m_RadioClips[i_ChannelIdx];
            Invoke("LaunchRadioChannel", 1.412f);
        }
    }

    void LaunchRadioChannel()
    {
        m_RadioAudio.time = Random.Range(0f,m_RadioAudio.clip.length);
        m_RadioAudio.Play();
    }

    public void StopRadio()
    {
        m_RadioAudio.volume = 0f;
    }

    public void DeskCheckRadio()
    {
        if (b_IsRadioActive)
        {
            m_RadioAudio.volume = 0.4f;
        }
    }

    public void StopDefinitiveRadio()
    {
        m_RadioAudio.volume = 0f;
    }

    public void ToggleStartRadio()
    {
        m_RadioAudio.Play();
        b_IsRadioActive = !b_IsRadioActive;
        if (b_IsRadioActive)
        {
            m_RadioAudio.volume = 0.4f;
        }
        else
        {
            m_RadioAudio.volume = 0f;
        }
    }
}
