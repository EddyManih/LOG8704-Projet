using UnityEngine;

public class DetectSuperposedHands : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        Debug.Log("hands superposed");
        if (other.gameObject.tag == "PalmCollider") RCRGestureManager.Instance.DetectHandOnHand(true);
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "PalmCollider") RCRGestureManager.Instance.DetectHandOnHand(false);
    }
}
