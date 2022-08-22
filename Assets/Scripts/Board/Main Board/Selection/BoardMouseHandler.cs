using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMouseHandler : Singleton<BoardMouseHandler>
{
    private bool _selecting;
    private IBoardSelectable _currentHoveredSelectable;
    private IBoardSelectable _currentSelectable;

    public void SetSelecting(bool selecting)
    {
        _selecting = selecting;
    }

    private void Update()
    {
        if (_selecting)
        {
            HandleMouseHover();

            if (_currentHoveredSelectable != null)
            {
                HandleClick();
            }
        }
    }

    #region Hover

    private void HandleMouseHover()
    {
        Ray ray = CameraManager.Instance.camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 9999f, Layer.BoardRelated))
        {
            Debug.Log(hit.transform.name + " im hitting!!!");

            IBoardSelectable selectable = hit.transform.GetComponentInParent<IBoardSelectable>();

            if (selectable != null)
            {
                SetNewSelection(selectable);
            }
            else
            {
                RemoveCurrentSelection();
            }
        }
        else
        {
            RemoveCurrentSelection();
        }
    }

    private void SetNewSelection(IBoardSelectable selectable)
    {
        _currentHoveredSelectable?.OnTarget(false);

        selectable.OnTarget(true);
        _currentHoveredSelectable = selectable;
    }

    private void RemoveCurrentSelection()
    {
        _currentHoveredSelectable?.OnTarget(false);
        _currentHoveredSelectable = null;
    }

    #endregion

    #region Clicking

    // Click on something targeted
    private void HandleClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _currentSelectable?.SetSelected(false);

            _currentSelectable = _currentHoveredSelectable;
            _currentSelectable.SetSelected(true);
        }
    }

    private void OnCancelSelection()
    {
        _currentSelectable = null;
    }

    #endregion

}
