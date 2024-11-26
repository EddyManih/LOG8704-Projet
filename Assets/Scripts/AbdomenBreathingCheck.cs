using UnityEngine;

public class AbdomenBreathingCheck : MonoBehaviour
{
    // This works if only one hand at a time enters the AbdomenBreathingCheck Collider. If you enter with both hands and exit with one, the CheckingAbdomen
    // status will become false.
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PalmCollider")) BreathingManager.Instance.CheckingAbdomen(true);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("PalmCollider")) BreathingManager.Instance.CheckingAbdomen(false);
    }
}
