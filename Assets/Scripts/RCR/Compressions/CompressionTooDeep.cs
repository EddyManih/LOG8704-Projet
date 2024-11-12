using UnityEngine;

public class CompressionTooDeep : MonoBehaviour
{
    [SerializeField] RCRCompressionManager m_RCRCompressionManager;
    void OnTriggerEnter(Collider other) {
        Debug.Log("Compression too deep");
        if (other.gameObject.layer == 4) m_RCRCompressionManager.compressionTooDeep();
    }
}
