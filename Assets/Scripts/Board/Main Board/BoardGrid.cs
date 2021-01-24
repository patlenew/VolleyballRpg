using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGrid : MonoBehaviour
{
    [Header("Tiles Related")]
    [SerializeField] private Transform _tileParent;

    [Header("Camera Related")]
    [SerializeField] private Transform _cameraHelper;

    private bool _init;
    private BoardTeam _team;
    private BoardTile[,] _tiles;
    private BoardSettings _settings;

    #region Init

    public void SetSettings(BoardSettings settings)
    {
        _settings = settings;
    }

    public void SetTeam(BoardTeam team)
    {
        _team = team;

        AssignTeamStartPosition();
    }

    private void AssignTeamStartPosition()
    {

    }

    public void SpawnTiles()
    {
        _tiles = new BoardTile[_settings.boardDimensionSize, _settings.boardDimensionSize];

        for (int i = 0; i < _settings.boardDimensionSize; i++)
        {
            for (int j = 0; j < _settings.boardDimensionSize; j++)
            {
                BoardTile tile = Instantiate(_settings.tileRef, _tileParent);
                float scaleX = tile.transform.GetScaleX();
                float scaleZ = tile.transform.GetScaleZ();

                tile.transform.localPosition = new Vector3(i * scaleX, 0f, j * scaleZ);

                _tiles[i, j] = tile;
            }
        }

        _init = true;
    }

    #endregion

    public void Clean()
    {
        if (!_init) return;

        for (int i = 0; i < _settings.boardDimensionSize; i++)
        {
            for (int j = 0; j < _settings.boardDimensionSize; j++)
            {
                Destroy(_tiles[i, j].gameObject);
            }
        }

        _tiles = null;
    }
}
