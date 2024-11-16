using TMPro;
using UnityEngine;


enum RCRState {
    DetectHandPose = 0,
    DetectHandOnChest = 1,
    DetectHandsGesture = 2,
    Compressions = 3,
}

public class RCRManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_HandPlacementValidText;
    bool m_HandPlacementRCRValid;

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
        m_HandPlacementRCRValid = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_HandPlacementValidText.text = m_HandPlacementRCRValid ? "Hand placement: Valid": "Hand placement: Invalid";
    }

    public void HandPlacementRCRValid(bool ValidPlacement) {
        m_HandPlacementRCRValid = ValidPlacement;
    }
}
