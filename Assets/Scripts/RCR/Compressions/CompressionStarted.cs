using UnityEngine;

public class CompressionStarted : MonoBehaviour
{
    [SerializeField] RCRCompressionManager m_RCRCompressionManager;
    void OnTriggerEnter(Collider other) {
        Debug.Log("Compression started");
        if (other.tag == "PalmCollider") m_RCRCompressionManager.CompressionStarted(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "PalmCollider") m_RCRCompressionManager.CompressionStarted(false);
    }
}
