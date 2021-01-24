using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoardHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoardTile _tileRef;
    [SerializeField] private BoardGrid _boardRef;
    [SerializeField] private TurnHandler _turnHandler;

    // Could be part of team settings, to see
    [Header("Base Game Settings")]

    [Header("Board Settings")]
    [SerializeField] private Transform _localBoardHelper;
    [SerializeField] private Transform _enemyBoardHelper;

    [Header("Ball Settings")]
    [SerializeField] private Transform _ballNetGoal;

    private BoardGrid _playerGrid;
    private BoardGrid _enemyGrid;
    
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

        PrepareBoards(battleSettings);
    }

    private void PrepareBoards(BattleSettings battleSettings)
    {
        CleanBoard();

        transform.position = battleSettings.battlePoint;

        InitBoardForLocal(battleSettings.localBoardSettings, battleSettings.LocalTeam);
        InitBoardForEnemy(battleSettings.enemyBoardSettings, battleSettings.EnemyTeam);
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
        _playerGrid.Clean();
        _enemyGrid.Clean();

        Destroy(_playerGrid.gameObject);
        Destroy(_enemyGrid.gameObject);

        _playerGrid = null;
        _enemyGrid = null;
    }

    #region Helpers

    private BoardGrid CreateGrid(Transform parent, BoardSettings settings)
    {
        BoardGrid board = Instantiate(_boardRef, parent);
        board.SetSettings(settings);
        board.SpawnTiles();

        return board;
    }

    #endregion
}

public class BattleSettings
{
    public Vector3 battlePoint;

    public BoardTeam LocalTeam { private set; get; }
    public BoardTeam EnemyTeam { private set; get; }

    public BoardSettings localBoardSettings { private set; get; }
    public BoardSettings enemyBoardSettings { private set; get; }

    public BattleSettings(MainPlayer local, WorldEnemy enemy)
    {
        LocalTeam = local.GetTeam();
        EnemyTeam = enemy.GetTeam();
    }

    public void SetBoardSettings(BoardSettings localSettings, BoardSettings enemySettings)
    {
        localBoardSettings = localSettings;
        enemyBoardSettings = enemySettings;
    }
}
