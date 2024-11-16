using UnityEngine;

public class DetectSuperposedHands : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        Debug.Log("hands superposed");
        if (other.CompareTag("HandsSuperposed")) RCRGestureManager.Instance.DetectHandOnHand(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("HandsSuperposed")) RCRGestureManager.Instance.DetectHandOnHand(false);
    }
}
