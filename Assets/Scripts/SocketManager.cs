using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketManager : MonoBehaviour
{
    [SerializeField] GameObject m_interactableGameObject;
    bool m_socketActive;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_socketActive = false;
    }

    public void SelectEntered() {
        Invoke("DisableSocket", 0.5f);
    }

    public bool SocketActive() {
        return m_socketActive;
    }

    private void DisableSocket() {
        m_interactableGameObject.GetComponent<XRGrabInteractable>().enabled = false;
        m_interactableGameObject.GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<XRSocketInteractor>().enabled = false;
        m_socketActive = true;
    }
}
