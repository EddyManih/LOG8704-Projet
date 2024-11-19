using UnityEngine;

public class SocketManager : MonoBehaviour
{
    bool m_socketActive;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_socketActive = false;
    }

    public void SelectEntered() {
        m_socketActive = true;
    }

    public void SelectExited() {
        m_socketActive = false;
    }

    public bool SocketActive() {
        return m_socketActive;
    }
}
