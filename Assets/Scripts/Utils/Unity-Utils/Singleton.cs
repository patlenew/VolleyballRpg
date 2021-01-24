using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; protected set; }

    [SerializeField] private bool _dontDestroyOnLoad;

    private void Awake()
    {
        Instance = this as T;

        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        Init_Awake();
    }

    private void OnDestroy()
    {
        Instance = null;

        Deinit_OnDestroy();
    }

    protected virtual void Init_Awake()
    {

    }

    protected virtual void Deinit_OnDestroy()
    {

    }
}
