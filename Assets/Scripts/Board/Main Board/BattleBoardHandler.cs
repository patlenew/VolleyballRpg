using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BattleBoardHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoardTile _tileRef;
    [SerializeField] private BoardGrid _boardRef;
    [SerializeField] private TurnHandler _turnHandler;

    // Could be part of team settings, to see
    [Header("Base Game Settings")]
    [SerializeField] private int _goalScore = 20;

    [Header("Board Settings")]
    [SerializeField] private Transform _localBoardHelper;
    [SerializeField] private Transform _enemyBoardHelper;

    [Header("Ball Settings")]
    [SerializeField] private Transform _ballNetGoal;
    [SerializeField] private Transform _ball;

    private int _playerScore;
    private int _enemyScore;
    private BoardGrid _playerGrid;
    private BoardGrid _enemyGrid;
    private BoardTile _currentBallTileGoal;
    private BattleSettings _battleSettings;

    private void Start()
    {
        GameEvents.OnStartFight.Register(OnStartBattle);
        GameEvents.OnUseCard.Register(OnUseCard);
        GameEvents.OnSelectCharacter.Register(OnSelectCharacter);
    }

    private void OnDestroy()
    {
        GameEvents.OnStartFight.Remove(OnStartBattle);
        GameEvents.OnUseCard.Remove(OnUseCard);
        GameEvents.OnSelectCharacter.Remove(OnSelectCharacter);
    }

    private void OnStartBattle(BattleSettings battleSettings)
    {
        // Prepare Boards
        // Setup Characters
        // Make player (local team) start for now
        // Turn takes current team data, and add/removes stats according to modifiers

        _playerScore = 0;
        _enemyScore = 0;

        PrepareBoards(battleSettings);

        GameEvents.SpriteHUD_Show.Invoke(typeof(HUD_DeckPanel));
        GameEvents.PrepareDeck.Invoke(battleSettings.Local.GetTeam().GetDeckData());

        // Prepare Turn
        _turnHandler.PrepareFirstTurn();

        PrepareTurn();
    }

    #region Prepare Board

    private void PrepareBoards(BattleSettings battleSettings)
    {
        CleanBoard();

        _battleSettings = battleSettings;

        transform.position = _battleSettings.battlePoint;

        InitBoardForLocal(_battleSettings.localBoardSettings, _battleSettings.LocalTeam);
        InitBoardForEnemy(_battleSettings.enemyBoardSettings, _battleSettings.EnemyTeam);

        _battleSettings.Local.Hide();
        _battleSettings.Enemy.Hide();
    }

    public void InitBoardForLocal(BoardSettings settings, BoardTeam team)
    {
        _playerGrid = CreateGrid(_localBoardHelper, settings);
        _playerGrid.SetTeam(team);
    }

    public void InitBoardForEnemy(BoardSettings settings, BoardTeam team)
    {
        _enemyGrid = CreateGrid(_enemyBoardHelper, settings);
        _enemyGrid.SetTeam(team);
    }

    private void CleanBoard()
    {
        if (_playerGrid == null || _enemyGrid == null) return;

        _playerGrid.Clean();
        _enemyGrid.Clean();

        Destroy(_playerGrid.gameObject);
        Destroy(_enemyGrid.gameObject);

        _playerGrid = null;
        _enemyGrid = null;
    }

    #endregion

    #region Turn Related

    private void PrepareTurn()
    {
        MoveBallToMiddle();

        // Transition & Start Next Turn
        _turnHandler.PrepareTurn(GetBaseReflex(), GetStartTurnAction(), GetEndTurnAction());

        TransitionToNextTurn(GetCameraHelper());
    }

    private void TransitionToNextTurn(Transform cameraHelper)
    {
        CameraManager.Instance.MoveTo(cameraHelper, 1f, OnTransitionToTeamComplete);
    }

    private void OnTransitionToTeamComplete()
    {
        StartTurn();
    }

    private void StartTurn()
    {
        _turnHandler.StartTurn();
    }

    private void OnStartTurnLocal()
    {
        SetBallGoal(_playerGrid);

        GameEvents.OnStartTurn_Local.Invoke();
    }

    private void OnStartTurnEnemy()
    {
        // Allo!!!
        SetBallGoal(_enemyGrid);
        Debug.Log("Ses ma tour");

        StartCoroutine(DelayTestEnemyTurn());
    }

    private IEnumerator DelayTestEnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("Ses fini ma tour");
        OnEndTurn();
    }

    #region Battle Flow Related

    // TO ADD: Cancel Move
    // -- Other move types
    // Make Possible to look at board, to plan next moves
    // Ponder about use of command pattern to be able to rewind moves

    private void OnUseCard(CardData cardData)
    {
        CameraManager.Instance.MoveTo(GetPlayCardCameraHelper(), 1f);

        _turnHandler.OnUseCard(cardData.GetReflexCost());

        switch (cardData.type)
        {
            case CardType.Movement:
                EnableCharacterSelection(_playerGrid);
                break;

            case CardType.Stats_Buff:
                BuffStats(cardData);
                break;
        }
    }

    private void EnableCharacterSelection(BoardGrid grid)
    {
        BoardMouseHandler.Instance.SetSelecting(true);

        grid.SetCharactersSelectable(true);
    }

    private void OnSelectCharacter(BoardCharacter character)
    {
        // Show Possibilities related to card used
    }

    private void BuffStats(CardData cardData)
    {

    }

    #endregion

    // After all reflex played (or end turn, the ball falls on the target chosen (either randomly or by the player if he played such card))
    // After we end the turn
    private void FallBall()
    {
        if (_currentBallTileGoal.IsEmpty())
        {
            // Current player loses points
            
        }
        else
        {
            BoardCharacter character = _currentBallTileGoal.GetCharacter();
            character.CatchBall(_ball, OnEndTurn);
        }
    }

    private void OnCompleteFallBall()
    {
        if (_turnHandler.GetCurrentTurnOwner() == TurnOwner.Local)
        {
            _enemyScore++;
        }
        else
        {
            _playerScore++;
        }

        // Maybe split the score into their own events, to validate
        GameEvents.OnScoreUpdate.Invoke(_playerScore, _enemyScore);
    }

    private void OnCheckScore()
    {
        if (_playerScore >= _goalScore)
        {
            Debug.Log("Player Wins!");

            OnCompleteBattle();
        }
        else if (_enemyScore >= _goalScore)
        {
            Debug.Log("Enemy Wins!");

            OnCompleteBattle();
        }
    }

    private void OnEndTurn()
    {
        _turnHandler.SetNextTurn();

        PrepareTurn();
    }

    #endregion

    #region Ball

    private void MoveBallToMiddle()
    {
        _ball.DOMove(_ballNetGoal.position, 1f);
    }

    private void SetBallGoal(BoardGrid grid)
    {
        _currentBallTileGoal = grid.GetRandomTileForBall();
    }

    #endregion

    // TODO: Clean up boards, enable moving again
    private void OnCompleteBattle()
    {
        _battleSettings.Local.Show();
    }

    #region Helpers

    private Action GetStartTurnAction()
    {
        Action action = null;

        if (_turnHandler.GetCurrentTurnOwner() == TurnOwner.Local)
        {
            action = OnStartTurnLocal;
        }
        else
        {
            action = OnStartTurnEnemy;
        }

        return action;
    }

    private Action GetEndTurnAction()
    {
        Action action = null;

        if (_turnHandler.GetCurrentTurnOwner() == TurnOwner.Local)
        {
            action = OnEndTurn;
        }
        else
        {
            action = OnEndTurn;
        }

        return action;
    }

    private BoardGrid CreateGrid(Transform parent, BoardSettings settings)
    {
        BoardGrid board = Instantiate(_boardRef, parent);
        board.transform.localPosition = Vector3.zero;

        board.SetSettings(settings);
        board.SpawnTiles();

        return board;
    }

    private Transform GetCameraHelper()
    {
        return _turnHandler.GetCurrentTurnOwner() == TurnOwner.Local ? _playerGrid.GetBaseCameraHelper() : _enemyGrid.GetBaseCameraHelper();
    }

    private Transform GetPlayCardCameraHelper()
    {
        return _turnHandler.GetCurrentTurnOwner() == TurnOwner.Local ? _playerGrid.GetPlayedCardCameraHelper() : _enemyGrid.GetPlayedCardCameraHelper();
    }

    private int GetBaseReflex()
    {
        return _turnHandler.GetCurrentTurnOwner() == TurnOwner.Local ? _playerGrid.GetTeamData().baseReflex : _enemyGrid.GetTeamData().baseReflex;
    }

    #endregion
}

public class BattleSettings
{
    public Vector3 battlePoint;

    public MainPlayer Local { private set; get; }
    public WorldEnemy Enemy { private set; get; }

    public BoardTeam LocalTeam { private set; get; }
    public BoardTeam EnemyTeam { private set; get; }

    public BoardSettings localBoardSettings { private set; get; }
    public BoardSettings enemyBoardSettings { private set; get; }

    public BattleSettings(MainPlayer local, WorldEnemy enemy)
    {
        Local = local;
        Enemy = enemy;

        LocalTeam = local.GetTeam();
        EnemyTeam = enemy.GetTeam();
    }

    public void SetBoardSettings(BoardSettings localSettings, BoardSettings enemySettings)
    {
        localBoardSettings = localSettings;
        enemyBoardSettings = enemySettings;
    }
}
