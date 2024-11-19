using UnityEngine;
using System;

enum RCRState {
    DetectHandPoseOnChest = 0,
    DetectHandsGesture = 1,
    Compressions = 2,
}

enum AEDState {
    DetectAEDInSocket = 0,
    DetectPadsPlacement = 1,
    AEDAnalysis = 2,
    AdministerShock = 3,
    End = 4,
}

[Serializable]
public class StateGameObjects
{
    public GameObject[] m_stateGameObjects;
}