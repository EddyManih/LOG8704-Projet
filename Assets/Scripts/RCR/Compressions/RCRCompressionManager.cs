using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RCRCompressionManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_compressionStatusText, m_compressionBPMText;

    bool m_compressionReachedValidDepth;
    bool m_compressionTooDeep;

    float m_timer;
    List<float> m_compressionTimes;
    int m_compressionBPM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_compressionReachedValidDepth = false;
        m_compressionTooDeep = false;

        m_timer = 0.0f;
        m_compressionTimes = new List<float>();
        m_compressionBPM = 0;
    }

    void Update() {
        m_timer += Time.deltaTime;
    }

    public void CompressionStarted(bool compressionStatus) {
        // Compression terminée
        if (!compressionStatus) {
            m_compressionTimes.Add(m_timer);
            // Compression invalidée
            if (m_compressionTooDeep) {
                m_compressionStatusText.text = "Comp: Deep!";
            } 
            // Compression valide
            else if (m_compressionReachedValidDepth) {
                m_compressionStatusText.text = "Comp: Good!";
            }
            // Compression pas assez profonde
            else {
                m_compressionStatusText.text = "Comp: Shallow!";
            }
        }

        // Nouvelle compression commencée
        if (compressionStatus) {
            if (m_compressionTimes.Count == 2) {
                // 1 compression/deltaT s * 60s/min => compression/min
                m_compressionBPM = (int) (60.0f / (m_compressionTimes[1] - m_compressionTimes[0]));

                m_timer = 0;
                m_compressionTimes.Clear();

                m_compressionBPMText.text = "BPM: " + m_compressionBPM.ToString();
            }

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
