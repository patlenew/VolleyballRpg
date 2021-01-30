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
    [SerializeField] private int _goalPoint = 20;

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
    private BattleSettings _battleSettings;

    private void Start()
    {
        GameEvents.OnStartFight.Register(OnStartBattle);
    }

    private void OnDestroy()
    {
        GameEvents.OnStartFight.Remove(OnStartBattle);
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

        // Prepare Turn
        _turnHandler.PrepareTurns();
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

    private void OnEndTurn()
    {

    }

    #endregion

    #region Ball

    private void MoveBallToMiddle()
    {
        _ball.DOMove(_ballNetGoal.position, 1f);
    }

    private void SetBallGoal(BoardGrid grid)
    {
        BoardTile goalTile = grid.GetRandomTileForBall();
    }

    #endregion


    private void OnCompleteBattle()
    {
        _battleSettings.Local.Show();
    }

    #region Helpers

    private BoardGrid CreateGrid(Transform parent, BoardSettings settings)
    {
        BoardGrid board = Instantiate(_boardRef, parent);
        board.transform.localPosition = Vector3.zero;

        board.SetSettings(settings);
        board.SpawnTiles();

        return board;
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
