using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// TO VALIDATE: should this be a mono beheavior?
public class TurnHandler : MonoBehaviour
{
    private TurnOwner _currentTurnOwner;
    private Turn _currentTurn;

    public void PrepareFirstTurn()
    {
        bool localStart = Random.Range(0, 2) == 1;

        _currentTurnOwner = localStart ? TurnOwner.Local : TurnOwner.Enemy;
    }

    public void PrepareTurn(int startReflex, Action onStartTurn, Action onCompleteTurn)
    {
        _currentTurn = TurnUtils.CreateTurn(startReflex, _currentTurnOwner, onStartTurn, onCompleteTurn);
    }

    public void StartTurn()
    {
        _currentTurn.OnStartTurn();
    }

    public void SetNextTurn()
    {
        bool playerTurn = _currentTurn.IsPlayerTurn();

        // Switch Turn
        _currentTurnOwner = playerTurn ? TurnOwner.Enemy : TurnOwner.Local;
    }

    public void OnUseCard(int reflexChange)
    {
        _currentTurn.OnUseCard(reflexChange);
    }

    #region Helpers

    public TurnOwner GetCurrentTurnOwner()
    {
        return _currentTurnOwner;
    }

    #endregion
}

public enum TurnOwner
{
    None = 0,

    Local = 1,
    Enemy = 2
}

public class Turn
{
    public int ReflexRemaining { private set; get; }

    private TurnOwner _owner;
    private Action _onStartTurn;
    private Action _onEndTurn;

    public Turn(int startReflex, TurnOwner owner)
    {
        ReflexRemaining = startReflex;

        _owner = owner;
    }

    public void OnStartTurn()
    {
        _onStartTurn.Invoke();
    }

    public void SetCallbacks(Action onStartTurn, Action onEndTurn)
    {
        _onStartTurn = onStartTurn;
        _onEndTurn = onEndTurn;
    }

    public void OnUseCard(int reflexChange)
    {
        ReflexRemaining += reflexChange;

        if (ReflexRemaining == 0)
        {
            _onEndTurn.Invoke();
        }
    }

    public bool IsPlayerTurn()
    {
        return _owner == TurnOwner.Local;
    }
}

public static class TurnUtils
{
    public static Turn CreateTurn(int startReflex, TurnOwner owner, Action onStartTurn, Action onCompleteTurn)
    {
        Turn turn = new Turn(startReflex, owner);
        turn.SetCallbacks(onStartTurn, onCompleteTurn);

        return turn;
    }
}