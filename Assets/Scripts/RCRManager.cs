using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

public class RCRManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_text;
    [SerializeField] StaticHandGesture m_leftHandGesture, m_rightHandGesture;
    [SerializeField] BoxCollider m_leftHandCollider, m_rightHandCollider;
    bool m_LeftHandPoseActive, m_RightHandPoseActive;
    bool m_isHandOnChest;

    bool m_HandPlacementRCRValid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_LeftHandPoseActive = false;
        m_RightHandPoseActive = false;
        m_HandPlacementRCRValid = false;

        m_leftHandCollider.enabled = true;
        m_rightHandCollider.enabled = true;
        m_leftHandGesture.enabled = true;
        m_rightHandGesture.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_LeftHandPoseActive && !m_RightHandPoseActive)
        {
            m_HandPlacementRCRValid = false;
        }

        if (m_isHandOnChest && (m_LeftHandPoseActive || m_RightHandPoseActive)) {
            m_HandPlacementRCRValid = true;
        }
    }

    public void IsHandOnChest(bool isHandOnChest) {
        m_isHandOnChest = isHandOnChest;
    }

    public void LeftHandRCRGesture(bool poseActive)
    {
        m_LeftHandPoseActive = poseActive;

        if (m_LeftHandPoseActive) {
            ToggleHand(m_rightHandCollider, m_rightHandGesture, false);
            m_text.text = "Left Hand Pose";
        } else {
            ToggleHand(m_rightHandCollider, m_rightHandGesture, true);
        }

        m_text.text = m_LeftHandPoseActive ? "Left Hand Pose" : "ended";
    }

    public void RightHandRCRGesture(bool poseActive)
    {
        m_RightHandPoseActive = poseActive;
        
        if (m_RightHandPoseActive) {
            ToggleHand(m_leftHandCollider, m_leftHandGesture, false);
        } else {
            ToggleHand(m_leftHandCollider, m_leftHandGesture, true);
        }
        
        m_text.text = m_RightHandPoseActive ? "Right Hand Pose" : "ended";
    }

    private void ToggleHand(Collider handCollider, StaticHandGesture handGesture, bool handActive) {
        handCollider.enabled = handActive;
        handGesture.enabled = handActive;
    }
}
