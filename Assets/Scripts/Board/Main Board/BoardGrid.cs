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

    #region Helpers

    public BoardTile GetRandomTileForBall()
    {
        BoardTile tile = _tiles.RandomElement();
        tile.SetBallHighlight(true);

        return tile;
    }

    #endregion
}
