using UnityEngine;

public class CompressionStarted : MonoBehaviour
{
    [SerializeField] RCRCompressionManager m_RCRCompressionManager;
    void OnTriggerEnter(Collider other) {
        Debug.Log("Compression started");
        if (other.gameObject.layer == 4) m_RCRCompressionManager.CompressionStarted(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == 4) m_RCRCompressionManager.CompressionStarted(false);
    }
}
