using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [Flags]
    private enum Axis
    {
        None = 0,
        X = 1,
        Y = 2,
        Z = 4,

        All = X | Y | Z,
    }

    [SerializeField] private Axis _axis;

    private void Update()
    {
        Transform goal = CameraManager.Instance.camera.transform;
        Vector3 goalPosition = Vector3.zero;

        switch (_axis)
        {
            case Axis.X:
                goalPosition.x = goal.position.x;
                break;

            case Axis.Y:
                goalPosition.x = goal.position.y;
                break;

            case Axis.Z:
                goalPosition.x = goal.position.z;
                break;

            case Axis.All:
                goalPosition = goal.position;
                break;
        }

        transform.LookAt(goalPosition, Vector3.up);
    }
}
