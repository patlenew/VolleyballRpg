using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardSelectable
{
    bool CanBeSelected();

    bool Selected();

    void OnTarget(bool targeted);

    void SetSelected(bool selected);

    void OnConfirmSelection();
}
