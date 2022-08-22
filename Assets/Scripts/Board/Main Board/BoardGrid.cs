using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoardGrid : MonoBehaviour
{
    [Header("Characters Related")]
    [SerializeField] private BoardCharacter _characterRef;
    [SerializeField] private Transform _charactersParent;

    [Header("Tiles Related")]
    [SerializeField] private Transform _tileParent;

    [Header("Camera Related")]
    [SerializeField] private Transform _cameraHelper;
    [SerializeField] private Transform _playCardCameraHelper;

    private bool _init;
    private BoardTeam _team;
    private BoardTile[,] _tiles;
    private BoardSettings _settings;

    private List<BoardCharacter> _boardCharacters = new List<BoardCharacter>();

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
        _boardCharacters.Clear();

        _tiles.Copy(out BoardTile[,] shuffledTiles);

        shuffledTiles.ShuffleGrid();

        int characterIndex = 0;

        for (int i = 0; i < shuffledTiles.GetLength(0); i++)
        {
            for (int j = 0; j < shuffledTiles.GetLength(1); j++)
            {
                BoardCharacter character = CreateBoardCharacter(shuffledTiles[i, j], _team.GetCharactersData()[characterIndex]);

                _boardCharacters.Add(character);

                characterIndex++;

                if (characterIndex >= _team.GetCharactersData().Length)
                {
                    break;
                }
            }

            if (characterIndex >= _team.GetCharactersData().Length)
            {
                break;
            }
        }
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
                tile.SetActive(true);

                _tiles[i, j] = tile;
            }
        }

        _init = true;
    }

    private BoardCharacter CreateBoardCharacter(BoardTile tile, BoardCharacterData data)
    {
        BoardCharacter character = Instantiate(_characterRef, _charactersParent);
        character.Init(data);
        character.SetTile(tile);

        tile.SetCharacter(character);

        return character;
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

    #region Battle Flow Related

    public void SetCharactersSelectable(bool selectable)
    {
        for (int i = 0; i < _boardCharacters.Count; i++)
        {
            _boardCharacters[i].SetSelectable(selectable);
        }
    }

    public void ShowMovingPossibilities(BoardCharacter character, int moveDistance)
    {
        int2 startIndex = GetTileIndexFromCharacter(character);

        for (int x = -moveDistance; x < moveDistance; x++)
        {
            for (int y = -moveDistance; y < moveDistance; y++)
            {
                if (_tiles[x, y].IsEmpty())
                {
                    _tiles[x, y].SetSelectable(true);
                }
            }
        }

    }

    #endregion

    #region Helpers

    public BoardSettings GetSettings()
    {
        return _settings;
    }

    public BoardTeamData GetTeamData()
    {
        return _team.GetTeamData();
    }

    public BoardTile GetRandomTileForBall()
    {
        BoardTile tile = _tiles.RandomElement();
        tile.SetBallHighlight(true);

        return tile;
    }

    private int2 GetTileIndexFromCharacter(BoardCharacter character)
    {
        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                bool isRightCharacter = _tiles[x, y].GetCharacter() == character;

                if (isRightCharacter)
                {
                    return new int2(x, y);
                }
            }
        }

        Debug.LogError("Could not find character on board -> " + character);
        return new int2(-1, -1);
    }

    public Transform GetBaseCameraHelper()
    {
        return _cameraHelper;
    }

    public Transform GetPlayedCardCameraHelper()
    {
        return _playCardCameraHelper;
    }

    #endregion
}
