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
    DetectPadsPlacement = 2,
    AEDAnalysis = 3,
    AdministerShock = 4,
    End = 5,
}

[Serializable]
public class StateGameObjects
{
    public GameObject[] m_stateGameObjects;
}