using UnityEngine;
using TMPro;

public class AEDManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_AEDStateText, m_AEDPadsText, m_AEDDeviceText;
    [SerializeField] StateGameObjects[] m_stateGameObjects;
    [SerializeField] AidOptions options;
    AEDState m_state;
    AudioSource m_audioSource;

    public static AEDManager Instance {get; private set;}

    bool m_AEDAnalysing;
    int m_nShocksAdministered;

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
        m_state = AEDState.ContactEmergency;
        m_AEDStateText.text = "State: ContactEmergency";
        m_nShocksAdministered = 0;
        m_AEDAnalysing = false;

        for (int i = m_stateGameObjects.Length - 1; i >= 0; i--) {
            AEDState state = (AEDState) i;
            ToggleStateGameObjects(state, state.Equals(m_state));
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_state) {
            case AEDState.ContactEmergency:
                if (ContactEmergencyManager.Instance.ContactedEmergency()) {
                    SwitchState(AEDState.BreathingCheck, "BreathingCheck");
                }
                break;

            case AEDState.BreathingCheck:
                if (BreathingManager.Instance.CheckedBreathing()) {
                    SwitchState(AEDState.DetectAEDInSocket, "DetectAEDInSocket");
                }
                break;

            case AEDState.DetectAEDInSocket:
                if (AEDDeviceManager.Instance.DeviceRetrieved()) {
                    SwitchState(AEDState.PressAEDOnButton, "PressAEDOnButton");
                }
                break;

            case AEDState.PressAEDOnButton:
                if (AEDDeviceManager.Instance.DeviceIsOn()) {
                    SwitchState(AEDState.DetectPadsPlacement, "DetectPadsPlacement");
                    AEDDeviceManager.Instance.SetActivePads();
                }
                break;

            case AEDState.DetectPadsPlacement:
                if (AEDDeviceManager.Instance.PadPlaced(true) && AEDDeviceManager.Instance.PadPlaced(false)) {
                    SwitchState(AEDState.AEDAnalysis, "AEDAnalysis");
                }
                else if (!AEDDeviceManager.Instance.PadPlaced(true)) {
                    m_AEDPadsText.text = "Place left Pad";
                }
                else if (!AEDDeviceManager.Instance.PadPlaced(false)) {
                    m_AEDPadsText.text = "Place right Pad";
                }
                break;

            case AEDState.AEDAnalysis:
                if (m_nShocksAdministered < 3 && !m_AEDAnalysing) {
                    m_AEDAnalysing = true;
                    m_AEDDeviceText.text = "Analysing...";
                    Invoke("AEDAnalysis", 3.0f);
                }
                else if (m_nShocksAdministered == 3) {
                    SwitchState(AEDState.End, "End");
                }
                break;

            case AEDState.AdministerShock:
                if (AEDDeviceManager.Instance.ApplyingShock()) {
                    ApplyShock();
                    SwitchState(AEDState.AEDAnalysis, "AEDAnalysis");
                }
                break;

            case AEDState.End:
                m_AEDDeviceText.text = "Patient's heart is stable";
                break;
        }
    }

    private void AEDAnalysis() {
        m_AEDDeviceText.text = "Ready for Shock";
        SwitchState(AEDState.AdministerShock, "AdministerShock");
        m_AEDAnalysing = false;
    }

    private void ApplyShock() {
        m_nShocksAdministered++;
        AEDDeviceManager.Instance.ShockApplied();
    }

    private void ToggleStateGameObjects(AEDState state, bool isActive) {
        GameObject[] stateGameObjects = m_stateGameObjects[(int) state].m_stateGameObjects;
        GameObject[] uiInstructions = m_stateGameObjects[(int) state].m_stateUiInstructions;

        for (int i = 0; i < stateGameObjects.Length; i++) {
            stateGameObjects[i].SetActive(isActive);
        }

        for (int i = 0; i < uiInstructions.Length; i++) {
            uiInstructions[i].SetActive(isActive && options.visualAids);
        }

        if (isActive && options.audioAids && m_stateGameObjects[(int) state].m_stateAudioClip != null) {
            m_audioSource.clip = m_stateGameObjects[(int) state].m_stateAudioClip;
            m_audioSource.Play(0);
        }
    }

    private void SwitchState(AEDState newState, string DebugString = "") {
        ToggleStateGameObjects(m_state, false);
    
        m_state = newState;
        m_AEDStateText.text = $"State: {DebugString}";
    
        ToggleStateGameObjects(m_state, true);
    }
}
