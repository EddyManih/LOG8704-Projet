using UnityEngine;

public class CompressionTooDeep : MonoBehaviour
{
    [SerializeField] RCRCompressionManager m_RCRCompressionManager;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) m_RCRCompressionManager.compressionTooDeep();
    }
}
