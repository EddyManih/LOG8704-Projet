using UnityEngine;

public class CompressionStarted : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) RCRCompressionManager.Instance.CompressionStarted(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("PalmCollider")) RCRCompressionManager.Instance.CompressionStarted(false);
    }
}
