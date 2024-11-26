using UnityEngine;
using System.Collections;

public class BreathingManager : MonoBehaviour
{
    [SerializeField] float m_CheckBreathingTimerThreshold = 3.0f;
    [SerializeField] AudioClip m_beepClip, m_doubleBeepClip;


    bool m_checkedBreathing, m_checkingAbdomen, m_checkingHead, m_startedCoroutine; 
    float m_timer;
    float m_playSoundInterval;
    AudioSource m_audioSource;

    public static BreathingManager Instance {get; private set;}

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
        m_audioSource.clip = m_beepClip;
        m_checkedBreathing = false;
        m_checkingAbdomen = false;
        m_checkingHead = false;
        m_startedCoroutine = false;
        m_timer = 0.0f;
        m_playSoundInterval = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_checkingAbdomen && m_checkingHead) {
            m_timer += Time.deltaTime;

            if (m_timer > m_playSoundInterval && m_playSoundInterval != m_CheckBreathingTimerThreshold) {
                m_audioSource.Play(0);
                m_playSoundInterval++;
            }

            if (m_timer > m_CheckBreathingTimerThreshold && !m_startedCoroutine) {
                m_audioSource.clip = m_doubleBeepClip;
                m_audioSource.Play(0);
                m_startedCoroutine = true;
                StartCoroutine(WaitForAudio());
            }
        }
        else if (!m_startedCoroutine) {
            m_timer = 0.0f;
            m_playSoundInterval = 1.0f;
        }
    }

    public bool CheckedBreathing() {
        return m_checkedBreathing;
    }

    public void CheckingAbdomen(bool status) {
        m_checkingAbdomen = status;
    }

    public void CheckingHead(bool status) {
        m_checkingHead = status;
    }

    private IEnumerator WaitForAudio()
    {
        while (m_audioSource.isPlaying)
        {
            yield return null;
        }

        m_checkedBreathing = true;
    }
}
