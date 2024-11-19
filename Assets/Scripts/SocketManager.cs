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
        m_socketActive = true;
        m_interactableGameObject.GetComponent<XRGrabInteractable>().enabled = false;
        GetComponent<XRSocketInteractor>().enabled = false;
    }

    public bool SocketActive() {
        return m_socketActive;
    }
}
