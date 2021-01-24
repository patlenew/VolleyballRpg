using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnemy : MonoBehaviour
{
    [SerializeField] private BoardTeam _currentTeam;

    #region Helpers

    public BoardTeam GetTeam()
    {
        return _currentTeam;
    }

    #endregion
}
