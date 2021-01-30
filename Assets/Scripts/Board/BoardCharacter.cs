using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    private BoardTile _tile;
    private BoardCharacterData _data;

    public void Init(BoardCharacterData data)
    {
        _data = data;

        _renderer.sprite = _data.characterSprite;

        FadeIn();
    }

    public void FadeIn()
    {
        DOTween.Kill(_renderer);

        _renderer.color = Color.clear;
        _renderer.DOColor(Color.white, 1f);
    }

    public void SetTile(BoardTile tile)
    {
        _tile = tile;

        transform.position = _tile.GetPosition();
    }

    public void FadeOut()
    {
        DOTween.Kill(_renderer);

        _renderer.color = Color.white;
        _renderer.DOColor(Color.clear, 1f);
    }
}
