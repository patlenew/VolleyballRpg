using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckData : MonoBehaviour
{
    [SerializeField] private CardData[] _cardData;

    #region Helpers

    public CardData[] GetCardData()
    {
        return _cardData;
    }

    #endregion
}
