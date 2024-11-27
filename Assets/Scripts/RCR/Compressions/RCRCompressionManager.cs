using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RCRCompressionManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_compressionStatusText, m_compressionBPMText, m_compressionNValidText;

    bool m_compressionReachedValidDepth, m_compressionTooDeep, m_ongoingCompression;

    float m_timer, m_timerBetweenCompressions;
    List<float> m_compressionTimes;
    int m_compressionBPM;
    int m_nValidCompressions;

    [SerializeField] Transform m_handTransform, m_chestTransform;
    [SerializeField] float m_maxChestOffset = 0.01f;
    float m_handVerticalPos;
    Vector3 m_chestInitialPos;

    public static RCRCompressionManager Instance {get; private set;}

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
        m_compressionReachedValidDepth = false;
        m_compressionTooDeep = false;

        m_timer = 0.0f;
        m_timerBetweenCompressions = 0.0f;
        m_compressionTimes = new List<float>();
        m_compressionBPM = 0;
        m_nValidCompressions = 0;
        m_handVerticalPos = 0.0f;
        m_chestInitialPos = m_chestTransform.position;

    }

    void Update() {
        m_timer += Time.deltaTime;

        HandleBPMCountReset();
        HandleChestMovement();
    }

    public void CompressionStarted(bool compressionStatus) {
        m_ongoingCompression = compressionStatus;

        // Compression terminée
        if (!m_ongoingCompression) {
            m_compressionTimes.Add(m_timer);
            // Compression invalidée
            if (m_compressionTooDeep) {
                m_compressionStatusText.text = "Compression trop profonde!";
            } 
            // Compression valide
            else if (m_compressionReachedValidDepth) {
                m_compressionStatusText.text = "Bonne profondeur!";
                if (m_compressionBPM >= 100 && m_compressionBPM <= 120) {
                    m_nValidCompressions++;
                    m_compressionNValidText.text = "Compressions valides: " + m_nValidCompressions.ToString();
                }
            }
            // Compression pas assez profonde
            else {
                m_compressionStatusText.text = "Compression pas assez profonde!";
            }
        }

        // Nouvelle compression commencée
        if (m_ongoingCompression) {
            m_handVerticalPos = m_handTransform.position.y;

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

    public int nValidCompressions() {
        return m_nValidCompressions;
    }

    public void CompressionReachedValidDepth() {
        m_compressionReachedValidDepth = true;
    }

    public void compressionTooDeep() {
        m_compressionTooDeep = true;
    }

    private void HandleChestMovement() {
        if (m_ongoingCompression) {
            float handOffset = Math.Abs(m_handVerticalPos - m_handTransform.position.y);

            m_chestTransform.transform.position = new Vector3(
                m_chestInitialPos.x,
                Mathf.Max(m_chestTransform.position.y - handOffset, m_chestInitialPos.y - m_maxChestOffset),
                m_chestInitialPos.z
            );
        } else {
            m_chestTransform.position = m_chestInitialPos;
        }

        m_handVerticalPos = m_handTransform.position.y;
    }

    private void HandleBPMCountReset() {
        if (!m_ongoingCompression) {
            m_timerBetweenCompressions += Time.deltaTime;

            if (m_timerBetweenCompressions > 4.0f) {
                m_compressionTimes.Clear();
                m_compressionBPM = 0;
                m_compressionBPMText.text = "BPM: " + m_compressionBPM.ToString();
            }
        } else {
            m_timerBetweenCompressions = 0.0f;
        }
    }
}
