using UnityEngine;

public class RCRDetectHandOnChest : MonoBehaviour
{
    [SerializeField] RCRManager m_RCRManager;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) m_RCRManager.IsHandOnChest(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("PalmCollider")) m_RCRManager.IsHandOnChest(false);
    }
}
