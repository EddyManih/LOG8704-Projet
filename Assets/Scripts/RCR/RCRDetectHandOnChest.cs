using UnityEngine;

public class RCRDetectHandOnChest : MonoBehaviour
{
    [SerializeField] RCRManager m_RCRManager;

    void OnTriggerEnter() {
        m_RCRManager.IsHandOnChest(true);
    }

    void OnTriggerExit() {
        m_RCRManager.IsHandOnChest(false);
    }
}
