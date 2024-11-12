using UnityEngine;

public class CompressionTooDeep : MonoBehaviour
{
    [SerializeField] RCRCompressionManager m_RCRCompressionManager;
    void OnTriggerEnter(Collider other) {
        Debug.Log("Compression too deep");
        if (other.tag == "PalmCollider") m_RCRCompressionManager.compressionTooDeep();
    }
}
