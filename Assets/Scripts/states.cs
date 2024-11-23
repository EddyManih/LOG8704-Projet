using UnityEngine;
using System;

enum RCRState {
    DetectHandPoseOnChest = 0,
    DetectHandsGesture = 1,
    Compressions = 2,
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