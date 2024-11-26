using TMPro;
using UnityEngine;

public class RCRManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_handPlacementValidText, m_RCRStateText;
    [SerializeField] StateGameObjects[] m_stateGameObjects;
    [SerializeField] AidOptions options;
    RCRState m_state;
    AudioSource m_audioSource;

    public static RCRManager Instance {get; private set;}

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
        m_state = RCRState.ContactEmergency;
        m_RCRStateText.text = "State: ContactEmergency";
        m_audioSource = GetComponent<AudioSource>();

        for (int i = m_stateGameObjects.Length - 1; i >= 0; i--) {
            RCRState state = (RCRState) i;
            ToggleStateGameObjects(state, state.Equals(m_state));
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_state) {
            case RCRState.ContactEmergency:
                if (ContactEmergencyManager.Instance.ContactedEmergency()) {
                    SwitchState(RCRState.BreathingCheck, "BreathingCheck");
                }
                break;

            case RCRState.BreathingCheck:
                if (BreathingManager.Instance.CheckedBreathing()) {
                    SwitchState(RCRState.DetectHandPoseOnChest, "DetectHandPoseOnChest");
                }
                break;

            case RCRState.DetectHandPoseOnChest:
                if (RCRGestureManager.Instance.HandPoseOnChestValid()) {
                    m_handPlacementValidText.text = "Hand placement: Valid";
                    SwitchState(RCRState.DetectHandsGesture, "DetectHandsGesture");
                }
                m_handPlacementValidText.text = "Hand placement: Invalid";
                break;

            case RCRState.DetectHandsGesture:
                if (RCRGestureManager.Instance.readyToStartCompressions()) {
                    SwitchState(RCRState.Compressions, "Compressions");
                }
                else if (!RCRGestureManager.Instance.handOnChest) {
                    m_RCRStateText.text = "Please keep your hand on the chest";
                }
                else if (!RCRGestureManager.Instance.handOnHand) {
                    m_RCRStateText.text = "Please place your other hand on top of your favored hand";
                }
                break;

            case RCRState.Compressions:
            if (RCRCompressionManager.Instance.nValidCompressions() >= 10) {
                SwitchState(RCRState.End, "End");
            }
                break;
        }

        m_handPlacementValidText.text = RCRGestureManager.Instance.HandPoseOnChestValid() ? "Hand placement: Valid": "Hand placement: Invalid";
    }

    private void ToggleStateGameObjects(RCRState state, bool isActive) {
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

    private void SwitchState(RCRState newState, string DebugString = "") {
        ToggleStateGameObjects(m_state, false);
    
        m_state = newState;
        m_RCRStateText.text = $"State: {DebugString}";
    
        ToggleStateGameObjects(m_state, true);
    }
}
