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

        for (int i = m_stateGameObjects.Length - 1; i >= 0; i--) {
            AEDState state = (AEDState) i;
            if (m_stateGameObjects[i].m_stepText && !m_state.Equals(state)) m_stateGameObjects[i].m_stepText.alpha = 0.15f;
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
                Invoke("AEDAnalysis", 3.0f);
                break;

            case AEDState.AdministerShock:
                if (AEDDeviceManager.Instance.ApplyingShock()) {
                    ApplyShock();
                    SwitchState(AEDState.End, "End");
                }
                break;

            case AEDState.End:
                m_AEDDeviceText.text = "Patient's heart is stable";
                break;
        }
    }

    private void AEDAnalysis() {
        SwitchState(AEDState.AdministerShock, "AdministerShock");
    }

    private void ApplyShock() {
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
    
        TextMeshProUGUI previousStateText = m_stateGameObjects[(int) m_state].m_stepText;
        TextMeshProUGUI currentStateText = m_stateGameObjects[(int) newState].m_stepText;
        if (previousStateText) {
            previousStateText.alpha = 0.15f;
            previousStateText.text = "<s>" + previousStateText.text;
        }
        if (currentStateText) currentStateText.alpha = 1;
        m_state = newState;
        m_AEDStateText.text = $"State: {DebugString}";
    
        ToggleStateGameObjects(m_state, true);
    }
}
