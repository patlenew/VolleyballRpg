using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reflex Buff Card Data", menuName = "VolleyBall RPG/Card Data/Reflex Buff Card Data", order = 2)]
public class ReflexBuffCardData : CardData
{
    public StatType StatType => StatType.Reflex;

    [Header("Reflex Card Settings")]
    [SerializeField] private int reflexIncrease = 1;

    public override int GetBaseValue()
    {
        return reflexIncrease;
    }

    public override int GetReflexCost()
    {
        return 0;
    }
}

public enum StatType
{
    None = 0,

    Reflex = 1,
    Power = 2,

}
