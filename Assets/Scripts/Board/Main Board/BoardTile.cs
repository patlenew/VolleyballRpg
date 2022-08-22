using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour, IBoardSelectable
{
    [SerializeField] private GameObject _ballHighlight;

    [Header("Selectable Settings")]
    [SerializeField] private Collider _collision;
    [SerializeField] private GameObject _selectableObject;
    [SerializeField] private GameObject _selectedVisual;
    [SerializeField] private GameObject _hoveredObject;

    private bool _selected;
    private bool _selectable;
    private BoardCharacter _character;

    private void Start()
    {
        OnTarget(false);
        SetSelected(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetCharacter(BoardCharacter character)
    {
        _character = character;
    }

    public void SetBallHighlight(bool ballGoal)
    {
        _ballHighlight.SetActive(ballGoal);
    }

    #region Selection

    public void SetSelectable(bool selectable)
    {
        _selectable = selectable;

        _collision.enabled = _selectable;
        _selectableObject.SetActive(selectable);
    }

    #endregion

    public bool IsEmpty()
    {
        return _character != null;
    }

    public BoardCharacter GetCharacter()
    {
        return _character;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    #region Selectable

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
        _hoveredObject.SetActive(targeted);
    }

    public void SetSelected(bool selected)
    {
        _selected = selected;

        _selectedVisual.SetActive(_selectable);
    }

    public void OnConfirmSelection()
    {
        
    }

    #endregion


}
