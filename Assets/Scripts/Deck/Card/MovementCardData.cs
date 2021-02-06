using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Card Data", menuName = "VolleyBall RPG/Card Data/Movement Card Data", order = 1)]
public class MovementCardData : CardData
{
    public override CardType type => CardType.Movement;

    [Header("Movement Card Setting")]
    [SerializeField] private int _movementGrant = 1;

    public override int GetBaseValue()
    {
        return _movementGrant;
    }

    public override int GetReflexCost()
    {
        return -1;
    }
}

