using UnityEngine;

public class CompressionReachedValidDepth : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) RCRCompressionManager.Instance.CompressionReachedValidDepth();
    }
}
