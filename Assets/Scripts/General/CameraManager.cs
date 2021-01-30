using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// TODO: Follow the main player in the world
public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private Transform _cameraParent;

    private Camera camera => Camera.main;

    public void MoveTo(Vector3 goal, float duration, TweenCallback onComplete = null)
    {
        _cameraParent.DOMove(goal, duration).OnComplete(onComplete ?? null);
    }
}
