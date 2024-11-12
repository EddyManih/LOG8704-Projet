using UnityEngine;

public class CompressionReachedValidDepth : MonoBehaviour
{
    [SerializeField] RCRCompressionManager m_RCRCompressionManager;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) m_RCRCompressionManager.CompressionReachedValidDepth();
    }
}
