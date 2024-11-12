using UnityEngine;

public class CompressionStarted : MonoBehaviour
{
    [SerializeField] RCRCompressionManager m_RCRCompressionManager;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) m_RCRCompressionManager.CompressionStarted(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("PalmCollider")) m_RCRCompressionManager.CompressionStarted(false);
    }
}
