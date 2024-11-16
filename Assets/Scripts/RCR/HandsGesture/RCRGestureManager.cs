using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

public class RCRGestureManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_poseText, m_handOnChestText;
    [SerializeField] StaticHandGesture m_leftHandGesture, m_rightHandGesture;
    [SerializeField] BoxCollider m_leftHandCollider, m_rightHandCollider;


    private bool m_handPoseActive, m_handOnChest, m_handOnHand;
    public static RCRGestureManager Instance {get; private set;}

    public bool handOnChest {
        get {
            return m_handOnChest;
        }
    }

    public bool handOnHand {
        get {
            return m_handOnHand;
        }
    }

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
        m_handPoseActive = false;
        m_handOnChest = false;
        m_handOnHand = false;

        m_leftHandCollider.enabled = true;
        m_rightHandCollider.enabled = true;
        m_leftHandGesture.enabled = true;
        m_rightHandGesture.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_handOnChestText.text = m_handOnChest ? "Hand on chest : True" : "Hand on chest : False";
    }

    public bool HandPoseOnChestValid() {
        return m_handPoseActive && m_handOnChest;
    }

    public bool readyToStartCompressions() {
        return m_handOnChest && m_handOnHand;
    }

    public void DetectHandOnChest(bool handOnChestDetected) {
        m_handOnChest = handOnChestDetected;
    }

    public void DetectHandOnHand(bool handOnHandDetected) {
        m_handOnHand = handOnHandDetected;
    }

    public void RCRHandPosePerformed(bool isLeftHand) {
        m_handPoseActive = true;
    
        if (isLeftHand) {
            ToggleHand(m_rightHandCollider, m_rightHandGesture, false);
        }
        else {
            ToggleHand(m_leftHandCollider, m_leftHandGesture, false);
        }

        m_poseText.text = isLeftHand ? "Pose: Left Hand" : "Pose: Right Hand";
    }

    public void RCRHandPoseEnded() {
        m_handPoseActive = false;
        ToggleHand(m_rightHandCollider, m_rightHandGesture, true);
        ToggleHand(m_leftHandCollider, m_leftHandGesture, true);

        m_poseText.text = "Pose: None";
    }

    private void ToggleHand(Collider handCollider, StaticHandGesture handGesture, bool handActive) {
        handCollider.enabled = handActive;
        handGesture.enabled = handActive;
    }
}
