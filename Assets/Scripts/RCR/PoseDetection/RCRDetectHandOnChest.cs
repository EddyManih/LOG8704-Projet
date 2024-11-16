using UnityEngine;

public class RCRDetectHandOnChest : MonoBehaviour
{
    [SerializeField] RCRGestureManager m_RCRGestureManager;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) m_RCRGestureManager.IsHandOnChest(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("PalmCollider")) m_RCRGestureManager.IsHandOnChest(false);
    }
}
