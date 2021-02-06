using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

// TO THINK: Right now it's all in scriptable object, think about a better way so create new cards (JSON? Excel?)
public abstract class CardData : ScriptableObject
{
    public virtual CardType type => CardType.None;

    [Header("Base Card Settings")]
    [SerializeField] private Sprite _cardSprite;
    [SerializeField] private string _description;

    [Header("Base Card Optional Settings")]
    [SerializeField] private bool _destroyAfterPlay; // Destroy for the rest of the fight
    [SerializeField] private bool _makeDraw;
    [ConditionalField(nameof(_makeDraw))] [SerializeField] private DrawCardOption _drawOption;

    public Sprite GetCardSprite()
    {
        return _cardSprite;
    }

    public string GetDescription()
    {
        return _description;
    }

    public abstract int GetBaseValue();

    public abstract int GetReflexCost();
}

public class DrawCardOption
{
    public int drawCount = 1;
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