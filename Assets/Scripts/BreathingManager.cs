using UnityEngine;

public class BreathingManager : MonoBehaviour
{
    [SerializeField] float m_CheckBeathingTimerThreshold;
    bool m_checkedBreathing, m_checkingAbdomen, m_checkingHead; 
    float m_timer;
    public static BreathingManager Instance {get; private set;}

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
        m_checkedBreathing = false;
        m_checkingAbdomen = false;
        m_checkingHead = false;
        m_timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_checkingAbdomen && m_checkingHead) {
            Debug.Log(m_timer);
            m_timer += Time.deltaTime;

            if (m_timer > m_CheckBeathingTimerThreshold) {
                m_checkedBreathing = true;
            }
        }
        else {
            m_timer = 0.0f;
        }
    }

    public bool CheckedBreathing() {
        return m_checkedBreathing;
    }

    public void CheckingAbdomen(bool status) {
        Debug.Log("CheckingAbdomen" + status);
        m_checkingAbdomen = status;
    }

    public void CheckingHead(bool status) {
        Debug.Log("CheckingHead" + status);
        m_checkingHead = status;
    }
}
