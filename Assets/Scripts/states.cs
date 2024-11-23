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
    DetectAEDInSocket = 1,
    PressAEDOnButton = 2,
    DetectPadsPlacement = 3,
    AEDAnalysis = 4,
    AdministerShock = 5,
    End = 6,
}

[Serializable]
public class StateGameObjects
{
    public GameObject[] m_stateGameObjects;
}