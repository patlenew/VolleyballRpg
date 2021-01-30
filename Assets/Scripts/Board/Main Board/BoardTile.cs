using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    private bool _ballGoal;
    private BoardCharacter _character;

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
        _ballGoal = ballGoal;
    }

    public bool IsEmpty()
    {
        return _character != null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
