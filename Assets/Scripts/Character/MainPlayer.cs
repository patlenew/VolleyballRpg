using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _showDuration = 0.5f;

    [Header("References")]
    [SerializeField] private SpriteRenderer _worldSprite;
    [SerializeField] private BoardTeam _currentTeam;

    private int _currentHp;
    private int _currentCardTurns;

    public void SetBaseData(BoardTeamData data)
    {
        _currentHp = data.baseBallPower;
        _currentCardTurns = data.baseReflex;
    }

    public void Show()
    {
        DOTween.Kill(_worldSprite);
        _worldSprite.DOColor(Color.white, _showDuration);
    }

    public void Hide()
    {
        DOTween.Kill(_worldSprite);
        _worldSprite.DOColor(Color.clear, _showDuration);
    }

    #region Helpers

    public BoardTeam GetTeam()
    {
        return _currentTeam;
    }

    #endregion
}
