using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardCharacter : MonoBehaviour, IBoardSelectable
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Collider _clickDetectionCollider;

    [Header("Selection Settings")]
    [SerializeField] private GameObject _hoverHighlight;
    [SerializeField] private GameObject _selectionHighlight;

    private bool _selected;
    private bool _selectable;
    private BoardTile _tile;
    private BoardCharacterData _data;

    private void Start()
    {
        OnTarget(false);
        SetSelected(false);
    }

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

    public void SetHighlight(bool highlight)
    {
        _selectionHighlight.SetActive(highlight);
    }

    #region Battle Flow Related

    public void SetSelectable(bool selectable)
    {
        _selectable = selectable;

        _clickDetectionCollider.enabled = selectable;
    }

    public void CatchBall(Transform ball, TweenCallback onComplete)
    {

    }

    #endregion

    #region Selection

    public override string ToString()
    {
        return _data.characterName;
    }

    public bool CanBeSelected()
    {
        return _selectable;
    }

    public bool Selected()
    {
        return _selected;
    }

    public void OnTarget(bool targeted)
    {
        _hoverHighlight.SetActive(targeted);
    }

    public void SetSelected(bool selected)
    {
        _selected = selected;

        SetHighlight(selected);
    }

    public void OnConfirmSelection()
    {
        GameEvents.OnSelectCharacter.Invoke(this);
    }

    #endregion
}
