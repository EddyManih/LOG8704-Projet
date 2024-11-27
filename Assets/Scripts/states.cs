using UnityEngine;
using System;
using TMPro;

enum RCRState {
    ContactEmergency = 0,
    BreathingCheck = 1,
    DetectHandPoseOnChest = 2,
    DetectHandsGesture = 3,
    Compressions = 4,
    End = 5,
}

enum AEDState {
    ContactEmergency = 0,
    BreathingCheck = 1,
    DetectAEDInSocket = 2,
    PressAEDOnButton = 3,
    DetectPadsPlacement = 4,
    AEDAnalysis = 5,
    AdministerShock = 6,
    End = 7,
}

[Serializable]
public class StateGameObjects
{
    public GameObject[] m_stateGameObjects;
    public GameObject[] m_stateUiInstructions;
    public TextMeshProUGUI m_stepText;
    public AudioClip m_stateAudioClip;
}