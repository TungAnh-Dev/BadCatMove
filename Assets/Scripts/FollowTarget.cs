using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform followTarget;
    public float rotationalSpeed = 30f;
    public float topClamp = 70f;
    public float bottomClap = -40f;

    float cinemachineTargetYaw;
    float cinemachineTargetPitch;
}
