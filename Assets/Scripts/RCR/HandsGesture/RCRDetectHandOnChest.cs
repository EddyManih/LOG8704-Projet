using UnityEngine;

public class RCRDetectHandOnChest : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) RCRGestureManager.Instance.IsHandOnChest(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("PalmCollider")) RCRGestureManager.Instance.IsHandOnChest(false);
    }
}
