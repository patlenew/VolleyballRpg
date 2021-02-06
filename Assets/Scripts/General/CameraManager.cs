using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// TODO: Follow the main player in the world
public class CameraManager : Singleton<CameraManager>
{
    public Transform SpriteHudHelper => _spriteHudHelper;

    [SerializeField] private Transform _cameraParent;
    [SerializeField] private Transform _spriteHudHelper;

    private Camera camera => Camera.main;

    protected override void Init_Awake()
    {
        base.Init_Awake();

        camera.transform.SetParent(transform);
        camera.transform.localPosition = Vector3.zero;
        camera.transform.localRotation = Quaternion.identity;
    }

    public void MoveTo(Transform goal, float duration, TweenCallback onComplete = null)
    {
        _cameraParent.DORotateQuaternion(goal.rotation, duration);
        _cameraParent.DOMove(goal.position, duration).OnComplete(onComplete ?? null);
    }
}
