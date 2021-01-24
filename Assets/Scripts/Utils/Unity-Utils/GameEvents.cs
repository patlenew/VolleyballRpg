using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameEvents
{
    public static GameEvent<BattleSettings> OnStartFight = new GameEvent<BattleSettings>();
}

public class GameEvent
{
    private Action _callback;

    public void Register(Action callback)
    {
        _callback += callback;
    }

    public void Invoke()
    {
        _callback.Invoke();
    }

    public void Remove(Action callback)
    {
        if (_callback == null || !_callback.GetInvocationList().Contains(callback))
        {
            Debug.LogError("Nothing to unregister");
        }

        _callback -= callback;
    }
}

public class GameEvent<T>
{
    private Action<T> _callback;

    public void Register(Action<T> callback)
    {
        _callback += callback;
    }

    public void Invoke(T t)
    {
        _callback.Invoke(t);
    }

    public void Remove(Action<T> callback)
    {
        if (_callback == null || !_callback.GetInvocationList().Contains(callback))
        {
            Debug.LogError("Nothing to unregister");
        }

        _callback -= callback;
    }
}

public class GameEvent<T, E>
{
    private Action<T, E> _callback;

    public void Register(Action<T, E> callback)
    {
        _callback += callback;
    }

    public void Invoke(T t, E e)
    {
        _callback.Invoke(t, e);
    }

    public void Remove(Action<T, E> callback)
    {
        if (_callback == null || !_callback.GetInvocationList().Contains(callback))
        {
            Debug.LogError("Nothing to unregister");
        }

        _callback -= callback;
    }
}
