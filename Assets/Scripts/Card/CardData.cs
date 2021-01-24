using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardData : MonoBehaviour
{
    public virtual CardType type => CardType.None;

    [SerializeField] private Sprite _cardSprite;
    [SerializeField] private string _description;

    public Sprite GetCardSprite()
    {
        return _cardSprite;
    }

    public string GetDescription()
    {
        return _description;
    }
}

public enum CardType
{
    None = 0,

    Movement = 1,
    Stats_Buff = 2,
    Terrain_Buff = 3,
    Stats_DeBuff = 4,
    Terrain_DeBuff = 5,
}