using TMPro;
using UnityEngine;


enum RCRState {
    DetectHandPoseOnChest = 0,
    DetectHandsGesture = 1,
    Compressions = 2,
}

public class RCRManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_handPlacementValidText, m_RCRStateText;
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
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_state) {
            case RCRState.DetectHandPoseOnChest:
                if (RCRGestureManager.Instance.HandPoseOnChestValid()) {
                    m_handPlacementValidText.text = "Hand placement: Valid";
                    SwitchState(RCRState.DetectHandsGesture, "DetectHandsGesture");
                    return;
                }
                m_handPlacementValidText.text = "Hand placement: Invalid";
                break;

            case RCRState.DetectHandsGesture:
                break;

            case RCRState.Compressions:
                break;
        }

        m_handPlacementValidText.text = RCRGestureManager.Instance.HandPoseOnChestValid() ? "Hand placement: Valid": "Hand placement: Invalid";
    }

    private void SwitchState(RCRState state, string DebugString = "") {
        m_state = state;
        m_RCRStateText.text = $"State: {DebugString}";
    }
}
