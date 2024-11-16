using UnityEngine;

public class CompressionTooDeep : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) RCRCompressionManager.Instance.compressionTooDeep();
    }
}
