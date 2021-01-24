using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deck Visual, where you see the actual cards
// Deck logic is here also for now
public class DeckPanel : MonoBehaviour
{
    [Header("Deck Settings")]
    [SerializeField] private Card _cardRef;
    [SerializeField] private Transform _cardParent;

    private DeckData _data;
    private Stack<Card> _cards = new Stack<Card>();
    private List<Card> _discardedCards = new List<Card>();

    private void OnReceiveDeckData(DeckData data)
    {
        _data = data;

        _cards = CreateDeck();
    }

    public void Reshuffle()
    {

    }

    public void Draw(int count)
    {
        for (int i = 0; i < count; i++)
        {
            
        }
    }


    #region Helpers

    private Stack<Card> CreateDeck()
    {
        Stack<Card> newDeck = new Stack<Card>();
        CardData[] cardData = _data.GetCardData();

        for (int i = 0; i < cardData.Length; i++)
        {
            Card card = CreateCard(cardData[i]);

            newDeck.Push(card);
        }

        newDeck.ToArray().Shuffle();

        return newDeck;
    }

    private Card CreateCard(CardData data)
    {
        Card card = Instantiate(_cardRef, _cardParent);
        card.SetData(data);

        return card;
    }

    #endregion
}
