using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// TO PONDER: Maybe we should just clean all events at once before switching level
public static class GameEvents
{
    public static GameEvent<BattleSettings> OnStartFight = new GameEvent<BattleSettings>();
    public static GameEvent<DeckData> PrepareDeck = new GameEvent<DeckData>();
    public static GameEvent<CardData> OnUseCard = new GameEvent<CardData>();
    public static GameEvent<BoardCharacter> OnSelectCharacter = new GameEvent<BoardCharacter>();

    public static GameEvent<Type> SpriteHUD_Show = new GameEvent<Type>();
    public static GameEvent<Type> SpriteHUD_Hide = new GameEvent<Type>();

    public static GameEvent<int, int> OnScoreUpdate = new GameEvent<int, int>();

    public static GameEvent OnStartTurn_Local = new GameEvent();
    public static GameEvent OnStartTurn_Enemy = new GameEvent();
    
    public static GameEvent OnEndTurn = new GameEvent();
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
        if (_callback == null || _callback.GetInvocationList().Length == 0)
        {
            Debug.LogError("Nothing to Call");
            return;
        }

        _callback.Invoke();
    }

    public void Remove(Action callback)
    {
        if (_callback == null || !_callback.GetInvocationList().Contains(callback))
        {
            Debug.LogError("Nothing to unregister");
            return;
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
        if (_callback == null || _callback.GetInvocationList().Length == 0)
        {
            Debug.LogError("Nothing to Call");
            return;
        }


        _callback.Invoke(t);
    }

    public void Remove(Action<T> callback)
    {
        if (_callback == null || !_callback.GetInvocationList().Contains(callback))
        {
            Debug.LogError("Nothing to unregister");
            return;
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
        if (_callback == null || _callback.GetInvocationList().Length == 0)
        {
            Debug.LogError("Nothing to Call");
            return;
        }

        _callback.Invoke(t, e);
    }

    public void Remove(Action<T, E> callback)
    {
        if (_callback == null || !_callback.GetInvocationList().Contains(callback))
        {
            Debug.LogError("Nothing to unregister");
            return;
        }

        _callback -= callback;
    }
}
