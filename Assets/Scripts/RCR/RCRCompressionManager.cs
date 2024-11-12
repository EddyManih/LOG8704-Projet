using TMPro;
using UnityEngine;

public class RCRCompressionManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_compressionStatus, m_compressionBPM;

    bool m_compressionReachedValidDepth;
    bool m_compressionTooDeep;

    float m_timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_compressionReachedValidDepth = false;
        m_compressionTooDeep = false;
        m_timer = 0.0f;
    }

    void Update() {
        m_timer += Time.deltaTime;
    }

    public void CompressionStarted(bool compressionStatus) {
        // Compression terminée
        if (!compressionStatus) {
            // Compression invalidée
            if (m_compressionTooDeep) {
                m_compressionStatus.text = "Compression too deep!";
            } 
            // Compression valide
            else if (m_compressionReachedValidDepth) {
                m_compressionStatus.text = "Good compression!";
            }
            // Compression pas assez profonde
            else {
                m_compressionStatus.text = "Compression too shallow";
            }
        }

        // Nouvelle compression commencée
        if (compressionStatus) {
            m_compressionReachedValidDepth = false;
            m_compressionTooDeep = false;
        }
    }

    public void CompressionReachedValidDepth() {
        m_compressionReachedValidDepth = true;
    }

    public void compressionTooDeep() {
        m_compressionTooDeep = true;
    }
}
