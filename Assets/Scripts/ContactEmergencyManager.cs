using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContactEmergencyManager : MonoBehaviour
{
    [SerializeField] Text m_dialText;
    [SerializeField] AudioClip m_emergencyAudio, m_wrongNumberAudio;
    AudioSource m_audioSource;
    bool m_contactedEmergency;
    bool m_dialing;


    public static ContactEmergencyManager Instance {get; private set;}

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_dialText.text = "";
        m_contactedEmergency = false;
    }

    public bool ContactedEmergency() {
        return m_contactedEmergency;
    }

    public void PressNumber(string number) {
        if (!m_dialing) {
            m_dialText.text = $"{m_dialText.text}{number}";
        }
    }

    public void PressDial() {
        if (!m_dialing) {
            m_dialing = true;
            
            if (m_dialText.text.Equals("911")) {
                m_audioSource.clip = m_emergencyAudio;
            } else {
                m_audioSource.clip = m_wrongNumberAudio;
            }

            m_audioSource.Play(0);
            StartCoroutine(WaitForAudio());
        }
    }

    public void EraseNumber() {
        if (!m_dialText.text.Equals("") && !m_dialing) {
            m_dialText.text = m_dialText.text[..^1];
        }
    }

    private IEnumerator WaitForAudio()
    {
        while (m_audioSource.isPlaying)
        {
            yield return null;
        }

        if (m_dialText.text.Equals("911")) {
            m_contactedEmergency = true;
        }

        m_dialText.text = "";
        m_dialing = false;
    }
}
