using UnityEngine;
using System;

enum RCRState {
    ContactEmergency = 0,
    DetectHandPoseOnChest = 1,
    DetectHandsGesture = 2,
    Compressions = 3,
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
}