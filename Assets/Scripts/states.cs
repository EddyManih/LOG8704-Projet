using UnityEngine;
using System;

enum RCRState {
    DetectHandPoseOnChest = 0,
    DetectHandsGesture = 1,
    Compressions = 2,
}

enum AEDState {
    AEDSnappedInSocket = 0,
}


[Serializable]
public class StateGameObjects
{
    public GameObject[] m_stateGameObjects;
}