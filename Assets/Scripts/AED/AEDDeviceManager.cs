using UnityEngine;

public class AEDDeviceManager : MonoBehaviour
{
    [SerializeField] SocketManager m_AEDSocket, m_leftPadSocket, m_rightPadSocket;
    [SerializeField] GameObject m_leftPad, m_rightPad;
    bool m_applyingShock, m_deviceOn;
    
    public static AEDDeviceManager Instance {get; private set;}

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void SetActivePads() {
        m_leftPad.SetActive(true);
        m_rightPad.SetActive(true);
    }

    public bool DeviceRetrieved() {
        return m_AEDSocket.SocketActive();
    }

    public void DeviceTurnedOn() {
        m_deviceOn = true;
    }

    public bool DeviceIsOn() {
        return m_deviceOn;
    }

    public bool PadPlaced(bool isLeft) {
        if (isLeft) {
            return m_leftPadSocket.SocketActive();
        }
        return m_rightPadSocket.SocketActive();
    }

    public void ShockButtonPressed() {
        m_applyingShock = true;
    }

    public void ShockApplied() {
        m_applyingShock = false;
    }

    public bool ApplyingShock() {
        return m_applyingShock;
    }
}
