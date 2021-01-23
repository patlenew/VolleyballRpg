using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoardHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoardTile _tileRef;
    [SerializeField] private BoardGrid _boardRef;

    [Header("Board Settings")]
    [SerializeField] private Transform _localBoardHelper;
    [SerializeField] private Transform _enemyBoardHelper;

    private BoardGrid _playerGrid;
    private BoardGrid _enemyGrid;

    public void InitBoardForLocal()
    {
    }

    public void InitBoardForEnemy()
    {

    }

    private void CleanBoard()
    {

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
