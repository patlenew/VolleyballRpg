using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO VALIDATE: should this be a mono beheavior?
public class TurnHandler : MonoBehaviour
{
    private Turn _localTurn;
    private Turn _enemyTurn;

    private Turn _currentTurn;

    private void Start()
    {

    }

    private void OnDestroy()
    {

    }

    public void PrepareTurns()
    {

    }

    private void StartCameraTransition()
    {
        CameraManager.Instance.MoveTo(_currentTurn.CameraHelper.position, 1f);
    }

    public void SetNextTurn()
    {
        bool playerTurn = _currentTurn.IsPlayerTurn();

        _currentTurn = playerTurn ? _enemyTurn : _localTurn;
    }

    private void OnUseCard(int reflexChange)
    {
        _currentTurn.OnUseCard(reflexChange);
    }

    private void OnEndTurn()
    {


        GameEvents.
    }
}

public class Turn
{
    public int ReflexRemaining { private set; get; }
    public Transform CameraHelper { private set; get; }

    private bool _playerTurn;
    private Action _onEndTurn;
    private BoardTeamData _data;

    public Turn(int startReflex, bool playerTurn)
    {
        ReflexRemaining = startReflex;
        _playerTurn = playerTurn;
    }

    public void ResetTurn()
    {
        // Maybe we can have card that adds more reflex for the next turn
        ReflexRemaining += _data.baseReflex;
    }

    public void SetCameraHelper(Transform cameraHelper)
    {
        CameraHelper = cameraHelper;
    }

    public void SetEndTurnCallback(Action onEndTurn)
    {
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
        return _playerTurn;
    }
}
