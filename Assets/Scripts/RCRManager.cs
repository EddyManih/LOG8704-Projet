using TMPro;
using UnityEngine;

public class RCRManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_text;
    bool m_LeftHandPoseActive, m_RightHandPoseActive;

    bool m_HandPlacementRCRValid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_LeftHandPoseActive = false;
        m_RightHandPoseActive = false;
        m_HandPlacementRCRValid = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_LeftHandPoseActive && !m_RightHandPoseActive)
        {
            m_HandPlacementRCRValid = false;
        }
    }


    public void LeftHandRCRGesture(bool poseActive)
    {
        m_LeftHandPoseActive = poseActive;
        m_text.text = m_LeftHandPoseActive ? "Left Hand Pose" : "ended";
    }

    public void RightHandRCRGesture(bool poseActive)
    {
        m_RightHandPoseActive = poseActive;
        m_text.text = m_RightHandPoseActive ? "Right Hand Pose" : "ended";
    }
}
