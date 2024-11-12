using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

public class RCRManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_PoseText, m_HandPlacementValidText, m_HandOnChestText;
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
        m_isHandOnChest = false;

        m_leftHandCollider.enabled = true;
        m_rightHandCollider.enabled = true;
        m_leftHandGesture.enabled = true;
        m_rightHandGesture.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_HandPlacementRCRValid = m_isHandOnChest && (m_LeftHandPoseActive || m_RightHandPoseActive);
        
        m_HandOnChestText.text = m_isHandOnChest ? "Hand on chest : True" : "Hand on chest : False";
        m_HandPlacementValidText.text = m_HandPlacementRCRValid ? "Hand placement: Valid": "Hand placement: Invalid";
    }

    public void IsHandOnChest(bool isHandOnChest) {
        m_isHandOnChest = isHandOnChest;
    }

    public void LeftHandRCRGesture(bool poseActive)
    {
        m_LeftHandPoseActive = poseActive;

        if (m_LeftHandPoseActive) {
            ToggleHand(m_rightHandCollider, m_rightHandGesture, false);
        } else {
            ToggleHand(m_rightHandCollider, m_rightHandGesture, true);
        }

        m_PoseText.text = m_LeftHandPoseActive ? "Pose: Left Hand" : "Pose: None";
    }

    public void RightHandRCRGesture(bool poseActive)
    {
        m_RightHandPoseActive = poseActive;
        
        if (m_RightHandPoseActive) {
            ToggleHand(m_leftHandCollider, m_leftHandGesture, false);
        } else {
            ToggleHand(m_leftHandCollider, m_leftHandGesture, true);
        }

        m_PoseText.text = m_RightHandPoseActive ? "Pose: Right Hand" : "Pose: None";
    }

    private void ToggleHand(Collider handCollider, StaticHandGesture handGesture, bool handActive) {
        handCollider.enabled = handActive;
        handGesture.enabled = handActive;
    }
}
