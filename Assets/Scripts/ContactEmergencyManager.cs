using UnityEngine;
using UnityEngine.UI;

public class ContactEmergencyManager : MonoBehaviour
{
    [SerializeField] Text m_dialText;
    bool m_contactedEmergency;
    bool m_dialing;


    public static ContactEmergencyManager Instance {get; private set;}

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_dialText.text = "";
        m_contactedEmergency = false;
    }

    public bool ContactedEmergency() {
        return m_contactedEmergency;
    }

    public void PressNumber(string number) {
        if (!m_dialing) {
            m_dialText.text = $"{m_dialText.text}{number}";
        }
    }

    public void PressDial() {
        m_dialing = true;
        Invoke("PressDialInvoke", 2.0f);
    }

    public void EraseNumber() {
        if (!m_dialText.text.Equals("") && !m_dialing) {
            m_dialText.text = m_dialText.text[..^1];
        }
    }

    private void PressDialInvoke() {
        if (m_dialText.text.Equals("911")) {
            m_contactedEmergency = true;
        }

        m_dialText.text = "";
        m_dialing = false;
    }
}
