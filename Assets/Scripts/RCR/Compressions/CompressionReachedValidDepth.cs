using UnityEngine;

public class CompressionReachedValidDepth : MonoBehaviour
{
    [SerializeField] RCRCompressionManager m_RCRCompressionManager;
    void OnTriggerEnter(Collider other) {
        Debug.Log("Compression reached good depth");
        if (other.tag == "PalmCollider") m_RCRCompressionManager.CompressionReachedValidDepth();
    }
}
