using UnityEngine;

public class HeadBreathingCheck : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("MainCamera")) BreathingManager.Instance.CheckingHead(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("MainCamera")) BreathingManager.Instance.CheckingHead(false);
    }
}
