using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// To keep in mind: maybe have different card per class if there also multiple "classes"
public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _cardImage;
    [SerializeField] private TMP_Text _descriptionTitle;
    [SerializeField] private Transform _visualsParent;

    [Header("Selected Settings")]
    [SerializeField] private float _selectedAnimDuration = 0.5f;
    [SerializeField] private GameObject _selectedObject;
    [SerializeField] private Transform _selectionGoalPoint;

    private CardData _data;

    private void Start()
    {
        SetSelected(false);
    }

    public void SetData(CardData data)
    {
        _data = data;

        _cardImage.sprite = data.GetCardSprite();
        _descriptionTitle.text = data.GetDescription();
    }

    // TO ADD: Draw/Discard Anim
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    #region Interaction

    public void SetSelected(bool selected)
    {
        _selectedObject.SetActive(selected);

        PlaySelectedCardAnim(selected);
    }

    private void PlaySelectedCardAnim(bool selected)
    {
        Vector3 goal = selected ? _selectionGoalPoint.localPosition : Vector3.zero;

        DOTween.Kill(_visualsParent);
        _visualsParent.DOLocalMove(goal, _selectedAnimDuration);
    }

    #endregion

    #region Helpers

    public CardData GetData()
    {
        return _data;
    }

    #endregion

}
