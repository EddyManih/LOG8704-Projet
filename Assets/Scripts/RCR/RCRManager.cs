using TMPro;
using System;
using UnityEngine;


enum RCRState {
    DetectHandPoseOnChest = 0,
    DetectHandsGesture = 1,
    Compressions = 2,
}

[System.Serializable]
public class StateGameObjects
{
    public GameObject[] m_stateGameObjects;
}

public class RCRManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_handPlacementValidText, m_RCRStateText;
    [SerializeField] StateGameObjects[] m_stateGameObjects;
    RCRState m_state;

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
        m_state = RCRState.DetectHandPoseOnChest;
        m_RCRStateText.text = "State: DetectHandPoseOnChest";

        for (int i = m_stateGameObjects.Length - 1; i >= 0; i--) {
            RCRState state = (RCRState) i;
            ToggleStateGameObjects(state, state.Equals(m_state));
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_state) {
            case RCRState.DetectHandPoseOnChest:
                if (RCRGestureManager.Instance.HandPoseOnChestValid()) {
                    m_handPlacementValidText.text = "Hand placement: Valid";
                    //SwitchState(RCRState.Compressions, "Compressions");
                    m_state = RCRState.DetectHandsGesture;
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
                break;
        }

        m_handPlacementValidText.text = RCRGestureManager.Instance.HandPoseOnChestValid() ? "Hand placement: Valid": "Hand placement: Invalid";
    }

    private void ToggleStateGameObjects(RCRState state, bool isActive) {
        GameObject[] stateGameObjects = m_stateGameObjects[(int) state].m_stateGameObjects;

        for (int i = 0; i < stateGameObjects.Length; i++) {
            stateGameObjects[i].SetActive(isActive);
        }
    }

    private void SwitchState(RCRState newState, string DebugString = "") {
        ToggleStateGameObjects(m_state, false);
    
        m_state = newState;
        m_RCRStateText.text = $"State: {DebugString}";
    
        ToggleStateGameObjects(m_state, true);
    }
}
