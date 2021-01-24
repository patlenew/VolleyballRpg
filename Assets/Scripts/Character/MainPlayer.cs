using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    [SerializeField] private BoardTeam _currentTeam;

    private int _currentHp;
    private int _currentCardTurns;

    public void SetBaseData(BoardTeamData data)
    {
        _currentHp = data.baseBallPower;
        _currentCardTurns = data.baseReflex;
    }

    #region Helpers

    public BoardTeam GetTeam()
    {
        return _currentTeam;
    }

    #endregion
}
